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
   
    public interface INationalityService
    {
        Nationality Find(params object[] keyValues);
        void Insert(Nationality entity);
        void Update(Nationality entity);
        void Delete(object id);
        void Delete(Nationality entity);
        IEnumerable<Nationality> GetAllNationality();
        IEnumerable<Nationality> GetActiveNationality(bool IsActive);
        Nationality GetNationalityById(int Id);

    }
    public class NationalityService : Service<Nationality>, INationalityService
    {
        private readonly IRepositoryAsync<Nationality> _repository;

        public NationalityService(

              IRepositoryAsync<Nationality> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Nationality> GetAllNationality()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<Nationality> GetActiveNationality(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public Nationality GetNationalityById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
