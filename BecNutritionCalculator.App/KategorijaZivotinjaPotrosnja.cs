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
    public delegate void KategorijaZivotinjaPotrosnjaSavedHandler();

    public partial class KategorijaZivotinjaPotrosnja : Form
    {
        private IKategorijaZivotinjaBL _kategorijaZivotinjaBL;
        private ISmesaBL _smesaBL;
        private IKategorijaZivotinjaSmesaPotrosnjaBL _kategorijaZivotinjaSmesaPotrosnjaBL;
        private KategorijaZivotinjaSmesaPotrosnja _kategorijaZivotinjaSmesaPotrosnja;
        //private KategorijaZivotinja _kategorijaZivotinja;
        //private KategorijaZivotinjaSmesa _kategorijaZivotinjaSmesa;
        public event KategorijaZivotinjaPotrosnjaSavedHandler Saved;

        public KategorijaZivotinjaPotrosnja(IKategorijaZivotinjaBL kategorijaZivotinjaBL, ISmesaBL smesaBL, IKategorijaZivotinjaSmesaPotrosnjaBL kategorijaZivotinjaSmesaPotrosnjaBL)
        {
            InitializeComponent();
            _kategorijaZivotinjaBL = kategorijaZivotinjaBL;
            _smesaBL = smesaBL;
            _kategorijaZivotinjaSmesaPotrosnjaBL = kategorijaZivotinjaSmesaPotrosnjaBL;
        }

        private void KategorijaZivotinjaPotrosnja_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            cmbKategorijaZivotinja.DisplayMember = "Naziv";
            cmbKategorijaZivotinja.ValueMember = "ID";
            cmbKategorijaZivotinja.DataSource = _kategorijaZivotinjaBL.GetAll();
            

            cmbSmesa.DataSource = _smesaBL.GetAll();
            cmbSmesa.DisplayMember = "Naziv";
            cmbSmesa.ValueMember = "ID";
        }

        private void cmbKategorijaZivotinja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKategorijaZivotinja.SelectedIndex > -1 //&& cmbSmesa.SelectedIndex > -1
                ) {
                //_kategorijaZivotinja = _kategorijaZivotinjaBL.GetByID(int.Parse(cmbKategorijaZivotinja.SelectedValue.ToString()));
                //_kategorijaZivotinjaSmesa = 
                _kategorijaZivotinjaSmesaPotrosnja = _kategorijaZivotinjaSmesaPotrosnjaBL.GetByKategorijaZivotinjaID(int.Parse(cmbKategorijaZivotinja.SelectedValue.ToString()));

                cmbSmesa.SelectedValue = _kategorijaZivotinjaSmesaPotrosnja.SmesaID;
                txtBrojZivotinja.Text = _kategorijaZivotinjaSmesaPotrosnja.BrojZivotinja.ToString();
                txtDnevnaPotrosnja.Text = _kategorijaZivotinjaSmesaPotrosnja.DnevnaPotrosnja.ToString();
                txtNedeljniBrojHranjenja.Text = _kategorijaZivotinjaSmesaPotrosnja.NedeljniBrojHranjenja.ToString();

                txtMesecnaPotrosnja.Text = string.Format("{0:N2}", _kategorijaZivotinjaSmesaPotrosnja.DnevnaPotrosnja * _kategorijaZivotinjaSmesaPotrosnja.BrojZivotinja * 31);
            }
        }

        private void izracunajPotrosnju(int type)
        {
            if(type == 1)
            {
                txtMesecnaPotrosnja.Text = string.Format("{0:N2}", int.Parse(txtBrojZivotinja.Text) * decimal.Parse(txtDnevnaPotrosnja.Text) * 31);
            }
            else if(type == 2)
            {
                txtDnevnaPotrosnja.Text = string.Format("{0:N2}", decimal.Parse(txtMesecnaPotrosnja.Text) / 31 / int.Parse(txtBrojZivotinja.Text));
            }
            else if(type == 3)
            {
                txtBrojZivotinja.Text = (int.Parse(Math.Round(decimal.Parse(txtMesecnaPotrosnja.Text) / 31 / decimal.Parse(txtDnevnaPotrosnja.Text), 0).ToString())).ToString();
            }
        }

        private void btnIzracunajDnevnuPotrosnju_Click(object sender, EventArgs e)
        {
            izracunajPotrosnju(2);
        }

        private void btnIzracunajMesecnuPotrosnju_Click(object sender, EventArgs e)
        {
            izracunajPotrosnju(1);
        }

        private void btnIzracunajBrojZivotinja_Click(object sender, EventArgs e)
        {
            izracunajPotrosnju(3);
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            int validator;
            decimal decimalValidator;
            if(cmbKategorijaZivotinja.SelectedIndex > -1
                && cmbSmesa.SelectedIndex > -1
                && txtBrojZivotinja.Text.Length > 0
                && int.TryParse(txtBrojZivotinja.Text, out validator)
                && txtDnevnaPotrosnja.Text.Length > 0
                && decimal.TryParse(txtDnevnaPotrosnja.Text, out decimalValidator)
                && txtNedeljniBrojHranjenja.Text.Length > 0
                && int.TryParse(txtNedeljniBrojHranjenja.Text, out validator)
                && txtMesecnaPotrosnja.Text.Length > 0
                && decimal.TryParse(txtMesecnaPotrosnja.Text, out decimalValidator))
            { 
                    KategorijaZivotinjaSmesaPotrosnja kategorijaZivotinjaSmesaPotrosnja = new KategorijaZivotinjaSmesaPotrosnja();
                    kategorijaZivotinjaSmesaPotrosnja.KategorijaZivotinjaID = int.Parse(cmbKategorijaZivotinja.SelectedValue.ToString());
                    kategorijaZivotinjaSmesaPotrosnja.BrojZivotinja = int.Parse(txtBrojZivotinja.Text);
                    kategorijaZivotinjaSmesaPotrosnja.SmesaID = int.Parse(cmbSmesa.SelectedValue.ToString());
                    kategorijaZivotinjaSmesaPotrosnja.DnevnaPotrosnja = decimal.Parse(txtDnevnaPotrosnja.Text);
                    kategorijaZivotinjaSmesaPotrosnja.NedeljniBrojHranjenja = int.Parse(txtNedeljniBrojHranjenja.Text);

                _kategorijaZivotinjaSmesaPotrosnjaBL.SaveItem(kategorijaZivotinjaSmesaPotrosnja);

                if (Saved != null)
                    Saved();
                MessageBox.Show("Uspešno sačuvano");
                this.Close();
            }
        }
    }
}
