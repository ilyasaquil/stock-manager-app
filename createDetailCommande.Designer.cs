﻿namespace LOGIN
{
    partial class createDetailCommande
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNCommande = new System.Windows.Forms.TextBox();
            this.txtQteCommande = new System.Windows.Forms.TextBox();
            this.txtPrixVente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbNomProduit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtNCommande
            // 
            this.txtNCommande.Location = new System.Drawing.Point(305, 125);
            this.txtNCommande.Name = "txtNCommande";
            this.txtNCommande.Size = new System.Drawing.Size(133, 20);
            this.txtNCommande.TabIndex = 0;
            this.txtNCommande.TextChanged += new System.EventHandler(this.txtNCommande_TextChanged);
            // 
            // txtQteCommande
            // 
            this.txtQteCommande.Location = new System.Drawing.Point(305, 211);
            this.txtQteCommande.Name = "txtQteCommande";
            this.txtQteCommande.Size = new System.Drawing.Size(133, 20);
            this.txtQteCommande.TabIndex = 2;
            this.txtQteCommande.TextChanged += new System.EventHandler(this.txtQteCommande_TextChanged);
            // 
            // txtPrixVente
            // 
            this.txtPrixVente.Location = new System.Drawing.Point(305, 253);
            this.txtPrixVente.Name = "txtPrixVente";
            this.txtPrixVente.Size = new System.Drawing.Size(133, 20);
            this.txtPrixVente.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Numéro Commande";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(107, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nom Produit";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quantité Commandée";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(107, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Prix Vente";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(341, 303);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "ajouter";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(341, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbNomProduit
            // 
            this.cmbNomProduit.FormattingEnabled = true;
            this.cmbNomProduit.Location = new System.Drawing.Point(305, 167);
            this.cmbNomProduit.Name = "cmbNomProduit";
            this.cmbNomProduit.Size = new System.Drawing.Size(133, 21);
            this.cmbNomProduit.TabIndex = 10;
            // 
            // createDetailCommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbNomProduit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPrixVente);
            this.Controls.Add(this.txtQteCommande);
            this.Controls.Add(this.txtNCommande);
            this.Name = "createDetailCommande";
            this.Text = "createDetailCommande";
            this.Load += new System.EventHandler(this.createDetailCommande_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNCommande;
        private System.Windows.Forms.TextBox txtQteCommande;
        private System.Windows.Forms.TextBox txtPrixVente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbNomProduit;
    }
}