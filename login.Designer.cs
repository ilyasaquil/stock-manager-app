namespace LOGIN
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.boxPasswd = new System.Windows.Forms.CheckBox();
            this.txtUser = new System.Windows.Forms.Label();
            this.txtPasswd = new System.Windows.Forms.Label();
            this.usertxt = new System.Windows.Forms.TextBox();
            this.passwdtxt = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelmssg = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.closebutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // boxPasswd
            // 
            this.boxPasswd.AutoSize = true;
            this.boxPasswd.Location = new System.Drawing.Point(350, 178);
            this.boxPasswd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.boxPasswd.Name = "boxPasswd";
            this.boxPasswd.Size = new System.Drawing.Size(99, 17);
            this.boxPasswd.TabIndex = 0;
            this.boxPasswd.Text = "show password";
            this.boxPasswd.UseVisualStyleBackColor = true;
            this.boxPasswd.CheckedChanged += new System.EventHandler(this.boxPasswd_CheckedChanged);
            // 
            // txtUser
            // 
            this.txtUser.AutoSize = true;
            this.txtUser.Location = new System.Drawing.Point(274, 113);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(29, 13);
            this.txtUser.TabIndex = 1;
            this.txtUser.Text = "User";
            this.txtUser.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtPasswd
            // 
            this.txtPasswd.AutoSize = true;
            this.txtPasswd.Location = new System.Drawing.Point(274, 152);
            this.txtPasswd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.Size = new System.Drawing.Size(53, 13);
            this.txtPasswd.TabIndex = 2;
            this.txtPasswd.Text = "Password";
            this.txtPasswd.Click += new System.EventHandler(this.txtPasswd_Click);
            // 
            // usertxt
            // 
            this.usertxt.Location = new System.Drawing.Point(350, 113);
            this.usertxt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usertxt.Name = "usertxt";
            this.usertxt.Size = new System.Drawing.Size(103, 20);
            this.usertxt.TabIndex = 4;
            this.usertxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // passwdtxt
            // 
            this.passwdtxt.Location = new System.Drawing.Point(350, 150);
            this.passwdtxt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.passwdtxt.Name = "passwdtxt";
            this.passwdtxt.PasswordChar = '*';
            this.passwdtxt.Size = new System.Drawing.Size(103, 20);
            this.passwdtxt.TabIndex = 5;
            this.passwdtxt.TextChanged += new System.EventHandler(this.passwdtxt_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 21);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(223, 247);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // labelmssg
            // 
            this.labelmssg.AutoSize = true;
            this.labelmssg.ForeColor = System.Drawing.Color.Red;
            this.labelmssg.Location = new System.Drawing.Point(335, 56);
            this.labelmssg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelmssg.Name = "labelmssg";
            this.labelmssg.Size = new System.Drawing.Size(0, 13);
            this.labelmssg.TabIndex = 7;
            this.labelmssg.Click += new System.EventHandler(this.label3_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(360, 199);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 24);
            this.buttonLogin.TabIndex = 8;
            this.buttonLogin.Text = "Log in";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // closebutton
            // 
            this.closebutton.Location = new System.Drawing.Point(360, 228);
            this.closebutton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.closebutton.Name = "closebutton";
            this.closebutton.Size = new System.Drawing.Size(75, 22);
            this.closebutton.TabIndex = 9;
            this.closebutton.Text = "Close";
            this.closebutton.UseVisualStyleBackColor = true;
            this.closebutton.Click += new System.EventHandler(this.closebutton_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.closebutton);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelmssg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.passwdtxt);
            this.Controls.Add(this.usertxt);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.boxPasswd);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox boxPasswd;
        private System.Windows.Forms.Label txtUser;
        private System.Windows.Forms.Label txtPasswd;
        private System.Windows.Forms.TextBox usertxt;
        private System.Windows.Forms.TextBox passwdtxt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelmssg;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button closebutton;
    }
}

