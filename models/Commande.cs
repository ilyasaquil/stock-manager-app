// In LOGIN.models namespace
using System;

namespace LOGIN.models
{
    public class Commande
    {
        public string n_commande { get; set; } = "";
        public DateTime date_commande { get; set; }
        public int client_id { get; set; }
        public string client_name { get; set; } = ""; // For display purposes
        public string nom_client { get; set; }

    }
}