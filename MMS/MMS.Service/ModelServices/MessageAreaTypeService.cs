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
  

    public interface IMessageAreaTypeService
    {
        MessageAreaType Find(params object[] keyValues);
        void Insert(MessageAreaType entity);
        void Update(MessageAreaType entity);
        void Delete(object id);
        void Delete(MessageAreaType entity);
        IEnumerable<MessageAreaType> GetAllMessageAreaType();
        IEnumerable<MessageAreaType> GetActiveMessageAreaType(bool IsActive);
        MessageAreaType GetMessageAreaTypeById(int Id);

    }
    public class MessageAreaTypeService : Service<MessageAreaType>, IMessageAreaTypeService
    {
        private readonly IRepositoryAsync<MessageAreaType> _repository;

        public MessageAreaTypeService(

              IRepositoryAsync<MessageAreaType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<MessageAreaType> GetAllMessageAreaType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MessageAreaType> GetActiveMessageAreaType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public MessageAreaType GetMessageAreaTypeById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }

    }
}
