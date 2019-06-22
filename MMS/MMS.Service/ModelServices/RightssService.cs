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

    public interface IRightssService
    {
        Rightss Find(params object[] keyValues);
        void Insert(Rightss entity);
        void Update(Rightss entity);
        void Delete(object id);
        void Delete(Rightss entity);
        IEnumerable<Rightss> GetAllRightss();
        IEnumerable<Rightss> GetActiveRightss(bool IsActive);
        Rightss GetRightssById(int Id);
        List<Rightss> GetRightsByModuleId(int ModuleId);
    }
    public class RightssService : Service<Rightss>, IRightssService
    {
        private readonly IRepositoryAsync<Rightss> _repository;

        public RightssService(

              IRepositoryAsync<Rightss> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Rightss> GetAllRightss()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<Rightss> GetActiveRightss(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public List<Rightss> GetRightsByModuleId(int ModuleId)
        {
            return _repository
                .Query(x => x.ModuleId == ModuleId)
                .Include(x=>x.Module)
                .Select().ToList();
        }
        public Rightss GetRightssById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
