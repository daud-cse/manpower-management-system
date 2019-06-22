using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MMS.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            MMSSERVICE objMMSSERVICE = new MMSSERVICE();
           objMMSSERVICE.debug();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new MMSSERVICE() 
            };
            ServiceBase.Run(ServicesToRun);
          
        }
    }
}
