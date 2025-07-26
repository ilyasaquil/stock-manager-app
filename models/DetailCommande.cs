namespace LOGIN.models
{
    public class DetailCommande
    {
        public string n_commande { get; set; } = string.Empty;
        public int n_produit { get; set; }
        public int qte_commande { get; set; }
        public decimal prix_vente { get; set; }
        public string nom_produit { get; set; }
        public int quantite { get; set; }
        public decimal prix_unitaire { get; set; }
     
    }
}
