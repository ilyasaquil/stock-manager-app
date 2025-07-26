using LOGIN.models;
using LOGIN.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOGIN
{
    public enum FormMode
    {
        Add,
        Edit,
        Search
    }
    // This form is reused for Adding, Editing, and Searching a Client
    public partial class createClient: Form
    {
        public FormMode Mode { get; set; }
        public createClient()
        {
            InitializeComponent();
            this.Load += createClient_Load; // Hook up the form load event
        }
        
        private void createClient_Load(object sender, EventArgs e)
        {
            if (Mode == FormMode.Search)
            {
                this.titleLabel.Text = "Chercher un client :";
                addButton.Text = "Rechercher"; // Replace saveButton with your actual button name
            }
            else if (Mode == FormMode.Add)
            {
                this.Text = "Ajouter un client";
            }
            else if (Mode == FormMode.Edit)
            {
                this.Text = "Modifier un client";
            }
        }

      
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private int clientid = 0;

        

        public void EditClient(Client client)
        {
           
            this.Text = "Modifier Client :";
            this.titleLabel.Text = "Modifier client :";
            this.idLabel.Text = client.id.ToString();
            this.nomBox.Text = client.nom;
            this.prenomBox.Text = client.prenom;
            this.addrBox.Text = client.adresse;
            this.phoneBox.Text = client.telephone;
            this.gmailBox.Text = client.gmail;

            this.clientid = client.id;

           
            this.addButton.Text = "Modifier";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (Mode == FormMode.Search)
            {
                // Simply close the dialog to trigger search in the calling form
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (!Regex.IsMatch(phoneBox.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Le numéro de téléphone doit contenir exactement 10 chiffres.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gmailBox.Text != "")
            {
                try
                {
                    MailAddress mail = new MailAddress(gmailBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Adresse e-mail invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                // Validate inputs only if adding or editing
                if (string.IsNullOrWhiteSpace(nomBox.Text))
                {
                    MessageBox.Show("Please enter a name", "Validation Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Client client = new Client
                {
                    id = this.clientid,
                    nom = nomBox.Text.Trim(),
                    prenom = prenomBox.Text.Trim(),
                    adresse = addrBox.Text.Trim(),
                    telephone = phoneBox.Text.Trim(),
                    gmail = gmailBox.Text.Trim()
                };

                var repo = new Clientrepo();
                bool success;

                if (client.id == 0)
                {
                    success = repo.CreatedClient(client);
                }
                else
                {
                    success = repo.UpdateClient(client);
                }

                if (success)
                {
                    MessageBox.Show("Client saved successfully!", "Success",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving client: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }
}
