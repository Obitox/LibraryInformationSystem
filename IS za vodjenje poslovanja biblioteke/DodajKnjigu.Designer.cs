namespace IS_za_vodjenje_poslovanja_biblioteke
{
    partial class DodajKnjigu
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
            this.txtImeKnjige = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dpDatumIzdavanja = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZanr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImeAutora = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrezimeAutora = new System.Windows.Forms.TextBox();
            this.numKolicina = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numKolicina)).BeginInit();
            this.SuspendLayout();
            // 
            // txtImeKnjige
            // 
            this.txtImeKnjige.Location = new System.Drawing.Point(119, 12);
            this.txtImeKnjige.Name = "txtImeKnjige";
            this.txtImeKnjige.Size = new System.Drawing.Size(200, 20);
            this.txtImeKnjige.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ime knjige:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(278, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Potvrdi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dpDatumIzdavanja
            // 
            this.dpDatumIzdavanja.CustomFormat = "yyyy-mm-dd";
            this.dpDatumIzdavanja.Location = new System.Drawing.Point(119, 38);
            this.dpDatumIzdavanja.Name = "dpDatumIzdavanja";
            this.dpDatumIzdavanja.Size = new System.Drawing.Size(200, 20);
            this.dpDatumIzdavanja.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Datum izdavanja:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zanr:";
            // 
            // txtZanr
            // 
            this.txtZanr.Location = new System.Drawing.Point(119, 64);
            this.txtZanr.Name = "txtZanr";
            this.txtZanr.Size = new System.Drawing.Size(200, 20);
            this.txtZanr.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Kolicina:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ime autora:";
            // 
            // txtImeAutora
            // 
            this.txtImeAutora.Location = new System.Drawing.Point(119, 117);
            this.txtImeAutora.Name = "txtImeAutora";
            this.txtImeAutora.Size = new System.Drawing.Size(200, 20);
            this.txtImeAutora.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Prezime autora:";
            // 
            // txtPrezimeAutora
            // 
            this.txtPrezimeAutora.Location = new System.Drawing.Point(119, 143);
            this.txtPrezimeAutora.Name = "txtPrezimeAutora";
            this.txtPrezimeAutora.Size = new System.Drawing.Size(200, 20);
            this.txtPrezimeAutora.TabIndex = 13;
            // 
            // numKolicina
            // 
            this.numKolicina.Location = new System.Drawing.Point(119, 90);
            this.numKolicina.Name = "numKolicina";
            this.numKolicina.Size = new System.Drawing.Size(200, 20);
            this.numKolicina.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(278, 33);
            this.button2.TabIndex = 16;
            this.button2.Text = "Otkaži";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DodajKnjigu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 263);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numKolicina);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPrezimeAutora);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtImeAutora);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtZanr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dpDatumIzdavanja);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImeKnjige);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DodajKnjigu";
            this.Text = "DodajKnjigu";
            ((System.ComponentModel.ISupportInitialize)(this.numKolicina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtImeKnjige;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dpDatumIzdavanja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZanr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImeAutora;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrezimeAutora;
        private System.Windows.Forms.NumericUpDown numKolicina;
        private System.Windows.Forms.Button button2;
    }
}