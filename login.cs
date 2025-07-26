using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;   

namespace LOGIN
{
    public partial class login: Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (usertxt.Text == "" || passwdtxt.Text == "")
            {
                labelmssg.Text = "entrez vos informations pour se connecter !";
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=AQUIL\\GSTR2_SERVER;Initial Catalog=Project;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
                String sql = "SELECT * FROM utilisateur WHERE nomutilisateur = @nomutilisateur AND password = @password";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nomutilisateur", usertxt.Text);
                cmd.Parameters.AddWithValue("@password", passwdtxt.Text);
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    acceuil acc = new acceuil();
                    acc.Show();
                    this.Hide();
                }
                else
                {
                    labelmssg.Text = "Utilisateur ou Mot de passe incorrect!";


                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void boxPasswd_CheckedChanged(object sender, EventArgs e)
        {
            passwdtxt.PasswordChar = boxPasswd.Checked ? '\0' : '*';
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPasswd_Click(object sender, EventArgs e)
        {

        }

        private void passwdtxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
