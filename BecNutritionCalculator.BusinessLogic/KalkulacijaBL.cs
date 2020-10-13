using Base.BusinessLogic;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using GenericBE;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic
{
    public class KalkulacijaBL : BaseBL<Kalkulacija>, IKalkulacijaBL
    {
        private IGenericRepository<Kalkulacija> _repository;
        
        public KalkulacijaBL(IGenericRepository<Kalkulacija> repository) : base(repository)
        {
            _repository = repository;
        }

        public DataTable GetTroskovi(string ids)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("ids", ids, true));
            return _repository.GetDataTable("kalkulacija", "getTroskovi", parameters);
        }

        public DataTable GetPotrosnja(string ids)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("ids", ids, true));
            return _repository.GetDataTable("kalkulacija", "getPotrosnja", parameters);
        }
    }
}
