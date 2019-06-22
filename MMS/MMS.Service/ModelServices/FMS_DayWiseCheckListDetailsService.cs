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
    

    public interface IFMS_DayWiseCheckListDetailsService
    {
        FMS_DayWiseCheckListDetails Find(params object[] keyValues);
        void Insert(FMS_DayWiseCheckListDetails entity);
        void Update(FMS_DayWiseCheckListDetails entity);
        void Delete(object id);
        void Delete(FMS_DayWiseCheckListDetails entity);
        IEnumerable<FMS_DayWiseCheckListDetails> GetAllFMS_DayWiseCheckListDetails();
        IEnumerable<FMS_DayWiseCheckListDetails> GetActiveFMS_DayWiseCheckListDetails(bool IsActive);
        FMS_DayWiseCheckListDetails GetFMS_DayWiseCheckListDetailsById(int Id);
        IEnumerable<FMS_DayWiseCheckListDetails> GetFMS_DayWiseCheckListDetails(int GlobalCompanyId,DateTime openDate);

    }
    public class FMS_DayWiseCheckListDetailsService : Service<FMS_DayWiseCheckListDetails>, IFMS_DayWiseCheckListDetailsService
    {
        private readonly IRepositoryAsync<FMS_DayWiseCheckListDetails> _repository;

        public FMS_DayWiseCheckListDetailsService(

              IRepositoryAsync<FMS_DayWiseCheckListDetails> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_DayWiseCheckListDetails> GetAllFMS_DayWiseCheckListDetails()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_DayWiseCheckListDetails> GetActiveFMS_DayWiseCheckListDetails(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public IEnumerable<FMS_DayWiseCheckListDetails> GetFMS_DayWiseCheckListDetails(int GlobalCompanyId,DateTime openDate)
        {
            return _repository
                   .Query(x => x.OpenDate == openDate && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x=>x.FMS_DayOpenCloseCheckList)
                   .Select();
        }

        public FMS_DayWiseCheckListDetails GetFMS_DayWiseCheckListDetailsById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
