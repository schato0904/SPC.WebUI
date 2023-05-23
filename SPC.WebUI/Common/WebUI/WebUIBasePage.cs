using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using CTF.Web.Framework.Helper;
using System.Collections.Generic;
using SPC.Common.Biz;
using SPC.SYST.Biz;
using SPC.MEAS.Biz;


namespace SPC.WebUI.Common
{
    /// <summary>
    /// 기능명 : 일반화면 PageBase
    /// 작성자 : SSH
    /// 작성일 : 2014-03-15
    /// 수정일 :
    /// 설  명 :
    /// </summary>
    public class WebUIBasePage : System.Web.UI.Page
    {
        #region 변수선언

        #region 함수선언 - Functions
        /// <summary>
        /// 함수선언
        /// </summary>
        public Function UF = new Function();

        /// <summary>
        /// 관련함수클래스
        /// </summary>
        public class Function
        {
            /// <summary>
            /// 암호화관련함수
            /// </summary>
            public Functions.Encrypts Encrypts = new Functions.Encrypts();
            /// <summary>
            /// 문자관련함수
            /// </summary>
            public Functions.Strings String = new Functions.Strings();
            /// <summary>
            /// 숫자관련함수
            /// </summary>
            public Functions.Numbers Number = new Functions.Numbers();
            /// <summary>
            /// 시스템관련함수
            /// </summary>
            public Functions.Systems System = new Functions.Systems();
            /// <summary>
            /// 컨트롤관련함수
            /// </summary>
            public Functions.Ctrols Ctrol = new Functions.Ctrols();
            /// <summary>
            /// 엑셀관련함수
            /// </summary>
            public Functions.Excels Excels = new Functions.Excels();
            /// <summary>
            /// 날짜관련함수
            /// </summary>
            public Functions.Dates Date = new Functions.Dates();
            /// <summary>
            /// 페이지관련함수
            /// </summary>
            public Functions.Pages Page = new Functions.Pages();
            /// <summary>
            /// GlobalResource 함수
            /// </summary>
            public Functions.GlobalResource resource = new Functions.GlobalResource();
        }
        #endregion

        #region DB Parametar Dictionary
        public System.Collections.Generic.Dictionary<string, string> oParamDic = new System.Collections.Generic.Dictionary<string, string>();
        public System.Collections.Generic.Dictionary<string, object> oOutParamDic = new System.Collections.Generic.Dictionary<string, object>();
        public System.Collections.Generic.List<object> oOutParamList = new System.Collections.Generic.List<object>();
        #endregion

        private const string KEY = "spckey";

        private bool _CreateAuth = false;
        private bool _ReadAuth = false;
        private bool _UpdateAuth = false;
        private bool _DeleteAuth = false;
        private bool _PrintAuth = false;

        private int _MnuSn = 0;
        private string _MnuNm = String.Empty;
        private string _RootURL = String.Empty;

        #endregion

        #region 생성자 - WebUIBasePage
        ///<summary>
        /// 생성자(핸들러지정)        
        ///</summary>
        public WebUIBasePage()
        {
            //this.Init += new EventHandler(PageBase_Init);
            this.Load += new EventHandler(PageBase_Load);
            if (CommonHelper.GetAppSectionsString("devMode").Equals("0"))
                this.Error += new EventHandler(PageBase_Error);
        }
        #endregion

