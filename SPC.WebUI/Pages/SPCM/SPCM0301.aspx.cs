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
    public partial class SPCM0301 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet tyds = null;
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
            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                SPCM0301D_LST();
            }

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
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

                ds = biz.DEVTREE_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 출고디테일 목록을 구한다
        void SPCM0301D_LST()
        {
            string[] oParams = null;
            oParams = hidGridParentKey.Text.Split('|');
            if (oParams[0] == null || String.IsNullOrEmpty(oParams[0]))
            {
                oParams[0] = "";
            }
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_STCD", (this.ddlStcd.Value ?? "").ToString());
                oParamDic.Add("F_FRDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITCD", txtITEMCD.Text);
                ds = biz.SPCM0301_LST(oParamDic, out errMsg);
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
                oParamDic.Add("F_ADMIN", "1");
                stds = biz.SPB01_LST(oParamDic, out errMsg);
            }
            ddlStcd.DataSource = stds;
            ddlStcd.TextField = "F_STNM";
            ddlStcd.ValueField = "F_STCD";
            ddlStcd.DataBind();
            ddlStcd.SelectedIndex = 0;
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

        #endregion

        #region 사용자이벤트B

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

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            SPCM0301D_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 월수불현황정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
          
        }

        #region txtITEMCD_Init
        /// <summary>
        /// txtITEMCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupSPItemSearch('FORM','')");
        }
        #endregion

        #region ITEMCallback_Callback

        protected void ITEMCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (SPCMBiz biz = new SPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCSRCOMPCD);
                oParamDic.Add("F_ITCD", txtITEMCD.Text);

                ds = biz.SPB05_LST(oParamDic, out errMsg);
            }

            string ITEMCD = String.Empty;
            string ITEMNM = String.Empty;

            if (true == bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        ITEMCD = (string)dtRow["F_ITCD"];
                        ITEMNM = (string)dtRow["F_ITNM"];
                    }
                }
            }

            ITEMCallback.JSProperties["cpITEMCD"] = ITEMCD;
            ITEMCallback.JSProperties["cpITEMNM"] = ITEMNM;
        }
        #endregion

        #endregion

        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            
        }


    }
}