using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class KalkulacijaView
    {
        [SqlFieldNameAttribute("id")]
        public int ID { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("datum")]
        public DateTime Datum { get; set; }

        [SqlFieldNameAttribute("smesa_naziv")]
        public string SmesaNaziv { get; set; }

        [SqlFieldNameAttribute("datum_izmene")]
        public DateTime DatumIzmene { get; set; }

        [SqlFieldNameAttribute("ukupno")]
        public Decimal Ukupno { get; set; }

        [SqlFieldNameAttribute("ukupno_kabaste")]
        public Decimal UkupnoKabaste { get; set; }

        [SqlFieldNameAttribute("ukupno_kupovne")]
        public Decimal UkupnoKupovne { get; set; }

        public string FullName
        {
            get
            {
                return Naziv.PadRight(30, ' ') + SmesaNaziv.PadRight(20, ' ') + Datum.ToString("dd.MM.yyyy");
            }
        }
    }
}
