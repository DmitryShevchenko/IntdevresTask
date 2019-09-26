using System;

namespace WebAPI.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        
        public string CPU { get; set; }
        public string MotherBoard { get; set; }
        public string GPU { get; set; }
        
        public Guid DataModelPKey { get; set; }
        public DataModel DataModel { get; set; }
    }
}