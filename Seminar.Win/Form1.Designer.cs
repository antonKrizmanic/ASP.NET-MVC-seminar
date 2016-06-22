namespace Seminar.Win
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanyAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompanyMail = new System.Windows.Forms.TextBox();
            this.bAddCompany = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Naziv";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(16, 30);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Adresa";
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.Location = new System.Drawing.Point(19, 82);
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.txtCompanyAddress.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyAddress.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Email";
            // 
            // txtCompanyMail
            // 
            this.txtCompanyMail.Location = new System.Drawing.Point(19, 131);
            this.txtCompanyMail.Name = "txtCompanyMail";
            this.txtCompanyMail.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyMail.TabIndex = 5;
            // 
            // bAddCompany
            // 
            this.bAddCompany.Location = new System.Drawing.Point(19, 173);
            this.bAddCompany.Name = "bAddCompany";
            this.bAddCompany.Size = new System.Drawing.Size(75, 23);
            this.bAddCompany.TabIndex = 6;
            this.bAddCompany.Text = "Dodaj";
            this.bAddCompany.UseVisualStyleBackColor = true;
            this.bAddCompany.Click += new System.EventHandler(this.bAddCompany_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.bAddCompany);
            this.Controls.Add(this.txtCompanyMail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCompanyAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanyAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompanyMail;
        private System.Windows.Forms.Button bAddCompany;
    }
}

