using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading;
using System.Globalization;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.User.Biz;
using CTF.Web.Framework.Helper;
using System.Web.Security;

namespace SPC.WebUI.API.Common
{
    /// <summary>
    /// csLogin의 요약 설명입니다.
    /// </summary>
    public class csLogin : IHttpHandler
    {
        public ctfLoginInfo LoginInfo = new ctfLoginInfo();
        public Functions.Encrypts Encrypts = new Functions.Encrypts();
        public Functions.GlobalResource resource = new Functions.GlobalResource();
        const string KEY = "spckey";

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = GetResponse(context);

            if (context.Request.Params["es"] == null)
            {
                response.Write("파라미터가 없습니다!!");
                response.End();
            }

            string m_sEncryptString = context.Request.Params["es"];

            if (String.IsNullOrEmpty(m_sEncryptString))
            {
                response.Write("해독할 암호키가 없습니다!!");
                response.End();
            }

            string currDT = DateTime.Now.ToString("yyyyMMddHH");
            string decryptString = String.Empty;
            bool bExecute = false;

            try
            {
                decryptString = Encrypts.RSADecryptString(m_sEncryptString.Replace(" ", "+"));

                string[] decryptArray = decryptString.Split('+');

                if (currDT.Equals(decryptArray[3]))
                {
                    SetLoginUserControl(context);
                    
                    string ErrorMsg = String.Empty;

                    if (!doLogin(decryptArray[0], decryptArray[1], decryptArray[2], context, out ErrorMsg))
                    {
                        throw new Exception(ErrorMsg);
                    }
                    else
                    {
                        bExecute = true;
                    }
                }
                else
                {
                    throw new Exception("인증시간이 만료되었습니다. 다시 로그인을 시도해주세요");
                }

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.AppendFormat("User ID : {0}<br/>", decryptArray[0]);
                //sb.AppendFormat("User PW : {0}<br/>", decryptArray[1]);
                //sb.AppendFormat("Comp CD : {0}<br/>", decryptArray[2]);
                //sb.AppendFormat("Curr DT : {0}<br/>", decryptArray[3]);

                //response.Write(sb.ToString());
            }
            catch (Exception ex)
            {
                response.Write(String.Format("암호해독중 장애가 발생하였습니다<br/>Error Message : {0}", ex.Message));
                response.End();
            }

            if (true == bExecute)
            {
                response.Redirect("~/MainForm.aspx");
            }
        }

        private static HttpResponse GetResponse(HttpContext context)
        {
            HttpResponse response = context.Response;
            return response;
        }

        #region 사이트 별 로그인 페이지 사용자 컨트럴을 로드한다
        void SetLoginUserControl(HttpContext context)
        {
            HttpResponse response = GetResponse(context);
            DataSet ds = null;
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetLoginPageInfo(out errMsg);
            }

            bool bExistsDataSetWhitoutCount = ds != null && ds.Tables.Count > 0;

            if (!bExistsDataSetWhitoutCount)
            {
                response.Cookies[KEY]["LOGINPGMID"] = "";
                response.Cookies[KEY]["USEBOARD"] = "False";
                response.Cookies[KEY]["CSRCOMPCD"] = "";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                LoginInfo.F_LOGINPGMID = dr["F_LOGINPGMID"].ToString();
                LoginInfo.F_COMPCD = dr["F_COMPCD"].ToString();
                LoginInfo.F_COMPNMKR = dr["F_COMPNMKR"].ToString();
                LoginInfo.F_COMPNMUS = dr["F_COMPNMUS"].ToString();
                LoginInfo.F_COMPNMCN = dr["F_COMPNMCN"].ToString();
                LoginInfo.F_COMPCOPY = dr["F_COMPCOPY"].ToString();
                LoginInfo.F_USEMULTILANG = (Boolean)dr["F_USEMULTILANG"];
                LoginInfo.F_USELANGKR = (Boolean)dr["F_USELANGKR"];
                LoginInfo.F_USELANGUS = (Boolean)dr["F_USELANGUS"];
                LoginInfo.F_USELANGCN = (Boolean)dr["F_USELANGCN"];
                LoginInfo.F_USEBOARD = (Boolean)dr["F_USEBOARD"];
                LoginInfo.F_ENCRTPTPW = (Boolean)dr["F_ENCRTPTPW"];
                LoginInfo.F_CSRCOMPCD = dr["F_CSRCOMPCD"].ToString();

                response.Cookies[KEY]["LOGINPGMID"] = LoginInfo.F_LOGINPGMID;
                response.Cookies[KEY]["USEBOARD"] = LoginInfo.F_USEBOARD.ToString();
                response.Cookies[KEY]["CSRCOMPCD"] = LoginInfo.F_CSRCOMPCD;
            }
        }
        #endregion

        bool doLogin(string id, string pw, string compcd, HttpContext context, out string ErrorMsg)
        {
            bool bExecute = false;
            HttpResponse response = GetResponse(context);

            DataSet ds = null;
            string loginResult = "Unknown Error";
            string userLangClsCd = "ko-KR";
            string userLangClsType = "KR";

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(userLangClsCd);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(userLangClsCd);

            #region 사용자 입력정보
            string userID = id;
            string userPW = pw;
            string userTP = "";
            #endregion

            #region 비밀번호 암호화(SHA1)
            userPW = !LoginInfo.F_ENCRTPTPW ? userPW : Encrypts.HashPasswordToString(userPW, "SHA1");
            #endregion

            string errMsg = String.Empty;

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();

            using (UserBiz biz = new UserBiz())
            {
                oParamDic.Add("F_USERID", userID);
                oParamDic.Add("F_USERPW", userPW);
                oParamDic.Add("F_LANGTP", userLangClsType);

                ds = biz.ProcLogin(oParamDic, out errMsg);
            }

            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                ErrorMsg = "존재하지 않는 아이디입니다.";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];

                if (!(Boolean)dr["F_USERPWCORRECT"])
                {
                    ErrorMsg = "비밀번호가 잘못되었습니다.";
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userID, false);

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
                    response.Cookies[KEY]["BYPASSURL"] = "";
                    response.Cookies[KEY]["ENCRTPTPW"] = HttpUtility.UrlEncode(!LoginInfo.F_ENCRTPTPW ? "0" : "1");
                    response.Cookies[KEY]["CSLOGIN"] = "0";

                    response.Cookies[KEY].Expires = DateTime.Now.AddHours(24);

                    ErrorMsg = "";
                    bExecute = true;
                }
            }

            return bExecute;
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