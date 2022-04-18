using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// LogMessage 的摘要描述
/// </summary>
namespace Advtek.Utility
{
    public class LogMessage
    {
        public string SNO { get; set; }
        public string SYS_ID { get; set; }
        public string FUNC_GROUP { get; set; }
        public string STORENO { get; set; }
        public string ACTION_TYPE { get; set; }
        public string MACHINE_ID { get; set; }
        public string HOST_IP { get; set; }
        public string PARAMETER { get; set; }
        public string OPERATOR { get; set; }
        public DateTime ENTERY_DTM { get; set; }
        public string FUNCTION_NO { get; set; }
        public string ROLE_TYPE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DTM { get; set; }
        public string MODI_USER { get; set; }
        public DateTime MODI_DTM { get; set; }
        public Int16 IMPACT_REC_COUNT { get; set; }

        public LogMessage()
        {
        }
    }
    
}
