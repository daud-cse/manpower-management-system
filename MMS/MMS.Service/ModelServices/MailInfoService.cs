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
    public interface IMailInfoService
    {
        MailInfo Find(params object[] keyValues);
        void Insert(MailInfo entity);
        void Update(MailInfo entity);
        void Delete(object id);
        void Delete(MailInfo entity);
        IEnumerable<MailInfo> GetAllMailInfo();
        IEnumerable<MailInfo> GetActiveMailInfo(bool IsActive);
        MailInfo GetMailInfoById(int Id);

    }
    public class MailInfoService : Service<MailInfo>, IMailInfoService
    {
        private readonly IRepositoryAsync<MailInfo> _repository;

        public MailInfoService(

              IRepositoryAsync<MailInfo> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<MailInfo> GetAllMailInfo()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MailInfo> GetActiveMailInfo(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsSent == true)
                   .Select();
        }

        public MailInfo GetMailInfoById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }

    }
}
