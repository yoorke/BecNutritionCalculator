using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class Kalkulacija
    {
        [SqlFieldNameAttribute("id")]
        public int ID { get; set; }

        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("datum")]
        public DateTime Datum { get; set; }

        [SqlFieldNameAttribute("smesa_id")]
        public int SmesaID { get; set; }

        [SqlFieldNameAttribute("kalkulacija_id", "KalkulacijaSirovinaVrednost", "id", Relation.OneToMany, "getByKalkulacijaID", true, false)]
        public List<KalkulacijaSirovinaVrednost> Vrednosti { get; set; }
    }
}