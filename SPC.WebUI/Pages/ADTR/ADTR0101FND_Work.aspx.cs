using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.ADTR.Biz;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0101FND_Work : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string ImageURL = String.Empty;
        protected string iconURL = String.Empty;
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

            //ImagURL = string.Format("../../Resources/controls/login/{0}/Image", gsLOGINPGMID);
            ImageURL = Page.ResolveUrl(String.Format("~/API/Common/Gathering/Download.ashx?code={0}&name={1}&gbn={2}", gsCOMPCD, "monitoring.png", "monitoring"));
            iconURL = Page.ResolveUrl("~/Resources/icons/");

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // PC정보를 구한다
                ADTR0101_WORK_LST();
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
                //QCDPCNM_ADTR0101_LST();
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "";
                pnlSearch.JSProperties["cpResultMsg"] = "";
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공정모니터링정보를 구한다
        void ADTR0101_WORK_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());


                ds = biz.ADTR0101_WORK_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "0";
                pnlSearch.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }


                pnlSearch.JSProperties["cpTable"] = serializer.Serialize(rows);
            }
        }
        #endregion

        #region 실시간 측정 모니터링(FND)
        void MEASURE_WORK_LST(string[] oParams)
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", "01");
                oParamDic.Add("F_LINECD", "01");
                oParamDic.Add("F_CURDT", oParams[0]);
                oParamDic.Add("F_WORKCD", oParams[1]);
                ds = biz.MEASURE_WORK_LST(oParamDic, out errMsg);
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

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void pnlSearch_Callback(object sender, DevExpress.Web.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            ADTR0101_WORK_LST();
        }
        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 실시간 측정 모니터링(FND)
            MEASURE_WORK_LST(e.Parameters.Split('|'));
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.IndexOf("F_TIMEZONE") < 0 && !e.Column.FieldName.Length.Equals(12)) return;

            string value = e.Value.ToString();
            if (!String.IsNullOrEmpty(value))
            {
                //if (value.Equals("R"))
                //{
                //    int nDateDiff = UF.Date.DateDiff(DateTime.Today.ToString("yyyy-MM-dd"), GetFromDt());
                //    int nTimeZone = DateTime.Now.Hour;
                //    nTimeZone = (nTimeZone < 8) ? nTimeZone + 17 : nTimeZone - 7;
                //    if (nDateDiff <= 0 && nTimeZone <= int.Parse(e.Column.FieldName.Substring(10, 2)))
                //        e.DisplayText = "예";
                //    else
                //        e.DisplayText = "";
                //}
                //else
                e.DisplayText = "";
            }

        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if ((!e.DataColumn.FieldName.Equals("F_ITEMCD"))
                && (e.DataColumn.FieldName.IndexOf("F_TIMEZONE") < 0 && !e.DataColumn.FieldName.Length.Equals(12))) return;

            if (e.DataColumn.FieldName.Equals("F_ITEMCD") && e.CellValue.ToString().Length > 0)
            {
                string sWORKCD = devGrid.GetRowValues(e.VisibleIndex, "F_WORKCD").ToString();
                e.Cell.Attributes.Add("ondblclick", String.Format("fn_OnPopupMonitoringWork('{0}|{1}|{2}|{3}|{4}')", gsCOMPCD, gsFACTCD, GetFromDt(), GetToDt(), sWORKCD));
            }
            else
            {
                string value = e.CellValue.ToString();

                if (!String.IsNullOrEmpty(value))
                {
                    if (value.Equals("W"))
                    {
                        e.Cell.BackColor = System.Drawing.Color.Blue;
                        e.Cell.Attributes.Add("ondblclick", String.Format("fn_OnPopupList('{0}','{1}')", e.DataColumn.Caption, e.VisibleIndex));
                    }
                    else if (value.Equals("R"))
                    {
                        int nDateDiff = UF.Date.DateDiff(DateTime.Today.ToString("yyyy-MM-dd"), GetFromDt());
                        int nTimeZone = DateTime.Now.Hour;
                        nTimeZone = (nTimeZone < 8) ? nTimeZone + 17 : nTimeZone - 7;
                        if (nDateDiff <= 0 && nTimeZone <= int.Parse(e.DataColumn.FieldName.Substring(10, 2)))
                            e.Cell.BackColor = System.Drawing.Color.FromArgb(0x00, 0xCC, 0x00);
                        else
                            e.Cell.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        e.Cell.Attributes.Add("data-toggle", "tooltip");
                        e.Cell.Attributes.Add("title", value);
                        e.Cell.BackColor = System.Drawing.Color.FromArgb(0xC0, 0xC3, 0xB9);
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}