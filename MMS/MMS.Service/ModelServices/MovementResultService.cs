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
   
    public interface IMovementResultService
    {
        MovementResult Find(params object[] keyValues);
        void Insert(MovementResult entity);
        void Update(MovementResult entity);
        void Delete(object id);
        void Delete(MovementResult entity);
        IEnumerable<MovementResult> GetAllMovementResult();
        IEnumerable<MovementResult> GetActiveMovementResult(bool IsActive);
        MovementResult GetMovementResultById(int Id);

    }
    public class MovementResultService : Service<MovementResult>, IMovementResultService
    {
        private readonly IRepositoryAsync<MovementResult> _repository;

        public MovementResultService(

              IRepositoryAsync<MovementResult> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<MovementResult> GetAllMovementResult()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MovementResult> GetActiveMovementResult(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public MovementResult GetMovementResultById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
