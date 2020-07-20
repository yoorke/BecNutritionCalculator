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
    public class NutritivniElementBL : BaseBL<NutritivniElement>, INutritivniElementBL
    {
        private IGenericRepository<NutritivniElement> _repository;

        public NutritivniElementBL(IGenericRepository<NutritivniElement> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}