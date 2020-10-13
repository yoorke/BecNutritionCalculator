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
    public class KategorijaZivotinjaSmesaPotrosnjaBL : BaseBL<KategorijaZivotinjaSmesaPotrosnja>, IKategorijaZivotinjaSmesaPotrosnjaBL
    {
        private IGenericRepository<KategorijaZivotinjaSmesaPotrosnja> _repository;

        public KategorijaZivotinjaSmesaPotrosnjaBL(IGenericRepository<KategorijaZivotinjaSmesaPotrosnja> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<KategorijaZivotinjaSmesaPotrosnja> GetBySmesaID(int smesaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("smesa_id", smesaID));

            return _repository.GetByParameter("getBySmesaID", parameters);
        }

        public KategorijaZivotinjaSmesaPotrosnja GetByKategorijaZivotinjaID(int kategorijaZivotinjaID)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("kategorija_zivotinja_id", kategorijaZivotinjaID));

            IEnumerable<KategorijaZivotinjaSmesaPotrosnja> kategorijaZivotinjaSmesaPotronjaList = _repository.GetByParameter("getByKategorijaZivotinjaID", parameters);
            if (kategorijaZivotinjaSmesaPotronjaList != null && kategorijaZivotinjaSmesaPotronjaList.Count() > 0)
                return kategorijaZivotinjaSmesaPotronjaList.ToList()[0];

            return null;
        }

        public void SaveItem(KategorijaZivotinjaSmesaPotrosnja kategorijaZivotinjaSmesaPotrosnja)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("kategorija_zivotinja_id", kategorijaZivotinjaSmesaPotrosnja.KategorijaZivotinjaID));
            parameters.Add(new QueryParameter("broj_zivotinja", kategorijaZivotinjaSmesaPotrosnja.BrojZivotinja));
            parameters.Add(new QueryParameter("dnevna_potrosnja", kategorijaZivotinjaSmesaPotrosnja.DnevnaPotrosnja));
            parameters.Add(new QueryParameter("nedeljni_broj_hranjenja", kategorijaZivotinjaSmesaPotrosnja.NedeljniBrojHranjenja));
            parameters.Add(new QueryParameter("smesa_id", kategorijaZivotinjaSmesaPotrosnja.SmesaID));

            _repository.ExecuteAction("kategorijaZivotinjaSmesaPotrosnja", "save", parameters);
        }
    }
}
