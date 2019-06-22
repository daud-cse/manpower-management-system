using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Utility
{
  
  public enum SubsidyType
  {
      CustomerType = 1
      ,
      AgentType = 2
      ,
      EmployeeType = 3
      ,
      CompanyType=4
      ,
      ApplicantType=5
      ,
      VendorType=6
      ,
      BankType=7

  };
  public enum UIType
  {
      CashPayment = 1
      ,
      CashReceived = 2
      ,
      BankPayment = 3
      ,
      BankReceived = 4
      ,        
      Journal=5
      ,
      Contra=6
    
  };
}
