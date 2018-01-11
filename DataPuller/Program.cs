using System.Windows.Forms;

namespace Z900.DataPuller
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [System.STAThread]
      static void Main()
      {
         Application.EnableVisualStyles( );
         Application.SetCompatibleTextRenderingDefault( false );
         //Application.Run( new Form1( ) );
         Engine.Start( );
      }
   }
}
