using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;
using System.Web;

namespace Advtek.Utility
{
    public class MyLogImpl : LogImpl, IMyLog
    {
        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private readonly static Type ThisDeclaringType = typeof(MyLogImpl);

        public MyLogImpl(ILogger logger) : base(logger)
        {
        }

        #region Implementation of IMyLog

        public void Debug(string msg)
        {
            if (this.IsDebugEnabled)
            {
                object obj = HttpContext.Current.Session["logMsg"];
                if (obj != null)
                {
                    LogMessage logMsg = obj as LogMessage;
                    LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, null, null);
                    loggingEvent.Properties["SYS_ID"] = logMsg.SYS_ID;
                    loggingEvent.Properties["FUNC_GROUP"] = logMsg.FUNC_GROUP;
                    loggingEvent.Properties["ACTION_TYPE"] = logMsg.ACTION_TYPE;
                    loggingEvent.Properties["MACHINE_ID"] = logMsg.MACHINE_ID;
                    loggingEvent.Properties["HOST_IP"] = logMsg.HOST_IP;
                    loggingEvent.Properties["PARAMETER"] = msg; // logMsg.PARAMETER;
                    loggingEvent.Properties["OPERATOR"] = logMsg.OPERATOR;
                    loggingEvent.Properties["ENTERY_DTM"] = logMsg.ENTERY_DTM;
                    loggingEvent.Properties["FUNCTION_NO"] = logMsg.FUNCTION_NO;
                    loggingEvent.Properties["ROLE_TYPE"] = logMsg.ROLE_TYPE;
                    loggingEvent.Properties["CREATE_USER"] = logMsg.CREATE_USER;
                    loggingEvent.Properties["CREATE_DTM"] = logMsg.CREATE_DTM;
                    loggingEvent.Properties["MODI_USER"] = logMsg.MODI_USER;
                    loggingEvent.Properties["MODI_DTM"] = logMsg.MODI_DTM;
                    loggingEvent.Properties["IMPACT_REC_COUNT"] = logMsg.IMPACT_REC_COUNT;

                    Logger.Log(loggingEvent);

                }


            }
        }

        public void Debug(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
        {
            Debug(operatorID, operand, actionType, message, ip, browser, machineName, null);
        }

        public void Debug(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName, System.Exception t)
        {
            if (this.IsDebugEnabled)
            {
                LoggingEvent loggingEvent =new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["Operator"] = operatorID;
                loggingEvent.Properties["Operand"] = operand;
                loggingEvent.Properties["ActionType"] = actionType;
                loggingEvent.Properties["IP"] = ip;
                loggingEvent.Properties["Browser"] = browser;
                loggingEvent.Properties["MachineName"] = machineName;
                Logger.Log(loggingEvent);
            }
        }


        public void Info(LogMessage msg)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, null, null);
                loggingEvent.Properties["SYS_ID"] = msg.SYS_ID;
                loggingEvent.Properties["FUNC_GROUP"] = msg.FUNC_GROUP;
                loggingEvent.Properties["ACTION_TYPE"] = msg.ACTION_TYPE;
                loggingEvent.Properties["MACHINE_ID"] = msg.MACHINE_ID;
                loggingEvent.Properties["HOST_IP"] = msg.HOST_IP;
                loggingEvent.Properties["PARAMETER"] = msg.PARAMETER;
                loggingEvent.Properties["OPERATOR"] = msg.OPERATOR;
                loggingEvent.Properties["ENTERY_DTM"] = msg.ENTERY_DTM;
                loggingEvent.Properties["FUNCTION_NO"] = msg.FUNCTION_NO;
                loggingEvent.Properties["ROLE_TYPE"] = msg.ROLE_TYPE;
                loggingEvent.Properties["CREATE_USER"] = msg.CREATE_USER;
                loggingEvent.Properties["CREATE_DTM"] = msg.CREATE_DTM;
                loggingEvent.Properties["MODI_USER"] = msg.MODI_USER;
                loggingEvent.Properties["MODI_DTM"] = msg.MODI_DTM;
                loggingEvent.Properties["IMPACT_REC_COUNT"] = msg.IMPACT_REC_COUNT;

