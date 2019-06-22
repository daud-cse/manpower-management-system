using Microsoft.Ajax.Utilities;
using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using MMS.Service.ModelServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MMS.API.API
{
    public class ApplicantController : ApiController
    {

        private readonly IApplicantService _applicantService;
        private readonly IStoredProcedures _storedProcedures;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ApplicantController(IApplicantService applicantService, IUnitOfWorkAsync unitOfWork
            , IStoredProcedures storedProcedures)
        {
            _applicantService = applicantService;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Applicant GetApplicant()
        {
            var applicant = _applicantService.GetApplicantsByIdForApi("44");
            return applicant;
            // IEnumerable<Applicant> applicant = null;
            // return applicant;
        }
        //[HttpGet]
        //public IHttpActionResult Get(string Applicantinfo)
        //{


        //    //var objApplicant = _applicantService.GetApplicantsByIdForApi(Applicantinfo);
        //    var applicantList = _storedProcedures.ApplicantFind(Applicantinfo);
        //    if (applicantList.Count == 0)
        //    {
        //        var message = string.Format("Applicant with id = {0} not found", Applicantinfo);
        //        HttpError err = new HttpError(message);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //    }
        //    else
        //    {
        //        //var objJson = new
        //        //{
        //        //    applicantInfo = new[] {
        //        //     new {ApplicantName = objApplicant.Name,AgentName=objApplicant.Agent.Name,RefApplicantId=objApplicant.PreRefApplicantId, PassportNo =objApplicant.PassportNo , CurrentLocation = objApplicant.Location.Name,NextLocation=objApplicant.Location1.Name}

        //        //}
        //        //};
               
        //        string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(applicantList);
        //       //string jsonResult= JSON.stringify(JSON. p arse(applicantList));
        //       // JObject.Parse(jsonResult).ToString(Formatting.Indented);

        //        return Request.CreateResponse(HttpStatusCode.OK, jsonResult);


        //    }

        //}
    }
}
