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

    public interface ISendingAreaTypeService
    {
        SendingAreaType Find(params object[] keyValues);
        void Insert(SendingAreaType entity);
        void Update(SendingAreaType entity);
        void Delete(object id);
        void Delete(SendingAreaType entity);
        IEnumerable<SendingAreaType> GetAllSendingAreaType();
        IEnumerable<SendingAreaType> GetActiveSendingAreaType(bool IsActive);
        SendingAreaType GetSendingAreaTypeById(int Id);
        List<KeyValuePair<int, string>> KvpGetSendingAreaType(bool IsActive);

    }
    public class SendingAreaTypeService : Service<SendingAreaType>, ISendingAreaTypeService
    {
        private readonly IRepositoryAsync<SendingAreaType> _repository;

        public SendingAreaTypeService(

              IRepositoryAsync<SendingAreaType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<SendingAreaType> GetAllSendingAreaType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<SendingAreaType> GetActiveSendingAreaType(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == true)
                   .Select();
        }

        public SendingAreaType GetSendingAreaTypeById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }
        public List<KeyValuePair<int, string>> KvpGetSendingAreaType(bool IsActive)
        {
            var kvpSendingAreaType = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.IsActive == IsActive).Select().ToList().ForEach(delegate(SendingAreaType item)
            {

                kvpSendingAreaType.Add(new KeyValuePair<int, string>(item.Id, item.SendingAreaName));
            });

            return kvpSendingAreaType;
        }

    }
}
