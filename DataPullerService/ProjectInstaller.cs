using System.ComponentModel;

namespace Z900.DataPullerService
{
   [RunInstaller( true )]
   public partial class ProjectInstaller : System.Configuration.Install.Installer
   {
      public ProjectInstaller()
      {
         InitializeComponent( );
      }
   }
}
