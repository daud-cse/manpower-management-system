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
  
    public interface IAddressService
    {
        Address Find(params object[] keyValues);
        void Insert(Address entity);
        void Update(Address entity);
        void Delete(object id);
        void Delete(Address entity);
        IEnumerable<Address> GetAllAddress();
        IEnumerable<Address> GetActiveAddress(bool IsActive);
        Address GetAddressById(int Id);
        Address newAddress();
        // Address Add(Address project);
    }
    public class AddressService : Service<Address>, IAddressService
    {
        private readonly IRepositoryAsync<Address> _repository;

        public AddressService(

              IRepositoryAsync<Address> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Address> GetAllAddress()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<Address> GetActiveAddress(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public Address GetAddressById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }
        public Address newAddress()
        {
            var lstTypes = new List<KeyValuePair<int, string>>();
            //_iQuestionTypeServicee.GetAllQuestionType().ToList().ForEach(delegate(QuestionType item)
            //{
            //    lstTypes.Add(new KeyValuePair<int, string>(item.Id, item.Type));
            //});

            Address objAddress = new Address();
            // objAddress.kvpQuestionType = lstTypes;
            return objAddress;
        }
    }
}
