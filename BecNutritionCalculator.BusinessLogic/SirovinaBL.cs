using System;
using System.Collections.Generic;
using GenericBE;
using Base.Models;
using Base.BusinessLogic;
using RepositoryInterfaces;
using BecNutritionCalculator.Models;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using System.Data;

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

        public IEnumerable<Sirovina> GetByTipSirovineCode(string code)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("code", code));
            return _repository.GetByParameter("getByTipSirovineCode", parameters);
        }

        public IEnumerable<Sirovina> GetByNutritivniElementNaziv(string nutritivniElementNaziv)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("nutritivni_element_naziv", nutritivniElementNaziv));
            return _repository.GetByParameter("getByNutritivniElementNaziv", parameters);
        }

        public IEnumerable<Sirovina> GetAll(bool includeNotActive)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("include_not_active", includeNotActive));
            return _repository.GetByParameter("get", parameters);
        }

        public DataTable GetPotrosnjaUPeriodu(string ids)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("ids", ids, true));
            return _repository.GetDataTable("sirovina", "getPotrosnjaUPeriodu", parameters);
        }
    }
}