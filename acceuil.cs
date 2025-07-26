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
    public partial class acceuil: Form
    {
        
        public acceuil()
        {
            InitializeComponent();
            this.Load += new EventHandler(Accueil_Load);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            produits produits = new produits();
            produits.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commandeForm commandes = new commandeForm();
            commandes.Show();
           this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clients clients = new clients();
            clients.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            detail_commandes detail_Commandes = new detail_commandes();
            detail_Commandes.Show();  // call Show on the instance, not the class
            this.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void Accueil_Load(object sender, EventArgs e)
        {
            LoadProduitsEnAlerte();
            ChargerStatistiquesAccueil();

        }
        private void ChargerStatistiquesAccueil()
        {
            Produitrepo produitRepo = new Produitrepo();
            CommanderRepo commandeRepo = new CommanderRepo();

            int totalProduits = produitRepo.GetNombreTotalProduits();
            int totalCommandes = commandeRepo.GetTotalCommandes();
            decimal chiffreAffaires = commandeRepo.GetChiffreAffairesTotal();

            lblTotalProduits.Text = $"\ud83d\udce6 Total produits : {totalProduits}";
            lblTotalCommandes.Text = $"\ud83d\udcdf Total commandes : {totalCommandes}";
            lblChiffreAffaires.Text = $"\ud83d\udcb0 Chiffre d'affaires : {chiffreAffaires:N2} DH";
        }
        private void LoadProduitsEnAlerte()
        {
            Produitrepo produitRepo = new Produitrepo();
            DataTable dtAlerte = produitRepo.GetProduitsEnAlertePeremption();
            dgvAlerteProduits.DataSource = dtAlerte;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatistiquesForm form = new StatistiquesForm();
            form.Show();
            this.Close();
        }

        private void lblTotalCommandes_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            createProduit form = new createProduit();
            form.ShowDialog();
        }
        

        private void lblChiffreAffaires_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalProduits_Click(object sender, EventArgs e)
        {

        }

        private void btnAjouterCommande_Click(object sender, EventArgs e)
        {
            commandeForm form = new commandeForm();
            form.ShowDialog();
        }

        private void btnAjouterClient_Click(object sender, EventArgs e)
        {
            createClient form = new createClient();
            form.ShowDialog();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
