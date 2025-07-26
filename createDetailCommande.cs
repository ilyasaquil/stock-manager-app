using LOGIN.models;
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
using System.Drawing.Printing;

namespace LOGIN
{
    public partial class createDetailCommande: Form
    {
        private PrintDocument printDocument = new PrintDocument();

        private Commande currentCommande;
        private List<DetailCommande> currentDetails;

        public createDetailCommande()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void LoadCommandeToPrint(Commande commande, List<DetailCommande> details)
        {
            currentCommande = commande;
            currentDetails = details;
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int leftMargin = 50;
            float yPos = 50;
            int lineHeight = 25;

            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font regularFont = new Font("Arial", 12);

            // Titre
            e.Graphics.DrawString("Commande N° " + currentCommande.n_commande, titleFont, Brushes.Black, leftMargin, yPos);
            yPos += lineHeight * 2;

            // Infos commande
            e.Graphics.DrawString($"Date: {currentCommande.date_commande:d}", regularFont, Brushes.Black, leftMargin, yPos);
            yPos += lineHeight;
            e.Graphics.DrawString($"Client: {currentCommande.nom_client}", regularFont, Brushes.Black, leftMargin, yPos);

            yPos += lineHeight * 2;

            // En-tête détails
            e.Graphics.DrawString("Détails de la commande :", headerFont, Brushes.Black, leftMargin, yPos);
            yPos += lineHeight;

            e.Graphics.DrawString("Produit", headerFont, Brushes.Black, leftMargin, yPos);
            e.Graphics.DrawString("Qté", headerFont, Brushes.Black, leftMargin + 300, yPos);
            e.Graphics.DrawString("Prix Unitaire", headerFont, Brushes.Black, leftMargin + 350, yPos);
            e.Graphics.DrawString("Total", headerFont, Brushes.Black, leftMargin + 450, yPos);
            yPos += lineHeight;

            decimal totalCommande = 0;

            // Liste des détails
            foreach (var detail in currentDetails)
            {
                decimal totalDetail = detail.qte_commande * detail.prix_vente;
                totalCommande += totalDetail;

                e.Graphics.DrawString(detail.nom_produit, regularFont, Brushes.Black, leftMargin, yPos);
                e.Graphics.DrawString(detail.qte_commande.ToString(), regularFont, Brushes.Black, leftMargin + 300, yPos);
                e.Graphics.DrawString(detail.prix_vente.ToString("C"), regularFont, Brushes.Black, leftMargin + 350, yPos);
                e.Graphics.DrawString(totalDetail.ToString("C"), regularFont, Brushes.Black, leftMargin + 450, yPos);

                yPos += lineHeight;
            }

            yPos += lineHeight;
            e.Graphics.DrawString("Total Commande : " + totalCommande.ToString("C"), headerFont, Brushes.Black, leftMargin + 350, yPos);
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtQteCommande_TextChanged(object sender, EventArgs e)
        {

        }

        public enum FormMode { Add, Edit }
        public FormMode Mode { get; set; } = FormMode.Add;

        

        public void EditDetail(DetailCommande detail)
        {
            if (detail == null) return;

            Mode = FormMode.Edit;

            txtNCommande.Text = detail.n_commande;
            txtNCommande.Enabled = true;

            // Sélectionne le produit dans la ComboBox via la valeur
            cmbNomProduit.SelectedValue = detail.n_produit;
            cmbNomProduit.Enabled = true;  // On bloque le changement si nécessaire

            txtQteCommande.Text = detail.qte_commande.ToString();
            txtPrixVente.Text = detail.prix_vente.ToString("F2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string n_commande = txtNCommande.Text.Trim();

            if (string.IsNullOrEmpty(n_commande) || cmbNomProduit.SelectedIndex == -1
                || string.IsNullOrEmpty(txtQteCommande.Text) || string.IsNullOrEmpty(txtPrixVente.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int n_produit = (int)cmbNomProduit.SelectedValue;

            if (!int.TryParse(txtQteCommande.Text.Trim(), out int qte_commande))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            if (!decimal.TryParse(txtPrixVente.Text.Trim(), out decimal prix_vente))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            var repo = new DetailCommandeRepo();

            if (Mode == FormMode.Add)
            {
                var newDetail = new DetailCommande
                {
                    n_commande = n_commande,
                    n_produit = n_produit,
                    qte_commande = qte_commande,
                    prix_vente = prix_vente
                };

                if (repo.Exists(n_commande, n_produit))
                {
                    MessageBox.Show("This detail already exists.");
                    return;
                }

                if (repo.Add(newDetail))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add detail.");
                }
            }
            else if (Mode == FormMode.Edit)
            {
                var detail = repo.GetById(n_commande, n_produit);
                if (detail == null)
                {
                    MessageBox.Show("Detail not found.");
                    return;
                }

                detail.qte_commande = qte_commande;
                detail.prix_vente = prix_vente;

                if (repo.Update(detail))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update detail.");
                }
            }
        }

        private void createDetailCommande_Load(object sender, EventArgs e)
        {
            var produitRepo = new Produitrepo();
            var produits = produitRepo.GetAllProduits();

            cmbNomProduit.DataSource = produits;
            cmbNomProduit.DisplayMember = "nom_produit";
            cmbNomProduit.ValueMember = "n_produit";
            cmbNomProduit.SelectedIndex = -1;

            // 🔁 Lier l'événement APRÈS avoir rempli la source
            cmbNomProduit.SelectedIndexChanged += cmbNomProduit_SelectedIndexChanged;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            commandeForm commandeForm = new commandeForm();
            commandeForm.Show();

            this.Close();
        }
        private void cmbNomProduit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNomProduit.SelectedItem is Produit selectedProduit)
            {
                txtPrixVente.Text = selectedProduit.prix_produit.ToString("F2"); // en format décimal
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (currentCommande == null || currentDetails == null || currentDetails.Count == 0)
            {
                MessageBox.Show("Aucune commande chargée pour impression.");
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void txtNCommande_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
