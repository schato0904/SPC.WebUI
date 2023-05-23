using System;
using System.Web;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// CookieHelper의 요약 설명입니다.
    /// </summary>
    public class CookieHelper
    {
        HttpContext ct = HttpContext.Current;
        HttpCookie cookie;
        string base_domain;
        string domain;
        string port;
        string cookie_name;

        public CookieHelper()
        {

            cookie_name = "CTF.SPC";
            cookie = ct.Request.Cookies[cookie_name];
            port = ct.Request.ServerVariables.Get("SERVER_PORT");
            domain = port.Equals("80") || port.Equals("443") ? ct.Request.ServerVariables.Get("SERVER_NAME") : String.Format("{0}:{1}", ct.Request.ServerVariables.Get("SERVER_NAME"), port);

            base_domain = System.Configuration.ConfigurationManager.AppSettings.Get("defaultURL");

            if (!domain.ToLower().Equals(base_domain))
                domain = "localhost";

            if (cookie == null)
                cookie = new HttpCookie(cookie_name);

            cookie.Path = "/";
            if (!domain.Equals("localhost"))
                cookie.Domain = domain;
        }

        public void SetCookiesWithOutEncoding(string name, string value)
        {
            cookie[name] = value;
            ct.Response.Cookies.Add(cookie);
        }

        public void SetCookies(string name, string value)
        {
            Functions.Encrypts _Encrypts = new Functions.Encrypts();
            cookie[name] = String.IsNullOrEmpty(value) ? value : _Encrypts.EncodeTo64(value);
            ct.Response.Cookies.Add(cookie);
        }

        public string GetCookiesWithOutDecoding(string name)
        {
            string strR = "";
            if (cookie != null && !String.IsNullOrEmpty(cookie[name]))
            {
                strR = cookie[name];
            }
            return strR;
        }

        public string GetCookies(string name)
        {
            string strR = "";
            if (cookie != null && !String.IsNullOrEmpty(cookie[name]))
            {
                Functions.Encrypts _Encrypts = new Functions.Encrypts();
                strR = _Encrypts.DecodeFrom64(cookie[name]);
            }
            return strR;
        }

        public void ICreate_Cookie()
        {
            cookie.Expires = DateTime.Now.AddHours(5);
            ct.Response.Cookies.Add(cookie);
        }

        public void IDispose_Cookie()
        {
            cookie.Expires = DateTime.Now.AddHours(-1);
            ct.Response.Cookies.Add(cookie);
        }
    }
}