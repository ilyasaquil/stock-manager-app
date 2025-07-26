using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static Produitrepo;
public class Produit
{
    public int n_produit { get; set; }
    public string nom_produit { get; set; }
    public int seuil { get; set; }
    public int qte_stock { get; set; }
    public DateTime? date_peremption { get; set; }
    public decimal prix_produit { get; set; }
}
public class Produitrepo
{
    public int GetNombreTotalProduits()
    {
        int total = 0;
        using (SqlConnection conn = new SqlConnection("Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM produits";
            SqlCommand cmd = new SqlCommand(query, conn);
            total = (int)cmd.ExecuteScalar();
        }
        return total;
    }
    public List<Produit> GetAllProduits()
    {
        List<Produit> produits = new List<Produit>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT n_produit, nom_produit, prix_produit FROM produits", con); // ✅ bien inclure `prix`
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                produits.Add(new Produit
                {
                    n_produit = reader.GetInt32(0),
                    nom_produit = reader.GetString(1),
                    prix_produit = reader.GetDecimal(2) // ✅ correctement assigné
                });
            }
        }

        return produits;
    }


    private readonly string connectionString =
         "Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

     public List<Produit> GetProduits()
     {
         var produits = new List<Produit>();
         using (var con = new SqlConnection(connectionString))
         {
             con.Open();
             string query = "SELECT * FROM produits ORDER BY n_produit DESC";
             var cmd = new SqlCommand(query, con);
             using (var reader = cmd.ExecuteReader())
             {
                 while (reader.Read())
                 {
                     produits.Add(ReadProduit(reader));
                 }
             }
         }
         return produits;
     }

     public Produit GetProduit(int id)
     {
         using (var con = new SqlConnection(connectionString))
         {
             con.Open();
             string query = "SELECT * FROM produits WHERE n_produit = @id";
             var cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@id", id);
             using (var reader = cmd.ExecuteReader())
             {
                 if (reader.Read())
                 {
                     return ReadProduit(reader);
                 }
             }
         }
         return null;
     }

     public bool CreateProduit(Produit produit)
     {
         using (var con = new SqlConnection(connectionString))
         {
             con.Open();
             string query = @"INSERT INTO produits 
                              (nom_produit, seuil, qte_stock, date_peremption, prix_produit) 
                              VALUES (@nom_produit, @seuil, @qte_stock, @date_peremption, @prix_produit)";
             var cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@nom_produit", produit.nom_produit);
             cmd.Parameters.AddWithValue("@seuil", produit.seuil);
             cmd.Parameters.AddWithValue("@qte_stock", produit.qte_stock);
             cmd.Parameters.AddWithValue("@date_peremption", produit.date_peremption);
             cmd.Parameters.AddWithValue("@prix_produit", produit.prix_produit);
             return cmd.ExecuteNonQuery() > 0;
         }
     }

     public bool UpdateProduit(Produit produit)
     {
         using (var con = new SqlConnection(connectionString))
         {
             con.Open();
             string query = @"UPDATE produits SET 
                                 nom_produit = @nom_produit,
                                 seuil = @seuil,
                                 qte_stock = @qte_stock,
                                 date_peremption = @date_peremption,
                                 prix_produit = @prix_produit
                              WHERE n_produit = @n_produit";
             var cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@n_produit", produit.n_produit);
             cmd.Parameters.AddWithValue("@nom_produit", produit.nom_produit);
             cmd.Parameters.AddWithValue("@seuil", produit.seuil);
             cmd.Parameters.AddWithValue("@qte_stock", produit.qte_stock);
             cmd.Parameters.AddWithValue("@date_peremption", produit.date_peremption);
             cmd.Parameters.AddWithValue("@prix_produit", produit.prix_produit);
             return cmd.ExecuteNonQuery() > 0;
         }
     }

     public void DeleteProduit(int id)
     {
        /*using (var con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "DELETE FROM produits WHERE n_produit = @id";
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }*/
        using (var con = new SqlConnection(connectionString))
        {
            con.Open();

            // Supprimer d'abord dans detail_commande
            string deleteDetailCmd = "DELETE FROM detail_commande WHERE n_produit = @id";
            using (var cmdDetail = new SqlCommand(deleteDetailCmd, con))
            {
                cmdDetail.Parameters.AddWithValue("@id", id);
                cmdDetail.ExecuteNonQuery();
            }

            // Ensuite supprimer dans produits
            string deleteProduitCmd = "DELETE FROM produits WHERE n_produit = @id";
            using (var cmdProduit = new SqlCommand(deleteProduitCmd, con))
            {
                cmdProduit.Parameters.AddWithValue("@id", id);
                cmdProduit.ExecuteNonQuery();
            }
        }
    }

    /* public List<Produit> SearchProduits(string nom)
     {
        /*var produits = new List<Produit>();
        using (var con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "SELECT * FROM produits WHERE nom_produit LIKE @nom";
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@nom", "%" + nom + "%");
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    produits.Add(ReadProduit(reader));
                }
            }
        }
        return produits;
        var produits = new List<Produit>();

        using (var con = new SqlConnection(connectionString))
        {
            con.Open();

            var conditions = new List<string>();
            var cmd = new SqlCommand();
            string query = "SELECT * FROM produits";

            // Construction dynamique des conditions de recherche
            if (!string.IsNullOrWhiteSpace(nom))
            {
                conditions.Add("nom_produit LIKE @nom");
                cmd.Parameters.AddWithValue("@nom", "%" + nom + "%");
            }

            if (seuil.HasValue)
            {
                conditions.Add("seuil = @seuil");
                cmd.Parameters.AddWithValue("@seuil", seuil.Value);
            }

            if (qteStock.HasValue)
            {
                conditions.Add("qte_stock = @qte_stock");
                cmd.Parameters.AddWithValue("@qte_stock", qteStock.Value);
            }

            if (datePeremption.HasValue)
            {
                conditions.Add("date_peremption = @date_peremption");
                cmd.Parameters.AddWithValue("@date_peremption", datePeremption.Value);
            }

            if (prix.HasValue)
            {
                conditions.Add("prix_produit = @prix");
                cmd.Parameters.AddWithValue("@prix", prix.Value);
            }

            // Ajouter WHERE si au moins un critère est défini
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            cmd.CommandText = query;
            cmd.Connection = con;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    produits.Add(ReadProduit(reader));
                }
            }
        }

        return produits;
    }*/



    public List<Produit> SearchByProductName(string nomProduit)
    {
        // Si le nom est vide, retourne tous les produits
        if (string.IsNullOrWhiteSpace(nomProduit))
        {
            return GetAllProduits();
        }

        var results = new List<Produit>();

        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var query = "SELECT * FROM produits WHERE nom_produit LIKE @nomProduit";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@nomProduit", "%" + nomProduit.Trim() + "%");

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new Produit
                    {
                        n_produit = (int)reader["n_produit"],
                        nom_produit = reader["nom_produit"].ToString(),
                        seuil = (int)reader["seuil"],
                        qte_stock = (int)reader["qte_stock"],
                        date_peremption = reader["date_peremption"] != DBNull.Value
                            ? (DateTime?)reader["date_peremption"]
                            : null,
                        prix_produit = (decimal)reader["prix_produit"]
                    });
                }
            }
        }

        return results;
    }








    private Produit ReadProduit(SqlDataReader reader)
     {
         return new Produit
         {
             n_produit = (int)reader["n_produit"],
             nom_produit = reader["nom_produit"].ToString(),
             seuil = Convert.ToInt32(reader["seuil"]),
             qte_stock = Convert.ToInt32(reader["qte_stock"]),
             date_peremption = Convert.ToDateTime(reader["date_peremption"]),
             prix_produit = Convert.ToDecimal(reader["prix_produit"])

         };

     }
    public DataTable GetProduitsEnAlertePeremption(int joursAvant = 7)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = @"SELECT nom_produit AS [Nom du produit], 
                                date_peremption AS [Date de péremption] 
                         FROM produits 
                         WHERE date_peremption <= @dateLimite
                         ORDER BY date_peremption ASC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@dateLimite", DateTime.Today.AddDays(joursAvant));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }
}
