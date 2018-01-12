namespace Z900.DataPuller
{
   public static class Engine2
   {
      public static void Start()
      {
         DataModel.DataStoreCollection dsColl = DataModel.DataStoreCollection.LoadDataStoreCollection( );
         if( dsColl.IsActive )
         {
            for( int i = 0; i < dsColl.List.Count; i++ )
            {
               DataModel.DataStore ds = dsColl.List[ i ];
               if( !ds.IsActive /*|| !ds.IsToPullRemotely*/ )
               {
                  continue;
               }
               try
               {
                  ActiveQueryBuilder.View.WinForms.QueryBuilder qb = PullMetadata( ds );
                  if( qb == null )
                  {
                     continue;
                  }
                  DumpAqbQb( qb, ds );
                  DataModel.MetadataItemCollection miColl = DumpMetadataItem( ds, qb );
                  // publish( qb, ds );
                  // push( qb, ds );
                  // baseline( qb, ds );
                  // diff( qb, ds );
                  // thesaurus( qb, ds );
                  // watchers( qb, ds );
                  // log( qb, ds );
               }
               catch( System.Exception ex )
               {
               }
            }
         }
         dsColl.SaveDataStoreCollection( );
      }

      #region --- AQB QB Handling... ---
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder PullMetadata( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb;
         switch( (DataModel.DataStore.SyntaxProviderEnum) ds.SyntaxProvider )
         {
            case DataModel.DataStore.SyntaxProviderEnum.SQLITE:
               qb = CreateAqbQbSQLite( ds );
               break;
            case DataModel.DataStore.SyntaxProviderEnum.MS_SQL_SERVER_2014:
               qb = CreateAqbQbMSSS( ds );
               break;
            case DataModel.DataStore.SyntaxProviderEnum.AUTO:
               return qb = null; // CreateAqbQbAuto( ds );
                                 //break;
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
         ActiveQueryBuilder.Core.MetadataLoadingOptions loadingOptions = qb.SQLContext.MetadataContainer.LoadingOptions;
         loadingOptions.LoadDefaultDatabaseOnly = ds.LoadDefaultDatabaseOnly;
         loadingOptions.LoadSystemObjects = ds.LoadSystemObjects;
         //loadingOptions.IncludeFilter.Types = MetadataType.Field;
         //loadingOptions.ExcludeFilter.Schemas.Add(“dbo”);
         {
            ds.PullTS = System.DateTime.Now;
            string x = string.Format( Global.TS_MASK_FORMAT, ds.PullTS ).Replace( ":", "" );
            ds.TempFileFullPathName = System.IO.Path.GetTempFileName( );
            ds.AqbQbFilename = Global.DataStoreCollectionPathName + x + "." + ds.Name + Global.FileExtensionXmlAqbQb;
            ds.MiFqnFilename = Global.DataStoreCollectionPathName + x + "." + ds.Name + Global.FileExtensionXmlMiFqn;
         }
         qb.MetadataContainer.LoadAll( ds.WithFields );
         //qb.InitializeDatabaseSchemaTree( );
         return qb;

      }

      #region --- Auto Handle and AQB-QB ---
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbAuto( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.AutoSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.UniversalMetadataProvider( )
         };
         //qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         qb.MetadataProvider.Connection = new System.Data.SQLite.SQLiteConnection( ds.ConnectionString );
         return qb;
      }
      #endregion

      #region --- SQLite Handle and AQB-QB ---
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbSQLite( DataModel.DataStore ds )
      {

         /*
            https://support.activequerybuilder.com/hc/en-us/articles/115001055349-Getting-started-with-AQB-NET-3-in-the-Separated-Controls-UI-or-Non-visual-mode

            Your initialization code will look as follows:

            // Non-visual objects
            _sqlContext = new SQLContext
            {
                SyntaxProvider = new MSSQLSyntaxProvider 
                {
                   ServerVersion = MSSQLServerVersion.MSSQL2012
                },
                MetadataProvider = new OLEDBMetadataProvider 
                {
                      Connection = new OleDbConnection() 
                      {
                         ConnectionString = ""
                      }
                }
            };

            _sqlQuery = new SQLQuery(_sqlContext);

            Sergey Smagin
            Ok, then you need only the SQLContext object.

            sqlContext.MetadataContainer.ExportToXML();

         */
         //ActiveQueryBuilder.Core.SQLContext sc = new ActiveQueryBuilder.Core.SQLContext( )
         //{
         //   SyntaxProvider = new ActiveQueryBuilder.Core.SQLiteSyntaxProvider( ),
         //   MetadataProvider = new ActiveQueryBuilder.Core.SQLiteMetadataProvider( )
         //   {
         //      Connection = new System.Data.SQLite.SQLiteConnection( )
         //      {
         //         ConnectionString = ds.ConnectionString
         //      }
         //   }
         //};
         //
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.SQLiteSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.SQLiteMetadataProvider( )
         };
         //qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         qb.MetadataProvider.Connection = new System.Data.SQLite.SQLiteConnection( ds.ConnectionString );
         return qb;
      }
      #endregion

      #region --- MSSS Handle and AQB-QB ---
      private static ActiveQueryBuilder.View.WinForms.QueryBuilder CreateAqbQbMSSS( DataModel.DataStore ds )
      {
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb = new ActiveQueryBuilder.View.WinForms.QueryBuilder( )
         {
            SyntaxProvider = new ActiveQueryBuilder.Core.MSSQLSyntaxProvider( ),
            MetadataProvider = new ActiveQueryBuilder.Core.MSSQLMetadataProvider( )
         };
         qb.MetadataProvider.Connection = new System.Data.SqlClient.SqlConnection( ds.ConnectionString );
         //qb.MetadataProvider.Connection = new System.Data.SQLite.SQLiteConnection( ds.ConnectionString );
         return qb;
      }
      #endregion

      private static void DumpAqbQb( ActiveQueryBuilder.View.WinForms.QueryBuilder qb, DataModel.DataStore ds )
      {
         string path = Global.DataStoreCollectionPathName + Global.TS_STR;
         {
            // Export AQB’s Query Builder XML Structures…
            string xmlStr = qb.MetadataContainer.XML;
            qb.MetadataContainer.ExportToXML( ds.TempFileFullPathName );
            // SQLite From TMP to "USER PATH"
            System.IO.File.Copy( ds.TempFileFullPathName, ds.AqbQbFilename, true );
            // qb.MetadataContainer.ImportFromXML( filename );
         }
         //tnm.ShowNotification( tnm.Notifications[ 0 ] );
      }
      #endregion

      #region --- EXTRACT METADATA VALUES ---
      private static DataModel.MetadataItemCollection DumpMetadataItem( DataModel.DataStore ds, ActiveQueryBuilder.View.WinForms.QueryBuilder qb )
      {
         DataModel.MetadataItemCollection o = new DataModel.MetadataItemCollection( );
         o.List = BuildBindingList( qb );
         // Export MetadataItem FQN Collection...
         string TempFileFullPathName = System.IO.Path.GetTempFileName( );
         o.Save( TempFileFullPathName );
         // SQLite From TMP to "USER PATH"
         System.IO.File.Copy( TempFileFullPathName, ds.MiFqnFilename, true );
         //
         return o;
      }

      private class StackItem
      {
         public ActiveQueryBuilder.Core.MetadataList list;
         public int index;
         public int parentID;
         public int grandParentID;
      }
      private static System.ComponentModel.BindingList<DataModel.MetadataItem> BuildBindingList(
         ActiveQueryBuilder.View.WinForms.QueryBuilder qb
         )
      {
         System.ComponentModel.BindingList<DataModel.MetadataItem> list = new System.ComponentModel.BindingList<DataModel.MetadataItem>( );
         using( var sqlContext = new ActiveQueryBuilder.Core.SQLContext( ) )
         {
            sqlContext.Assign( qb.SQLContext );
            //sqlContext.MetadataContainer.LoadingOptions.LoadDefaultDatabaseOnly = false;
            //sqlContext.MetadataContainer.LoadingOptions.LoadSystemObjects = false;

            using( ActiveQueryBuilder.Core.MetadataList miList = new ActiveQueryBuilder.Core.MetadataList( sqlContext.MetadataContainer ) )
            {
               miList.Load( ActiveQueryBuilder.Core.MetadataType.All, true );
               System.Collections.Generic.Stack<StackItem> stack = new System.Collections.Generic.Stack<StackItem>( );
               stack.Push( new StackItem { list = miList, index = 0, parentID = -1, grandParentID = -1 } );
               do
               {
                  StackItem si = stack.Pop( );
                  ActiveQueryBuilder.Core.MetadataList actualMIList = si.list;
                  int actualIndex = si.index;
                  int actualParentID = si.grandParentID; // IMPORTANT!!!
                  {
                     for( ; actualIndex < actualMIList.Count; actualIndex++ )
                     {
                        ExtractMetadataItem( list, actualMIList[ actualIndex ], actualParentID );
                        if( actualMIList[ actualIndex ].Items.Count > 0 ) // branch...
                        {
                           // Push the "next" Item...
                           stack.Push( new StackItem
                           {
                              list = actualMIList,
                              index = actualIndex + 1,
                              parentID = list[ list.Count - 1 ].ID,
                              grandParentID = actualParentID
                           } );
                           // Reset the loop to process the "actual" Item...
                           actualParentID = list[ list.Count - 1 ].ID;
                           actualMIList = actualMIList[ actualIndex ].Items;
                           actualIndex = -1;
                        }
                     } // for(;;)...
                  }
               } while( stack.Count > 0 );
            } // using()...
         } // using()...
         return list;
      } // buildBindingList(...)

      private static void ExtractMetadataItem(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         switch( mi.Type )
         {
            case ActiveQueryBuilder.Core.MetadataType.Root:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Server:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Database:
               ExtractNamespace( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Schema:
               ExtractNamespace( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Package:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Namespaces:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Table:
               ExtractTable( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.View:
               ExtractTable( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Procedure:
               ExtractProcedure( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Synonym:
               ExtractSynonym( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Objects:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Aggregate:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Parameter:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.Field:
               ExtractField( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.ForeignKey:
               ExtractForeignKey( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.ObjectMetadata:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.UserQuery:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.UserField:
               ExtractItem( list, mi, parentID );
               break;
            case ActiveQueryBuilder.Core.MetadataType.All:
               ExtractItem( list, mi, parentID );
               break;
            default:
               ExtractItem( list, mi, parentID );
               break;
         } // switch()...
      }
      private static void ExtractProcedure(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         {
            ActiveQueryBuilder.Core.MetadataObject m = mi as ActiveQueryBuilder.Core.MetadataObject;
         }
      }
      private static void ExtractSynonym(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         {
            ActiveQueryBuilder.Core.MetadataObject m = mi as ActiveQueryBuilder.Core.MetadataObject;
            o.ReferencedObject = m.ReferencedObject?.NameFull;
            //
            for( int i = 0; i < m.ReferencedObjectName.Count; i++ )
            {
               ActiveQueryBuilder.Core.MetadataQualifiedNamePart x = m.ReferencedObjectName[ i ];
               o.ReferencedObjectName += "["
               + System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataType ), x.Type )
               + ":"
               + x.Name
               + "]"
            ;
            }
         }
      }
      private static void ExtractNamespace(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         {
            ActiveQueryBuilder.Core.MetadataNamespace m = mi as ActiveQueryBuilder.Core.MetadataNamespace;
         }
      }
      private static void ExtractTable(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         {
            ActiveQueryBuilder.Core.MetadataObject m = mi as ActiveQueryBuilder.Core.MetadataObject;
         }
      }
      private static void ExtractForeignKey(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         o.FieldType = null;
         {
            ActiveQueryBuilder.Core.MetadataForeignKey m = mi as ActiveQueryBuilder.Core.MetadataForeignKey;
            o.ReferencedObject = m.ReferencedObject.NameFull;
            //
            for( int i = 0; i < m.ReferencedObjectName.Count; i++ )
            {
               ActiveQueryBuilder.Core.MetadataQualifiedNamePart x = m.ReferencedObjectName[ i ];
               o.ReferencedObjectName += "["
               + System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataType ), x.Type )
               + ":"
               + x.Name
               + "]"
            ;
            }
            //
            o.ReferencedFieldsCount = m.ReferencedFields.Count;
            for( int i = 0; i < m.ReferencedFields.Count; i++ )
            {
               o.ReferencedFields += "[" + m.ReferencedFields[ i ] + "]"
            ;
            }
            //
            o.ReferencedCardinality = System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataForeignKeyCardinality ), m.ReferencedCardinality );
            //
            o.FieldsCount = m.Fields.Count;
            for( int i = 0; i < m.Fields.Count; i++ )
            {
               o.Fields += "[" + m.Fields[ i ] + "]"
            ;
            }
            //
            o.Cardinality = System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataForeignKeyCardinality ), m.Cardinality );
         }
      }
      private static void ExtractField(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         if( mi == null ) return;
         var o = ExtractItem( list, mi, parentID );
         {
            ActiveQueryBuilder.Core.MetadataField m = mi as ActiveQueryBuilder.Core.MetadataField;
            o.Expression = m.Expression;
            o.FieldType = System.Enum.GetName( typeof( System.Data.DbType ), m.FieldType );
            o.FieldTypeName = m.FieldTypeName;
            o.IsNullable = m.Nullable;
            o.Precision = m.Precision;
            o.Scale = m.Scale;
            o.Size = m.Size;
            o.IsPK = m.PrimaryKey;
            o.IsRO = m.ReadOnly;
         }
      }
      private static DataModel.MetadataItem ExtractItem(
         System.ComponentModel.BindingList<DataModel.MetadataItem> list,
         ActiveQueryBuilder.Core.MetadataItem mi,
         int parentID
         )
      {
         var o = new DataModel.MetadataItem( );
         {
            o.MetadataProvider = mi.SQLContext?.MetadataProvider.Description;
            o.SyntaxProvider = mi.SQLContext?.SyntaxProvider.Description;
            o.ID = list.Count;
            o.ParentID = parentID;
            if( mi.Parent != null )
            {
               o.ParentType = System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataType ), mi.Parent.Type );
            }
            o.Type = System.Enum.GetName( typeof( ActiveQueryBuilder.Core.MetadataType ), mi.Type );
            o.IsSystem = mi.System;
            //
            // o.RootName = item.Root?.Name;
            o.Server = mi.Server?.Name;
            o.Database = mi.Database?.Name;
            o.Schema = mi.Schema?.Name;
            o.ObjectName = mi.Object?.Name;
            //
            o.NameFullQualified = mi.NameFull;
            o.NameFullQualified += mi.NameFull.EndsWith( "." ) ? "<?>" : "";
            o.NameQuoted = mi.NameQuoted;
            o.AltName = mi.AltName;
            o.Field = mi.Name != null ? mi.Name : "<?>";
            //
            //
            o.HasDefault = mi.Default;
            o.Description = mi.Description;
            o.Tag = mi.Tag;
            o.UserData = mi.UserData;
         }
         list.Add( o );
         return o;
      }
      #endregion
   }
}
