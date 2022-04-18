namespace Advtek.Utility.UserException
{
    using System;

    public class ConfigurationException : Exception
    {
        public override string ToString()
        {
            return "web.config設定有誤";
        }
    }
}

