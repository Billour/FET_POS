namespace Advtek.Utility.UserException
{
    using System;

    public class ResetTypeDefineException : Exception
    {
        public override string ToString()
        {
            return "SeqNo指定的ResetType錯誤, 請檢查SeqNo Table的內容";
        }
    }
}

