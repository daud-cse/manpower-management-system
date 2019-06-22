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
    
    public interface IFMS_DayOpenCloseCheckListService
    {
        FMS_DayOpenCloseCheckList Find(params object[] keyValues);
        void Insert(FMS_DayOpenCloseCheckList entity);
        void Update(FMS_DayOpenCloseCheckList entity);
        void Delete(object id);
        void Delete(FMS_DayOpenCloseCheckList entity);
        IEnumerable<FMS_DayOpenCloseCheckList> GetAllFMS_DayOpenCloseCheckList();
        IEnumerable<FMS_DayOpenCloseCheckList> GetActiveFMS_DayOpenCloseCheckList(bool IsActive);
        FMS_DayOpenCloseCheckList GetFMS_DayOpenCloseCheckListById(int Id);

    }
    public class FMS_DayOpenCloseCheckListService : Service<FMS_DayOpenCloseCheckList>, IFMS_DayOpenCloseCheckListService
    {
        private readonly IRepositoryAsync<FMS_DayOpenCloseCheckList> _repository;

        public FMS_DayOpenCloseCheckListService(

              IRepositoryAsync<FMS_DayOpenCloseCheckList> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_DayOpenCloseCheckList> GetAllFMS_DayOpenCloseCheckList()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_DayOpenCloseCheckList> GetActiveFMS_DayOpenCloseCheckList(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_DayOpenCloseCheckList GetFMS_DayOpenCloseCheckListById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
