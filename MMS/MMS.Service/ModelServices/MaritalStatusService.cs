using MMS.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{
  
    public interface IMaritalStatusService
    {
        MaritalStatu Find(params object[] keyValues);
        void Insert(MaritalStatu entity);
        void Update(MaritalStatu entity);
        void Delete(object id);
        void Delete(MaritalStatu entity);
        IEnumerable<MaritalStatu> GetAllMaritalStatu();
        IEnumerable<MaritalStatu> GetActiveMaritalStatu(bool IsActive);
        MaritalStatu GetMaritalStatuById(int Id);

    }
    public class MaritalStatuService : Service<MaritalStatu>, IMaritalStatusService
    {
        private readonly IRepositoryAsync<MaritalStatu> _repository;

        public MaritalStatuService(

              IRepositoryAsync<MaritalStatu> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<MaritalStatu> GetAllMaritalStatu()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MaritalStatu> GetActiveMaritalStatu(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public MaritalStatu GetMaritalStatuById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
