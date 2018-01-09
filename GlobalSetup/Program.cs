using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlobalSetup
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         //Application.EnableVisualStyles( );
         //Application.SetCompatibleTextRenderingDefault( false );
         //Application.Run( new Form1( ) );
         //
         //
         //System.Configuration.Configuration cm = System.Configuration.ConfigurationManager.OpenExeConfiguration( System.Configuration.ConfigurationUserLevel.None );
         //System.Configuration.KeyValueConfigurationCollection confCollection = cm.AppSettings.Settings;
         ////
         //var value = confCollection[ "TMPDIR" ].Value;
         //var value1 = confCollection[ "COLLDIR" ].Value;
         ////
         //confCollection[ "TMPDIR" ].Value = System.IO.Path.GetTempFileName( );
         //confCollection[ "COLLDIR" ].Value = System.IO.Path.GetTempFileName( ); 
         ////
         //cm.Save( System.Configuration.ConfigurationSaveMode.Modified );
         //System.Configuration.ConfigurationManager.RefreshSection( cm.AppSettings.SectionInformation.Name );
         //
         //
         var tempPathName = Z900.Global.TempPathName;
         var dataStoreCollectionXmlFilePathName = Z900.Global.DataStoreCollectionPathName;
      }
   }
}
