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
  
  public interface IAgentTypeService
  {
      AgentType Find(params object[] keyValues);
      void Insert(AgentType entity);
      void Update(AgentType entity);
      void Delete(object id);
      void Delete(AgentType entity);
      IEnumerable<AgentType> GetAllAgentType();
      IEnumerable<AgentType> GetAllAgentType(int GlobalCompanyId);
      IEnumerable<AgentType> GetActiveAgentType(int GlobalCompanyId,bool IsActive);
      AgentType GetAgentTypeById(int Id, int GlobalCompanyId);
    
  }
  public class AgentTypeService : Service<AgentType>, IAgentTypeService
  {
      private readonly IRepositoryAsync<AgentType> _repository;
     
      public AgentTypeService(

            IRepositoryAsync<AgentType> repository

          )
          : base(repository)
      {
          _repository = repository;

      }
      public IEnumerable<AgentType> GetAllAgentType(int GlobalCompanyId)
      {
          return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
      }
      public IEnumerable<AgentType> GetAllAgentType()
      {
          return _repository.Query().Select();
      }
      public IEnumerable<AgentType> GetActiveAgentType(int GlobalCompanyId,bool IsActive)
      {
          return _repository
                 .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                 .Select();
      }

      public AgentType GetAgentTypeById(int Id, int GlobalCompanyId)
      {
          return _repository
              .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
              .Select().FirstOrDefault();
      }
     
  }
}
