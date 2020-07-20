using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface ICalculatorBL
    {
        DataTable SirovineDT { get; set; }
        DataTable SmesaDT { get; set; }
        DataTable TotalDT { get; set; }
        DataTable ZahtevaneVrednostiDT { get; set; }

        void DodajSirovinu(int sirovinaID);
        void IzmeniVrednost(int sirovinaID, decimal vrednost);
    }
}
