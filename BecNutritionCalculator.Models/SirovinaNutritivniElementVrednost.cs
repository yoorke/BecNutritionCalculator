using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class SirovinaNutritivniElementVrednost
    {
        [SqlFieldNameAttribute("sirovina_id")]
        public int SirovinaID { get; set; }

        [SqlFieldNameAttribute("nutritivni_element_id")]
        public int NutritivniElementID { get; set; }

        [SqlFieldNameAttribute("vrednost")]
        public decimal Vrednost { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("stara_vrednost")]
        public decimal StaraVrednost { get; set; }
    }
}
