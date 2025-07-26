using System;
using System.Windows.Forms;
using LOGIN.models;

namespace LOGIN
{
    
    public partial class createProduit : Form
    {
        

        public class SearchCriteria
        {
            public string NomProduit { get; set; } = null;
            public int? Seuil { get; set; } = null;
            public int? QteStock { get; set; } = null;
            public DateTime? DatePeremption { get; set; } = null;
            public decimal? Prix { get; set; } = null;
        }
        public string RechercheNom { get; private set; } = "";
        public enum FormMode
        {
            Create,
            Edit,
            Search
        }

        public FormMode Mode { get; set; } = FormMode.Create;

        

        public createProduit()
        {
            InitializeComponent();
            Load += createProduit_Load; // Gère l'affichage selon le mode
        }

        private void createProduit_Load(object sender, EventArgs e)
        {
            if (Mode == FormMode.Search)
            {
                // Masquer tous les autres contrôles
                nProduit.Visible = false;
                nProduitTextBox.Visible = false;
                seuilNumericUpDown.Visible = false;
                numUpDown2.Visible = false;
                dateTimePicker.Visible = false;
                textBox3.Visible = false;
                label3.Visible = false;  // Supposons que vous avez ces labels
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;

                // Seulement le nom du produit visible
                nomProduitTextBox.Visible = true;
                nomProduit.Visible = true;

                saveButton.Text = "Rechercher";
            }

            
            else if (Mode == FormMode.Create)
            {
                this.Text = "Ajout de produit";
                nProduitTextBox.Visible = false;
                nProduitTextBox.Enabled = true;
                seuilNumericUpDown.Enabled = true;
                numUpDown2.Enabled = true;
                dateTimePicker.Enabled = true;
                textBox3.Enabled = true;
                nomProduitTextBox.Enabled = true;

                saveButton.Text = "ajouter";
                saveButton.Visible = true;
            }
            else if (Mode == FormMode.Edit)
            {
                this.Text = "Modification de produit";
                nProduitTextBox.Visible = false;
                nProduitTextBox.Enabled = false; // ID non modifiable
                seuilNumericUpDown.Enabled = true;
                numUpDown2.Enabled = true;
                dateTimePicker.Enabled = true;
                textBox3.Enabled = true;
                nomProduitTextBox.Enabled = true;

                saveButton.Text = "modifier";
                saveButton.Visible = true;
            }
        }

        public void EditProduit(Produit produit)
        {
            if (produit == null) return;

            Mode = FormMode.Edit;

            nProduitTextBox.Text = produit.n_produit.ToString();
            nomProduitTextBox.Text = produit.nom_produit;
            seuilNumericUpDown.Value = produit.seuil;
            numUpDown2.Value = produit.qte_stock;
            if (produit.date_peremption.HasValue)
            {
                dateTimePicker.Checked = true;
                dateTimePicker.Value = produit.date_peremption.Value;
            }
            else
            {
                dateTimePicker.Checked = false;
                // Optionally set to a default date if needed
                // dateTimePicker.Value = DateTime.Today;
            }
            textBox3.Text = produit.prix_produit.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

           if (Mode == FormMode.Search)
            {
                var criteres = new SearchCriteria();

                // Ne pas ajouter de critère si le champ est vide
                if (!string.IsNullOrWhiteSpace(nomProduitTextBox.Text))
                    criteres.NomProduit = nomProduitTextBox.Text.Trim();

                if (seuilNumericUpDown.Value > 0)
                    criteres.Seuil = (int)seuilNumericUpDown.Value;

                if (numUpDown2.Value > 0)
                    criteres.QteStock = (int)numUpDown2.Value;

                if (dateTimePicker.Checked)
                    criteres.DatePeremption = dateTimePicker.Value.Date;

                if (decimal.TryParse(textBox3.Text, out decimal prix) && prix > 0)
                    criteres.Prix = prix;

                this.Tag = criteres;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            

            // Rest of your create/edit logic...
            if (Mode == FormMode.Create)
            {
                if (string.IsNullOrWhiteSpace(nomProduitTextBox.Text) ||
                    !decimal.TryParse(textBox3.Text, out decimal prixValid) || prixValid <= 0)
                {
                    MessageBox.Show("Veuillez remplir tous les champs correctement.", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Parse values with unique variable names
            int produitId = int.TryParse(nProduitTextBox.Text, out int idVal) ? idVal : 0;
            decimal produitPrix = decimal.TryParse(textBox3.Text, out decimal prixVal) ? prixVal : 0;

            var produit = new Produit

            {
                n_produit = produitId,
                nom_produit = nomProduitTextBox.Text.Trim(),
                seuil = (int)seuilNumericUpDown.Value,
                qte_stock = (int)numUpDown2.Value,
                date_peremption = dateTimePicker.Value,
                prix_produit = produitPrix
            };

            var repo = new Produitrepo();
            bool success = false;

            if (Mode == FormMode.Create)
            {
                success = repo.CreateProduit(produit);
            }
            else if (Mode == FormMode.Edit)
            {
                success = repo.UpdateProduit(produit);
            }

            if (success)
            {
                MessageBox.Show("Produit enregistré avec succès.");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement.");
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Events inutilisés (facultatif : à nettoyer si besoin)
        private void label1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void nProduitTextBox_TextChanged(object sender, EventArgs e) { }
        private void nomProduitTextBox_TextChanged(object sender, EventArgs e) { }
        private void seuilNumericUpDown_ValueChanged(object sender, EventArgs e) { }
        private void numUpDown2_ValueChanged(object sender, EventArgs e) { }
        private void dateTimePicker_ValueChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void nomProduit_Click(object sender, EventArgs e)
        {

        }

        private void createProduit_Load_1(object sender, EventArgs e)
        {

        }
    }
}

