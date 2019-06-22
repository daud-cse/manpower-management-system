using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{

    public interface IFMS_TransactionVMService
    {
        FMS_TransactionVM GetFMS_TransactionVM();
        FMS_TransactionVM SaveFMS_TransactionVM(int GlobalCompanyId, FMS_TransactionVM objFMS_TransactionVM, IUnitOfWork _unitOfWork, bool IsMultipleCashPayRec, string UserId);
        bool DeleteTransactionDet(int Id);
        FMS_TransactionVM UpdateFMS_TransactionVM(int GlobalCompanyId, FMS_TransactionVM objFMS_TransactionVM, IUnitOfWork _unitOfWork, bool IsMultipleCashPayRec, string UserId);
        FMS_TransactionVM newFMS_TransactionVM(int GlobalCompanyId, int voucherTypeId);
        FMS_TransactionVM GetTransactionVMByTransactionId(int TransactionId, int GlobalCompanyId);
    }
    public class FMS_TransactionVMService : IFMS_TransactionVMService
    {
        private readonly IFMS_TransactionService _fMS_TransactionService;
        private readonly IFMS_TransactionDetService _fMS_TransactionDetService;
        private readonly IFMS_SubsidyAccountService _fMS_SubsidyAccountService;
        private readonly IFMS_VoucherTypeService _fMS_VoucherTypeService;
        private readonly IFMS_PaymentReceivedTypeService _fMS_PaymentReceivedTypeService;
        private readonly IFMS_GLAccountService _iFMS_GLAccountService;
        private readonly IFMS_VoucherTypeWiseGLMapService _fMS_VoucherTypeWiseGLMapService;
        private readonly IFMS_VoucherTypeWiseOppositeGLMapService _fMS_VoucherTypeWiseOppositeGLMapService;
        private readonly IFMS_GLSubsidyTypeMapService _fMS_GLSubsidyTypeMapService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;
        private readonly IFMS_DayOpenCloseService _iFMS_DayOpenCloseService;
        private readonly IStoredProcedures _iStoredProcedures;

        private readonly IStoredProcedures _storedProcedures;
        public FMS_TransactionVMService(IFMS_TransactionService fMS_TransactionService
            , IFMS_TransactionDetService fMS_TransactionDetService
            , IFMS_VoucherTypeService fMS_VoucherTypeService
            , IFMS_GLAccountService iFMS_GLAccountService
            , IFMS_PaymentReceivedTypeService fMS_PaymentReceivedTypeService
            , IFMS_VoucherTypeWiseGLMapService fMS_VoucherTypeWiseGLMapService
            , IFMS_SubsidyAccountService fMS_SubsidyAccountService
            , IFMS_VoucherTypeWiseOppositeGLMapService fMS_VoucherTypeWiseOppositeGLMapService
            , IStoredProcedures storedProcedures
            , IFMS_GLSubsidyTypeMapService fMS_GLSubsidyTypeMapService
            , IFMS_SettingsService iFMS_SettingsService
            , IFMS_DayOpenCloseService iFMS_DayOpenCloseService
            , IStoredProcedures iStoredProcedures
            )
        {
            _fMS_TransactionService = fMS_TransactionService;
            _iStoredProcedures = iStoredProcedures;
            _fMS_TransactionDetService = fMS_TransactionDetService;
            _fMS_VoucherTypeService = fMS_VoucherTypeService;
            _fMS_PaymentReceivedTypeService = fMS_PaymentReceivedTypeService;
            _fMS_VoucherTypeWiseGLMapService = fMS_VoucherTypeWiseGLMapService;
            _iFMS_GLAccountService = iFMS_GLAccountService;
            _fMS_SubsidyAccountService = fMS_SubsidyAccountService;
            _fMS_VoucherTypeWiseOppositeGLMapService = fMS_VoucherTypeWiseOppositeGLMapService;
            _storedProcedures = storedProcedures;
            _fMS_GLSubsidyTypeMapService = fMS_GLSubsidyTypeMapService;
            _iFMS_DayOpenCloseService = iFMS_DayOpenCloseService;
            _iFMS_SettingsService = iFMS_SettingsService;
        }

        public FMS_TransactionVM GetTransactionVMByTransactionId(int TransactionId, int GlobalCompanyId)
        {
            var objTransactionVM = new FMS_TransactionVM();
            objTransactionVM.Transaction = new FMS_Transaction();
            objTransactionVM.TransactionDetList = new List<FMS_TransactionDet>();
            objTransactionVM.lstSubsidyAccount = new List<FMS_SubsidyAccount>();
            objTransactionVM.Transaction = _fMS_TransactionService.GetFMS_TransactionById(TransactionId,GlobalCompanyId);
            objTransactionVM.TransactionDetList = objTransactionVM.Transaction.FMS_TransactionDet.OrderByDescending(x => x.TransactionSLNo).ToList();

            //For Multiple Entry
            if (objTransactionVM.Transaction.IsMultipleOrSingle)
            {
                if (objTransactionVM.Transaction.VoucherTypeId == 5)
                {
                    objTransactionVM.TransactionDet2 = new FMS_TransactionDet();
                    objTransactionVM.TransactionDet2.kvpSubsidyType = new List<KeyValuePair<int, string>>();
                    int i = 0;
                    objTransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                    {

                        objTransactionVM.TransactionDetList[i].FMS_SubSidyVM = new FMS_SubSidyVM();
                        if (item.SubsidyTypeId == null || item.SubsidyTypeId == 0)
                        {
                            i++;
                        }
                        else
                        {
                            //Get Subsidy Information From SP--[FMS_SubsidyByCriteria]  
                            //Calling Part of SP
                            ///ELSE IF @SubsidyTypeId=2 and @SubsidyAccountId<>0---It's Call from Transaction UI only for single Agent 
                            var subsidyAccount = _storedProcedures.FMS_SubsidyByCriteria(GlobalCompanyId,item.SubsidyTypeId, item.SubsidyAccountId, "");
                            //if (subsidyAccount.Any())
                            //{
                            objTransactionVM.TransactionDetList[i].FMS_SubSidyVM = subsidyAccount.FirstOrDefault();
                            i++;
                            //}

                        }

                    });
                }
                else
                {
                    objTransactionVM.TransactionDet1 = objTransactionVM.TransactionDetList.Find(x => x.TransactionSLNo == 1 && x.TransactionId == objTransactionVM.Transaction.TransactionId);
                    objTransactionVM.TransactionDetList.RemoveAll(x => x.TransactionSLNo == 1 && x.TransactionId == objTransactionVM.Transaction.TransactionId);

                    objTransactionVM.TransactionDet2 = new FMS_TransactionDet();
                    objTransactionVM.TransactionDet2.kvpSubsidyType = new List<KeyValuePair<int, string>>();
                    int i = 0;
                    objTransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                    {

                        objTransactionVM.TransactionDetList[i].FMS_SubSidyVM = new FMS_SubSidyVM();
                        if (item.SubsidyTypeId == null || item.SubsidyTypeId == 0)
                        {
                            i++;
                        }
                        else
                        {
                            //Get Subsidy Information From SP--[FMS_SubsidyByCriteria]  
                            //Calling Part of SP
                            ///ELSE IF @SubsidyTypeId=2 and @SubsidyAccountId<>0---It's Call from Transaction UI only for single Agent 
                            var subsidyAccount = _storedProcedures.FMS_SubsidyByCriteria(GlobalCompanyId,item.SubsidyTypeId, item.SubsidyAccountId, "");

                            //if (subsidyAccount.Any())
                            //{
                            objTransactionVM.TransactionDetList[i].FMS_SubSidyVM = subsidyAccount.FirstOrDefault();
                            i++;
                            //}

                        }

                    });
                }
                return objTransactionVM;
            }
            else//For Single Entry
            {

                if (objTransactionVM.TransactionDetList.Count > 0)
                {
                    //For Tranaction SL NO one
                    objTransactionVM.TransactionDet1 = objTransactionVM.TransactionDetList.Find(x => x.TransactionSLNo == 1 && x.TransactionId == objTransactionVM.Transaction.TransactionId);


                    if (objTransactionVM.TransactionDet1.SubsidyTypeId == null || objTransactionVM.TransactionDet1.SubsidyTypeId == 0)
                    {
                        objTransactionVM.TransactionDet1.FMS_SubSidyVM = new FMS_SubSidyVM();
                    }
                    else
                    {
                        var subsidyAccount = _storedProcedures.FMS_SubsidyByCriteria(GlobalCompanyId, objTransactionVM.TransactionDet1.SubsidyTypeId, objTransactionVM.TransactionDet1.SubsidyAccountId, "");
                        objTransactionVM.TransactionDet1.FMS_SubSidyVM = subsidyAccount.FirstOrDefault();
                    }

                    //For Tranaction SL NO Two

                    objTransactionVM.TransactionDet2 = objTransactionVM.TransactionDetList.Find(x => x.TransactionSLNo == 2 && x.TransactionId == objTransactionVM.Transaction.TransactionId);
                    objTransactionVM.TransactionDet2.kvpSubsidyType = new List<KeyValuePair<int, string>>();

                    if (objTransactionVM.TransactionDet2.SubsidyTypeId == null || objTransactionVM.TransactionDet2.SubsidyTypeId == 0)
                    {
                        objTransactionVM.TransactionDet2.FMS_SubSidyVM = new FMS_SubSidyVM();
                    }
                    else
                    {
                        var subsidyAccount1 = _storedProcedures.FMS_SubsidyByCriteria(GlobalCompanyId,objTransactionVM.TransactionDet2.SubsidyTypeId, objTransactionVM.TransactionDet2.SubsidyAccountId, "");
                        objTransactionVM.TransactionDet2.kvpSubsidyType = _fMS_GLSubsidyTypeMapService.GetGLSubsidyTypeMap(GlobalCompanyId,objTransactionVM.TransactionDet2.GLAccountId);
                        objTransactionVM.TransactionDet2.FMS_SubSidyVM = subsidyAccount1.FirstOrDefault();
                    }


                }
            }
            return objTransactionVM;
        }
        public bool DeleteTransactionDet(int id)
        {

            try
            {
                _fMS_TransactionDetService.Delete(id);
                return true;
            }
            catch (Exception exe)
            {
                return false;
            }
        }
        public FMS_TransactionVM newFMS_TransactionVM(int GlobalCompanyId, int voucherTypeId)
        {
            FMS_TransactionVM objFMS_TransactionVM = new FMS_TransactionVM();
            objFMS_TransactionVM.Transaction = new FMS_Transaction();
            objFMS_TransactionVM.Transaction.FMS_VoucherType = new FMS_VoucherType();
            objFMS_TransactionVM.TransactionDetList = new List<FMS_TransactionDet>();
            objFMS_TransactionVM.TransactionDet1 = new FMS_TransactionDet();
            objFMS_TransactionVM.TransactionDet1.FMS_SubSidyVM = new FMS_SubSidyVM();
            objFMS_TransactionVM.TransactionDet2 = new FMS_TransactionDet();
            objFMS_TransactionVM.TransactionDet2.FMS_SubSidyVM = new FMS_SubSidyVM();
            objFMS_TransactionVM.VoucherTypeWiseGLMap = new FMS_VoucherTypeWiseGLMap();
            objFMS_TransactionVM.VoucherTypeWiseGLMap.FMS_GLAccount = new FMS_GLAccount();
            objFMS_TransactionVM.VoucherTypeWiseGLMap.FMS_VoucherType = new FMS_VoucherType();

            objFMS_TransactionVM.TransactionDet2.kvpSubsidyType = new List<KeyValuePair<int, string>>();

            var voucherTypeWiseGLMap = _fMS_VoucherTypeWiseGLMapService.GetFMSMapGLByVoucherTypeId(GlobalCompanyId, voucherTypeId);
            if (voucherTypeWiseGLMap != null)
            {
                objFMS_TransactionVM.VoucherTypeWiseGLMap = voucherTypeWiseGLMap;
                objFMS_TransactionVM.TransactionDet1.GLAccountId = voucherTypeWiseGLMap.GLAccountId;

                var GLSubsidyTypeMap = _fMS_GLSubsidyTypeMapService.GetGLSubsidyTypeMapByGLAccountId(GlobalCompanyId,voucherTypeWiseGLMap.GLAccountId);

                if (GLSubsidyTypeMap != null)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyTypeId = GLSubsidyTypeMap.SubsidyTypeId;
                }
            }
            var lstVoucherType = new List<KeyValuePair<int, string>>();

            objFMS_TransactionVM.Transaction.FMS_VoucherType = _fMS_VoucherTypeService.Find(voucherTypeId);

            _fMS_VoucherTypeService.GetActiveFMS_VoucherType(true).ToList().ForEach(delegate(FMS_VoucherType item)
            {
                lstVoucherType.Add(new KeyValuePair<int, string>(item.VoucherTypeId, item.VoucherTypeName));
            });
            var lstPaymentReceivedType = new List<KeyValuePair<int, string>>();
            _fMS_PaymentReceivedTypeService.GetActiveFMS_PaymentReceivedType(true).ToList().ForEach(delegate(FMS_PaymentReceivedType item)
            {
                lstPaymentReceivedType.Add(new KeyValuePair<int, string>(item.PRTypeId, item.PRTypeName));
            });
            var lstFMS_GLAccount = new List<KeyValuePair<int, string>>();

            if (voucherTypeId == 5)//For Journal Voucher 
            {
                _iFMS_GLAccountService.GetFMS_GLAccountList( GlobalCompanyId,false).ToList().ForEach(delegate(FMS_GLAccount item)
                {
                    lstFMS_GLAccount.Add(new KeyValuePair<int, string>(item.GLAccountId, item.GLAccountName));
                });

            }
            else
            {
                _fMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(voucherTypeId, true,GlobalCompanyId).ToList().ForEach(delegate(FMS_VoucherTypeWiseOppositeGLMap item)
                {
                    lstFMS_GLAccount.Add(new KeyValuePair<int, string>(item.GLAccountId, item.FMS_GLAccount.GLAccountName));
                });

            }
            var settings = _iFMS_SettingsService.GetFMS_Settings(GlobalCompanyId);
            var transactionDate = settings.CurrentDate == null ? null : settings.CurrentDate.ToString();
            objFMS_TransactionVM.Transaction.TransactionDate = Convert.ToDateTime(transactionDate);
            objFMS_TransactionVM.Transaction.ValueDate = Convert.ToDateTime(transactionDate);
            //_iFMS_GLAccountService.GetFMS_GLAccountList(3, true).ToList().ForEach(delegate(FMS_GLAccount item)
            //{
            //    lstFMS_GLAccount.Add(new KeyValuePair<string, string>(item.GLAccountId, item.GLAccountName));
            //});
            //if (voucherTypeId == 1 || voucherTypeId==2 || voucherTypeId==3 || voucherTypeId==4)
            //{
            //    if (voucherTypeWiseGLMap != null)
            //    {
            //        lstFMS_GLAccount.RemoveAll(x=>x.Key ==voucherTypeWiseGLMap.GLAccountId);
            //    }
            //}
            objFMS_TransactionVM.TransactionDet2.kvpGLAccount = lstFMS_GLAccount;
            objFMS_TransactionVM.Transaction.kvpPRType = lstPaymentReceivedType;
            objFMS_TransactionVM.Transaction.kvpVoucherType = lstVoucherType;

            return objFMS_TransactionVM;
        }
        public FMS_TransactionVM GetFMS_TransactionVM()
        {

            FMS_TransactionVM objFMS_TransactionVM = new FMS_TransactionVM();

            return objFMS_TransactionVM;
        }
        public FMS_TransactionVM SaveFMS_TransactionVM(int GlobalCompanyId, FMS_TransactionVM objFMS_TransactionVM, IUnitOfWork _unitOfWork, bool IsMultipleCashPayRec, string UserId)
        {

            objFMS_TransactionVM.Transaction.SetBy = UserId;
            objFMS_TransactionVM.Transaction.SetDate = DateTime.Now;
            objFMS_TransactionVM.Transaction.ValueDate = objFMS_TransactionVM.Transaction.TransactionDate;
            objFMS_TransactionVM.Transaction.IsAuto = true;
            objFMS_TransactionVM.Transaction.IsPosted = false;           
            objFMS_TransactionVM.Transaction.GlobalCompanyId = GlobalCompanyId;
            objFMS_TransactionVM.Transaction.ActionType = "Insert";
            var lstTransactionDet = new List<FMS_TransactionDet>();

            var autoTransactionId=_iStoredProcedures.GetAutoTransactionId(GlobalCompanyId, objFMS_TransactionVM.Transaction.VoucherTypeId);
            objFMS_TransactionVM.Transaction.VoucherId = autoTransactionId;
            //Cash Payment
            if (objFMS_TransactionVM.Transaction.VoucherTypeId == 1 && IsMultipleCashPayRec == false)//CashPayment
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment               
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
                //if (objFMS_TransactionVM.Transaction.TransactionId < 10)
                //{
                //    objFMS_TransactionVM.Transaction.VoucherId = "CP0" + objFMS_TransactionVM.Transaction.TransactionId;
                //}
                //else
                //{
                //    objFMS_TransactionVM.Transaction.VoucherId = "CP" + objFMS_TransactionVM.Transaction.TransactionId;
                //}
                //_fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                //_unitOfWork.SaveChanges();

                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objTransactionDet);
                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                //   objFMS_TransactionVM.TransactionDet2.DrCrType=1;//2 for Debit
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            //CashPayment Multiple
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 1 && IsMultipleCashPayRec == true)//CashPayment Multiple
            {

                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();             
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {
                    item.SetDate = DateTime.Now;
                    item.SetBy = UserId;
                    item.GlobalCompanyId = GlobalCompanyId;
                    item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });

                return objFMS_TransactionVM;
            }

            //CashReceived
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 2 && IsMultipleCashPayRec == false)////CashReceived
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();               
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);
                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            //CashReceived Multiple
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 2 && IsMultipleCashPayRec == true)//CashReceived Multiple
            {


                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
               
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {
                    item.SetDate = DateTime.Now;
                    item.SetBy = UserId;
                    item.GlobalCompanyId = GlobalCompanyId;
                    item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });

                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 3)//BankPayment
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
              
                var objTransactionDet = new FMS_TransactionDet();

                //Bank Account
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                //objTransactionDet.Particulars = objFMS_TransactionVM.TransactionDet1.Particulars;
                objTransactionDet.SubsidyAccountId = objFMS_TransactionVM.TransactionDet1.SubsidyAccountId;
                objTransactionDet.SubsidyTypeId = objFMS_TransactionVM.TransactionDet1.SubsidyTypeId;
                objTransactionDet.ChequeNo = objFMS_TransactionVM.TransactionDet1.ChequeNo;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                //objTransactionDet.DrCrType=2;//1 for Credit
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                //  objTransactionDet.ActionType = "Insert";
                lstTransactionDet.Add(objTransactionDet);

                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                // objFMS_TransactionVM.TransactionDet2.DrCrType=1;//2 for Debit
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.ChequeNo = null;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 4)//BankReceived
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();               

                var objTransactionDet = new FMS_TransactionDet();

                //Bank Account
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objTransactionDet.SubsidyAccountId = objFMS_TransactionVM.TransactionDet1.SubsidyAccountId;
                objTransactionDet.SubsidyTypeId = objFMS_TransactionVM.TransactionDet1.SubsidyTypeId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objTransactionDet);

                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.ChequeNo = null;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 5)//Journal
            {

                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
               
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {
                    item.SetDate = DateTime.Now;
                    item.SetBy = UserId;
                    item.GlobalCompanyId = GlobalCompanyId;
                    item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });

                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 6)//Contra
            {
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 7 && IsMultipleCashPayRec == false)//CashPayment Against A/P 
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
                
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);

                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 8 && IsMultipleCashPayRec == false)//Cash Received Accounts Receiable
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
                
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);


                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 9 && IsMultipleCashPayRec == false)//Bank Payment Against A/P 
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
                
                var objTransactionDet = new FMS_TransactionDet();

                //Bank Account
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                //objTransactionDet.Particulars = objFMS_TransactionVM.TransactionDet1.Particulars;
                objTransactionDet.SubsidyAccountId = objFMS_TransactionVM.TransactionDet1.SubsidyAccountId;
                objTransactionDet.SubsidyTypeId = objFMS_TransactionVM.TransactionDet1.SubsidyTypeId;
                objTransactionDet.ChequeNo = objFMS_TransactionVM.TransactionDet1.ChequeNo;
                //objTransactionDet.DrCrType=2;//1 for Credit
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                //  objTransactionDet.ActionType = "Insert";
                lstTransactionDet.Add(objTransactionDet);

                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                // objFMS_TransactionVM.TransactionDet2.DrCrType=1;//2 for Debit
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.ChequeNo = null;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                // objFMS_TransactionVM.TransactionDet2.ActionType = "Insert";
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 10 && IsMultipleCashPayRec == false)//Bank Received Against A/P 
            {

                objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
               

                var objTransactionDet = new FMS_TransactionDet();

                //Bank Account
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objTransactionDet.SubsidyAccountId = objFMS_TransactionVM.TransactionDet1.SubsidyAccountId;
                objTransactionDet.SubsidyTypeId = objFMS_TransactionVM.TransactionDet1.SubsidyTypeId;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);

                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.ChequeNo = null;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 11 && IsMultipleCashPayRec == false)//Purcahse Against A/P 
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
                

                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);


                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 12 && IsMultipleCashPayRec == false)//sales Accounts Receiable
            {
                objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Insert(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();
               
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Insert
                objTransactionDet.GLAccountId = objFMS_TransactionVM.TransactionDet1.GLAccountId;
                objTransactionDet.TransactionSLNo = 1;
                objTransactionDet.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objTransactionDet.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                objTransactionDet.SetDate = DateTime.Now;
                objTransactionDet.SetBy = UserId;
                lstTransactionDet.Add(objTransactionDet);


                ///Transaction Acocunt Data Insert
                objFMS_TransactionVM.TransactionDet2.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                objFMS_TransactionVM.TransactionDet2.TransactionSLNo = 2;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Insert(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            return objFMS_TransactionVM;
        }
        public FMS_TransactionVM UpdateFMS_TransactionVM(int GlobalCompanyId, FMS_TransactionVM objFMS_TransactionVM, IUnitOfWork _unitOfWork, bool IsMultipleCashPayRec, string UserId)
        {
            objFMS_TransactionVM.Transaction.SetBy = UserId;
            objFMS_TransactionVM.Transaction.SetDate = DateTime.Now;
            objFMS_TransactionVM.Transaction.ValueDate = objFMS_TransactionVM.Transaction.TransactionDate;
            objFMS_TransactionVM.Transaction.GlobalCompanyId = GlobalCompanyId;
            objFMS_TransactionVM.Transaction.ActionType = "Update";
            var lstTransactionDet = new List<FMS_TransactionDet>();
            //Cash Payment

            if (objFMS_TransactionVM.Transaction.VoucherTypeId == 1 && IsMultipleCashPayRec == false)//CashPayment
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);


                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);
                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            // // //CashPayment Multiple
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 1 && IsMultipleCashPayRec == true)// // //CashPayment Multiple
            {
                bool IsSucess = false;

                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                var TransactionDetListFromDataBase = _fMS_TransactionDetService.GetTransactionDet(objFMS_TransactionVM.Transaction.TransactionId);
                if (TransactionDetListFromDataBase.ToList().Count >= objFMS_TransactionVM.TransactionDetList.Count)
                {
                    TransactionDetListFromDataBase.ToList().ForEach(delegate(FMS_TransactionDet item)
                    {
                        var transactionDet = objFMS_TransactionVM.TransactionDetList.Find(x => x.TransactionDetId == item.TransactionDetId);
                        if (transactionDet == null)
                        {
                            _fMS_TransactionDetService.Delete(item.TransactionDetId);

                        }
                    });
                }
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {
                    
                    if (item.TransactionDetId > 0)
                    {                        
                        var objTransactionDet = _fMS_TransactionDetService.Find(item.TransactionDetId);
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.TransactionId = item.TransactionId;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.SubsidyAccountId = item.SubsidyAccountId;
                        objTransactionDet.SubsidyTypeId = item.SubsidyTypeId;
                        objTransactionDet.GLAccountId = item.GLAccountId;
                        objTransactionDet.DrAmount = item.DrAmount;
                        objTransactionDet.CrAmount = item.CrAmount;
                        objTransactionDet.Particulars = item.Particulars;
                        objTransactionDet.ChequeNo = item.ChequeNo;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                        objTransactionDet.SetDate = DateTime.Now;
                        objTransactionDet.SetBy = UserId;
                        _fMS_TransactionDetService.Update(objTransactionDet);                        
                    }
                    else
                    {
                        item.SetDate = DateTime.Now;
                        item.SetBy = UserId;
                        item.GlobalCompanyId=GlobalCompanyId;
                        item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                        _fMS_TransactionDetService.Insert(item);                        
                    }

                });
                _unitOfWork.SaveChanges();
                return objFMS_TransactionVM;
            }
            //CashReceived
            if (objFMS_TransactionVM.Transaction.VoucherTypeId == 2 && IsMultipleCashPayRec == false)//CashReceived
            {
                // objFMS_TransactionVM.Transaction.PRTypeId = 2;//Received
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);


                ///Transaction Subsidy Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0 || objFMS_TransactionVM.TransactionDet2.SubsidyTypeId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                    objFMS_TransactionVM.TransactionDet2.SubsidyTypeId = null;
                }
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId= GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            //CashReceived Multiple
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 2 && IsMultipleCashPayRec == true)//CashReceived Multiple
            {
                bool IsSucess = false;

                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                var TransactionDetListFromDataBase = _fMS_TransactionDetService.GetTransactionDet(objFMS_TransactionVM.Transaction.TransactionId);
                if (TransactionDetListFromDataBase.ToList().Count >= objFMS_TransactionVM.TransactionDetList.Count)
                {
                    TransactionDetListFromDataBase.ToList().ForEach(delegate(FMS_TransactionDet item)
                    {
                        var transactionDet = objFMS_TransactionVM.TransactionDetList.Find(x => x.TransactionDetId == item.TransactionDetId);
                        if (transactionDet == null)
                        {
                            _fMS_TransactionDetService.Delete(item.TransactionDetId);

                        }
                    });
                }
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {

                    if (item.TransactionDetId > 0)
                    {
                        
                        var objTransactionDet = _fMS_TransactionDetService.Find(item.TransactionDetId);
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.TransactionId = item.TransactionId;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.SubsidyAccountId = item.SubsidyAccountId;
                        objTransactionDet.SubsidyTypeId = item.SubsidyTypeId;
                        objTransactionDet.GLAccountId = item.GLAccountId;
                        objTransactionDet.DrAmount = item.DrAmount;
                        objTransactionDet.CrAmount = item.CrAmount;
                        objTransactionDet.Particulars = item.Particulars;
                        objTransactionDet.ChequeNo = item.ChequeNo;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                        objTransactionDet.SetDate = DateTime.Now;
                        objTransactionDet.SetBy = UserId;
                        _fMS_TransactionDetService.Update(objTransactionDet);
                    }
                    else
                    {
                        item.SetDate = DateTime.Now;
                        item.SetBy = UserId;
                        item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                        _fMS_TransactionDetService.Insert(item);                        
                    }

                });
                _unitOfWork.SaveChanges();
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 3)//BankPayment
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);

                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 4)//BankReceived
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId =GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);

                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else  if (objFMS_TransactionVM.Transaction.VoucherTypeId == 7 && IsMultipleCashPayRec == false)//CashPayment Accounts Payable
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);


                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 9 && IsMultipleCashPayRec == false)//Bank Payment Accounts Payable
            {
                // objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;
                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);
                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;
                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 8 && IsMultipleCashPayRec == false)//Cash Received Accounts Receable
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);


                ///Transaction Subsidy Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0 || objFMS_TransactionVM.TransactionDet2.SubsidyTypeId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                    objFMS_TransactionVM.TransactionDet2.SubsidyTypeId = null;
                }
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId =GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 10 && IsMultipleCashPayRec == false)//Bank Received Accounts Receable
            {
                // objFMS_TransactionVM.Transaction.PRTypeId = 1;//Payment
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;                
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);
                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 11 && IsMultipleCashPayRec == false)////Purchase Against A/P
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.CrAmount = objFMS_TransactionVM.TransactionDet2.DrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);

                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 12 && IsMultipleCashPayRec == false)//Sales Against A/R
            {
                
                objFMS_TransactionVM.Transaction.TranactionAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                ///Transaction Details Object Bind
                var objTransactionDet = new FMS_TransactionDet();
                //Map Account Data Updae        
                if (objFMS_TransactionVM.TransactionDet1.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet1.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet1.DrAmount = objFMS_TransactionVM.TransactionDet2.CrAmount;
                objFMS_TransactionVM.TransactionDet1.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet1.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet1.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet1);

                ///Transaction Acocunt Data Update
                if (objFMS_TransactionVM.TransactionDet2.SubsidyAccountId == 0)
                {
                    objFMS_TransactionVM.TransactionDet2.SubsidyAccountId = null;
                }
                objFMS_TransactionVM.TransactionDet2.GlobalCompanyId = GlobalCompanyId;
                objFMS_TransactionVM.TransactionDet2.SetDate = DateTime.Now;
                objFMS_TransactionVM.TransactionDet2.SetBy = UserId;                
                lstTransactionDet.Add(objFMS_TransactionVM.TransactionDet2);

                lstTransactionDet.ForEach(delegate(FMS_TransactionDet item)
                {
                    _fMS_TransactionDetService.Update(item);
                    _unitOfWork.SaveChanges();
                });
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 5)//Journal
            {
                bool IsSucess = false;

                _fMS_TransactionService.Update(objFMS_TransactionVM.Transaction);
                _unitOfWork.SaveChanges();

                var TransactionDetListFromDataBase = _fMS_TransactionDetService.GetTransactionDet(objFMS_TransactionVM.Transaction.TransactionId);
                if (TransactionDetListFromDataBase.ToList().Count >= objFMS_TransactionVM.TransactionDetList.Count)
                {
                    TransactionDetListFromDataBase.ToList().ForEach(delegate(FMS_TransactionDet item)
                    {
                        var transactionDet = objFMS_TransactionVM.TransactionDetList.Find(x => x.TransactionDetId == item.TransactionDetId);
                        if (transactionDet == null)
                        {
                            _fMS_TransactionDetService.Delete(item.TransactionDetId);

                        }
                    });
                }
                objFMS_TransactionVM.TransactionDetList.ForEach(delegate(FMS_TransactionDet item)
                {

                    //item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;

                    if (item.TransactionDetId > 0)
                    {
                        
                        var objTransactionDet = _fMS_TransactionDetService.Find(item.TransactionDetId);
                        
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.TransactionId = item.TransactionId;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.SubsidyAccountId = item.SubsidyAccountId;
                        objTransactionDet.SubsidyTypeId = item.SubsidyTypeId;
                        objTransactionDet.GLAccountId = item.GLAccountId;
                        objTransactionDet.DrAmount = item.DrAmount;
                        objTransactionDet.CrAmount = item.CrAmount;
                        objTransactionDet.Particulars = item.Particulars;
                        objTransactionDet.ChequeNo = item.ChequeNo;
                        objTransactionDet.TransactionSLNo = item.TransactionSLNo;
                        objTransactionDet.TransactionDetId = item.TransactionDetId;
                        objTransactionDet.GlobalCompanyId = GlobalCompanyId;
                        objTransactionDet.SetDate = DateTime.Now;
                        objTransactionDet.SetBy = UserId;
                        _fMS_TransactionDetService.Update(objTransactionDet);
                        
                    }
                    else
                    {
                        item.SetDate = DateTime.Now;
                        item.SetBy = UserId;
                        item.TransactionId = objFMS_TransactionVM.Transaction.TransactionId;
                        _fMS_TransactionDetService.Insert(item);
                        
                    }

                });
                _unitOfWork.SaveChanges();
                return objFMS_TransactionVM;
            }
            else if (objFMS_TransactionVM.Transaction.VoucherTypeId == 6)//Contra
            {
                return objFMS_TransactionVM;
            }
           

            return objFMS_TransactionVM;
        }
    }
}
