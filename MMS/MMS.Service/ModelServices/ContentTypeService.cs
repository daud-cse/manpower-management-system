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
 
    public interface IContentTypeService
    {
        ContentType Find(params object[] keyValues);
        void Insert(ContentType entity);
        void Update(ContentType entity);
        void Delete(object id);
        void Delete(ContentType entity);
        IEnumerable<ContentType> GetAllContentType();
        IEnumerable<ContentType> GetActiveContentType(bool IsActive);
        ContentType GetContentTypeById(int Id);

    }
    public class ContentTypeService : Service<ContentType>, IContentTypeService
    {
        private readonly IRepositoryAsync<ContentType> _repository;

        public ContentTypeService(

              IRepositoryAsync<ContentType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ContentType> GetAllContentType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ContentType> GetActiveContentType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ContentType GetContentTypeById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
