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
   
    public interface IRoleService
    {
        Role Find(params object[] keyValues);
        void Insert(Role entity);
        void Update(Role entity);
        void Delete(object id);
        void Delete(Role entity);
        IEnumerable<Role> GetAllRole(int GlobalCompanyId);
        IEnumerable<Role> GetActiveRole(int GlobalCompanyId,bool IsActive);
        Role GetRoleById(int GlobalCompanyId,int Id);
        Role newRole();
        Role GetRoleByModuleId(int GlobalCompanyId,int moduleId);
        List<Role> GetRoleModuleId(int GlobalCompanyId,int moduleId);

    }
    public class RoleService : Service<Role>, IRoleService
    {
        private readonly IRepositoryAsync<Role> _repository;
        private readonly IModuleService _moduleService;
        public RoleService(

              IRepositoryAsync<Role> repository,
            IModuleService moduleService

            )
            : base(repository)
        {
            _repository = repository;
            _moduleService = moduleService;

        }
        public Role newRole()
        {

            var lstModule = new List<KeyValuePair<int, string>>();
            _moduleService.GetActiveModule(true).ToList().ForEach(delegate(Module item)
            {
                lstModule.Add(new KeyValuePair<int, string>(item.ModuleId, item.Name));
            });
            Role role = new Role();
            role.kvpModule=lstModule;
            return role;
        }
        public Role GetRoleByModuleId(int GlobalCompanyId,int moduleId)
        {
            Role objRole = new Role();
           
            var lstRole = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.ModuleId == moduleId && x.IsActive == true && x.GlobalCompanyId == GlobalCompanyId).Select().ToList().ForEach(delegate(Role item)
            {
                lstRole.Add(new KeyValuePair<int, string>(item.RoleId, item.RoleName));
            });
            objRole.kvpRole = lstRole;
            return objRole;
        }

        public List<Role> GetRoleModuleId(int GlobalCompanyId,int moduleId)
        {

            return _repository.Query(x => x.ModuleId == moduleId && x.GlobalCompanyId == GlobalCompanyId).Select().ToList();
             
        }
        public IEnumerable<Role> GetAllRole(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.Module).Select();
        }
        public IEnumerable<Role> GetActiveRole(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public Role GetRoleById(int GlobalCompanyId,int Id)
        {
            return _repository
                .Query(x => x.RoleId == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
