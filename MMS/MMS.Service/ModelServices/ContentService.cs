using MMS.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MMS.Service.ModelServices
{
   
   public interface IContentService
   {
       Content Find(params object[] keyValues);
       void Insert(Content entity);
       void Update(Content entity);
       void Delete(object id);
       void Delete(Content entity);
       IEnumerable<Content> GetAllContent();
       IEnumerable<Content> GetActiveContent(bool IsActive);
       Content GetContentById(int Id);
       Content SaveContent(IEnumerable<HttpPostedFileBase> uploadAgentImage, IUnitOfWork _unitOfWork,int GlobalCompanyId, string UserId);
       Content UpdateContent(IEnumerable<HttpPostedFileBase> fileUpload, IUnitOfWork _unitOfWork,int GlobalCompanyId, int? contentId);

   }
   public class ContentService : Service<Content>, IContentService
   {
       private readonly IRepositoryAsync<Content> _repository;
       private readonly IContentDetailService _iContentDetailService;
     
       public ContentService(

             IRepositoryAsync<Content> repository,
           IContentDetailService iContentDetailService

           )
           : base(repository)
       {
           _repository = repository;
           _iContentDetailService = iContentDetailService;

       }
       private byte[] ConvertsToBytes(HttpPostedFileBase image)
       {
           byte[] imageBytes = null;
           BinaryReader reader = new BinaryReader(image.InputStream);
           imageBytes = reader.ReadBytes((int)image.ContentLength);
           return imageBytes;
       }
       public Content SaveContent(IEnumerable<HttpPostedFileBase> fileUpload, IUnitOfWork _unitOfWork,int GlobalCompanyId, string UserId)
       {
           Content objContent = new Content();
           try
           {
               objContent.GlobalCompanyId = GlobalCompanyId;
               _repository.Insert(objContent);
               _unitOfWork.SaveChanges();
               foreach (var file in fileUpload)
               {
                   ContentDetail objContentDetails = new ContentDetail();
                   var imagebyte = ConvertsToBytes(file);
                   objContentDetails.ContentId = objContent.ID;
                   objContentDetails.GlobalCompanyId = GlobalCompanyId;
                   objContentDetails.Object = imagebyte;
                   objContentDetails.FileName = file.FileName;
                   objContentDetails.FileExtension = file.ContentType;
                   objContentDetails.ContentTypeID = 1;//1 for Image
                   objContentDetails.Name = file.FileName;
                   _iContentDetailService.Insert(objContentDetails);
                   _unitOfWork.SaveChanges();
               }
           }
           catch { 
           };
           return objContent;

       }
       public Content UpdateContent(IEnumerable<HttpPostedFileBase> fileUpload, IUnitOfWork _unitOfWork,int GlobalCompanyId,int? contentId)
       {
           Content objContent = new Content();
          
           try
           {
               objContent.GlobalCompanyId = GlobalCompanyId;
               int contentIdNotNull = contentId == null ? 0 : Convert.ToInt32(contentId);
             
               var contentDetaillist = _iContentDetailService.GetContentDetailListByContentId(contentIdNotNull);
               if (contentDetaillist.ToList().Count > 0)
               {
                   objContent.ID = contentIdNotNull;
                   var contentDetail = _iContentDetailService.GetContentDetailByContentId(contentIdNotNull);
                   foreach (var file in fileUpload)
                   {
                       ContentDetail objContentDetails = new ContentDetail();
                       var imagebyte = ConvertsToBytes(file);
                       objContentDetails.GlobalCompanyId = GlobalCompanyId;
                       contentDetail.Object = imagebyte;
                       contentDetail.FileName = file.FileName;
                       contentDetail.FileExtension = file.ContentType;
                       contentDetail.ContentTypeID = 1;//1 for Image
                       contentDetail.Name = file.FileName;
                       _iContentDetailService.Update(contentDetail);
                       _unitOfWork.SaveChanges();
                   }
               }
               else
               {
                   _repository.Insert(objContent);
                   _unitOfWork.SaveChanges();
                   foreach (var file in fileUpload)
                   {
                       ContentDetail objContentDetails = new ContentDetail();
                       var imagebyte = ConvertsToBytes(file);
                       objContentDetails.GlobalCompanyId = GlobalCompanyId;
                       objContentDetails.ContentId = objContent.ID;
                       objContentDetails.Object = imagebyte;
                       objContentDetails.FileName = file.FileName;
                       objContentDetails.FileExtension = file.ContentType;
                       objContentDetails.ContentTypeID = 1;//1 for Image
                       objContentDetails.Name = file.FileName;
                       _iContentDetailService.Insert(objContentDetails);
                       _unitOfWork.SaveChanges();
                   }
               }
              
           }
           catch
           {
           };
           return objContent;

       }
       public IEnumerable<Content> GetAllContent()
       {
           return _repository.Query().Select();
       }
       public IEnumerable<Content> GetActiveContent(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }

       public Content GetContentById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Select().FirstOrDefault();
       }

   }
}
