using BecNutritionCalculator.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BecNutritionCalculator.App
{
    public partial class Main : Form
    {
        private ISirovinaBL _sirovinaBL;
        private INutritivniElementVrednostBL _nutritivniElementVrednostBL;
        private ISmesaBL _smesaBL;
        private ITipSirovineBL _tipSirovineBL;
        private IJmBL _jmBL;
        private ISirovinaNutritivniElementVrednostBL _sirovinaNutritivniElementVrednostBL;
        private IKalkulacijaBL _kalkulacijaBL;
        private IKalkulacijaViewBL _kalkulacijaViewBL;
        private IKategorijaZivotinjaSmesaPotrosnjaBL _kategorijaZivotinjaSmesaPotrosnjaBL;
        private IExportBL _exportBL;
        private ISmesaNutritivniElementVrednostBL _smesaNutritivniElementVrednostBL;
        private IKategorijaZivotinjaBL _kategorijaZivotinjaBL;
        private int _reportID;

        public Main(ISirovinaBL sirovinaBL, INutritivniElementVrednostBL nutritivniElementVrednostBL, ISmesaBL smesaBL, ITipSirovineBL tipSirovineBL, IJmBL jmBL, ISirovinaNutritivniElementVrednostBL sirovinaNutritivniElementVrednostBL, IKalkulacijaBL kalkulacijaBL, IKalkulacijaViewBL kalkulacijaViewBL, IKategorijaZivotinjaSmesaPotrosnjaBL kategorijaZivotinjaSmesaPotrosnjaBL, IExportBL exportBL, ISmesaNutritivniElementVrednostBL smesaNutritivniElementVrednostBL, IKategorijaZivotinjaBL kategorijaZivotinjaBL)
        {
            InitializeComponent();
            _sirovinaBL = sirovinaBL;
            _nutritivniElementVrednostBL = nutritivniElementVrednostBL;
            _smesaBL = smesaBL;
            _tipSirovineBL = tipSirovineBL;
            _jmBL = jmBL;
            _sirovinaNutritivniElementVrednostBL = sirovinaNutritivniElementVrednostBL;
            _kalkulacijaBL = kalkulacijaBL;
            _kalkulacijaViewBL = kalkulacijaViewBL;
            _kategorijaZivotinjaSmesaPotrosnjaBL = kategorijaZivotinjaSmesaPotrosnjaBL;
            _exportBL = exportBL;
            _smesaNutritivniElementVrednostBL = smesaNutritivniElementVrednostBL;
            _kategorijaZivotinjaBL = kategorijaZivotinjaBL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Calculator calculator = new Calculator(_sirovinaBL, _nutritivniElementVrednostBL, _smesaBL, _tipSirovineBL, _jmBL, _sirovinaNutritivniElementVrednostBL, _kalkulacijaBL, _kalkulacijaViewBL, -1);
            //calculator.MdiParent = this;
            //calculator.Show();
        }

        private void kalkulacijaSmešeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator(_sirovinaBL, _nutritivniElementVrednostBL, _smesaBL, _tipSirovineBL, _jmBL, _sirovinaNutritivniElementVrednostBL, _kalkulacijaBL, _kalkulacijaViewBL, _kategorijaZivotinjaSmesaPotrosnjaBL, _exportBL, _smesaNutritivniElementVrednostBL, -1);
            calculator.MdiParent = this;
            calculator.Show();
        }

        private void kalkulacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
            kalkulacije.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Kalkulator smeše v" + ConfigurationManager.AppSettings["appVersion"];
        }

        private void potrošnjaKabastihSirovinaUPerioduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reportID = 1;
            Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
            kalkulacije.KalkulacijeSelected += Kalkulacije_KalkulacijeSelected;
            kalkulacije.ShowDialog();
        }

        protected void Kalkulacije_KalkulacijeSelected(string ids)
        {
            DataTable potrosnja = _sirovinaBL.GetPotrosnjaUPeriodu(ids);
            Reports.reportDataSet rds = new Reports.reportDataSet();
            DataRow newRow = rds.Tables["PotrosnjaKabastihSirovinaUPeriodu"].NewRow();
            foreach(DataRow row in potrosnja.Rows)
            {
                newRow = rds.Tables["PotrosnjaKabastihSirovinaUPeriodu"].NewRow();
                newRow["NazivKalkulacije"] = row["kalkulacija_naziv"];
                newRow["NazivSirovine"] = row["sirovina_naziv"];
                newRow["Kolicina"] = row["kolicina"];
                newRow["MesecnaPotrosnja"] = row["mesecno"];
                newRow["GodisnjaPotrosnja"] = row["godisnje"];
                rds.Tables["PotrosnjaKabastihSirovinaUPeriodu"].Rows.Add(newRow);
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (_reportID == 1)
                rd.Load("Reports/PotrosnjaKabastihSirovinaUPeriodu.rpt");
            else if (_reportID == 2)
                rd.Load("Reports/PotrosnjaKabastihSirovina.rpt");
            rd.SetDataSource(rds);

            frmPrint objfrmPrint = new frmPrint();
            objfrmPrint.crystalReportViewer1.ReportSource = rd;
            objfrmPrint.Show();
        }

        private void potrošnjaKabastihSirovinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reportID = 2;
            Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
            kalkulacije.KalkulacijeSelected += Kalkulacije_KalkulacijeSelected;
            kalkulacije.ShowDialog();
            //potrošnjaKabastihSirovinaUPerioduToolStripMenuItem_Click(sender, e);
        }

        private void kategorijeŽivotinjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KategorijaZivotinjaPotrosnja kategorijaZivotinjaPotrosnja = new KategorijaZivotinjaPotrosnja(_kategorijaZivotinjaBL, _smesaBL, _kategorijaZivotinjaSmesaPotrosnjaBL);
            kategorijaZivotinjaPotrosnja.ShowDialog();
        }

        private void troškoviPoKalkulacijamaDetaljnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
            kalkulacije.KalkulacijeSelected += TroskoviKalkulacije_KalkulacijeSelected;
            kalkulacije.ShowDialog();
        }

        protected void TroskoviKalkulacije_KalkulacijeSelected(string ids)
        {
            DataTable troskovi = _kalkulacijaBL.GetTroskovi(ids);
            Reports.reportDataSet rds = new Reports.reportDataSet();
            DataRow newRow;
            foreach(DataRow row in troskovi.Rows)
            {
                newRow = rds.Tables["KalkulacijeTroskovi"].NewRow();
                newRow["KalkulacijaID"] = row["kalkulacija_id"];
                newRow["KalkulacijaNaziv"] = row["kalkulacija_naziv"];
                newRow["SmesaNaziv"] = row["smesa_naziv"];
                newRow["SirovinaNaziv"] = row["sirovina_naziv"];
                newRow["Kolicina"] = row["kolicina"];
                newRow["JmID"] = row["jm_id"];
                newRow["JmNaziv"] = row["jm_naziv"];
                newRow["SirovinaCena"] = row["sirovina_cena"];
                newRow["Ukupno"] = row["ukupno"];

                rds.Tables["KalkulacijeTroskovi"].Rows.Add(newRow);
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rd.Load("Reports/KalkulacijeTroskovi.rpt");
            rd.SetDataSource(rds);

            frmPrint objfrmPrint = new frmPrint();
            objfrmPrint.crystalReportViewer1.ReportSource = rd;
            objfrmPrint.Show();
        }

        protected void PotrosnjaKalkulacije_KalkulacijeSelected(string ids)
        {
            DataTable potrosnja = _kalkulacijaBL.GetPotrosnja(ids);
            Reports.reportDataSet rds = new Reports.reportDataSet();
            DataRow newRow;
            foreach(DataRow row in potrosnja.Rows)
            {
                newRow = rds.Tables["KalkulacijePotrosnja"].NewRow();
                newRow["KalkulacijaID"] = row["kalkulacija_id"];
                newRow["KalkulacijaNaziv"] = row["kalkulacija_naziv"];
                newRow["SmesaNaziv"] = row["smesa_naziv"];
                newRow["Dnevno"] = row["dnevno"];
                newRow["Mesecno"] = row["mesecno"];
                newRow["Godisnje"] = row["godisnje"];

                rds.Tables["KalkulacijePotrosnja"].Rows.Add(newRow);
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rd.Load("Reports/KalkulacijePotrosnja.rpt");
            rd.SetDataSource(rds);

            frmPrint objfrmPrint = new frmPrint();
            objfrmPrint.crystalReportViewer1.ReportSource = rd;
            objfrmPrint.Show();
        }

        private void potrošnjaPoKalkulacijamaDetaljnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
            kalkulacije.KalkulacijeSelected += PotrosnjaKalkulacije_KalkulacijeSelected;
            kalkulacije.ShowDialog();
        }
    }
}
