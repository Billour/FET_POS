namespace Advtek.Utility.UserException
{
    using System;

    public class SegmentValueNullPointerException : Exception
    {
        public override string ToString()
        {
            return "將SegType設為User, 但傳入的參數是null";
        }
    }
}

