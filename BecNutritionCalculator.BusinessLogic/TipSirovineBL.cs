using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BusinessLogic;
using BecNutritionCalculator.Models;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using RepositoryInterfaces;

namespace BecNutritionCalculator.BusinessLogic
{
    public class TipSirovineBL : BaseBL<TipSirovine>, ITipSirovineBL
    {
        private IGenericRepository<TipSirovine> _repository;

        public TipSirovineBL(IGenericRepository<TipSirovine> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
