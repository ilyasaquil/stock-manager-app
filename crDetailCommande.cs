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

namespace LOGIN
{
    public partial class crDetailCommande: Form
    {
        public crDetailCommande()
        {
            InitializeComponent();
        }
        private Commande currentCommande;
        private List<DetailCommande> currentDetails;
        private void btnSave_Click(object sender, EventArgs e)
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
                    // Ouvre explicitement le formulaire de commande
                    commandeForm commandeForm = new commandeForm();
                    commandeForm.Show();

                    // Puis ferme ce formulaire
                    this.Close();
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
        public enum FormMode { Add, Edit }
        public FormMode Mode { get; set; } = FormMode.Add;
        private void crDetailCommande_Load(object sender, EventArgs e)
        {
            var produitRepo = new Produitrepo();
            var produits = produitRepo.GetAllProduits();

            cmbNomProduit.DataSource = produits;
            cmbNomProduit.DisplayMember = "nom_produit";
            cmbNomProduit.ValueMember = "n_produit";
            cmbNomProduit.SelectedIndex = -1;

            cmbNomProduit.SelectedIndexChanged += cmbNomProduit_SelectedIndexChanged;
        }


        public void EditDetail(DetailCommande detail)
        {
            if (detail == null) return;

            Mode = FormMode.Edit;

            txtNCommande.Text = detail.n_commande;
            txtNCommande.Enabled = false;

            // Sélectionne le produit dans la ComboBox via la valeur
            cmbNomProduit.SelectedValue = detail.n_produit;
            cmbNomProduit.Enabled = false;  // On bloque le changement si nécessaire

            txtQteCommande.Text = detail.qte_commande.ToString();
            txtPrixVente.Text = detail.prix_vente.ToString("F2");
        }
        private void cmbNomProduit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNomProduit.SelectedItem is Produit selectedProduit)
            {
                txtPrixVente.Text = selectedProduit.prix_produit.ToString("F2"); // en format décimal
            }
        }

        private void txtNCommande_TextChanged(object sender, EventArgs e)
        {
            
        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            commandeForm commandeForm = new commandeForm();
            commandeForm.Show();

            this.Close();
        }
    }
}
