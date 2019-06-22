using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using Repository.Pattern.Repositories;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using MMS.Entities.Models;
using Repository.Pattern.Ef6;
using MMS.Entities.StoredProcedures;

namespace MMS.FMS.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IDataContextAsync, MPMSContext>(new PerRequestLifetimeManager())
                 .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                 .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                 
                 .RegisterType<IRepositoryAsync<Agent>, Repository<Agent>>()
                 .RegisterType<IAgentService, AgentService>()

                 .RegisterType<IRepositoryAsync<CompanyType>, Repository<CompanyType>>()
                 .RegisterType<ICompanyTypeService, CompanyTypeService>()

                 .RegisterType<IRepositoryAsync<ApplicantMovementResultHistory>, Repository<ApplicantMovementResultHistory>>()
                 .RegisterType<IApplicantMovementResultHistoryService, ApplicantMovementResultHistoryService>()


                 .RegisterType<IRepositoryAsync<AgentType>, Repository<AgentType>>()
                 .RegisterType<IAgentTypeService, AgentTypeService>()
                 .RegisterType<IRepositoryAsync<Country>, Repository<Country>>()
                 .RegisterType<ICountryService, CountryService>()
                 .RegisterType<IRepositoryAsync<Division>, Repository<Division>>()
                 .RegisterType<IDivisionService, DivisionService>()
                 .RegisterType<IRepositoryAsync<Upazila>, Repository<Upazila>>()
                 .RegisterType<IUpazilaService, UpazilaService>()
                 .RegisterType<IRepositoryAsync<District>, Repository<District>>()
                 .RegisterType<IDistrictService, DistrictService>()
                 .RegisterType<IRepositoryAsync<Nationality>, Repository<Nationality>>()
                 .RegisterType<INationalityService, NationalityService>()

                 .RegisterType<IRepositoryAsync<Content>, Repository<Content>>()
                 .RegisterType<IContentService, ContentService>()

                 .RegisterType<IRepositoryAsync<AgentContent>, Repository<AgentContent>>()
                 .RegisterType<IAgentContentService, AgentContentService>()


                 .RegisterType<IRepositoryAsync<Applicant>, Repository<Applicant>>()
                 .RegisterType<IApplicantService, ApplicantService>()


                 .RegisterType<IRepositoryAsync<Complain>, Repository<Complain>>()
                 .RegisterType<IComplainService, ComplainService>()

                 .RegisterType<IRepositoryAsync<ComplainType>, Repository<ComplainType>>()
                 .RegisterType<IComplainTypeService, ComplainTypeService>()


                 .RegisterType<IRepositoryAsync<Status>, Repository<Status>>()
                 .RegisterType<IStatusService, StatusService>()

                 .RegisterType<IRepositoryAsync<ApplicantType>, Repository<ApplicantType>>()
                 .RegisterType<IApplicantTypeService, ApplicantTypeService>()

                 .RegisterType<IRepositoryAsync<ApplicantFileLot>, Repository<ApplicantFileLot>>()
                 .RegisterType<IApplicantFileLotService, ApplicantFileLotService>()

                 .RegisterType<IRepositoryAsync<ApplicantCheckList>, Repository<ApplicantCheckList>>()
                 .RegisterType<IApplicantCheckListService, ApplicantCheckListService>()

                 .RegisterType<IRepositoryAsync<CheckListGroupMapping>, Repository<CheckListGroupMapping>>()
                 .RegisterType<ICheckListGroupMappingService, CheckListGroupMappingService>()

                 .RegisterType<IAgentVMService, AgentVMService>()
                 .RegisterType<IApplicantVMService, ApplicantVMService>()
                 .RegisterType<ILocationChangeVMService, LocationChangeVMService>()
                 .RegisterType<ISearchApplicantVMService, SearchApplicantVMService>()
                 .RegisterType<ISearchPassportInfoVMService, SearchPassportInfoVMService>()

                 .RegisterType<IFMS_TransactionVMService, FMS_TransactionVMService>()

                 .RegisterType<IDashboardVMService, DashboardVMService>()
                .RegisterType<IStoredProcedures, MPMSContext>()


                 .RegisterType<IRepositoryAsync<Content>, Repository<Content>>()
                 .RegisterType<IContentService, ContentService>()


                 .RegisterType<IRepositoryAsync<ContentDetail>, Repository<ContentDetail>>()
                 .RegisterType<IContentDetailService, ContentDetailService>()

                 .RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>()
                 .RegisterType<ICustomerService, CustomerService>()

                 .RegisterType<IRepositoryAsync<CompanyWiseApplicant>, Repository<CompanyWiseApplicant>>()
                 .RegisterType<ICompanyWiseApplicantService, CompanyWiseApplicantService>()


                 .RegisterType<IRepositoryAsync<CustomerType>, Repository<CustomerType>>()
                 .RegisterType<ICustomerTypeService, CustomerTypeService>()


                 .RegisterType<IRepositoryAsync<MaritalStatu>, Repository<MaritalStatu>>()
                 .RegisterType<IMaritalStatusService, MaritalStatuService>()

                   .RegisterType<IRepositoryAsync<User>, Repository<User>>()
                 .RegisterType<IUserService, UserService>()

                   .RegisterType<IRepositoryAsync<Role>, Repository<Role>>()
                 .RegisterType<IRoleService, RoleService>()

                 .RegisterType<IRepositoryAsync<PassportInfo>, Repository<PassportInfo>>()
                 .RegisterType<IPassportInfoService, PassportInfoService>()

                 .RegisterType<IRepositoryAsync<PassPortType>, Repository<PassPortType>>()
                 .RegisterType<IPassPortTypeService, PassPortTypeService>()

                 .RegisterType<IRepositoryAsync<RPOType>, Repository<RPOType>>()
                 .RegisterType<IRPOTypeService, RPOTypeService>()

                 .RegisterType<IRepositoryAsync<Sex>, Repository<Sex>>()
                 .RegisterType<ISexService, SexService>()

                 .RegisterType<IApplicantContentService, ApplicantContentService>()
                 .RegisterType<IRepositoryAsync<ApplicantContent>, Repository<ApplicantContent>>()

                 .RegisterType<ICompanyService, CompanyService>()
                 .RegisterType<IRepositoryAsync<Company>, Repository<Company>>()

                 .RegisterType<IContentService, ContentService>()
                 .RegisterType<IRepositoryAsync<Content>, Repository<Content>>()

                 .RegisterType<IContentTypeService, ContentTypeService>()

                 .RegisterType<IRepositoryAsync<ContentType>, Repository<ContentType>>()

                 .RegisterType<ILocationMapDetailService, LocationMapDetailService>()

                 .RegisterType<IRepositoryAsync<LocationMapDetail>, Repository<LocationMapDetail>>()

                 .RegisterType<ILocationService, LocationService>()

                 .RegisterType<IRepositoryAsync<Location>, Repository<Location>>()

                 .RegisterType<IMovementResultService, MovementResultService>()

                 .RegisterType<IRepositoryAsync<MovementResult>, Repository<MovementResult>>()

                 .RegisterType<IRightssService, RightssService>()

                 .RegisterType<IRepositoryAsync<Rightss>, Repository<Rightss>>()

                 .RegisterType<IRoleRightssService, RoleRightssService>()

                 .RegisterType<IRepositoryAsync<RoleRightss>, Repository<RoleRightss>>()

                  .RegisterType<IUserService, UserService>()

                 .RegisterType<IRepositoryAsync<User>, Repository<User>>()

                 .RegisterType<IApplicantLocationDetailService, ApplicantLocationDetailService>()

                 .RegisterType<IRepositoryAsync<ApplicantLocationDetail>, Repository<ApplicantLocationDetail>>()

                 .RegisterType<IApplicantMovementService, ApplicantMovementService>()

                 .RegisterType<IRepositoryAsync<ApplicantMovement>, Repository<ApplicantMovement>>()

                 ///
                   .RegisterType<IVendorTypeService, VendorTypeService>()
                 .RegisterType<IRepositoryAsync<VendorType>, Repository<VendorType>>()
                     .RegisterType<IVendorInfoService, VendorInfoService>()
                 .RegisterType<IRepositoryAsync<VendorInfo>, Repository<VendorInfo>>()
                   .RegisterType<IBankAccountTypeService, BankAccountTypeService>()
                 .RegisterType<IRepositoryAsync<BankAccountType>, Repository<BankAccountType>>()
                 ///FMS
                 ///

                    .RegisterType<IFMS_SubSidyVMService, FMS_SubSidyVMService>()

                      .RegisterType<IFMS_DayOpenCloseVMService, FMS_DayOpenCloseVMService>()


                        .RegisterType<IFMS_DayOpenCloseService, FMS_DayOpenCloseService>()
                 .RegisterType<IRepositoryAsync<FMS_DayOpenClose>, Repository<FMS_DayOpenClose>>()




                        .RegisterType<IFMS_DayWiseCheckListDetailsService, FMS_DayWiseCheckListDetailsService>()
                 .RegisterType<IRepositoryAsync<FMS_DayWiseCheckListDetails>, Repository<FMS_DayWiseCheckListDetails>>()

                 .RegisterType<IFMS_SettingsService, FMS_SettingsService>()
                 .RegisterType<IRepositoryAsync<FMS_Settings>, Repository<FMS_Settings>>()


                  .RegisterType<IBankAccountInfoService, BankAccountInfoService>()
                 .RegisterType<IRepositoryAsync<BankAccountInfo>, Repository<BankAccountInfo>>()

                 .RegisterType<IEmployeeInfoService, EmployeeInfoService>()
                 .RegisterType<IRepositoryAsync<EmployeeInfo>, Repository<EmployeeInfo>>()

                  .RegisterType<IDesignationService, DesignationService>()
                 .RegisterType<IRepositoryAsync<Designation>, Repository<Designation>>()

                 .RegisterType<IFMS_GLAccountService, FMS_GLAccountService>()
                 .RegisterType<IRepositoryAsync<FMS_GLAccount>, Repository<FMS_GLAccount>>()

                 .RegisterType<IFMS_GLSubsidyTypeMapService, FMS_GLSubsidyTypeMapService>()
                 .RegisterType<IRepositoryAsync<FMS_GLSubsidyTypeMap>, Repository<FMS_GLSubsidyTypeMap>>()

                 .RegisterType<IFMS_SubsidyTypeService, FMS_SubsidyTypeService>()
                 .RegisterType<IRepositoryAsync<FMS_SubsidyType>, Repository<FMS_SubsidyType>>()


                 .RegisterType<IFMS_TransactionDetService, FMS_TransactionDetService>()
                 .RegisterType<IRepositoryAsync<FMS_TransactionDet>, Repository<FMS_TransactionDet>>()

                 .RegisterType<IFMS_TransactionService, FMS_TransactionService>()
                 .RegisterType<IRepositoryAsync<FMS_Transaction>, Repository<FMS_Transaction>>()

                 .RegisterType<IFMS_VoucherTypeService, FMS_VoucherTypeService>()
                 .RegisterType<IRepositoryAsync<FMS_VoucherType>, Repository<FMS_VoucherType>>()

                 .RegisterType<IFMS_PaymentReceivedTypeService, FMS_PaymentReceivedTypeService>()
                 .RegisterType<IRepositoryAsync<FMS_PaymentReceivedType>, Repository<FMS_PaymentReceivedType>>()

                 .RegisterType<IFMS_SubsidyAccountService, FMS_SubsidyAccountService>()
                 .RegisterType<IRepositoryAsync<FMS_SubsidyAccount>, Repository<FMS_SubsidyAccount>>()

                 .RegisterType<IFMS_SettingsService, FMS_SettingsService>()
                 .RegisterType<IRepositoryAsync<FMS_Settings>, Repository<FMS_Settings>>()


                 .RegisterType<IFMS_DayOpenCloseService, FMS_DayOpenCloseService>()
                 .RegisterType<IRepositoryAsync<FMS_DayOpenClose>, Repository<FMS_DayOpenClose>>()

                 .RegisterType<IFMS_VoucherTypeWiseGLMapService, FMS_VoucherTypeWiseGLMapService>()
                 .RegisterType<IRepositoryAsync<FMS_VoucherTypeWiseGLMap>, Repository<FMS_VoucherTypeWiseGLMap>>()
                 .RegisterType<IFMS_VoucherTypeWiseOppositeGLMapService, FMS_VoucherTypeWiseOppositeGLMapService>()
                 .RegisterType<IRepositoryAsync<FMS_VoucherTypeWiseOppositeGLMap>, Repository<FMS_VoucherTypeWiseOppositeGLMap>>()

                   .RegisterType<IRepositoryAsync<Module>, Repository<Module>>()
                 .RegisterType<IModuleService, ModuleService>()

                  .RegisterType<IRepositoryAsync<UserModule>, Repository<UserModule>>()
                 .RegisterType<IUserModuleService, UserModuleService>()
                 ;

        }
    }
}
