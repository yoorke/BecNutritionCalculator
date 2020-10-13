using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class SmesaNutritivniElementVrednost
    {
        [SqlFieldNameAttribute("smesa_id")]
        public int SmesaID { get; set; }

        [SqlFieldNameAttribute("nutritivni_element_id")]
        public int NutritivniElementID { get; set; }

        [SqlFieldNameAttribute("zahtevana_vrednost")]
        public decimal Vrednost { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("stara_vrednost")]
        public decimal StaraVrednost { get; set; }
    }
}
