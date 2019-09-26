using System;
using System.Management;

namespace PcDataService
{
    public class UserTrace
    {
        private UserTrace(){}
        
        private static UserTrace _instance;

        public static UserTrace GetInstance() 
            => _instance ?? (_instance = new UserTrace());
        
        
        public string GetUsername()
        {
            string username = null;
            try
            {
                var ms = new ManagementScope("\\\\.\\root\\cimv2");
                ms.Connect();
 
                var query = new ObjectQuery
                    ("SELECT * FROM Win32_ComputerSystem");
                var searcher =
                    new ManagementObjectSearcher(ms, query);
                
                foreach (var o in searcher.Get())
                {
                    var mo = (ManagementObject) o;
                    username = mo["UserName"].ToString();
                }
                // Remove the domain part from the username
                var usernameParts = username.Split('\\');
                username = usernameParts[usernameParts.Length - 1];
            }
            catch (Exception)
            {
                // The system currently has no users who are logged on
                // Set the username to "SYSTEM" to denote that
                username = "SYSTEM";
            }
            return username;
        }
    }
}