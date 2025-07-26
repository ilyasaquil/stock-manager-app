namespace LOGIN
{
    partial class createClient
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.nomLabel = new System.Windows.Forms.Label();
            this.prenomLabel = new System.Windows.Forms.Label();
            this.addrLabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.gmailLabel = new System.Windows.Forms.Label();
            this.prenomBox = new System.Windows.Forms.TextBox();
            this.nomBox = new System.Windows.Forms.TextBox();
            this.addrBox = new System.Windows.Forms.TextBox();
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.gmailBox = new System.Windows.Forms.TextBox();
            this.idvalue = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(154, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(166, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Ajouter un client : ";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(46, 109);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(34, 21);
            this.idLabel.TabIndex = 1;
            this.idLabel.Text = "id : ";
            // 
            // nomLabel
            // 
            this.nomLabel.AutoSize = true;
            this.nomLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomLabel.Location = new System.Drawing.Point(46, 152);
            this.nomLabel.Name = "nomLabel";
            this.nomLabel.Size = new System.Drawing.Size(56, 21);
            this.nomLabel.TabIndex = 1;
            this.nomLabel.Text = "Nom : ";
            // 
            // prenomLabel
            // 
            this.prenomLabel.AutoSize = true;
            this.prenomLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prenomLabel.Location = new System.Drawing.Point(46, 195);
            this.prenomLabel.Name = "prenomLabel";
            this.prenomLabel.Size = new System.Drawing.Size(65, 21);
            this.prenomLabel.TabIndex = 1;
            this.prenomLabel.Text = "Prenom";
            // 
            // addrLabel
            // 
            this.addrLabel.AutoSize = true;
            this.addrLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addrLabel.Location = new System.Drawing.Point(46, 241);
            this.addrLabel.Name = "addrLabel";
            this.addrLabel.Size = new System.Drawing.Size(85, 21);
            this.addrLabel.TabIndex = 1;
            this.addrLabel.Text = "Addresse : ";
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneLabel.Location = new System.Drawing.Point(46, 287);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(91, 21);
            this.phoneLabel.TabIndex = 1;
            this.phoneLabel.Text = "Téléphone : ";
            // 
            // gmailLabel
            // 
            this.gmailLabel.AutoSize = true;
            this.gmailLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gmailLabel.Location = new System.Drawing.Point(46, 336);
            this.gmailLabel.Name = "gmailLabel";
            this.gmailLabel.Size = new System.Drawing.Size(62, 21);
            this.gmailLabel.TabIndex = 1;
            this.gmailLabel.Text = "Gmail : ";
            this.gmailLabel.Click += new System.EventHandler(this.label5_Click);
            // 
            // prenomBox
            // 
            this.prenomBox.Location = new System.Drawing.Point(218, 202);
            this.prenomBox.Name = "prenomBox";
            this.prenomBox.Size = new System.Drawing.Size(164, 22);
            this.prenomBox.TabIndex = 2;
            this.prenomBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // nomBox
            // 
            this.nomBox.Location = new System.Drawing.Point(218, 159);
            this.nomBox.Name = "nomBox";
            this.nomBox.Size = new System.Drawing.Size(164, 22);
            this.nomBox.TabIndex = 2;
            this.nomBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // addrBox
            // 
            this.addrBox.Location = new System.Drawing.Point(218, 248);
            this.addrBox.Name = "addrBox";
            this.addrBox.Size = new System.Drawing.Size(164, 22);
            this.addrBox.TabIndex = 2;
            this.addrBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // phoneBox
            // 
            this.phoneBox.Location = new System.Drawing.Point(218, 294);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(164, 22);
            this.phoneBox.TabIndex = 2;
            this.phoneBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // gmailBox
            // 
            this.gmailBox.Location = new System.Drawing.Point(218, 343);
            this.gmailBox.Name = "gmailBox";
            this.gmailBox.Size = new System.Drawing.Size(164, 22);
            this.gmailBox.TabIndex = 2;
            this.gmailBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // idvalue
            // 
            this.idvalue.AutoSize = true;
            this.idvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idvalue.Location = new System.Drawing.Point(218, 119);
            this.idvalue.Name = "idvalue";
            this.idvalue.Size = new System.Drawing.Size(0, 20);
            this.idvalue.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(231, 407);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(125, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Ajouter";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(231, 447);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(125, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // createClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 585);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.idvalue);
            this.Controls.Add(this.nomBox);
            this.Controls.Add(this.gmailBox);
            this.Controls.Add(this.phoneBox);
            this.Controls.Add(this.addrBox);
            this.Controls.Add(this.prenomBox);
            this.Controls.Add(this.gmailLabel);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.addrLabel);
            this.Controls.Add(this.prenomLabel);
            this.Controls.Add(this.nomLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "createClient";
            this.Text = "createClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nomLabel;
        private System.Windows.Forms.Label prenomLabel;
        private System.Windows.Forms.Label addrLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label gmailLabel;
        private System.Windows.Forms.Label idvalue;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox prenomBox;
        public System.Windows.Forms.TextBox nomBox;
        public System.Windows.Forms.TextBox addrBox;
        public System.Windows.Forms.TextBox phoneBox;
        public System.Windows.Forms.TextBox gmailBox;
    }
}