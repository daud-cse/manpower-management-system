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


    public interface IUserService
    {
        User Find(params object[] keyValues);
        void Insert(User entity);
        void Update(User entity);
        void Delete(object id);
        void Delete(User entity);
        IEnumerable<User> GetAllUser(int GlobalCompanyId);
        IEnumerable<User> GetActiveUser(int GlobalCompanyId,bool IsActive);
        User GetUserById(int Id);
        User GetUserById(int GlobalCompanyId, string userId, string Password);

        User GetUserInfoByUserId(string userId, int GlobalCompanyId);
        User newUser(int GlobalCompanyId);
        User newUser(int GlobalCompanyId,int moduleId);
    }
    public class UserService : Service<User>, IUserService
    {
        private readonly IRepositoryAsync<User> _repository;
        private readonly IRoleService _iroleService;
        private readonly IModuleService _imoduleService;
        public UserService(

              IRepositoryAsync<User> repository
              , IRoleService iroleService
              , IModuleService imoduleService

            )
            : base(repository)
        {
            _repository = repository;
            _iroleService = iroleService;
            _imoduleService = imoduleService;

        }
        public User newUser(int GlobalCompanyId)
        {
            var lstRole = new List<KeyValuePair<int, string>>();
            _iroleService.GetActiveRole(GlobalCompanyId, true).ToList().ForEach(delegate(Role item)
            {
                lstRole.Add(new KeyValuePair<int, string>(item.RoleId, item.RoleName));
            });
            User user = new User();
            user.kvpRole = lstRole;
            user.kvpModule = _imoduleService.KvpGetActiveModule(true);
            return user;

        }
        public User newUser(int GlobalCompanyId,int moduleId)
        {

            var objRole = _iroleService.GetRoleByModuleId(GlobalCompanyId,moduleId);
            if (objRole == null)
            {
                objRole = new Role();
            }
            User user = new User();
            user.kvpRole = objRole.kvpRole;
            user.kvpModule = _imoduleService.KvpGetActiveModule(true);
            return user;
        }
        public IEnumerable<User> GetAllUser(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x => x.Role)
                .Include(x => x.Role.Module)
                .Include(x => x.UserModules)
                .Select();
        }
        public IEnumerable<User> GetActiveUser(int GlobalCompanyId, bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x => x.Role)
                   .Select();
        }
        public User GetUserById(int GlobalCompanyId, string userId, string Password)
        {
            var user = _repository
                   .Query(x => x.UserAccountsId == userId && x.Password == Password && x.IsActive == true && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x => x.Role)
                   .Include(x => x.Role.RoleRightsses)
                   .Include(x => x.UserModules)
                   .Include(x => x.GlobalCompany)
                   .Select().FirstOrDefault();
            return user;

        }
        public User GetUserById(int Id)
        {
            var user = _repository
                 .Query(x => x.ID == Id && x.IsActive == true)
                 .Include(x => x.Role)
                 .Include(x => x.Role.RoleRightsses)
                  .Include(x => x.GlobalCompany)
                 .Select().FirstOrDefault();
            return user;
        }
        public User GetOnlyUserInfo(int Id, int GlobalCompanyId)
        {
            var user = _repository
                 .Query(x => x.ID == Id && x.IsActive == true && x.GlobalCompanyId == GlobalCompanyId)
                 .Select().FirstOrDefault();
            return user;
        }
        public User GetUserInfoByUserId(string userId, int GlobalCompanyId)
        {
            var user = _repository
                .Query(x => x.UserAccountsId == userId && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
            return user;
        }
    }
}
