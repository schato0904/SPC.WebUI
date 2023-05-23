using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using DevExpress.Web;
using SPC.WERD.Biz;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0312 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
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
            //new ASPxGridViewCellMerger(devGrid, "F_USERID|F_USERNM|F_WORKCD|F_WORKNM");
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
        {
        }
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

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD0312LST()
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());
                ds = biz.WERD0312_LST(oParamDic, out errMsg);
            }

            //devGrid.DataSource = ds;
            SetGrid(ds.Tables[0], ds.Tables[1], ds.Tables[2]);
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid.DataBind();
            }
        }
        #endregion

        void SetGrid(DataTable dt, DataTable dt2, DataTable dt3)
        {
            if (dt == null)
                return;

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("생산수량", typeof(String));
            dtTemp.Columns.Add("검사수량", typeof(String));
            dtTemp.Columns.Add("부적합수량", typeof(String));
            dtTemp.Columns.Add("부적합률(%)", typeof(String));
            dtTemp.Columns.Add("PPM", typeof(String));

            if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["F_TYPENM"].ToString() != null && dt2.Rows[0]["F_TYPENM"].ToString() != "")
                {
                    dtTemp.Columns.Add("부적합유형", typeof(String));
                    foreach (DataRow dtRow3 in dt2.Rows)
                    {
                        if (dtTemp.Columns.IndexOf(dtRow3["F_TYPENM"].ToString()) < 0)
                        {
                            dtTemp.Columns.Add(dtRow3["F_TYPENM"].ToString(), typeof(String));
                        }
                        else
                        {
                            dtTemp.Columns.Add(dtRow3["F_TYPENM"].ToString() + "(유형)", typeof(String));
                        }

                    }
                }

                if (dt3.Rows.Count > 0)
                {
                    if (dt3.Rows[0]["F_CAUSENM"].ToString() != null && dt3.Rows[0]["F_CAUSENM"].ToString() != "")
                    {
                        dtTemp.Columns.Add("부적합원인", typeof(String));
                        foreach (DataRow dtRow4 in dt3.Rows)
                        {
                            if (dtTemp.Columns.IndexOf(dtRow4["F_CAUSENM"].ToString()) < 0)
                            {
                                dtTemp.Columns.Add(dtRow4["F_CAUSENM"].ToString(), typeof(String));
                            }
                            else
                            {
                                dtTemp.Columns.Add(dtRow4["F_CAUSENM"].ToString() + "(원인)", typeof(String));
                            }

                        }
                    }
                }

                // 그리드 DataSource용 DataTable(Pivot)
                foreach (DataRow dtRow1 in dt.Rows)
                {
                    // 데이타 구성용 DataRow
                    //idx = 0;
                    DataRow dtNewRow = dtTemp.NewRow();
                    dtNewRow["검사일자"] = (dtRow1["F_WORKDATE"] ?? "").ToString();
                    dtNewRow["생산수량"] = Convert.ToInt32(dtRow1["F_PRODUCTQTY"] ?? "").ToString("#,##0");
                    dtNewRow["검사수량"] = Convert.ToInt32(dtRow1["F_INSPQTY"] ?? "").ToString("#,##0");
                    dtNewRow["부적합수량"] = Convert.ToInt32(dtRow1["F_NGQTY"] ?? "").ToString("#,##0");
                    dtNewRow["부적합률(%)"] = (dtRow1["F_NGRATE"] ?? "").ToString();
                    dtNewRow["PPM"] = Convert.ToInt32(dtRow1["F_PPM"]).ToString("#,##0");

                    if (dtRow1["F_TYPECNT"] != null && dtRow1["F_TYPECNT"].ToString() != "")
                    {
                        string[] arrstrTypecnt = dtRow1["F_TYPECNT"].ToString().Split('|');
                        string[] arrstrTypenm = dtRow1["F_TYPENM"].ToString().Split('|');
                        int idx = 0;
                        foreach (string strTyepcnt in arrstrTypecnt)
                        {
                            if (dtTemp.Columns.IndexOf(arrstrTypenm[idx] + "(유형)") < 0)
                                dtNewRow[arrstrTypenm[idx]] = string.Format("{0:n0}", strTyepcnt);
                            else
                                dtNewRow[arrstrTypenm[idx] + "(유형)"] = string.Format("{0:n0}", strTyepcnt);
                            idx++;
                        }
                    }

                    if (dtRow1["F_CAUSECNT"] != null && dtRow1["F_CAUSECNT"].ToString() != "")
                    {
                        string[] arrstrTypecnt = dtRow1["F_CAUSECNT"].ToString().Split('|');
                        string[] arrstrTypenm = dtRow1["F_CAUSENM"].ToString().Split('|');
                        int idx = 0;
                        foreach (string strTyepcnt in arrstrTypecnt)
                        {
                            if (dtTemp.Columns.IndexOf(arrstrTypenm[idx] + "(원인)") < 0)
                                dtNewRow[arrstrTypenm[idx]] = string.Format("{0:n0}", strTyepcnt);
                            else
                                dtNewRow[arrstrTypenm[idx] + "(원인)"] = string.Format("{0:n0}", strTyepcnt);
                            idx++;
                        }
                    }
                    dtTemp.Rows.Add(dtNewRow);
                }

                // Pivot Fill
                DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "검사일자");

                devGrid.DataSource = dtPivotTable;
                devGrid.DataBind();
            }
        }

        #endregion

        #region 사용자이벤트

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("검사일자").ToString();

            if (strJudge == "부적합유형")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0xDD, 0xDD, 0xDD);
            }
            else if (strJudge == "부적합원인")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0xDD, 0xDD, 0xDD);
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
            //DevExpressLib.GetUsedString(new string[] { "F_USEYN" }, e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// 저장 처리
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            var result = new Dictionary<string, string>();
            var btnType = e.Parameter;
        }
        #endregion

        #region devGrid1_CustomCallback
        /// <summary>
        /// 거래처별 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region devGrid_CustomCallback
        /// <summary>
        /// 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD0312LST();
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            WERD0312LST();
            devGridExporter.WriteXlsToResponse(String.Format("월부적합유형/원인집계_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion


        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<BR/>", " ");
            }
        }
        #endregion

        protected void devGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName != "검사일자")
                e.Cell.HorizontalAlign = HorizontalAlign.Right;
            else if (e.DataColumn.FieldName == "검사일자")
                e.Cell.HorizontalAlign = HorizontalAlign.Left;  
        }


        #endregion


    }
        #endregion
}