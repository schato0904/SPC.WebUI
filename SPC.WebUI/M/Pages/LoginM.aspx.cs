using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DevExpress.Web;
using CTF.Web.Framework.Component;
using CTF.Web.Framework.Helper;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.User.Biz;

namespace SPC.WebUI.M.Pages
{
    public partial class LoginM : LoginBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        private readonly ctfLoginInfo LoginInfo = new ctfLoginInfo();
        const string KEY = "spckey";
        //protected bool bUseBootStrap
        //{
        //    get
        //    {
        //        switch (LoginInfo.F_LOGINPGMID)
        //        {
        //            default:
        //                return true;
        //            case "oto":
        //            case "ctf":
        //            case "heasung":
        //                return false;
        //        }
        //    }
        //}
        #endregion

        #endregion

        #region Event

        #region Page Init
        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // csLogin 전용인 경우 에러처리
            //if (CommonHelper.GetAppSectionsString("csLogin").Equals("Y"))
            //{
            //    //Redirect HTTP errors to HttpError page
            //    Server.Transfer("~/Pages/ERROR/Login.aspx");
            //}
            //else
            //{
            //    if (this.User.Identity.IsAuthenticated)
            //    {
            //        if (isGoodCookie())
            //        {
            //            string pLangClsCd = "ko-KR";
            //            if (base.Context.Request.Cookies[KEY] != null) pLangClsCd = HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"]);

            //            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.gsLANGCD);
            //            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.gsLANGCD);
            //            base.InitializeCulture();

