namespace Advtek.Utility.UserException
{
    using System;

    public class SegUserIndexOutOfBoundException : Exception
    {
        public override string ToString()
        {
            return "SegUserIndex欄位所指定的值超過您所提供的ArrayList的個數";
        }
    }
}

