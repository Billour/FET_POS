using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Advtek.Utility
{
    public interface IMyLog : ILog
    {
        void Debug(string msg);
        void Debug(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName);
        void Debug(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, Exception t);

        void Info(string msg);
        void Info(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName);
        void Info(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, Exception t);

        void Warn(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName);
        void Warn(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, Exception t);

        void Error(string msg);
        void Error(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName);
        void Error(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, Exception t);

        void Fatal(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName);
        void Fatal(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, Exception t);
    }
}
