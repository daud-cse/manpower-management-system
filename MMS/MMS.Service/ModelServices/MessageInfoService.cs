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

    public interface IMessageInfoService
    {
        MessageInfo Find(params object[] keyValues);
        void Insert(MessageInfo entity);
        void Update(MessageInfo entity);
        void Delete(object id);
        void Delete(MessageInfo entity);
        IEnumerable<MessageInfo> GetAllMessageInfo();
        IEnumerable<MessageInfo> GetActiveMessageInfo(bool IsActive);
        IEnumerable<MessageInfo> GetNotSentMessage(bool IsSent);
        MessageInfo GetMessageInfoById(int Id);

    }
    public class MessageInfoService : Service<MessageInfo>, IMessageInfoService
    {
        private readonly IRepositoryAsync<MessageInfo> _repository;

        public MessageInfoService(

              IRepositoryAsync<MessageInfo> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<MessageInfo> GetAllMessageInfo()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MessageInfo> GetActiveMessageInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public IEnumerable<MessageInfo> GetNotSentMessage(bool IsSent)
        {
            return _repository
                   .Query(x => x.IsSent == IsSent)
                   .Select();
        }
        public MessageInfo GetMessageInfoById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }

    }
}
