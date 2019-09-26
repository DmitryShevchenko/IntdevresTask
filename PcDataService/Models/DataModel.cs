using System.Collections.Generic;

namespace PcDataService
{
    public class DataModel
    {
        public string PcName { get; set; }
        public string OS { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<LoggedUser> Users { get; set; }
        public string CpuLoad { get; set; }
        public string RamLoad { get; set; }
    }
}