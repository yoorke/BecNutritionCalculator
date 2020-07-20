using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.BusinessLogic;
using RepositoryInterfaces;
using GenericBE;

namespace BecNutritionCalculator.BusinessLogic
{
    public class NutritivniElementVrednostBL : BaseBL<NutritivniElementVrednost>, INutritivniElementVrednostBL
    {
        private IGenericRepository<NutritivniElementVrednost> _repository;

        public NutritivniElementVrednostBL(IGenericRepository<NutritivniElementVrednost> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<NutritivniElementVrednost> GetBySirovinaID(int sirovinaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("sirovina_id", sirovinaID));
            return _repository.GetByParameter("getBySirovinaID", parameters);
        }

        public IEnumerable<NutritivniElementVrednost> GetBySmesaID(int smesaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("smesa_id", smesaID));
            return _repository.GetByParameter("getBySmesaID", parameters);
        }
    }
}
