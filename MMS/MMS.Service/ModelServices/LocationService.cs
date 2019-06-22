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
  
    public interface ILocationService
    {
        Location Find(params object[] keyValues);
        void Insert(Location entity);
        void Update(Location entity);
        void Delete(object id);
        void Delete(Location entity);
        IEnumerable<Location> GetAllLocation(int GlobalCompanyId);
        IEnumerable<Location> GetActiveLocation(int GlobalCompanyId,bool IsActive);

        List<KeyValuePair<int,string>> GetKVPActiveLocation(int GlobalCompanyId, bool IsActive);
        Location GetLocationById(int Id, int GlobalCompanyId);

    }
    public class LocationService : Service<Location>, ILocationService
    {
        private readonly IRepositoryAsync<Location> _repository;

        public LocationService(

              IRepositoryAsync<Location> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Location> GetAllLocation(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<Location> GetActiveLocation(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x=>x.GlobalCompanyId==GlobalCompanyId)
                   .Select();
        }

        public Location GetLocationById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }
        public List<KeyValuePair<int, string>> GetKVPActiveLocation(int GlobalCompanyId, bool IsActive)
        {
            var lstkvpLocation = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId && x.IsActive == IsActive).Select().ToList().ForEach(delegate(Location item)
            {

                lstkvpLocation.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            return lstkvpLocation;
        }
    }
}
