﻿using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
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
    public partial class Calculator : Form
    {
        private ISirovinaBL _sirovinaBL;
        private INutritivniElementVrednostBL _nutritivniElementVrednostBL;
        private ISmesaBL _smesaBL;
        private ITipSirovineBL _tipSirovineBL;
        private IJmBL _jmBL;
        private ISirovinaNutritivniElementVrednostBL _sirovinaNutritivniElementVrednostBL;
        //private List<SirovinaNeVrednost> _sirovine;
        private DataTable _sirovineDT;
        private DataTable _smesaDT;
        private DataTable _totalDT;
        private DataTable _zahtevanoDT;
        private DataTable _amkTotalDT;
        private DataTable _sirovinaNeJm;
        private DataTable _smesaNeJm;

        public Calculator(ISirovinaBL sirovinaBL, INutritivniElementVrednostBL nutritivniElementVrednostBL, ISmesaBL smesaBL, ITipSirovineBL tipSirovineBL, IJmBL jmBL, ISirovinaNutritivniElementVrednostBL sirovinaNutritivniElementVrednostBL)
        {
            InitializeComponent();

            _sirovinaBL = sirovinaBL;
            _nutritivniElementVrednostBL = nutritivniElementVrednostBL;
            _smesaBL = smesaBL;
            _tipSirovineBL = tipSirovineBL;
            _jmBL = jmBL;
            _sirovinaNutritivniElementVrednostBL = sirovinaNutritivniElementVrednostBL;

            //_sirovine = new List<SirovinaNeVrednost>();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            setDataGridViewSize();

            cmbSirovina.DataSource = _sirovinaBL.GetAll();
            cmbSirovina.DisplayMember = "Naziv";
            cmbSirovina.ValueMember = "ID";

            //cmbSmesa.SelectedIndexChanged -= cmbSmesa_SelectedIndexChanged;
            cmbSmesa.DisplayMember = "Naziv";
            cmbSmesa.ValueMember = "ID";
            cmbSmesa.DataSource = _smesaBL.GetAll();

            //cmbSmesa.SelectedIndexChanged += cmbSmesa_SelectedIndexChanged;

            this.SizeChanged += Calculator_SizeChanged;
        }

        private void Calculator_SizeChanged(object sender, EventArgs e)
        {
            setDataGridViewSize();
            //throw new NotImplementedException();
        }

        private void btnDodajSirovinu_Click(object sender, EventArgs e)
        {
            int sirovinaID = int.Parse(cmbSirovina.SelectedValue.ToString());
            if(getRowIndexBySirovinaID(sirovinaID) == -1)
            { 
                //Sirovina sirovina = _sirovinaBL.GetByID(sirovinaID);
                Sirovina sirovina = (Sirovina)cmbSirovina.SelectedItem;
                TipSirovine tipSirovine = _tipSirovineBL.GetByID(sirovina.TipSirovineID);

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

                _sirovineDT.Rows.Add(createRow(vrednosti, _sirovineDT, false, string.Empty, 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code));
                _smesaDT.Rows.Add(createRow(vrednosti, _smesaDT, true, sirovina.Naziv + " [" + sirovina.JmNaziv + "]", 0, sirovina.TipSirovineID, sirovina.JmID, tipSirovine.Color, tipSirovine.Code));

                _sirovinaNeJm.Rows.Add(createRowJm(vrednosti, _sirovinaNeJm, sirovinaID));

                //sortDataGrid();
                //calculateTotal();
            }
            setActiveCell(getRowIndexBySirovinaID(sirovinaID));
        }

        private void setDataTables(IEnumerable<NutritivniElementVrednost> nutritivniElementi)
        {
            _sirovineDT = new DataTable();
            _smesaDT = new DataTable();
            _totalDT = new DataTable();
            _amkTotalDT = new DataTable();
            _sirovinaNeJm = new DataTable();
            //_smesaNeJm = new DataTable();

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
            dgvCalculator.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCalculator.AllowUserToDeleteRows = false;
            dgvCalculator.Columns["SirovinaID"].Visible = false;
            dgvCalculator.Columns["SirovinaID"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvCalculator.Columns["Naziv"].ReadOnly = true;
            dgvCalculator.Columns["Naziv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvCalculator.Columns["TipSirovineID"].Visible = false;
            dgvCalculator.Columns["TipSirovineCode"].Visible = false;
            dgvCalculator.Columns["JmID"].Visible = false;
            dgvCalculator.Columns["Kolicina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

            
        }

        private void dgvCalculator_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 7)
                if (decimal.Parse(dgvCalculator.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > 0)
                    e.CellStyle.Font = new Font(dgvCalculator.Font, FontStyle.Bold);
        }

        private void dgvCalculator_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvCalculator.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#" + dgvCalculator.Rows[e.RowIndex].Cells["Color"].Value.ToString());
            sortDataGrid();
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
                        if ((jmInputID == 1 && jmOuputID == 1) || (jmInputID == 4 && jmOuputID == 4) || (jmInputID == 2 && jmOuputID == 2) || (jmInputID == 3 && jmOuputID == 3))
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) / total * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()); //decimal.Parse(_sirovineDT.Rows[i][j].ToString());
                        else if (jmInputID == 1 && jmOuputID == 3)
                            _smesaDT.Rows[i][j] = decimal.Parse(_smesaDT.Rows[i]["Kolicina"].ToString()) * decimal.Parse(findRowBySirovinaID(int.Parse(_smesaDT.Rows[i]["SirovinaID"].ToString()))[j].ToString()) / 100 * 1000 / total;
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
                    columnTotal += decimal.Parse(_amkTotalDT.Rows[0][i].ToString()) / 1000;
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

        private DataRow createRow(IEnumerable<NutritivniElementVrednost> vrednosti, DataTable dt, bool setZeros = false, string naziv = "", decimal kolicina = 0, int tipSirovineID = -1, int jmID = -1, string color = "ffffff", string tipSirovineCode = "")
        {
            //DataRow newRow = isSmesa ? _smesaDT.NewRow() : _sirovineDT.NewRow();
            DataRow newRow = dt.NewRow();
            newRow["SirovinaID"] = int.Parse(cmbSirovina.SelectedValue.ToString());
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
                _zahtevanoDT.Rows.Add(createRow(zahtevaneVrednosti, _zahtevanoDT, false, "Zahtevano", 100, -1, -1, "ffffff", string.Empty));

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
                _zahtevanoDT.Columns.Add(ne.Naziv, typeof(decimal));
                _smesaNeJm.Columns.Add(ne.Naziv, typeof(decimal));
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
        }

        private void validateTotal()
        {
            for (int i = 6; i < _totalDT.Columns.Count; i++)
            {
                if (Math.Abs(decimal.Parse(_totalDT.Rows[0][i].ToString()) - decimal.Parse(_zahtevanoDT.Rows[0][i].ToString())) > decimal.Parse(_zahtevanoDT.Rows[0][i].ToString()) * (decimal)0.01)
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
            if(cmbSirovina.SelectedIndex > -1)
            {
                SirovinaForm sirovinaForm = new SirovinaForm(int.Parse(cmbSirovina.SelectedValue.ToString()), _sirovinaBL, _sirovinaNutritivniElementVrednostBL, _tipSirovineBL, _jmBL);
                sirovinaForm.ShowDialog();
            }
        }

        private void calculateAmk()
        {
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
            _smesaDT.Rows.Clear();
            _sirovineDT.Rows.Clear();
            _totalDT.Rows.Clear();
            _zahtevanoDT.Rows.Clear();
            _amkTotalDT.Rows.Clear();
        }

        private DataRow createRowJm(IEnumerable<NutritivniElementVrednost> vrednosti, DataTable dt, int sirovinaID)
        {
            int i = 7;
            DataRow newRow = dt.NewRow();
            newRow["SirovinaID"] = sirovinaID;
            foreach (NutritivniElementVrednost vrednost in vrednosti)
                newRow[i++] = vrednost.JmID;

            return newRow;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int sirovinaID = -1;
            if(dgvCalculator.SelectedRows.Count <= 0 || dgvCalculator.SelectedRows.Count > 1)
            {
                MessageBox.Show("Odaberite tačno jedan red za brisanje", "Brisanje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCalculator.SelectedRows.Count == 1 && MessageBox.Show("Da li ste sigurni da želite da obrišete red: " + dgvCalculator.SelectedRows[0].Cells["Naziv"].Value.ToString(), "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                sirovinaID = int.Parse(dgvCalculator.SelectedRows[0].Cells["SirovinaID"].Value.ToString());

            int sirovinaDTIndex = getRowIndexBySirovinaID(sirovinaID, _sirovineDT);
            int sirovinaJmDtIndex = getRowIndexBySirovinaID(sirovinaID, _sirovinaNeJm);
            int smesaDTIndex = getRowIndexBySirovinaID(sirovinaID, _smesaDT);

            if(sirovinaDTIndex > -1 && sirovinaJmDtIndex > -1 && smesaDTIndex > -1)
            {
                _sirovineDT.Rows[sirovinaDTIndex].Delete();
                _sirovinaNeJm.Rows[sirovinaJmDtIndex].Delete();
                _smesaDT.Rows[smesaDTIndex].Delete();
            }

            calculateRows();
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
    }
}
