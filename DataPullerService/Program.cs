using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Z900.DataPuller

{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main()
      {
         ServiceBase[ ] ServicesToRun;
         ServicesToRun = new ServiceBase[ ]
         {
                new DataPullerService()
         };
         ServiceBase.Run( ServicesToRun );
      }
   }
}
