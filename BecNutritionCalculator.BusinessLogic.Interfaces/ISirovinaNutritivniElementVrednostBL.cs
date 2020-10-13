using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface ISirovinaNutritivniElementVrednostBL : IBaseBL<SirovinaNutritivniElementVrednost>
    {
        IEnumerable<SirovinaNutritivniElementVrednost> GetBySirovinaID(int sirovinaID);
        void UpdateVrednost(SirovinaNutritivniElementVrednost sirovinaNutritivniElementVrednost);
        void SaveVrednost(SirovinaNutritivniElementVrednost sirovinaNutritivniElementVrednost);
    }
}
