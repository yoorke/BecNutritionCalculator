using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BecNutritionCalculator.App
{
    public partial class Calculator : Form
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
        private ISmesaNutritivniElementVrednostBL _smesaNutritivniElementVrednostBL;
        private IExportBL _exportBL;
        //private List<SirovinaNeVrednost> _sirovine;
        private DataTable _sirovineDT;
        private DataTable _smesaDT;
        private DataTable _totalDT;
        private DataTable _zahtevanoDT;
        private DataTable _amkTotalDT;
        private DataTable _sirovinaNeJm;
        private DataTable _smesaNeJm;
        private DataTable _sirovinaCenaDT;

        private Kalkulacija _kalkulacija;
        private int _kalkulacijaID;

        private bool _dataUpdated = false;

        private bool _showAmk = true;

        private SirovinaCena _sirovinaCenaForm;

        private IEnumerable<TipSirovine> _tipoviSirovine;

        public Calculator(ISirovinaBL sirovinaBL, INutritivniElementVrednostBL nutritivniElementVrednostBL, ISmesaBL smesaBL, ITipSirovineBL tipSirovineBL, IJmBL jmBL, ISirovinaNutritivniElementVrednostBL sirovinaNutritivniElementVrednostBL, IKalkulacijaBL kalkulacijaBL, IKalkulacijaViewBL kalkulacijaViewBL, IKategorijaZivotinjaSmesaPotrosnjaBL kategorijaZivotinjaSmesaPotrosnjaBL, IExportBL exportBL, ISmesaNutritivniElementVrednostBL smesaNutritivniElementVrednostBL, int kalkulacijaID)
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
            _smesaNutritivniElementVrednostBL = smesaNutritivniElementVrednostBL;
            _exportBL = exportBL;

            _kalkulacijaID = kalkulacijaID;

            //_sirovine = new List<SirovinaNeVrednost>();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            setDataGridViewSize();

            cmbSirovina.DataSource = _sirovinaBL.GetAll(chkShowAllSirovina.Checked);
            cmbSirovina.DisplayMember = "Naziv";
            cmbSirovina.ValueMember = "ID";
            cmbSirovina.KeyPress += cmbSirovina_KeyPress;

            //cmbSmesa.SelectedIndexChanged -= cmbSmesa_SelectedIndexChanged;
            cmbSmesa.DisplayMember = "Naziv";
            cmbSmesa.ValueMember = "ID";
            cmbSmesa.DataSource = _smesaBL.GetAll();

            //cmbSmesa.SelectedIndexChanged += cmbSmesa_SelectedIndexChanged;

            this.SizeChanged += Calculator_SizeChanged;

            btnAmk.Left = this.Width - btnAmk.Width - 20;
            btnShowCosts.Left = this.Width - btnAmk.Width - btnShowCosts.Width - 40;

            _tipoviSirovine = _tipSirovineBL.GetAll();

            btnAmk.Text = "Sakrij AMK";

            setDoubleBuffered();
        }

        private void cmbSirovina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnDodajSirovinu_Click(this, null);
                e.Handled = true;
            }
        }

        private void Calculator_SizeChanged(object sender, EventArgs e)
        {
            setDataGridViewSize();
            //throw new NotImplementedException();
        }

        private void btnDodajSirovinu_Click(object sender, EventArgs e)
        {
            if (cmbSirovina.SelectedIndex > -1)
            {
                int sirovinaID = int.Parse(cmbSirovina.SelectedValue.ToString());
                if (getRowIndexBySirovinaID(sirovinaID) == -1)
                {
                    //Sirovina sirovina = _sirovinaBL.GetByID(sirovinaID);
                    Sirovina sirovina = (Sirovina)cmbSirovina.SelectedItem;
                    //TipSirovine tipSirovine = _tipSirovineBL.GetByID(sirovina.TipSirovineID);
                    TipSirovine tipSirovine = _tipoviSirovine.First(item => item.ID == sirovina.TipSirovineID);

                    IEnumerable<NutritivniElementVrednost> vrednosti = _nutritivniElementVrednostBL.GetBySirovinaID(sirovinaID);

                    //_sirovine.Add(new SirovinaNeVrednost()
                    //{
                    //SirovinaID = sirovinaID,
                    //Naziv = ((Sirovina)cmbSirovina.SelectedItem).Naziv,
                    //Kolicina = 0,
                    //Vrednosti = vrednosti
                    //});

                    if (_sirovineDT == null)
                        //setSirovineDT(vrednosti);
                        setDataTables(vrednosti);

                    _sirovineDT.Rows.Add(createRow(vrednosti, _sirovineDT, false, sirovina.Naziv, 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));
                    _smesaDT.Rows.Add(createRow(vrednosti, _smesaDT, true, sirovina.Naziv + " [" + sirovina.JmNaziv + "]", 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));

                    _sirovinaNeJm.Rows.Add(createRowJm(vrednosti, _sirovinaNeJm, sirovinaID));

                    _sirovinaCenaDT.Rows.Add(createRowSirovinaCena(sirovina, tipSirovine));
                    _dataUpdated = true;

                    //sortDataGrid();
                    //calculateTotal();
                }
                setActiveCell(getRowIndexBySirovinaID(sirovinaID));
            }
            else
                MessageBox.Show("Odaberite sirovinu", "Unos sirovine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void setDataTables(IEnumerable<NutritivniElementVrednost> nutritivniElementi)
        {
            _sirovineDT = new DataTable();
            _smesaDT = new DataTable();
            _totalDT = new DataTable();
            _amkTotalDT = new DataTable();
            _sirovinaNeJm = new DataTable();
            //_smesaNeJm = new DataTable();
            _sirovinaCenaDT = new DataTable();

            _sirovineDT.Columns.Add("SirovinaID", typeof(int));
            _sirovineDT.Columns.Add("TipSirovineID", typeof(int));
            _sirovineDT.Columns.Add("TipSirovineCode", typeof(string));
            _sirovineDT.Columns.Add("JmID", typeof(int));
            _sirovineDT.Columns.Add("Naziv", typeof(string));
            _sirovineDT.Columns.Add("Color", typeof(string));
            _sirovineDT.Columns.Add("Kolicina", typeof(decimal));
            

            _smesaDT.Columns.Add("SirovinaID", typeof(int));
            _smesaDT.Columns.Add("TipSirovineID", typeof(int));
            _smesaDT.Columns.Add("TipSirovineCode", typeof(string));
            _smesaDT.Columns.Add("JmID", typeof(int));
            _smesaDT.Columns.Add("Naziv", typeof(string));
            _smesaDT.Columns.Add("Color", typeof(string));
            _smesaDT.Columns.Add("Kolicina", typeof(decimal));
            

            _totalDT.Columns.Add("SirovinaID", typeof(int));
            _totalDT.Columns.Add("TipSirovineID", typeof(int));
            _totalDT.Columns.Add("TipSirovineCode", typeof(string));
            _totalDT.Columns.Add("JmID", typeof(int));
            _totalDT.Columns.Add("Naziv", typeof(string));
            _totalDT.Columns.Add("Color", typeof(string));
            _totalDT.Columns.Add("Kolicina", typeof(decimal));

            _amkTotalDT.Columns.Add("SirovinaID", typeof(int));
            _amkTotalDT.Columns.Add("TipSirovineID", typeof(int));
            _amkTotalDT.Columns.Add("TipSirovineCode", typeof(string));
            _amkTotalDT.Columns.Add("JmID", typeof(int));
            _amkTotalDT.Columns.Add("Naziv", typeof(string));
            _amkTotalDT.Columns.Add("Color", typeof(string));
            _amkTotalDT.Columns.Add("Kolicina", typeof(decimal));

            _sirovinaNeJm.Columns.Add("SirovinaID", typeof(int));
            _sirovinaNeJm.Columns.Add("TipSirovineID", typeof(int));
            _sirovinaNeJm.Columns.Add("TipSirovineCode", typeof(string));
            _sirovinaNeJm.Columns.Add("JmID", typeof(int));
            _sirovinaNeJm.Columns.Add("Naziv", typeof(string));
            _sirovinaNeJm.Columns.Add("Color", typeof(string));
            _sirovinaNeJm.Columns.Add("Kolicina", typeof(decimal));

            //_smesaNeJm.Columns.Add("SirovinaID", typeof(int));
            //_smesaNeJm.Columns.Add("TipSirovineID", typeof(int));
            //_smesaNeJm.Columns.Add("TipSirovineCode", typeof(string));
            //_smesaNeJm.Columns.Add("JmID", typeof(int));
            //_smesaNeJm.Columns.Add("Naziv", typeof(string));
            //_smesaNeJm.Columns.Add("Color", typeof(string));
            //_smesaNeJm.Columns.Add("Kolicina", typeof(decimal));

            _sirovinaCenaDT.Columns.Add("SirovinaID", typeof(int));
            _sirovinaCenaDT.Columns.Add("Naziv", typeof(string));
            _sirovinaCenaDT.Columns.Add("Jm", typeof(string));
            _sirovinaCenaDT.Columns.Add("Kolicina", typeof(decimal));
            _sirovinaCenaDT.Columns.Add("KolicinaKorigovano", typeof(decimal));
            _sirovinaCenaDT.Columns.Add("Cena", typeof(decimal));
            _sirovinaCenaDT.Columns.Add("Ukupno", typeof(decimal));
            _sirovinaCenaDT.Columns.Add("TipSirovineCode", typeof(string));
            _sirovinaCenaDT.Columns.Add("TipSirovineID", typeof(int));
            _sirovinaCenaDT.Columns.Add("Color", typeof(string));
            _sirovinaCenaDT.Columns.Add("KolicinskiOdnos", typeof(decimal));
            
            

            foreach(var ne in nutritivniElementi)
            {
                _sirovineDT.Columns.Add(ne.SkraceniNaziv, typeof(decimal));
                _smesaDT.Columns.Add(ne.SkraceniNaziv, typeof(decimal));
                _totalDT.Columns.Add(ne.SkraceniNaziv, typeof(decimal));
                _amkTotalDT.Columns.Add(ne.SkraceniNaziv, typeof(decimal));

                _sirovinaNeJm.Columns.Add(ne.SkraceniNaziv, typeof(int));
                //_smesaNeJm.Columns.Add(ne.SkraceniNaziv, typeof(int));
            }

            setDataGridView();
        }

        private void setDataGridView()
        {
            dgvCalculator.DataSource = _smesaDT;
            dgvTotal.DataSource = _totalDT;
            dgvAmk.DataSource = _amkTotalDT;

            dgvCalculator.RowHeadersVisible = false;
            dgvCalculator.AllowUserToAddRows = false;
            dgvCalculator.AllowUserToResizeRows = false;
            //dgvCalculator.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCalculator.AllowUserToDeleteRows = false;
            dgvCalculator.AllowUserToOrderColumns = false;
            dgvCalculator.Columns["SirovinaID"].Visible = false;
            dgvCalculator.Columns["SirovinaID"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvCalculator.Columns["Naziv"].ReadOnly = true;
            dgvCalculator.Columns["Naziv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvCalculator.Columns["TipSirovineID"].Visible = false;
            dgvCalculator.Columns["TipSirovineCode"].Visible = false;
            dgvCalculator.Columns["JmID"].Visible = false;
            dgvCalculator.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCalculator.Columns["Kolicina"].DefaultCellStyle.Format = "N4";
            dgvCalculator.Columns["Color"].Visible = false;

            for(int i = 7; i < dgvCalculator.Columns.Count; i++)
            {
                dgvCalculator.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgvCalculator.Columns[i].ReadOnly = true;
                dgvCalculator.Columns[i].DefaultCellStyle.Format = "N4";
                dgvCalculator.Columns[i].Width = 70;
                dgvCalculator.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dgvCalculator.CellEndEdit += dgvCalculator_CellEndEdit;
            dgvCalculator.Scroll += dgvCalculator_Scroll;
            dgvCalculator.RowsAdded += dgvCalculator_RowsAdded;
            dgvCalculator.CellFormatting += dgvCalculator_CellFormatting;
            dgvCalculator.CellClick += dgvCalculator_CellClick;
            dgvCalculator.CellValidating += dgvCalculator_CellValidating;
            dgvCalculator.RowPrePaint += dgvCalculator_RowPrePaint;
            dgvCalculator.ColumnWidthChanged += dgvCalculator_ColumnWidthChanged;
            sortDataGrid();

            dgvTotal.RowHeadersVisible = false;
            dgvTotal.AllowUserToAddRows = false;
            dgvTotal.ColumnHeadersVisible = false;
            dgvTotal.Columns["SirovinaID"].Visible = false;
            dgvTotal.DefaultCellStyle.Font = new Font(dgvTotal.Font, FontStyle.Bold);
            dgvTotal.ScrollBars = ScrollBars.None;
            dgvTotal.Columns["TipSirovineID"].Visible = false;
            dgvTotal.Columns["TipSirovineCode"].Visible = false;
            dgvTotal.Columns["JmID"].Visible = false;
            dgvTotal.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTotal.Columns["Color"].Visible = false;

            for(int i = 1; i < dgvTotal.Columns.Count; i++)
            {
                dgvTotal.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgvTotal.Columns[i].ReadOnly = true;
                dgvTotal.Columns[i].DefaultCellStyle.Format = "N4";
                if(i >= 6)
                { 
                    if(i >= 7)
                        dgvTotal.Columns[i].Width = 70;
                    dgvTotal.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgvTotal.Columns[i].ToolTipText = "Ukupno " + _sirovineDT.Columns[i].ColumnName.ToLower();
                }
            }

            dgvTotal.ColumnWidthChanged += dgvCalculator_ColumnWidthChanged;
            dgvTotal.ContextMenuStrip = new ContextMenuStrip();
            dgvTotal.ContextMenuStrip.Items.Add("Izračunaj količinu do zahtevane");
            dgvTotal.ContextMenuStrip.Items[0].Click += mnuIzracunajKolicinuDoZahtevane_Click;
            dgvTotal.CellContextMenuStripNeeded += dgvTotal_CellContextMenuStripNeeded;

            dgvAmk.RowHeadersVisible = false;
            dgvAmk.AllowUserToAddRows = false;
            dgvAmk.ColumnHeadersVisible = false;
            dgvAmk.Columns["SirovinaID"].Visible = false;
            dgvAmk.Columns["TipSirovineID"].Visible = false;
            dgvAmk.Columns["TipSirovineCode"].Visible = false;
            dgvAmk.Columns["JmID"].Visible = false;
            dgvAmk.Columns["Color"].Visible = false;
            dgvAmk.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAmk.DefaultCellStyle.Font = new Font(dgvAmk.Font, FontStyle.Bold);
            dgvAmk.ScrollBars = ScrollBars.None;

            for(int i = 1; i < dgvAmk.Columns.Count; i++)
            {
                dgvAmk.Columns[i].DefaultCellStyle.BackColor = Color.AntiqueWhite;
                dgvAmk.Columns[i].ReadOnly = true;
                dgvAmk.Columns[i].DefaultCellStyle.Format = "N4";

                if(i >= 6)
                {
                    if(i >= 7)
                        dgvAmk.Columns[i].Width = 70;
                    dgvAmk.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            dgvAmk.ColumnWidthChanged += dgvCalculator_ColumnWidthChanged;

            
        }

        private void dgvCalculator_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            resizeColumn(e.Column);
        }

        private void resizeColumn(DataGridViewColumn column)
        {
            dgvTotal.Columns[column.Name].Width = column.Width;
            dgvZahtevano.Columns[column.Name].Width = column.Width;
            dgvAmk.Columns[column.Name].Width = column.Width;
            dgvCalculator.Columns[column.Name].Width = column.Width;
        }

        private void dgvCalculator_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgvCalculator.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#" + dgvCalculator.Rows[e.RowIndex].Cells["Color"].Value.ToString());
        }

        private void dgvCalculator_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(e.ColumnIndex == dgvCalculator.Columns["Kolicina"].Index)
            {
                decimal value;
                if (dgvCalculator.Rows[e.RowIndex].IsNewRow)
                    return;
                if(!decimal.TryParse(e.FormattedValue.ToString(), out value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Unesite broj u pravilnom formatu", "Unos količine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvCalculator_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTotal.ClearSelection();
            dgvTotal.Rows[0].Cells[e.ColumnIndex].Selected = true;
            dgvAmk.ClearSelection();
            dgvAmk.Rows[0].Cells[e.ColumnIndex].Selected = true;
            dgvZahtevano.ClearSelection();
            dgvZahtevano.Rows[0].Cells[e.ColumnIndex].Selected = true;
        }

        private void dgvCalculator_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 7)
                if (decimal.Parse(dgvCalculator.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > 0)
                    e.CellStyle.Font = new Font(dgvCalculator.Font, FontStyle.Bold);
        }

        private void dgvCalculator_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //if(dgvCalculator.Rows[e.RowIndex].Cells["Color"].Value != null)
                //dgvCalculator.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#" + dgvCalculator.Rows[e.RowIndex].Cells["Color"].Value.ToString());
            //sortDataGrid();
            //setActiveCell(e.RowIndex);
        }

        private void dgvCalculator_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            { 
                dgvTotal.HorizontalScrollingOffset = e.NewValue;
                dgvZahtevano.HorizontalScrollingOffset = e.NewValue;
                dgvAmk.HorizontalScrollingOffset = e.NewValue;
            }
        }

        private void setSirovineDT(IEnumerable<NutritivniElementVrednost> nutritivniElementi)
        {
            //_sirovineDT = new DataTable();

            //_sirovineDT.Columns.Add("SirovinaID", typeof(int));
            //_sirovineDT.Columns.Add(new DataColumn()
            //{
                //Caption = "SirovinaID",
                //ColumnName = "SirovinaID",
                //DataType = typeof(string),
                //DefaultValue = -1,
                //ReadOnly = true
            //});
            //_sirovineDT.Columns.Add("SirovinaNaziv", typeof(string));
            //_sirovineDT.Columns.Add("Kolicina", typeof(decimal));
            //foreach (NutritivniElementVrednost ne in nutritivniElementi)
                //_sirovineDT.Columns.Add(ne.Naziv, typeof(decimal));

            //dgvCalculator.DataSource = _sirovineDT;

            //dgvCalculator.RowHeadersVisible = false;
            //dgvCalculator.AllowUserToAddRows = false;
            //dgvCalculator.Columns[0].Visible = false;
            //dgvCalculator.Columns[0].DefaultCellStyle.BackColor = Color.Gray;
            //dgvCalculator.Columns[1].ReadOnly = true;
            //dgvCalculator.Columns[1].DefaultCellStyle.BackColor = Color.Gray;

            //for(int i = 3; i < dgvCalculator.Columns.Count; i++)
            //{
                //dgvCalculator.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                //dgvCalculator.Columns[i].ReadOnly = true;
                //dgvCalculator.Columns[i].DefaultCellStyle.Format = "N2";
            //}

            //dgvCalculator.CellEndEdit += dgvCalculator_CellEndEdit;
        }

        private void dgvCalculator_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //calculateRow(e.RowIndex, decimal.Parse(dgvCalculator.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
            dgvCalculator.EndEdit();
            calculateRows();

            _sirovinaCenaDT.Rows[getRowIndexBySirovinaID(int.Parse(dgvCalculator.Rows[e.RowIndex].Cells["SirovinaID"].Value.ToString()), _sirovinaCenaDT)]["Kolicina"] = dgvCalculator.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            _dataUpdated = true;
        }

        private void calculateRow(int rowIndex, decimal kolicina)
        {
            decimal total = calculateTotalWeight();
            for(int i = 6; i < dgvCalculator.Columns.Count; i++)
            {
                _smesaDT.Rows[rowIndex][i] = kolicina / total * decimal.Parse(_sirovineDT.Rows[rowIndex][i].ToString());
            }

            calculateTotal();
        }

        private void calculateRows()
        {
            calculateAmk();
            decimal total = calculateTotalWeight();
            if(total > 0)
            { 
                for(int i = 0; i < dgvCalculator.Rows.Count; i++)
                    for(int j = 7; j < dgvCalculator.Columns.Count; j++)
                    {
                        int sirovinaID = int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString());
                        int jmInputID = int.Parse(_sirovinaNeJm.Rows[getRowIndexBySirovinaID(sirovinaID, _sirovinaNeJm)][j].ToString());
                        int jmOuputID = int.Parse(_smesaNeJm.Rows[getRowIndexBySirovinaID(-1, _smesaNeJm)][j].ToString());

                        int jmIndex = int.Parse(_smesaDT.Rows[i]["JmID"].ToString()) == 5 ? 1 : 1000;

                        //ako je ulaz procenat i izlaz procenat
                        //1-1, jmIndex = 1000
                        if (jmInputID == 1 && jmOuputID == 1 && jmIndex == 1000)
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) / 100 * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()) / jmIndex;
                        //1-1, 4-4, 2-2
                        else if ((jmInputID == 1 && jmOuputID == 1) || (jmInputID == 4 && jmOuputID == 4) || (jmInputID == 2 && jmOuputID == 2) || (jmInputID == 3 && jmOuputID == 31))
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) / 100 * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()); //decimal.Parse(_sirovineDT.Rows[i][j].ToString());
                        //1-3
                        else if (jmInputID == 1 && jmOuputID == 3)
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()) / 100 * 1000 / total;
                        //3-3
                        else if (jmInputID == 3 && jmOuputID == 3)
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()) / total / jmIndex;
                        else if (jmInputID == 1 && jmOuputID == 7 && jmIndex == 1000)
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()) / 100 * 1000 * 1000 / total;
                        else if (jmInputID == 8 && jmOuputID == 1)
                            _smesaDT.Rows[i][j] = decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString());
                    }

                
            }
            calculateAmk();
            calculateTotal();
        }

        private DataRow findRowBySirovinaID(int sirovinaID)
        {
            DataRow row = null;
            for (int i = 0; i < _sirovineDT.Rows.Count; i++)
                if (int.Parse(_sirovineDT.Rows[i][0].ToString()) == sirovinaID)
                    row = _sirovineDT.Rows[i];

            return row;
        }

        private void calculateTotal()
        {
            //calculateAmk();

            DataRow newRow = _totalDT.NewRow();
            _totalDT.Rows.Clear();

            newRow["SirovinaID"] = -1;
            newRow["Naziv"] = "Ukupno";
            newRow["Kolicina"] = 0;
            newRow["TipSirovineID"] = -1;
            newRow["TipSirovineCode"] = string.Empty;
            newRow["JmID"] = -1;
            newRow["Color"] = "ffffff";

            decimal columnTotal;
            for (int i = 6; i < _smesaDT.Columns.Count; i++)
            {
                columnTotal = 0;
                for(int j = 0; j < _smesaDT.Rows.Count; j++)
                {
                    if(_smesaDT.Rows[j]["TipSirovineCode"].Equals("SIL") || _smesaDT.Rows[j]["TipSirovineCode"].Equals("AMKTOTAL"))
                        columnTotal += decimal.Parse(_smesaDT.Rows[j][i].ToString());
                }
                if (_amkTotalDT.Columns[i].ColumnName.Equals("Kolicina"))
                    columnTotal += decimal.Parse(_amkTotalDT.Rows[0][i].ToString()) / 1000;
                else
                    columnTotal += decimal.Parse(_amkTotalDT.Rows[0][i].ToString());
                newRow[i] = columnTotal;
            }

            _totalDT.Rows.Add(newRow);
            validateTotal();
        }

        private decimal calculateTotalWeight()
        {
            decimal total = 0;

            foreach (DataRow row in _smesaDT.Rows)
                if(row["TipSirovineCode"].Equals("SIL") || row["TipSirovineCode"].Equals("AMKTOTAL"))
                    total += decimal.Parse(row["Kolicina"].ToString());

            if(_amkTotalDT.Rows.Count > 0)
                total += decimal.Parse(_amkTotalDT.Rows[0]["Kolicina"].ToString()) / 1000;

            return total;
        }

        private DataRow createRow(IEnumerable<NutritivniElementVrednost> vrednosti, DataTable dt, bool setZeros = false, string naziv = "", decimal kolicina = 0, int tipSirovineID = -1, int jmID = -1, string color = "ffffff", string tipSirovineCode = "", int sirovinaID = -1)
        {
            //DataRow newRow = isSmesa ? _smesaDT.NewRow() : _sirovineDT.NewRow();
            DataRow newRow = dt.NewRow();
            //newRow["SirovinaID"] = int.Parse(cmbSirovina.SelectedValue.ToString());
            //newRow["SirovinaID"] = sirovinaID > 0 ? sirovinaID : int.Parse(cmbSirovina.SelectedValue.ToString());
            newRow["SirovinaID"] = sirovinaID;
            newRow["Naziv"] = naziv == string.Empty ? ((Sirovina)cmbSirovina.SelectedItem).Naziv : naziv;
            newRow["Kolicina"] = kolicina;
            newRow["TipSirovineID"] = tipSirovineID;
            newRow["JmID"] = jmID;
            newRow["Color"] = color;
            newRow["TipSirovineCode"] = tipSirovineCode;

            int i = 7;

            foreach(NutritivniElementVrednost ne in vrednosti ?? new List<NutritivniElementVrednost>())
            {
                newRow[i++] = setZeros ? 0 : ne.Vrednost;
                //newRow[i++] = 0;
            }

            return newRow;
        }

        private void dgvCalculator_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbSmesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ucitaj zahtevane vrednosti
            if(cmbSmesa.SelectedIndex > -1)
            { 
                int smesaID = int.Parse(cmbSmesa.SelectedValue.ToString());
                IEnumerable<NutritivniElementVrednost> zahtevaneVrednosti = _nutritivniElementVrednostBL.GetBySmesaID(smesaID);

                if(_zahtevanoDT == null)
                    setZahtevanoDT(zahtevaneVrednosti);

                _zahtevanoDT.Rows.Clear();
                _zahtevanoDT.Rows.Add(createRow(zahtevaneVrednosti, _zahtevanoDT, false, "Zahtevano", 100, -1, -1, "ffffff", string.Empty, -1));

                _smesaNeJm.Rows.Clear();
                _smesaNeJm.Rows.Add(createRowJm(zahtevaneVrednosti, _smesaNeJm, -1));
            }
        }

        private void setZahtevanoDT(IEnumerable<NutritivniElementVrednost> zahtevaneVrednosti)
        {
            _zahtevanoDT = new DataTable();

            _zahtevanoDT.Columns.Add("SirovinaID", typeof(int));
            _zahtevanoDT.Columns.Add("TipSirovineID", typeof(int));
            _zahtevanoDT.Columns.Add("TipSirovineCode", typeof(string));
            _zahtevanoDT.Columns.Add("JmID", typeof(int));
            _zahtevanoDT.Columns.Add("Naziv", typeof(string));
            _zahtevanoDT.Columns.Add("Color", typeof(string));
            _zahtevanoDT.Columns.Add("Kolicina", typeof(decimal));

            _smesaNeJm = new DataTable();
            _smesaNeJm.Columns.Add("SirovinaID", typeof(int));
            _smesaNeJm.Columns.Add("TipSirovineID", typeof(int));
            _smesaNeJm.Columns.Add("TipSirovineCode", typeof(string));
            _smesaNeJm.Columns.Add("JmID", typeof(int));
            _smesaNeJm.Columns.Add("Naziv", typeof(string));
            _smesaNeJm.Columns.Add("Color", typeof(string));
            _smesaNeJm.Columns.Add("Kolicina", typeof(decimal));
            

            foreach (var ne in zahtevaneVrednosti)
            { 
                _zahtevanoDT.Columns.Add(ne.SkraceniNaziv, typeof(decimal));
                _smesaNeJm.Columns.Add(ne.SkraceniNaziv, typeof(decimal));
            }

            setZahtevanoDataGridView();
        }

        private void setZahtevanoDataGridView()
        {
            dgvZahtevano.DataSource = _zahtevanoDT;

            dgvZahtevano.RowHeadersVisible = false;
            dgvZahtevano.AllowUserToAddRows = false;
            dgvZahtevano.Columns["SirovinaID"].Visible = false;
            dgvZahtevano.ColumnHeadersVisible = false;
            dgvZahtevano.DefaultCellStyle.Font = new Font(dgvZahtevano.Font, FontStyle.Bold);
            dgvZahtevano.ScrollBars = ScrollBars.None;
            dgvZahtevano.Columns["TipSirovineID"].Visible = false;
            dgvZahtevano.Columns["TipSirovineCode"].Visible = false;
            dgvZahtevano.Columns["JmID"].Visible = false;
            dgvZahtevano.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvZahtevano.Columns["Color"].Visible = false;

            for (int i = 0; i < dgvZahtevano.Columns.Count; i++)
            {
                dgvZahtevano.Columns[i].ReadOnly = true;
                dgvZahtevano.Columns[i].DefaultCellStyle.BackColor = Color.LightGreen;
                dgvZahtevano.Columns[i].DefaultCellStyle.Format = "N4";
                if(i >=6)
                { 
                    if(i >= 7)
                        dgvZahtevano.Columns[i].Width = 70;
                    dgvZahtevano.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            dgvZahtevano.ColumnWidthChanged += dgvCalculator_ColumnWidthChanged;
        }

        private void validateTotal()
        {
            for (int i = 6; i < _totalDT.Columns.Count; i++)
            {
                if (Math.Abs(decimal.Parse(_totalDT.Rows[0][i].ToString()) - decimal.Parse(_zahtevanoDT.Rows[0][i].ToString())) > decimal.Parse(_zahtevanoDT.Rows[0][i].ToString()) * (decimal)0.05)
                {
                    dgvTotal.Rows[0].Cells[i].Style.BackColor = Color.LightPink;
                }
            }
        }

        private void setDataGridViewSize()
        {
            int availableHeight = this.Height - pnlTop.Height - pnlBottom.Height - toolStrip1.Height;
            dgvCalculator.Height = availableHeight / 2;
            //dgvAmk.Height = availableHeight - dgvCalculator.Height - 50;
        }

        private void sortDataGrid()
        {
            if(dgvCalculator.DataSource != null && dgvCalculator.Columns["TipSirovineID"] != null)
                dgvCalculator.Sort(dgvCalculator.Columns["TipSirovineID"], ListSortDirection.Ascending);
        }

        private void setActiveCell(int rowIndex)
        {
            dgvCalculator.Select();
            dgvCalculator.CurrentCell = dgvCalculator.Rows[rowIndex].Cells["Kolicina"];
            dgvCalculator.Rows[rowIndex].Cells["Kolicina"].Selected = true;

            dgvCalculator.BeginEdit(true);
            //DataGridViewTextBoxEditingControl editControl = (DataGridViewTextBoxEditingControl)dgvCalculator.EditingControl;
            //editControl.SelectionStart = 0;
            //editControl.SelectionLength = editControl.Text.Length;
        }

        private int getRowIndexBySirovinaID(int sirovinaID)
        {
            int rowIndex = -1;
            for (int i = 0; i < dgvCalculator.Rows.Count; i++)
                if (int.Parse(dgvCalculator.Rows[i].Cells["SirovinaID"].Value.ToString()) == sirovinaID)
                { 
                    rowIndex = i;
                    break;
                }
            return rowIndex;
        }

        private void btnOpenSirovinaForm_Click(object sender, EventArgs e)
        {
            if (cmbSirovina.SelectedIndex > -1)
            {
                SirovinaForm sirovinaForm = new SirovinaForm(int.Parse(cmbSirovina.SelectedValue.ToString()), _sirovinaBL, _sirovinaNutritivniElementVrednostBL, _tipSirovineBL, _jmBL);
                sirovinaForm.SirovinaSaved += sirovinaForm_SirovinaSaved;
                sirovinaForm.ShowDialog();
            }
            else
                MessageBox.Show("Odaberie sirovinu", "Izmena sirovine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void sirovinaForm_SirovinaSaved()
        {
            cmbSirovina.DataSource = _sirovinaBL.GetAll(chkShowAllSirovina.Checked);
        }

        private void calculateAmk()
        {
            if(_amkTotalDT != null)
                _amkTotalDT.Rows.Clear();

            DataRow newRow = _amkTotalDT.NewRow();

            newRow["SirovinaID"] = -1;
            newRow["Naziv"] = "AMK TOTAL";
            newRow["TipSirovineID"] = -1;
            newRow["TipSirovineCode"] = string.Empty;
            newRow["JmID"] = -1;
            newRow["Color"] = string.Empty;

            decimal columnTotal = 0;
            for(int i = 6; i < _smesaDT.Columns.Count; i++)
            {
                columnTotal = 0;
                for (int j = 0; j < _smesaDT.Rows.Count; j++)
                    if (_smesaDT.Rows[j]["TipSirovineCode"].Equals("AMK"))
                        columnTotal += decimal.Parse(_smesaDT.Rows[j][i].ToString());
                newRow[i] = columnTotal;
            }

            _amkTotalDT.Rows.Add(newRow);

        }

        private void btnClearDataTable_Click(object sender, EventArgs e)
        {
            if(_smesaDT != null)
            { 
                _smesaDT.Rows.Clear();
                _sirovineDT.Rows.Clear();
                _totalDT.Rows.Clear();
                _zahtevanoDT.Rows.Clear();
                _amkTotalDT.Rows.Clear();
                _smesaNeJm.Rows.Clear();
                _sirovinaNeJm.Rows.Clear();
                _sirovinaCenaDT.Rows.Clear();
            }

            if (_zahtevanoDT != null)
                _zahtevanoDT.Rows.Clear();

            if (_smesaNeJm != null)
                _smesaNeJm.Rows.Clear();

            txtNaziv.Text = string.Empty;
            cmbSmesa.SelectedIndex = -1;
            cmbSirovina.SelectedIndex = -1;
        }

        private DataRow createRowJm(IEnumerable<NutritivniElementVrednost> vrednosti, DataTable dt, int sirovinaID)
        {
            int i = 7;
            DataRow newRow = dt.NewRow();
            newRow["SirovinaID"] = sirovinaID;
            foreach (NutritivniElementVrednost vrednost in vrednosti ?? new List<NutritivniElementVrednost>())
                newRow[i++] = vrednost.JmID;

            return newRow;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int sirovinaID = -1;
            //if(dgvCalculator.SelectedRows.Count <= 0 || dgvCalculator.SelectedRows.Count > 1)
            if(dgvCalculator.SelectedCells.Count == 0 || dgvCalculator.SelectedCells.Count > 1)
            {
                MessageBox.Show("Odaberite tačno jedan red za brisanje", "Brisanje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //if (dgvCalculator.SelectedRows.Count == 1 && MessageBox.Show("Da li ste sigurni da želite da obrišete red: " + dgvCalculator.SelectedRows[0].Cells["Naziv"].Value.ToString(), "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            if (dgvCalculator.SelectedCells.Count == 1 && MessageBox.Show("Da li ste sigurni da želite da obrišete red: " + dgvCalculator.Rows[dgvCalculator.SelectedCells[0].RowIndex].Cells["Naziv"].Value.ToString(), "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //sirovinaID = int.Parse(dgvCalculator.SelectedRows[0].Cells["SirovinaID"].Value.ToString());
                sirovinaID = int.Parse(dgvCalculator.Rows[dgvCalculator.SelectedCells[0].RowIndex].Cells["SirovinaID"].Value.ToString());

            int sirovinaDTIndex = getRowIndexBySirovinaID(sirovinaID, _sirovineDT);
            int sirovinaJmDtIndex = getRowIndexBySirovinaID(sirovinaID, _sirovinaNeJm);
            int smesaDTIndex = getRowIndexBySirovinaID(sirovinaID, _smesaDT);
            int sirovinaCenaDTIndex = getRowIndexBySirovinaID(sirovinaID, _sirovinaCenaDT);

            if(sirovinaDTIndex > -1 && sirovinaJmDtIndex > -1 && smesaDTIndex > -1 && sirovinaCenaDTIndex > -1)
            {
                _sirovineDT.Rows[sirovinaDTIndex].Delete();
                _sirovinaNeJm.Rows[sirovinaJmDtIndex].Delete();
                _smesaDT.Rows[smesaDTIndex].Delete();
                _sirovinaCenaDT.Rows[sirovinaCenaDTIndex].Delete();
            }

            calculateRows();
            _dataUpdated = true;
        }

        private int getRowIndexBySirovinaID(int sirovinaID, DataTable dt)
        {
            int index = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
                if (int.Parse(dt.Rows[i]["SirovinaID"].ToString()) == sirovinaID)
                { 
                    index = i;
                    break;
                }

            return index;
        }

        private void btnSaveCalculation_Click(object sender, EventArgs e)
        {
            save();
        }

        private void btnSaveCalculationAs_Click(object sender, EventArgs e)
        {
            _kalkulacijaID = -1;
            save();
        }

        private void save()
        {
            if (cmbSmesa.SelectedIndex > -1 && txtNaziv.Text.Trim() != string.Empty && _smesaDT != null)
            {
                try
                {
                    _kalkulacija = new Kalkulacija();
                    _kalkulacija.ID = _kalkulacijaID;
                    _kalkulacija.Naziv = txtNaziv.Text;
                    _kalkulacija.Datum = DateTime.Now;
                    _kalkulacija.SmesaID = int.Parse(cmbSmesa.SelectedValue.ToString());

                    _kalkulacija.Vrednosti = new List<KalkulacijaSirovinaVrednost>();
                    foreach (DataRow row in _smesaDT.Rows)
                    {
                        _kalkulacija.Vrednosti.Add(new KalkulacijaSirovinaVrednost()
                        {
                            KalkulacijaID = _kalkulacijaID,
                            Kolicina = decimal.Parse(row["Kolicina"].ToString()),
                            Cena = decimal.Parse(_sirovinaCenaDT.Rows[getRowIndexBySirovinaID(int.Parse(row["SirovinaID"].ToString()), _sirovinaCenaDT)]["Cena"].ToString()),
                            SirovinaID = int.Parse(row["SirovinaID"].ToString())
                        });
                    }

                    _kalkulacijaID = _kalkulacijaBL.Save(_kalkulacija);

                    MessageBox.Show("Kalkulacija uspešno sačuvana", "Sačuvaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom upisa kalkulacije" + ex.Message + ex.StackTrace, "Sačuvaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Odaberite smešu i unesite naziv kalkulacije. Kalkulacija mora da ima bar jednu sirovinu unetu.", "Unos kalkulacije", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            { 
                Kalkulacije kalkulacije = new Kalkulacije(_kalkulacijaViewBL, _kalkulacijaBL);
                kalkulacije.KalkulacijaSelected += loadKalkulacija;
                kalkulacije.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja kalkulacije", "Učitavanje kalkulacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadKalkulacija(int id)
        {
            try
            { 
                if(MessageBox.Show("Učitaj odabranu kalkulaciju? Podaci koji nisu sačuvani biće izgubljeni.", "Učitavanje kalkulacije", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    _kalkulacija = _kalkulacijaBL.GetByID(id);

                    if(_kalkulacija != null && _kalkulacija.ID > 0)
                    { 
                        btnClearDataTable_Click(this, null);
                        _kalkulacijaID = _kalkulacija.ID;

                        showKalkulacija();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja kalkulacije", "Učitavanje kalkulacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showKalkulacija()
        {
            try
            { 
                txtNaziv.Text = _kalkulacija.Naziv;
                cmbSmesa.SelectedValue = _kalkulacija.SmesaID;

                Sirovina sirovina = null;
                TipSirovine tipSirovine = null;
                foreach (var vrednost in _kalkulacija.Vrednosti ?? new List<KalkulacijaSirovinaVrednost>())
                {
                    //sirovina = _sirovinaBL.GetByID(vrednost.SirovinaID);
                    sirovina = ((List<Sirovina>)cmbSirovina.DataSource).Find(item => item.ID == vrednost.SirovinaID);
                    //tipSirovine = _tipSirovineBL.GetByID(sirovina.TipSirovineID);
                    tipSirovine = _tipoviSirovine.First(item => item.ID == sirovina.TipSirovineID);

                    if(sirovina != null && tipSirovine != null)
                    { 

                        IEnumerable<NutritivniElementVrednost> vrednosti = _nutritivniElementVrednostBL.GetBySirovinaID(sirovina.ID);

                        if (_sirovineDT == null)
                            setDataTables(vrednosti);

                        _sirovineDT.Rows.Add(createRow(vrednosti, _sirovineDT, false, sirovina.Naziv, 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));
                        _smesaDT.Rows.Add(createRow(vrednosti, _smesaDT, true, sirovina.Naziv + " [" + sirovina.JmNaziv + "]", 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));

                        _sirovinaNeJm.Rows.Add(createRowJm(vrednosti, _sirovinaNeJm, sirovina.ID));

                        //sirovina.Cena = vrednost.Cena;
                        _sirovinaCenaDT.Rows.Add(createRowSirovinaCena(sirovina, tipSirovine));

                        _smesaDT.Rows[_smesaDT.Rows.Count - 1]["Kolicina"] = vrednost.Kolicina;

                        _sirovinaCenaDT.Rows[_sirovinaCenaDT.Rows.Count - 1]["Kolicina"] = vrednost.Kolicina;

                    }
                }

                if(_kalkulacija.Vrednosti != null && _kalkulacija.Vrednosti.Count > 0)
                    calculateRows();

                _dataUpdated = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja kalkulacije", "Prikaz kalkulacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAmk_Click(object sender, EventArgs e)
        {
            _showAmk = !_showAmk;
            setAmkRows(_showAmk);
            btnAmk.BackColor = _showAmk ? Color.LightGray : Color.DarkGray;
            btnAmk.Text = _showAmk ? "Sakrij AMK" : "Prikaži AMK";
        }

        private void setAmkRows(bool visible)
        {
            foreach (DataGridViewRow row in dgvCalculator.Rows)
                if (row.Cells["TipSirovineCode"].Value.ToString().Equals("AMK"))
                    row.Visible = visible;
        }

        private void btnShowCosts_Click(object sender, EventArgs e)
        {
            if (_sirovinaCenaDT != null)
            {
                //if (_sirovinaCenaForm == null)
                _sirovinaCenaForm = new SirovinaCena(_kategorijaZivotinjaSmesaPotrosnjaBL, _exportBL, cmbSmesa.Text, txtNaziv.Text);

                //if(_dataUpdated)
                //{ 
                int jmIndex = 1;
                //foreach (DataRow row in _sirovinaCenaDT.Rows)
                //{
                //jmIndex = row["TipSirovineCode"].ToString().Equals("AMK") ? 1000 : 1;
                //row["Ukupno"] = decimal.Parse(row["Kolicina"].ToString()) * decimal.Parse(row["Cena"].ToString()) / jmIndex;
                //}
                calculateSirovinaCenaDT();
                _sirovinaCenaForm.AddDataTable(_sirovinaCenaDT.Copy(), int.Parse(cmbSmesa.SelectedValue.ToString()));
                //}

                _sirovinaCenaForm.Show();

                _dataUpdated = false;
            }
            else
                MessageBox.Show("Nema podataka za prikaz", "Prikaz troškova", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DataRow createRowSirovinaCena(Sirovina sirovina, TipSirovine tipSirovine)
        {
            DataRow newRow = _sirovinaCenaDT.NewRow();
            newRow["SirovinaID"] = sirovina.ID;
            newRow["Naziv"] = sirovina.Naziv;
            newRow["Jm"] = sirovina.JmNaziv;
            newRow["Kolicina"] = 0;
            newRow["Cena"] = sirovina.Cena;
            newRow["Ukupno"] = 0;
            newRow["TipSirovineCode"] = tipSirovine.Code;
            newRow["TipSirovineID"] = tipSirovine.ID;
            newRow["Color"] = tipSirovine.Color;
            newRow["KolicinskiOdnos"] = sirovina.KolicinskiOdnos;

            return newRow;
        }

        private void btnSortByType_Click(object sender, EventArgs e)
        {
            sortDataGrid();
        }

        private void btnOpenSmesaForm_Click(object sender, EventArgs e)
        {
            if (cmbSmesa.SelectedIndex > -1)
            {
                SmesaForm smesaForm = new SmesaForm(int.Parse(cmbSmesa.SelectedValue.ToString()), _smesaBL, _smesaNutritivniElementVrednostBL);
                smesaForm.SmesaSaved += smesaForm_SmesaSaved;
                smesaForm.ShowDialog();
            }
            else
                MessageBox.Show("Odaberite smešu", "Izmena smeše", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected void smesaForm_SmesaSaved()
        {
            cmbSmesa.DataSource = _smesaBL.GetAll();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = txtNaziv.Text;
            saveFileDialog.Filter = "Excel | *.xlsx";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                calculateSirovinaCenaDT();
                //_exportBL.CreateExcelSpreadsheet(_smesaDT, saveFileDialog.FileName, new List<DataTable>() { _totalDT, _zahtevanoDT }, new List<string>() { "SirovinaID", "TipSirovineID", "TipSirovineCode", "Color", "JmID", "KolicinskiOdnos" }, _sirovinaCenaDT);
                if(_exportBL.CreateExcelSpreadsheet(new List<ExportModel>() {
                                            new ExportModel() { DataTable = _smesaDT,
                                                                FooterDataTables = new List<DataTable>() { _totalDT, _zahtevanoDT },
                                                                Name = "Kalkulacija" },
                                            new ExportModel() { DataTable = _sirovinaCenaDT,
                                                                FooterDataTables = new List<DataTable>(), Name = "Trošak" } },
                                                                saveFileDialog.FileName,
                                                                new List<string> { "SirovinaID", "TipSirovineID", "TipSirovineCode", "Color", "JmID", "KolicinskiOdnos" }) == true)
                    MessageBox.Show("Izvoz kalkulacije uspešan", "Izvoz kalkulacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void calculateSirovinaCenaDT()
        {
            int jmIndex = 1;
            foreach (DataRow row in _sirovinaCenaDT.Rows)
            {
                jmIndex = row["TipSirovineCode"].ToString().Equals("AMK") ? 1000 : 1;
                decimal kolicinskiOdnos = decimal.Parse(row["KolicinskiOdnos"].ToString());
                if(kolicinskiOdnos != 1)
                {
                    row["KolicinaKorigovano"] = decimal.Parse(row["Kolicina"].ToString()) / kolicinskiOdnos;
                    row["Ukupno"] = decimal.Parse(row["KolicinaKorigovano"].ToString()) * decimal.Parse(row["Cena"].ToString()) / jmIndex;
                }
                else
                    row["Ukupno"] = decimal.Parse(row["Kolicina"].ToString()) * decimal.Parse(row["Cena"].ToString()) / jmIndex;
            }
        }

        protected void dgvTotal_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            dgvTotal.ClearSelection();
            dgvTotal.Rows[0].Cells[e.ColumnIndex].Selected = true;
        }

        protected void mnuIzracunajKolicinuDoZahtevane_Click(object sender, EventArgs e)
        {
            decimal total = decimal.Parse(_totalDT.Rows[0][dgvTotal.SelectedCells[0].ColumnIndex].ToString());
            decimal zahtevano = decimal.Parse(_zahtevanoDT.Rows[0][dgvTotal.SelectedCells[0].ColumnIndex].ToString());
            int smesaJmID = int.Parse(_smesaNeJm.Rows[0][dgvTotal.SelectedCells[0].ColumnIndex].ToString());
            string nutritivniElementNaziv = dgvTotal.Columns[dgvTotal.SelectedCells[0].ColumnIndex].Name;

            NevCalculator nevCalculator = new NevCalculator(_sirovinaBL, _nutritivniElementVrednostBL, total, zahtevano, smesaJmID, nutritivniElementNaziv);
            nevCalculator.KolicinaIzracunata += nevCalculator_KolicinaIzracunata;
            nevCalculator.ShowDialog();
        }

        protected void nevCalculator_KolicinaIzracunata(decimal kolicina, Sirovina sirovina)
        {
            if(getRowIndexBySirovinaID(sirovina.ID) == -1)
            {
                TipSirovine tipSirovine = _tipoviSirovine.First(item => item.ID == sirovina.TipSirovineID);

                IEnumerable<NutritivniElementVrednost> vrednosti = _nutritivniElementVrednostBL.GetBySirovinaID(sirovina.ID);

                _sirovineDT.Rows.Add(createRow(vrednosti, _sirovineDT, false, sirovina.Naziv, 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));
                _smesaDT.Rows.Add(createRow(vrednosti, _smesaDT, true, sirovina.Naziv + " [" + sirovina.JmNaziv + "]", kolicina, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code, sirovina.ID));
                _sirovinaNeJm.Rows.Add(createRowJm(vrednosti, _sirovinaNeJm, sirovina.ID));
                _sirovinaCenaDT.Rows.Add(createRowSirovinaCena(sirovina, tipSirovine));
                _sirovinaCenaDT.Rows[_sirovinaCenaDT.Rows.Count - 1]["Kolicina"] = kolicina;
            }
            else
            {
                if(MessageBox.Show(sirovina.Naziv + " već postoji u kalkulaciji. Izmeni vrednost na osnovu izračunate?", "Izračunavanje količine amk sirovine", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int smesaIndex = getRowIndexBySirovinaID(sirovina.ID, _smesaDT);
                    decimal currentValue = decimal.Parse(_smesaDT.Rows[smesaIndex]["Kolicina"].ToString());
                    _smesaDT.Rows[smesaIndex]["Kolicina"] = currentValue + kolicina;
                }
            }

            calculateRows();
        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = txtNaziv.Text;
            saveFileDialog.Filter = "CSV | *.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                calculateSirovinaCenaDT();
                _exportBL.ExportToCsv(new ExportModel() { DataTable = _smesaDT, FooterDataTables = new List<DataTable>() { _totalDT, _zahtevanoDT }, Name = "Kalkulacija" }, saveFileDialog.FileName, new List<string>() { "SirovinaID", "TipSirovineID", "TipSirovineCode", "Color", "JmID", "KolicinskiOdnos" });
            }
        }

        private void setDoubleBuffered()
        {
            PropertyInfo propertyInfo = dgvCalculator.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(dgvCalculator, true, null);

            PropertyInfo propertyInfoZahtevano = dgvZahtevano.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfoZahtevano.SetValue(dgvZahtevano, true, null);

            PropertyInfo propertyInfoTotal = dgvTotal.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfoTotal.SetValue(dgvTotal, true, null);

            PropertyInfo propertyInfoAmk = dgvAmk.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfoAmk.SetValue(dgvAmk, true, null);
        }

        private void chkShowAllSirovina_CheckedChanged(object sender, EventArgs e)
        {
            sirovinaForm_SirovinaSaved();
        }
    }
}
