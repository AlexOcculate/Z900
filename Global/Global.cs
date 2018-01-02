namespace Z900
{
   public static class Global
   {
      #region --- TS_STR ---
      public const string TS_MASK_FORMAT = "{0:yyyyMMdd-HHmmss-ffffzzz}";
      public static string TS_STR
      {
         [System.Diagnostics.DebuggerStepThrough]
         get
         {
            return string.Format( TS_MASK_FORMAT, System.DateTime.Now ).Replace( ":", "" );
         }
      }
      #endregion

      #region --- CREATION TIMESTAMP ----
      public static System.DateTime CreationTS
      {
         [System.Diagnostics.DebuggerStepThrough]
         get;
      }
      #endregion

      public static string FileExtensionSQLite = @".db";
      public static string FileExtensionXml = @".xml";
      //
      public static string DataStoreCollectionPathName = @"D:\TEMP\SQLite\";
      public static string DataStoreCollectionBaseName = @"DataStoreCollection";
      public static string DataStoreCollectionSQLiteFileName = DataStoreCollectionBaseName + FileExtensionSQLite;
      public static string DataStoreCollectionXmlFileName = DataStoreCollectionBaseName + FileExtensionXml;
      public static string DataStoreCollectionXmlFilePathName = DataStoreCollectionPathName + DataStoreCollectionXmlFileName;

      public static string GetTempFilePathWithExtension( string extension )
      {
         var path = System.IO.Path.GetTempPath( );
         var fileName = System.Guid.NewGuid( ).ToString( ) + extension;
         return System.IO.Path.Combine( path, fileName );
      }

      public static System.IO.FileInfo GetLastWrittenXmlFileInfo()
      {
         string s = "*." + Global.DataStoreCollectionBaseName + Global.FileExtensionXml;
         System.IO.DirectoryInfo di = new System.IO.DirectoryInfo( Global.DataStoreCollectionPathName );
         System.IO.FileInfo[ ] fis = di.GetFiles( s, System.IO.SearchOption.TopDirectoryOnly );
         if( fis.Length == 0 )
         {
            return null;
         }
         System.IO.FileInfo fi = fis[ 0 ];
         for( int i = 1; i < fis.Length; i++ )
         {
            if( fi.LastWriteTimeUtc <= fis[ i ].LastWriteTimeUtc )
            {
               fi = fis[ i ];
            }
         }
         return fi;
      }

      public static System.IO.FileInfo GetLastWrittenSQLiteFileInfo()
      {
         string s = "*." + Global.DataStoreCollectionBaseName + Global.FileExtensionSQLite;
         System.IO.DirectoryInfo di = new System.IO.DirectoryInfo( Global.DataStoreCollectionPathName );
         System.IO.FileInfo[ ] fis = di.GetFiles( s, System.IO.SearchOption.TopDirectoryOnly );
         if( fis.Length == 0 )
         {
            return null;
         }
         System.IO.FileInfo fi = fis[ 0 ];
         for( int i = 1; i < fis.Length; i++ )
         {
            if( fi.LastWriteTimeUtc <= fis[ i ].LastWriteTimeUtc )
            {
               fi = fis[ i ];
            }
         }
         return fi;
      }
   }
}
