using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSettlement
{
    public class Log_Sms 
    {
      
        public int LogId { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public string Body { get; set; }
        public string SessionId { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string ResultBody { get; set; }
        public bool IsSuccessful { get; set; }
        public short ProviderId { get; set; }
    }
}
