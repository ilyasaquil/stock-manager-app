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
    public partial class clients: Form
    {

        public clients()
        {
            InitializeComponent();
            SetupDataGridViewColumns(); // Call this before loading data
            ReadClients();
        }

        private void SetupDataGridViewColumns()
        {
            clientsTable.AutoGenerateColumns = false; // Disable auto-generation
            clientsTable.Columns.Clear();

            // Add columns manually
            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "id" // Must match the property name in your Client class
            });

            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNom",
                HeaderText = "Nom",
                DataPropertyName = "nom"
            });

            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrenom",
                HeaderText = "Prénom",
                DataPropertyName = "prenom"
            });

            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdresse",
                HeaderText = "Adresse",
                DataPropertyName = "adresse"
            });

            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTelephone",
                HeaderText = "Téléphone",
                DataPropertyName = "telephone"
            });

            clientsTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGmail",
                HeaderText = "Email",
                DataPropertyName = "gmail"
            });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        
        private void ReadClients()
        {
            var repo = new Clientrepo();
            var clients = repo.GetClients();

            // Clear existing data
            clientsTable.Rows.Clear();

            foreach (var client in clients)
            {
                clientsTable.Rows.Add(
                    client.id,
                    client.nom,
                    client.prenom,
                    client.adresse,
                    client.telephone,
                    client.gmail
                );
            }
        }

        private void clients_Load(object sender, EventArgs e)
        {

            ReadClients();

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void clientsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ajoubouton_Click(object sender, EventArgs e)
        {
            using (var cc = new createClient())  // Add 'using' to properly dispose the form
            {
                if (cc.ShowDialog() == DialogResult.OK)
                {
                    ReadClients();
                }
            }
           
        }




        private void modboutton_Click(object sender, EventArgs e)
        {
            if (clientsTable.SelectedRows.Count == 0) return;

            var val = clientsTable.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            if (int.TryParse(val, out int clientId))
            {
                var repo = new Clientrepo();
                var client = repo.GetClient(clientId);
                if (client == null) return;

                using (var cc = new createClient())
                {
                    cc.EditClient(client);  // This is the correct method name
                    if (cc.ShowDialog() == DialogResult.OK)
                    {
                        ReadClients();
                    }
                }
            }

        }

        private void suppbouton_Click(object sender, EventArgs e)
        {
            if (clientsTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a client to delete.");
                return;
            }

            var val = clientsTable.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            if (!int.TryParse(val, out int clientid))
            {
                MessageBox.Show("Invalid client ID.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to delete this client?",
                "Delete Client",
                MessageBoxButtons.YesNo
            );

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var repo = new Clientrepo();
            repo.DeleteClient(clientid);

            ReadClients(); // Refresh the table
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            
            using (var cc = new createClient())
            {
                cc.Mode = FormMode.Search;
                if (cc.ShowDialog() == DialogResult.OK)
                {
                    string nom = cc.nomBox.Text.Trim();
                    string prenom = cc.prenomBox.Text.Trim();
                    string email = cc.gmailBox.Text.Trim();
                    string adresse = cc.addrBox.Text.Trim();
                    string telephone = cc.phoneBox.Text.Trim();

                    // Si tous les champs sont vides, afficher tous les clients
                    if (string.IsNullOrEmpty(nom) && string.IsNullOrEmpty(prenom)
                        && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(adresse)
                        && string.IsNullOrEmpty(telephone))
                    {
                        ReadClients();
                        return;
                    }

                    var repo = new Clientrepo();
                    var results = repo.AdvancedSearchClients(nom, prenom, email, adresse, telephone);

                    clientsTable.Rows.Clear();

                    if (results.Count == 0)
                    {
                        MessageBox.Show("Aucun client ne correspond à ces critères. Affichage de tous les clients.",
                                        "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReadClients();
                        return;
                    }

                    foreach (var client in results)
                    {
                        clientsTable.Rows.Add(
                            client.id,
                            client.nom,
                            client.prenom,
                            client.adresse,
                            client.telephone,
                            client.gmail
                        );
                    }

                    MessageBox.Show($"{results.Count} client(s) trouvé(s).",
                                    "Résultat de recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            produits produits = new produits();
            produits.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            acceuil acceuil = new acceuil();
            acceuil.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commandeForm commandeForm = new commandeForm();
            commandeForm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            detail_commandes form = new detail_commandes();
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
