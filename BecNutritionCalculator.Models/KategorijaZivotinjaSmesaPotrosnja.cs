using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class KategorijaZivotinjaSmesaPotrosnja
    {
        [SqlFieldNameAttribute("kategorija_zivotinja_id")]
        public int KategorijaZivotinjaID { get; set; }

        [SqlFieldNameAttribute("kategorija_zivotinja_naziv")]
        public string KategorijaZivotinjaNaziv { get; set; }

        [SqlFieldNameAttribute("broj_zivotinja")]
        public int BrojZivotinja { get; set; }

        [SqlFieldNameAttribute("dnevna_potrosnja")]
        public decimal DnevnaPotrosnja { get; set; }

        [SqlFieldNameAttribute("nedeljni_broj_hranjenja")]
        public int NedeljniBrojHranjenja { get; set; }

        [SqlFieldNameAttribute("smesa_id")]
        public int SmesaID { get; set; }
    }
}
