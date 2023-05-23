using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Common
{
    public class LoginBasePage : System.Web.UI.Page
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
        #endregion

        private string KEY = "spckey";
        private string _RootURL = string.Empty;
        private string _InitLangClsCd = string.Empty;

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

        public string gsINSPWORKGBN { get { return HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["INSPWORKGBN"]); } } //공정검사관리 PPM, %구분 추가
        #endregion

        #region 쿠키관리

        #region  쿠키체크
        /// <summary>
        /// 쿠키체크
        /// </summary>
        /// <returns>bool</returns>
        public bool isGoodCookie()
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

        #endregion

        #region MainBasePage
        /// <summary>
        /// 이벤트핸들러 지정
        /// </summary>
        public LoginBasePage()
        {
            this.Load += new EventHandler(PageBase_Load);
        }
        #endregion

        #region InitializeCulture
        /// <summary>
        /// 문자셋 초기화
        /// </summary>
        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.InitLangClsCd);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.InitLangClsCd);
            base.InitializeCulture();
        }
        #endregion

        #region PageBase_Load
        /// <summary>
        /// 페이지로드
        /// </summary>
        /// <param name="sender">이벤트객체</param>
        /// <param name="e">이벤트정보</param>
        private void PageBase_Load(object sender, System.EventArgs e)
        {
            this._InitLangClsCd = CommonHelper.GetAppSectionsString("InitLangClsCd");

            if (this.User.Identity.IsAuthenticated)
            {

            }
            Page.DataBind();
        }

        #endregion

        #region 에러메세지호출
        /// <summary>
        /// 에러메세지호출
        /// </summary>
        /// <param name="sender">이벤트객체</param>
        /// <param name="e">이벤트정보</param>
        private void fnShowErrorForm()
        {
            #region 시스템 에러 메세지

            // ErrorPage
            string errorPage = "{0}?ErrorPage={1}&ErrorCode={2}";
            // Get the Page Name
            string _pageNM = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                HttpException httpException = exc as HttpException;

                // 에러페이지의 Url을 읽어온다.
                errorPage = String.Format(errorPage,
                    CommonHelper.GetAppSectionsString("ErrorPage"),
                    _pageNM,
                    httpException.ErrorCode);
            }

            // 에러페이지의 Url을 읽어온다.
            errorPage = String.Format(errorPage,
                CommonHelper.GetAppSectionsString("ErrorPage"),
                _pageNM,
                "500");

            Server.ClearError();

            //Redirect HTTP errors to HttpError page
            Server.Transfer(errorPage);

            #endregion
        }

        #endregion

        #region 속성 설정
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
        /// <summary>
        /// menuform 페이지
        /// </summary>
        public string CrtMenuForm
        {
            get
            {
                return this._RootURL + "menuform.aspx";
            }
        }
        /// <summary>
        /// topform 페이지
        /// </summary>
        public string CrtTopForm
        {
            get
            {
                return this._RootURL + "topform.aspx";
            }
        }
        /// <summary>
        /// index 페이지
        /// </summary>
        public string CrtIndex
        {
            get
            {
                return this._RootURL + "index.aspx";
            }
        }
        /// <summary>
        /// 이미지폴더명(업체구분별)
        /// </summary>
        public string CrtCmpFolNm
        {
            get
            {
                string LangClsCd = HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"].ToString());
                string CmpClsSn = HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["CmpClsSn"].ToString());

                return String.Format("{0}Resource/images/{1}/{2}/", this._RootURL, LangClsCd, CmpClsSn); //   "/SPC.WebUI/images/ko-KR/1/";
            }
        }
        /// <summary>
        /// 이미지폴더명
        /// </summary>
        public string CrtCommonFolNm
        {
            get
            {
                string pLangClsCd = base.Context.Request.Cookies[KEY] != null ? HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["LANGCD"].ToString()) : this._InitLangClsCd;

                return String.Format("{0}Resource/images/{1}/common/", this._RootURL, pLangClsCd); //   "/SPC.WebUI/images/ko-KR/common/";
            }
        }
        /// <summary>
        /// 기본 언어(ko-KR:한국,en-US:미국,ja-JP:일본,zh-CN:중국)
        /// </summary>
        public string InitLangClsCd
        {
            get
            {
                return _InitLangClsCd;
            }
        }
        /// <summary>
        /// 언어구분타입
        /// </summary>
        public string InitLangClsType
        {
            get
            {
                switch (InitLangClsCd)
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
        /// <summary>
        /// DB종류
        /// </summary>
        public string CrtDBClsCd
        {
            get
            {
                return base.Context.Request.Cookies[KEY] != null ? HttpUtility.UrlDecode(base.Context.Request.Cookies[KEY]["DBClsCd"].ToString()) : "";
            }
        }
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
    }
}