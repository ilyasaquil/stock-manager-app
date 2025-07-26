/*LOGIN.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LOGIN.Repo
{
    public class CommanderRepo
    {
        private readonly string connectionString =
            "Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public Commande GetById(string n_commande)
        {
            Commande commande = null;
            using (var connection = new SqlConnection(connectionString))
            {
                // Requête SQL avec jointure client pour récupérer nom et prénom
                string query = @"
            SELECT c.n_commande, c.date_commande, cl.nom + ' ' + cl.prenom AS client_name
            FROM commande c
            JOIN client cl ON c.id = cl.id
            WHERE c.n_commande = @n_commande";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@n_commande", n_commande);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    commande = new Commande
                    {
                        n_commande = reader["n_commande"].ToString(),
                        date_commande = Convert.ToDateTime(reader["date_commande"]),
                        client_name = reader["client_name"].ToString()  // récupère bien le nom complet
                    };
                }
            }
            return commande;
        }
        public List<Commande> GetCommandes()
        {
            var commandes = new List<Commande>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = @"SELECT c.n_commande, c.date_commande, c.id, 
                                 cl.nom + ' ' + cl.prenom as client_name 
                                 FROM commande c 
                                 INNER JOIN client cl ON c.id = cl.id
                                 ORDER BY c.date_commande DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            commandes.Add(new Commande
                            {
                                n_commande = reader.GetString(0),
                                date_commande = reader.GetDateTime(1),
                                client_id = reader.GetInt32(2),
                                client_name = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return commandes;
        }

        public bool CreateCommande(Commande commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO commande (n_commande, date_commande, id) " +
                                "VALUES (@n_commande, @date_commande, @id)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", commande.n_commande);
                        cmd.Parameters.AddWithValue("@date_commande", commande.date_commande);
                        cmd.Parameters.AddWithValue("@id", commande.client_id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating commande: {ex.Message}");
                throw;
            }
        }

        public bool UpdateCommande(Commande commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE commande SET date_commande=@date_commande, " +
                                 "id=@id WHERE n_commande=@n_commande";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", commande.n_commande);
                        cmd.Parameters.AddWithValue("@date_commande", commande.date_commande);
                        cmd.Parameters.AddWithValue("@id", commande.client_id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

        public void DeleteCommande(string n_commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM commande WHERE n_commande=@n_commande";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", n_commande);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

        public List<Client> GetClientsForComboBox()
        {
            var clients = new List<Client>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT id, nom, prenom FROM client ORDER BY nom";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                id = reader.GetInt32(0),
                                nom = reader.GetString(1),
                                prenom = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return clients;
        }
        public int GetTotalCommandes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Commande";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetChiffreAffairesTotal()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(SUM(Total), 0) FROM Commande";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public int GetNombreClientsUniques()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(DISTINCT IdClient) FROM Commande";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetProduitsLesPlusCommandes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT TOP 5 p.NomProduit, SUM(dc.Quantite) AS QuantiteTotale
                    FROM Detail_Commande dc
                    JOIN Produits p ON dc.IdProduit = p.Id
                    GROUP BY p.NomProduit
                    ORDER BY QuantiteTotale DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}*/
using LOGIN.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LOGIN.Repo
{
    public class CommanderRepo
    {
        private readonly string connectionString =
            "Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Commande GetById(string n_commande)
        {
            Commande commande = null;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.n_commande, c.date_commande, cl.nom + ' ' + cl.prenom AS client_name
                    FROM commande c
                    JOIN client cl ON c.id = cl.id
                    WHERE c.n_commande = @n_commande";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@n_commande", n_commande);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    commande = new Commande
                    {
                        n_commande = reader["n_commande"].ToString(),
                        date_commande = Convert.ToDateTime(reader["date_commande"]),
                        client_name = reader["client_name"].ToString()
                    };
                }
            }
            return commande;
        }

        public List<Commande> GetCommandes()
        {
            var commandes = new List<Commande>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = @"
                        SELECT c.n_commande, c.date_commande, c.id, cl.nom + ' ' + cl.prenom as client_name
                        FROM commande c
                        INNER JOIN client cl ON c.id = cl.id
                        ORDER BY c.date_commande DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            commandes.Add(new Commande
                            {
                                n_commande = reader.GetString(0),
                                date_commande = reader.GetDateTime(1),
                                client_id = reader.GetInt32(2),
                                client_name = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return commandes;
        }

        public bool CreateCommande(Commande commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO commande (n_commande, date_commande, id) " +
                                 "VALUES (@n_commande, @date_commande, @id)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", commande.n_commande);
                        cmd.Parameters.AddWithValue("@date_commande", commande.date_commande);
                        cmd.Parameters.AddWithValue("@id", commande.client_id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating commande: {ex.Message}");
                throw;
            }
        }

        public bool UpdateCommande(Commande commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE commande SET date_commande=@date_commande, id=@id WHERE n_commande=@n_commande";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", commande.n_commande);
                        cmd.Parameters.AddWithValue("@date_commande", commande.date_commande);
                        cmd.Parameters.AddWithValue("@id", commande.client_id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

        public void DeleteCommande(string n_commande)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM commande WHERE n_commande=@n_commande";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@n_commande", n_commande);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

        public List<Client> GetClientsForComboBox()
        {
            var clients = new List<Client>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT id, nom, prenom FROM client ORDER BY nom";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                id = reader.GetInt32(0),
                                nom = reader.GetString(1),
                                prenom = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return clients;
        }

        public int GetTotalCommandes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM commande";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetChiffreAffairesTotal()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Calcul chiffre d'affaires total = somme(qte_commande * prix_vente)
                string query = @"
                    SELECT ISNULL(SUM(dc.qte_commande * dc.prix_vente), 0) 
                    FROM detail_commande dc";

                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public int GetNombreClientsUniques()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Nombre distinct de clients dans commandes
                string query = @"
                    SELECT COUNT(DISTINCT c.id) 
                    FROM commande c";

                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetProduitsLesPlusCommandes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT TOP 5 p.nom_produit, SUM(dc.qte_commande) AS QuantiteTotale
                    FROM detail_commande dc
                    JOIN produits p ON dc.n_produit = p.n_produit
                    GROUP BY p.nom_produit
                    ORDER BY QuantiteTotale DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public List<Commande> SearchCommandes(string numeroCommande = null, string nomClient = null)
        {
            var commandes = new List<Commande>();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var sql = new StringBuilder(@"
            SELECT c.n_commande, c.date_commande, c.id, cl.nom + ' ' + cl.prenom as client_name
            FROM commande c
            INNER JOIN client cl ON c.id = cl.id
            WHERE 1=1");

                var cmd = new SqlCommand();

                if (!string.IsNullOrEmpty(numeroCommande))
                {
                    sql.Append(" AND c.n_commande LIKE @numeroCommande");
                    cmd.Parameters.AddWithValue("@numeroCommande", "%" + numeroCommande + "%");
                }

                if (!string.IsNullOrEmpty(nomClient))
                {
                    sql.Append(" AND (cl.nom LIKE @nomClient OR cl.prenom LIKE @nomClient)");
                    cmd.Parameters.AddWithValue("@nomClient", "%" + nomClient + "%");
                }

                sql.Append(" ORDER BY c.date_commande DESC");

                cmd.Connection = con;
                cmd.CommandText = sql.ToString();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        commandes.Add(new Commande
                        {
                            n_commande = reader.GetString(0),
                            date_commande = reader.GetDateTime(1),
                            client_id = reader.GetInt32(2),
                            client_name = reader.GetString(3)
                        });
                    }
                }
            }

            return commandes;
        }
    }
}


