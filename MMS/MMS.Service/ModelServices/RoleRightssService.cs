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
   
    public interface IRoleRightssService
    {
        RoleRightss Find(params object[] keyValues);
        void Insert(RoleRightss entity);
        void Update(RoleRightss entity);
        void Delete(object id);
        void Delete(RoleRightss entity);
        List<RoleRightss> GetAllRoleRightss(int moduleId,int roleid);
        IEnumerable<RoleRightss> GetActiveRoleRightss(bool IsActive);
        RoleRightss GetRoleRightssById(int Id);
        IEnumerable<RoleRightss> GetRightssRoleId(int RoleId);
        RoleRightss newRoleRightss();

    }
    public class RoleRightssService : Service<RoleRightss>, IRoleRightssService
    {
        private readonly IRepositoryAsync<RoleRightss> _repository;
        private readonly IModuleService _moduleService;
        private readonly IRightssService _rightssService;
      
        public RoleRightssService(

              IRepositoryAsync<RoleRightss> repository,
             IModuleService moduleService
            ,IRightssService rightssService
          


            )
            : base(repository)
        {
            _repository = repository;
            _rightssService = rightssService;
            _moduleService = moduleService;
          
        }


        public List<RoleRightss> GetAllRoleRightss(int moduleId,int roleid)
        {
            var rights=_rightssService.GetRightsByModuleId(moduleId);
            var rolerights=_repository.Query(x => x.Rightss.ModuleId == moduleId && x.RoleId==roleid).Include(x=>x.Role)
                .Include(x=>x.Rightss).Select();
            List<RoleRightss> lstRoleRights = new List<RoleRightss>();
            rights.ToList().ForEach(delegate(Rightss item)
            {

       
                var  objRoleRights= rolerights.ToList().Find(x=>x.RightId==item.ID);
                if (objRoleRights==null)
                {
                    objRoleRights = new RoleRightss();
                    objRoleRights.Rightss = new Rightss();
                    objRoleRights.Rightss.RightsName = item.RightsName; 
                    objRoleRights.RightId = item.ID;
                    objRoleRights.RoleId=roleid;
                }
                lstRoleRights.Add(objRoleRights);
            });
            return lstRoleRights;
        }
        public IEnumerable<RoleRightss> GetActiveRoleRightss(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        
        public RoleRightss newRoleRightss()
        {

            var lstModule = new List<KeyValuePair<int, string>>();
            _moduleService.GetActiveModule(true).ToList().ForEach(delegate(Module item)
            {
                lstModule.Add(new KeyValuePair<int, string>(item.ModuleId, item.Name));
            });

            RoleRightss roleRights = new RoleRightss();
            roleRights.kvpModule = lstModule;
            roleRights.kvpRole = new List<KeyValuePair<int, string>>();
            roleRights.rights = new List<Rightss>();
            return roleRights;
        }
        public RoleRightss GetRoleRightssById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }
        public IEnumerable<RoleRightss> GetRightssRoleId(int RoleId)
        {
            return _repository
                .Query(x => x.RoleId == RoleId && x.IsActive==true)
                .Include(x => x.Rightss)
                .Select();
        }

    }
}
