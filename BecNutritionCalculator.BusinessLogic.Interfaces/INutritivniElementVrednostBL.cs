using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface INutritivniElementVrednostBL : IBaseBL<NutritivniElementVrednost>
    {
        IEnumerable<NutritivniElementVrednost> GetBySirovinaID(int sirovinaID);
        IEnumerable<NutritivniElementVrednost> GetBySmesaID(int smesaID);
    }
}
