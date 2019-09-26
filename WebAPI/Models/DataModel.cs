using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        public string PcName { get; set; }
        public string OS { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<LoggedUser> Users { get; set; }
        public string CpuLoad { get; set; }
        public string RamLoad { get; set; }
    }
}