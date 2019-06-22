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
   
    public interface IDivisionService
    {
        Division Find(params object[] keyValues);
        void Insert(Division entity);
        void Update(Division entity);
        void Delete(object id);
        void Delete(Division entity);
        IEnumerable<Division> GetAllDivision();
        IEnumerable<Division> GetActiveDivision(bool IsActive);
        Division GetDivisionById(int Id);

    }
    public class DivisionService : Service<Division>, IDivisionService
    {
        private readonly IRepositoryAsync<Division> _repository;

        public DivisionService(

              IRepositoryAsync<Division> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Division> GetAllDivision()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<Division> GetActiveDivision(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public Division GetDivisionById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
