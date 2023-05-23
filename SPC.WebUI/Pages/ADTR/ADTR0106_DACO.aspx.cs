using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ADTR.Biz;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0106_DACO : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;

        string[] procResult = { "2", "Unknown Error" };

        DataSet ds2
        {
            get
            {
                return (DataSet)Session["ADTR0106_DACO"];
            }
            set
            {
                Session["ADTR0106_DACO"] = value;
            }
        }

        protected string oSetParam = String.Empty;
        string a = "";
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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";


            }
            //GetYear();
            //ddlYEAR.SelectedIndex = 0;
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
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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
        {

            yearCOMBO_DataBind(this.ddlYEAR);


            if (!String.IsNullOrEmpty(oSetParam))
            {
                string[] oSetParams = oSetParam.Split('|');
                ucComp.compParam = oSetParams[0];
                ucFact.factParam = oSetParams[1];
            }
        }
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
            //this.ddlYEAR.SelectedIndex = 0;
        }
        #endregion

        #endregion

        #region 사용자 정의 함수


        #region 라인별 측정 데이터 구한다
        void ADTR0106_DACO_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_YY", (this.ddlYEAR.Value ?? "").ToString());
                ds = biz.ADTR0106_DACO_LST(oParamDic, out errMsg);


            }

            ds2 = ds;

            devGrid.DataSource = ds;
            devGrid.DataBind();

            // Grid Callback Init
            devGrid.JSProperties["cpResultCode"] = "0";
            devGrid.JSProperties["cpResultMsg"] = errMsg;


        }
        #endregion

        #region 라인별 측정 데이터 구한다 - 엑셀출력용
        void QWK04A_ADTR0102_EXCEL()
        {
            devGrid2.DataSource = ds2;
        }
        #endregion



        #endregion

        #region 사용자이벤트




        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 라인별 측정 데이터 구한다            
            ADTR0106_DACO_LST();

            devGrid.DataBind();

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
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
            else if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_UNIT") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.TextValue = e.Text;
            }
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ds2.Tables[0].Rows.Count == 0)
            {
                devGrid2.JSProperties["cpResultMsg"] = "다시 조회 하세요.";
            }
            else
            {
                QWK04A_ADTR0102_EXCEL();
                devGrid2.DataBind();
                devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 월별모니터링정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            }
        }
        #endregion


        void Download_Excelfile()
        {

        }


        //protected void yearCOMBO_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        //{
        //    GetYear();
        //}

        void yearCOMBO_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox)
        {
            string[] Y1 = new string[20];
            for (int a = 0; a < 20; a++)
            {
                Y1[a] = DateTime.Now.AddYears(-a).ToString("yyyy");
            }




            ddlComboBox.DataSource = Y1;
            ddlComboBox.DataBind();
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });
        }


        void GetYear()
        {
            string[] Y1 = new string[20];
            for (int a = 0; a < 20; a++)
            {
                Y1[a] = DateTime.Now.AddYears(-a).ToString("yyyy");
            }




            ddlYEAR.DataSource = Y1;
            ddlYEAR.DataBind();
            ddlYEAR.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });


        }

        #endregion
    }
}