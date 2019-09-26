using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PcDataService
{
    public partial class Service1 : ServiceBase
    {
        private readonly PcData _pcData;
        public Service1()
        {
            InitializeComponent();
            _pcData = new PcData();
        }

        protected override void OnStart(string[] args)
        {
            _pcData.Start();
        }

        protected override void OnStop()
        {
            _pcData.Stop();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            _pcData.OnSessionChange();
        }
    }
}
