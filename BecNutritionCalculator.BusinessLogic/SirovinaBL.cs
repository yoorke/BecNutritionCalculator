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
    public class SirovinaBL : BaseBL<Sirovina>, ISirovinaBL
    {
        private IGenericRepository<Sirovina> _repository;

        public SirovinaBL(IGenericRepository<Sirovina> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Sirovina> GetByTipSirovine(int tipSirovineID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("tip_sirovine_id", tipSirovineID));
            return _repository.GetByParameter("getByTipSirovineID", parameters);
        }
    }
}