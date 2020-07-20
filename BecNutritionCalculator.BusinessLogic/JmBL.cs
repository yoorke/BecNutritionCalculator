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
    public class JmBL : BaseBL<Jm>, IJmBL
    {
        private IGenericRepository<Jm> _repository;

        public JmBL(IGenericRepository<Jm> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}