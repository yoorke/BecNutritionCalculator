using BecNutritionCalculator.BusinessLogic.Interfaces;
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
    public delegate void IDSelectedHandler(int id);
    public delegate void IDsSelectedHandler(string ids);

    public partial class Kalkulacije : Form
    {
        private IKalkulacijaViewBL _kalkulacijaViewBL;
        private IKalkulacijaBL _kalkulacijaBL;
        public IDSelectedHandler KalkulacijaSelected;
        public IDsSelectedHandler KalkulacijeSelected;

        public Kalkulacije(IKalkulacijaViewBL kalkulacijaViewBL, IKalkulacijaBL kalkulacijaBL)
        {
            InitializeComponent();
            _kalkulacijaViewBL = kalkulacijaViewBL;
            _kalkulacijaBL = kalkulacijaBL;
        }

        private void Kalkulacije_Load(object sender, EventArgs e)
        {
            loadKalkulacije();
        }

        private void loadKalkulacije()
        {
            dgvKalkulacije.DataSource = _kalkulacijaViewBL.GetAll();
            setDataGridView();
        }

        private void setDataGridView()
        {
            dgvKalkulacije.AllowUserToAddRows = false;
            dgvKalkulacije.AllowUserToDeleteRows = false;
            dgvKalkulacije.AllowUserToResizeRows = false;
            dgvKalkulacije.RowHeadersVisible = false;
            //dgvKalkulacije.Columns["KalkulacijaID"].Visible = false;
            dgvKalkulacije.Columns["ID"].Visible = false;
            dgvKalkulacije.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvKalkulacije.Columns["SmesaNaziv"].HeaderText = "Smeša";
            dgvKalkulacije.Columns["DatumIzmene"].HeaderText = "Datum izmene";
            dgvKalkulacije.Columns["Naziv"].DisplayIndex = 1;
            dgvKalkulacije.Columns["SmesaNaziv"].DisplayIndex = 2;
            dgvKalkulacije.Columns["Datum"].DisplayIndex = 3;
            dgvKalkulacije.Columns["DatumIzmene"].DisplayIndex = 4;
            dgvKalkulacije.Columns["UkupnoKabaste"].DisplayIndex = 5;
            dgvKalkulacije.Columns["UkupnoKupovne"].DisplayIndex = 6;
            dgvKalkulacije.Columns["Ukupno"].DisplayIndex = 7;
            dgvKalkulacije.Columns["Ukupno"].DefaultCellStyle.Format = "N2";
            dgvKalkulacije.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvKalkulacije.Columns["UkupnoKabaste"].DefaultCellStyle.Format = "N2";
            dgvKalkulacije.Columns["UkupnoKabaste"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvKalkulacije.Columns["UkupnoKupovne"].DefaultCellStyle.Format = "N2";
            dgvKalkulacije.Columns["UkupnoKupovne"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvKalkulacije.Columns["FullName"].Visible = false;
            dgvKalkulacije.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (DataGridViewColumn column in dgvKalkulacije.Columns)
                column.ReadOnly = true;

            dgvKalkulacije.DoubleClick += dgvKalkulacije_DoubleClick;
        }

        private void dgvKalkulacije_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(this, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(dgvKalkulacije.SelectedRows.Count > 0 && KalkulacijaSelected != null)
            {
                KalkulacijaSelected(int.Parse(dgvKalkulacije.SelectedRows[0].Cells["ID"].Value.ToString()));
            }

            if(dgvKalkulacije.SelectedRows.Count > 0 && KalkulacijeSelected != null)
            {
                string ids = string.Empty;
                foreach (DataGridViewRow row in dgvKalkulacije.SelectedRows)
                    ids += row.Cells["ID"].Value.ToString() + ",";

                KalkulacijeSelected(ids.Substring(0, ids.Length - 1));
            }

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if(dgvKalkulacije.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("Da lis te sigurni da želite da obrišete kalkulaciju " + dgvKalkulacije.SelectedRows[0].Cells["Naziv"].Value.ToString() + "?", "Brisanje kalkulacije", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _kalkulacijaBL.Delete(int.Parse(dgvKalkulacije.SelectedRows[0].Cells["ID"].Value.ToString()));
                    loadKalkulacije();
                }
            }
        }
    }
}
