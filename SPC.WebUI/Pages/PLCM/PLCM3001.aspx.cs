using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.PLCM.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM3001 : WebUIBasePage
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
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        { }
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
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region Data조회
        void PLCM3001_LST()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", GetFromDt());
                ds = biz.USP_PLCM3001_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds.Tables[0];

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                {
                    devGrid.Columns["HEADER01"].Caption = ds.Tables[1].Rows[0]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER02"].Caption = ds.Tables[1].Rows[1]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER03"].Caption = ds.Tables[1].Rows[2]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER04"].Caption = ds.Tables[1].Rows[3]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER05"].Caption = ds.Tables[1].Rows[4]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER06"].Caption = ds.Tables[1].Rows[5]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER07"].Caption = ds.Tables[1].Rows[6]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER08"].Caption = ds.Tables[1].Rows[7]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER10"].Caption = ds.Tables[1].Rows[8]["F_MACHNM"].ToString();
                    devGrid.Columns["HEADER12"].Caption = ds.Tables[1].Rows[9]["F_MACHNM"].ToString();

                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            var grid = sender as ASPxGridView;
            string fieldname = e.DataColumn.FieldName;

            switch (fieldname)
            {
                case "HEADER01":
                case "HEADER02":
                case "HEADER03":
                case "HEADER04":
                case "HEADER05":
                case "HEADER06":
                case "HEADER07":
                case "HEADER08":
                case "HEADER10":
                case "HEADER12":
                    //string strJudge = e.GetValue("HEADER04").ToString();
                    string strJudge = e.CellValue.ToString();
                    if (strJudge != "")
                    {
                        string[] sp = strJudge.Split('|');
                        e.Cell.Text = sp[0] + "<br />" + sp[1] + "<br />" + sp[2] + "<br />" + sp[3];
                    }

                    break;
            }

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
            PLCM3001_LST();
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            PLCM3001_LST();
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 일별작업현황", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}