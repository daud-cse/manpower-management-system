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
   
    public interface ICheckListGroupService
    {
        CheckListGroup Find(params object[] keyValues);
        void Insert(CheckListGroup entity);
        void Update(CheckListGroup entity);
        void Delete(object id);
        void Delete(CheckListGroup entity);
        IEnumerable<CheckListGroup> GetAllCheckListGroup();
        IEnumerable<CheckListGroup> GetActiveCheckListGroup(bool IsActive);
        CheckListGroup GetCheckListGroupById(int Id);

    }
    public class CheckListGroupService : Service<CheckListGroup>, ICheckListGroupService
    {
        private readonly IRepositoryAsync<CheckListGroup> _repository;

        public CheckListGroupService(

              IRepositoryAsync<CheckListGroup> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<CheckListGroup> GetAllCheckListGroup()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<CheckListGroup> GetActiveCheckListGroup(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public CheckListGroup GetCheckListGroupById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
