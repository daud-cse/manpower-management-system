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

    public interface ILocationMapDetailService
    {
        LocationMapDetail Find(params object[] keyValues);
        void Insert(LocationMapDetail entity);
        void Update(LocationMapDetail entity);
        void Delete(object id);
        void Delete(LocationMapDetail entity);
        IEnumerable<LocationMapDetail> GetAllLocationMapDetail(int GlobalCompanyId);
        IEnumerable<LocationMapDetail> GetActiveLocationMapDetail(int GlobalCompanyId, bool IsActive);
        LocationMapDetail GetLocationMapDetailById(int Id, int GlobalCompanyId);
        IEnumerable<LocationMapDetail> GetLocationMapDetailByLocationId(int GlobalCompanyId, int locationId);
        LocationMapDetail newLocationMapDetail(int GlobalCompanyId);
        LocationMapDetail LocationMapDetail(int GlobalCompanyId, int locationId,int controlTypeId);

    }
    public class LocationMapDetailService : Service<LocationMapDetail>, ILocationMapDetailService
    {
        private readonly IRepositoryAsync<LocationMapDetail> _repository;
        private readonly ILocationService _locationService;
        private readonly IControlTypeService _controlTypeService;
        public LocationMapDetailService(

              IRepositoryAsync<LocationMapDetail> repository
            , ILocationService locationService
            , IControlTypeService controlTypeService

            )
            : base(repository)
        {
            _locationService = locationService;
            _repository = repository;
            _controlTypeService = controlTypeService;

        }
        public IEnumerable<LocationMapDetail> GetAllLocationMapDetail(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.ControlType)
                .Include(x => x.Location).Select();
        }
        public IEnumerable<LocationMapDetail> GetActiveLocationMapDetail(int GlobalCompanyId, bool IsActive)
        {
            return _repository
                   .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public IEnumerable<LocationMapDetail> GetLocationMapDetailByLocationId(int GlobalCompanyId, int locationId)
        {
            return _repository
                   .Query(x => x.LocationId == locationId && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public LocationMapDetail GetLocationMapDetailById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }
        public LocationMapDetail newLocationMapDetail(int GlobalCompanyId)
        {
            var objLocationMapDetail = new LocationMapDetail();
            objLocationMapDetail.kvpLocation = _locationService.GetKVPActiveLocation(GlobalCompanyId,true);
            objLocationMapDetail.kvpControlType = _controlTypeService.GetKVPActiveControlType(true);
            return objLocationMapDetail;

        }
        public LocationMapDetail LocationMapDetail(int GlobalCompanyId, int locationId, int controlTypeId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId && x.LocationId == locationId && x.ControlTypeId == controlTypeId).Select().FirstOrDefault();
        }
    }
}
