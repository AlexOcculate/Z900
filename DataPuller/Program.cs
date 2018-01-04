using System;
using System.Windows.Forms;

namespace Z900.DataPuller
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles( );
         Application.SetCompatibleTextRenderingDefault( false );
         f( );
         //         Application.Run( new DataPuller.Form1( ) );
      }

      private static void f()
      {
         DataModel.DataStoreCollection dsColl = DataModel.DataStoreCollection.LoadDataStoreCollection( );
         if( dsColl.IsActive )
         {
            for( int i = 0; i < dsColl.List.Count; i++ )
            {
               Z900.DataModel.DataStore ds = dsColl.List[ i ];
               if( !ds.IsActive || !ds.IsToPullRemotely )
               {
                  continue;
               }
               ActiveQueryBuilder.View.WinForms.QueryBuilder qb = PullMetadata( ds );
               if( qb == null )
               {
                  continue;
               }
               //DumpAqbQb( qb, ds );
               //DumpMetadataItem( qb, ds );

               // publish( qb, ds );
               // push( qb, ds );
               // baseline( qb, ds );
               // diff( qb, ds );
               // thesaurus( qb, ds );
               //  watchers( qb, ds );
               // log( qb, ds );

            }
         }
         dsColl.SaveDataStoreCollection( );
      }
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder PullMetadata( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb;
         switch( (DataModel.DataStore.SyntaxProviderEnum) ds.SyntaxProvider )
         {
            case DataModel.DataStore.SyntaxProviderEnum.SQLITE:
               return qb = HandleSQLite( ds );
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2014:
               return qb = null; //  HandleMSSQL( ds );
            case DataModel.DataStore.SyntaxProviderEnum.AUTO:
            case DataModel.DataStore.SyntaxProviderEnum.GENERIC:
            case DataModel.DataStore.SyntaxProviderEnum.ANSI_SQL_2003:
            case DataModel.DataStore.SyntaxProviderEnum.ANSI_SQL_89:
            case DataModel.DataStore.SyntaxProviderEnum.ANSI_SQL_92:
            case DataModel.DataStore.SyntaxProviderEnum.FIREBIRD_1_0:
            case DataModel.DataStore.SyntaxProviderEnum.FIREBIRD_1_5:
            case DataModel.DataStore.SyntaxProviderEnum.FIREBIRD_2_0:
            case DataModel.DataStore.SyntaxProviderEnum.FIREBIRD_2_5:
            case DataModel.DataStore.SyntaxProviderEnum.IBM_DB2:
            case DataModel.DataStore.SyntaxProviderEnum.IBM_INFORMIX_10:
            case DataModel.DataStore.SyntaxProviderEnum.IBM_INFORMIX_8:
            case DataModel.DataStore.SyntaxProviderEnum.IBM_INFORMIX_9:
            case DataModel.DataStore.SyntaxProviderEnum.MS_ACCESS_2000:
            case DataModel.DataStore.SyntaxProviderEnum.MS_ACCESS_2003:
            case DataModel.DataStore.SyntaxProviderEnum.MS_ACCESS_97:
            case DataModel.DataStore.SyntaxProviderEnum.MS_ACCESS_XP:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2000:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2005:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2008:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2012:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_7:
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_COMPACT_EDITION:
            case DataModel.DataStore.SyntaxProviderEnum.MYSQL_3_XX:
            case DataModel.DataStore.SyntaxProviderEnum.MYSQL_4_0:
            case DataModel.DataStore.SyntaxProviderEnum.MYSQL_4_1:
            case DataModel.DataStore.SyntaxProviderEnum.MYSQL_5_0:
            case DataModel.DataStore.SyntaxProviderEnum.ORACLE_10:
            case DataModel.DataStore.SyntaxProviderEnum.ORACLE_11:
            case DataModel.DataStore.SyntaxProviderEnum.ORACLE_7:
            case DataModel.DataStore.SyntaxProviderEnum.ORACLE_8:
            case DataModel.DataStore.SyntaxProviderEnum.ORACLE_9:
            case DataModel.DataStore.SyntaxProviderEnum.POSTGRESQL:
            case DataModel.DataStore.SyntaxProviderEnum.SYBASE_ASE:
            case DataModel.DataStore.SyntaxProviderEnum.SYBASE_SQL_ANYWHERE:
            case DataModel.DataStore.SyntaxProviderEnum.TERADATA:
            case DataModel.DataStore.SyntaxProviderEnum.VISTADB:
            default:
               return null;
         }
      }
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder HandleSQLite( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = CreateAqbQbSQLite( ds );
         ActiveQueryBuilder.Core.MetadataLoadingOptions loadingOptions = qb.SQLContext.MetadataContainer.LoadingOptions;
         loadingOptions.LoadDefaultDatabaseOnly = ds.LoadDefaultDatabaseOnly;
         loadingOptions.LoadSystemObjects = ds.LoadSystemObjects;
         //loadingOptions.IncludeFilter.Types = MetadataType.Field;
         //loadingOptions.ExcludeFilter.Schemas.Add(“dbo”);
         //qb.InitializeDatabaseSchemaTree();
         //qb.MetadataContainer.LoadAll(withFields);
         return qb;
      }
      public static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbSQLite( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.SQLiteSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.SQLiteMetadataProvider( )
         };
         qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         return qb;
      }
   }
}
