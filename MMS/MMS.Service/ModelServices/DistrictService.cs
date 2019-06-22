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

    public interface IDistrictService
    {
        District Find(params object[] keyValues);
        void Insert(District entity);
        void Update(District entity);
        void Delete(object id);
        void Delete(District entity);
        IEnumerable<District> GetAllDistrict();
        IEnumerable<District> GetActiveDistrict(bool IsActive);
        District GetDistrictByDivisionID(int divisionID);
        District GetDistrictById(int Id);

    }
    public class DistrictService : Service<District>, IDistrictService
    {
        private readonly IRepositoryAsync<District> _repository;

        public DistrictService(

              IRepositoryAsync<District> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<District> GetAllDistrict()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<District> GetActiveDistrict(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public District GetDistrictByDivisionID(int divisionID)
        {
            District objDistrict=new District();
           // var district= 
            var lstdistrict= new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.DivisionID == divisionID).Select().ToList().ForEach(delegate(District item)
            {
                lstdistrict.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            objDistrict.kvpDistrict = lstdistrict;
            return objDistrict;
        }
        public District GetDistrictById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
