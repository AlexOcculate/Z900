namespace Z900.DataPuller

{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      //[System.STAThread] // @#$%
      static void Main()
      {
         //System.Windows.Forms.Application.EnableVisualStyles( );
         //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault( false );
         //Application.Run( new DataPullerManagerForm( ) );
         //
         System.ServiceProcess.ServiceBase[ ] ServicesToRun;
         ServicesToRun = new System.ServiceProcess.ServiceBase[ ]
         {
                new DataPullerService()
         };
         System.ServiceProcess.ServiceBase.Run( ServicesToRun );
      }
   }
}

/*
http://mstechnoblogger.blogspot.com.au/2009/04/deleting-services-forcefully-from.html

Creating a Service:

sc.exe create PayCalcService binPath= "C:\Program Files\PaymentCalculation\paycalc.exe" DisplayName= "PaymentCalculationService"

Starting a Service:
sc.exe start PaymentCalculationService

Stopping a Service:
sc.exe stop PaymentCalculationService

Deleting a Service:
sc.exe delete PaymentCalculationService

Ref: https://stackoverflow.com/questions/225275/how-to-force-uninstallation-of-windows-service

One thing that has caught me out in the past is that if you have the services viewer running then that prevents the services from being fully deleted, so close that beforehand

You don't have to restart your machine. Start cmd or PowerShell in elevated mode.

sc.exe queryex <SERVICE_NAME>
taskkill /pid <SERVICE_PID> /f

sc delete <Service_Name>


SERVICE_NAME: DataPullerServiceInstaller
DISPLAY_NAME: DataPuller
        TYPE               : 10  WIN32_OWN_PROCESS
        STATE              : 4  RUNNING
                                (STOPPABLE, NOT_PAUSABLE, ACCEPTS_SHUTDOWN)
        WIN32_EXIT_CODE    : 0  (0x0)
        SERVICE_EXIT_CODE  : 0  (0x0)
        CHECKPOINT         : 0x0
        WAIT_HINT          : 0x0
        PID                : 2360
        FLAGS              :

SUCCESS: The process with PID 2360 has been terminated.


*/
