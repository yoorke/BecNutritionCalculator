using BecNutritionCalculator.Models;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface IExportBL
    {
        void CreateExcelSpreadsheet(DataTable dataTable, string destination, List<DataTable> footerDataTables = null, List<string> excludedColumns = null, DataTable sirovinaCena = null);
        bool CreateExcelSpreadsheet(List<ExportModel> dataTables, string destination, List<string> excludedColumns);
        void ExportToCsv(ExportModel dataTable, string destination, List<string> excludedColumns);
    }
}
