using LOGIN.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIN.Repo
{
    public class Clientrepo
    {
        private readonly string connectionString =
    "Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
       
        public List<Client> AdvancedSearchClients(string nom, string prenom, string gmail, string adresse, string telephone)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var query = "SELECT * FROM Client WHERE 1=1";
                var cmd = new SqlCommand();

                if (!string.IsNullOrEmpty(nom))
                {
                    query += " AND nom LIKE @nom";
                    cmd.Parameters.AddWithValue("@nom", "%" + nom + "%");
                }
                if (!string.IsNullOrEmpty(prenom))
                {
                    query += " AND prenom LIKE @prenom";
                    cmd.Parameters.AddWithValue("@prenom", "%" + prenom + "%");
                }
                if (!string.IsNullOrEmpty(gmail))
                {
                    query += " AND gmail LIKE @gmail";
                    cmd.Parameters.AddWithValue("@gmail", "%" + gmail + "%");
                }
                if (!string.IsNullOrEmpty(adresse))
                {
                    query += " AND adresse LIKE @adresse";
                    cmd.Parameters.AddWithValue("@adresse", "%" + adresse + "%");
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    query += " AND telephone LIKE @telephone";
                    cmd.Parameters.AddWithValue("@telephone", "%" + telephone + "%");
                }

                cmd.Connection = conn;
                cmd.CommandText = query;

                var reader = cmd.ExecuteReader();
                var results = new List<Client>();

                while (reader.Read())
                {
                    results.Add(new Client
                    {
                        id = (int)reader["id"],
                        nom = reader["nom"].ToString(),
                        prenom = reader["prenom"].ToString(),
                        adresse = reader["adresse"].ToString(),
                        telephone = reader["telephone"].ToString(),
                        gmail = reader["gmail"].ToString()
                    });
                }

                return results;
            }
        }


        public List<Client> GetClients()
        {
            var clients = new List<Client>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM client ORDER BY id DESC";
                    Console.WriteLine("Executing: " + sql); // Log SQL query

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No rows found in 'client' table."); // Debug
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine($"Found: {reader["nom"]} {reader["prenom"]}"); // Debug
                            clients.Add(new Client
                            {
                                id = reader.GetInt32(0),
                                nom = reader.GetString(1),
                                prenom = reader.GetString(2),
                                adresse = reader.GetString(3),
                                telephone = reader.GetString(4),
                                gmail = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message); // Log errors
            }
            return clients;
        }
        public Client GetClient(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "SELECT * FROM client WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Client client = new Client();
                                client.id = reader.GetInt32(0);
                                client.nom = reader.GetString(1);
                                client.prenom = reader.GetString(2);
                                client.adresse = reader.GetString(3);
                                client.telephone = reader.GetString(4);
                                client.gmail = reader.GetString(5);

                                return client;

                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return null;
        }

        public bool CreatedClient(Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO client (nom, prenom, adresse, telephone, gmail) " +
                                "VALUES (@nom, @prenom, @adresse, @telephone, @gmail)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@nom", client.nom);
                        cmd.Parameters.AddWithValue("@prenom", client.prenom);
                        cmd.Parameters.AddWithValue("@adresse", client.adresse ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@telephone", client.telephone);
                        cmd.Parameters.AddWithValue("@gmail", client.gmail ?? (object)DBNull.Value);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Returns true if at least one row was inserted
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (consider using a proper logger)
                Console.WriteLine($"Error creating client: {ex.Message}");
                throw; // Re-throw the exception to handle in the calling code
            }

        }

        public bool UpdateClient(Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE client SET nom=@nom, prenom=@prenom, adresse=@adresse, " +
                                 "telephone=@telephone, gmail=@gmail WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", client.id);
                        cmd.Parameters.AddWithValue("@nom", client.nom);
                        cmd.Parameters.AddWithValue("@prenom", client.prenom);
                        cmd.Parameters.AddWithValue("@adresse", client.adresse);
                        cmd.Parameters.AddWithValue("@telephone", client.telephone);
                        cmd.Parameters.AddWithValue("@gmail", client.gmail);

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

        public void DeleteClient(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "DELETE FROM client WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }

    }
}
