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
   
    public interface ICustomerTypeService
    {
        CustomerType Find(params object[] keyValues);
        void Insert(CustomerType entity);
        void Update(CustomerType entity);
        void Delete(object id);
        void Delete(CustomerType entity);
        IEnumerable<CustomerType> GetAllCustomerType();
        IEnumerable<CustomerType> GetActiveCustomerType(bool IsActive);
        CustomerType GetCustomerTypeById(int Id);

    }
    public class CustomerTypeService : Service<CustomerType>, ICustomerTypeService
    {
        private readonly IRepositoryAsync<CustomerType> _repository;

        public CustomerTypeService(

              IRepositoryAsync<CustomerType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<CustomerType> GetAllCustomerType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<CustomerType> GetActiveCustomerType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public CustomerType GetCustomerTypeById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
