using LOGIN.models;
using LOGIN.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOGIN
{
    public partial class commandeForm: Form
    {
        private PrintDocument printDocument1 = new PrintDocument();
        private int currentRow = 0;
        private Commande currentCommande;
        private List<DetailCommande> currentDetails;
        private int currentDetailIndex = 0;

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 20;
            int leftMargin = e.MarginBounds.Left;
            Font fontTitle = new Font("Arial", 14, FontStyle.Bold);
            Font font = new Font("Arial", 10);
            int lineHeight = font.Height + 5;

            // Imprimer les infos de la commande
            e.Graphics.DrawString("Commande N°: " + currentCommande.n_commande, fontTitle, Brushes.Black, leftMargin, y);
            y += lineHeight * 2;

            e.Graphics.DrawString("Date: " + currentCommande.date_commande.ToShortDateString(), font, Brushes.Black, leftMargin, y);
            y += lineHeight;

            e.Graphics.DrawString("Client: " + currentCommande.client_name, font, Brushes.Black, leftMargin, y);
            y += lineHeight * 2;

            // Entêtes détails
            int x = leftMargin;
            e.Graphics.DrawString("Produit", font, Brushes.Black, x, y);
            x += 200;
            e.Graphics.DrawString("Quantité", font, Brushes.Black, x, y);
            x += 100;
            e.Graphics.DrawString("Prix Unitaire", font, Brushes.Black, x, y);
            x += 100;
            e.Graphics.DrawString("Total", font, Brushes.Black, x, y);
            y += lineHeight;

            // Imprimer les lignes détails
            while (currentDetailIndex < currentDetails.Count)
            {
                var detail = currentDetails[currentDetailIndex];
                x = leftMargin;

                e.Graphics.DrawString(detail.nom_produit, font, Brushes.Black, x, y);
                x += 200;
                e.Graphics.DrawString(detail.quantite.ToString(), font, Brushes.Black, x, y);
                x += 100;
                e.Graphics.DrawString(detail.prix_unitaire.ToString("N2") + " MAD", font, Brushes.Black, x, y);
                x += 100;
                e.Graphics.DrawString((detail.quantite * detail.prix_unitaire).ToString("N2") + " MAD", font, Brushes.Black, x, y);
                y += lineHeight;

                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    currentDetailIndex++;
                    e.HasMorePages = true; // paginer si trop long
                    return;
                }

                currentDetailIndex++;
            }

            e.HasMorePages = false;
        }
        public commandeForm()
        {
            InitializeComponent();
            SetupDataGridViewColumns();
            ReadCommandes();
        }

        private void SetupDataGridViewColumns()
        {
            commandesDataGridView.AutoGenerateColumns = false;
            commandesDataGridView.Columns.Clear();

            commandesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNumero",
                HeaderText = "Numéro",
                DataPropertyName = "n_commande"
            });

            commandesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDate",
                HeaderText = "Date",
                DataPropertyName = "date_commande",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
            });

            commandesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colClient",
                HeaderText = "Client",
                DataPropertyName = "client_name"
            });
        }

        private void ReadCommandes()
        {
            var repo = new CommanderRepo();
            var commandes = repo.GetCommandes();

            commandesDataGridView.DataSource = commandes;
        }


        private void ajoubouton_Click(object sender, EventArgs e)
        {
            using (var form = new createCommande())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReadCommandes();
                }
            }

        }

        private void modifierButton_Click(object sender, EventArgs e)
        {
           if(commandesDataGridView.SelectedRows.Count == 0) return;

            var selectedCommande = (Commande)commandesDataGridView.SelectedRows[0].DataBoundItem;
            if (selectedCommande == null) return;

            using (var form = new createCommande())
            {
                form.EditCommande(selectedCommande);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReadCommandes();
                }
            }

        }

        private void supprimerButton_Click(object sender, EventArgs e)
        {
            if (commandesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.");
                return;
            }

            var selectedCommande = (Commande)commandesDataGridView.SelectedRows[0].DataBoundItem;
            if (selectedCommande == null) return;

            DialogResult dialogResult = MessageBox.Show(
                "Êtes-vous sûr de vouloir supprimer cette commande?",
                "Supprimer Commande",
                MessageBoxButtons.YesNo
            );

            if (dialogResult == DialogResult.No) return;

            var repo = new CommanderRepo();
            repo.DeleteCommande(selectedCommande.n_commande);

            ReadCommandes();
        }
        
        private void commandeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private CommanderRepo commanderRepo = new CommanderRepo();
        private DetailCommandeRepo detailCommandeRepo = new DetailCommandeRepo();


        private void btnPrintCommande_Click(object sender, EventArgs e)
        {
            
            if (commandesDataGridView.SelectedRows.Count == 0) return;

            var selectedCommande = (Commande)commandesDataGridView.SelectedRows[0].DataBoundItem;
            if (selectedCommande == null) return;

            currentCommande = commanderRepo.GetById(selectedCommande.n_commande);
            currentDetails = detailCommandeRepo.GetDetailsByCommande(selectedCommande.n_commande);
            currentDetailIndex = 0;

            // Retirer ancien event handler pour éviter accumulations
            printDocument1.PrintPage -= printDocument1_PrintPage;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Aperçu avant impression
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument1;
            previewDialog.ShowDialog();

            // Impression réelle (optionnel selon ton flux)
            // printDocument1.Print();

        }

        private void btnPrintNow_Click(object sender, EventArgs e)
        {
            if (commandesDataGridView.SelectedRows.Count == 0) return;

            var selectedCommande = (Commande)commandesDataGridView.SelectedRows[0].DataBoundItem;
            if (selectedCommande == null) return;

            currentCommande = commanderRepo.GetById(selectedCommande.n_commande);
            currentDetails = detailCommandeRepo.GetDetailsByCommande(selectedCommande.n_commande);

            currentDetailIndex = 0;
            currentRow = 0;

            printDocument1.PrintPage -= printDocument1_PrintPage;
            printDocument1.PrintPage += printDocument1_PrintPage;

            printDocument1.Print(); // Impression réelle
        }

        private void button8_Click(object sender, EventArgs e)
        {
            crDetailCommande crDetailCommande = new crDetailCommande();
            crDetailCommande.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            acceuil acceuil = new acceuil();
            acceuil.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatistiquesForm form = new StatistiquesForm();
            form.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            detail_commandes detail_Commandes = new detail_commandes();
            detail_Commandes.Show();  // call Show on the instance, not the class
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clients form = new clients();
            form.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            produits form = new produits();
            form.Show();
            this.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

            using (var cp = new createCommande())
            {
                cp.IsSearchMode = true;
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    string searchTerm = cp.numeroCommandeTextBox.Text.Trim();
                    var repo = new CommanderRepo();
                    var results = repo.SearchCommandes(numeroCommande: searchTerm);

                    commandesDataGridView.DataSource = results;

                    string message = results.Count == 0
                        ? "Aucune commande trouvée"
                        : $"{results.Count} commande(s) trouvée(s)";
                    MessageBox.Show(message, "Résultats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
