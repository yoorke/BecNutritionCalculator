using System;
using System.ComponentModel.DataAnnotations;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class NutritivniElement : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [Required]
        [StringLength(50)]
        [SqlFieldNameAttribute("skraceni_naziv")]
        public string SkraceniNaziv { get; set; }
    }
}