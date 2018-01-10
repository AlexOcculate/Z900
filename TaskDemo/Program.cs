using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskDemo
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main( string[ ] args )
      {
         Application.EnableVisualStyles( );
         Application.SetCompatibleTextRenderingDefault( false );
         //Application.Run( new Form1( ) );
         ProcessJustFirst( args );
      }
      ////////////////////////////////////////////
      private static string ForumTemplate = "http://social.msdn.microsoft.com/Forums/en-US/{0}/threads?outputas=xml";

      static void ProcessJustFirst( string[ ] args )
      {
         string[ ] forums = System.IO.File.ReadAllLines( Environment.CurrentDirectory + @"\forums.txt" );
         Task[ ] tasks = new Task[ 1 ];
         DateTime Start = DateTime.Now;
         tasks[ 0 ] = Task.Factory.StartNew( () => DoSomeWork( forums[ 0 ] ), TaskCreationOptions.LongRunning );
         //
         var status = tasks[ 0 ].Status;
         //
         Task.WaitAll( tasks );
         DateTime end = DateTime.Now;
         TimeSpan elapsed = end - Start;
         string totalMs = elapsed.TotalMilliseconds.ToString( );
         Console.WriteLine( "DONE in " + totalMs + " ms. Any Key to quit." );
      }

      static void ProcessEverything( string[ ] args )
      {
         string[ ] forums = System.IO.File.ReadAllLines( Environment.CurrentDirectory + @"\forums.txt" );
         Task[ ] tasks = new Task[ forums.Length ];
         int ctr = 0;
         DateTime Start = DateTime.Now;
         foreach( string s in forums )
         {
            object state = s;
            var task = Task.Factory.StartNew( () => DoSomeWork( state ), TaskCreationOptions.LongRunning );
            tasks[ ctr ] = task;
            ctr++;
         }

         Task.WaitAll( tasks );
         DateTime end = DateTime.Now;
         TimeSpan elapsed = end - Start;
         string totalMs = elapsed.TotalMilliseconds.ToString( );
         Console.WriteLine( "DONE in " + totalMs + " ms. Any Key to quit." );
         Console.ReadKey( );
      }


      static void DoSomeWork( object state )
      {
         string forumShortName = (string) state;
         string url = string.Format( ForumTemplate, forumShortName );
         System.Net.WebClient wc = new System.Net.WebClient( );
         try
         {
            wc.DownloadFile( url, Environment.CurrentDirectory + @"\" + forumShortName + ".xml" );
         }
         catch
         {
            // we probably timed out here so, nada!
         }
         finally
         {
            wc.Dispose( );
         }
         Console.WriteLine( "saved: " + forumShortName );
      }
   }
}