                Logger.Log(loggingEvent);
            }
        }

        public void Info(string msg)
        {
            if (this.IsInfoEnabled)
            {
                object obj = HttpContext.Current.Session["logMsg"];
                if (obj != null)
                {
                    LogMessage logMsg = obj as LogMessage;
                    LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, null, null);
                    loggingEvent.Properties["SYS_ID"] = logMsg.SYS_ID;
                    loggingEvent.Properties["FUNC_GROUP"] = logMsg.FUNC_GROUP;
                    loggingEvent.Properties["ACTION_TYPE"] = logMsg.ACTION_TYPE;
                    loggingEvent.Properties["MACHINE_ID"] = logMsg.MACHINE_ID;
                    loggingEvent.Properties["HOST_IP"] = logMsg.HOST_IP;
                    loggingEvent.Properties["PARAMETER"] = msg; // logMsg.PARAMETER;
                    loggingEvent.Properties["OPERATOR"] = logMsg.OPERATOR;
                    loggingEvent.Properties["ENTERY_DTM"] = logMsg.ENTERY_DTM;
                    loggingEvent.Properties["FUNCTION_NO"] = logMsg.FUNCTION_NO;
                    loggingEvent.Properties["ROLE_TYPE"] = logMsg.ROLE_TYPE;
                    loggingEvent.Properties["CREATE_USER"] = logMsg.CREATE_USER;
                    loggingEvent.Properties["CREATE_DTM"] = logMsg.CREATE_DTM;
                    loggingEvent.Properties["MODI_USER"] = logMsg.MODI_USER;
                    loggingEvent.Properties["MODI_DTM"] = logMsg.MODI_DTM;
                    loggingEvent.Properties["IMPACT_REC_COUNT"] = logMsg.IMPACT_REC_COUNT;

                    Logger.Log(loggingEvent);
                }
            }
        }

        public void Info(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName)
        {
            Info(operatorID, operand, actionType, message, ip, browser, machineName, null);
        }

        public void Info(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, System.Exception t)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["Operator"] = operatorID;
                loggingEvent.Properties["Operand"] = operand;
                loggingEvent.Properties["ActionType"] = actionType;
                loggingEvent.Properties["IP"] = ip;
                loggingEvent.Properties["Browser"] = browser;
                loggingEvent.Properties["MachineName"] = machineName;
                Logger.Log(loggingEvent);
            }
        }

        public void Warn(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName)
        {
            Warn(operatorID, operand, actionType, message, ip, browser, machineName, null);
        }

        public void Warn(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, System.Exception t)
        {
            if (this.IsWarnEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository,Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["Operator"] = operatorID;
                loggingEvent.Properties["Operand"] = operand;
                loggingEvent.Properties["ActionType"] = actionType;
                loggingEvent.Properties["IP"] = ip;
                loggingEvent.Properties["Browser"] = browser;
                loggingEvent.Properties["MachineName"] = machineName;
                Logger.Log(loggingEvent);
            }
        }

        public void Error(string msg)
        {
            if (this.IsErrorEnabled)
            {
                object obj = HttpContext.Current.Session["logMsg"];
                if (obj != null)
                {
                    LogMessage logMsg = obj as LogMessage;
                    LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, null, null);
                    loggingEvent.Properties["SYS_ID"] = logMsg.SYS_ID;
                    loggingEvent.Properties["FUNC_GROUP"] = logMsg.FUNC_GROUP;
                    loggingEvent.Properties["ACTION_TYPE"] = logMsg.ACTION_TYPE;
                    loggingEvent.Properties["MACHINE_ID"] = logMsg.MACHINE_ID;
                    loggingEvent.Properties["HOST_IP"] = logMsg.HOST_IP;
                    loggingEvent.Properties["PARAMETER"] = msg; // logMsg.PARAMETER;
                    loggingEvent.Properties["OPERATOR"] = logMsg.OPERATOR;
                    loggingEvent.Properties["ENTERY_DTM"] = logMsg.ENTERY_DTM;
                    loggingEvent.Properties["FUNCTION_NO"] = logMsg.FUNCTION_NO;
                    loggingEvent.Properties["ROLE_TYPE"] = logMsg.ROLE_TYPE;
                    loggingEvent.Properties["CREATE_USER"] = logMsg.CREATE_USER;
                    loggingEvent.Properties["CREATE_DTM"] = logMsg.CREATE_DTM;
                    loggingEvent.Properties["MODI_USER"] = logMsg.MODI_USER;
                    loggingEvent.Properties["MODI_DTM"] = logMsg.MODI_DTM;
                    loggingEvent.Properties["IMPACT_REC_COUNT"] = logMsg.IMPACT_REC_COUNT;

                    Logger.Log(loggingEvent);
                }

            }
        }

        public void Error(int operatorID, string operand, int actionType, object message,string ip, string browser, string machineName)
        {
            Error(operatorID, operand, actionType, message, ip, browser, machineName, null);
        }

        public void Error(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, System.Exception t)
        {
            if (this.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["Operator"] = operatorID;
                loggingEvent.Properties["Operand"] = operand;
                loggingEvent.Properties["ActionType"] = actionType;
                loggingEvent.Properties["IP"] = ip;
                loggingEvent.Properties["Browser"] = browser;
                loggingEvent.Properties["MachineName"] = machineName;
                Logger.Log(loggingEvent);
            }
        }

        public void Fatal(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName)
        {
            Fatal(operatorID, operand, actionType, message, ip, browser, machineName, null);
        }

        public void Fatal(int operatorID, string operand, int actionType, object message, string ip, string browser, string machineName, System.Exception t)
        {
            if (this.IsFatalEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["Operator"] = operatorID;
                loggingEvent.Properties["Operand"] = operand;
                loggingEvent.Properties["ActionType"] = actionType;
                loggingEvent.Properties["IP"] = ip;
                loggingEvent.Properties["Browser"] = browser;
                loggingEvent.Properties["MachineName"] = machineName;
                Logger.Log(loggingEvent);
            }
        }
        #endregion Implementation of IMyLog
    }
}
