using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Management;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PcDataService
{
    public sealed class PcData
    {
        private readonly Timer _timer;
        private readonly List<LoggedUser> _loggedUsers = new List<LoggedUser>();

        public PcData()
        {
            _timer = new Timer(1000 * 60 * 30) {AutoReset = false};
           // _timer = new Timer(1000 * 15) {AutoReset = false};
            _timer.Elapsed += async (_, __) => await GetDataCycle();
        }

        private async Task GetDataCycle()
        {
            try
            {
                var pcData = GetData();
                await PostData(pcData);
            }
            catch
            {
                // ignored
                // Log exception
                //Request error api not access
            } 
            finally
            {
                _loggedUsers.Clear();
                _timer.Start();
            }
        }

        private async Task PostData(DataModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000");
                await httpClient.PostAsync("/api/PcData/Post", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            }
        }


        private DataModel GetData()
        {
            var currentMachineName = Environment.MachineName;

            var os = GetComponent("Win32_OperatingSystem", "Caption");

            var cpu = GetComponent("Win32_Processor", "Name");
            var gpu = GetComponent("Win32_VideoController", "Name");
            var mb = GetComponent("Win32_BaseBoard", "Product");

            var cpuLoad = GetComponent("Win32_Processor", "LoadPercentage");
            var ramLoad = GetRamLoadPercentage();


            return new DataModel()
            {
                PcName = currentMachineName,
                OS = os,
                Manufacturer = new Manufacturer()
                {
                    CPU = cpu,
                    GPU = gpu,
                    MotherBoard = mb,
                },
                Users = _loggedUsers,
                CpuLoad = cpuLoad,
                RamLoad = ramLoad,
            };
        }

        private string GetComponent(string hwsClass, string syntax)
        {
            var query = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwsClass);
            var result = string.Empty;
            try
            {
                foreach (var item in query.Get())
                {
                    result = Convert.ToString(item[syntax]);
                }
            }
            catch
            {
                return string.Empty;
            }

            return result;
        }

        private string GetRamLoadPercentage()
        {
            var totalRam = double.Parse(GetComponent("Win32_OperatingSystem", "TotalVisibleMemorySize"));
            var freeRam = double.Parse(GetComponent("Win32_OperatingSystem", "FreePhysicalMemory"));
            return Convert.ToString((int) (((totalRam - freeRam) / totalRam) * 100), CultureInfo.CurrentCulture);
        }

        public void Start()
        {
            this._timer.Start();
        }

        public void Stop()
        {
            this._timer.Stop();
        }

        public void OnSessionChange()
        {
            var userName = UserTrace.GetInstance().GetUsername();
            _loggedUsers.Add(new LoggedUser() {LogginDate = DateTime.Now, UserName = userName});
        }
    }
}