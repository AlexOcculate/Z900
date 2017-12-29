using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.IO;

namespace DataStoreTest
{
   class Program
   {
      static void Main( string[ ] args )
      {
         Console.WriteLine( "Hello World!" );
         //string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
         //string path = Z900.Global.DataStoreCollectionPathName;
         //string file = Z900.Global.DataStoreCollectionSQLiteFileName;
         //string connectionString = SQLiteConnectionProvider.GetConnectionString( Path.Combine( path, file ) );
         //
         string tmpFilename = System.IO.Path.GetTempFileName( );
         string connectionString = SQLiteConnectionProvider.GetConnectionString( tmpFilename );
         //
         IDataLayer dataLayer = XpoDefault.GetDataLayer( connectionString, AutoCreateOption.DatabaseAndSchema );
         XpoDefault.DataLayer = dataLayer;
         Session session = Session.DefaultSession;
         {
            Z900.DataModel.DataStoreCollection dsColl = new Z900.DataModel.DataStoreCollection( );
            dsColl.TempFileFullPathName = tmpFilename;
            session.Save( dsColl );
            {
               Z900.DataModel.DataStore o = dsColl.NewDataStore( );
               session.Save( o );
            }
            {
               Z900.DataModel.DataStore o = dsColl.NewDataStore( );
               session.Save( o );
            }
            {
               Z900.DataModel.DataStore o = dsColl.NewDataStore( );
               session.Save( o );
            }
            {
               Z900.DataModel.DataStore o = dsColl.NewDataStore( );
               session.Save( o );
            }
            dsColl.Finishing( );
            //string tsStr = Z900.Global.TS_STR;
            //string sqliteFn = Path.Combine( path, tsStr + "." + Z900.Global.DataStoreCollectionSQLiteFileName );
            //string xmlFn = Path.Combine( path, tsStr + "." + Z900.Global.DataStoreCollectionXmlFileName );
            //dsColl.SQLiteFileFullPathName = sqliteFn;
            //dsColl.FinishTS = System.DateTime.Now;
            session.Save( dsColl );
            //File.Copy( dsColl.TempFileFullPathName, dsColl.SQLiteFileFullPathName, true );
            //dsColl.Save( dsColl.XmlFileFullPathName );
            dsColl.Finish( );
         }
      }
   }
}
