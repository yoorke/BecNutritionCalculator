using GenericBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.Models
{
    public class SirovinaNeVrednost
    {
        public int SirovinaID { get; set; }

        public string Naziv { get; set; }

        public decimal Kolicina { get; set; }

        public IEnumerable<NutritivniElementVrednost> Vrednosti { get; set; }
    }
}
