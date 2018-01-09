using System;
using System.Windows.Forms;

namespace Z900.DataProcessor
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
         //Application.Run( new Form1( ) );
      }
      private static void f()
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
//                  ActiveQueryBuilder.View.WinForms.QueryBuilder qb = PullMetadata( ds );
                  //if( qb == null )
                  //{
                  //   continue;
                  //}
                  //DumpMetadataItem( qb, ds );

                  // publish( qb, ds );
                  // push( qb, ds );
                  // baseline( qb, ds );
                  // diff( qb, ds );
                  // thesaurus( qb, ds );
                  //  watchers( qb, ds );
                  // log( qb, ds );
               }
               catch( Exception ex )
               {
               }
            }
         }
         dsColl.SaveDataStoreCollection( );
      }
   }
}
