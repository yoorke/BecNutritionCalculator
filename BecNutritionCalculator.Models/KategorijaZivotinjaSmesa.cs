using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class KategorijaZivotinjaSmesa
    {
        [SqlFieldNameAttribute("kategorija_zivotinja_id")]
        public int KategorijaZivotinjaID { get; set; }

        [SqlFieldNameAttribute("smesa_id")]
        public int SmesaID { get; set; }

        [SqlFieldNameAttribute("dnevna_potrosnja")]
        public decimal DnevnaPotrosnja { get; set; }

        [SqlFieldNameAttribute("nedeljni_broj_hranjenja")]
        public int NedeljniBrojHranjenja { get; set; }
    }
}
