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
  
    public interface IContentDetailService
    {
        ContentDetail Find(params object[] keyValues);
        void Insert(ContentDetail entity);
        void Update(ContentDetail entity);
        void Delete(object id);
        void Delete(ContentDetail entity);
        IEnumerable<ContentDetail> GetAllContentDetail();
        IEnumerable<ContentDetail> GetActiveContentDetail(bool IsActive);
        ContentDetail GetContentDetailById(int Id);
        ContentDetail GetContentDetailByContentId(int ContentId);
        ContentDetail GetContentDefaultImage(int typeId);
        List<ContentDetail> GetContentDetailListByContentId(int ContentId);
    }
    public class ContentDetailService : Service<ContentDetail>, IContentDetailService
    {
        private readonly IRepositoryAsync<ContentDetail> _repository;

        public ContentDetailService(

              IRepositoryAsync<ContentDetail> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ContentDetail> GetAllContentDetail()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ContentDetail> GetActiveContentDetail(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public ContentDetail GetContentDetailByContentId(int ContentId)
        {
            return _repository
                .Query(x => x.ContentId == ContentId)
                .Select().FirstOrDefault();
        }
        public List<ContentDetail> GetContentDetailListByContentId(int ContentId)
        {
            return _repository
                .Query(x => x.ContentId == ContentId)
                .Select().ToList();
        }
        public ContentDetail GetContentDetailById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }
       public ContentDetail GetContentDefaultImage(int typeId)
        {
            return _repository
               .Query(x => x.ContentTypeID == typeId)
               .Select().FirstOrDefault();
        }

    }
}
