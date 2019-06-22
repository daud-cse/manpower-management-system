using MMS.Entities.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
    public interface IProcessVMService
    {
        string DayEndProcess();
        string MonthEndProcess();
        string YearEndProcess();
    }
    public class ProcessVMService : IProcessVMService
    {

        private readonly IStoredProcedures _storedProcedures;
        public ProcessVMService(IStoredProcedures storedProcedures)
        {
            _storedProcedures = storedProcedures;
        }
        public string DayEndProcess()
        {
            return "sfd";

        }
        public string MonthEndProcess()
        {
            return "sfd";

        }
        public string YearEndProcess()
        {
            return "sfd";

        }
    }
}
