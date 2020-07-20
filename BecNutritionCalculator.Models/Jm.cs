using System;
using System.ComponentModel.DataAnnotations;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class Jm : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }
    }
}