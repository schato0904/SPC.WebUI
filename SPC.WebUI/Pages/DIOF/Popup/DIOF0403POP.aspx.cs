using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.DIOF.Popup
{
    public partial class DIOF0403POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

        #endregion

        #endregion

        #region 이벤트

        #region Page Init
        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Request
            GetRequest();

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                // devGrid.JSProperties["cpResultCode"] = "";
                // devGrid.JSProperties["cpResultMsg"] = "";
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            srcF_REMEDYIDX.Text = Request.QueryString.Get("KEY");
        }
        #endregion

        #region Web Init
        /// <summary>
        /// Web_Init
        /// </summary>
        void Web_Init()
        {
            // DefaultButton 세팅
            SetDefaultButton();

            // 객체 초기화
            SetDefaultObject();

            // 클라이언트 스크립트
            SetClientScripts();

            // 서버 컨트럴 객체에 기초값 세팅
            SetDefaultValue();
        }
        #endregion

        #region DefaultButton 세팅
        /// <summary>
        /// SetDefaultButton
        /// </summary>
        void SetDefaultButton()
        { }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        { }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        { }
        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 뷰(선택한 설비 상세정보 조회)
        DataSet RetrieveDetail(string sREMEDYIDX, out string errMsg)
        {
            errMsg = String.Empty;
            if (ds != null) ds.Clear();
            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_REMEDYIDX", sREMEDYIDX);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST(oParamDic, out errMsg);
            }
            return ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"])
            {
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    if (ds != null) ds.Clear(); ;
                    ds = RetrieveDetail(paramDic["F_REMEDYIDX"], out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ISOK = false;
                        msg = errMsg;
                    }
                    else if (!bExistsDataSet(ds))
                    {
                        ISOK = false;
                        msg = "조회된 데이터가 없습니다.";
                    }
                    else
                    {
                        ISOK = true;
                        msg = string.Empty;
                        DataRow dr = ds.Tables[0].Rows[0];
                        // 조회한 데이터를 Dictionary 형태로 변환
                        PAGEDATA = ds.Tables[0].Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    }
                    result["ISOK"] = ISOK;
                    result["MSG"] = msg;
                    result["PAGEDATA"] = PAGEDATA;
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #endregion
    }
}