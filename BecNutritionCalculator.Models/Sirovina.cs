using System;
using System.ComponentModel.DataAnnotations;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class Sirovina : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [Required]

        [SqlFieldNameAttribute("tip_sirovine_id")]
        public int TipSirovineID { get; set; }

        [SqlFieldNameAttribute("jm_id")]
        public int JmID { get; set; }

        [SqlFieldNameAttribute("jm_naziv")]
        public string JmNaziv { get; set; }

        [SqlFieldNameAttribute("cena")]
        public decimal Cena { get; set; }

        [SqlFieldNameAttribute("kolicinski_odnos")]
        public decimal KolicinskiOdnos { get; set; }
    }
}