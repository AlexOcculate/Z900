//using ActiveQueryBuilder.Core;
//using ActiveQueryBuilder.View.WinForms;

namespace DataStoreTest
{
   class Program
   {
      static void Main( string[ ] args )
      {
         Z900.DataModel.DataStoreCollection dsColl = Z900.DataModel.DataStoreCollection.LoadDataStoreCollection( );
         dsColl.SaveDataStoreCollection( );
      }
   }
}

/*

      private static void f( Z900.DataModel.DataStoreCollection dsColl )
      {
         for( int i = 0; i < dsColl.List.Count; i++ )
         {
            DataStore ds = dsColl.List[ i ];
            if( !ds.IsActive || !ds.IsToPullRemotely )
            {
               continue;
            }
            QueryBuilder qb = pull( ds );
            DumpAqbQb( qb, ds );
            DumpMetadataItem( qb, ds );
            
            // publish( qb, ds );
            // push( qb, ds );
            // baseline( qb, ds );
            // diff( qb, ds );
            // thesaurus( qb, ds );
            //  watchers( qb, ds );
            // log( qb, ds );
            
         }
      }

private QueryBuilder pull( ds )
{
   QueryBuilder qb;
   switch( (DataStore.SyntaxProviderEnum) ds.SyntaxProvider )
   {
      case DataStore.SyntaxProviderEnum.SQLITE:
         qb = HandleSQLite( ds );
         break;
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2014:
         qb = HandleMSSQL( ds );
         break;
      case DataStore.SyntaxProviderEnum.AUTO:
      case DataStore.SyntaxProviderEnum.GENERIC:
      case DataStore.SyntaxProviderEnum.ANSI_SQL_2003:
      case DataStore.SyntaxProviderEnum.ANSI_SQL_89:
      case DataStore.SyntaxProviderEnum.ANSI_SQL_92:
      case DataStore.SyntaxProviderEnum.FIREBIRD_1_0:
      case DataStore.SyntaxProviderEnum.FIREBIRD_1_5:
      case DataStore.SyntaxProviderEnum.FIREBIRD_2_0:
      case DataStore.SyntaxProviderEnum.FIREBIRD_2_5:
      case DataStore.SyntaxProviderEnum.IBM_DB2:
      case DataStore.SyntaxProviderEnum.IBM_INFORMIX_10:
      case DataStore.SyntaxProviderEnum.IBM_INFORMIX_8:
      case DataStore.SyntaxProviderEnum.IBM_INFORMIX_9:
      case DataStore.SyntaxProviderEnum.MS_ACCESS2000:
      case DataStore.SyntaxProviderEnum.MS_ACCESS2003:
      case DataStore.SyntaxProviderEnum.MS_ACCESS97:
      case DataStore.SyntaxProviderEnum.MS_ACCESSXP:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2000:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2005:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2008:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2012:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_7:
      case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_COMPACT_EDITION:
      case DataStore.SyntaxProviderEnum.MYSQL_3_XX:
      case DataStore.SyntaxProviderEnum.MYSQL_4_0:
      case DataStore.SyntaxProviderEnum.MYSQL_4_1:
      case DataStore.SyntaxProviderEnum.MYSQL_5_0:
      case DataStore.SyntaxProviderEnum.ORACLE_10:
      case DataStore.SyntaxProviderEnum.ORACLE_11:
      case DataStore.SyntaxProviderEnum.ORACLE_7:
      case DataStore.SyntaxProviderEnum.ORACLE_8:
      case DataStore.SyntaxProviderEnum.ORACLE_9:
      case DataStore.SyntaxProviderEnum.POSTGRESQL:
      case DataStore.SyntaxProviderEnum.SYBASE_ASE:
      case DataStore.SyntaxProviderEnum.SYBASE_SQL_ANYWHERE:
      case DataStore.SyntaxProviderEnum.TERADATA:
      case DataStore.SyntaxProviderEnum.VISTADB:
      default:
         throw new Illegal( );
         return null;
   } // switch(…) …
}

private QueryBuilder HandleMSSQL( ds )
{
   QueryBuilder qb = CreateAqbQbMSSQL( ds );
   MetadataLoadingOptions loadingOptions = qb.SQLContext.MetadataContainer.LoadingOptions;
   loadingOptions.LoadDefaultDatabaseOnly = loadDefaultDatabaseOnly;
   loadingOptions.LoadSystemObjects = loadSystemObjects;
   //loadingOptions.IncludeFilter.Types = MetadataType.Field;
   //loadingOptions.ExcludeFilter.Schemas.Add(“dbo”);
   //qb.InitializeDatabaseSchemaTree();
   //qb.MetadataContainer.LoadAll(withFields);
   return qb;
}

public static QueryBuilder CreateAqbQbMSSQL( ds )
{
   QueryBuilder qb = new QueryBuilder( )
   {
      SyntaxProvider = new ActiveQueryBuilder.Core.MSSQLSyntaxProvider( ),
      MetadataProvider = new ActiveQueryBuilder.Core.MSSQLMetadataProvider( )
   };
   qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( connectionString );
   return qb;
}

private static void DumpAqbQb( QueryBuilder qb, DataStore ds )
{
   string TS_STR = @”D:\TEMP\SQLite\” +DataStore.TS_STR.Replace( “:”, “” );
   {
      // Export AQB’s Query Builder XML Structures…
      //string xmlStr = qb.MetadataContainer.XML;
      string filename = TS_STR + “.” +ds.AqbQbFilename;
      qb.MetadataContainer.ExportToXML( filename );
      // qb.MetadataContainer.ImportFromXML( filename );
   }
   tnm.ShowNotification( tnm.Notifications[ 0 ] );
}

private static void DumpMetadataItem( QueryBuilder qb, DataStore ds )
{
   string TS_STR = @”D:\TEMP\SQLite\” +DataStore.TS_STR.Replace( “:”, “” );
   {
      // Export MetadataItem FQN Collection…
      MetadataItemFQNCollection o = new MetadataItemFQNCollection( );
      o.List = ExtractMetadataValues.BuildBindingList( qb );
      string filename = TS_STR + “.” +ds.MiFqnFilename;
      o.Save( filename );
   }
   tnm.ShowNotification( tnm.Notifications[ 0 ] );
}
*/
////////////////////////////////////////////////////////////////
/*
         //{
         //   DevExpress.Xpo.Session session = DevExpress.Xpo.Session.DefaultSession;
         //   {
         //      Z900.DataModel.DataStore o = dsColl.NewDataStore( );
         //      o.ConnectionString = @"Data Source = D:\TEMP\SQLite\chinook\chinook.db;";
         //      o.IsActive = false;
         //      o.IsToPullRemotely = false;
         //      session.Save( o );
         //   }
         //   {
         //      Z900.DataModel.DataStore o = dsColl.NewDataStore( );
         //      o.ConnectionString = @"Data Source=DBSRV\QWERTY;Database=Sales;User Id=user02;Password=8a0IucJ@Nx1Qy5HfFrX0Ob3m;";
         //      o.IsActive = true;
         //      o.IsToPullRemotely = false;
         //      session.Save( o );
         //   }
         //   {
         //      Z900.DataModel.DataStore o = dsColl.NewDataStore( );
         //      o.ConnectionString = @"Data Source=D:\TEMP\SQLite\SQLITEDB1.db3;";
         //      o.IsActive = false;
         //      o.IsToPullRemotely = false;
         //      session.Save( o );
         //   }
         //}

*/
///////////////////////////////////////////////////////////////
/*
// string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
// string path = Z900.Global.DataStoreCollectionPathName;
// string file = Z900.Global.DataStoreCollectionSQLiteFileName;
// string connectionString = SQLiteConnectionProvider.GetConnectionString( Path.Combine( path, file ) );


      private static void DataStoreCollectionBootstrap()
      {
         System.Console.WriteLine( "Hello World!" );
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
            session.Save( dsColl );
            dsColl.Finish( );
         }
      }

*/
