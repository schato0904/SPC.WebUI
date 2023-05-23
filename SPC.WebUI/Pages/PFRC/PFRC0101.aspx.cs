using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.PFRC
{
    public partial class PFRC0101 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet dsGrid
        {
            get
            {
                return (DataSet)Session["PFRC0101"];
            }
            set
            {
                Session["PFRC0101"] = value;
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
            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 트리정보를 구한다
                SetCommonCodeTreeBind();
            }
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
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Root Node Expand
                devTree.ExpandToLevel(1);

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

        #region 트리정보를 구한다
        /// <summary>
        /// SetCommonCodeTreeBind
        /// </summary>
        void SetCommonCodeTreeBind()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.GetCommonCodeList(oParamDic, out errMsg);
            }

            DataTable dt = ds.Tables[0];

            // Root Node 생성
            dt.Rows.Add(
                "00",
                "99",
                "기본코드",
                "기본코드",
                "기본코드",
                "기본코드",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                0,
                true
                );

            devTree.DataSource = dt;
            devTree.DataBind();
        }
        #endregion

        #region 선택된 코드의 그리드정보를 구한다
        /// <summary>
        /// SetCommonCodeGridBind
        /// </summary>
        /// <param name="COMMCD">그룹코드</param>
        void SetCommonCodeGridBind(string COMMCD)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_GROUPCD", COMMCD);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                dsGrid = ds;
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

        #region 공통코드 중복체크
        void CHK_QCM10_DUPLICATE(Dictionary<string, string> oDic, out bool bExists)
        {
            using (CommonBiz biz = new CommonBiz())
            {
                bExists = biz.PROC_QCM10_EXT_COMMCD(oParamDic);
            }

            if (bExists)
            {
                procResult = new string[] { "0", "이미 사용중인 코드입니다" };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
        }
        #endregion

        #region 공통코드 신규저장
        void PROC_QCM10_INS(Dictionary<string, string> oDic)
        {
            bool bExecute = false;

            using (CommonBiz biz = new CommonBiz())
            {
                bExecute = biz.PROC_QCM10_INS(oDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "신규저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "신규저장이 완료되었습니다." };
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
        }
        #endregion

        #region 공통코드 수정
        void PROC_QCM10_UPD(Dictionary<string, string> oDic)
        {
            bool bExecute = false;

            using (CommonBiz biz = new CommonBiz())
            {
                bExecute = biz.PROC_QCM10_UPD(oDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "수정 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "수정이 완료되었습니다." };
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
        }
        #endregion

        #region 공통코드 삭제
        void PROC_QCM10_DEL(string F_GROUPCD, string F_COMMCD)
        {
            string[] arr_COMMCD = F_COMMCD.Split(',');
            object[] oParameters = new object[arr_COMMCD.Length];
            int idx = 0;

            foreach (string COMMCD in arr_COMMCD)
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMMCD", COMMCD.Trim());
                oParameters[idx] = (object)oParamDic;
                idx++;
            }

            bool bExecute = false;

            using (CommonBiz biz = new CommonBiz())
            {
                bExecute = biz.PROC_QCM10_DEL(oParameters);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "삭제가 완료되었습니다." };
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];

            // 그리드정보를 구한다
            SetCommonCodeGridBind(F_GROUPCD);

            // 트리정보를 구한다
            SetCommonCodeTreeBind();
            DevExpress.Web.ASPxTreeList.TreeListNode FocusNode = devTree.FindNodeByKeyValue(String.IsNullOrEmpty(hidTreeFocusedKey.Text) ? "99" : hidTreeFocusedKey.Text);
            FocusNode.Focus();
            devTree.ExpandToLevel(FocusNode.Level);
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] oParam = e.Parameters.Split(';');
            string sAction = oParam[0];
            oParam = oParam[1].Split('|');
            if (sAction.Equals("select"))
            {
                // 그리드정보를 구한다
                SetCommonCodeGridBind(oParam[0]);

                // 입력, 수정모드 취소
                devGrid.CancelEdit();
            }
            else if (sAction.Equals("new"))
            {
                // 그리드정보를 구한다
                SetCommonCodeGridBind(oParam[0]);

                // 입력 Row 생성
                devGrid.AddNewRow();
            }
            else if (sAction.Equals("edit"))
            {
                // 그리드정보를 구한다
                SetCommonCodeGridBind(oParam[0]);

                // 수정모드 시작
                devGrid.StartEdit(devGrid.FindVisibleIndexByKeyValue(oParam[1]));
            }
            else if (sAction.Equals("cancel"))
            {
                // 그리드정보를 구한다
                SetCommonCodeGridBind(oParam[0]);

                // 입력, 수정모드 취소
                devGrid.CancelEdit();
            }
            else if (sAction.Equals("delete"))
            {
                PROC_QCM10_DEL(oParam[0], oParam[1]);
                
                //// 그리드정보를 구한다
                //SetCommonCodeGridBind(oParam[0]);
            }
            else if (sAction.Equals("save"))
            {
                // 수정모드인 경우 그리드 바인딩 후 업데이트한다
                if (!devGrid.IsNewRowEditing)
                {
                    // 그리드정보를 구한다
                    SetCommonCodeGridBind(oParam[0]);
                }

                // 업데이트 시작
                devGrid.UpdateEdit();
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

        #region devGrid InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            // 그룹코드 기본값(선택된 그룹코드) 셋팅
            DevExpress.Web.ASPxTextBox txtGROUPCD = DevExpressLib.devTextBox(devGrid, "txtGROUPCD");
            txtGROUPCD.Text = hidTreeFocusedKey.Text;
            txtGROUPCD.ClientSideEvents.Init = "fn_OnControlDisable";

            // 사용여부 기본값(false) 셋팅
            DevExpressLib.devCheckBox(devGrid, "chkSTATUS").Checked = true;
        }
        #endregion

        #region devGrid HtmlEditFormCreated
        /// <summary>
        /// devGrid_HtmlEditFormCreated
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditFormEventArgs</param>
        protected void devGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("cancel") && !param.Contains("new") && !param.Contains("save"))
                {
                    DevExpressLib.devTextBox(devGrid, "txtGROUPCD").ClientSideEvents.Init = "fn_OnControlDisable";
                    DevExpressLib.devTextBox(devGrid, "txtCOMMCD").ClientSideEvents.Init = "fn_OnControlDisable";
                }
            }
        } 
        #endregion

        #region devGrid RowInserting
        /// <summary>
        /// devGrid_RowInserting
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInsertingEventArgs</param>
        protected void devGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            // 중복검사
            bool bExists = false;
            oParamDic.Clear();
            oParamDic.Add("F_COMMCD", e.NewValues["F_COMMCD"].ToString());
            CHK_QCM10_DUPLICATE(oParamDic, out bExists);

            if (!bExists)
            {
                oParamDic.Clear();
                oParamDic.Add("F_GROUPCD", e.NewValues["F_GROUPCD"] == null ? "" : e.NewValues["F_GROUPCD"].ToString().Trim());
                oParamDic.Add("F_COMMCD", e.NewValues["F_COMMCD"] == null ? "" : e.NewValues["F_COMMCD"].ToString().Trim());
                oParamDic.Add("F_COMMNMKR", e.NewValues["F_COMMNMKR"] == null ? "" : e.NewValues["F_COMMNMKR"].ToString().Trim());
                oParamDic.Add("F_COMMNMUS", e.NewValues["F_COMMNMUS"] == null ? "" : e.NewValues["F_COMMNMUS"].ToString().Trim());
                oParamDic.Add("F_COMMNMCN", e.NewValues["F_COMMNMCN"] == null ? "" : e.NewValues["F_COMMNMCN"].ToString().Trim());
                oParamDic.Add("F_PARAM01", e.NewValues["F_PARAM01"] == null ? "" : e.NewValues["F_PARAM01"].ToString().Trim());
                oParamDic.Add("F_PARAM02", e.NewValues["F_PARAM02"] == null ? "" : e.NewValues["F_PARAM02"].ToString().Trim());
                oParamDic.Add("F_PARAM03", e.NewValues["F_PARAM03"] == null ? "" : e.NewValues["F_PARAM03"].ToString().Trim());
                oParamDic.Add("F_PARAM04", e.NewValues["F_PARAM04"] == null ? "" : e.NewValues["F_PARAM04"].ToString().Trim());
                oParamDic.Add("F_PARAM05", e.NewValues["F_PARAM05"] == null ? "" : e.NewValues["F_PARAM05"].ToString().Trim());
                oParamDic.Add("F_REMARK1", e.NewValues["F_REMARK1"] == null ? "" : e.NewValues["F_REMARK1"].ToString().Trim());
                oParamDic.Add("F_REMARK2", e.NewValues["F_REMARK2"] == null ? "" : e.NewValues["F_REMARK2"].ToString().Trim());
                oParamDic.Add("F_SORTNO", e.NewValues["F_SORTNO"] == null ? "" : e.NewValues["F_SORTNO"].ToString().Trim());
                oParamDic.Add("F_STATUS", e.NewValues["F_STATUS"] == null ? "" : e.NewValues["F_STATUS"].ToString().Trim());
                oParamDic.Add("F_USER", gsUSERID);

                // 공통코드 신규저장
                PROC_QCM10_INS(oParamDic);

                devGrid.CancelEdit();
            }
            else
            {
                oParamDic.Add("F_GROUPCD", e.NewValues["F_GROUPCD"].ToString());
            }

            
            e.Cancel = true;

            // 그리드정보를 구한다
            SetCommonCodeGridBind(oParamDic["F_GROUPCD"]);

            if (!bExists)
            {
                // 트리정보를 구한다
                SetCommonCodeTreeBind();
                DevExpress.Web.ASPxTreeList.TreeListNode FocusNode = devTree.FindNodeByKeyValue(String.IsNullOrEmpty(hidTreeFocusedKey.Text) ? "99" : hidTreeFocusedKey.Text);
                FocusNode.Focus();
                devTree.ExpandToLevel(FocusNode.Level);
            }
        }
        #endregion

        #region devGrid RowUpdating
        /// <summary>
        /// devGrid_RowUpdating
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataUpdatingEventArgs</param>
        protected void devGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_GROUPCD", e.NewValues["F_GROUPCD"] == null ? "" : e.NewValues["F_GROUPCD"].ToString().Trim());
            oParamDic.Add("F_COMMCD", e.NewValues["F_COMMCD"] == null ? "" : e.NewValues["F_COMMCD"].ToString().Trim());
            oParamDic.Add("F_COMMNMKR", e.NewValues["F_COMMNMKR"] == null ? "" : e.NewValues["F_COMMNMKR"].ToString().Trim());
            oParamDic.Add("F_COMMNMUS", e.NewValues["F_COMMNMUS"] == null ? "" : e.NewValues["F_COMMNMUS"].ToString().Trim());
            oParamDic.Add("F_COMMNMCN", e.NewValues["F_COMMNMCN"] == null ? "" : e.NewValues["F_COMMNMCN"].ToString().Trim());
            oParamDic.Add("F_PARAM01", e.NewValues["F_PARAM01"] == null ? "" : e.NewValues["F_PARAM01"].ToString().Trim());
            oParamDic.Add("F_PARAM02", e.NewValues["F_PARAM02"] == null ? "" : e.NewValues["F_PARAM02"].ToString().Trim());
            oParamDic.Add("F_PARAM03", e.NewValues["F_PARAM03"] == null ? "" : e.NewValues["F_PARAM03"].ToString().Trim());
            oParamDic.Add("F_PARAM04", e.NewValues["F_PARAM04"] == null ? "" : e.NewValues["F_PARAM04"].ToString().Trim());
            oParamDic.Add("F_PARAM05", e.NewValues["F_PARAM05"] == null ? "" : e.NewValues["F_PARAM05"].ToString().Trim());
            oParamDic.Add("F_REMARK1", e.NewValues["F_REMARK1"] == null ? "" : e.NewValues["F_REMARK1"].ToString().Trim());
            oParamDic.Add("F_REMARK2", e.NewValues["F_REMARK2"] == null ? "" : e.NewValues["F_REMARK2"].ToString().Trim());
            oParamDic.Add("F_SORTNO", e.NewValues["F_SORTNO"] == null ? "" : e.NewValues["F_SORTNO"].ToString().Trim());
            oParamDic.Add("F_STATUS", e.NewValues["F_STATUS"] == null ? "" : e.NewValues["F_STATUS"].ToString().Trim());
            oParamDic.Add("F_USER", gsUSERID);

            // 공통코드 수정
            PROC_QCM10_UPD(oParamDic);

            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;

            // 그리드정보를 구한다
            SetCommonCodeGridBind(oParamDic["F_GROUPCD"]);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            RefreshCommonCodeCache();
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
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("사용<br/>여부", "사용여부");
            }
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
            //QWK04A_ADTR0103_LST();
            devGrid.DataSource = dsGrid;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 공통코드정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}