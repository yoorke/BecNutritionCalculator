using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface ISmesaNutritivniElementVrednostBL : IBaseBL<SmesaNutritivniElementVrednost>
    {
        IEnumerable<SmesaNutritivniElementVrednost> GetBySmesaID(int smesaID);
        void SaveVrednost(SmesaNutritivniElementVrednost smesaNutritivniElementVrednost);
    }
}
