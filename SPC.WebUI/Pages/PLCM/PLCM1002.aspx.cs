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
using System.Text;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM1002 : WebUIBasePage
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 검사항목목록을 구한다
                PHIL33_LST();
            }

            // Grid Columns Sum Width
            //hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 검사항목 목록을 구한다
        void PHIL33_LST()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.PHIL33_LST(oParamDic, out errMsg);
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
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

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
            e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);

            if (e.VisibleRowIndex < 0) return;
            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);
            if (e.Column.FieldName.Equals("F_MACHCD"))
            {
                e.DisplayText = dtRow["F_MACHNM"].ToString();
            }
        }
        #endregion

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }
        #endregion


        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_MACHCD")) return;

            if (e.Column.FieldName.Equals("F_MACHCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;

                string errMsg = String.Empty;

                using (PLCMBiz biz = new PLCMBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);

                    ds = biz.PLC01_LST(oParamDic, out errMsg);
                }

                comboBox.TextField = "F_MACHNM";
                comboBox.ValueField = "F_MACHCD";
                comboBox.DataSource = ds;
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }
        }
        #endregion

        #region devGrid BatchUpdate
        /// <summary>
        /// devGrid_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MACHCD", (Value.NewValues["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_MEAINSPCD", (Value.NewValues["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_INSPDETAIL", (Value.NewValues["F_INSPDETAIL"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_PHIL33_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MACHCD", (Value.NewValues["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_MEAINSPCD", (Value.NewValues["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_INSPDETAIL", (Value.NewValues["F_INSPDETAIL"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_PHIL33_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MACHCD", (Value.Values["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_MEAINSPCD", (Value.Values["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[idx] = "USP_PHIL33_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                StringBuilder sb_OutMsg = new StringBuilder();
                foreach (Dictionary<string, object> _oOutParamDic in oOutParamList)
                {
                    foreach (KeyValuePair<string, object> _oOutPair in _oOutParamDic)
                    {
                        if (!String.IsNullOrEmpty(_oOutPair.Value.ToString()))
                            sb_OutMsg.AppendFormat("검사항목코드 {0}는 검사기준에서 사용중이므로 삭제할 수 없습니다.\r", _oOutPair.Value);
                    }
                }

                if (!String.IsNullOrEmpty(sb_OutMsg.ToString()))
                {
                    procResult = new string[] { "2", sb_OutMsg.ToString() };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                }

                // 검사항목목록을 구한다
                PHIL33_LST();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
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
            devGrid.FilterExpression = "";
            // 검사항목을 구한다
            PHIL33_LST();
        }
        #endregion

        #region devGrid_AutoFilterCellEditorInitialize
        protected void devGrid_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_MACHCD")) return;

            string[] filters = devGrid.FilterExpression.Replace(" And ", "|").Split('|');
            string[] filterParams = { "", "" };

            if (e.Column.FieldName.Equals("F_MACHCD"))
            {
                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                string errMsg = String.Empty;

                using (PLCMBiz biz = new PLCMBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);

                    ds = biz.PLC01_LST(oParamDic, out errMsg);
                }

                ddlComboBox.TextField = "F_MACHNM";
                ddlComboBox.ValueField = "F_MACHCD";
                ddlComboBox.DataSource = ds;
                ddlComboBox.DataBind();

                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "", Value = "", Selected = true });

                foreach (string filter in filters)
                {
                    if (filter.Contains("F_MACHCD"))
                    {
                        filterParams = filter.Split('=');
                        ddlComboBox.SelectedIndex = ddlComboBox.Items.FindByValue(filterParams[1].Trim().Replace("'", "")).Index;
                    }
                }
            }
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 검사항목관리정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion
        #endregion
    }
}