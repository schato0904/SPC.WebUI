using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.WebUI.Pages.INSP.Report;
using SPC.INSP.Biz;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSPREPORTPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Dictionary<string, string> PopParam = new Dictionary<string, string>();
        string F_ITEMCD = String.Empty;
        string F_WORKCD = String.Empty;
        string F_QWK13IMID = String.Empty;
        string F_REPORTTP = String.Empty;

        DataTable dtQWK13M = new DataTable();
        DataTable dtQWK13D = new DataTable();
        DataTable dtQWK13IM = new DataTable();
        DataTable dtQWK13ID = new DataTable();
        DataTable dtQWK13IS = new DataTable();
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

            // 마스터 정보(팝업)
            QWK13IM_GET();

            switch (F_REPORTTP)
            {
                case "AAI101"://제품검사(가스용A)
                case "AAI102"://제품검사(가스용B)
                case "AAI103"://제품검사(수도용A)
                case "AAI104"://제품검사(수도용B)
                    devDocument.Report = new AAI101RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI111"://인수검사(원재료-수도)
                case "AAI112"://인수검사(원재료-가스)
                    devDocument.Report = new AAI111RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI113"://인수검사(고무)
                    devDocument.Report = new AAI113RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI114"://인수검사(부품)
                    devDocument.Report = new AAI114RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI115"://인수검사(열선)
                    devDocument.Report = new AAI115RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI121"://중간검사(스피곳가스)
                case "AAI122"://중간검사(스피곳수도)
                    devDocument.Report = new AAI121RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
                case "AAI123"://중간검사(EF)
                    devDocument.Report = new AAI123RPT(dtQWK13M, dtQWK13D, dtQWK13IM, dtQWK13ID, dtQWK13IS);
                    break;
            }

            List<string> oList = new List<string>();

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
            PopParam = DeserializeJSON(Request.QueryString.Get("PopParam"));

            F_ITEMCD = GetDicValue(PopParam, "F_ITEMCD");
            F_WORKCD = GetDicValue(PopParam, "F_WORKCD");
            F_QWK13IMID = GetDicValue(PopParam, "F_QWK13IMID");
            F_REPORTTP = GetDicValue(PopParam, "F_REPORTTP");
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

        #region 마스터 정보(팝업)
        void QWK13IM_GET()
        {
            string errMsg = String.Empty;
            if (ds != null) ds.Clear();
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13IMID", F_QWK13IMID);
                oParamDic.Add("F_REPORTTP", F_REPORTTP);
                oParamDic.Add("F_ITEMCD", F_ITEMCD);
                oParamDic.Add("F_WORKCD", F_WORKCD);

                ds = biz.QWK13_REPORT_GET(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg) || !bExistsDataSet(ds))
            {
                Server.Transfer("~/Pages/ERROR/Report.aspx");
            }
            else
            {
                this.dtQWK13M = ds.Tables[0];
                this.dtQWK13D = ds.Tables[1];
                this.dtQWK13IM = ds.Tables[2];
                this.dtQWK13ID = ds.Tables[3];
                this.dtQWK13IS = ds.Tables[4];
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #endregion
    }
}