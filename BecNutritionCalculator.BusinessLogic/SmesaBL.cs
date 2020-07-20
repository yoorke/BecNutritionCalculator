using System;
using System.Collections.Generic;
using GenericBE;
using Base.Models;
using Base.BusinessLogic;
using RepositoryInterfaces;
using BecNutritionCalculator.Models;
using BecNutritionCalculator.BusinessLogic.Interfaces;

namespace BecNutritionCalculator.BusinessLogic
{
    public class SmesaBL : BaseBL<Smesa>, ISmesaBL
    {
        private IGenericRepository<Smesa> _repository;

        public SmesaBL(IGenericRepository<Smesa> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}