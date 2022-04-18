namespace Advtek.Utility.UserException
{
    using System;

    public class FieldNotMatchException : Exception
    {
        public override string ToString()
        {
            return "Grid的欄位數與FieldList的欄位數不合";
        }
    }
}

