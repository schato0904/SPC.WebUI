using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MEAS.Biz;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS4003 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public string oSetParam = string.Empty;
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
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid.JSProperties["cpResultEtc"] = "";
            }

            if (IsCallback)
            {
                //PL01M_LST(GetSearchCondition());
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
            oSetParam = Request["oSetParam"] ?? "";
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
        {
            AspxCombox_DataBind(this.schF_EQUIPDIVCD, "SS", "SS01", "전체");
            AspxCombox_DataBind(this.schF_FIXTYPECD, "SS", "SS04", "전체");
            AspxCombox_DataBind(this.schF_JUDGECD, "SS", "SS02", "전체");
            //AspxCombox_DataBind(this.schF_SRCTYPECD, "AA", "AAF2");
            AspxCombox_DataBind(this.schF_FIXDIVCD, "SS", "SS05", "전체");
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 조회 조건 가져오기
        Dictionary<string, string> GetSearchCondition()
        {
            var result = new Dictionary<string, string>();

            result["F_FROMDT"] = schF_FIXDT_FROM.Text;
            result["F_TODT"] = schF_FIXDT_TO.Text;
            result["F_EQUIPDIVCD"] = (schF_EQUIPDIVCD.Value ?? "").ToString();
            result["F_FIXTYPECD"] = (schF_FIXTYPECD.Value ?? "").ToString();
            result["F_JUDGECD"] = (schF_JUDGECD.Value ?? "").ToString();
            if ( !string.IsNullOrEmpty(schF_MS01MID.Text)) result["F_MS01MID"] = schF_MS01MID.Text;
            result["F_FIXDIVCD"] = (schF_FIXDIVCD.Value ?? "").ToString();

            return result;
        }
        #endregion

        #region 그리드 목록 조회
        /// <summary>
        /// SetDT02MGridBind
        /// </summary>
        /// <param name="COMMCD">그룹코드</param>
        void MS01D5_LST(Dictionary<string, string> FIELDS = null)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();

                if (FIELDS != null)
                    FIELDS.Keys.ToList<string>().ForEach(x => oParamDic.Add(x, FIELDS[x]));

                oParamDic["F_LANGTYPE"] = gsLANGTP;
                //oParamDic["LOGINUSER"] = gsUSERID;

                ds = biz.MS01D5_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (!IsCallback)
                //    devGrid.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //var pkey = string.Empty;
            //// 그리드정보를 구한다
            //PL01M_LST(GetSearchCondition());
            //devGrid.DataBind();
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetUsedString(new string[] { "F_USEYN" }, e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
        }
        #endregion

        #region devGrid_HtmlRowCreated
        protected void devGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Row.Height = Unit.Pixel(35);
            }
        }
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            // 그리드정보를 구한다
            MS01D5_LST(GetSearchCondition());
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            // 그리드정보를 구한다
            //this.IN01M_LST(GetSearchCondition());
            devGridExporter.WriteXlsToResponse(String.Format("검교정현황({0}~{1})", schF_FIXDT_FROM.Text, schF_FIXDT_TO.Text), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.Column.Name == "F_USEYN" || e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #endregion
    }
}