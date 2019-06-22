using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_GLAccount
    {
        public FMS_GLAccount()
        {
            this.FMS_GLAccount1 = new List<FMS_GLAccount>();
            this.FMS_GLSubsidyTypeMap = new List<FMS_GLSubsidyTypeMap>();
            this.FMS_GLWiseOpponentsGL = new List<FMS_GLWiseOpponentsGL>();
            this.FMS_GLWiseOpponentsGL1 = new List<FMS_GLWiseOpponentsGL>();
            this.FMS_TransactionDet = new List<FMS_TransactionDet>();
            this.FMS_VoucherTypeWiseOppositeGLMap = new List<FMS_VoucherTypeWiseOppositeGLMap>();
            this.FMS_VoucherTypeWiseGLMap = new List<FMS_VoucherTypeWiseGLMap>();
        }

        public int GLAccountId { get; set; }
        public string GLAccountCode { get; set; }
        public int GLAccountIdentifyId { get; set; }
        public string GLAccountName { get; set; }
        public string GLAccountTreeName { get; set; }
        public Nullable<int> ParentAccountId { get; set; }
        public string SubCtrlPrefix { get; set; }
        public int AccountTypeID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> OpeningDate { get; set; }
        public Nullable<decimal> LevelCode { get; set; }
        public Nullable<decimal> SortOrder { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public Nullable<decimal> CurrentYearOpeningBalance { get; set; }
        public bool IsSubsidyExist { get; set; }
        public bool HasChild { get; set; }
        public Nullable<int> ReportTypeID { get; set; }
        public string DrCr { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_AccountingReportType FMS_AccountingReportType { get; set; }
        public virtual ICollection<FMS_GLAccount> FMS_GLAccount1 { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount2 { get; set; }
        public virtual ICollection<FMS_GLSubsidyTypeMap> FMS_GLSubsidyTypeMap { get; set; }
        public virtual ICollection<FMS_GLWiseOpponentsGL> FMS_GLWiseOpponentsGL { get; set; }
        public virtual ICollection<FMS_GLWiseOpponentsGL> FMS_GLWiseOpponentsGL1 { get; set; }
        public virtual ICollection<FMS_TransactionDet> FMS_TransactionDet { get; set; }
        public virtual ICollection<FMS_VoucherTypeWiseOppositeGLMap> FMS_VoucherTypeWiseOppositeGLMap { get; set; }
        public virtual ICollection<FMS_VoucherTypeWiseGLMap> FMS_VoucherTypeWiseGLMap { get; set; }
    }
}
