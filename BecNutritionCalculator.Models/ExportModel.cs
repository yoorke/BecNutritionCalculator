using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class ExportModel
    {
        public DataTable DataTable { get; set; }
        public List<DataTable> FooterDataTables { get; set; }
        public string Name { get; set; }
    }
}
