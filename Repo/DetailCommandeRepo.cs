using LOGIN.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LOGIN.Repo
{
    public class DetailCommandeRepo
    {
        private readonly string connectionString =
            "Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<DetailCommande> GetDetailsByCommande(string n_commande)
        {
            var details = new List<DetailCommande>();
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT dc.n_commande, dc.n_produit, dc.qte_commande, dc.prix_vente,
                   p.nom_produit
            FROM detail_commande dc
            INNER JOIN produits p ON dc.n_produit = p.n_produit
            WHERE dc.n_commande = @n_commande";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@n_commande", n_commande);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(new DetailCommande
                        {
                            n_commande = reader["n_commande"].ToString(),
                            n_produit = Convert.ToInt32(reader["n_produit"]),
                            quantite = Convert.ToInt32(reader["qte_commande"]),
                            prix_unitaire = Convert.ToDecimal(reader["prix_vente"]),
                            nom_produit = reader["nom_produit"].ToString()
                        });
                    }
                }
            }
            return details;
        }


        // Get all details
        public List<DetailCommande> GetAll()
        {
            var list = new List<DetailCommande>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM detail_commande ORDER BY n_commande, n_produit";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DetailCommande
                            {
                                n_commande = reader["n_commande"].ToString(),
                                n_produit = (int)reader["n_produit"],
                                qte_commande = (int)reader["qte_commande"],
                                prix_vente = (decimal)reader["prix_vente"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading detail_commande: " + ex.Message);
            }

            return list;
        }

        // Add new detail
        public bool Add(DetailCommande detail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO detail_commande (n_commande, n_produit, qte_commande, prix_vente) " +
                                 "VALUES (@n_commande, @n_produit, @qte_commande, @prix_vente)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", detail.n_commande);
                        cmd.Parameters.AddWithValue("@n_produit", detail.n_produit);
                        cmd.Parameters.AddWithValue("@qte_commande", detail.qte_commande);
                        cmd.Parameters.AddWithValue("@prix_vente", detail.prix_vente);

                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding detail_commande: " + ex.Message);
                return false;
            }
        }

        // Update existing detail
        public bool Update(DetailCommande detail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE detail_commande SET qte_commande=@qte_commande, prix_vente=@prix_vente " +
                                 "WHERE n_commande=@n_commande AND n_produit=@n_produit";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", detail.n_commande);
                        cmd.Parameters.AddWithValue("@n_produit", detail.n_produit);
                        cmd.Parameters.AddWithValue("@qte_commande", detail.qte_commande);
                        cmd.Parameters.AddWithValue("@prix_vente", detail.prix_vente);

                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating detail_commande: " + ex.Message);
                return false;
            }
        }

        // Delete detail by composite key
        public bool Delete(string n_commande, int n_produit)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "DELETE FROM detail_commande WHERE n_commande=@n_commande AND n_produit=@n_produit";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", n_commande);
                        cmd.Parameters.AddWithValue("@n_produit", n_produit);

                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting detail_commande: " + ex.Message);
                return false;
            }
        }

        // Search by n_commande
        public List<DetailCommande> SearchByCommande(string n_commande)
        {
            var list = new List<DetailCommande>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM detail_commande WHERE n_commande LIKE @n_commande";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", "%" + n_commande + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new DetailCommande
                                {
                                    n_commande = reader["n_commande"].ToString(),
                                    n_produit = (int)reader["n_produit"],
                                    qte_commande = (int)reader["qte_commande"],
                                    prix_vente = (decimal)reader["prix_vente"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching detail_commande: " + ex.Message);
            }

            return list;
        }

        // Check if detail exists by composite key
        public bool Exists(string n_commande, int n_produit)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT COUNT(*) FROM detail_commande WHERE n_commande = @n_commande AND n_produit = @n_produit";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", n_commande);
                        cmd.Parameters.AddWithValue("@n_produit", n_produit);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking existence of detail_commande: " + ex.Message);
                return false;
            }
        }

        // Get detail by composite key
        public DetailCommande GetById(string n_commande, int n_produit)
        {
            DetailCommande detail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM detail_commande WHERE n_commande = @n_commande AND n_produit = @n_produit";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", n_commande);
                        cmd.Parameters.AddWithValue("@n_produit", n_produit);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                detail = new DetailCommande
                                {
                                    n_commande = reader["n_commande"].ToString(),
                                    n_produit = (int)reader["n_produit"],
                                    qte_commande = (int)reader["qte_commande"],
                                    prix_vente = (decimal)reader["prix_vente"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting detail_commande: " + ex.Message);
            }

            return detail;
        }
    }
}
