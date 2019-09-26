using System;

namespace WebAPI.Models
{
    public class LoggedUser
    {
        public Guid Id { get; set; }
        public DateTime LogginDate { get; set; }
        public string UserName { get; set; }
        
        public Guid DataModelPKey { get; set; }
        public DataModel DataModel { get; set; }
    }
}