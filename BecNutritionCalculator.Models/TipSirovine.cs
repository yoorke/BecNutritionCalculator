using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using GenericBE;

namespace BecNutritionCalculator.Models
{
    public class TipSirovine : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [SqlFieldNameAttribute("naziv")]
        public string Naziv { get; set; }

        [SqlFieldNameAttribute("color")]
        public string Color { get; set; }

        [SqlFieldNameAttribute("code")]
        public string Code { get; set; }
    }
}
