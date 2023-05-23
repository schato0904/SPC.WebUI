using System;
using System.Web;

using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.Security;
using CTF.Web.Framework.Helper;
using SPC.User.Biz;

namespace SPC.WebUI.API.Common.ByPass
{
    /// <summary>
    /// bypass의 요약 설명입니다.
    /// </summary>
    public class bypass : IHttpHandler
    {
        Functions.Encrypts UFEncrypts = new Functions.Encrypts();
        const string KEY = "spckey";

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            
            try
            {
                string m_sUserEncryptInfo = context.Request.Params["pUSIF"];

                if (String.IsNullOrEmpty(m_sUserEncryptInfo))
                    throw new Exception("Wrong Connecting(had no parameters)");

                string[] m_sUserDecryptInfo = UFEncrypts.RSADecryptString(m_sUserEncryptInfo).Split('|');

                ByPassLogin(m_sUserDecryptInfo[0], m_sUserDecryptInfo[1], response);

                LogHelper.LogWrite(new Exception(String.Format("{0} 사용자가 ByPass로 로그인했습니다.", m_sUserDecryptInfo[0])), "ByPassLogin");
            }
            catch (Exception e)
            {
                LogHelper.LogWrite(e, "ByPassLogin");
                response.Write(String.Format("<script type=\"text/javascript\">alert('협력사 SPC 로그인 중 장애가 발생했습니다.\\r장애원인 : {0}');</script>", e.Message.Replace("'", "")));
                response.End();
            }

            response.Redirect("../../../MainForm.aspx");
        }

        string InitLangClsTp(string userLangClsCd)
        {
            switch (userLangClsCd)
            {
                default:
                    return "";
                case "ko-KR":
                    return "KR";
                case "en-US":
                    return "US";
                case "zh-CN":
                    return "CN";
                case "ja-JP":
                    return "JP";
            }
        }

        void ByPassLogin(string USERID, string PGMID, HttpResponse response)
        {
            DataSet ds = null;
            string userLangClsCd = CommonHelper.GetAppSectionsString("InitLangClsCd");
            string userLangClsType = InitLangClsTp(userLangClsCd);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(userLangClsCd);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(userLangClsCd);

            string errMsg = String.Empty;

            using (UserBiz biz = new UserBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_USERID", USERID);
                oParamDic.Add("F_LANGTP", userLangClsType);

                ds = biz.ProcByPassLogin(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
                throw new Exception(String.Format("Wrong Connecting({0})", errMsg));

            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                throw new Exception(String.Format("Wrong Connecting(not found User ID<{0}>)", USERID));
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];

                FormsAuthentication.SetAuthCookie(USERID, false);

                response.Cookies["WebID"].Expires = DateTime.Now.AddDays(-1);
                response.Cookies[KEY]["LANGCD"] = HttpUtility.UrlEncode(userLangClsCd);
                response.Cookies[KEY]["COMPCD"] = HttpUtility.UrlEncode(dr["F_COMPCD"].ToString());
                response.Cookies[KEY]["COMPNM"] = HttpUtility.UrlEncode(dr["F_COMPNM"].ToString());
                response.Cookies[KEY]["FACTCD"] = HttpUtility.UrlEncode(dr["F_FACTCD"].ToString());
                response.Cookies[KEY]["FACTNM"] = HttpUtility.UrlEncode(dr["F_FACTNM"].ToString());
                response.Cookies[KEY]["USERID"] = HttpUtility.UrlEncode(dr["F_USERID"].ToString());
                response.Cookies[KEY]["USERNM"] = HttpUtility.UrlEncode(dr["F_USERNM"].ToString());
                response.Cookies[KEY]["DEPARTCD"] = HttpUtility.UrlEncode(dr["F_DEPARTCD"].ToString());
                response.Cookies[KEY]["DEPARTNM"] = HttpUtility.UrlEncode(dr["F_DEPARTNM"].ToString());
                response.Cookies[KEY]["GRADECD"] = HttpUtility.UrlEncode(dr["F_GRADECD"].ToString());
                response.Cookies[KEY]["GRADENM"] = HttpUtility.UrlEncode(dr["F_GRADENM"].ToString());
                response.Cookies[KEY]["GROUPCD"] = HttpUtility.UrlEncode(dr["F_GROUPCD"].ToString());
                response.Cookies[KEY]["STATUS"] = HttpUtility.UrlEncode(dr["F_STATUS"].ToString());
                response.Cookies[KEY]["MONITORINGYN"] = HttpUtility.UrlEncode(dr["F_MONITORINGYN"].ToString());
                response.Cookies[KEY]["MOBILENO"] = HttpUtility.UrlEncode(dr["F_MOBILENO"].ToString());
                response.Cookies[KEY]["EMAIL"] = HttpUtility.UrlEncode(dr["F_EMAIL"].ToString());
                response.Cookies[KEY]["UCLLCL"] = HttpUtility.UrlEncode(dr["F_UCLLCL"].ToString());
                response.Cookies[KEY]["DEV"] = HttpUtility.UrlEncode(dr["F_DEV"].ToString());
                response.Cookies[KEY]["MASTERCHK"] = HttpUtility.UrlEncode(dr["F_MASTERCHK"].ToString());
                if (CommonHelper.GetIP4Address().Contains("192."))
                    response.Cookies[KEY]["BYPASSURL"] = HttpUtility.UrlEncode(dr["F_PARM02"].ToString());
                else
                    response.Cookies[KEY]["BYPASSURL"] = HttpUtility.UrlEncode(dr["F_PARM03"].ToString());
                response.Cookies[KEY]["LOGINPGMID"] = !String.IsNullOrEmpty(PGMID) ? PGMID : "";
                response.Cookies[KEY]["CSLOGIN"] = "0";

                response.Cookies[KEY].Expires = DateTime.Now.AddHours(24);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}