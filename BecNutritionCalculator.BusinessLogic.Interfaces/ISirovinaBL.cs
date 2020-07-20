using System;
using Base.Models;
using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System.Collections;
using System.Collections.Generic;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface ISirovinaBL : IBaseBL<Sirovina>
    {
        IEnumerable<Sirovina> GetByTipSirovine(int tipSirovineID);
    }
}