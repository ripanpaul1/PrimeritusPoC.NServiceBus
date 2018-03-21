namespace Lateetud.NServiceBus.Subscriber
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PrimeritusServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.PrimeritusServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // PrimeritusServiceProcessInstaller
            // 
            this.PrimeritusServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.PrimeritusServiceProcessInstaller.Password = null;
            this.PrimeritusServiceProcessInstaller.Username = null;
            // 
            // PrimeritusServiceInstaller
            // 
            this.PrimeritusServiceInstaller.DisplayName = "PrimeritusPoCNServiceBus";
            this.PrimeritusServiceInstaller.ServiceName = "PrimeritusPoCNServiceBus";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.PrimeritusServiceProcessInstaller,
            this.PrimeritusServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller PrimeritusServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller PrimeritusServiceInstaller;
    }
}