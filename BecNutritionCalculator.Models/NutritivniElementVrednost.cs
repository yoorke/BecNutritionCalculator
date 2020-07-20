using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class NutritivniElementVrednost
    {
        [SqlFieldNameAttribute("nutritivni_element_id")]
        public int NutritivniElementID { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("vrednost")]
        public decimal Vrednost { get; set; }

        [SqlFieldNameAttribute("skraceni_naziv")]
        public string SkraceniNaziv { get; set; }

        [SqlFieldNameAttribute("jm_id")]
        public int JmID { get; set; }
    }
}
