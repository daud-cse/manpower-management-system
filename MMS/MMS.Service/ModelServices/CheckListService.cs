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
    
    public interface ICheckListService
    {
        CheckList Find(params object[] keyValues);
        void Insert(CheckList entity);
        void Update(CheckList entity);
        void Delete(object id);
        void Delete(CheckList entity);
        IEnumerable<CheckList> GetAllCheckList(int GlobalCompanyId);
        IEnumerable<CheckList> GetAllCheckList();
        IEnumerable<CheckList> GetActiveCheckList(int GlobalCompanyId, bool IsActive);
        CheckList GetCheckListById(int Id, int GlobalCompanyId);

    }
    public class CheckListService : Service<CheckList>, ICheckListService
    {
        private readonly IRepositoryAsync<CheckList> _repository;

        public CheckListService(

              IRepositoryAsync<CheckList> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<CheckList> GetAllCheckList(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<CheckList> GetAllCheckList()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<CheckList> GetActiveCheckList(int GlobalCompanyId, bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public CheckList GetCheckListById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
