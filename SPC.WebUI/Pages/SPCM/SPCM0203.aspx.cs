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
    public partial class SPCM0203 : WebUIBasePage
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
                SPCM0203_LST();
                SPCM0203D_LST();
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
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devTree.JSProperties["cpResultCode"] = "";
                devTree.JSProperties["cpResultMsg"] = "";
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
            //SetCscd();
            ddlStcdBind();
            ddlOtypeBind();
            devTree.DataBind();
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

        #region 출고마스터 목록을 구한다
        void SPCM0203_LST()
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_FROMDT", GetFromDt1());
                oParamDic.Add("F_TODT", GetToDt1());
                ds = biz.SPCM0203_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid2.DataBind();
            }
        }


        #endregion

        #region 출고디테일 목록을 구한다
        void SPCM0203D_LST()
        {
            string[] oParams = null;
            oParams = hidGridParentKey.Text.Split('|');
            if (oParams[0] == null || String.IsNullOrEmpty(oParams[0]))
            {
                oParams[0] = "";
                //hidGridNew.Text = "NEW";
            }
            else {
                //hidGridNew.Text = "";
            }
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_OPILBONO", oParams[0]);
                ds = biz.SPCM0203D_LST(oParamDic, out errMsg);
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
            ddlStcd.DataSource = stds;
            ddlStcd.TextField = "F_STNM";
            ddlStcd.ValueField = "F_STCD";
            ddlStcd.DataBind();
            ddlStcd.SelectedIndex = 0;
        }

        #endregion

        #region 출고유형을 구한다

        void ddlOtypeBind()
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STATUS", "1");
                stds = biz.SPB03_LST(oParamDic, out errMsg);
            }
            ddlOtype.DataSource = stds;
            ddlOtype.TextField = "F_OTYPENM";
            ddlOtype.ValueField = "F_OTYPECD";
            ddlOtype.DataBind();
            ddlOtype.SelectedIndex = 0;
        }

        #endregion

        #region 출고번호를 구한다
        string MAXOPNO()
        {
            string errMsg = String.Empty;
            string maxopbo = string.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_DATE",GetFromDt());
                ds = biz.MAXOPNO_LST(oParamDic, out errMsg);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                maxopbo = ds.Tables[0].Rows[0]["MAXOPNO"].ToString();
                //ipno = ds.Tables[0].Rows[0]["MAXIPNO"].ToString();
            }
            else maxopbo = "";

            return maxopbo;
        }


        #endregion

        #region 트리정보 목록조회(DevExpress 용)
        DataSet DEVTREE_LST(out string errMsg)
        {
            string resultCode = String.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            UserControl _UserControl = Page.FindControl("ucCommonCodeDDL") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMMONCODECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());

                ds = biz.DEVTREE_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트B

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

            string maxopilbono = MAXOPNO();
            if (maxopilbono.Length == 0) return;

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                    oParamDic.Add("F_OPILBONO", maxopilbono);
                    oParamDic.Add("F_ITCD", (Value.NewValues["F_ITCD"] ?? "").ToString());
                    oParamDic.Add("F_OPQTY", (Value.NewValues["F_OPQTY"] ?? "").ToString());
                    oParamDic.Add("F_REMARK", (Value.NewValues["F_REMARK"] ?? "").ToString());
                    oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());
                    oParamDic.Add("F_OPDT", GetFromDt());
                    oParamDic.Add("F_OTYPECD", (this.ddlOtype.Value ?? "").ToString());
                    oParamDic.Add("F_INSUSER", gsUSERNM);
                    
                    oSPs[idx] = "USP_SPM04_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                //foreach (var Value in e.UpdateValues)
                //{
                //    oParamDic = new Dictionary<string, string>();
                //    oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                //    oParamDic.Add("F_ITYPECD", (Value.NewValues["F_ITYPECD"] ?? "").ToString());
                //    oParamDic.Add("F_ITYPENM", (Value.NewValues["F_ITYPENM"] ?? "").ToString());
                //    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                //    oSPs[idx] = "USP_SPB06_UPD";
                //    oParameters[idx] = (object)oParamDic;

                //    idx++;
                //}
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
                bExecute = biz.PROC_SPB01_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                
                SPCM0203_LST();
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
            SPCM0203D_LST();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            SPCM0203_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 출고등록정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devTree_CustomCallback
        /// <summary>
        /// devTree_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">TreeListCustomCallbackEventArgs</param>
        protected void devTree_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            // 트리정보를 구한다
            devTree.DataBind();
        }
        #endregion

        #region devTree_DataBound


        protected void devTree_DataBound(object sender, EventArgs e)
        {
            string errMsg = String.Empty;

            //devTree.DataSource = DEVTREE_LST(out errMsg);

            //if (!String.IsNullOrEmpty(errMsg))
            //{
            //    // Grid Callback Init
            //    devTree.JSProperties["cpResultCode"] = "0";
            //    devTree.JSProperties["cpResultMsg"] = errMsg;
            //}

            DataView dView = null;

            dView = DEVTREE_LST(out errMsg).Tables[0].DefaultView;


            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devTree.JSProperties["cpResultCode"] = "0";
                devTree.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                // 트리정보를 구한다
                devTree.DataSource = dView;
            }
        }
        #endregion

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
          
        }

        #endregion

        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            
        }

       
    }
}