using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace PcDataService
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
            var serviceInstaller = new ServiceInstaller();
            var processInstaller = new ServiceProcessInstaller {Account = ServiceAccount.LocalSystem};
            
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "PcDataTraceService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