            //            Response.Redirect("MainForm.aspx");
            //        }
            //    }
            //}
        }
        #endregion

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }
            if (IsCallback)
            {
            }
            SetLoginUserControl();
        }
        #endregion

        #endregion

        #region 사용자 정의 메소드

        #region 사이트 별 로그인 페이지 사용자 컨트럴을 로드한다
        void SetLoginUserControl()
        {
            DataSet ds = null;
            Control userControl = null;

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetLoginPageInfo(out errMsg);
            }

            if (!bExistsDataSetWhitoutCount(ds))
            {
                Response.Cookies[KEY]["LOGINPGMID"] = "";
                Response.Cookies[KEY]["USEBOARD"] = "False";
                Response.Cookies[KEY]["CSRCOMPCD"] = "";
                userControl = Page.LoadControl("~/Resources/controls/login/login.ascx");
                userControl.ID = "loginControl";
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

                Response.Cookies[KEY]["LOGINPGMID"] = LoginInfo.F_LOGINPGMID;
                Response.Cookies[KEY]["USEBOARD"] = LoginInfo.F_USEBOARD.ToString();
                Response.Cookies[KEY]["CSRCOMPCD"] = LoginInfo.F_CSRCOMPCD;

                string userControlPath = Server.MapPath(String.Format("~/Resources/controls/login/{0}/login.ascx", LoginInfo.F_LOGINPGMID));

                if (System.IO.File.Exists(userControlPath))
                    userControl = Page.LoadControl(String.Format("~/Resources/controls/login/{0}/login.ascx", LoginInfo.F_LOGINPGMID), LoginInfo);
                else
                    userControl = Page.LoadControl("~/Resources/controls/login/login.ascx");

                userControl.ID = "loginControl";
            }

            //pHolder.Controls.Add(userControl);
        }
        #endregion

        #region Log In
        protected void Login(object source, DevExpress.Web.CallbackEventArgs e)
        {
            Thread.Sleep(500); //Paused for demonstrative purposes

            UserControl loginControl = (UserControl)this.Page.FindControl("loginControl");

            DataSet ds = null;
            string[] loginResult = { "2", "Unknown Error" };
            string userLangClsCd = "ko-KR";
            string userLangClsType = "KR";

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(userLangClsCd);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(userLangClsCd);

            #region 사용자 입력정보
            string userID = txtUserID.Text;
            string userPW = txtUserPW.Text;
            #endregion

            #region 비밀번호 암호화(SHA1)
            userPW = !LoginInfo.F_ENCRTPTPW ? userPW : UF.Encrypts.HashPasswordToString(userPW, "SHA1");
            #endregion

            string errMsg = String.Empty;

            using (UserBiz biz = new UserBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_USERID", userID);
                oParamDic.Add("F_USERPW", userPW);
                oParamDic.Add("F_LANGTP", userLangClsType);

                ds = biz.ProcLogin(oParamDic, out errMsg);
            }

            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                loginResult = new string[] { "0", this.GetMessage("LOGIN_FAILURE_ID") };
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];

                if (!(Boolean)dr["F_USERPWCORRECT"])
                {
                    loginResult = new string[] { "9", this.GetMessage("LOGIN_FAILURE_PASSWORD") };
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userID, false);

                    HttpCookie cookieWebID = new HttpCookie("WebID") { Value = userID, Expires = DateTime.MaxValue };
                    Response.Cookies.Add(cookieWebID);

                    Response.Cookies[KEY]["LANGCD"] = HttpUtility.UrlEncode(userLangClsCd);
                    Response.Cookies[KEY]["COMPCD"] = HttpUtility.UrlEncode(dr["F_COMPCD"].ToString());
                    Response.Cookies[KEY]["COMPNM"] = HttpUtility.UrlEncode(dr["F_COMPNM"].ToString());
                    Response.Cookies[KEY]["FACTCD"] = HttpUtility.UrlEncode(dr["F_FACTCD"].ToString());
                    Response.Cookies[KEY]["FACTNM"] = HttpUtility.UrlEncode(dr["F_FACTNM"].ToString());
                    Response.Cookies[KEY]["USERID"] = HttpUtility.UrlEncode(dr["F_USERID"].ToString());
                    Response.Cookies[KEY]["USERNM"] = HttpUtility.UrlEncode(dr["F_USERNM"].ToString());
                    Response.Cookies[KEY]["DEPARTCD"] = HttpUtility.UrlEncode(dr["F_DEPARTCD"].ToString());
                    Response.Cookies[KEY]["DEPARTNM"] = HttpUtility.UrlEncode(dr["F_DEPARTNM"].ToString());
                    Response.Cookies[KEY]["GRADECD"] = HttpUtility.UrlEncode(dr["F_GRADECD"].ToString());
                    Response.Cookies[KEY]["GRADENM"] = HttpUtility.UrlEncode(dr["F_GRADENM"].ToString());
                    Response.Cookies[KEY]["GROUPCD"] = HttpUtility.UrlEncode(dr["F_GROUPCD"].ToString());
                    Response.Cookies[KEY]["STATUS"] = HttpUtility.UrlEncode(dr["F_STATUS"].ToString());
                    Response.Cookies[KEY]["MONITORINGYN"] = HttpUtility.UrlEncode(dr["F_MONITORINGYN"].ToString());
                    Response.Cookies[KEY]["MOBILENO"] = HttpUtility.UrlEncode(dr["F_MOBILENO"].ToString());
                    Response.Cookies[KEY]["EMAIL"] = HttpUtility.UrlEncode(dr["F_EMAIL"].ToString());
                    Response.Cookies[KEY]["UCLLCL"] = HttpUtility.UrlEncode(dr["F_UCLLCL"].ToString());
                    Response.Cookies[KEY]["DEV"] = HttpUtility.UrlEncode(dr["F_DEV"].ToString());
                    Response.Cookies[KEY]["MASTERCHK"] = HttpUtility.UrlEncode(dr["F_MASTERCHK"].ToString());
                    if (CommonHelper.GetIP4Address().Contains("192."))
                        Response.Cookies[KEY]["BYPASSURL"] = HttpUtility.UrlEncode(dr["F_PARM02"].ToString());
                    else
                        Response.Cookies[KEY]["BYPASSURL"] = HttpUtility.UrlEncode(dr["F_PARM03"].ToString());
                    Response.Cookies[KEY]["CSLOGIN"] = "0";

                    Response.Cookies[KEY].Expires = DateTime.Now.AddHours(24);

                    loginResult = new string[] { "1", "DIOF/DIOF0101.aspx" };
                }
            }

            ASPxCallback callbackControl = source as ASPxCallback;
            callbackControl.JSProperties["cpResultCode"] = loginResult[0];
            callbackControl.JSProperties["cpResultMsg"] = loginResult[1];
        }
        #endregion

        #endregion
    }
}