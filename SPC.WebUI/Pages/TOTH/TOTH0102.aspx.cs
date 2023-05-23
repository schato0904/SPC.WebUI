﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.TOTH.Biz;

namespace SPC.WebUI.Pages.TOTH
{
    public partial class TOTH0102 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet ds2 = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["TOTH0102_1"];
            }
            set
            {
                Session["TOTH0102_1"] = value;
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
            // Request
            GetRequest();
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

                //ucPager.TotalItems = 0;
                //ucPager.PagerDataBind();
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
            //AspxCombox_DataBind(this.ddlINSPCD, "AAC5");
            //AspxCombox_DataBind(this.ddlJUDGE, "AAE2");
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

        #region Data총갯수
        //Int32 QWK04A_ADTR0402_CNT()
        //{
        //    using (TOTHBiz biz = new TOTHBiz())
        //    {
        //        oParamDic.Clear();
        //        oParamDic.Add("F_COMPCD", GetCompCD());
        //        oParamDic.Add("F_FACTCD", GetFactCD());
        //        oParamDic.Add("F_FROMDT", GetFromDt());
        //        oParamDic.Add("F_TODT", GetToDt());
        //        oParamDic.Add("F_ITEMCD", GetItemCD());
        //        oParamDic.Add("F_WORKCD", GetWorkPOPCD());
        //        oParamDic.Add("F_BANCODE", GetBanCD());

        //        oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Text);
        //        //oParamDic.Add("F_MEANINSPCODE", GetInspItemCD());
        //        oParamDic.Add("F_CHK", this.rdoHIPISNG.SelectedItem.Value.ToString());

        //        //oParamDic.Add("F_INSPCD", (this.ddlINSPCD.Value ?? "").ToString());
        //        //oParamDic.Add("F_LOTNO", (this.txtLOTNO.Text ?? "").ToString());
        //        totalCnt = biz.QWK03A_TOTH0103_CNT(oParamDic);
        //    }

        //    return totalCnt;
        //}
        #endregion

        //void QWK04A_ADTR0402_MONTH_LST(int nPageSize, int nCurrPage, bool bCallback)
        //{
        //    string errMsg = String.Empty;

        //    using (TOTHBiz biz = new TOTHBiz())
        //    {
        //        oParamDic.Clear();
        //        oParamDic.Add("F_FRYM", GetFromDt());
        //        oParamDic.Add("F_TOYM", GetToDt());
        //        ds2 = biz.QWK03A_TOTH0103_MONTH_LST(oParamDic, out errMsg);
        //    }

        //    if (!String.IsNullOrEmpty(errMsg))
        //    {
        //        // Grid Callback Init
        //        devGrid.JSProperties["cpResultCode"] = "0";
        //        devGrid.JSProperties["cpResultMsg"] = errMsg;
        //    }
        //    else
        //    {
        //        if (!bCallback)
        //        {
        //            // Pager Setting
        //            ucPager.TotalItems = 0;
        //            ucPager.PagerDataBind();
        //        }
        //        else
        //        {
        //            devGrid.JSProperties["cpResultCode"] = "pager";
        //            devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0402_CNT());
        //        }
        //    }
        //}

        #region Data조회
        //void TOTH0102_LST(int nPageSize, int nCurrPage, bool bCallback)
        void TOTH0102_LST()
        {
            string errMsg = String.Empty;

            using (TOTHBiz biz = new TOTHBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("P_COMPCD", GetCompCD());
                oParamDic.Add("P_FACTCD", GetFactCD());
                oParamDic.Add("P_FROMDT", GetFromDt());
                oParamDic.Add("P_TODT", GetToDt());

                //oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                //oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.TOTH0102_1(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            dsGrid1 = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (!bCallback)
                //{
                //    // Pager Setting
                //    ucPager.TotalItems = 0;
                //    ucPager.PagerDataBind();
                //}
                //else
                //{
                //    devGrid.JSProperties["cpResultCode"] = "pager";
                //    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0402_CNT());
                //}
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
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //int nCurrPage = 0;
            //int nPageSize = 0;

            //if (!String.IsNullOrEmpty(e.Parameters))
            //{
            //    string[] oParams = new string[2];
            //    foreach (string oParam in e.Parameters.Split(';'))
            //    {
            //        oParams = oParam.Split('=');
            //        if (oParams[0].Equals("PAGESIZE"))
            //            nPageSize = Convert.ToInt32(oParams[1]);
            //        else if (oParams[0].Equals("CURRPAGE"))
            //            nCurrPage = Convert.ToInt32(oParams[1]);
            //    }
            //}
            //else
            //{
            //    nCurrPage = 1;
            //    nPageSize = ucPager.GetPageSize();
            //}
            this.TOTH0102_LST();
            //QWK04A_ADTR0402_LST(nPageSize, nCurrPage, true);
            devGrid.DataBind();
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                string color = e.GetValue("F_COLOR").ToString();
                if (!string.IsNullOrWhiteSpace(color))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                }
            }

            //string strJudge = e.GetValue("F_NGOKCHK").ToString();

            //if (strJudge == "1")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Red;
            //}
            //else if (strJudge == "2")
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Blue;
            //}
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
            //QWK04A_ADTR0103_LST();
            devGrid.DataSource = dsGrid1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 치형로그정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}