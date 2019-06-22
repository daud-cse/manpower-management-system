using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MMS.Entities.Models.Mapping;
using Repository.Pattern.Ef6;

namespace MMS.Entities.Models
{
    public partial class MPMSContext : DataContext
    {
        static MPMSContext()
        {
            Database.SetInitializer<MPMSContext>(null);
        }

        public MPMSContext()
            : base("Name=MPMSContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentContent> AgentContents { get; set; }
        public DbSet<AgentType> AgentTypes { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantCheckList> ApplicantCheckLists { get; set; }
        public DbSet<ApplicantContent> ApplicantContents { get; set; }
        public DbSet<ApplicantFileLot> ApplicantFileLots { get; set; }
        public DbSet<ApplicantLocationDetail> ApplicantLocationDetails { get; set; }
        public DbSet<ApplicantLotStatu> ApplicantLotStatus { get; set; }
        public DbSet<ApplicantMovement> ApplicantMovements { get; set; }
        public DbSet<ApplicantMovementResultHistory> ApplicantMovementResultHistories { get; set; }
        public DbSet<ApplicantType> ApplicantTypes { get; set; }
        public DbSet<BankAccountInfo> BankAccountInfoes { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CheckListGroup> CheckListGroups { get; set; }
        public DbSet<CheckListGroupMapping> CheckListGroupMappings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContent> CompanyContents { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyWiseApplicant> CompanyWiseApplicants { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<ComplainType> ComplainTypes { get; set; }
        public DbSet<ConfigureEmailMessage> ConfigureEmailMessages { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentDetail> ContentDetails { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<ControlType> ControlTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<EmployeeInfo> EmployeeInfoes { get; set; }
        public DbSet<FMS_AccountingReportType> FMS_AccountingReportType { get; set; }
        public DbSet<FMS_CurrentLedgerSummary> FMS_CurrentLedgerSummary { get; set; }
        public DbSet<FMS_CurrentLedgerSummary_LastCopy> FMS_CurrentLedgerSummary_LastCopy { get; set; }
        public DbSet<FMS_DailyLedgerSummary> FMS_DailyLedgerSummary { get; set; }
        public DbSet<FMS_DailySubsidyBalanceTransactionWise> FMS_DailySubsidyBalanceTransactionWise { get; set; }
        public DbSet<FMS_DailySubsidyLedgerSummary> FMS_DailySubsidyLedgerSummary { get; set; }
        public DbSet<FMS_DayOpenClose> FMS_DayOpenClose { get; set; }
        public DbSet<FMS_DayOpenCloseCheckList> FMS_DayOpenCloseCheckList { get; set; }
        public DbSet<FMS_DayWiseCheckListDetails> FMS_DayWiseCheckListDetails { get; set; }
        public DbSet<FMS_GLAccount> FMS_GLAccount { get; set; }
        public DbSet<FMS_GLAccountMasterType> FMS_GLAccountMasterType { get; set; }
        public DbSet<FMS_GLAccountType> FMS_GLAccountType { get; set; }
        public DbSet<FMS_GLSubsidyTypeMap> FMS_GLSubsidyTypeMap { get; set; }
        public DbSet<FMS_GLWiseOpponentsGL> FMS_GLWiseOpponentsGL { get; set; }
        public DbSet<FMS_MonthlyLedgerSummary> FMS_MonthlyLedgerSummary { get; set; }
        public DbSet<FMS_PaymentReceivedType> FMS_PaymentReceivedType { get; set; }
        public DbSet<FMS_Settings> FMS_Settings { get; set; }
        public DbSet<FMS_SubsidyAccount> FMS_SubsidyAccount { get; set; }
        public DbSet<FMS_SubsidyType> FMS_SubsidyType { get; set; }
        public DbSet<FMS_Transaction> FMS_Transaction { get; set; }
        public DbSet<FMS_TransactionDet> FMS_TransactionDet { get; set; }
        public DbSet<FMS_VoucherType> FMS_VoucherType { get; set; }
        public DbSet<FMS_VoucherTypeWiseGLMap> FMS_VoucherTypeWiseGLMap { get; set; }
        public DbSet<FMS_VoucherTypeWiseOppositeGLMap> FMS_VoucherTypeWiseOppositeGLMap { get; set; }
        public DbSet<FMS_YearlyLedgerSummary> FMS_YearlyLedgerSummary { get; set; }
        public DbSet<GlobalCompany> GlobalCompanies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationMapDetail> LocationMapDetails { get; set; }
        public DbSet<MailInfo> MailInfoes { get; set; }
        public DbSet<MaritalStatu> MaritalStatus { get; set; }
        public DbSet<MessageAreaType> MessageAreaTypes { get; set; }
        public DbSet<MessageInfo> MessageInfoes { get; set; }
        public DbSet<MessageMailMaster> MessageMailMasters { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<MovementResult> MovementResults { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<PassportInfo> PassportInfoes { get; set; }
        public DbSet<PassPortType> PassPortTypes { get; set; }
        public DbSet<Rightss> Rightsses { get; set; }
        public DbSet<RoleRightss> RoleRightsses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RPOType> RPOTypes { get; set; }
        public DbSet<SendingAreaType> SendingAreaTypes { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        public DbSet<UserModule> UserModules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VendorInfo> VendorInfoes { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<SubsidySummary> SubsidySummaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new AgentMap());
            modelBuilder.Configurations.Add(new AgentContentMap());
            modelBuilder.Configurations.Add(new AgentTypeMap());
            modelBuilder.Configurations.Add(new ApplicantMap());
            modelBuilder.Configurations.Add(new ApplicantCheckListMap());
            modelBuilder.Configurations.Add(new ApplicantContentMap());
            modelBuilder.Configurations.Add(new ApplicantFileLotMap());
            modelBuilder.Configurations.Add(new ApplicantLocationDetailMap());
            modelBuilder.Configurations.Add(new ApplicantLotStatuMap());
            modelBuilder.Configurations.Add(new ApplicantMovementMap());
            modelBuilder.Configurations.Add(new ApplicantMovementResultHistoryMap());
            modelBuilder.Configurations.Add(new ApplicantTypeMap());
            modelBuilder.Configurations.Add(new BankAccountInfoMap());
            modelBuilder.Configurations.Add(new BankAccountTypeMap());
            modelBuilder.Configurations.Add(new CheckListMap());
            modelBuilder.Configurations.Add(new CheckListGroupMap());
            modelBuilder.Configurations.Add(new CheckListGroupMappingMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyContentMap());
            modelBuilder.Configurations.Add(new CompanyTypeMap());
            modelBuilder.Configurations.Add(new CompanyWiseApplicantMap());
            modelBuilder.Configurations.Add(new ComplainMap());
            modelBuilder.Configurations.Add(new ComplainTypeMap());
            modelBuilder.Configurations.Add(new ConfigureEmailMessageMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ContentDetailMap());
            modelBuilder.Configurations.Add(new ContentTypeMap());
            modelBuilder.Configurations.Add(new ControlTypeMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerTypeMap());
            modelBuilder.Configurations.Add(new DesignationMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new DivisionMap());
            modelBuilder.Configurations.Add(new EmployeeInfoMap());
            modelBuilder.Configurations.Add(new FMS_AccountingReportTypeMap());
            modelBuilder.Configurations.Add(new FMS_CurrentLedgerSummaryMap());
            modelBuilder.Configurations.Add(new FMS_CurrentLedgerSummary_LastCopyMap());
            modelBuilder.Configurations.Add(new FMS_DailyLedgerSummaryMap());
            modelBuilder.Configurations.Add(new FMS_DailySubsidyBalanceTransactionWiseMap());
            modelBuilder.Configurations.Add(new FMS_DailySubsidyLedgerSummaryMap());
            modelBuilder.Configurations.Add(new FMS_DayOpenCloseMap());
            modelBuilder.Configurations.Add(new FMS_DayOpenCloseCheckListMap());
            modelBuilder.Configurations.Add(new FMS_DayWiseCheckListDetailsMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountMasterTypeMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountTypeMap());
            modelBuilder.Configurations.Add(new FMS_GLSubsidyTypeMapMap());
            modelBuilder.Configurations.Add(new FMS_GLWiseOpponentsGLMap());
            modelBuilder.Configurations.Add(new FMS_MonthlyLedgerSummaryMap());
            modelBuilder.Configurations.Add(new FMS_PaymentReceivedTypeMap());
            modelBuilder.Configurations.Add(new FMS_SettingsMap());
            modelBuilder.Configurations.Add(new FMS_SubsidyAccountMap());
            modelBuilder.Configurations.Add(new FMS_SubsidyTypeMap());
            modelBuilder.Configurations.Add(new FMS_TransactionMap());
            modelBuilder.Configurations.Add(new FMS_TransactionDetMap());
            modelBuilder.Configurations.Add(new FMS_VoucherTypeMap());
            modelBuilder.Configurations.Add(new FMS_VoucherTypeWiseGLMapMap());
            modelBuilder.Configurations.Add(new FMS_VoucherTypeWiseOppositeGLMapMap());
            modelBuilder.Configurations.Add(new FMS_YearlyLedgerSummaryMap());
            modelBuilder.Configurations.Add(new GlobalCompanyMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new LocationMapDetailMap());
            modelBuilder.Configurations.Add(new MailInfoMap());
            modelBuilder.Configurations.Add(new MaritalStatuMap());
            modelBuilder.Configurations.Add(new MessageAreaTypeMap());
            modelBuilder.Configurations.Add(new MessageInfoMap());
            modelBuilder.Configurations.Add(new MessageMailMasterMap());
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new MovementResultMap());
            modelBuilder.Configurations.Add(new NationalityMap());
            modelBuilder.Configurations.Add(new PassportInfoMap());
            modelBuilder.Configurations.Add(new PassPortTypeMap());
            modelBuilder.Configurations.Add(new RightssMap());
            modelBuilder.Configurations.Add(new RoleRightssMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RPOTypeMap());
            modelBuilder.Configurations.Add(new SendingAreaTypeMap());
            modelBuilder.Configurations.Add(new SexMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UpazilaMap());
            modelBuilder.Configurations.Add(new UserModuleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new VendorInfoMap());
            modelBuilder.Configurations.Add(new VendorTypeMap());
            modelBuilder.Configurations.Add(new SubsidySummaryMap());
        }
    }
}
