namespace BecNutritionCalculator.App
{
    partial class Calculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.cmbSirovina = new System.Windows.Forms.ComboBox();
            this.btnDodajSirovinu = new System.Windows.Forms.Button();
            this.dgvCalculator = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnOpenSirovinaForm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSmesa = new System.Windows.Forms.ComboBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.dgvTotal = new System.Windows.Forms.DataGridView();
            this.dgvZahtevano = new System.Windows.Forms.DataGridView();
            this.dgvAmk = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveCalculation = new System.Windows.Forms.ToolStripButton();
            this.btnSaveCalculationAs = new System.Windows.Forms.ToolStripButton();
            this.btnClearDataTable = new System.Windows.Forms.ToolStripButton();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculator)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZahtevano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAmk)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSirovina
            // 
            this.cmbSirovina.FormattingEnabled = true;
            this.cmbSirovina.Location = new System.Drawing.Point(451, 12);
            this.cmbSirovina.Name = "cmbSirovina";
            this.cmbSirovina.Size = new System.Drawing.Size(408, 21);
            this.cmbSirovina.TabIndex = 0;
            // 
            // btnDodajSirovinu
            // 
            this.btnDodajSirovinu.Location = new System.Drawing.Point(906, 11);
            this.btnDodajSirovinu.Name = "btnDodajSirovinu";
            this.btnDodajSirovinu.Size = new System.Drawing.Size(101, 22);
            this.btnDodajSirovinu.TabIndex = 1;
            this.btnDodajSirovinu.Text = "Dodaj";
            this.btnDodajSirovinu.UseVisualStyleBackColor = true;
            this.btnDodajSirovinu.Click += new System.EventHandler(this.btnDodajSirovinu_Click);
            // 
            // dgvCalculator
            // 
            this.dgvCalculator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalculator.Location = new System.Drawing.Point(0, 126);
            this.dgvCalculator.Name = "dgvCalculator";
            this.dgvCalculator.Size = new System.Drawing.Size(1169, 438);
            this.dgvCalculator.TabIndex = 2;
            this.dgvCalculator.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalculator_CellContentClick);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnObrisi);
            this.pnlTop.Controls.Add(this.btnOpenSirovinaForm);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.textBox1);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cmbSmesa);
            this.pnlTop.Controls.Add(this.cmbSirovina);
            this.pnlTop.Controls.Add(this.btnDodajSirovinu);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 35);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1169, 91);
            this.pnlTop.TabIndex = 3;
            // 
            // btnOpenSirovinaForm
            // 
            this.btnOpenSirovinaForm.Location = new System.Drawing.Point(865, 12);
            this.btnOpenSirovinaForm.Name = "btnOpenSirovinaForm";
            this.btnOpenSirovinaForm.Size = new System.Drawing.Size(35, 21);
            this.btnOpenSirovinaForm.TabIndex = 7;
            this.btnOpenSirovinaForm.Text = "...";
            this.btnOpenSirovinaForm.UseVisualStyleBackColor = true;
            this.btnOpenSirovinaForm.Click += new System.EventHandler(this.btnOpenSirovinaForm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Naziv:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sirovina:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(55, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Smeša";
            // 
            // cmbSmesa
            // 
            this.cmbSmesa.FormattingEnabled = true;
            this.cmbSmesa.Location = new System.Drawing.Point(55, 12);
            this.cmbSmesa.Name = "cmbSmesa";
            this.cmbSmesa.Size = new System.Drawing.Size(304, 21);
            this.cmbSmesa.TabIndex = 2;
            this.cmbSmesa.SelectedIndexChanged += new System.EventHandler(this.cmbSmesa_SelectedIndexChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.dgvZahtevano);
            this.pnlBottom.Controls.Add(this.dgvTotal);
            this.pnlBottom.Controls.Add(this.dgvAmk);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 564);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1169, 85);
            this.pnlBottom.TabIndex = 4;
            // 
            // dgvTotal
            // 
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTotal.Location = new System.Drawing.Point(0, 30);
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.Size = new System.Drawing.Size(1169, 26);
            this.dgvTotal.TabIndex = 0;
            // 
            // dgvZahtevano
            // 
            this.dgvZahtevano.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZahtevano.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvZahtevano.Location = new System.Drawing.Point(0, 56);
            this.dgvZahtevano.Name = "dgvZahtevano";
            this.dgvZahtevano.Size = new System.Drawing.Size(1169, 26);
            this.dgvZahtevano.TabIndex = 1;
            // 
            // dgvAmk
            // 
            this.dgvAmk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAmk.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAmk.Location = new System.Drawing.Point(0, 0);
            this.dgvAmk.Name = "dgvAmk";
            this.dgvAmk.Size = new System.Drawing.Size(1169, 30);
            this.dgvAmk.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCalculation,
            this.btnSaveCalculationAs,
            this.btnClearDataTable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1169, 35);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSaveCalculation
            // 
            this.btnSaveCalculation.AutoSize = false;
            this.btnSaveCalculation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCalculation.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCalculation.Image")));
            this.btnSaveCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCalculation.Name = "btnSaveCalculation";
            this.btnSaveCalculation.Size = new System.Drawing.Size(32, 32);
            this.btnSaveCalculation.Text = "Sačuvaj kalkulaciju";
            // 
            // btnSaveCalculationAs
            // 
            this.btnSaveCalculationAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCalculationAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCalculationAs.Image")));
            this.btnSaveCalculationAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCalculationAs.Name = "btnSaveCalculationAs";
            this.btnSaveCalculationAs.Size = new System.Drawing.Size(23, 32);
            this.btnSaveCalculationAs.Text = "Sačuvaj kao novu";
            // 
            // btnClearDataTable
            // 
            this.btnClearDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearDataTable.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDataTable.Image")));
            this.btnClearDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearDataTable.Name = "btnClearDataTable";
            this.btnClearDataTable.Size = new System.Drawing.Size(23, 32);
            this.btnClearDataTable.Text = "toolStripButton1";
            this.btnClearDataTable.Click += new System.EventHandler(this.btnClearDataTable_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(1013, 11);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(75, 22);
            this.btnObrisi.TabIndex = 8;
            this.btnObrisi.Text = "Obriši ";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 649);
            this.Controls.Add(this.dgvCalculator);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnlBottom);
            this.Name = "Calculator";
            this.Text = "Kalkulacija smeše";
            this.Load += new System.EventHandler(this.Calculator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculator)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZahtevano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAmk)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSirovina;
        private System.Windows.Forms.Button btnDodajSirovinu;
        private System.Windows.Forms.DataGridView dgvCalculator;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.DataGridView dgvTotal;
        private System.Windows.Forms.ComboBox cmbSmesa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveCalculation;
        private System.Windows.Forms.ToolStripButton btnSaveCalculationAs;
        private System.Windows.Forms.DataGridView dgvZahtevano;
        private System.Windows.Forms.DataGridView dgvAmk;
        private System.Windows.Forms.Button btnOpenSirovinaForm;
        private System.Windows.Forms.ToolStripButton btnClearDataTable;
        private System.Windows.Forms.Button btnObrisi;
    }
}