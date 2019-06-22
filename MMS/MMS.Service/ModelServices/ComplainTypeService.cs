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
   

    public interface IComplainTypeService
    {
        ComplainType Find(params object[] keyValues);
        void Insert(ComplainType entity);
        void Update(ComplainType entity);
        void Delete(object id);
        void Delete(ComplainType entity);
        IEnumerable<ComplainType> GetAllComplainType();
        IEnumerable<ComplainType> GetActiveComplainType(bool IsActive);
        ComplainType GetComplainTypeById(int Id);

    }
    public class ComplainTypeService : Service<ComplainType>, IComplainTypeService
    {
        private readonly IRepositoryAsync<ComplainType> _repository;

        public ComplainTypeService(

              IRepositoryAsync<ComplainType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ComplainType> GetAllComplainType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ComplainType> GetActiveComplainType(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive)
                   .Select();
        }

        public ComplainType GetComplainTypeById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
