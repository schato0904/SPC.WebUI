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
    public partial class ADTR0401_FND : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        static int CurrPage = 0;
        protected string oSetParam = String.Empty;
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
            if (!Page.IsCallback || (!String.IsNullOrEmpty(hidGridAction.Text) && hidGridAction.Text.Equals("true")))
            {
                // 작업표준서 목록을 구한다
                QWK04A_ADTR0401_LST(ucPager.GetPageSize(), CurrPage, Page.IsCallback);
            }

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();
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
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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
            AspxCombox_DataBind(this.ddlJUDGE, "AAE2");
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
        Int32 QWK04A_ADTR0401_CNT()
        {
            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());                
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_JUDGE", (this.ddlJUDGE.Value ?? "").ToString());
                oParamDic.Add("F_INSPCD", "AAC501");
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Text ?? "");
                totalCnt = biz.QWK04A_ADTR0401_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region Data조회
        void QWK04A_ADTR0401_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_JUDGE", (this.ddlJUDGE.Value ?? "").ToString());
                oParamDic.Add("F_INSPCD", "AAC501");
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Text ?? "");
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.QWK04A_ADTR0401_LST(oParamDic, out errMsg);
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
                if (!bCallback)
                {
                    // Pager Setting
                    ucPager.TotalItems = 0;
                    ucPager.PagerDataBind();
                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK04A_ADTR0401_CNT());
                    devGrid.DataBind();
                }
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
            //e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
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
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_INSPDETAIL","F_ITEMCD"};

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
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
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string[] oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                }
            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager.GetPageSize();
            }

            CurrPage = nCurrPage;
            QWK04A_ADTR0401_LST(nPageSize, CurrPage, true);
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
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("F_NGOKCHK").ToString();

            if (strJudge == "1")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (strJudge == "2")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }
        #endregion

        #region devGrid_RowUpdating
        /// <summary>
        /// devGrid_RowUpdating
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataUpdatingEventArgs</param>
        protected void devGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", e.NewValues["F_ITEMCD"] == null ? "" : e.NewValues["F_ITEMCD"].ToString());
            oParamDic.Add("F_WORKCD", e.NewValues["F_WORKCD"] == null ? "" : e.NewValues["F_WORKCD"].ToString());
            oParamDic.Add("F_SERIALNO", e.NewValues["F_SERIALNO"] == null ? "" : e.NewValues["F_SERIALNO"].ToString());
            oParamDic.Add("F_MEASNO", e.NewValues["F_MEASNO"] == null ? "" : e.NewValues["F_MEASNO"].ToString());
            oParamDic.Add("F_NUMBER", e.NewValues["F_NUMBER"] == null ? "" : e.NewValues["F_NUMBER"].ToString());
            oParamDic.Add("F_TSERIALNO", e.NewValues["F_TSERIALNO"] == null ? "" : e.NewValues["F_TSERIALNO"].ToString());
            oParamDic.Add("F_MEASURE", e.NewValues["F_MODIFYMEASURE"] == null ? "" : e.NewValues["F_MODIFYMEASURE"].ToString());
            oParamDic.Add("F_MODIFYGB", DevExpressLib.devCheckBox(devGrid, "chkDELETE").Checked ? "D" : "");
            oParamDic.Add("F_NGOKCHK", e.NewValues["F_NGOKCHK"] == null ? "" : e.NewValues["F_NGOKCHK"].ToString());
            oParamDic.Add("F_MACHVALUE", e.NewValues["F_MACHVALUE"] == null ? "" : e.NewValues["F_MACHVALUE"].ToString());
            oParamDic.Add("F_CONTENTS", e.NewValues["F_CONTENTS"] == null ? "" : e.NewValues["F_CONTENTS"].ToString());
            oParamDic.Add("F_USERID", gsUSERID);
            oParamDic.Add("F_USERNM", gsUSERNM);
            
            bool bExecute = false;

            using (ADTRBiz biz = new ADTRBiz())
            {
                if (DevExpressLib.devCheckBox(devGrid, "chkDELETE").Checked)
                    bExecute = biz.QWK04A_ADTR0401_DEL(oParamDic);
                else
                    bExecute = biz.QWK04A_ADTR0401_UPD(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                devGrid.CancelEdit();
            }

            e.Cancel = true;

            if (true == bExecute)
            {
                QWK04A_ADTR0401_LST(ucPager.GetPageSize(), CurrPage, false);
            }
        }
        #endregion

        #region devGrid_RowDeleting
        /// <summary>
        /// devGrid_RowDeleting
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataDeletingEventArgs</param>
        protected void devGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", e.Values["F_ITEMCD"] == null ? "" : e.Values["F_ITEMCD"].ToString());
            oParamDic.Add("F_WORKCD", e.Values["F_WORKCD"] == null ? "" : e.Values["F_WORKCD"].ToString());
            oParamDic.Add("F_SERIALNO", e.Values["F_SERIALNO"] == null ? "" : e.Values["F_SERIALNO"].ToString());
            oParamDic.Add("F_MEASNO", e.Values["F_MEASNO"] == null ? "" : e.Values["F_MEASNO"].ToString());
            oParamDic.Add("F_NUMBER", e.Values["F_NUMBER"] == null ? "" : e.Values["F_NUMBER"].ToString());
            oParamDic.Add("F_TSERIALNO", e.Values["F_TSERIALNO"] == null ? "" : e.Values["F_TSERIALNO"].ToString());
            oParamDic.Add("F_MEASURE", e.Values["F_MEASURE"] == null ? "" : e.Values["F_MEASURE"].ToString());
            oParamDic.Add("F_MODIFYGB", "D");
            oParamDic.Add("F_NGOKCHK", e.Values["F_NGOKCHK"] == null ? "" : e.Values["F_NGOKCHK"].ToString());
            oParamDic.Add("F_MACHVALUE", e.Values["F_MACHVALUE"] == null ? "" : e.Values["F_MACHVALUE"].ToString());
            oParamDic.Add("F_CONTENTS", e.Values["F_CONTENTS"] == null ? "" : e.Values["F_CONTENTS"].ToString());
            oParamDic.Add("F_USERID", gsUSERID);
            bool bExecute = false;

            using (ADTRBiz biz = new ADTRBiz())
            {
                bExecute = biz.QWK04A_ADTR0401_DEL(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }

            e.Cancel = true;

            if (true == bExecute)
            {
                QWK04A_ADTR0401_LST(ucPager.GetPageSize(), CurrPage, false);
            }
        }
        #endregion

        #endregion
    }
}