using Base.BusinessLogic;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic
{
    public class KategorijaZivotinjaBL : BaseBL<KategorijaZivotinja>, IKategorijaZivotinjaBL
    {
        private IGenericRepository<KategorijaZivotinja> _repository;

        public KategorijaZivotinjaBL(IGenericRepository<KategorijaZivotinja> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
