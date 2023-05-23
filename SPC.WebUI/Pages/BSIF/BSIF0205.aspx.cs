using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using System.Text;
using SPC.SYST.Biz;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0205 : WebUIBasePage
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
                // 품목목록을 구한다
                QCD01_LST();
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

        #region 품목 목록을 구한다
        void QCD01_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.GetQCD01_LST(oParamDic, out errMsg);
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

        #region 공통코드를 구한다
        DataTable SYCOD01_LST(string sCODEGROUP, string sGROUP = "")
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CODEGROUP", sCODEGROUP);
                if (!String.IsNullOrEmpty(sGROUP))
                    oParamDic.Add("F_IPADDRESS", sGROUP);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYCOD01_LST_FOR_ITEM(oParamDic, out errMsg);
            }

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_CODE", typeof(string));
            dtTable.Columns.Add("F_CODENM", typeof(string));

            if (String.IsNullOrEmpty(sGROUP))
                dtTable.Rows.Add("", "선택");

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtTable.Rows.Add(dtRow["F_CODE"].ToString(), dtRow["F_CODENM"].ToString());
            }

            return dtTable;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_Init
        /// <summary>
        /// devGrid_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void devGrid_Init(object sender, EventArgs e)
        {
            DevExpress.Web.GridViewDataComboBoxColumn comboBox = null;

            // 품목구분
            comboBox = devGrid.Columns["F_GUBN"] as DevExpress.Web.GridViewDataComboBoxColumn;
            comboBox.PropertiesComboBox.TextField = "F_CODENM";
            comboBox.PropertiesComboBox.ValueField = "F_CODE";
            comboBox.PropertiesComboBox.DataSource = SYCOD01_LST("24");

            // 품목그룹
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_CODE", typeof(string));
            dtTable.Columns.Add("F_CODENM", typeof(string));

            foreach (var oValue in CachecommonCode["AA"]["AAE5"].codeGroup.Values)
            {
                dtTable.Rows.Add(oValue.COMMCD, oValue.COMMNMKR);
            }

            comboBox = devGrid.Columns["F_GROUP"] as DevExpress.Web.GridViewDataComboBoxColumn;
            comboBox.PropertiesComboBox.TextField = "F_CODENM";
            comboBox.PropertiesComboBox.ValueField = "F_CODE";
            comboBox.PropertiesComboBox.DataSource = dtTable;

            // 단위
            comboBox = devGrid.Columns["F_UNIT"] as DevExpress.Web.GridViewDataComboBoxColumn;
            comboBox.PropertiesComboBox.TextField = "F_CODENM";
            comboBox.PropertiesComboBox.ValueField = "F_CODE";
            comboBox.PropertiesComboBox.DataSource = SYCOD01_LST("23");
        }
        #endregion

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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS", "F_MACHSTND" }, e);

            if (e.Column.FieldName.Equals("F_GUBN")
                || e.Column.FieldName.Equals("F_GROUP")
                || e.Column.FieldName.Equals("F_UNIT")
                || e.Column.FieldName.Equals("F_DANGA"))
            {
                if (e.Value == null || e.Value.ToString() == "")
                    e.DisplayText = String.Empty;
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
            if (!e.Column.FieldName.Equals("F_MODELCD")) return;

            if (e.Column.FieldName.Equals("F_MODELCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;

                string errMsg = String.Empty;

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_STATUS", "1");

                    ds = biz.QCD17_LST(oParamDic, out errMsg);
                }

                comboBox.TextField = "F_MODELNM";
                comboBox.ValueField = "F_MODELCD";
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", (Value.NewValues["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_ITEMNM", (Value.NewValues["F_ITEMNM"] ?? "").ToString());
                    oParamDic.Add("F_SPEC", (Value.NewValues["F_SPEC"] ?? "").ToString());
                    oParamDic.Add("F_MODELCD", (Value.NewValues["F_MODELCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_GUBN", (Value.NewValues["F_GUBN"] ?? "").ToString());
                    oParamDic.Add("F_UNIT", (Value.NewValues["F_UNIT"] ?? "").ToString());
                    oParamDic.Add("F_DANGA", (Value.NewValues["F_DANGA"] ?? "0.000000").ToString());
                    oParamDic.Add("F_MACHSTND", Convert.ToBoolean(Value.NewValues["F_MACHSTND"]) ? "1" : "0");
                    oParamDic.Add("F_DESIGN", (Value.NewValues["F_DESIGN"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD01_INS";
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", (Value.NewValues["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_ITEMNM", (Value.NewValues["F_ITEMNM"] ?? "").ToString());
                    oParamDic.Add("F_SPEC", (Value.NewValues["F_SPEC"] ?? "").ToString());
                    oParamDic.Add("F_MODELCD", (Value.NewValues["F_MODELCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_CODENO", (Value.NewValues["F_CODENO"] ?? "").ToString());
                    oParamDic.Add("F_GUBN", (Value.NewValues["F_GUBN"] ?? "").ToString());
                    oParamDic.Add("F_UNIT", (Value.NewValues["F_UNIT"] ?? "").ToString());
                    oParamDic.Add("F_DANGA", (Value.NewValues["F_DANGA"] ?? "0.000000").ToString());
                    oParamDic.Add("F_MACHSTND", Convert.ToBoolean(Value.NewValues["F_MACHSTND"]) ? "1" : "0");
                    oParamDic.Add("F_DESIGN", (Value.NewValues["F_DESIGN"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD01_UPD";
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", (Value.Values["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[idx] = "USP_QCD01_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
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
                            sb_OutMsg.AppendFormat("품목코드 {0}는 검사기준에서 사용중이므로 삭제할 수 없습니다.\r", _oOutPair.Value);
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

                // 품목목록을 구한다
                QCD01_LST();
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
            // 품목목록을 구한다
            QCD01_LST();
        }
        #endregion

        #region devGrid_AutoFilterCellEditorInitialize
        protected void devGrid_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_MODELCD")
                && !e.Column.FieldName.Equals("F_STATUS")
                && !e.Column.FieldName.Equals("F_MACHSTND")
                && !e.Column.FieldName.Equals("F_GROUP")) return;

            string[] filters = devGrid.FilterExpression.Replace(" And ", "|").Split('|');
            string[] filterParams = { "", "" };

            if (e.Column.FieldName.Equals("F_MODELCD"))
            {
                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                string errMsg = String.Empty;

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_STATUS", "1");

                    ds = biz.QCD17_LST(oParamDic, out errMsg);
                }

                ddlComboBox.TextField = "F_MODELNM";
                ddlComboBox.ValueField = "F_MODELCD";
                ddlComboBox.DataSource = ds;
                ddlComboBox.DataBind();

                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "", Value = "", Selected = true });

                foreach (string filter in filters)
                {
                    if (filter.Contains("F_MODELCD"))
                    {
                        filterParams = filter.Split('=');
                        ddlComboBox.SelectedIndex = ddlComboBox.Items.FindByValue(filterParams[1].Trim().Replace("'", "")).Index;
                    }
                }
            }
            else if (e.Column.FieldName.Equals("F_STATUS") || e.Column.FieldName.Equals("F_MACHSTND"))
            {

                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                ddlComboBox.Items.Clear();
                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "중단", Value = false, Selected = false });
                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "사용", Value = true, Selected = false });
                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = null, Selected = true });

                foreach (string filter in filters)
                {
                    if (filter.Contains("F_STATUS") || filter.Contains("F_MACHSTND"))
                    {
                        filterParams = filter.Split('=');
                        ddlComboBox.SelectedIndex = ddlComboBox.Items.FindByValue(Convert.ToBoolean(filterParams[1].Trim().Replace("'", ""))).Index;
                    }
                }
            }
            else if (e.Column.FieldName.Equals("F_GROUP"))
            {
                DevExpress.Web.ASPxComboBox ddlComboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = null, Selected = true });

                foreach (string filter in filters)
                {
                    if (filter.Contains("F_GROUP"))
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 품목정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGrid_DataBound
        /// <summary>
        /// devGrid_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.QCD17_LST(oParamDic, out errMsg);
            }

            var combo = devGrid.Columns["F_MODELCD"] as DevExpress.Web.GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.TextField = "F_MODELNM";
            combo.PropertiesComboBox.ValueField = "F_MODELCD";
            combo.PropertiesComboBox.DataSource = ds;


            //ddlComboBox.TextField = "F_MODELNM";
            //ddlComboBox.ValueField = "F_MODELCD";
            //ddlComboBox.DataSource = ds;
            //ddlComboBox.DataBind();

            //ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "", Value = "", Selected = true });

            //foreach (string filter in filters)
            //{
            //    if (filter.Contains("F_MODELCD"))
            //    {
            //        filterParams = filter.Split('=');
            //        ddlComboBox.SelectedIndex = ddlComboBox.Items.FindByValue(filterParams[1].Trim().Replace("'", "")).Index;
            //    }
            //}
        }
        #endregion

        #endregion
    }
}