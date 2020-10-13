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
    public delegate void SmesaSavedHandler();

    public partial class SmesaForm : Form
    {
        private int _smesaID = -1;
        private ISmesaBL _smesaBL;
        private ISmesaNutritivniElementVrednostBL _smesaNutritivniElementVrednostBL;
        private IEnumerable<SmesaNutritivniElementVrednost> _vrednosti;

        public event SmesaSavedHandler SmesaSaved;

        public SmesaForm(int smesaID, ISmesaBL smesaBL
            , ISmesaNutritivniElementVrednostBL smesaNutritivniElementVrednostBL
            )
        {
            InitializeComponent();
            _smesaID = smesaID;
            _smesaBL = smesaBL;
            _smesaNutritivniElementVrednostBL = smesaNutritivniElementVrednostBL;
        }

        private void Smesa_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            if (_smesaID > 0)
                loadSmesa();
        }

        private void loadSmesa()
        {
            Smesa smesa = _smesaBL.GetByID(_smesaID);

            txtNaziv.Text = smesa.Naziv;

            _vrednosti = _smesaNutritivniElementVrednostBL.GetBySmesaID(_smesaID);

            setDataGridView();
        }

        private void setDataGridView()
        {
            dgvVrednosti.Rows.Clear();
            dgvVrednosti.DataSource = _vrednosti;
            dgvVrednosti.RowHeadersVisible = false;
            dgvVrednosti.AllowUserToAddRows = false;
            dgvVrednosti.AllowUserToDeleteRows = false;
            dgvVrednosti.AllowUserToResizeRows = false;
            dgvVrednosti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvVrednosti.Columns["NutritivniElementID"].Visible = false;
            dgvVrednosti.Columns["SmesaID"].Visible = false;
            dgvVrednosti.Columns["Naziv"].Width = 200;
            dgvVrednosti.Columns["Naziv"].ReadOnly = true;
            dgvVrednosti.Columns["Naziv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvVrednosti.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVrednosti.Columns["Naziv"].DisplayIndex = 0;
            dgvVrednosti.Columns["Vrednost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvVrednosti.Columns["Vrednost"].Width = 200;
            dgvVrednosti.Columns["Vrednost"].DefaultCellStyle.Format = "N2";
            dgvVrednosti.Columns["StaraVrednost"].Visible = false;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            _smesaID = saveSmesa();

            foreach(DataGridViewRow row in dgvVrednosti.Rows)
            {
                if (decimal.Parse(row.Cells["Vrednost"].Value.ToString()) != decimal.Parse(row.Cells["StaraVrednost"].Value.ToString()))
                    _smesaNutritivniElementVrednostBL.SaveVrednost(new SmesaNutritivniElementVrednost()
                    {
                        SmesaID = _smesaID,
                        NutritivniElementID = int.Parse(row.Cells["NutritivniElementID"].Value.ToString()),
                        Naziv = row.Cells["Naziv"].Value.ToString(),
                        Vrednost = decimal.Parse(row.Cells["Vrednost"].Value.ToString())
                    });
            }

            if (SmesaSaved != null)
                SmesaSaved();

            MessageBox.Show("Smeša uspešno sačuvana.", "Unos smeše", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private int saveSmesa()
        {
            try
            {
                Smesa smesa = new Smesa();
                smesa.Naziv = txtNaziv.Text;
                smesa.ID = _smesaID;

                return _smesaBL.Save(smesa);
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
            _smesaID = -1;

            _vrednosti = _smesaNutritivniElementVrednostBL.GetBySmesaID(1);
            foreach(DataGridViewRow row in dgvVrednosti.Rows)
            {
                row.Cells["Vrednost"].Value = decimal.Parse("0");
                row.Cells["StaraVrednost"].Value = decimal.Parse("-1");
            }

            txtNaziv.Select();
        }

        private void btnNovaSmesa_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
