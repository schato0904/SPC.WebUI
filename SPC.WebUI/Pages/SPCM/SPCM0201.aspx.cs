using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.SPCM.Biz;

namespace SPC.WebUI.Pages.SPCM
{
    public partial class SPCM0201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet csds = null;
        DataSet stds = null;
        //string ipno = null;
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
                // 생산무사유 목록을 구한다
                SPCM0201_LST();
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

                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
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
        {
            SetCscd();
            ddlStcdBind();
            //기초입고번호 한번만 입력
            //txt_IPNO.Text = "i000000000000";
            //ipno = "i000000000000";
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 출고유형 목록을 구한다
        void SPCM0201_LST()
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());
                ds = biz.SPCM0201D_LST(oParamDic, out errMsg);
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["F_NQTY"] = string.Format("{0:n0}", int.Parse(dr["F_NQTY"].ToString()));
                dr["F_PRICE"] = string.Format("{0:n0}", int.Parse(dr["F_PRICE"].ToString()));
                dr["F_SUM"] = string.Format("{0:n0}", int.Parse(dr["F_SUM"].ToString()));
                //dr["F_IPQTY"] = string.Format("{0:n0}", int.Parse(dr["F_IPQTY"].ToString()));

            }
            devGrid.DataSource = ds;
            //hidGridCount.Text = ds.Tables[0].Rows.Count.ToString();

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

        #region 창고목록을 구한다

        void ddlStcdBind()
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STATUS", "1");
                stds = biz.SPB01_LST(oParamDic, out errMsg);
            }

            if (stds.Tables[0].Rows.Count > 0)
            {
                stds.Tables[0].Rows.Add();
                stds.Tables[0].Rows[stds.Tables[0].Rows.Count - 1]["F_STCD"] = "";
                stds.Tables[0].Rows[stds.Tables[0].Rows.Count - 1]["F_STNM"] = "전체";
            }
            ddlStcd.DataSource = stds;
            ddlStcd.TextField = "F_STNM";
            ddlStcd.ValueField = "F_STCD";
            ddlStcd.DataBind();
            ddlStcd.SelectedIndex = stds.Tables[0].Rows.Count;
        }

        #endregion

        #region 거래처목록을 구한다

        void SetCscd()
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STATUS", "1");
                csds = biz.SPB02_LST(oParamDic, out errMsg);
            }
            
            csds.Tables[0].Rows.Add();
            csds.Tables[0].Rows[csds.Tables[0].Rows.Count - 1]["F_CSCD"] = "";
            csds.Tables[0].Rows[csds.Tables[0].Rows.Count - 1]["F_CSNM"] = "없음";

            //ddlCscd.DataSource = csds;
            //ddlCscd.TextField = "F_CSNM";
            //ddlCscd.ValueField = "F_CSCD";
            //ddlCscd.DataBind();
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
            //e.NewValues["F_STATUS"] = true;
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
            //DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
            //DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);
            //if (e.Column.FieldName.Equals("F_NOWORKNM"))
            //{
            //    e.DisplayText = dtRow["F_NOWORKNM"].ToString();
            //}
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
                //foreach (var Value in e.InsertValues)
                //{
                //    oParamDic = new Dictionary<string, string>();
                //    oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                //    oParamDic.Add("F_IPILBONO", "i0000000000" + this.ddlStcd.Value);
                //    oParamDic.Add("F_ITCD", (Value.NewValues["F_ITCD"] ?? "").ToString());
                //    oParamDic.Add("F_IPQTY", (Value.NewValues["F_IPQTY"] ?? "").ToString());
                //    oParamDic.Add("F_PRICE", (Value.NewValues["F_PRICE"] ?? "").ToString());
                //    oParamDic.Add("F_CSCD", (Value.NewValues["F_CSCD"] ?? "").ToString());
                //    oParamDic.Add("F_REMARK", (Value.NewValues["F_REMARK"] ?? "").ToString());
                //    oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());
                //    oParamDic.Add("F_IPDT", GetFromDt());
                //    oParamDic.Add("F_ITYPECD", "IP000");
                //    oParamDic.Add("F_INSUSER", gsUSERNM);

                //    oSPs[idx] = "USP_SPM02_INS";
                //    oParameters[idx] = (object)oParamDic;

                //    idx++;
                //}
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                    oParamDic.Add("F_ITCD", (Value.NewValues["F_ITCD"] ?? "").ToString());
                    oParamDic.Add("F_NQTY", (Value.NewValues["F_NQTY"] ?? "").ToString());
                    oParamDic.Add("F_PRICE", (Value.NewValues["F_PRICE"] ?? "").ToString());
                    oParamDic.Add("F_SUM", (Value.NewValues["F_SUM"] ?? "").ToString());
                    oParamDic.Add("F_CSCD", (Value.NewValues["F_CSCD"] ?? "").ToString());
                    oParamDic.Add("F_REMARK", (Value.NewValues["F_REMARK"] ?? "").ToString());
                    oParamDic.Add("F_STCD", (Value.Keys[0] ?? "").ToString());
                    oParamDic.Add("F_IPDT", GetFromDt());
                    //oParamDic.Add("F_ITYPECD", "IP000");
                    oParamDic.Add("F_INSUSER", gsUSERNM);

                    oSPs[idx] = "USP_SPM08_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                //foreach (var Value in e.DeleteValues)
                //{
                //    oParamDic = new Dictionary<string, string>();
                //    oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                //    oParamDic.Add("F_ITYPECD", (Value.Values["F_ITYPECD"] ?? "").ToString());

                //    oSPs[idx] = "USP_SPB06_DEL";
                //    oParameters[idx] = (object)oParamDic;

                //    idx++;
                //}
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                bExecute = biz.PROC_SPB01_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                
                SPCM0201_LST();
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
            // 생산무사유 목록을 구한다
            SPCM0201_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 기초재고정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STATUS", "1");
                csds = biz.SPB02_LST(oParamDic, out errMsg);
            }

            csds.Tables[0].Rows.Add();
            csds.Tables[0].Rows[csds.Tables[0].Rows.Count - 1]["F_CSCD"] = "";
            csds.Tables[0].Rows[csds.Tables[0].Rows.Count - 1]["F_CSNM"] = "";

            var combo = devGrid.Columns["F_CSCD"] as DevExpress.Web.GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.TextField = "F_CSNM";
            combo.PropertiesComboBox.ValueField = "F_CSCD";
            combo.PropertiesComboBox.DataSource = csds;
        }

        #endregion

        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            //if (!e.Column.FieldName.Equals("F_CSCD")) return;


            //if (e.Column.FieldName.Equals("F_CSCD"))
            //{
            //    DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;

            //    string errMsg = String.Empty;

            //    using (SPCMBiz biz = new SPCMBiz())
            //    {
            //        oParamDic.Clear();
            //        oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
            //        oParamDic.Add("F_STATUS", "1");
            //        csds = biz.SPB02_LST(oParamDic, out errMsg);
            //    }

            //    comboBox.TextField = "F_CSNM";
            //    comboBox.ValueField = "F_CSCD";
            //    comboBox.DataSource = csds;
            //    comboBox.DataBind();
            //    comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "없음", Value = "", Selected = true });
            //}
        }

        #region devCallback_Callback
        /// <summary>
        /// 재고마감처리 콜백
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string[] oSPs = new string[1];
            object[] oParameters = new object[1];

                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());
                oParamDic.Add("F_IPDT", GetFromDt());
                oParamDic.Add("F_INSUSER", gsUSERNM);

                oSPs[0] = "USP_SPM08D_INS";
                oParameters[0] = (object)oParamDic;


            bool bExecute = false;
            string resultMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                bExecute = biz.PROC_SPB01_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };


                SPCM0201_LST();
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
        }

        #endregion
    }
}