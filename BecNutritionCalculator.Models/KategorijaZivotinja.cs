using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class KategorijaZivotinja
    {
        [SqlFieldNameAttribute("id")]
        public int ID { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("broj_zivotinja")]
        public int BrojZivotinja { get; set; }
    }
}
