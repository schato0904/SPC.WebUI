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
    public partial class ADTR0104FND : WebUIBasePage
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

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
            new ASPxGridViewCellMerger(devGrid, "F_WORKCD|F_WORKNM");
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

        #region 실시간 측정 모니터링(FND)
        void MEASURE_WORK_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", "01");
                oParamDic.Add("F_LINECD", "01");
                oParamDic.Add("F_CURDT", GetFromDt());
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

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 실시간 측정 모니터링(FND)
            MEASURE_WORK_LST();
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
            if (e.DataColumn.FieldName.IndexOf("F_TIMEZONE") < 0 && !e.DataColumn.FieldName.Length.Equals(12)) return;

            //if (!e.DataColumn.FieldName.Equals("F_TIMEZONE05"))
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