namespace Advtek.Utility.UserException
{
    using System;

    public class FieldListNotSetException : Exception
    {
        public override string ToString()
        {
            return "FieldList沒有設定";
        }
    }
}

