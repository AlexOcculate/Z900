namespace Z900.DataPullerService
{
   partial class ProjectInstaller
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose( bool disposing )
      {
         if( disposing && (components != null) )
         {
            components.Dispose( );
         }
         base.Dispose( disposing );
      }

      #region Component Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.DataPullerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
         this.DataPullerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
         // 
         // DataPullerServiceProcessInstaller
         // 
         this.DataPullerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
         this.DataPullerServiceProcessInstaller.Password = null;
         this.DataPullerServiceProcessInstaller.Username = null;
         // 
         // DataPullerServiceInstaller
         // 
         this.DataPullerServiceInstaller.Description = "The Z900\'s Data Puller Service";
         this.DataPullerServiceInstaller.DisplayName = "DataPuller";
         this.DataPullerServiceInstaller.ServiceName = "DataPullerServiceInstaller";
         this.DataPullerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
         this.DataPullerServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.DataPullerServiceInstaller_AfterInstall);
         // 
         // ProjectInstaller
         // 
         this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DataPullerServiceProcessInstaller,
            this.DataPullerServiceInstaller});

      }

      #endregion

      private System.ServiceProcess.ServiceProcessInstaller DataPullerServiceProcessInstaller;
      private System.ServiceProcess.ServiceInstaller DataPullerServiceInstaller;
   }
}