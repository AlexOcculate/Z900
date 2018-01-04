namespace Z900.DataModel.Test
{
   class Program
   {
      static void Main( string[ ] args )
      {
         try
         {
            Z900.DataModel.DataStoreCollection dsColl = Z900.DataModel.DataStoreCollection.LoadDataStoreCollection( );
            for( int i = 0; i < dsColl.List.Count; i++ )
            {
               Z900.DataModel.DataStore ds = dsColl.List[ i ];
               if( !ds.IsActive || !ds.IsToPullRemotely )
               {
                  continue;
               }
               ActiveQueryBuilder.View.WinForms.QueryBuilder qb = pull( ds );
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
            dsColl.SaveDataStoreCollection( );
         }
         catch( System.Exception ex )
         {
            
         }
      }
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder pull( DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb;
         switch( (DataStore.SyntaxProviderEnum) ds.SyntaxProvider )
         {
            case DataStore.SyntaxProviderEnum.SQLITE:
               return qb = null; // HandleSQLite( ds );
            case DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2014:
               return qb = null; //  HandleMSSQL( ds );
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
            case DataStore.SyntaxProviderEnum.MS_ACCESS_2000:
            case DataStore.SyntaxProviderEnum.MS_ACCESS_2003:
            case DataStore.SyntaxProviderEnum.MS_ACCESS_97:
            case DataStore.SyntaxProviderEnum.MS_ACCESS_XP:
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
               return null;
         }
      }
/*
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder HandleSQLite( DataStore ds )
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
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder HandleMSSQL( DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = CreateAqbQbMSSQL( ds );
         ActiveQueryBuilder.Core.MetadataLoadingOptions loadingOptions = qb.SQLContext.MetadataContainer.LoadingOptions;
         loadingOptions.LoadDefaultDatabaseOnly = ds.LoadDefaultDatabaseOnly;
         loadingOptions.LoadSystemObjects = ds.LoadSystemObjects;
         //loadingOptions.IncludeFilter.Types = MetadataType.Field;
         //loadingOptions.ExcludeFilter.Schemas.Add(“dbo”);
         //qb.InitializeDatabaseSchemaTree();
         //qb.MetadataContainer.LoadAll(withFields);
         return qb;
      }

      public static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbSQLite( DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.SQLiteSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.SQLiteMetadataProvider( )
         };
         qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         return qb;
      }

      public static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbMSSQL( DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.MSSQLSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.MSSQLMetadataProvider( )
         };
         qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         return qb;
      }
*/
   }
}

/*

      private static void DumpAqbQb( ActiveQueryBuilder.View.WinForms.QueryBuilder qb, DataStore ds )
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

      private static void DumpMetadataItem( ActiveQueryBuilder.View.WinForms.QueryBuilder qb, DataStore ds )
      {
         string TS_STR = @”D:\TEMP\SQLite\” +DataStore.TS_STR.Replace( “:”, “” );
         {
            // Export MetadataItem FQN Collection…
            MetadataItemCollection o = new MetadataItemCollection( );
            o.List = ExtractMetadataValues.BuildBindingList( qb );
            string filename = TS_STR + “.” +ds.MiFqnFilename;
            o.Save( filename );
         }
         tnm.ShowNotification( tnm.Notifications[ 0 ] );
      }

*/
