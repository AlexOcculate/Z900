namespace Z900.DataPuller
{
   // Must be int between 128 and 255
   // Numbers 0-127 are reserved for windows, and are handled in the base class.
   // 128 to 255 are there for your own use.
   public enum CustomCommand
   {
      LogIt = 255
   };
   //
   public partial class DataPullerService : System.ServiceProcess.ServiceBase
   {
      public const string SERVICE_NAME_PREFIX = "DataPuller";
      public const string SERVICE_LOGGER_NAME = SERVICE_NAME_PREFIX + "Logger";
      public const string SERVICE_LOGGER_SOURCE_NAME = SERVICE_LOGGER_NAME + "Service";
      public const int SERVICE_TIMER_INTERVAL = (60*60*1000);

      private System.Timers.Timer _timer = null;

      public DataPullerService()
      {
         InitializeComponent( );
         // Set the timer to fire every sixty seconds
         // (remember the timer is in millisecond resolution,
         //  so 1000 = 1 second. )
         this._timer = new System.Timers.Timer( SERVICE_TIMER_INTERVAL );
         // Now tell the timer when the timer fires
         // (the Elapsed event) call the _timer_Elapsed
         // method in our code
         this._timer.Elapsed += new System.Timers.ElapsedEventHandler( this._timer_Elapsed );
      }

      #region --- Write To Log Methdos ---
      private void WriteToLog( string msg, System.Diagnostics.EventLogEntryType type = System.Diagnostics.EventLogEntryType.Information )
      {
         System.Diagnostics.EventLog evt = new System.Diagnostics.EventLog( SERVICE_LOGGER_NAME );
         string message = msg
            + ": "
            + System.DateTime.Now.ToShortDateString( )
            + " "
            + System.DateTime.Now.ToLongTimeString( )
            ;
         evt.Source = SERVICE_LOGGER_SOURCE_NAME;
         evt.WriteEntry( message, type );
      }
      private void li( string msg )
      {
         WriteToLog( msg, System.Diagnostics.EventLogEntryType.Information );
      }
      private void le( string msg )
      {
         WriteToLog( msg, System.Diagnostics.EventLogEntryType.Error );
      }
      private void laf( string msg )
      {
         WriteToLog( msg, System.Diagnostics.EventLogEntryType.FailureAudit );
      }
      private void las( string msg )
      {
         WriteToLog( msg, System.Diagnostics.EventLogEntryType.SuccessAudit );
      }
      private void lw( string msg )
      {
         WriteToLog( msg, System.Diagnostics.EventLogEntryType.Warning );
      }
      #endregion

      #region --- Service Events ---
      protected override void OnCustomCommand( int command )
      {
         base.OnCustomCommand( command );
         if( command == (int) CustomCommand.LogIt )
         {
            WriteToLog( SERVICE_NAME_PREFIX + CustomCommand.LogIt.ToString( ) + ": [START] " );
            try
            {
               Z900.DataPuller.Engine.Start( );
            }
            catch( System.Exception ex )
            {

            }
            WriteToLog( SERVICE_NAME_PREFIX + CustomCommand.LogIt.ToString( ) + ": [  END]" );
         }
      }

      protected override void OnStart( string[ ] args )
      {
         this._timer.Start( );
         WriteToLog( SERVICE_NAME_PREFIX + " Start" );
      }

      protected override void OnStop()
      {
         this._timer.Stop( );
         WriteToLog( SERVICE_NAME_PREFIX + " Stop" );
      }

      protected override void OnContinue()
      {
         base.OnContinue( );
         this._timer.Start( );
      }

      protected override void OnPause()
      {
         base.OnPause( );
         this._timer.Stop( );
      }

      protected override void OnShutdown()
      {
         base.OnShutdown( );
         this._timer.Stop( );
      }
      #endregion

      // This method is called when the timer fires
      // it’s elapsed event. It will write the time
      // to the event log.
      protected void _timer_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
      {
         WriteToLog( SERVICE_NAME_PREFIX + " Timer" );
      }
   }
}
