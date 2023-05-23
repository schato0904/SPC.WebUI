using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.User.Biz;

namespace SPC.WebUI.Pages.SYST
{
    public partial class SYST0105 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string[] columnName;
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

            if (!IsCallback)
            {
                // 동적으로 컬럼을 생성한다(대메뉴기준)
                CreateColumns();

                // 사용자통계를 구한다(Master)
                SYUSRLOG_MASTER_LST();
            }

            new ASPxGridViewCellMerger(devGridDetail, "F_WORKDAY|F_MODULE1|F_MODULE2");

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

                devGridDetail.JSProperties["cpResultCode"] = "";
                devGridDetail.JSProperties["cpResultMsg"] = "";
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

        #region 동적으로 컬럼을 생성한다(대메뉴기준)
        void CreateColumns()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.SYUSRLOG_LMENU_LST(oParamDic, out errMsg);
            }

            columnName = new string[ds.Tables[0].Rows.Count];

            int idx = 0;
            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = dtRow["F_COMMCD"].ToString(), Caption = dtRow["F_COMMNM"].ToString(), Width = Unit.Parse("90") };
                devGrid.Columns.Add(column);

                columnName[idx] = dtRow["F_COMMCD"].ToString();
                idx++;
            }
        }
        #endregion

        #region 엑셀용 그리드 컬럼 생성
        void CreateColumns_EXCEL()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.SYUSRLOG_LMENU_LST(oParamDic, out errMsg);
            }

            columnName = new string[ds.Tables[0].Rows.Count];

            int idx = 0;
            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = dtRow["F_COMMCD"].ToString(), Caption = dtRow["F_COMMNM"].ToString(), Width = Unit.Parse("90") };
                devGrid2.Columns.Add(column);

                columnName[idx] = dtRow["F_COMMCD"].ToString();
                idx++;
            }
        }
        #endregion

        #region 사용자통계를 구한다(Master)
        void SYUSRLOG_MASTER_LST()
        {
            string errMsg = String.Empty;

            using (UserBiz biz = new UserBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_GROUPCD", hidAuthority.Text);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYUSRLOG_MASTER_LST(oParamDic, out errMsg);
            }
            
            DataTable dtUserLog = new DataTable();
            dtUserLog.Columns.Add("F_USERNM", typeof(String));
            dtUserLog.Columns.Add("F_USERID", typeof(String));
            dtUserLog.Columns.Add("F_TOTALCNT", typeof(Int32));
            foreach (string column in columnName)
            {
                dtUserLog.Columns.Add(column, typeof(Int32));
            }

            int idx = 0, i = 0, j = 0;
            int[] nCount = new int[columnName.Length + 1];
            if (bExistsDataSet(ds))
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    // 카운트 초기화
                    for (i = 0; i < nCount.Length; i++) nCount[i] = 0;

                    // 모듈별 카운트 설정
                    for (j = 0; j < columnName.Length; j++)
                    {
                        if (columnName[j].Equals(dtRow["F_MODULE1"].ToString()))
                        {
                            nCount[j] = int.Parse(dtRow["F_WORKCNT"].ToString());
                            nCount[nCount.Length - 1] = int.Parse(dtRow["F_WORKCNT"].ToString());
                        }
                    }

                    if (idx.Equals(0))
                    {
                        DataRow newUserLogRow = dtUserLog.NewRow();
                        newUserLogRow["F_USERID"] = dtRow["F_USERID"].ToString();
                        newUserLogRow["F_USERNM"] = dtRow["F_USERNM"].ToString();
                        newUserLogRow["F_TOTALCNT"] = nCount[nCount.Length - 1];
                        j = 0;
                        foreach (string column in columnName)
                        {
                            newUserLogRow[column] = nCount[j];
                            j++;
                        }

                        dtUserLog.Rows.Add(newUserLogRow);
                    }
                    else
                    {
                        bool bExistsPrev = dtUserLog.Select(String.Format("F_USERID='{0}'", dtRow["F_USERID"])).Length > 0;

                        if (!bExistsPrev)
                        {
                            DataRow newUserLogRow = dtUserLog.NewRow();
                            newUserLogRow["F_USERID"] = dtRow["F_USERID"].ToString();
                            newUserLogRow["F_USERNM"] = dtRow["F_USERNM"].ToString();
                            newUserLogRow["F_TOTALCNT"] = nCount[nCount.Length - 1];
                            j = 0;
                            foreach (string column in columnName)
                            {
                                newUserLogRow[column] = nCount[j];
                                j++;
                            }

                            dtUserLog.Rows.Add(newUserLogRow);
                        }
                        else
                        {
                            foreach (DataRow dtUserRow in dtUserLog.Select(String.Format("F_USERID='{0}'", dtRow["F_USERID"])))
                            {
                                j = 0;
                                dtUserRow["F_TOTALCNT"] = int.Parse(dtUserRow["F_TOTALCNT"].ToString()) + nCount[nCount.Length - 1];
                                foreach (string column in columnName)
                                {
                                    dtUserRow[column] = int.Parse(dtUserRow[column].ToString()) + nCount[j];
                                    j++;
                                }
                            }
                        }
                    }

                    idx++;
                }
            }

            devGrid.DataSource = dtUserLog;

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

        #region 엑셀용 사용자 통계
        void SYUSRLOG_MASTER_EXCEL()
        {
            string errMsg = String.Empty;
            
            using (UserBiz biz = new UserBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_GROUPCD", hidAuthority.Text);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYUSRLOG_MASTER_LST(oParamDic, out errMsg);
            }

            DataTable dtUserLog = new DataTable();
            dtUserLog.Columns.Add("F_USERNM", typeof(String));
            dtUserLog.Columns.Add("F_USERID", typeof(String));
            dtUserLog.Columns.Add("F_TOTALCNT", typeof(Int32));
            foreach (string column in columnName)
            {
                dtUserLog.Columns.Add(column, typeof(Int32));
            }

            int idx = 0, i = 0, j = 0;
            int[] nCount = new int[columnName.Length + 1];
            if (bExistsDataSet(ds))
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    // 카운트 초기화
                    for (i = 0; i < nCount.Length; i++) nCount[i] = 0;

                    // 모듈별 카운트 설정
                    for (j = 0; j < columnName.Length; j++)
                    {
                        if (columnName[j].Equals(dtRow["F_MODULE1"].ToString()))
                        {
                            nCount[j] = int.Parse(dtRow["F_WORKCNT"].ToString());
                            nCount[nCount.Length - 1] = int.Parse(dtRow["F_WORKCNT"].ToString());
                        }
                    }

                    if (idx.Equals(0))
                    {
                        DataRow newUserLogRow = dtUserLog.NewRow();
                        newUserLogRow["F_USERID"] = dtRow["F_USERID"].ToString();
                        newUserLogRow["F_USERNM"] = dtRow["F_USERNM"].ToString();
                        newUserLogRow["F_TOTALCNT"] = nCount[nCount.Length - 1];
                        j = 0;
                        foreach (string column in columnName)
                        {
                            newUserLogRow[column] = nCount[j];
                            j++;
                        }

                        dtUserLog.Rows.Add(newUserLogRow);
                    }
                    else
                    {
                        bool bExistsPrev = dtUserLog.Select(String.Format("F_USERID='{0}'", dtRow["F_USERID"])).Length > 0;

                        if (!bExistsPrev)
                        {
                            DataRow newUserLogRow = dtUserLog.NewRow();
                            newUserLogRow["F_USERID"] = dtRow["F_USERID"].ToString();
                            newUserLogRow["F_USERNM"] = dtRow["F_USERNM"].ToString();
                            newUserLogRow["F_TOTALCNT"] = nCount[nCount.Length - 1];
                            j = 0;
                            foreach (string column in columnName)
                            {
                                newUserLogRow[column] = nCount[j];
                                j++;
                            }

                            dtUserLog.Rows.Add(newUserLogRow);
                        }
                        else
                        {
                            foreach (DataRow dtUserRow in dtUserLog.Select(String.Format("F_USERID='{0}'", dtRow["F_USERID"])))
                            {
                                j = 0;
                                dtUserRow["F_TOTALCNT"] = int.Parse(dtUserRow["F_TOTALCNT"].ToString()) + nCount[nCount.Length - 1];
                                foreach (string column in columnName)
                                {
                                    dtUserRow[column] = int.Parse(dtUserRow[column].ToString()) + nCount[j];
                                    j++;
                                }
                            }
                        }
                    }

                    idx++;
                }
            }

            devGrid2.DataSource = dtUserLog;

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

        #region 사용자통계를 구한다(Detail)
        void SYUSRLOG_DETAIL_LST(string F_USERID)
        {
            string errMsg = String.Empty;

            using (UserBiz biz = new UserBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_USERID", F_USERID);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYUSRLOG_DETAIL_LST(oParamDic, out errMsg);
            }

            devGridDetail.DataSource = ds;
            devGridDetail.DataBind();

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridDetail.JSProperties["cpResultCode"] = "0";
                devGridDetail.JSProperties["cpResultMsg"] = errMsg;
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlAuthority_Init
        /// <summary>
        /// ddlAuthority_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlAuthority_Init(object sender, EventArgs e)
        {
            ddlAuthority.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlAuthority.ValueField = "COMMCD";
            ddlAuthority.DataSource = CachecommonCode["AA"]["AAC6"].codeGroup.Values;
            ddlAuthority.DataBind();
        }
        #endregion

        #region ddlAuthority_DataBound
        /// <summary>
        /// ddlAuthority_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlAuthority_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "" };
            ddlAuthority.Items.Insert(0, item);
            ddlAuthority.SelectedIndex = ddlAuthority.Items.FindByValue(hidAuthority.Text).Index;
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
            // 동적으로 컬럼을 생성한다(대메뉴기준)
            CreateColumns();

            // 사용자통계를 구한다(Master)
            SYUSRLOG_MASTER_LST();
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
            if (e.VisibleIndex < 0) return;
            
            if (e.DataColumn.FieldName.Equals("F_TOTALCNT"))
            {
                e.Cell.Font.Bold = Convert.ToInt32(e.CellValue.ToString()) > 0 ? true : false;
            }

            foreach (string column in columnName)
            {
                if (e.DataColumn.FieldName.Equals(column))
                {
                    e.Cell.Font.Bold = Convert.ToInt32(e.CellValue.ToString()) > 0 ? true : false;
                }
            }
        }
        #endregion

        #region devGridDetail CustomCallback
        /// <summary>
        /// devGridDetail_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridDetail_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 사용자통계를 구한다(Detail)
            SYUSRLOG_DETAIL_LST(e.Parameters);
        }
        #endregion

        #region devGridDetail CustomColumnDisplayText
        /// <summary>
        /// devGridDetail_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGridDetail_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            DataRow dtRow = devGridDetail.GetDataRow(e.VisibleRowIndex);

            if (e.Column.FieldName.Equals("F_MODULE1"))
            {
                e.DisplayText = GetCommonCodeText(CachecommonCode["MM"][e.Value.ToString()]);
            }
            else if (e.Column.FieldName.Equals("F_MODULE2"))
            {
                e.DisplayText = GetCommonCodeText(CachecommonCode["MM"][dtRow["F_MODULE1"].ToString()][e.Value.ToString()]);
            }
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            CreateColumns_EXCEL();
            SYUSRLOG_MASTER_EXCEL();
            //devGrid2.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 사용자 통계정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGridExporter_RenderBrick
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {

            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
        }
        #endregion

        #endregion

        
    }
}