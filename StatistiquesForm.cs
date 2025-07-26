using LOGIN.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOGIN
{
    public partial class StatistiquesForm: Form
    {
        public StatistiquesForm()
        {
            InitializeComponent();
        }

        private void StatistiquesForm_Load(object sender, EventArgs e)
        {
            ChargerStatistiques();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            acceuil acceuil = new acceuil();
            acceuil.Show();
            this.Close();
        }

        private void ChargerStatistiques()
        {
            CommanderRepo commandeRepo = new CommanderRepo();

            // Créer une culture personnalisée pour afficher en dirhams (DH)
            var culture = new System.Globalization.CultureInfo("fr-MA");
            culture.NumberFormat.CurrencySymbol = "DH";

            // Total des commandes
            int totalCommandes = commandeRepo.GetTotalCommandes();
            lblTotalCommandes.Text = $"Total des commandes : {totalCommandes}";

            // Chiffre d’affaires total
            decimal chiffreAffaires = commandeRepo.GetChiffreAffairesTotal();
            lblChiffreAffaires.Text = "Chiffre d'affaires : " + chiffreAffaires.ToString("C", culture);

            // Moyenne par commande
            decimal moyenne = totalCommandes > 0 ? chiffreAffaires / totalCommandes : 0;
            lblMoyenneCommande.Text = "Moyenne par commande : " + moyenne.ToString("C", culture);

            // Nombre de clients uniques
            int nbClients = commandeRepo.GetNombreClientsUniques();
            lblClientsUniques.Text = $"Clients uniques : {nbClients}";

            // Produits les plus commandés
            DataTable dtProduits = commandeRepo.GetProduitsLesPlusCommandes();
            dgvTopProduits.DataSource = dtProduits;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalCommandes_Click(object sender, EventArgs e)
        {

        }

        private void lblChiffreAffaires_Click(object sender, EventArgs e)
        {

        }

        private void lblClientsUniques_Click(object sender, EventArgs e)
        {

        }

        private void dgvTopProduits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            produits form = new produits();
            form.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commandeForm form = new commandeForm();
            form.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clients form = new clients();
            form.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            detail_commandes form = new detail_commandes();
           
            form.Show();
            this.Close();
        }
    }
}
