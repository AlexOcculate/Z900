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
            return string.Format( TS_MASK_FORMAT, System.DateTime.Now ).Replace(":","");
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
   }
}
