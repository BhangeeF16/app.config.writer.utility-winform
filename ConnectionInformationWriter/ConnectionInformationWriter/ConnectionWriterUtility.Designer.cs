
namespace ConnectionInformationWriter
{
    partial class ConnectionInfoWriterUtility
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
            this.btnmodifyconstr = new System.Windows.Forms.Button();
            this.btndecrypt = new System.Windows.Forms.Button();
            this.btnencrypt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btngetservers = new System.Windows.Forms.Button();
            this.btngetconstr = new System.Windows.Forms.Button();
            this.cmbdb = new System.Windows.Forms.ComboBox();
            this.txtconstr = new System.Windows.Forms.TextBox();
            this.chkwa = new System.Windows.Forms.CheckBox();
            this.cmbserver = new System.Windows.Forms.ComboBox();
            this.chksqlau = new System.Windows.Forms.CheckBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtsonstrinreg = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnmodifyconstr
            // 
            this.btnmodifyconstr.Location = new System.Drawing.Point(433, 173);
            this.btnmodifyconstr.Name = "btnmodifyconstr";
            this.btnmodifyconstr.Size = new System.Drawing.Size(182, 49);
            this.btnmodifyconstr.TabIndex = 33;
            this.btnmodifyconstr.Text = "Modify Connection String";
            this.btnmodifyconstr.UseVisualStyleBackColor = true;
            this.btnmodifyconstr.Click += new System.EventHandler(this.btnmodify_Click);
            // 
            // btndecrypt
            // 
            this.btndecrypt.Location = new System.Drawing.Point(90, 173);
            this.btndecrypt.Name = "btndecrypt";
            this.btndecrypt.Size = new System.Drawing.Size(73, 49);
            this.btndecrypt.TabIndex = 35;
            this.btndecrypt.Text = "Decrypt Connection Strings";
            this.btndecrypt.UseVisualStyleBackColor = true;
            this.btndecrypt.Click += new System.EventHandler(this.btndecrypt_Click);
            // 
            // btnencrypt
            // 
            this.btnencrypt.Location = new System.Drawing.Point(11, 173);
            this.btnencrypt.Name = "btnencrypt";
            this.btnencrypt.Size = new System.Drawing.Size(73, 49);
            this.btnencrypt.TabIndex = 36;
            this.btnencrypt.Text = "Encrypt Connection Strings";
            this.btnencrypt.UseVisualStyleBackColor = true;
            this.btnencrypt.Click += new System.EventHandler(this.btnencrypt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtsonstrinreg);
            this.groupBox2.Controls.Add(this.btngetservers);
            this.groupBox2.Controls.Add(this.btngetconstr);
            this.groupBox2.Controls.Add(this.cmbdb);
            this.groupBox2.Controls.Add(this.txtconstr);
            this.groupBox2.Controls.Add(this.chkwa);
            this.groupBox2.Controls.Add(this.cmbserver);
            this.groupBox2.Controls.Add(this.chksqlau);
            this.groupBox2.Controls.Add(this.txtusername);
            this.groupBox2.Controls.Add(this.btnlogin);
            this.groupBox2.Controls.Add(this.txtpass);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(603, 155);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modify Connection";
            // 
            // btngetservers
            // 
            this.btngetservers.Location = new System.Drawing.Point(13, 17);
            this.btngetservers.Name = "btngetservers";
            this.btngetservers.Size = new System.Drawing.Size(76, 24);
            this.btngetservers.TabIndex = 25;
            this.btngetservers.Text = "Get Servers";
            this.btngetservers.UseVisualStyleBackColor = true;
            this.btngetservers.Click += new System.EventHandler(this.btngetservers_Click);
            // 
            // btngetconstr
            // 
            this.btngetconstr.Location = new System.Drawing.Point(475, 122);
            this.btngetconstr.Name = "btngetconstr";
            this.btngetconstr.Size = new System.Drawing.Size(122, 27);
            this.btngetconstr.TabIndex = 23;
            this.btngetconstr.Text = "Get Connection String";
            this.btngetconstr.UseVisualStyleBackColor = true;
            this.btngetconstr.Click += new System.EventHandler(this.btngetconstr_Click);
            // 
            // cmbdb
            // 
            this.cmbdb.FormattingEnabled = true;
            this.cmbdb.Location = new System.Drawing.Point(293, 19);
            this.cmbdb.Name = "cmbdb";
            this.cmbdb.Size = new System.Drawing.Size(155, 21);
            this.cmbdb.TabIndex = 16;
            this.cmbdb.SelectedIndexChanged += new System.EventHandler(this.cmbdb_SelectedIndexChanged);
            // 
            // txtconstr
            // 
            this.txtconstr.Enabled = false;
            this.txtconstr.Location = new System.Drawing.Point(13, 123);
            this.txtconstr.Name = "txtconstr";
            this.txtconstr.Size = new System.Drawing.Size(456, 20);
            this.txtconstr.TabIndex = 14;
            // 
            // chkwa
            // 
            this.chkwa.AutoSize = true;
            this.chkwa.Checked = true;
            this.chkwa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkwa.Location = new System.Drawing.Point(457, 19);
            this.chkwa.Name = "chkwa";
            this.chkwa.Size = new System.Drawing.Size(141, 17);
            this.chkwa.TabIndex = 21;
            this.chkwa.Text = "Windows Authentication";
            this.chkwa.UseVisualStyleBackColor = true;
            this.chkwa.CheckedChanged += new System.EventHandler(this.chkwa_CheckedChanged);
            // 
            // cmbserver
            // 
            this.cmbserver.FormattingEnabled = true;
            this.cmbserver.Location = new System.Drawing.Point(92, 19);
            this.cmbserver.Name = "cmbserver";
            this.cmbserver.Size = new System.Drawing.Size(195, 21);
            this.cmbserver.TabIndex = 15;
            this.cmbserver.SelectedIndexChanged += new System.EventHandler(this.cmbserver_SelectedIndexChanged);
            // 
            // chksqlau
            // 
            this.chksqlau.AutoSize = true;
            this.chksqlau.Location = new System.Drawing.Point(457, 46);
            this.chksqlau.Name = "chksqlau";
            this.chksqlau.Size = new System.Drawing.Size(112, 17);
            this.chksqlau.TabIndex = 20;
            this.chksqlau.Text = "Sql Authentication";
            this.chksqlau.UseVisualStyleBackColor = true;
            this.chksqlau.CheckedChanged += new System.EventHandler(this.chksqlau_CheckedChanged);
            // 
            // txtusername
            // 
            this.txtusername.Enabled = false;
            this.txtusername.Location = new System.Drawing.Point(13, 71);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(375, 20);
            this.txtusername.TabIndex = 17;
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(394, 71);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(203, 46);
            this.btnlogin.TabIndex = 19;
            this.btnlogin.Text = "Attempt Login/Autherization";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // txtpass
            // 
            this.txtpass.Enabled = false;
            this.txtpass.Location = new System.Drawing.Point(13, 97);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(375, 20);
            this.txtpass.TabIndex = 18;
            this.txtpass.UseSystemPasswordChar = true;
            // 
            // txtsonstrinreg
            // 
            this.txtsonstrinreg.Enabled = false;
            this.txtsonstrinreg.Location = new System.Drawing.Point(13, 46);
            this.txtsonstrinreg.Name = "txtsonstrinreg";
            this.txtsonstrinreg.Size = new System.Drawing.Size(438, 20);
            this.txtsonstrinreg.TabIndex = 26;
            // 
            // ConnectionInfoWriterUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 229);
            this.Controls.Add(this.btnmodifyconstr);
            this.Controls.Add(this.btndecrypt);
            this.Controls.Add(this.btnencrypt);
            this.Controls.Add(this.groupBox2);
            this.Name = "ConnectionInfoWriterUtility";
            this.Text = "Connection Information Writer";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnmodifyconstr;
        private System.Windows.Forms.Button btndecrypt;
        private System.Windows.Forms.Button btnencrypt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btngetservers;
        private System.Windows.Forms.Button btngetconstr;
        private System.Windows.Forms.ComboBox cmbdb;
        private System.Windows.Forms.TextBox txtconstr;
        private System.Windows.Forms.CheckBox chkwa;
        private System.Windows.Forms.ComboBox cmbserver;
        private System.Windows.Forms.CheckBox chksqlau;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtsonstrinreg;
    }
}

