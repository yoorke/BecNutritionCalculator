using BecNutritionCalculator.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Main(ISirovinaBL sirovinaBL, INutritivniElementVrednostBL nutritivniElementVrednostBL, ISmesaBL smesaBL, ITipSirovineBL tipSirovineBL, IJmBL jmBL, ISirovinaNutritivniElementVrednostBL sirovinaNutritivniElementVrednostBL)
        {
            InitializeComponent();
            _sirovinaBL = sirovinaBL;
            _nutritivniElementVrednostBL = nutritivniElementVrednostBL;
            _smesaBL = smesaBL;
            _tipSirovineBL = tipSirovineBL;
            _jmBL = jmBL;
            _sirovinaNutritivniElementVrednostBL = sirovinaNutritivniElementVrednostBL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator(_sirovinaBL, _nutritivniElementVrednostBL, _smesaBL, _tipSirovineBL, _jmBL, _sirovinaNutritivniElementVrednostBL);
            calculator.MdiParent = this;
            calculator.Show();
        }

        private void kalkulacijaSmešeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator(_sirovinaBL, _nutritivniElementVrednostBL, _smesaBL, _tipSirovineBL, _jmBL, _sirovinaNutritivniElementVrednostBL);
            calculator.MdiParent = this;
            calculator.Show();
        }
    }
}
