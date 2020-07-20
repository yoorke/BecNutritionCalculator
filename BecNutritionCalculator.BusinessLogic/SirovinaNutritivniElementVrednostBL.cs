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
    public class SirovinaNutritivniElementVrednostBL : BaseBL<SirovinaNutritivniElementVrednost>, ISirovinaNutritivniElementVrednostBL
    {
        private IGenericRepository<SirovinaNutritivniElementVrednost> _repository;

        public SirovinaNutritivniElementVrednostBL(IGenericRepository<SirovinaNutritivniElementVrednost> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<SirovinaNutritivniElementVrednost> GetBySirovinaID(int sirovinaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("sirovina_id", sirovinaID));
            return _repository.GetByParameter("getBySirovinaID", parameters);
        }

        public void UpdateVrednost(SirovinaNutritivniElementVrednost sirovinaNutritivniElementVrednost)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("sirovina_id", sirovinaNutritivniElementVrednost.SirovinaID));
            parameters.Add(new QueryParameter("nutritivni_element_id", sirovinaNutritivniElementVrednost.NutritivniElementID));
            parameters.Add(new QueryParameter("vrednost", sirovinaNutritivniElementVrednost.Vrednost));

            _repository.GetByParameter("update", parameters);
        }
    }
}
