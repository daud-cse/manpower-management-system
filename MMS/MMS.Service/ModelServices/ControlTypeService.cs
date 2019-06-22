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

    public interface IControlTypeService
    {
        ControlType Find(params object[] keyValues);
        void Insert(ControlType entity);
        void Update(ControlType entity);
        void Delete(object id);
        void Delete(ControlType entity);
        IEnumerable<ControlType> GetAllControlType();
        IEnumerable<ControlType> GetActiveControlType(bool IsActive);
        List<KeyValuePair<int, string>> GetKVPActiveControlType(bool IsActive);
        ControlType GetControlTypeById(int Id);

    }
    public class ControlTypeService : Service<ControlType>, IControlTypeService
    {
        private readonly IRepositoryAsync<ControlType> _repository;

        public ControlTypeService(

              IRepositoryAsync<ControlType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ControlType> GetAllControlType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ControlType> GetActiveControlType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ControlType GetControlTypeById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }
        public List<KeyValuePair<int, string>> GetKVPActiveControlType(bool IsActive)
        {
            var kvpControlType = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.IsActive == IsActive).Select().ToList().ForEach(delegate(ControlType item) {

                kvpControlType.Add(new KeyValuePair<int, string>(item.Id, item.ControlTypeName));

            });
            return kvpControlType;
        }

    }
}