        //#region InitializeCulture
        ///// <summary>
        ///// 문자셋 초기화
        ///// </summary>
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.gsLANGCD);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.gsLANGCD);
        //    base.InitializeCulture();
        //}
        //#endregion

        #region PreInit
        /// <summary>
        /// Preview Initialize
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (isGoodCookie())
                {
                    if (!this.RetrieveAuthority())
                    {
                        //권한정보가 없는경우
                        Response.Redirect("~/Pages/ERROR/403.aspx");
                    }

                    string pLangClsCd = "ko-KR";
                    if (base.Context.Request.Cookies[KEY] != null) pLangClsCd = HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"]);

                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.gsLANGCD);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.gsLANGCD);
                    base.InitializeCulture();
                }
                else
                {
                    if (!IsCallback)
                    {
                        //쿠키정보가 없는경우
                        Response.Write(String.Format("<script type=\"text/javascript\">top.location.href = '{0}'</script>", ResolveUrl("~/Pages/ERROR/401.aspx")));
                        Response.End();
                    }
                    else
                    {
                        throw new Exception("isLogOut");
                    }
                }
            }
            else
            {
                if (!IsCallback)
                {
                    //쿠키정보가 없는경우
                    Response.Write(String.Format("<script type=\"text/javascript\">top.location.href = '{0}'</script>", ResolveUrl("~/Pages/ERROR/401.aspx")));
                    Response.End();
                }
                else
                {
                    throw new Exception("isLogOut");
                }
            }

            base.OnPreInit(e);
        }
        #endregion

        //#region Page Init - PageBase_Init
        ///// <summary>
        ///// Page Init
        ///// </summary>
        ///// <param name="sender">이벤트객체</param>
        ///// <param name="e">이벤트정보</param>
        //private void PageBase_Init(object sender, EventArgs e)
        //{
        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        if (isGoodCookie())
        //        {
        //            if (!this.RetrieveAuthority())
        //            {
        //                //권한정보가 없는경우
        //                Response.Redirect("~/Pages/ERROR/403.aspx");
        //            }

        //            string pLangClsCd = "ko-KR";
        //            if (base.Context.Request.Cookies[KEY] != null) pLangClsCd = HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"]);

        //            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.gsLANGCD);
        //            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.gsLANGCD);
        //            base.InitializeCulture();
        //        }
        //        else
        //        {
        //            //쿠키정보가 없는경우
        //            Response.Write(String.Format("<script type=\"text/javascript\">top.location.href = '{0}'</script>", ResolveUrl("~/Pages/ERROR/401.aspx")));
        //            Response.End();
        //        }
        //    }
        //    else
        //    {
        //        //쿠키정보가 없는경우
        //        Response.Write(String.Format("<script type=\"text/javascript\">top.location.href = '{0}'</script>", ResolveUrl("~/Pages/ERROR/401.aspx")));
        //        Response.End();
        //    }
        //}
        //#endregion

        #region 페이지로드 - PageBase_Load
        /// <summary>
        /// 페이지로드
        /// </summary>
        /// <param name="sender">이벤트객체</param>
        /// <param name="e">이벤트정보</param>
        private void PageBase_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }
        #endregion

        #region 페이지에러 - PageBase_Error
        /// <summary>
        /// Page Init
        /// </summary>
        /// <param name="sender">이벤트객체</param>
        /// <param name="e">이벤트정보</param>
        private void PageBase_Error(object sender, EventArgs e)
        {
            // ErrorPage
            string errorPage = "{0}?ErrorPage={1}&ErrorCode={2}&ErrorMsg={3}&pPARAM={4}";
            // Get the Page Name
            string _pageNM = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            // Get the exception object.
            Exception exc = Server.GetLastError();

            //// Handle HTTP errors
            //if (exc.GetType() == typeof(HttpException))
            //{
            //    HttpException httpException = exc as HttpException;

            //    // 에러페이지의 Url을 읽어온다.
            //    errorPage = String.Format(errorPage,
            //        CommonHelper.GetAppSectionsString("ErrorPage"),
            //        _pageNM,
            //        httpException.ErrorCode);
            //}

            // 에러페이지의 Url을 읽어온다.
            errorPage = String.Format(errorPage,
                CommonHelper.GetAppSectionsString("ErrorPage"),
                _pageNM,
                "500",
                Server.UrlEncode(exc.Message),
                HttpContext.Current.Request.QueryString.Get("pParam"));

            // 에러로그를 남긴다.
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("  1) ErrorPage       : ");
            sb.AppendLine(_pageNM);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine("  2) ErrorMessage       : ");
            sb.AppendLine(exc.Message);
            sb.AppendLine("--------------------------------------------------------------------------------");
            LogHelper.LogWrite(sb.ToString());

            Server.ClearError();

            //Redirect HTTP errors to HttpError page
            Server.Transfer(errorPage);
        }
        #endregion

        #region 속성설정
        #endregion

        #region 로그인한 사용자 정보
        public string gsLANGCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"]); } }
        public string gsLANGTP
        {
            get
            {
                switch (gsLANGCD)
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
        }
        public string gsCOMPCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["COMPCD"]); } }
        public string gsCOMPNM { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["COMPNM"]); } }
        public string gsFACTCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["FACTCD"]); } }
        public string gsFACTNM { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["FACTNM"]); } }
        public string gsUSERID { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["USERID"]); } }
        public string gsUSERNM { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["USERNM"]); } }
        public string gsDEPARTCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["DEPARTCD"]); } }
        public string gsDEPARTNM { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["DEPARTNM"]); } }
        public string gsGRADECD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["GRADECD"]); } }
        public string gsGRADENM { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["GRADENM"]); } }
        public string gsGROUPCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["GROUPCD"]); } }
        public string gsSTATUS { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["STATUS"]); } }
        public string gsMONITORINGYN { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["MONITORINGYN"]); } }
        public string gsMOBILENO { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["MOBILENO"]); } }
        public string gsEMAIL { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["EMAIL"]); } }
        public string gsUCLLCL { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["UCLLCL"]); } }
        public string gsDEV { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["DEV"]); } }
        public string gsMASTERCHK { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["MASTERCHK"]); } }
        public bool gsVENDOR { get { return !Boolean.Parse(gsMASTERCHK); } }
        public string gsBYPASSURL { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["BYPASSURL"]); } }
        public string gsLOGINPGMID { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LOGINPGMID"]); } }
        public string gsUSEBOARD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["USEBOARD"]); } }
        public string gsCSRCOMPCD { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["CSRCOMPCD"]); } }
        public string gsINSPWORKGBN { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["INSPWORKGBN"]); } }
        public string gsENCRTPTPW { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["ENCRTPTPW"]); } }
        #endregion

        #region 쿠키관리

        #region  쿠키체크
        /// <summary>
        /// 쿠키체크
        /// </summary>
        /// <returns>bool</returns>
        private bool isGoodCookie()
        {
            bool Isbool = false;

            if (Context != null)
            {
                if (base.Context.Request.Cookies[KEY] != null)
                {
                    if (this.gsCOMPCD != null &&
                        this.gsCOMPNM != null &&
                        this.gsFACTCD != null &&
                        this.gsFACTNM != null &&
                        this.gsUSERID != null &&
                        this.gsUSERNM != null &&
                        this.gsDEPARTCD != null &&
                        this.gsDEPARTNM != null &&
                        this.gsGRADECD != null &&
                        this.gsGRADENM != null &&
                        this.gsGROUPCD != null &&
                        this.gsSTATUS != null &&
                        this.gsMONITORINGYN != null &&
                        this.gsMOBILENO != null &&
                        this.gsEMAIL != null &&
                        this.gsUCLLCL != null &&
                        this.gsDEV != null &&
                        this.gsMASTERCHK != null)
                    {
                        Isbool = true;
                    }
                }
            }

            return Isbool;
        }

        #endregion

        #region 쿠키삭제 - 로그아웃
        /// <summary>
        /// 쿠키삭제
        /// </summary>
        public void Logout_Cookie()
        {
            base.Context.Response.Cookies[KEY]["LANGCD"] = null;
            base.Context.Response.Cookies[KEY]["COMPCD"] = null;
            base.Context.Response.Cookies[KEY]["COMPNM"] = null;
            base.Context.Response.Cookies[KEY]["FACTCD"] = null;
            base.Context.Response.Cookies[KEY]["FACTNM"] = null;
            base.Context.Response.Cookies[KEY]["USERID"] = null;
            base.Context.Response.Cookies[KEY]["USERNM"] = null;
            base.Context.Response.Cookies[KEY]["DEPARTCD"] = null;
            base.Context.Response.Cookies[KEY]["DEPARTNM"] = null;
            base.Context.Response.Cookies[KEY]["GRADECD"] = null;
            base.Context.Response.Cookies[KEY]["GRADENM"] = null;
            base.Context.Response.Cookies[KEY]["GROUPCD"] = null;
            base.Context.Response.Cookies[KEY]["STATUS"] = null;
            base.Context.Response.Cookies[KEY]["MONITORINGYN"] = null;
            base.Context.Response.Cookies[KEY]["MOBILENO"] = null;
            base.Context.Response.Cookies[KEY]["EMAIL"] = null;
            base.Context.Response.Cookies[KEY]["UCLLCL"] = null;
            base.Context.Response.Cookies[KEY]["DEV"] = null;
            base.Context.Response.Cookies[KEY]["MASTERCHK"] = null;
            base.Context.Response.Cookies[KEY]["CSLOGIN"] = null;
            base.Context.Response.Cookies[KEY]["INSPWORKGBN"] = null;
            base.Context.Response.Cookies[KEY]["ENCRTPTPW"] = null;

            base.Context.Response.Cookies[KEY].Expires = DateTime.Now.AddDays(-60);
        }
        #endregion

        #endregion

        #region 현재페이지관리
        /// <summary>
        /// 현재 페이지의 Create 권한을 설정한다.
        /// </summary>
        public bool CrtAuth
        {
            get
            {
                return this._CreateAuth;
            }
        }

        /// <summary>
        /// 현재 페이지의 Read 권한을 설정한다.
        /// </summary>
        public bool RedAuth
        {
            get
            {
                return this._ReadAuth;
            }
        }

        /// <summary>
        /// 현재 페이지의 Update 권한을 설정한다.
        /// </summary>
        public bool UdtAuth
        {
            get
            {
                return this._UpdateAuth;
            }
        }

        /// <summary>
        /// 현재 페이지의 Delete 권한을 설정한다.
        /// </summary>
        public bool DelAuth
        {
            get
            {
                return this._DeleteAuth;
            }
        }
        /// <summary>
        /// 현재 페이지의 Print 권한을 설정한다.
        /// </summary>
        public bool PrtAuth
        {
            get
            {
                return this._PrintAuth;
            }
        }
        /// <summary>
        /// 현재 페이지의 이름을 얻는다.
        /// </summary>
        public string CrtMnuNm
        {
            get
            {
                return this._MnuNm;
            }
        }
        /// <summary>
        /// 현재 페이지의 메뉴일련번호를 얻는다.
        /// </summary>
        public int CrtMnuSn
        {
            get
            {
                return this._MnuSn;
            }
        }

        /// <summary>
        /// Root URL를 얻는다.
        /// </summary>
        public string CrtRootURL
        {
            get
            {
                return this._RootURL;
            }
        }
        #endregion

        #region 권한체크
        /// <summary>
        /// 현재 페이지에 대한 현재 로그인한 사용자의 권한정보        
        /// </summary>
        /// <returns>bool</returns>
        private bool RetrieveAuthority()
        {
            //string pLangClsCd = this.gsLANGCD.ToString();
            //string pEmpActCd = this.CrtEmpActCd.ToString();
            //int pMnuSn = this.RetrieveMnuSn();
            //string pCmpCd = this.CrtCmpCd;
            //string pDBClsCd = this.CrtDBClsCd;

            //bool blnAuthority = false;

            //if (this.CrtEmpActCd.Length > 0)
            //{
            //    blnAuthority = this.RetrieveEmpPageRight(pLangClsCd, pEmpActCd, pMnuSn, pCmpCd, pDBClsCd);
            //}
            //else
            //{
            //    blnAuthority = false;
            //}
            //return blnAuthority;
            return true;
        }

        /// <summary>
        /// 직원별페이지권한조회
        /// </summary>
        /// <param name="pLangClsCd">ko-KR:한국,en-US:미국,ja-JP:일본,zh-CN:중국</param>
        /// <param name="pEmpActCd">직원활동센터코드</param>
        /// <param name="pMnuSn">메뉴일련번호</param>
        /// <param name="pCmpCd">업체코드</param>
        /// <param name="pDBClsCd">DB구분코드</param>
        /// <returns>bool</returns>
        private bool RetrieveEmpPageRight(string pLangClsCd, string pEmpActCd, int pMnuSn, string pCmpCd, string pDBClsCd)
        {
            //CommonBizNTx biz = null;
            //DataSet ds = new DataSet();
            //bool bIsRight = false;

            //try
            //{
            //    //0:일반적인경우,-10:특수한경우
            //    if (pMnuSn > 0 || pMnuSn == -10)
            //    {
            //        biz = new CommonBizNTx();

            //        ds = biz.RetrieveEmpPageRight(pLangClsCd, pEmpActCd, pMnuSn, pCmpCd, pDBClsCd);

            //        if (ds != null && ds.Tables["TB_Page"] != null)
            //        {
            //            if (ds.Tables["TB_Page"].Rows.Count > 0)
            //            {
            //                this._CreateAuth = ds.Tables["TB_Page"].Rows[0]["CrtYn"].ToString() == "1" ? true : false;
            //                this._ReadAuth = ds.Tables["TB_Page"].Rows[0]["RedYn"].ToString() == "1" ? true : false;
            //                this._UpdateAuth = ds.Tables["TB_Page"].Rows[0]["UdtYn"].ToString() == "1" ? true : false;
            //                this._DeleteAuth = ds.Tables["TB_Page"].Rows[0]["DelYn"].ToString() == "1" ? true : false;
            //                this._PrintAuth = ds.Tables["TB_Page"].Rows[0]["PrtYn"].ToString() == "1" ? true : false;

            //                this._MnuSn = int.Parse(ds.Tables["TB_Page"].Rows[0]["MnuSn"].ToString());
            //                this._MnuNm = ReplaceChar(0, ds.Tables["TB_Page"].Rows[0]["MnuNms"].ToString());

            //                this.SetCookies(this._MnuSn, this._MnuNm, this._CreateAuth.ToString() + "|" + this._ReadAuth + "|" + this._UpdateAuth + "|" + this._DeleteAuth + "|" + _PrintAuth);

            //                bIsRight = this._ReadAuth;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //특수한 메뉴권한설정(0 ~ -9까지 사용)
            //        switch (pMnuSn)
            //        {
            //            case 0:
            //                //팝업페이지 : 현재메뉴일련번호의 세션정보가 Null인경우
            //                break;
            //            case -1:
            //                this._MnuSn = pMnuSn;
            //                this._MnuNm = "Home  &gt;  사이트맵";

            //                this.SetCookies(this._MnuSn, this._MnuNm, "false|true|false|false|false");
            //                break;
            //            case -2:
            //                break;                        
            //            default:
            //                break;
            //        }

            //        this._CreateAuth = true;
            //        this._ReadAuth = true;
            //        this._UpdateAuth = true;
            //        this._DeleteAuth = true;
            //        this._PrintAuth = true;

            //        bIsRight = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (biz != null)
            //    {
            //        biz.Dispose();
            //        biz = null;
            //    }
            //}
            //return bIsRight;
            return false;
        }
        #endregion

        #region 기타함수

        #endregion

        #region 메세지 관리
        /// <summary>
        /// 메세지 관리
        /// </summary>
        /// <param name="pMessageID">메세지ID</param>
        /// <returns></returns>
        public string GetMessage(string pMessageID)
        {
            return UF.resource.GetResource(pMessageID);
        }

        public static string GetMessageStatic(string pMessageID)
        {
            return StaticFunctions.staticGlobalResource.GetResource(pMessageID);
        }
        #endregion

        #region CommonCodeListCache
        void CreateCommonCodeCache(CommonCode commonCode)
        {
            commonCode.LoadCommonCode();
            CacheHelper.Add(commonCode, "SPCCommonCode");
        }

        public CommonCode CachecommonCode
        {
            get
            {
                CommonCode commonCode = new CommonCode();

                if (!CacheHelper.Exists("SPCCommonCode"))
                {
                    CreateCommonCodeCache(commonCode);
                }
                else
                {
                    CacheHelper.Get("SPCCommonCode", out commonCode);

                    if (null == commonCode)
                    {
                        CreateCommonCodeCache(commonCode);
                    }
                }

                return commonCode;
            }
        }

        public void RefreshCommonCodeCache()
        {
            if (CacheHelper.Exists("SPCCommonCode"))
            {
                CacheHelper.Clear("SPCCommonCode");
            }
        }
        #endregion

        #region 컨트롤 조작 함수
        #region 컨트롤 목록 조작
        /// <summary>
        /// 현재 페이지에서 id로 컨트롤 검색하여 반환
        /// </summary>
        /// <param name="id">검색대상 아이디</param>
        /// <returns></returns>
        public Control FindControlById(string id)
        {
            List<Control> tmp = new List<Control>();
            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page.HasControls() == false) return null;
            tmp.AddRange(page.Controls.Cast<Control>());
            while (true)
            {
                if (tmp.Count == 0) break;
                if (tmp[0].HasControls()) tmp.AddRange(tmp[0].Controls.Cast<Control>());
                if (!string.IsNullOrEmpty(tmp[0].ID) && tmp[0].ID == id) return tmp[0];
                tmp.RemoveAt(0);
            }
            return null;
        }

        /// <summary>
        /// 주어진 ControlCollection에서 특정 접두사(기본:src)로 시작하는 필드 수집하여 Dictionary<string, string>타입으로 반환
        /// </summary>
        /// <param name="ctrls">대상 컨트롤 컬렉션</param>
        /// <param name="prefix">대상 필드 컨트롤 접두사</param>
        /// <param name="removePrefix">Dictionary생성시 접두사 제거 여부</param>
        /// <param name="forceUpperCase">Dictionary생성시 키값 대문자 강제변환 여부</param>
        /// <returns></returns>
        public Dictionary<string, string> GetControls(ControlCollection ctrls, string prefix = "src", bool removePrefix = false, bool forceUpperCase = false)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<Control> tmpList = new List<Control>();
            if (ctrls == null || ctrls.Count == 0) return result;
            tmpList.AddRange(ctrls.Cast<Control>());
            while (true)
            {
                if (tmpList.Count == 0) break;
                if (!(tmpList[0] is WebUIBasePageUserControl) && tmpList[0].HasControls()) tmpList.AddRange(tmpList[0].Controls.Cast<Control>());
                if (!string.IsNullOrEmpty(tmpList[0].ID) && (tmpList[0].ID.StartsWith(prefix)))
                {
                    var tId = (forceUpperCase ? tmpList[0].ID.ToUpper() : tmpList[0].ID);
                    if (removePrefix) tId = tId.Substring(prefix.Length);

                    if (tmpList[0] is DevExpress.Web.ASPxComboBox)
                        result.Add(tId, ((tmpList[0] as DevExpress.Web.ASPxComboBox).Value ?? "").ToString());
                    else if (tmpList[0] is DevExpress.Web.ASPxRadioButtonList)
                        result.Add(tId, ((tmpList[0] as DevExpress.Web.ASPxRadioButtonList).Value ?? "").ToString());
                    else if (tmpList[0] is DevExpress.Web.ASPxCheckBox)
                        result.Add(tId, ((tmpList[0] as DevExpress.Web.ASPxCheckBox).Checked).ToString());
                    else if (tmpList[0] is DevExpress.Web.ASPxTextEdit)
                        result.Add(tId, (tmpList[0] as DevExpress.Web.ASPxTextEdit).Text);
                    else if (tmpList[0] is DevExpress.Web.ASPxLabel)
                        result.Add(tId, (tmpList[0] as DevExpress.Web.ASPxLabel).Text);
                    else if (tmpList[0] is WebUIBasePageUserControl)
                        result.Add(tId, (tmpList[0] as WebUIBasePageUserControl).GetValue());
                }
                tmpList.RemoveAt(0);
            }
            return result;
        }

        /// <summary>
        /// 주어진 ControlCollection에서 src로 시작하는 필드 수집하여 Dictionary<string, Control>타입으로 반환
        /// </summary>
        /// <param name="containerId">컨테이너 컨트롤 아이디</param>
        /// <param name="prefix">대상 필드 컨트롤 접두사</param>
        /// <param name="removePrefix">Dictionary생성시 접두사 제거 여부</param>
        /// <param name="forceUpperCase"></param>
        /// <returns></returns>
        public Dictionary<string, Control> GetControlList(string containerId, string prefix = "src", bool removePrefix = false, bool forceUpperCase = false)
        {
            return GetControlList(FindControlById(containerId), prefix, removePrefix, forceUpperCase);
        }

        /// <summary>
        /// 주어진 ControlCollection에서 src로 시작하는 필드 수집하여 Dictionary<string, Control>타입으로 반환
        /// </summary>
        /// <param name="ctrlContainer">컨테이너 컨트롤</param>
        /// <param name="prefix">대상 필드 컨트롤 접두사</param>
        /// <param name="removePrefix">Dictionary생성시 접두사 제거 여부</param>
        /// <param name="forceUpperCase"></param>
        /// <returns></returns>
        public Dictionary<string, Control> GetControlList(Control ctrlContainer, string prefix = "src", bool removePrefix = false, bool forceUpperCase = false)
        {
            Dictionary<string, Control> result = new Dictionary<string, Control>();
            List<Control> tmpList = new List<Control>();
            Control ctrl = ctrlContainer;

            if (ctrl.HasControls() == false) return null;
            tmpList.AddRange(ctrl.Controls.Cast<Control>());
            while (true)
            {
                if (tmpList.Count == 0) break;
                if (tmpList[0].HasControls()) tmpList.AddRange(tmpList[0].Controls.Cast<Control>());
                if (!string.IsNullOrEmpty(tmpList[0].ID) && (tmpList[0].ID.StartsWith(prefix)))
                {
                    var tId = forceUpperCase ? tmpList[0].ID.ToUpper() : tmpList[0].ID;
                    if (removePrefix)
                        result.Add(tId.Substring(prefix.Length), tmpList[0]);
                    else
                        result.Add(tId, tmpList[0]);
                }
                tmpList.RemoveAt(0);
            }
            return result;
        }

        /// <summary>
        /// 컨테이너 컨트롤에 포함된 필드 반환
        /// </summary>
        /// <param name="ctrlContainer">컨테이너 컨트롤</param>
        /// <param name="prefix">대상 필드 컨트롤 접두사</param>
        /// <param name="removePrefix">Dictionary생성시 키값 접두사 제외여부</param>
        /// <param name="forceUpperCase">Dictionary생성시 키값 대문자 강제변환 여부</param>
        /// <returns></returns>
        public Dictionary<string, string> GetControls(Control ctrlContainer, string prefix = "src", bool removePrefix = false, bool forceUpperCase = false)
        {
            if (ctrlContainer == null) return null;
            return GetControls(ctrlContainer.Controls, prefix, removePrefix, forceUpperCase);
        }

        /// <summary>
        /// 컨테이너 컨트롤 아이디로 해당 컨테이너에 포함된 필드 반환
        /// </summary>
        /// <param name="containerId">컨테이너 아이디</param>
        /// <param name="prefix">대상컨트롤 접두사</param>
        /// <param name="removePrefix">Dictionary생성시 키값 접두사 제외여부</param>
        /// <param name="forceUpperCase">Dictionary생성시 키값 대문자 강제변환 여부</param>
        /// <returns></returns>
        public Dictionary<string, string> GetControls(string containerId, string prefix = "src", bool removePrefix = false, bool forceUpperCase = false)
        {
            return GetControls(FindControlById(containerId), prefix, removePrefix, forceUpperCase);
        }
        #endregion
        #endregion

        #region 구분코드를 구한다
        public string GetCommonCodeText(CommonCode.CodeDic codeDic)
        {
            try
            {
                switch(gsLANGTP)
                {
                    default:
                        return "Empty";
                    case "KR":
                        return codeDic.COMMNMKR;
                    case "US":
                        return codeDic.COMMNMUS;
                    case "CN":
                        return codeDic.COMMNMCN;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetCommonCodeText(string code)
        {
            try
            {
                CommonCode.CodeDic codeDic = CachecommonCode[code.Substring(0, 2)];

                for (int i = 2; i < code.Length; i += 2)
                {
                    codeDic = codeDic[code.Substring(0, i + 2)];

                    // 코드가 존재하지 않을경우, 공백 반환
                    if (codeDic == null) return string.Empty;
                }

                switch (gsLANGTP)
                {
                    default:
                        return "Empty";
                    case "KR":
                        return codeDic.COMMNMKR;
                    case "US":
                        return codeDic.COMMNMUS;
                    case "CN":
                        return codeDic.COMMNMCN;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion

        #region 데이타셋 결과값 존재유무(Exists : true, Not Exists : false)

        #region 데이타셋 결과값 존재유무(Exists : true, Not Exists : false) - Table 있는경우
        public bool bExistsDataSet(DataSet ds)
        {
            return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) ? true : false;
        }
        #endregion

        #region 데이타셋 결과값 존재유무(Exists : true, Not Exists : false) - Table 없는경우
        public bool bExistsDataSetWhitoutCount(DataSet ds)
        {
            return (ds != null && ds.Tables.Count > 0) ? true : false;
        }
        #endregion

        #endregion

        #region Find UserControl Control Value

        #region 업체
        public string GetCompCD()
        {
            string resultCode = String.Empty;

            if (gsVENDOR)
                resultCode = gsCOMPCD;
            else
            {
                ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
                UserControl _UserControl = cph.FindControl("ucComp") as UserControl;

                if (_UserControl != null)
                {
                    DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMPCD") as DevExpress.Web.ASPxTextBox;
                    resultCode = _hidTextBox.Text;
                }
            }

            return resultCode;
        }
        #endregion

        #region 공장
        public string GetFactCD()
        {
            string resultCode = String.Empty;

            if (gsVENDOR)
                resultCode = gsFACTCD;
            else
            {
                ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
                UserControl _UserControl = cph.FindControl("ucFact") as UserControl;

                if (_UserControl != null)
                {
                    DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidFACTCD") as DevExpress.Web.ASPxTextBox;
                    resultCode = _hidTextBox.Text;
                }
            }

            return resultCode;
        }
        #endregion

        #region 반
        public string GetBanCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucBan") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidBANCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 라인
        public string GetLineCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucLine") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidLINECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 품목
        public string GetItemCD(string UserCtlNm = "ucItem")
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucItem") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidITEMCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 품목1
        public string GetItemCD1()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucItem1") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidITEMCD1") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 품목2
        public string GetItemCD2()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucItem2") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidITEMCD2") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정
        public string GetWorkCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWork") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정팝업
        public string GetWorkPOPCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWorkPOP") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKPOPCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정팝업1
        public string GetWorkPOPCD1()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWorkPOP1") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKPOPCD1") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정팝업2
        public string GetWorkPOPCD2()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWorkPOP2") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKPOPCD2") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion
        
        #region 검사항목
        public string GetInspItemCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucInspectionItem") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidINSPITEMCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion
       
        #region 검사유형
        public string GetInspectionCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucInspection") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidINSPECTION") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region From일자
        public string GetFromDt()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCFROMDT") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region To일자
        public string GetToDt()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCTODT") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region From일자1
        public string GetFromDt1()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate1") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCFROMDT1") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region To일자1
        public string GetToDt1()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate1") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCTODT1") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region From일자2
        public string GetFromDt2()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate2") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCFROMDT2") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region To일자2
        public string GetToDt2()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucDate2") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCTODT2") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공통코드
        public string GetCommonCode()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucCommonCode") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMMONCODECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 모델
        public string GetModelCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucModelDDL") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidMODELCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 주차년도
        public string GetWeekYear()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWeek") as UserControl;

            if (_UserControl != null)
            {

                DevExpress.Web.ASPxSpinEdit _hidTextBox = _UserControl.FindControl("spin_year") as DevExpress.Web.ASPxSpinEdit;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 주차FROM
        public string GetWeekFrom()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWeek") as UserControl;

            if (_UserControl != null)
            {

                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWeek1") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 주차TO
        public string GetWeekTO()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucWeek") as UserControl;

            if (_UserControl != null)
            {

                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWeek2") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 설비번호
        public string GetMachCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucMachine") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidMACHCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 설비타입
        public string GetMachTypeCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucMachine") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidMACHTYPECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region GetF_USERNM (사용자명)
        [System.Web.Services.WebMethod]
        public static string GetF_USERNM(string F_USERID)
        {
            System.Collections.Generic.Dictionary<string, string> oParamDic = new System.Collections.Generic.Dictionary<string, string>();
            DataSet ds = null;
            string errMsg = string.Empty;
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_USERID", F_USERID);
                //ds = biz.SYUSR01_LST(oParamDic, out errMsg);
            }

            result = GlobalFunction.FirstRowToDictionary(ds);

            if (result.Count == 0)
            {
                throw new Exception("일치하는 항목이 없습니다");
            }

            return GlobalFunction.SerializeJSON(result);
        }
        #endregion

        #region 설비
        public string GetEquipCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucEquip") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidEQUIPCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #endregion

        #region 지정된 컨트롤(패널)안의 데이터 Set, Get
        /// <summary>
        /// 지정된 컨테이너에 포함된 특정 접두사의 필드에 데이터 설정
        /// 그중, 설정 제외 필드는 설정하지 않는다.
        /// </summary>
        /// <param name="containerCtrl">컨테이너(패널) 컨트롤</param>
        /// <param name="srcDt">값을 설정할 데이터 테이블. 첫번째 Row만 사용함</param>
        /// <param name="exceptColumnList">캡션설정 제외 필드 목록</param>
        /// <param name="targetFieldPrefix">컨테이너에 포함된 필드를 특정지을 접두사</param>
        /// <param name="forceUpperCase">필드명 대문자 비교설정</param>
        public void SetPanelData(Control containerCtrl, DataTable srcDt, List<string> exceptColumnList = null, string targetFieldPrefix = "src", bool forceUpperCase = true)
        {
            var ctrls = this.GetControlList(containerCtrl, targetFieldPrefix, true, forceUpperCase);
            DataTable dt = srcDt.Copy();
            DataRow dr = dt.Rows.Count > 0 ? dt.Rows[0] : dt.NewRow();

            // 대문자 처리
            if (forceUpperCase)
            {
                if (exceptColumnList != null)
                    exceptColumnList.ForEach(x => x = x.ToUpper());
                for (int i = 0; i < dt.Columns.Count; ++i)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                }
            }

            foreach (DataColumn col in dt.Columns)
            {
                string colvalue = dr[col.ColumnName].ToString();
                if (exceptColumnList != null && exceptColumnList.Contains(col.ColumnName)) continue;

                if (ctrls.ContainsKey(col.ColumnName))
                {
                    if (ctrls[col.ColumnName] is TextBox)
                    {
                        (ctrls[col.ColumnName] as TextBox).Text = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is DropDownList)
                    {
                        (ctrls[col.ColumnName] as DropDownList).SelectedValue = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is RadioButtonList)
                    {
                        (ctrls[col.ColumnName] as RadioButtonList).SelectedValue = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is CheckBox)
                    {
                        (ctrls[col.ColumnName] as CheckBox).Checked = (colvalue == "1" || colvalue.ToUpper() == "TRUE");
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxCheckBox)
                    {
                        (ctrls[col.ColumnName] as DevExpress.Web.ASPxCheckBox).Checked = (colvalue == "1" || colvalue.ToUpper() == "TRUE");
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxRadioButtonList)
                    {
                        (ctrls[col.ColumnName] as DevExpress.Web.ASPxRadioButtonList).Value = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxComboBox)
                    {
                        (ctrls[col.ColumnName] as DevExpress.Web.ASPxComboBox).Value = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxDateEdit)
                    {
                        DateTime dtValue;
                        if (dr[col.ColumnName] is DateTime)
                        {
                            (ctrls[col.ColumnName] as DevExpress.Web.ASPxDateEdit).Value = dr[col.ColumnName];
                        }
                        else if (DateTime.TryParse(colvalue, out dtValue))
                        {
                            (ctrls[col.ColumnName] as DevExpress.Web.ASPxDateEdit).Value = dtValue;
                        }
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxTextBox)
                    {
                        (ctrls[col.ColumnName] as DevExpress.Web.ASPxTextBox).Text = colvalue;
                    }
                    else if (ctrls[col.ColumnName] is DevExpress.Web.ASPxEditBase)
                    {
                        (ctrls[col.ColumnName] as DevExpress.Web.ASPxEditBase).Value = colvalue;
                    }
                }
            }
        }

        /// <summary>
        /// 지정된 컨테이너에 포함된 특정 접두사의 필드에 데이터 설정
        /// 그중, 설정 제외 필드는 설정하지 않는다.
        /// </summary>
        /// <param name="containerCtrl">컨테이너(패널) 컨트롤</param>
        /// <param name="srcDictionary">값을 설정할 Dictionary.</param>
        /// <param name="exceptColumnList">캡션설정 제외 필드 목록</param>
        /// <param name="targetFieldPrefix">컨테이너에 포함된 필드를 특정지을 접두사</param>
        /// <param name="forceUpperCase">필드명 대문자 비교설정</param>
        public void SetPanelData(Control containerCtrl, Dictionary<string, string> srcDictionary, List<string> exceptColumnList = null, string targetFieldPrefix = "src", bool forceUpperCase = true)
        {
            var ctrls = this.GetControlList(containerCtrl, targetFieldPrefix, true, forceUpperCase);
            //DataTable dt = srcDt.Copy();
            //DataRow dr = dt.Rows.Count > 0 ? dt.Rows[0] : dt.NewRow();

            // 대문자 처리
            if (forceUpperCase)
            {
                if (exceptColumnList != null)
                    exceptColumnList.ForEach(x => x = x.ToUpper());
            }

            foreach (KeyValuePair<string, string> item in srcDictionary)
            {
                string colvalue = item.Value;
                string colname = (forceUpperCase ? item.Key.ToUpper() : item.Key);
                if (exceptColumnList != null && exceptColumnList.Contains(colname)) continue;

                if (ctrls.ContainsKey(colname))
                {
                    if (ctrls[colname] is TextBox)
                    {
                        (ctrls[colname] as TextBox).Text = colvalue;
                    }
                    else if (ctrls[colname] is DropDownList)
                    {
                        (ctrls[colname] as DropDownList).SelectedValue = colvalue;
                    }
                    else if (ctrls[colname] is RadioButtonList)
                    {
                        (ctrls[colname] as RadioButtonList).SelectedValue = colvalue;
                    }
                    else if (ctrls[colname] is CheckBox)
                    {
                        (ctrls[colname] as CheckBox).Checked = (colvalue == "1" || colvalue.ToUpper() == "TRUE");
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxCheckBox)
                    {
                        (ctrls[colname] as DevExpress.Web.ASPxCheckBox).Checked = (colvalue == "1" || colvalue.ToUpper() == "TRUE");
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxRadioButtonList)
                    {
                        (ctrls[colname] as DevExpress.Web.ASPxRadioButtonList).Value = colvalue;
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxComboBox)
                    {
                        (ctrls[colname] as DevExpress.Web.ASPxComboBox).Value = colvalue;
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxDateEdit)
                    {
                        DateTime dtValue;
                        if (DateTime.TryParse(colvalue, out dtValue))
                        {
                            (ctrls[colname] as DevExpress.Web.ASPxDateEdit).Value = dtValue;
                        }
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxTextBox)
                    {
                        (ctrls[colname] as DevExpress.Web.ASPxTextBox).Text = colvalue;
                    }
                    else if (ctrls[colname] is DevExpress.Web.ASPxEditBase)
                    {
                        (ctrls[colname] as DevExpress.Web.ASPxEditBase).Value = colvalue;
                    }
                }
            }
        }

        /// <summary>
        /// 지정된 컨테이너에 포함된 특정 접두사의 필드에 데이터 클리어
        /// 단, 날짜 필드는 현재일시로 변경
        /// </summary>
        /// <param name="containerCtrl"></param>
        /// <param name="exceptColumnList"></param>
        /// <param name="targetFieldPrefix"></param>
        /// <param name="forceUpperCase"></param>
        public void SetPanelDataClear(Control containerCtrl, List<string> exceptColumnList = null, string targetFieldPrefix = "src", bool forceUpperCase = true)
        {
            var ctrls = this.GetControlList(containerCtrl, targetFieldPrefix, true, forceUpperCase);

            // 대문자 처리
            if (forceUpperCase)
            {
                if (exceptColumnList != null)
                    exceptColumnList.ForEach(x => x = x.ToUpper());
            }

            foreach (KeyValuePair<string, Control> ctrl in ctrls)
            {
                string colvalue = "";
                string key = (forceUpperCase ? ctrl.Key.ToUpper() : ctrl.Key);
                if (exceptColumnList != null && exceptColumnList.Contains(key)) continue;

                if (ctrl.Value is TextBox)
                {
                    (ctrl.Value as TextBox).Text = colvalue;
                }
                else if (ctrl.Value is DropDownList)
                {
                    (ctrl.Value as DropDownList).SelectedValue = colvalue;
                }
                else if (ctrl.Value is RadioButtonList)
                {
                    (ctrl.Value as RadioButtonList).SelectedValue = colvalue;
                }
                else if (ctrl.Value is CheckBox)
                {
                    (ctrl.Value as CheckBox).Checked = false;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxCheckBox)
                {
                    (ctrl.Value as DevExpress.Web.ASPxCheckBox).Checked = false;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxRadioButtonList)
                {
                    (ctrl.Value as DevExpress.Web.ASPxRadioButtonList).Value = colvalue;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxComboBox)
                {
                    (ctrl.Value as DevExpress.Web.ASPxComboBox).Value = colvalue;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxDateEdit)
                {
                    (ctrl.Value as DevExpress.Web.ASPxDateEdit).Value = DateTime.Now;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxTextBox)
                {
                    (ctrl.Value as DevExpress.Web.ASPxTextBox).Text = colvalue;
                }
                else if (ctrl.Value is DevExpress.Web.ASPxEditBase)
                {
                    (ctrl.Value as DevExpress.Web.ASPxEditBase).Value = colvalue;
                }
            }
        }

        /// <summary>
        /// 지정된 컨테이너 내의 데이터 Dictionary형태로 반환
        /// </summary>
        /// <param name="containerCtrl"></param>
        /// <param name="targetFieldPrefix"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetPanelData(Control containerCtrl, string targetFieldPrefix = "src", List<string> filterList = null)
        {
            var tmp = this.GetControls(containerCtrl.Controls, targetFieldPrefix, true, true);
            var result = new Dictionary<string, string>();
            if (filterList == null) result = tmp;
            else result = tmp.Where(x => filterList.Contains(x.Key)).ToDictionary(x1 => x1.Key, x2 => x2.Value);
            return result;
        }
        #endregion

        #region JSON 파라미터
        /// <summary>
        /// 직렬화된 JSON 문자열을 파싱하여 반환
        /// </summary>
        /// <param name="serializedJSONString"></param>
        /// <returns></returns>
        
        public string SerializeJSON(object obj)
        {
            return (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(obj);
        }


        public System.Collections.Generic.Dictionary<string, string> DeserializeJSON(string serializedJSONString)
        {
            return (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<System.Collections.Generic.Dictionary<string, string>>(HttpUtility.UrlDecode(serializedJSONString));
        }
        #endregion

        #region 공통코드 콤보박스 바인드
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        public void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ComboBoxID, string CommonCodegroup, string CommonCode, string FirstText = "", bool goBind = true)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = ComboBoxID;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode[CommonCodegroup][CommonCode].codeGroup.Values;
            if (goBind) ddlComboBox.DataBind();
            if (!string.IsNullOrEmpty(FirstText))
            {
                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem(FirstText, ""));
                ddlComboBox.SelectedIndex = 0;
                //ddlComboBox.NullText = FirstText;
                //ddlComboBox.Value = null;
            }
        }
        #endregion

        #region GetLoginUserInfo (사용자정보)
        [System.Web.Services.WebMethod]
        public static string GetLoginUserInfo(string infoList)
        {
            DataSet ds = null;
            string errMsg = string.Empty;
            var Page = HttpContext.Current.CurrentHandler as WebUIBasePage;
            Dictionary<string, string> result = new Dictionary<string, string>();

            var fields = infoList.Split(';');

            foreach (var field in fields)
            {
                switch (field)
                {
                    case "USERID":
                        result.Add(field, Page.gsUSERID);
                        break;
                    case "USERNM":
                        result.Add(field, Page.gsUSERNM);
                        break;
                    case "COMPCD":
                        result.Add(field, Page.gsCOMPCD);
                        break;
                    case "COMPNM":
                        result.Add(field, Page.gsCOMPNM);
                        break;
                    case "FACTCD":
                        result.Add(field, Page.gsFACTCD);
                        break;
                    case "FACTNM":
                        result.Add(field, Page.gsFACTNM);
                        break;
                    case "DEPTCD":
                        result.Add(field, Page.gsDEPARTCD);
                        break;
                    case "DEPTNM":
                        result.Add(field, Page.gsDEPARTNM);
                        break;
                    default: break;
                }
            }

            if (result.Count == 0)
            {
                throw new Exception("일치하는 항목이 없습니다");
            }

            return GlobalFunction.SerializeJSON(result);
        }
        #endregion

        #region GetTable
        [System.Web.Services.WebMethod]
        public static string GetTable(string CATEGORY, string TABLE, string PKEYNM, string PKEYVALUE)
        {
            System.Collections.Generic.Dictionary<string, string> oParamDic = new System.Collections.Generic.Dictionary<string, string>();
            DataSet ds = null;
            string errMsg = string.Empty;
            Dictionary<string, string> result = new Dictionary<string, string>();

            oParamDic.Clear();

            switch (CATEGORY)
            {
               
                case "Common":
                    using (SYSTBiz biz = new SYSTBiz())
                    {
                        oParamDic.Add(PKEYNM, PKEYVALUE);
                        switch (TABLE)
                        {
                            case "SYUSR01":
                                oParamDic["F_COMPCD"] = ((WebUIBasePage)HttpContext.Current.CurrentHandler).gsCOMPCD;
                                oParamDic["F_FACTCD"] = ((WebUIBasePage)HttpContext.Current.CurrentHandler).gsFACTCD;
                                ds = biz.SYUSR01_LST(oParamDic, out errMsg);
                                break;
                        }
                    }
                    break;

                case "MEAS":
                    using (MEASBiz biz = new MEASBiz())
                    {
                        oParamDic.Add(PKEYNM, PKEYVALUE);
                        switch (TABLE)
                        {
                            case "QMM10_TBL":
                                ds = biz.MS01D4_GRID_LST(oParamDic, out errMsg);
                                break;
                        }
                    }
                    break;
            }

            result = GlobalFunction.FirstRowToDictionary(ds);

            if (result.Count == 0)
            {
                throw new Exception("일치하는 항목이 없습니다");
            }

            return GlobalFunction.SerializeJSON(result);
        }
        #endregion

        #region 4M
        public string Get4MCD()
        {
            string resultCode = String.Empty;

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;
            UserControl _UserControl = cph.FindControl("ucFourM") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hid4MCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 기타함수
        #region 데이터테이블 자동 넘버링 컬럼 추가
        public DataTable AutoNumberTable(DataTable dt, string FieldName = "No")
        {
            DataTable ndt = new DataTable();

            while (dt.Columns.Contains(FieldName))
            {
                FieldName = FieldName + "_1";
            }

            var col = ndt.Columns.Add(FieldName, typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;

            ndt.Merge(dt);

            return ndt;
        } 
        #endregion

        #region DataTable Dictionary 처리
        // 데이터테이블의 첫번째 로우를 json 문자열로 반환(DataRow -> Json)
        public string Dr2EncodeJson(DataTable dt)
        {
            string result = string.Empty;
            DataRow dr = null;
            if (dt != null && dt.Rows.Count > 0) dr = dt.Rows[0];
            if (dr != null)
            {
                var dict = dr.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => dr[c].ToString());
                result = SerializeJSON(dict);
            }
            return result;
        }
        // 데이터로우를 json 문자열로 반환(DataRow -> Json)
        public string Dr2EncodeJson(DataRow dr)
        {
            string result = string.Empty;
            if (dr != null)
            {
                var dict = dr.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => dr[c].ToString());
                result = SerializeJSON(dict);
            }
            return result;
        }
        // 데이터로우를 Dictionary<string, string>로 반환
        public Dictionary<string, string> DataRow2Dictionary(DataRow dr)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (dr != null)
            {
                result = dr.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => dr[c].ToString());
            }
            return result;
        }
        // 데이터테이블을 json 문자열로 반환(DataTable -> Json)
        public string DataTable2EncodeJson(DataTable dt)
        {
            string result = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                var dict = dt.AsEnumerable().Select(dr => dr.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => dr[c].ToString())).ToArray();
                result = SerializeJSON(dict);
            }
            return result;
        }
        #endregion

        #region html 인코딩
        /// <summary>
        /// Uri.EscapeDataString이 32766글자가 넘을 경우, 나누어 처리
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public string EscapeDataString(string target)
        {
            string result = string.Empty;
            int mcnt = 32766;
            if (target.Length <= mcnt) result = Uri.EscapeDataString(target);
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                char[] buff = new char[mcnt];
                using (System.IO.StringReader sr = new System.IO.StringReader(target))
                {
                    while (sr.Read(buff, 0, mcnt) > 0)
                    {
                        sb.Append(Uri.EscapeDataString(new String(buff)));
                        Array.Clear(buff, 0, mcnt);
                    }
                }
                result = sb.ToString();
            }
            return result;
        }
        #endregion

        #endregion

        #region Dictionary 검색 및 값 반환
        public string GetDicValue(Dictionary<string, object> oDic, string sKey)
        {
            return oDic.ContainsKey(sKey) && !oDic[sKey].ToString().ToLower().Equals("null") ? oDic[sKey].ToString() : "";
        }
        public string GetDicValue(Dictionary<string, string> oDic, string sKey)
        {
            return oDic.ContainsKey(sKey) && !oDic[sKey].ToLower().Equals("null") ? oDic[sKey] : "";
        }
        #endregion
    }
}
