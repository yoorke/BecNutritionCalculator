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
    public class KalkulacijaViewBL : BaseBL<KalkulacijaView>, IKalkulacijaViewBL
    {
        private IGenericRepository<KalkulacijaView> _repository;

        public KalkulacijaViewBL(IGenericRepository<KalkulacijaView> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
