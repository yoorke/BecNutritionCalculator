using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface IKalkulacijaBL : IBaseBL<Kalkulacija>
    {
        DataTable GetTroskovi(string ids);
        DataTable GetPotrosnja(string ids);
    }
}
