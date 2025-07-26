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
    public partial class createCommande: Form
    {
       
       
        private CommanderRepo repo = new CommanderRepo();
        public bool IsSearchMode { get; set; } = false;
       

        public createCommande()
        {
            InitializeComponent();
            LoadClients();
        }

        private void createCommande_Load(object sender, EventArgs e)
        {
            if (IsSearchMode)
            {
                ConfigureSearchMode();
            }
        }

        private void ConfigureSearchMode()
        {
            this.Text = "Rechercher Commande";

            // Masquer les éléments inutiles pour la recherche
            dateCommandeDateTimePicker.Visible = false;
            label2.Visible = false;
            clientComboBox.Visible = false;
            label3.Visible = false;

            // Modifier le texte du bouton
            addButton.Text = "Rechercher";
        }
        
        private void LoadClients()
        {
            var clients = repo.GetClientsForComboBox();
            clientComboBox.DisplayMember = "FullName";
            clientComboBox.ValueMember = "id";

            foreach (var client in clients)
            {
                clientComboBox.Items.Add(new
                {
                    FullName = $"{client.nom} {client.prenom}",
                    id = client.id
                });
            }
        }
        public void EditCommande(Commande commande)
        {
            this.Text = "Modifier Commande";
            this.numeroCommandeTextBox.Text = commande.n_commande;
            this.dateCommandeDateTimePicker.Value = commande.date_commande;

            // Select the client in combobox
            foreach (var item in clientComboBox.Items)
            {
                dynamic clientItem = item;
                if (clientItem.id == commande.client_id)
                {
                    clientComboBox.SelectedItem = item;
                    break;
                }
            }

            numeroCommandeTextBox.Enabled = false; // Don't allow editing the command number
            addButton.Text = "Modifier";
        }


        private void numeroCommandeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateCommandeDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void clientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            if (IsSearchMode)
            {
                // En mode recherche, on ferme juste le formulaire avec OK
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (string.IsNullOrWhiteSpace(numeroCommandeTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un numéro de commande", "Erreur de validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client", "Erreur de validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var commande = new Commande
                {
                    n_commande = numeroCommandeTextBox.Text.Trim(),
                    date_commande = dateCommandeDateTimePicker.Value,
                    client_id = ((dynamic)clientComboBox.SelectedItem).id
                };

                bool success;
                if (addButton.Text == "Modifier")
                {
                    success = repo.UpdateCommande(commande);
                }
                else
                {
                    success = repo.CreateCommande(commande);
                }

                if (success)
                {
                    MessageBox.Show("Commande enregistrée avec succès!", "Succès",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement: {ex.Message}", "Erreur",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
