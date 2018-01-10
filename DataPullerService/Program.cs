using System.ServiceProcess;

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
