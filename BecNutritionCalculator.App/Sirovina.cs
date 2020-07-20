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
    public partial class SirovinaForm : Form
    {
        private int _sirovinaID = -1;
        private ISirovinaBL _sirovinaBL;
        private ISirovinaNutritivniElementVrednostBL _sirovinaNutritivniElementVrednostBL;
        private ITipSirovineBL _tipSirovineBL;
        private IJmBL _jmBL;
        private IEnumerable<SirovinaNutritivniElementVrednost> _nutritivniElementiVrednosti;

        public SirovinaForm(int sirovinaID, ISirovinaBL sirovinaBL, ISirovinaNutritivniElementVrednostBL sirovinaNutritivniElementiBL, ITipSirovineBL tipSirovineBL, IJmBL jmBL)
        {
            InitializeComponent();
            _sirovinaBL = sirovinaBL;
            _sirovinaID = sirovinaID;
            _tipSirovineBL = tipSirovineBL;
            _jmBL = jmBL;
            _sirovinaNutritivniElementVrednostBL = sirovinaNutritivniElementiBL;
        }

        private void Sirovina_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;

            loadData();

            if (_sirovinaID > 0)
                loadSirovina();
        }

        private void loadSirovina()
        {
            Sirovina sirovina = _sirovinaBL.GetByID(_sirovinaID);

            txtNaziv.Text = sirovina.Naziv;
            cmbTipSirovine.SelectedValue = sirovina.TipSirovineID;
            cmbJm.SelectedValue = sirovina.JmID;

            
            _nutritivniElementiVrednosti = _sirovinaNutritivniElementVrednostBL.GetBySirovinaID(_sirovinaID);

            setDataGridView();
        }

        private void setDataGridView()
        {
            dgvVrednosti.Rows.Clear();
            dgvVrednosti.DataSource = _nutritivniElementiVrednosti;
            dgvVrednosti.RowHeadersVisible = false;
            dgvVrednosti.AllowUserToAddRows = false;
            dgvVrednosti.AllowUserToResizeRows = false;
            dgvVrednosti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvVrednosti.Columns["NutritivniElementID"].Visible = false;
            dgvVrednosti.Columns["SirovinaID"].Visible = false;
            dgvVrednosti.Columns["Naziv"].Width = 200;
            dgvVrednosti.Columns["Naziv"].ReadOnly = true;
            dgvVrednosti.Columns["Naziv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvVrednosti.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVrednosti.Columns["Naziv"].DisplayIndex = 0;
            dgvVrednosti.Columns["Vrednost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvVrednosti.Columns["Vrednost"].Width = 200;
            dgvVrednosti.Columns["Vrednost"].DefaultCellStyle.Format = "N2";
            dgvVrednosti.Columns["StaraVrednost"].Visible = false;
            //dgvVrednosti.Columns["SkraceniNaziv"].Visible = false;
            //dgvVrednosti.Columns["SirovinaID"].Visible = false;
        }

        private void loadData()
        {
            cmbTipSirovine.DataSource = _tipSirovineBL.GetAll();
            cmbTipSirovine.DisplayMember = "Naziv";
            cmbTipSirovine.ValueMember = "ID";

            cmbJm.DataSource = _jmBL.GetAll();
            cmbJm.DisplayMember = "Naziv";
            cmbJm.ValueMember = "ID";

        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            foreach(DataRow row in dgvVrednosti.Rows)
            {
                if(decimal.Parse(row["Vrednost"].ToString()) != decimal.Parse(row["StaraVrednost"].ToString()))
                    _sirovinaNutritivniElementVrednostBL.UpdateVrednost(new SirovinaNutritivniElementVrednost()
                    {
                        SirovinaID = _sirovinaID,
                        NutritivniElementID = int.Parse(row["NutritivniElementID"].ToString()),
                        Naziv = row["Naziv"].ToString(),
                        //SkraceniNaziv = row["SkraceniNaziv"].ToString(),
                        Vrednost = decimal.Parse(row["Vrednost"].ToString())
                    }
                    );
            }
        }
    }
}
