using Base.BusinessLogic;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using GenericBE;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecNutritionCalculator.BusinessLogic
{
    public class SmesaNutritivniElementVrednostBL : BaseBL<SmesaNutritivniElementVrednost>, ISmesaNutritivniElementVrednostBL
    {
        private IGenericRepository<SmesaNutritivniElementVrednost> _repository;

        public SmesaNutritivniElementVrednostBL(IGenericRepository<SmesaNutritivniElementVrednost> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<SmesaNutritivniElementVrednost> GetBySmesaID(int smesaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("smesa_id", smesaID));
            return _repository.GetByParameter("getBySmesaID", parameters);
        }

        public void SaveVrednost(SmesaNutritivniElementVrednost smesaNutritivniElementVrednost)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("smesa_id", smesaNutritivniElementVrednost.SmesaID));
            parameters.Add(new QueryParameter("nutritivni_element_id", smesaNutritivniElementVrednost.NutritivniElementID));
            parameters.Add(new QueryParameter("vrednost", smesaNutritivniElementVrednost.Vrednost));
            _repository.GetByParameter("save", parameters);
        }
    }
}
