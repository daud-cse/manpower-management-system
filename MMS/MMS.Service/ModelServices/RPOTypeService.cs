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

    public interface IRPOTypeService
    {
        RPOType Find(params object[] keyValues);
        void Insert(RPOType entity);
        void Update(RPOType entity);
        void Delete(object id);
        void Delete(RPOType entity);
        IEnumerable<RPOType> GetAllRPOType();
        IEnumerable<RPOType> GetActiveRPOType(bool IsActive);
        RPOType GetRPOTypeById(int Id);

    }
    public class RPOTypeService : Service<RPOType>, IRPOTypeService
    {
        private readonly IRepositoryAsync<RPOType> _repository;

        public RPOTypeService(

              IRepositoryAsync<RPOType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<RPOType> GetAllRPOType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<RPOType> GetActiveRPOType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public RPOType GetRPOTypeById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
