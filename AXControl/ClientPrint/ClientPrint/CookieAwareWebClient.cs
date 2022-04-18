using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Advtek.Web
{
    public class CookieAwareWebClient : WebClient
    {
        private CookieContainer cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = cookieContainer;
            }
            return request;
        }
    }
}
