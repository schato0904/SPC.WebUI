using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.FITM.Biz;

namespace SPC.WebUI.Pages.FITM
{
    public partial class FITM0204 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        #endregion

        #region 프로퍼티

        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["FITM0204"];
            }
            set
            {
                Session["FITM0204"] = value;
            }
        }

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

            new ASPxGridViewCellMerger(devGrid, "공정명|검사일자|검사시간");
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
        {
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
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #endregion

        #region Data조회
        void QWK04A_FITM0204_LST()
        {
            string errMsg = String.Empty;

            using (FITMBiz biz = new FITMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                ds = biz.QWK04A_FITM0204_LST(oParamDic, out errMsg);
            }

            DataTable dtGroup = null;

            dtGroup = new DataTable();
            dtGroup.Columns.Add("F_WORKDATE", typeof(String));
            dtGroup.Columns.Add("F_WORKTIME", typeof(String));
            dtGroup.Columns.Add("F_TSERIALNO", typeof(String));
            dtGroup.Columns.Add("F_NUMBER", typeof(String));
            dtGroup.Columns.Add("F_WORKCD", typeof(String));
            dtGroup.Columns.Add("F_WORKNM", typeof(String));
            dtGroup.Columns.Add("F_ITEMCD", typeof(String));
            dtGroup.Columns.Add("F_LOTNO", typeof(String));
            dtGroup.Columns.Add("F_SUBCOUNT", typeof(String));

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtGroup.Rows.Add(dtRow["F_WORKDATE"], dtRow["F_WORKTIME"], dtRow["F_TSERIALNO"], dtRow["F_NUMBER"], dtRow["F_WORKCD"], dtRow["F_WORKNM"], dtRow["F_ITEMCD"], dtRow["F_LOTNO"], 0);
            }

            DataTable dtGroupList = CTF.Web.Framework.Helper.StaticFunctions.staticData.GetGroupedBy(
                dtGroup,
                "F_WORKDATE,F_WORKTIME,F_WORKCD,F_WORKNM,F_ITEMCD,F_TSERIALNO,F_NUMBER,F_LOTNO,F_SUBCOUNT",
                "F_WORKDATE,F_WORKTIME,F_WORKCD,F_WORKNM,F_ITEMCD,F_TSERIALNO,F_NUMBER,F_LOTNO",
                "Count");

            dtGroup = new DataTable();
            dtGroup.Columns.Add("F_INSPDETAIL", typeof(String));
            dtGroup.Columns.Add("F_SUBCOUNT", typeof(String));

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtGroup.Rows.Add(dtRow["F_INSPDETAIL"], 0);
            }

            DataTable dtInspList = CTF.Web.Framework.Helper.StaticFunctions.staticData.GetGroupedBy(
                dtGroup,
                "F_INSPDETAIL,F_SUBCOUNT",
                "F_INSPDETAIL",
                "Count");

            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("공정명", typeof(String));
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("검사시간", typeof(String));
            dtTemp.Columns.Add("순번", typeof(String));
            dtTemp.Columns.Add("LotNo", typeof(String));
            foreach (DataRow dtRow in dtInspList.Rows)
            {
                dtTemp.Columns.Add(dtRow["F_INSPDETAIL"].ToString(), typeof(String));
            }

            foreach (DataRow dtRow in dtGroupList.Rows)
            {
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["공정명"] = dtRow["F_WORKNM"];
                dtNewRow["검사일자"] = dtRow["F_WORKDATE"];
                dtNewRow["검사시간"] = dtRow["F_WORKTIME"];
                dtNewRow["순번"] = dtRow["F_NUMBER"];
                dtNewRow["LotNo"] = dtRow["F_LOTNO"];

                foreach (DataRow dtSubRow in ds.Tables[0].Select(String.Format("F_WORKDATE='{0}' AND F_WORKTIME='{1}' AND F_WORKCD='{2}' AND F_ITEMCD='{3}' AND F_TSERIALNO='{4}' AND F_NUMBER='{5}'",
                    dtRow["F_WORKDATE"],
                    dtRow["F_WORKTIME"],
                    dtRow["F_WORKCD"],
                    dtRow["F_ITEMCD"],
                    dtRow["F_TSERIALNO"],
                    dtRow["F_NUMBER"])))
                {
                    dtNewRow[dtSubRow["F_INSPDETAIL"].ToString()] = dtSubRow["F_MEASURE"].ToString();
                }

                dtTemp.Rows.Add(dtNewRow);
            }

            foreach (DataColumn column in dtTemp.Columns)
            {
                DevExpress.Web.GridViewDataColumn col = new DevExpress.Web.GridViewDataColumn();
                col.FieldName = column.ColumnName;
                col.Caption = column.ColumnName;

                if (column.ColumnName.Equals("공정명"))
                {
                    col.Width = new Unit(200);
                }

                devGrid.Columns.Add(col);
            }

            devGrid.DataSource = dtTemp;
            dtGrid1 = dtTemp;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #region 사용자이벤트

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
        }
        #endregion

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_INSPDETAIL","F_ITEMCD"};

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
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
            e.EncodeHtml = false;
            e.DisplayText = (e.Value ??"").ToString();
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
            QWK04A_FITM0204_LST();
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
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
            e.Text = e.Text.Replace(@"<span style='color:red;'>", "").Replace(@"<span style='color:blue;'>", "").Replace(@"</span>", "");
            e.TextValue = e.Text;
            /*
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
            else {
                
            }*/
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
            QWK04A_FITM0204_LST();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 검사항목별Data정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}