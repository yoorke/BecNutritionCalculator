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
            this.btnOpenSmesaForm = new System.Windows.Forms.Button();
            this.btnSortByType = new System.Windows.Forms.Button();
            this.btnShowCosts = new System.Windows.Forms.Button();
            this.btnAmk = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnOpenSirovinaForm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSmesa = new System.Windows.Forms.ComboBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.dgvZahtevano = new System.Windows.Forms.DataGridView();
            this.dgvTotal = new System.Windows.Forms.DataGridView();
            this.dgvAmk = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnSaveCalculation = new System.Windows.Forms.ToolStripButton();
            this.btnSaveCalculationAs = new System.Windows.Forms.ToolStripButton();
            this.btnClearDataTable = new System.Windows.Forms.ToolStripButton();
            this.btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.btnExportToCsv = new System.Windows.Forms.ToolStripButton();
            this.chkShowAllSirovina = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculator)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZahtevano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAmk)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSirovina
            // 
            this.cmbSirovina.FormattingEnabled = true;
            this.cmbSirovina.Location = new System.Drawing.Point(480, 12);
            this.cmbSirovina.Name = "cmbSirovina";
            this.cmbSirovina.Size = new System.Drawing.Size(408, 21);
            this.cmbSirovina.TabIndex = 2;
            // 
            // btnDodajSirovinu
            // 
            this.btnDodajSirovinu.Location = new System.Drawing.Point(935, 12);
            this.btnDodajSirovinu.Name = "btnDodajSirovinu";
            this.btnDodajSirovinu.Size = new System.Drawing.Size(101, 22);
            this.btnDodajSirovinu.TabIndex = 4;
            this.btnDodajSirovinu.Text = "Dodaj";
            this.btnDodajSirovinu.UseVisualStyleBackColor = true;
            this.btnDodajSirovinu.Click += new System.EventHandler(this.btnDodajSirovinu_Click);
            // 
            // dgvCalculator
            // 
            this.dgvCalculator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalculator.Location = new System.Drawing.Point(0, 135);
            this.dgvCalculator.Name = "dgvCalculator";
            this.dgvCalculator.Size = new System.Drawing.Size(1209, 429);
            this.dgvCalculator.TabIndex = 2;
            this.dgvCalculator.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalculator_CellContentClick);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.chkShowAllSirovina);
            this.pnlTop.Controls.Add(this.btnOpenSmesaForm);
            this.pnlTop.Controls.Add(this.btnSortByType);
            this.pnlTop.Controls.Add(this.btnShowCosts);
            this.pnlTop.Controls.Add(this.btnAmk);
            this.pnlTop.Controls.Add(this.btnObrisi);
            this.pnlTop.Controls.Add(this.btnOpenSirovinaForm);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.txtNaziv);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cmbSmesa);
            this.pnlTop.Controls.Add(this.cmbSirovina);
            this.pnlTop.Controls.Add(this.btnDodajSirovinu);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 35);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1209, 100);
            this.pnlTop.TabIndex = 3;
            // 
            // btnOpenSmesaForm
            // 
            this.btnOpenSmesaForm.Location = new System.Drawing.Point(365, 12);
            this.btnOpenSmesaForm.Name = "btnOpenSmesaForm";
            this.btnOpenSmesaForm.Size = new System.Drawing.Size(35, 21);
            this.btnOpenSmesaForm.TabIndex = 10;
            this.btnOpenSmesaForm.Text = "...";
            this.btnOpenSmesaForm.UseVisualStyleBackColor = true;
            this.btnOpenSmesaForm.Click += new System.EventHandler(this.btnOpenSmesaForm_Click);
            // 
            // btnSortByType
            // 
            this.btnSortByType.Location = new System.Drawing.Point(12, 71);
            this.btnSortByType.Name = "btnSortByType";
            this.btnSortByType.Size = new System.Drawing.Size(75, 23);
            this.btnSortByType.TabIndex = 9;
            this.btnSortByType.Text = "Sort po tipu";
            this.btnSortByType.UseVisualStyleBackColor = true;
            this.btnSortByType.Click += new System.EventHandler(this.btnSortByType_Click);
            // 
            // btnShowCosts
            // 
            this.btnShowCosts.Location = new System.Drawing.Point(932, 71);
            this.btnShowCosts.Name = "btnShowCosts";
            this.btnShowCosts.Size = new System.Drawing.Size(104, 23);
            this.btnShowCosts.TabIndex = 8;
            this.btnShowCosts.Text = "Prikaži troškove";
            this.btnShowCosts.UseVisualStyleBackColor = true;
            this.btnShowCosts.Click += new System.EventHandler(this.btnShowCosts_Click);
            // 
            // btnAmk
            // 
            this.btnAmk.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAmk.Location = new System.Drawing.Point(1042, 71);
            this.btnAmk.Name = "btnAmk";
            this.btnAmk.Size = new System.Drawing.Size(115, 23);
            this.btnAmk.TabIndex = 7;
            this.btnAmk.Text = "AMK";
            this.btnAmk.UseVisualStyleBackColor = false;
            this.btnAmk.Click += new System.EventHandler(this.btnAmk_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(1042, 12);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(75, 22);
            this.btnObrisi.TabIndex = 5;
            this.btnObrisi.Text = "Obriši ";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnOpenSirovinaForm
            // 
            this.btnOpenSirovinaForm.Location = new System.Drawing.Point(894, 12);
            this.btnOpenSirovinaForm.Name = "btnOpenSirovinaForm";
            this.btnOpenSirovinaForm.Size = new System.Drawing.Size(35, 21);
            this.btnOpenSirovinaForm.TabIndex = 3;
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
            this.label2.Location = new System.Drawing.Point(426, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sirovina:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(55, 40);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(250, 20);
            this.txtNaziv.TabIndex = 6;
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
            this.cmbSmesa.TabIndex = 1;
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
            this.pnlBottom.Size = new System.Drawing.Size(1209, 85);
            this.pnlBottom.TabIndex = 4;
            // 
            // dgvZahtevano
            // 
            this.dgvZahtevano.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZahtevano.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvZahtevano.Location = new System.Drawing.Point(0, 56);
            this.dgvZahtevano.Name = "dgvZahtevano";
            this.dgvZahtevano.Size = new System.Drawing.Size(1209, 26);
            this.dgvZahtevano.TabIndex = 1;
            // 
            // dgvTotal
            // 
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTotal.Location = new System.Drawing.Point(0, 30);
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.Size = new System.Drawing.Size(1209, 26);
            this.dgvTotal.TabIndex = 0;
            // 
            // dgvAmk
            // 
            this.dgvAmk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAmk.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAmk.Location = new System.Drawing.Point(0, 0);
            this.dgvAmk.Name = "dgvAmk";
            this.dgvAmk.Size = new System.Drawing.Size(1209, 30);
            this.dgvAmk.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnSaveCalculation,
            this.btnSaveCalculationAs,
            this.btnClearDataTable,
            this.btnExportToExcel,
            this.btnExportToCsv});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1209, 35);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLoad
            // 
            this.btnLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(23, 32);
            this.btnLoad.Text = "Učitaj kalkulaciju";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
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
            this.btnSaveCalculation.Click += new System.EventHandler(this.btnSaveCalculation_Click);
            // 
            // btnSaveCalculationAs
            // 
            this.btnSaveCalculationAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCalculationAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCalculationAs.Image")));
            this.btnSaveCalculationAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCalculationAs.Name = "btnSaveCalculationAs";
            this.btnSaveCalculationAs.Size = new System.Drawing.Size(23, 32);
            this.btnSaveCalculationAs.Text = "Sačuvaj kao novu";
            this.btnSaveCalculationAs.Click += new System.EventHandler(this.btnSaveCalculationAs_Click);
            // 
            // btnClearDataTable
            // 
            this.btnClearDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearDataTable.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDataTable.Image")));
            this.btnClearDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearDataTable.Name = "btnClearDataTable";
            this.btnClearDataTable.Size = new System.Drawing.Size(23, 32);
            this.btnClearDataTable.Text = "Nova kalkulacija";
            this.btnClearDataTable.Click += new System.EventHandler(this.btnClearDataTable_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(23, 32);
            this.btnExportToExcel.Text = "Izvezi u Excel";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnExportToCsv
            // 
            this.btnExportToCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToCsv.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToCsv.Image")));
            this.btnExportToCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToCsv.Name = "btnExportToCsv";
            this.btnExportToCsv.Size = new System.Drawing.Size(23, 32);
            this.btnExportToCsv.Text = "Izvezi u CSV";
            this.btnExportToCsv.Click += new System.EventHandler(this.btnExportToCsv_Click);
            // 
            // chkShowAllSirovina
            // 
            this.chkShowAllSirovina.AutoSize = true;
            this.chkShowAllSirovina.Location = new System.Drawing.Point(480, 39);
            this.chkShowAllSirovina.Name = "chkShowAllSirovina";
            this.chkShowAllSirovina.Size = new System.Drawing.Size(177, 17);
            this.chkShowAllSirovina.TabIndex = 11;
            this.chkShowAllSirovina.Text = "Prikaži sve (i neaktivne sirovine)";
            this.chkShowAllSirovina.UseVisualStyleBackColor = true;
            this.chkShowAllSirovina.CheckedChanged += new System.EventHandler(this.chkShowAllSirovina_CheckedChanged);
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 649);
            this.Controls.Add(this.dgvCalculator);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Calculator";
            this.Text = "Kalkulacija smeše";
            this.Load += new System.EventHandler(this.Calculator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculator)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZahtevano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
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
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveCalculation;
        private System.Windows.Forms.ToolStripButton btnSaveCalculationAs;
        private System.Windows.Forms.DataGridView dgvZahtevano;
        private System.Windows.Forms.DataGridView dgvAmk;
        private System.Windows.Forms.Button btnOpenSirovinaForm;
        private System.Windows.Forms.ToolStripButton btnClearDataTable;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.Button btnAmk;
        private System.Windows.Forms.Button btnShowCosts;
        private System.Windows.Forms.Button btnSortByType;
        private System.Windows.Forms.Button btnOpenSmesaForm;
        private System.Windows.Forms.ToolStripButton btnExportToExcel;
        private System.Windows.Forms.ToolStripButton btnExportToCsv;
        private System.Windows.Forms.CheckBox chkShowAllSirovina;
    }
}