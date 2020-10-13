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
    public delegate void KalkulacijaSelectedHandler(string selectedIds);

    public partial class KalkulacijaSelector : Form
    {
        private IKalkulacijaViewBL _kalkulacijaViewBL;

        public event KalkulacijaSelectedHandler KalkulacijaSelected;

        public KalkulacijaSelector(IKalkulacijaViewBL kalkulacijaViewBL)
        {
            InitializeComponent();
            _kalkulacijaViewBL = kalkulacijaViewBL;
        }

        private void KalkulacijaSelector_Load(object sender, EventArgs e)
        {
            chkKalkulacije.Items.Clear();
            chkKalkulacije.DataSource = _kalkulacijaViewBL.GetAll();
            chkKalkulacije.DisplayMember = "FullName";
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            string selectedIds = string.Empty;
            for (int i = 0; i < chkKalkulacije.CheckedItems.Count; i++)
                selectedIds += ((KalkulacijaView)chkKalkulacije.CheckedItems[i]).ID + ",";

            if (selectedIds != string.Empty)
                selectedIds = selectedIds.Substring(0, selectedIds.Length - 1);

            if (KalkulacijaSelected != null)
                KalkulacijaSelected(selectedIds);
        }

        private void btnOdaberiSve_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkKalkulacije.Items.Count; i++)
                chkKalkulacije.SetItemChecked(i, true);
        }
    }
}
