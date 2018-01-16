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

      private void DataPullerServiceInstaller_AfterInstall( object sender, System.Configuration.Install.InstallEventArgs e )
      {
         //new System.ServiceProcess.ServiceController( this.DataPullerServiceInstaller.ServiceName ).Start();
      }
   }
}
