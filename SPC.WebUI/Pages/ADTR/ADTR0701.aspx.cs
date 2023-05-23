using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ADTR.Biz;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0701 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
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
        {
        }
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
        {            
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region Data조회
        void ADTR0701_QWK03E_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                oParamDic.Add("F_GUBUN", (this.ddlGUBUN.Value ?? "").ToString());

                ds = biz.ADTR0701_QWK03E_LST(oParamDic, out errMsg);
            }

            

            DataTable dt = new DataTable();
            dt.Columns.Add("F_WORKDATE");
            dt.Columns.Add("F_BANCD");
            dt.Columns.Add("F_BANNM");
            dt.Columns.Add("F_LINECD");
            dt.Columns.Add("F_LINENM");
            dt.Columns.Add("F_ITEMCD");
            dt.Columns.Add("F_ITEMNM");
            dt.Columns.Add("F_DAILYCOUNT");
            dt.Columns.Add("F_DAILYNG");

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dt.Columns.Add(ds.Tables[1].Rows[i]["F_NGCODE"].ToString());
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["F_WORKDATE"] = ds.Tables[2].Rows[i]["F_WORKDATE"].ToString();
                dt.Rows[i]["F_BANCD"] = ds.Tables[2].Rows[i]["F_BANCD"].ToString();
                dt.Rows[i]["F_BANNM"] = ds.Tables[2].Rows[i]["F_BANNM"].ToString();
                dt.Rows[i]["F_LINECD"] = ds.Tables[2].Rows[i]["F_LINECD"].ToString();
                dt.Rows[i]["F_LINENM"] = ds.Tables[2].Rows[i]["F_LINENM"].ToString();
                dt.Rows[i]["F_ITEMCD"] = ds.Tables[2].Rows[i]["F_ITEMCD"].ToString();
                dt.Rows[i]["F_ITEMNM"] = ds.Tables[2].Rows[i]["F_ITEMNM"].ToString();
                dt.Rows[i]["F_DAILYCOUNT"] = ds.Tables[2].Rows[i]["F_DAILYCOUNT"].ToString();
                dt.Rows[i]["F_DAILYNG"] = ds.Tables[2].Rows[i]["F_DAILYNG"].ToString();
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (dt.Rows[i]["F_WORKDATE"].ToString() == ds.Tables[0].Rows[j]["F_WORKDATE"].ToString() && dt.Rows[i]["F_BANCD"].ToString() == ds.Tables[0].Rows[j]["F_BANCD"].ToString()
                        && dt.Rows[i]["F_LINECD"].ToString() == ds.Tables[0].Rows[j]["F_LINECD"].ToString() && dt.Rows[i]["F_ITEMCD"].ToString() == ds.Tables[0].Rows[j]["F_ITEMCD"].ToString())
                    {
                        for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
                        {
                            if (ds.Tables[1].Rows[x]["F_NGCODE"].ToString() == ds.Tables[0].Rows[j]["F_NGCODE"].ToString())
                            {
                                dt.Rows[i][ds.Tables[1].Rows[x]["F_NGCODE"].ToString()] = ds.Tables[0].Rows[j]["F_NGCOUNT"].ToString();
                            }                            
                        }
                    }
                }
            }

            DevExpress.Web.GridViewDataColumn column1 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_WORKDATE", Caption = "작업일자", Width = Unit.Parse("90") };
            DevExpress.Web.GridViewDataColumn column2 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_BANCD", Caption = "반코드", Width = Unit.Parse("70") };
            DevExpress.Web.GridViewDataColumn column3 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_BANNM", Caption = "반명", Width = Unit.Parse("150") };
            DevExpress.Web.GridViewDataColumn column4 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_LINECD", Caption = "라인코드", Width = Unit.Parse("70") };
            DevExpress.Web.GridViewDataColumn column5 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_LINENM", Caption = "라인명", Width = Unit.Parse("150") };
            DevExpress.Web.GridViewDataColumn column6 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_ITEMCD", Caption = "품목코드", Width = Unit.Parse("100") };
            DevExpress.Web.GridViewDataColumn column7 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_ITEMNM", Caption = "품목명", Width = Unit.Parse("150") };
            DevExpress.Web.GridViewDataColumn column8 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_DAILYCOUNT", Caption = "생산수", Width = Unit.Parse("100") };
            DevExpress.Web.GridViewDataColumn column9 = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_DAILYNG", Caption = "불량수", Width = Unit.Parse("100") };

            devGrid.Columns.Add(column1);
            devGrid.Columns.Add(column2);
            devGrid.Columns.Add(column3);
            devGrid.Columns.Add(column4);
            devGrid.Columns.Add(column5);
            devGrid.Columns.Add(column6);
            devGrid.Columns.Add(column7);
            devGrid.Columns.Add(column8);
            devGrid.Columns.Add(column9);

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = ds.Tables[1].Rows[i]["F_NGCODE"].ToString(), Caption = ds.Tables[1].Rows[i]["F_NGDETAIL"].ToString(), Width = Unit.Parse("100") };
                devGrid.Columns.Add(column);
            }

            devGrid.DataSource = dt;

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
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
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
            ADTR0701_QWK03E_LST();
            devGrid.DataBind();
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox, string CommonCode)
        {
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

        }
        #endregion
        

        #endregion
    }
}