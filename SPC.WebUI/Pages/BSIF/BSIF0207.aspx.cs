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

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0207 : WebUIBasePage
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
                // 공정목록을 구한다
                QCD74_LST();
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

        #region 반코드 정보를 불러온다
        void QCD72_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.GetQCD72_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_BANNM";
            comboBox.ValueField = "F_BANCD";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 라인코드 정보를 불러온다
        void QCD73_LST(string strBancd, DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", strBancd);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.QCD73_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_LINENM";
            comboBox.ValueField = "F_LINECD";
            comboBox.DataSource = ds;
        }
        #endregion

        #region PC 정보를 불러온다
        void PCNM_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.QCDPCNM_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_PCNM";
            comboBox.ValueField = "F_PCNM";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 공정정보를 구한다
        void QCD74_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());


                ds = biz.QCD74_MACH_LST(oParamDic, out errMsg);
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
            if (!e.Column.FieldName.Equals("F_BANCD") && !e.Column.FieldName.Equals("F_LINECD") && !e.Column.FieldName.Equals("F_PCNM")) return;

            if (e.Column.FieldName.Equals("F_BANCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                QCD72_LST(comboBox);
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }

            if (e.Column.FieldName.Equals("F_PCNM"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                PCNM_LST(comboBox);
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "", Value = "", Selected = true });
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
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
            //if (e.InsertValues.Count > 0)
            //{
            //    foreach (var Value in e.InsertValues)
            //    {
            //        oParamDic = new Dictionary<string, string>();
            //        oParamDic.Add("F_COMPCD", gsCOMPCD);
            //        oParamDic.Add("F_FACTCD", gsFACTCD);
            //        oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
            //        oParamDic.Add("F_LINECD", (Value.NewValues["F_LINECD"] ?? "").ToString());
            //        oParamDic.Add("F_WORKCD", (Value.NewValues["F_WORKCD1"] ?? "").ToString() + (Value.NewValues["F_WORKCD2"] ?? "").ToString());
            //        oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
            //        oParamDic.Add("F_MACHGUBUN", (Value.NewValues["F_MACHGUBUN"] ?? "").ToString());
            //        oParamDic.Add("F_WORKNM", (Value.NewValues["F_WORKNM"] ?? "").ToString());
            //        oParamDic.Add("F_SORTNO", (Value.NewValues["F_SORTNO"] ?? "").ToString());
            //        oParamDic.Add("F_PCNM", (Value.NewValues["F_PCNM"] ?? "").ToString());
            //        oParamDic.Add("F_USER", gsUSERID);

            //        oSPs[idx] = "USP_QCD74_INS";
            //        oParameters[idx] = (object)oParamDic;

            //        idx++;
            //    }
            //}
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_WORKCD", (Value.NewValues["F_WORKCD1"] ?? "").ToString() + (Value.NewValues["F_WORKCD2"] ?? "").ToString());
                    oParamDic.Add("F_MACHCD", (Value.NewValues["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD74_MACH_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            //if (e.DeleteValues.Count > 0)
            //{
            //    foreach (var Value in e.DeleteValues)
            //    {
            //        oParamDic = new Dictionary<string, string>();
            //        oParamDic.Add("F_COMPCD", gsCOMPCD);
            //        oParamDic.Add("F_FACTCD", gsFACTCD);
            //        oParamDic.Add("F_MODULE1", (Value.Values["F_MODULE1"] ?? "").ToString());
            //        oParamDic.Add("F_MODULE2", (Value.Values["F_MODULE2"] ?? "").ToString());
            //        oParamDic.Add("F_PGMID", (Value.Values["F_PGMID"] ?? "").ToString());

            //        oSPs[idx] = "USP_QCD72_TRAN";
            //        oParameters[idx] = (object)oParamDic;

            //        idx++;
            //    }
            //}
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
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 공정목록을 구한다
                QCD74_LST();
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
            // 공정목록을 구한다
            QCD74_LST();
        }
        #endregion

        #region ddlLINEEdit_DataBound
        /// <summary>
        /// ddlLINEEdit_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlLINEEdit_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #region ddlLINEEdit_Callback
        /// <summary>
        /// ddlLINEEdit_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlLINEEdit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParam = e.Parameter.Split('|');
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Clear();
            QCD73_LST(oParam[0], comboBox);
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 공정별 설비정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}