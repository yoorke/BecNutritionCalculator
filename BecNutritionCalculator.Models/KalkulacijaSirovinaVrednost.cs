using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class KalkulacijaSirovinaVrednost
    {
        [SqlFieldNameAttribute("kalkulacija_id")]
        public int KalkulacijaID { get; set; }

        [SqlFieldNameAttribute("sirovina_id")]
        public int SirovinaID { get; set; }

        [SqlFieldNameAttribute("kolicina")]
        public decimal Kolicina { get; set; }

        [SqlFieldNameAttribute("cena")]
        public decimal Cena { get; set; }

        [SqlFieldNameAttribute("id")]
        public int ID { get; set; }
    }
}
