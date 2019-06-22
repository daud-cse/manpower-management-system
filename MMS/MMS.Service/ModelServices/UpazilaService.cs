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
   
    public interface IUpazilaService
    {
        Upazila Find(params object[] keyValues);
        void Insert(Upazila entity);
        void Update(Upazila entity);
        void Delete(object id);
        void Delete(Upazila entity);
        IEnumerable<Upazila> GetAllUpazila();
        IEnumerable<Upazila> GetActiveUpazila(bool IsActive);
        Upazila GetUpazilaById(int Id);
        Upazila GetUpazilaByDistrictID(int districtId);

    }
    public class UpazilaService : Service<Upazila>, IUpazilaService
    {
        private readonly IRepositoryAsync<Upazila> _repository;

        public UpazilaService(

              IRepositoryAsync<Upazila> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Upazila> GetAllUpazila()
        {
            return _repository.Query().Select();
        }
        public Upazila GetUpazilaByDistrictID(int districtId)
        {
            Upazila objUpazila = new Upazila();
            var lstUpazila = new List<KeyValuePair<int, string>>();
           _repository.Query(x => x.DistrictID == districtId).Select().ToList().ForEach(delegate(Upazila item)
            {
                lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
           objUpazila.kvpUpazila = lstUpazila;
           return objUpazila;
        }
        public IEnumerable<Upazila> GetActiveUpazila(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public Upazila GetUpazilaById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
