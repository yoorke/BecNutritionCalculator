using System;
using Base.Models;
using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface ISirovinaBL : IBaseBL<Sirovina>
    {
        IEnumerable<Sirovina> GetByTipSirovine(int tipSirovineID);
        IEnumerable<Sirovina> GetByTipSirovineCode(string code);
        IEnumerable<Sirovina> GetByNutritivniElementNaziv(string nutritivniElementNaziv);
        IEnumerable<Sirovina> GetAll(bool includeNotActive);
        DataTable GetPotrosnjaUPeriodu(string ids);
    }
}