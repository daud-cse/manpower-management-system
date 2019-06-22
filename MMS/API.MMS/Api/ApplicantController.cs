using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MMS.Api
{
    public class ApplicantController : ApiController
    {
        private readonly IApplicantService _applicantService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ApplicantController(IApplicantService applicantService, IUnitOfWorkAsync unitOfWork)
        {
            _applicantService = applicantService;

            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Applicant> GetApplicant()
        {
              return _applicantService.GetAllApplicant();
           // IEnumerable<Applicant> applicant = null;
           // return applicant;
        }
    }
}
