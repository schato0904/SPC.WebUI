using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.PFRC.Biz;

namespace SPC.WebUI.Pages.PFRC
{
    public partial class PFRC0202 : WebUIBasePage
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
                // 메뉴분류를 구한다
                GetCommonCodeList("MM", ddlMODULE1);

                // 메뉴목록을 구한다
                SYPGM02_LST();
            }

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

        #region 메뉴분류를 구한다
        void GetCommonCodeList(string groupCD, DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            if (!String.IsNullOrEmpty(groupCD))
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_GROUPCD", groupCD);
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);

                    ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                }
            }
            else
                ds = null;

            comboBox.TextField = "F_COMMNM";
            comboBox.ValueField = "F_COMMCD";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 프로그램목록을 구한다
        void SYPGM01_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (PFRCBiz biz = new PFRCBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                ds = biz.SYPGM01_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_PGMNM";
            comboBox.ValueField = "F_PGMID";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 메뉴목록을 구한다
        void SYPGM02_LST()
        {
            string errMsg = String.Empty;

            using (PFRCBiz biz = new PFRCBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MODULE1", txtMODULE1.Text);
                oParamDic.Add("F_MODULE2", txtMODULE2.Text);
                oParamDic.Add("F_PGMID", txtPGMID.Text);
                if (chkISADMIN.Checked)
                    oParamDic.Add("F_ISADMIN", "1");
                if (chkISDEV.Checked)
                    oParamDic.Add("F_ISDEV", "1");
                if (!String.IsNullOrEmpty(rdoSTATUS.SelectedItem.Value.ToString()))
                    oParamDic.Add("F_STATUS", rdoSTATUS.SelectedItem.Value.ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYPGM02_LST(oParamDic, out errMsg);
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
            e.NewValues["F_ISADMIN"] = false;
            e.NewValues["F_ISDEV"] = false;
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS1", "F_STATUS2", "F_STATUS3", "F_STATUS", "F_ISADMIN", "F_ISDEV" }, e);
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

        #region ddlMODULE2_Callback
        /// <summary>
        /// ddlMODULE2_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlMODULE2_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            ddlMODULE2.Items.Clear();
            // 메뉴분류를 구한다
            GetCommonCodeList(e.Parameter, ddlMODULE2);
            ddlMODULE2.DataBind();
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
            if (!e.Column.FieldName.Equals("F_MODULE1") && !e.Column.FieldName.Equals("F_MODULE2") && !e.Column.FieldName.Equals("F_PGMID")) return;

            if (e.Column.FieldName.Equals("F_MODULE1"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                GetCommonCodeList("MM", comboBox);
                comboBox.DataBindItems();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }
            else if (e.Column.FieldName.Equals("F_PGMID"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                SYPGM01_LST(comboBox);
                comboBox.DataBindItems();

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
                    oParamDic.Add("F_MODULE1", (Value.NewValues["F_MODULE1"] ?? "").ToString());
                    oParamDic.Add("F_MODULE2", (Value.NewValues["F_MODULE2"] ?? "").ToString());
                    oParamDic.Add("F_PGMID", (Value.NewValues["F_PGMID"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_ISADMIN", (Value.NewValues["F_ISADMIN"] ?? "").ToString());
                    oParamDic.Add("F_ISDEV", (Value.NewValues["F_ISDEV"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_SYPGM02_INS";
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
                    oParamDic.Add("F_MODULE1_OLD", (Value.OldValues["F_MODULE1"] ?? "").ToString());
                    oParamDic.Add("F_MODULE2_OLD", (Value.OldValues["F_MODULE2"] ?? "").ToString());
                    oParamDic.Add("F_PGMID_OLD", (Value.OldValues["F_PGMID"] ?? "").ToString());
                    oParamDic.Add("F_MODULE1", (Value.NewValues["F_MODULE1"] ?? "").ToString());
                    oParamDic.Add("F_MODULE2", (Value.NewValues["F_MODULE2"] ?? "").ToString());
                    oParamDic.Add("F_PGMID", (Value.NewValues["F_PGMID"] ?? "").ToString());
                    oParamDic.Add("F_SEQNO", (Value.NewValues["F_SEQNO"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_ISADMIN", (Value.NewValues["F_ISADMIN"] ?? "").ToString());
                    oParamDic.Add("F_ISDEV", (Value.NewValues["F_ISDEV"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_SYPGM02_UPD";
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
                    oParamDic.Add("F_MODULE1", (Value.Values["F_MODULE1"] ?? "").ToString());
                    oParamDic.Add("F_MODULE2", (Value.Values["F_MODULE2"] ?? "").ToString());
                    oParamDic.Add("F_PGMID", (Value.Values["F_PGMID"] ?? "").ToString());

                    oSPs[idx] = "USP_SYPGM02_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (PFRCBiz biz = new PFRCBiz())
            {
                bExecute = biz.PROC_SYPGM02_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 메뉴목록을 구한다
                SYPGM02_LST();
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
            // 메뉴목록을 구한다
            SYPGM02_LST();
        }
        #endregion

        #region ddlMODULE2Edit_DataBound
        /// <summary>
        /// ddlMODULE2Edit_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlMODULE2Edit_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #region ddlMODULE2Edit_Callback
        /// <summary>
        /// ddlMODULE2Edit_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlMODULE2Edit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParam = e.Parameter.Split('|');
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Clear();
            GetCommonCodeList(oParam[0], comboBox);
            comboBox.DataBind();

            if (!String.IsNullOrEmpty(oParam[1]))
                comboBox.SelectedIndex = comboBox.Items.FindByValue(oParam[1]).Index;
            else
                comboBox.SelectedIndex = 0;
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
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Header
                || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_ISADMIN") && e.RowType == DevExpress.Web.GridViewRowType.Header
                || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_ISDEV") && e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<br />", "");
            }
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data
                || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_ISADMIN") && e.RowType == DevExpress.Web.GridViewRowType.Data
                || devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_ISDEV") && e.RowType == DevExpress.Web.GridViewRowType.Data)
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
            //QWK04A_ADTR0103_LST();
            //devGrid.DataSource = dsGrid;
            //devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 메뉴정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}