using LOGIN.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Produitrepo;

namespace LOGIN
{
    namespace LOGIN.models
    {
        public class Produit
        {
            public int n_produit { get; set; }
            public string nom_produit { get; set; } = "";
            public decimal prix { get; set; }
        }
    }
    public partial class produits: Form
    {
        

        public produits()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.produits_Load);


        }

        private void SetupDataGridViewColumns()
        {
            produitsTable.AutoGenerateColumns = false;
            produitsTable.Columns.Clear();

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNProduit",
                HeaderText = "N° Produit",
                DataPropertyName = "n_produit"
            });

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNomProduit",
                HeaderText = "Nom Produit",
                DataPropertyName = "nom_produit"
            });

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSeuil",
                HeaderText = "Seuil",
                DataPropertyName = "seuil"
            });

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQteStock",
                HeaderText = "Quantité en stock",
                DataPropertyName = "qte_stock"
            });

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDatePeremption",
                HeaderText = "Date Péremption",
                DataPropertyName = "date_peremption"
            });

            produitsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrixProduit",
                HeaderText = "Prix Produit",
                DataPropertyName = "prix_produit"
            });
        }
        private void produits_Load(object sender, EventArgs e)
        {
            SetupDataGridViewColumns();
            ReadProduits();
        }
        private void ReadProduits()
        {
            /*var repo = new Produitrepo();
            var produits = repo.GetProduits();

            produitsTable.Rows.Clear();

            foreach (var produit in produits)
            {
                produitsTable.Rows.Add(
                    produit.n_produit,
                    produit.nom_produit,
                    produit.seuil,
                    produit.qte_stock,
                    produit.date_peremption.ToShortDateString(),
                    produit.prix_produit
                );
            }*/
            var repo = new Produitrepo();
            var produits = repo.GetProduits();

            produitsTable.DataSource = null; // reset
            produitsTable.DataSource = produits;
        }



        private void clientsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private Produitrepo produitsrepo = new Produitrepo();
        private void searchButton_Click(object sender, EventArgs e)
        {
            using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Search;
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    var repo = new Produitrepo();
                    List<Produit> results;

                    if (cp.Tag is createProduit.SearchCriteria criteres &&
                        !string.IsNullOrWhiteSpace(criteres.NomProduit))
                    {
                        // Recherche par nom si un nom est spécifié
                        results = repo.SearchByProductName(criteres.NomProduit);

                        if (results.Count == 0)
                        {
                            MessageBox.Show("Aucun produit ne correspond à ce nom. Affichage de tous les produits.",
                                          "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            results = repo.GetAllProduits(); // Affiche tous les produits si aucun résultat
                        }
                    }
                    else
                    {
                        // Affiche tous les produits si aucun nom n'est spécifié
                        results = repo.GetAllProduits();
                    }

                    produitsTable.DataSource = results;
                    MessageBox.Show($"{results.Count} produit(s) affiché(s).",
                                  "Résultats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
            /*using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Search;
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    string nom = cp.nomProduitTextBox.Text.Trim();
                    int seuil = (int)cp.seuilNumericUpDown.Value;
                    int qteStock = (int)cp.numUpDown2.Value;
                    DateTime datePeremption = cp.dateTimePicker.Value;

                    decimal prix = 0;
                    decimal.TryParse(cp.textBox3.Text.Trim(), out prix);

                    // Vérifie si tous les champs sont vides
                    bool allEmpty = string.IsNullOrEmpty(nom)
                        && seuil == 0
                        && qteStock == 0
                        && cp.textBox3.Text.Trim() == ""
                        && !cp.dateTimePicker.Checked;

                    if (allEmpty)
                    {
                        ReadProduits();
                        return;
                    }

                    // Création de l'objet critère de recherche
                    var criteria = new ProduitSearchCriteria
                    {
                        Nom = string.IsNullOrWhiteSpace(nom) ? null : nom,
                        Seuil = seuil > 0 ? (int?)seuil : null,
                        QteStock = qteStock > 0 ? (int?)qteStock : null,
                        Prix = prix > 0 ? (decimal?)prix : null,
                        DatePeremption = cp.dateTimePicker.Checked ? (DateTime?)datePeremption : null,
                        FilterByDate = cp.dateTimePicker.Checked
                    };

                    var repo = new Produitrepo();
                    var results = repo.AdvancedSearchProduits(criteria);

                    produitsTable.DataSource = null;
                    produitsTable.Rows.Clear();

                    if (results.Count == 0)
                    {
                        MessageBox.Show("Aucun produit ne correspond à ces critères. Affichage de tous les produits.",
                                        "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReadProduits();
                        return;
                    }

                    foreach (var produit in results)
                    {
                        produitsTable.Rows.Add(
                            produit.n_produit,
                            produit.nom_produit,
                            produit.seuil,
                            produit.qte_stock,
                            produit.date_peremption.ToShortDateString(),
                            produit.prix_produit
                        );
                    }

                    MessageBox.Show($"{results.Count} produit(s) trouvé(s).",
                                    "Résultat de recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }


            /*using (var cp = new createProduit()) // reuse a form or create a search dialog
            {
                cp.Mode = createProduit.FormMode.Search; // You should define FormMode if needed
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    string nom = cp.nomProduitTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(nom))
                    {
                        ReadProduits();
                        return;
                    }

                    var repo = new Produitrepo();
                    var results = repo.SearchProduits(nom);

                    produitsTable.Rows.Clear();

                    if (results.Count == 0)
                    {
                        MessageBox.Show("No products found matching the criteria.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReadProduits();
                        return;
                    }

                    foreach (var produit in results)
                    {
                        produitsTable.Rows.Add(
                            produit.n_produit,
                            produit.nom_produit,
                            produit.seuil,
                            produit.qte_stock,
                            produit.date_peremption.ToShortDateString(),
                            produit.prix_produit
                        );
                    }

                    MessageBox.Show($"{results.Count} product(s) found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //2nd
            /*using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Search;
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    string nom = cp.nomProduitTextBox.Text.Trim();

                    var repo = new Produitrepo();
                    var results = string.IsNullOrEmpty(nom)
                        ? repo.GetProduits()
                        : repo.SearchProduits(nom);

                    produitsTable.DataSource = null;
                    produitsTable.DataSource = results;

                    if (results.Count == 0)
                    {
                        MessageBox.Show("Aucun produit trouvé.", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReadProduits();
                    }
                    else
                    {
                        MessageBox.Show($"{results.Count} produit(s) trouvé(s).", "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }*/
            /*using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Search;
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    string nomRecherche = cp.RechercheNom;

                    var repo = new Produitrepo();
                    var results = string.IsNullOrEmpty(nomRecherche)
                        ? repo.GetProduits()
                        : repo.SearchProduits(nomRecherche);

                    produitsTable.DataSource = null;
                    produitsTable.DataSource = results;

                    if (results.Count == 0)
                        MessageBox.Show("Aucun produit trouvé.", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show($"{results.Count} produit(s) trouvé(s).", "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }*/
            /* using (var cp = new createProduit())
             {
                 cp.Mode = createProduit.FormMode.Search;
                 if (cp.ShowDialog() == DialogResult.OK)
                 {
                     var criteres = cp.Tag as Produit;
                     var repo = new Produitrepo();

                     // Récupère tous les produits puis filtre localement
                     var tousProduits = repo.GetProduits();
                     var results = tousProduits.Where(p =>
                         (string.IsNullOrEmpty(criteres.nom_produit) || p.nom_produit.Contains(criteres.nom_produit)) &&
                         (criteres.seuil <= 0 || p.seuil == criteres.seuil) &&
                         (criteres.qte_stock <= 0 || p.qte_stock == criteres.qte_stock) &&
                         (criteres.date_peremption == DateTime.MinValue || p.date_peremption.Date == criteres.date_peremption.Date) &&
                         (criteres.prix_produit <= 0 || p.prix_produit == criteres.prix_produit)
                     ).ToList();

                     produitsTable.DataSource = null;
                     produitsTable.DataSource = results;

                     MessageBox.Show($"{results.Count} produit(s) trouvé(s).", "Résultats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
             }*/
            /*using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Search;

                if (cp.ShowDialog() == DialogResult.OK)
                {
                    if (cp.Tag is createProduit.SearchCriteria criteres)
                    {
                        var repo = new Produitrepo();
                        var tousProduits = repo.GetProduits();

                        var results = tousProduits.AsEnumerable(); // Pour le filtrage en mémoire

                        // Application progressive des filtres
                        if (!string.IsNullOrEmpty(criteres.NomProduit))
                            results = results.Where(p => p.nom_produit.ToLower().Contains(criteres.NomProduit.ToLower()));

                        if (criteres.Seuil.HasValue)
                            results = results.Where(p => p.seuil == criteres.Seuil.Value);

                        if (criteres.QteStock.HasValue)
                            results = results.Where(p => p.qte_stock == criteres.QteStock.Value);

                        if (criteres.DatePeremption.HasValue)
                            results = results.Where(p => p.date_peremption.Date == criteres.DatePeremption.Value.Date);

                        if (criteres.Prix.HasValue)
                            results = results.Where(p => p.prix_produit == criteres.Prix.Value);

                        var resultList = results.ToList();
                        produitsTable.DataSource = resultList;

                        MessageBox.Show(resultList.Count == 0
                            ? "Aucun produit ne correspond aux critères"
                            : $"{resultList.Count} produit(s) trouvé(s)",
                            "Résultats",
                            MessageBoxButtons.OK,
                            resultList.Count > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    }
                }*/

        
        



        private void modboutton_Click(object sender, EventArgs e)
        {
            /*if (produitsTable.SelectedRows.Count == 0) return;

            var val = produitsTable.SelectedRows[0].Cells["colNProduit"].Value?.ToString();
            //var val = produitsTable.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            if (int.TryParse(val, out int produitId))
            {
                var repo = new Produitrepo();
                var produit = repo.GetProduit(produitId);
                if (produit == null) return;

                using (var cp = new createProduit())
                {
                    cp.EditProduit(produit); // Method to load produit in form for editing
                    if (cp.ShowDialog() == DialogResult.OK)
                    {
                        ReadProduits();
                    }
                }
            }*/
            if (produitsTable.SelectedRows.Count == 0) return;

            var selectedProduit = produitsTable.SelectedRows[0].DataBoundItem as Produit;
            if (selectedProduit == null) return;

            using (var cp = new createProduit())
            {
                cp.EditProduit(selectedProduit);
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    ReadProduits(); // refresh
                }
            }

        }

        private void suppbouton_Click(object sender, EventArgs e)
        {
            if (produitsTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            var val = produitsTable.SelectedRows[0].Cells["colNProduit"].Value?.ToString();
            //var val = produitsTable.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            if (!int.TryParse(val, out int produitId))
            {
                MessageBox.Show("Invalid product ID.");
                return;
            }

            var dialogResult = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Delete Product",
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                var repo = new Produitrepo();
                repo.DeleteProduit(produitId);
                ReadProduits();
            }

        }

        private void ajoubouton_Click(object sender, EventArgs e)
        {
            using (var cp = new createProduit())
            {
                cp.Mode = createProduit.FormMode.Create; // 👈 Set the mode to Create
                if (cp.ShowDialog() == DialogResult.OK)
                {
                    ReadProduits(); // 👈 Refresh the table
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void produitsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            clients clients = new clients();
            clients.Show();
            this.Close();
        }

        private void produits_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            acceuil acceuil = new acceuil();
            acceuil.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            acceuil form = new acceuil();
            form.Show();
            this.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            commandeForm form = new commandeForm();
            form.Show();
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            clients form = new clients();
            form.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            detail_commandes detail_Commandes = new detail_commandes();
            detail_Commandes.Show();  // call Show on the instance, not the class
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
