using BecNutritionCalculator.BusinessLogic.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic
{
    public class ExportModel
    {
        public DataTable DataTable { get; set; }
        public List<DataTable> FooterDataTables { get; set; }
        public string Name { get; set; }
    }

    public class ExportBL : IExportBL
    {
        public void CreateExcelSpreadsheet(DataTable dataTable, string destination, List<DataTable> footerDataTables = null, List<string> excludedColumns = null, DataTable sirovinaCena = null)
        {
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "sheet"
            };
            sheets.Append(sheet);

            //Sheet sheet2 = new Sheet()
            //{
                //Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                //SheetId = 2,
                //Name = "Troškovi"
            //};
            //sheets.Append(sheet2);

            WorkbookStylesPart stylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = new Stylesheet();
            Font fontDefault = new Font(new FontSize() { Val = 10 }, new FontName() { Val = "Calibri" }, new Color() { Rgb = new HexBinaryValue() { Value = "000000" } });
            Font fontBold = new Font(new Bold() { Val = true }, new FontSize() { Val = 10 }, new Color() { Rgb = new HexBinaryValue() { Value = "000000" } }, new FontName() { Val = "Calibri" });
            stylesPart.Stylesheet.Fonts = new Fonts() { Count = 2 };
            stylesPart.Stylesheet.Fonts.Append(fontDefault);
            stylesPart.Stylesheet.Fonts.Append(fontBold);
            stylesPart.Stylesheet.Save();

            var fills = new Fills() { Count = 1 };
            fills.Append(new Fill() { PatternFill = new PatternFill() { PatternType = new EnumValue<PatternValues>(PatternValues.None)} });
            stylesPart.Stylesheet.Append(fills);

            var borders = new Borders() { Count = 1 };
            borders.Append(new Border() { LeftBorder = new LeftBorder(), RightBorder = new RightBorder(), TopBorder = new TopBorder(), BottomBorder = new BottomBorder(), DiagonalBorder = new DiagonalBorder() });
            stylesPart.Stylesheet.Append(borders);

            UInt32Value fontBoldId = Convert.ToUInt32(stylesPart.Stylesheet.Fonts.ChildElements.Count - 1);
            UInt32Value fontDefaultId = Convert.ToUInt32(stylesPart.Stylesheet.Fonts.ChildElements.Count - 2);
            CellFormat cellFormat = new CellFormat() { FontId = fontBoldId, FillId = 0, BorderId = 0, ApplyFont = true };
            CellFormat cellFormatDefault = new CellFormat() { FontId = fontDefaultId };
            stylesPart.Stylesheet.CellFormats = new CellFormats() { Count = 2 };
            stylesPart.Stylesheet.CellFormats.Append(cellFormatDefault);
            stylesPart.Stylesheet.CellFormats.Append(cellFormat);
            stylesPart.Stylesheet.Save();
            
            int cellFormatId = stylesPart.Stylesheet.CellFormats.ChildElements.Count - 1;

            Row headerRow = new Row();
            List<string> columns = new List<string>();
            foreach(DataColumn column in dataTable.Columns)
            {
                if(!excludedColumns.Contains(column.ColumnName))
                { 
                    columns.Add(column.ColumnName);
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }
            }

            //sheet.AppendChild(headerRow);
            sheetData.AppendChild(headerRow);

            //int rowIndex = 1;
            int columnIndex = 1;
            //int columnPrefix = 0;
            foreach (DataRow row in dataTable.Rows)
            { 
                Row newRow = new Row();
                foreach(DataColumn column in dataTable.Columns)
                {
                    if(!excludedColumns.Contains(column.ColumnName))
                    { 
                        Cell cell = new Cell();
                        //{
                            //CellReference = (columnPrefix > 0 ? ((char)columnPrefix).ToString() : string.Empty) + ((char)columnIndex).ToString() + rowIndex.ToString()
                        //};
                        cell.DataType = getCellValueType(column);
                        //cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(getCellValue(row, column));
                        if(cell.DataType == CellValues.Number && decimal.Parse(getCellValue(row, column)) > 0 && columnIndex > 6)
                            cell.StyleIndex = 1U;
                        //cell.CellValue = new CellValue(column.ColumnName);
                        newRow.Append(cell);
                        columnIndex++;
                        //if(columnIndex == 91)
                        //{
                            //columnIndex = 65;
                            //columnPrefix = 65;
                        //}
                    }
                }
                //rowIndex++;
                columnIndex = 1;
                //columnPrefix = 0;
                sheetData.Append(newRow);
            }

            foreach(DataTable dt in footerDataTables??new List<DataTable>())
            {
                foreach(DataRow row in dt.Rows)
                {
                    Row newRow = new Row();
                    foreach(DataColumn column in dt.Columns)
                    {
                        if(!excludedColumns.Contains(column.ColumnName))
                        { 
                            Cell cell = new Cell();
                            cell.DataType = getCellValueType(column);
                            cell.CellValue = new CellValue(getCellValue(row, column));
                            cell.StyleIndex = 1U;
                            newRow.Append(cell);
                        }
                    }
                    sheetData.Append(newRow);
                }
            }

            //workbookPart.Workbook.Save();

            //spreadsheetDocument.Close();

            WorksheetPart troskoviWorksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
            troskoviWorksheetPart.Worksheet = new Worksheet(new SheetData());
            workbookPart.Workbook.Save();

            SheetData troskoviSheetData = troskoviWorksheetPart.Worksheet.GetFirstChild<SheetData>();

            Sheets troskoviSheets = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = spreadsheetDocument.WorkbookPart.GetIdOfPart(troskoviWorksheetPart);

            Sheet troskoviSheet = new Sheet() { Id = relationshipId, SheetId = 2, Name = "Troskovi" };
            troskoviSheets.Append(troskoviSheet);

            if(sirovinaCena != null)
            { 
            Row troskoviHeaderRow = new Row();
            foreach(DataColumn column in sirovinaCena.Columns)
            {
                if(!excludedColumns.Contains(column.ColumnName))
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    troskoviHeaderRow.Append(cell);
                }
            }

            troskoviSheetData.Append(troskoviHeaderRow);

            foreach(DataRow row in sirovinaCena.Rows)
            {
                Row newRow = new Row();
                foreach(DataColumn column in sirovinaCena.Columns)
                {
                    if(!excludedColumns.Contains(column.ColumnName))
                    {
                        Cell cell = new Cell();
                        cell.DataType = getCellValueType(column);
                        cell.CellValue = new CellValue(getCellValue(row, column));
                        newRow.Append(cell);
                    }
                }
                troskoviSheetData.Append(newRow);
            }
            }

            workbookPart.Workbook.Save();
            spreadsheetDocument.Close();
            //using (var workbook = SpreadsheetDocument.Create(destination, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            //{
            //    var workbookPart = workbook.AddWorkbookPart();
            //    workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
            //    workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

            //    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
            //    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
            //    sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

            //    Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            //    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

            //    uint sheetId = 1;
            //    if (sheets.Elements<Sheet>().Count() > 0)
            //        sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;

            //    Sheet sheet = new Sheet()
            //    {
            //        Id = relationshipId,
            //        SheetId = sheetId,
            //        Name = dataTable.TableName
            //    };
            //    sheets.Append(sheet);

                //Row headerRow = new Row();
                //List<string> columns = new List<string>();
                //foreach(DataColumn column in dataTable.Columns)
                //{
                    //columns.Add(column.ColumnName);
                    //Cell cell = new Cell();
                    //cell.DataType = CellValues.String;
                    //cell.CellValue = new CellValue(column.ColumnName);
                    //headerRow.AppendChild(cell);
                //}

                //sheetData.AppendChild(headerRow);

                //foreach(DataRow row in dataTable.Rows)
                //{
                //    Row newRow = new Row();
                //    foreach(string column in columns)
                //    {
                //        Cell cell = new Cell();
                //        //cell.DataType = CellValues.String;
                //        cell.DataType = getCellValueType(dataTable.Columns[column]);
                //        cell.CellValue = new CellValue(row[column].ToString());
                //        cell.CellValue = new CellValue(getCellValue(row, dataTable.Columns[column]));
                //        newRow.AppendChild(cell);
                //    }
                //    sheetData.AppendChild(newRow);
                //}

            //    workbookPart.Workbook.Save();

            //    workbook.Close();
            //}
        }

        private CellValues getCellValueType(DataColumn column)
        {
            TypeCode typeCode = Type.GetTypeCode(column.DataType);
            switch (typeCode)
            {
                case TypeCode.DateTime: return CellValues.Date;
                case TypeCode.String: return CellValues.String;
                case TypeCode.Int32: return CellValues.Number;
                case TypeCode.Decimal: return CellValues.Number;
            }
            return CellValues.String;
        }

        private string getCellValue(DataRow row, DataColumn column)
        {
            if (row[column.ColumnName] == null || string.IsNullOrWhiteSpace(row[column.ColumnName].ToString()))
                return "";
            TypeCode typeCode = Type.GetTypeCode(column.DataType);
            switch (typeCode)
            {
                case TypeCode.Decimal: return Math.Round(decimal.Parse(row[column.ColumnName].ToString()), column.ColumnName.ToString() == "Kolicina" ? 5 : 2).ToString().Replace(',','.');
            }
            return row[column.ColumnName].ToString();
        }

        public bool CreateExcelSpreadsheet(List<BecNutritionCalculator.Models.ExportModel> dataTables, string destination, List<string> excludedColumns)
        {
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            WorkbookStylesPart stylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = getWorkbookStylesPart();

            int sheetId = 1;

            foreach(var exportModel in dataTables)
            {
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = Convert.ToUInt32(sheetId++),
                    Name = exportModel.Name
                };
                sheets.Append(sheet);

                Row row = new Row();
                foreach(DataColumn column in exportModel.DataTable.Columns)
                {
                    if(!excludedColumns.Contains(column.ColumnName))
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ColumnName);
                        cell.StyleIndex = 1U;
                        row.Append(cell);
                    }
                }
                sheetData.Append(row);

                foreach(DataRow dataRow in exportModel.DataTable.Rows)
                {
                    row = new Row();
                    foreach(DataColumn column in exportModel.DataTable.Columns)
                    {
                        if(!excludedColumns.Contains(column.ColumnName))
                        {
                            Cell cell = new Cell();
                            cell.DataType = getCellValueType(column);
                            cell.CellValue = new CellValue(getCellValue(dataRow, column));
                            row.Append(cell);
                        }
                    }
                    sheetData.Append(row);
                }

                foreach(DataTable footerDataTable in exportModel.FooterDataTables)
                {
                    foreach(DataRow dataRow in footerDataTable.Rows)
                    {
                        row = new Row();
                        foreach(DataColumn column in exportModel.DataTable.Columns)
                        {
                            if(!excludedColumns.Contains(column.ColumnName))
                            {
                                Cell cell = new Cell();
                                cell.DataType = getCellValueType(column);
                                cell.CellValue = new CellValue(getCellValue(dataRow, column));
                                cell.StyleIndex = 2U;
                                row.Append(cell);
                            }
                        }
                        sheetData.Append(row);
                    }
                }
            }

            workbookPart.Workbook.Save();
            spreadsheetDocument.Close();

            return true;
        }

        private Stylesheet getWorkbookStylesPart()
        {
            return new Stylesheet(
                new Fonts(
                        new Font(new FontSize() { Val = 10 }, new FontName() { Val = "Calibri" }, new Color() { Rgb = new HexBinaryValue() { Value = "000000" } }),
                        new Font(new FontSize() { Val = 10 }, new FontName() { Val = "Calibri" }, new Color() { Rgb = new HexBinaryValue() { Value = "000000" } }, new Bold() { Val = true })
                    ),
                new Fills(
                        new Fill() { PatternFill = new PatternFill() { PatternType = new EnumValue<PatternValues>(PatternValues.None) } },
                        new Fill(
                            new PatternFill() {
                                PatternType = new EnumValue<PatternValues>(PatternValues.None)
                                //,
                                //BackgroundColor = new BackgroundColor() { Rgb = "000000" },
                                //ForegroundColor = new ForegroundColor() { Rgb = "999999" }
                            } ),
                        new Fill() { PatternFill = new PatternFill() {
                            PatternType = new EnumValue<PatternValues>(PatternValues.Solid),
                            BackgroundColor = new BackgroundColor() { Indexed = 64U },
                            ForegroundColor = new ForegroundColor() { Rgb = "FFD9D9D9" }
                        } }
                    ),
                new Borders(
                        new Border() { LeftBorder = new LeftBorder(), RightBorder = new RightBorder(), TopBorder = new TopBorder(), BottomBorder = new BottomBorder(), DiagonalBorder = new DiagonalBorder() }
                    ),
                new CellFormats(
                        new CellFormat() { FontId = 0, FillId = 0, BorderId = 0, ApplyFont = true },
                        new CellFormat() { FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true },
                        new CellFormat() { FontId = 1, FillId = 2U, BorderId = 0, ApplyFont = true, ApplyFill = true }
                    )
                );
        }

        public void ExportToCsv(BecNutritionCalculator.Models.ExportModel dataTable, string destination, List<string> excludedColumns)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dataTable.DataTable.Columns.Cast<DataColumn>().Where(column => !excludedColumns.Contains(column.ColumnName)).Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach(DataRow row in dataTable.DataTable.Rows)
            {
                StringBuilder line = new StringBuilder();
                foreach(DataColumn column in dataTable.DataTable.Columns)
                {
                    if(!excludedColumns.Contains(column.ColumnName))
                        line.Append(getCellValue(row, column).Replace('.', ',')).Append(";");
                    //IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    //sb.AppendLine(string.Join(";", fields));
                }
                sb.AppendLine(line.ToString());
            }

            foreach(DataTable dt in dataTable.FooterDataTables)
            {
                foreach(DataRow row in dt.Rows)
                {
                    StringBuilder line = new StringBuilder();
                    foreach(DataColumn column in dt.Columns)
                    {
                        if (!excludedColumns.Contains(column.ColumnName))
                            line.Append(getCellValue(row, column).Replace('.', ',')).Append(";");
                    }
                    sb.AppendLine(line.ToString());
                }
            }

            File.WriteAllText(destination, sb.ToString());
        }
    }
}
