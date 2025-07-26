using System;

namespace LOGIN.models
{
    public class Produit
    {
        public int n_produit { get; set; }
        public string nom_produit { get; set; } = "";
        public int seuil { get; set; }
        public int qte_stock { get; set; }
        public DateTime? date_peremption { get; set; }
        public decimal prix_produit { get; set; }
    }
}
