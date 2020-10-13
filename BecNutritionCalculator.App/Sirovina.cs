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
    public delegate void SirovinaSavedHandler();

    public partial class SirovinaForm : Form
    {
        private int _sirovinaID = -1;
        private ISirovinaBL _sirovinaBL;
        private ISirovinaNutritivniElementVrednostBL _sirovinaNutritivniElementVrednostBL;
        private ITipSirovineBL _tipSirovineBL;
        private IJmBL _jmBL;
        private IEnumerable<SirovinaNutritivniElementVrednost> _nutritivniElementiVrednosti;

        public event SirovinaSavedHandler SirovinaSaved;

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
            txtKolicinskiOdnos.Text = sirovina.KolicinskiOdnos.ToString();
            txtCena.Text = string.Format("{0:N2}", sirovina.Cena);
            chkIsActive.Checked = sirovina.is_active;

            
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
            //if (_sirovinaID == -1)
                _sirovinaID = saveSirovina();

            if(_sirovinaID > 0)
            { 
                foreach (DataGridViewRow row in dgvVrednosti.Rows)
                {
                    if(decimal.Parse(row.Cells["Vrednost"].Value.ToString()) != decimal.Parse(row.Cells["StaraVrednost"].Value.ToString()))
                        _sirovinaNutritivniElementVrednostBL.SaveVrednost(new SirovinaNutritivniElementVrednost()
                        {
                            SirovinaID = _sirovinaID,
                            NutritivniElementID = int.Parse(row.Cells["NutritivniElementID"].Value.ToString()),
                            Naziv = row.Cells["Naziv"].Value.ToString(),
                            //SkraceniNaziv = row["SkraceniNaziv"].ToString(),
                            Vrednost = decimal.Parse(row.Cells["Vrednost"].Value.ToString())
                        }
                        );
                }

                if (SirovinaSaved != null)
                    SirovinaSaved();

                MessageBox.Show("Sirovina uspešno sačuvana.", "Unos sirovine", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private int saveSirovina()
        {
            try
            { 
                Sirovina sirovina = new Sirovina();
                sirovina.Naziv = txtNaziv.Text;
                sirovina.TipSirovineID = int.Parse(cmbTipSirovine.SelectedValue.ToString());
                sirovina.JmID = int.Parse(cmbJm.SelectedValue.ToString());
                sirovina.ID = _sirovinaID;
                sirovina.KolicinskiOdnos = decimal.Parse(txtKolicinskiOdnos.Text);
                sirovina.Cena = decimal.Parse(txtCena.Text);
                sirovina.is_active = chkIsActive.Checked;

                return _sirovinaBL.Save(sirovina);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Proverite unete podatke." + ex.Message);
                return -1;
            }
        }

        private void reset()
        {
            txtNaziv.Text = string.Empty;
            cmbTipSirovine.SelectedIndex = -1;
            cmbJm.SelectedIndex = -1;
            _sirovinaID = -1;
            txtKolicinskiOdnos.Text = "1";


            _nutritivniElementiVrednosti = _sirovinaNutritivniElementVrednostBL.GetBySirovinaID(1);
            foreach (DataGridViewRow row in dgvVrednosti.Rows) { 
                row.Cells["Vrednost"].Value = decimal.Parse("0");
                row.Cells["StaraVrednost"].Value = decimal.Parse("-1");
            }


            txtNaziv.Select();
        }

        private void btnNovaSirovina_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
