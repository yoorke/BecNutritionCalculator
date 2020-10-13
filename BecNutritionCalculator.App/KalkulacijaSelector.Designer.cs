namespace BecNutritionCalculator.App
{
    partial class KalkulacijaSelector
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
            this.chkKalkulacije = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnOdaberi = new System.Windows.Forms.Button();
            this.btnOdaberiSve = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkKalkulacije
            // 
            this.chkKalkulacije.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkKalkulacije.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKalkulacije.FormattingEnabled = true;
            this.chkKalkulacije.Location = new System.Drawing.Point(0, 0);
            this.chkKalkulacije.Name = "chkKalkulacije";
            this.chkKalkulacije.Size = new System.Drawing.Size(478, 320);
            this.chkKalkulacije.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOdaberiSve);
            this.panel1.Controls.Add(this.btnZatvori);
            this.panel1.Controls.Add(this.btnOdaberi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 320);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 64);
            this.panel1.TabIndex = 1;
            // 
            // btnZatvori
            // 
            this.btnZatvori.Location = new System.Drawing.Point(391, 29);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(75, 23);
            this.btnZatvori.TabIndex = 1;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnOdaberi
            // 
            this.btnOdaberi.Location = new System.Drawing.Point(12, 29);
            this.btnOdaberi.Name = "btnOdaberi";
            this.btnOdaberi.Size = new System.Drawing.Size(75, 23);
            this.btnOdaberi.TabIndex = 0;
            this.btnOdaberi.Text = "Odaberi";
            this.btnOdaberi.UseVisualStyleBackColor = true;
            this.btnOdaberi.Click += new System.EventHandler(this.btnOdaberi_Click);
            // 
            // btnOdaberiSve
            // 
            this.btnOdaberiSve.Location = new System.Drawing.Point(93, 29);
            this.btnOdaberiSve.Name = "btnOdaberiSve";
            this.btnOdaberiSve.Size = new System.Drawing.Size(75, 23);
            this.btnOdaberiSve.TabIndex = 2;
            this.btnOdaberiSve.Text = "Odaberi sve";
            this.btnOdaberiSve.UseVisualStyleBackColor = true;
            this.btnOdaberiSve.Click += new System.EventHandler(this.btnOdaberiSve_Click);
            // 
            // KalkulacijaSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 384);
            this.Controls.Add(this.chkKalkulacije);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KalkulacijaSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izaberite kalkulacije";
            this.Load += new System.EventHandler(this.KalkulacijaSelector_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkKalkulacije;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnOdaberi;
        private System.Windows.Forms.Button btnOdaberiSve;
    }
}