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
    public delegate void KolicinaIzracunataHandler(decimal kolicina, Sirovina sirovina);

    public partial class NevCalculator : Form
    {
        private ISirovinaBL _sirovinaBL;
        private decimal _total;
        private decimal _zahtevano;
        private int _smesaJmID;
        private string _nutritivniElementNaziv;
        private INutritivniElementVrednostBL _nutritivniElementVrednostBL;

        private Sirovina _sirovina;

        public event KolicinaIzracunataHandler KolicinaIzracunata;

        public NevCalculator(ISirovinaBL sirovinaBL, INutritivniElementVrednostBL nutritivniElementVrednostBL, decimal total, decimal zahtevano, int smesaJmID, string nutritivniElementNaziv)
        {
            InitializeComponent();
            _sirovinaBL = sirovinaBL;
            _nutritivniElementVrednostBL = nutritivniElementVrednostBL;
            _total = total;
            _zahtevano = zahtevano;
            _smesaJmID = smesaJmID;
            _nutritivniElementNaziv = nutritivniElementNaziv;
        }

        private void NevCalculator_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            cmbSirovina.SelectedIndexChanged += cmbSirovina_SelectedIndexChanged;
            cmbSirovina.DataSource = _sirovinaBL.GetByNutritivniElementNaziv(_nutritivniElementNaziv);
            cmbSirovina.DisplayMember = "Naziv";
            cmbSirovina.ValueMember = "ID";
            

            lblTotal.Text = string.Format("{0:N4}", _total);
            lblZahtevano.Text = string.Format("{0:N4}", _zahtevano);

            btnDodaj.Enabled = false;
        }

        private void cmbSirovina_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDodaj.Enabled = false;
            if(cmbSirovina.SelectedIndex > -1)
                lblJm.Text = ((Sirovina)cmbSirovina.SelectedItem).JmNaziv;
        }

        private void btnIzracunaj_Click(object sender, EventArgs e)
        {
            if(cmbSirovina.SelectedIndex > -1)
            {
                _sirovina = ((Sirovina)cmbSirovina.SelectedItem);
                var nutritivniElement = _nutritivniElementVrednostBL.GetBySirovinaID(_sirovina.ID).First(x => x.SkraceniNaziv == _nutritivniElementNaziv);
                decimal kolicina = 0;
                txtKolicina.Text = "0,00";
                int jmIndex = _sirovina.JmID == 5 ? 1 : 1000;

                //1-3
                if (nutritivniElement.JmID == 1 && _smesaJmID == 3 && nutritivniElement.Vrednost > 0)
                {
                    kolicina = (_zahtevano - _total) * 100 / (nutritivniElement.Vrednost / 100 * 1000);
                    //btnDodaj.Enabled = false;
                    txtKolicina.Text = string.Format("{0:N4}", kolicina);
                    btnDodaj.Enabled = true;
                    lblJm.Text = _sirovina.JmNaziv;
                }
                else if(nutritivniElement.JmID == 1 && _smesaJmID == 1 && nutritivniElement.Vrednost > 0)
                {
                    kolicina = (_zahtevano - _total) * jmIndex / nutritivniElement.Vrednost * 100;
                    txtKolicina.Text = string.Format("{0:N4}", kolicina);
                    btnDodaj.Enabled = true;
                    lblJm.Text = _sirovina.JmNaziv;
                }
                else if(nutritivniElement.JmID == 4 && _smesaJmID == 4 && nutritivniElement.Vrednost > 0)
                {
                    kolicina = (_zahtevano - _total) * 100 / nutritivniElement.Vrednost;
                    txtKolicina.Text = string.Format("{0:N2}", kolicina);
                    btnDodaj.Enabled = true;
                    lblJm.Text = _sirovina.JmNaziv;
                }
                else if(nutritivniElement.JmID == 3 && _smesaJmID == 3 && nutritivniElement.Vrednost > 0)
                {
                    kolicina = (_zahtevano - _total) * 100 * jmIndex / nutritivniElement.Vrednost;
                    txtKolicina.Text = string.Format("{0:N4}", kolicina);
                    btnDodaj.Enabled = true;
                    lblJm.Text = _sirovina.JmNaziv;
                }
                else if(nutritivniElement.JmID == 1 && _smesaJmID == 7 && nutritivniElement.Vrednost > 0)
                {
                    kolicina = (_zahtevano - _total) * 100 * 100 / nutritivniElement.Vrednost / 1000 / 1000;
                    txtKolicina.Text = string.Format("{0:N4}", kolicina);
                    btnDodaj.Enabled = true;
                    lblJm.Text = _sirovina.JmNaziv;
                }
                else if (nutritivniElement.Vrednost == 0)
                { 
                    MessageBox.Show("Sirovina ne sadrži dati nutritivni element", "Izračunavanje količine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnDodaj.Enabled = false;
                }
                else
                { 
                    MessageBox.Show("Nije moguće izračunati količinu za dati nutritivni element", "Izračunavanje količine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnDodaj.Enabled = false;
                }
            }
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (KolicinaIzracunata != null)
            { 
                KolicinaIzracunata(decimal.Parse(txtKolicina.Text), _sirovina);
                this.Close();
            }
        }
    }
}
