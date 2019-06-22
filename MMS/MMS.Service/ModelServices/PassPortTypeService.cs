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
  
    public interface IPassPortTypeService
    {
        PassPortType Find(params object[] keyValues);
        void Insert(PassPortType entity);
        void Update(PassPortType entity);
        void Delete(object id);
        void Delete(PassPortType entity);
        IEnumerable<PassPortType> GetAllPassPortType();
        IEnumerable<PassPortType> GetActivePassPortType(bool IsActive);
        PassPortType GetPassPortTypeById(int Id);

    }
    public class PassPortTypeService : Service<PassPortType>, IPassPortTypeService
    {
        private readonly IRepositoryAsync<PassPortType> _repository;

        public PassPortTypeService(

              IRepositoryAsync<PassPortType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<PassPortType> GetAllPassPortType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<PassPortType> GetActivePassPortType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public PassPortType GetPassPortTypeById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
