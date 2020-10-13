using Base.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic.Interfaces
{
    public interface IKategorijaZivotinjaSmesaPotrosnjaBL : IBaseBL<KategorijaZivotinjaSmesaPotrosnja>
    {
        IEnumerable<KategorijaZivotinjaSmesaPotrosnja> GetBySmesaID(int smesaID);
        KategorijaZivotinjaSmesaPotrosnja GetByKategorijaZivotinjaID(int kategorijaZivotinjaID);
        void SaveItem(KategorijaZivotinjaSmesaPotrosnja kategorijaZivotinjaSmesaPotrosnja);
    }
}
