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
    public partial class detail_commandes: Form
    {
        public detail_commandes()
        {
            InitializeComponent();
            SetupDataGridViewColumns();
            ReadDetails();
        }

        private void SetupDataGridViewColumns()
        {
            detailCommandesTable.AutoGenerateColumns = false;
            detailCommandesTable.Columns.Clear();

            detailCommandesTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNCommande",
                HeaderText = "Numéro Commande",
                DataPropertyName = "n_commande"
            });

            detailCommandesTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNProduit",
                HeaderText = "Numéro Produit",
                DataPropertyName = "n_produit"
            });

            detailCommandesTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQteCommande",
                HeaderText = "Quantité Commandée",
                DataPropertyName = "qte_commande"
            });

            detailCommandesTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrixVente",
                HeaderText = "Prix Vente",
                DataPropertyName = "prix_vente"
            });
        }

        private void ReadDetails()
        {
            var repo = new DetailCommandeRepo();
            var details = repo.GetAll();

            detailCommandesTable.Rows.Clear();

            foreach (var detail in details)
            {
                detailCommandesTable.Rows.Add(
                    detail.n_commande,
                    detail.n_produit,
                    detail.qte_commande,
                    detail.prix_vente
                );
            }
        }

        private void detail_commandes_Load(object sender, EventArgs e)
        {
            ReadDetails();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var form = new createDetailCommande()) // You should create this form similarly to createClient
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReadDetails();
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (detailCommandesTable.SelectedRows.Count == 0) return;

            var n_commande = detailCommandesTable.SelectedRows[0].Cells[0].Value?.ToString();
            var n_produitObj = detailCommandesTable.SelectedRows[0].Cells[1].Value;
            if (string.IsNullOrEmpty(n_commande) || n_produitObj == null) return;

            if (!int.TryParse(n_produitObj.ToString(), out int n_produit)) return;

            var repo = new DetailCommandeRepo();
            var detail = repo.GetById(n_commande, n_produit);
            if (detail == null) return;

            using (var form = new createDetailCommande())
            {
                form.EditDetail(detail);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReadDetails();
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (detailCommandesTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a detail to delete.");
                return;
            }

            var n_commande = detailCommandesTable.SelectedRows[0].Cells[0].Value?.ToString();
            var n_produitObj = detailCommandesTable.SelectedRows[0].Cells[1].Value;
            if (string.IsNullOrEmpty(n_commande) || n_produitObj == null) return;

            if (!int.TryParse(n_produitObj.ToString(), out int n_produit)) return;

            var confirm = MessageBox.Show("Are you sure you want to delete this detail?", "Delete Detail", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            var repo = new DetailCommandeRepo();
            repo.Delete(n_commande, n_produit);

            ReadDetails();
        }

        private void detail_commandes_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            acceuil acceuil = new acceuil();
            acceuil.Show();
            this.Close();
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

        private void button7_Click(object sender, EventArgs e)
        {
            StatistiquesForm form = new StatistiquesForm();
            form.Show();
            this.Close();
        }
    }
}
