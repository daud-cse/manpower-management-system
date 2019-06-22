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
   

    public interface IDesignationService
    {
        Designation Find(params object[] keyValues);
        void Insert(Designation entity);
        void Update(Designation entity);
        void Delete(object id);
        void Delete(Designation entity);
        IEnumerable<Designation> GetAllDesignation(int GlobalCompanyId);
        IEnumerable<Designation> GetActiveDesignation(int GlobalCompanyId, bool IsActive);
        Designation GetDesignationById(int Id,int GlobalCompanyId);

    }
    public class DesignationService : Service<Designation>, IDesignationService
    {
        private readonly IRepositoryAsync<Designation> _repository;

        public DesignationService(

              IRepositoryAsync<Designation> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Designation> GetAllDesignation(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<Designation> GetActiveDesignation(int GlobalCompanyId, bool IsActive)
        {
            return _repository
                   .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public Designation GetDesignationById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
