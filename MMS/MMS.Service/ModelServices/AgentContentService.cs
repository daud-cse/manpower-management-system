using MMS.Entities.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

   public interface IAgentContentService
   {
       AgentContent Find(params object[] keyValues);
       void Insert(AgentContent entity);
       void Update(AgentContent entity);
       void Delete(object id);
       void Delete(AgentContent entity);
       IEnumerable<AgentContent> GetAllAgentContent();
       IEnumerable<AgentContent> GetActiveAgentContent(bool IsActive);
     
       AgentContent GetAgentContentById(int Id);

   }
   public class AgentContentService : Service<AgentContent>, IAgentContentService
   {
       private readonly IRepositoryAsync<AgentContent> _repository;

       public AgentContentService(

             IRepositoryAsync<AgentContent> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<AgentContent> GetAllAgentContent()
       {
           return _repository.Query().Select();
       }
       public IEnumerable<AgentContent> GetActiveAgentContent(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }
      
       public AgentContent GetAgentContentById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Select().FirstOrDefault();
       }

   }
}
