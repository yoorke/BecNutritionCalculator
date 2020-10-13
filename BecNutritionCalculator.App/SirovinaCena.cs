using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BecNutritionCalculator.App
{
    public partial class SirovinaCena : Form
    {
        private List<DataTable> _sirovinaCenaDT = new List<DataTable>();
        private int _maxHistory = 3;
        private DataTable _potrosnjaDT;
        private IExportBL _exportBL;
        private string _smesaNaziv;
        private string _kalkulacijaNaziv;

        private IKategorijaZivotinjaSmesaPotrosnjaBL _kategorijaZivotinjaSmesaPotrosnjaBL;

        public SirovinaCena(IKategorijaZivotinjaSmesaPotrosnjaBL kategorijaZivotinjaSmesaPotrosnjaBL, IExportBL exportBL, string smesaNaziv, string kalkulacijaNaziv)
        {
            InitializeComponent();
            dgvSirovinaCena1.RowPrePaint += dgvSirovinaCena1_RowPrePaint;
            dgvSirovinaCena2.RowPrePaint += dgvSirovinaCena1_RowPrePaint;
            dgvSirovinaCena3.RowPrePaint += dgvSirovinaCena1_RowPrePaint;

            _kategorijaZivotinjaSmesaPotrosnjaBL = kategorijaZivotinjaSmesaPotrosnjaBL;
            _exportBL = exportBL;
            _smesaNaziv = smesaNaziv;
            _kalkulacijaNaziv = kalkulacijaNaziv;
        }

        private void dgvSirovinaCena1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //string color = dgvSirovinaCena1.Rows[e.RowIndex].Cells["TipSirovineCode"].Value.ToString().Equals("SIL") ? "#b3ffd9" : "#ffffcc";
            ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#" + ((DataGridView)sender).Rows[e.RowIndex].Cells["Color"].Value.ToString());
        }

        private void SirovinaCena_Load(object sender, EventArgs e)
        {
            setDataGridView();

            this.Resize += SirovinaCena_Resize;
            this.WindowState = FormWindowState.Maximized;

            //setDataTable();
        }

        private void SirovinaCena_Resize(object sender, EventArgs e)
        {
            setDataGridView();
        }

        public void AddDataTable(DataTable sirovinaCena, int smesaID)
        {
            _sirovinaCenaDT.Clear();
            if (_sirovinaCenaDT.Count < _maxHistory)
            { 
                _sirovinaCenaDT.Add(sirovinaCena);

                setDataGridViewColumns(((DataGridView)this.Controls.Find("dgvSirovinaCena" + _sirovinaCenaDT.Count, true)[0]), sirovinaCena);
                calculateTotal(sirovinaCena, _sirovinaCenaDT.Count, smesaID);
            }
            else
            {
                for (int i = 0; i < _sirovinaCenaDT.Count - 1; i++)
                { 
                    _sirovinaCenaDT[i] = _sirovinaCenaDT[i + 1];
                    ((DataGridView)this.Controls.Find("dgvSirovinaCena" + (i + 1).ToString(), true)[0]).DataSource = _sirovinaCenaDT[i];
                }
                _sirovinaCenaDT[_maxHistory - 1] = sirovinaCena;
                dgvSirovinaCena3.DataSource = _sirovinaCenaDT[_maxHistory - 1];
            }
            sortDataGridView(dgvSirovinaCena1);
            //calculateTotal(_sirovinaCenaDT[0], 1);
        }

        private void setDataGridView()
        {
            dgvSirovinaCena1.Width = panel3.Width / 2;
            //dgvSirovinaCena3.Width = this.Width / 3;
            //dgvSirovinaCena2.Width = this.Width - dgvSirovinaCena1.Width - dgvSirovinaCena3.Width;
            //dgvSirovinaCena1.Height = panel3.Height;
            //dgvSirovinaCena2.Height = panel3.Height;
            //dgvSirovinaCena3.Height = panel3.Height;
            //dgvSirovinaCena2.Left = dgvSirovinaCena1.Width + 5;
            //dgvSirovinaCena3.Left = dgvSirovinaCena1.Width + dgvSirovinaCena2.Width + 10;
            dgvSirovinaCena1.ScrollBars = ScrollBars.Vertical;
            panel6.Width = panel2.Width / 2;
            panel5.Width = this.Width / 2;
            pnlPotrosnja.Width = panel3.Width / 2;
            //dgvTroskovi.Height = panel3.Height;

            //dgvSirovinaCena1.DataSource = _sirovinaCenaDT[0];
            //if(_sirovinaCenaDT.Count > 1)
            //dgvSirovinaCena2.DataSource = _sirovinaCenaDT[1];
            //if(_sirovinaCenaDT.Count > 2)
            //dgvSirovinaCena2.DataSource = _sirovinaCenaDT[2];

            //setDataGridViewColumns(dgvSirovinaCena1);
            //if(_sirovinaCenaDT.Count > 1)
            //setDataGridViewColumns(dgvSirovinaCena2);
            //if(_sirovinaCenaDT.Count > 2)
            //setDataGridViewColumns(dgvSirovinaCena3);

            label10.Text = dgvSirovinaCena1.Width.ToString() + "panel2: " + panel2.Width.ToString() + "panel6: " + panel6.Width.ToString();
            label11.Text = pnlPotrosnja.Width.ToString();
        }

        private void setDataGridViewColumns(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.DataSource = dataTable;
            dataGridView.Columns["SirovinaID"].Visible = false;
            dataGridView.Columns["Cena"].DefaultCellStyle.Format = "N2";
            dataGridView.Columns["Cena"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Ukupno"].DefaultCellStyle.Format = "N2";
            dataGridView.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["TipSirovineCode"].Visible = false;
            dataGridView.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Kolicina"].DefaultCellStyle.Format = "N2";
            dataGridView.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Jm"].Width = 50;
            dataGridView.Columns["TipSirovineID"].Visible = false;
            dataGridView.Columns["Color"].Visible = false;
            dataGridView.Columns["KolicinskiOdnos"].Visible = false;
            dataGridView.Columns["KolicinaKorigovano"].DefaultCellStyle.Format = "N2";
            dataGridView.Columns["KolicinaKorigovano"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Naziv"].DisplayIndex = 0;
            dataGridView.Columns["Kolicina"].DisplayIndex = 1;
            dataGridView.Columns["KolicinaKorigovano"].DisplayIndex = 2;
            dataGridView.Columns["Jm"].DisplayIndex = 3;
            dataGridView.Columns["Cena"].DisplayIndex = 9;
            dataGridView.Columns["Ukupno"].DisplayIndex = 10;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void sortDataGridView(DataGridView dataGridView)
        {
            dataGridView.Sort(dataGridView.Columns["TipSirovineID"], ListSortDirection.Ascending);
        }

        private void calculateTotal(DataTable dataTable, int index, int smesaID)
        {
            decimal total = 0;
            decimal totalKabaste = 0;
            decimal totalKupovne = 0;
            decimal totalAmk = 0;
            foreach (DataRow row in dataTable.Rows)
            { 
                total += decimal.Parse(row["Ukupno"].ToString());
                if (row["TipSirovineID"].ToString().Equals("1"))
                    totalKabaste += decimal.Parse(row["Ukupno"].ToString());
                else
                    totalKupovne += decimal.Parse(row["Ukupno"].ToString());
                if (row["TipSirovineCode"].ToString().Equals("AMK"))
                    totalAmk += decimal.Parse(row["Ukupno"].ToString());
            }

            ((Label)this.Controls.Find("lblTotal" + index, true)[0]).Text = string.Format("{0:N2}", total);
            //lblTotal1.Text = string.Format("{0:N2}", total);
            lblTotalKabaste1.Text = string.Format("{0:N2}", totalKabaste);
            lblTotalKupovne1.Text = string.Format("{0:N2}", totalKupovne);
            lblTotalAmk1.Text = string.Format("{0:N2}", totalAmk);

            calculatePotrosnja(smesaID, total);
        }

        private void calculatePotrosnja(int smesaID, decimal total)
        {
            if (_potrosnjaDT == null)
                setDataTable();
            _potrosnjaDT.Rows.Clear();

            IEnumerable<KategorijaZivotinjaSmesaPotrosnja> kategorijaZivotinjaSmesaPotrosnja = _kategorijaZivotinjaSmesaPotrosnjaBL.GetBySmesaID(smesaID);

            DataRow newRow;
            foreach (var kzsp in kategorijaZivotinjaSmesaPotrosnja)
            {
                newRow = _potrosnjaDT.NewRow();
                //newRow["KategorijaZivotinjaID"] = kzsp.KategorijaZivotinjaID;
                newRow["KategorijaZivotinjaNaziv"] = kzsp.KategorijaZivotinjaNaziv;
                newRow["BrojZivotinja"] = kzsp.BrojZivotinja;
                newRow["DnevnaPotrosnjaPoZivotinji"] = kzsp.DnevnaPotrosnja;
                newRow["NedeljniBrojHranjenja"] = kzsp.NedeljniBrojHranjenja;
                newRow["Cena"] = total;
                newRow["DnevnaPotrosnja"] = kzsp.DnevnaPotrosnja * kzsp.BrojZivotinja;
                newRow["NedeljnaPotrosnja"] = (kzsp.DnevnaPotrosnja * kzsp.BrojZivotinja) * kzsp.NedeljniBrojHranjenja;

                _potrosnjaDT.Rows.Add(newRow);
            }

            decimal ukupnaNedeljnaPotrosnja = 0;
            decimal ukupnaMesecnaPotrosnja = 0;
            decimal ukupnaGodisnjaPotrosnja = 0;

            foreach(DataRow row in _potrosnjaDT.Rows)
            {
                ukupnaNedeljnaPotrosnja += decimal.Parse(row["NedeljnaPotrosnja"].ToString());
                //ukupnaMesecnaPotrosnja += decimal.Parse(row["NedeljnaPotrosnja"].ToString()) * (decimal)4.5;
                ukupnaMesecnaPotrosnja += decimal.Parse(row["DnevnaPotrosnja"].ToString()) * 31;
                //ukupnaGodisnjaPotrosnja += decimal.Parse(row["NedeljnaPotrosnja"].ToString()) * (decimal)4.5 * 12;
                ukupnaGodisnjaPotrosnja += decimal.Parse(row["DnevnaPotrosnja"].ToString()) * 365;
                //ukupnaMesecnaPotrosnja += ukupnaNedeljnaPotrosnja * (decimal)4.5;
                //ukupnaGodisnjaPotrosnja += ukupnaMesecnaPotrosnja * 12;
            }

            lblUkupnaNedeljnaPotrosnja.Text = string.Format("{0:N2}", ukupnaNedeljnaPotrosnja);
            lblUkupnaMesecnaPotrosnja.Text = string.Format("{0:N2}", ukupnaMesecnaPotrosnja);
            lblUkupnaGodisnjaPotrosnja.Text = string.Format("{0:N2}", ukupnaGodisnjaPotrosnja);

            lblUkupnaNedeljnaPotrosnjaDin.Text = string.Format("{0:N2}", ukupnaNedeljnaPotrosnja * total / 100);
            lblUkupnaMesecnaPotrosnjaDin.Text = string.Format("{0:N2}", ukupnaMesecnaPotrosnja * total / 100);
            lblUkupnaGodinjaPotrosnjaDin.Text = string.Format("{0:N2}", ukupnaGodisnjaPotrosnja * total / 100);
        }

        private void setDataTable()
        {
            _potrosnjaDT = new DataTable();
            //_potrosnjaDT.Columns.Add("KategorijaZivotinjaID", typeof(int));
            _potrosnjaDT.Columns.Add("KategorijaZivotinjaNaziv", typeof(string));
            _potrosnjaDT.Columns.Add("BrojZivotinja", typeof(int));
            _potrosnjaDT.Columns.Add("DnevnaPotrosnjaPoZivotinji", typeof(decimal));
            _potrosnjaDT.Columns.Add("NedeljniBrojHranjenja", typeof(int));
            _potrosnjaDT.Columns.Add("Cena", typeof(decimal));
            _potrosnjaDT.Columns.Add("DnevnaPotrosnja", typeof(decimal));
            _potrosnjaDT.Columns.Add("NedeljnaPotrosnja", typeof(decimal));
            //_potrosnjaDT.Columns.Add("UkupnaNedeljnaPotrosnja", typeof(decimal));
            //_potrosnjaDT.Columns.Add("UKupnaMesecnaPotrosnja", typeof(decimal));
            //_potrosnjaDT.Columns.Add("UkupnaGodisnjaPotrosnja", typeof(decimal));
            setDataGridViewTroskovi();
        }

        private void setDataGridViewTroskovi()
        {
            dgvTroskovi.DataSource = _potrosnjaDT;
            dgvTroskovi.AllowUserToAddRows = false;
            dgvTroskovi.AllowUserToDeleteRows = false;
            dgvTroskovi.RowHeadersVisible = false;
            dgvTroskovi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvTroskovi.Columns["KategorijaZivotinjaID"].Visible = false;
            dgvTroskovi.Columns["BrojZivotinja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTroskovi.Columns["BrojZivotinja"].HeaderText = "Broj životinja";
            dgvTroskovi.Columns["DnevnaPotrosnjaPoZivotinji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTroskovi.Columns["DnevnaPotrosnjaPoZivotinji"].DefaultCellStyle.Format = "N2";
            dgvTroskovi.Columns["DnevnaPotrosnjaPoZivotinji"].HeaderText = "Dnevno [kg/živ]";
            dgvTroskovi.Columns["NedeljniBrojHranjenja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTroskovi.Columns["NedeljniBrojHranjenja"].Visible = false;
            dgvTroskovi.Columns["Cena"].Visible = false;
            dgvTroskovi.Columns["DnevnaPotrosnja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTroskovi.Columns["DnevnaPotrosnja"].DefaultCellStyle.Format = "N2";
            dgvTroskovi.Columns["DnevnaPotrosnja"].HeaderText = "Dnevno [kg]";
            dgvTroskovi.Columns["NedeljnaPotrosnja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTroskovi.Columns["NedeljnaPotrosnja"].DefaultCellStyle.Format = "N2";
            dgvTroskovi.Columns["NedeljnaPotrosnja"].HeaderText = "Nedeljno [kg]";
            dgvTroskovi.Columns["KategorijaZivotinjaNaziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTroskovi.Columns["KategorijaZivotinjaNaziv"].HeaderText = "Kategorija životinja";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = _smesaNaziv + "_troskovi";
                saveFileDialog.Filter = "Excel | *.xlsx";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //_exportBL.CreateExcelSpreadsheet(_sirovinaCenaDT[0], saveFileDialog.FileName, null, new List<string>(), _potrosnjaDT);
                    _exportBL.CreateExcelSpreadsheet(new List<ExportModel>()
                    {
                        new ExportModel(){ DataTable = _sirovinaCenaDT[0], FooterDataTables = new List<DataTable>() { getTroskoviFooter() }, Name = "Troškovi" },
                        new ExportModel(){ DataTable = _potrosnjaDT, FooterDataTables = new List<DataTable>(), Name = "Potrošnja" },
                        new ExportModel(){ DataTable = getPotrosnjaFooter() , FooterDataTables = new List<DataTable>(), Name = "Potrošnja ukupno" }
                    }, 
                    saveFileDialog.FileName, new List<string>() { "SirovinaID", "TipSirovineCode", "Color", "KolicinskiOdnos" });

                    MessageBox.Show("Troškovi uspešno izvezeni.", "Izvoz troškova", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom izvoza troškova. " + ex.Message, "Izvoz troškova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable getTroskoviFooter()
        {
            DataTable dataTable = _sirovinaCenaDT[0].Copy();
            dataTable.Rows.Clear();

            DataRow newRow = dataTable.NewRow();
            newRow["Naziv"] = "Kabaste";
            newRow["Ukupno"] = lblTotalKabaste1.Text;
            newRow["Jm"] = "din";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["Naziv"] = "Kupovne";
            newRow["Ukupno"] = lblTotalKupovne1.Text;
            newRow["Jm"] = "din";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["Naziv"] = "Ukupno";
            newRow["Ukupno"] = lblTotal1.Text;
            newRow["Jm"] = "din";
            dataTable.Rows.Add(newRow);

            return dataTable;
        }

        private DataTable getPotrosnjaFooter()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Naziv");
            dataTable.Columns.Add("Ukupno [kg]", typeof(decimal));
            //dataTable.Columns.Add("UkupnoKgJmNaziv");
            dataTable.Columns.Add("Ukupno [din]", typeof(decimal));
            //dataTable.Columns.Add("UkupnoDinJmNaziv");

            DataRow newRow = dataTable.NewRow();
            newRow["Naziv"] = "Ukupno nedeljno";
            newRow["Ukupno [kg]"] = lblUkupnaNedeljnaPotrosnja.Text;
            //newRow["UkupnoKgJmNaziv"] = "kg";
            newRow["Ukupno [din]"] = lblUkupnaNedeljnaPotrosnjaDin.Text;
            //newRow["UkupnoDinJmNaziv"] = "din";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["Naziv"] = "Ukupno mesečno";
            newRow["Ukupno [kg]"] = lblUkupnaMesecnaPotrosnja.Text;
            //newRow["UkupnoKgJmNaziv"] = "kg";
            newRow["Ukupno [din]"] = lblUkupnaMesecnaPotrosnjaDin.Text;
            //newRow["UkupnoDinJmNaziv"] = "din";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["Naziv"] = "Ukupno godišnje";
            newRow["Ukupno [kg]"] = lblUkupnaGodisnjaPotrosnja.Text;
            //newRow["UkupnoKgJmNaziv"] = "kg";
            newRow["Ukupno [din]"] = lblUkupnaGodinjaPotrosnjaDin.Text;
            //newRow["UkupnoDinJmNaziv"] = "din";
            dataTable.Rows.Add(newRow);

            return dataTable;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.reportDataSet rds = new Reports.reportDataSet();
            DataRow newRow = rds.Tables["Kalkulacija"].NewRow();
            newRow["NazivSmese"] = _smesaNaziv;
            newRow["UkupnoKabaste"] = lblTotalKabaste1.Text;
            newRow["UkupnoKupovne"] = lblTotalKupovne1.Text;
            newRow["UkupnoAmk"] = lblTotalAmk1.Text;
            newRow["Ukupno"] = lblTotal1.Text;
            newRow["NazivKalkulacije"] = _kalkulacijaNaziv;
            rds.Tables["Kalkulacija"].Rows.Add(newRow);

            foreach(DataRow row in _sirovinaCenaDT[0].Rows)
            {
                newRow = rds.Tables["KalkulacijaSirovina"].NewRow();
                newRow["naziv"] = row["Naziv"];
                newRow["kolicina"] = row["KolicinaKorigovano"].ToString() != string.Empty ? row["KolicinaKorigovano"] : row["Kolicina"];
                newRow["cena"] = row["Cena"];
                newRow["ukupno"] = row["Ukupno"];
                newRow["jmNaziv"] = row["Jm"];
                newRow["tipSirovineCode"] = int.Parse(row["TipSirovineID"].ToString()) == 1 ? "Kabaste" : "Kupovne";
                newRow["udeo"] = decimal.Parse(row["Ukupno"].ToString()) / decimal.Parse(lblTotal1.Text) * 100;
                rds.Tables["KalkulacijaSirovina"].Rows.Add(newRow);
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rd.Load("Reports/KalkulacijaTroskovi.rpt");
            rd.SetDataSource(rds);

            frmPrint objfrmPrint = new frmPrint();
            objfrmPrint.crystalReportViewer1.ReportSource = rd;
            objfrmPrint.Show();
        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = _smesaNaziv;
            saveFileDialog.Filter = "Csv | *.csv";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _exportBL.ExportToCsv(new ExportModel() { DataTable = _sirovinaCenaDT[0], FooterDataTables = new List<DataTable>() { getTroskoviFooter() }, Name = "Troškovi" }, saveFileDialog.FileName, new List<string>() { "SirovinaID", "TipSirovineCode", "Color", "KolicinskiOdnos" });
            }
        }

        private void btnPrintConsumptionReport_Click(object sender, EventArgs e)
        {
            Reports.reportDataSet rds = new Reports.reportDataSet();
            DataRow newRowKalkulacijaPotrosnja = rds.Tables["KalkulacijaPotrosnja"].NewRow();
            newRowKalkulacijaPotrosnja["CenaSmese"] = decimal.Parse(lblTotal1.Text);
            newRowKalkulacijaPotrosnja["KalkulacijaNaziv"] = _kalkulacijaNaziv;
            newRowKalkulacijaPotrosnja["SmesaNaziv"] = _smesaNaziv;
            rds.Tables["KalkulacijaPotrosnja"].Rows.Add(newRowKalkulacijaPotrosnja);
            foreach(DataGridViewRow row in dgvTroskovi.Rows)
            {
                DataRow newRow = rds.Tables["KalkulacijaPotrosnjaStavka"].NewRow();
                newRow["KategorijaZivotinja"] = row.Cells["KategorijaZivotinjaNaziv"].Value;
                newRow["BrojZivotinja"] = row.Cells["BrojZivotinja"].Value;
                newRow["DnevnaPotrosnjaPoZivotinji"] = row.Cells["DnevnaPotrosnjaPoZivotinji"].Value;
                newRow["DnevnaPotrosnjaKolicina"] = row.Cells["DnevnaPotrosnja"].Value;
                newRow["DnevnaPotrosnjaIznos"] = decimal.Parse(row.Cells["DnevnaPotrosnja"].Value.ToString()) * decimal.Parse(lblTotal1.Text) / (decimal)100;
                newRow["NedeljnaPotrosnjaKolicina"] = row.Cells["NedeljnaPotrosnja"].Value;
                newRow["NedeljnaPotrosnjaIznos"] = decimal.Parse(row.Cells["NedeljnaPotrosnja"].Value.ToString()) * decimal.Parse(lblTotal1.Text) / (decimal)100;
                //newRow["MesecnaPotrosnjaKolicina"] = decimal.Parse(row.Cells["NedeljnaPotrosnja"].Value.ToString()) * (decimal)4.5;
                newRow["MesecnaPotrosnjaKolicina"] = decimal.Parse(row.Cells["DnevnaPotrosnja"].Value.ToString()) * 31;
                //newRow["MesecnaPotrosnjaIznos"] = decimal.Parse(row.Cells["NedeljnaPotrosnja"].Value.ToString()) * (decimal)4.5 * decimal.Parse(lblTotal1.Text) / (decimal)100;
                newRow["MesecnaPotrosnjaIznos"] = decimal.Parse(row.Cells["DnevnaPotrosnja"].Value.ToString()) * 31 * decimal.Parse(lblTotal1.Text) / (decimal)100;
                //newRow["GodisnjaPotrosnjaKolicina"] = decimal.Parse(newRow["MesecnaPotrosnjaKolicina"].ToString()) * (decimal)12;
                newRow["GodisnjaPotrosnjaKolicina"] = decimal.Parse(row.Cells["DnevnaPotrosnja"].Value.ToString()) * 365;
                //newRow["GodisnjaPotrosnjaIznos"] = decimal.Parse(newRow["MesecnaPotrosnjaKolicina"].ToString()) * (decimal)12 * decimal.Parse(lblTotal1.Text) / (decimal)100;
                newRow["GodisnjaPotrosnjaIznos"] = decimal.Parse(row.Cells["DnevnaPotrosnja"].Value.ToString()) * 365 * decimal.Parse(lblTotal1.Text) / (decimal)100;

                rds.Tables["KalkulacijaPotrosnjaStavka"].Rows.Add(newRow);
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rd.Load("Reports/KalkulacijaPotrosnja.rpt");
            rd.SetDataSource(rds);

            frmPrint objfrmPrint = new frmPrint();
            objfrmPrint.crystalReportViewer1.ReportSource = rd;
            objfrmPrint.Show();
            
        }

        private void btnIzmeniPotrosnju_Click(object sender, EventArgs e)
        {
            //KategorijaZivotinjaPotrosnja kategorijaZivotinjaPotrosnja = new KategorijaZivotinjaPotrosnja(_kategorijaZivotinjaBL, _smesaBL, _kategorijaZivotinjaSmesaPotrosnjaBL);
            //kategorijaZivotinjaPotrosnja.Saved += kategorijaZivotinjaPotrosnja_Saved;
            //kategorijaZivotinjaPotrosnja.ShowDialog();
        }

        //private void kategorijaZivotinjaPotrosnja_Saved()
        //{
            //calculatePotrosnja();
        //}
    }
}
