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
    public partial class ADTR0404 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet ds1 = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        static int CurrPage = 0;
        protected string oSetParam = String.Empty;
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
            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                // 작업표준서 목록을 구한다
                QWK03A_ADTR0404_LST(ucPager.GetPageSize(), CurrPage, false);
            }

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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
            try
            {
                oSetParam = Request.QueryString.Get("oSetParam") ?? "";
            }
            catch (Exception)
            {
                oSetParam = "";
            }
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
            //AspxCombox_DataBind(this.ddlFIRSTITEM, "AAE3");
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
        Int32 QWK03A_ADTR0404_CNT()
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
                oParamDic.Add("F_LOTNO", (this.txtLOTNO.Text ?? "").ToString());
                totalCnt = biz.QWK03A_ADTR0404_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region Data조회
        void QWK03A_ADTR0404_LST(int nPageSize, int nCurrPage, bool bCallback)
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
                oParamDic.Add("F_LOTNO", (this.txtLOTNO.Text ?? "").ToString());
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.QWK03A_ADTR0404_LST(oParamDic, out errMsg);
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
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, QWK03A_ADTR0404_CNT());
                }
            }
        }
        #endregion

        #region 상세내용
        void QWK03A_ADTR0404_DETAIL_LST(string[] Param)
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", Param[0]);
                oParamDic.Add("F_FACTCD", Param[1]);
                oParamDic.Add("F_ITEMCD", Param[2]);
                oParamDic.Add("F_WORKCD", Param[3]);
                oParamDic.Add("F_TSERIALNO", Param[5]);
                oParamDic.Add("F_WORKTIME", Param[6]);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_UCLLCL", gsUCLLCL);

                ds1 = biz.QWK03A_ADTR0404_DETAIL_LST(oParamDic, out errMsg);
            }


            devGrid2.DataSource = ds1;




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
            string[] sTooltipFields = { "F_ITEMNM", "F_WORKNM", "F_INSPDETAIL", "F_ITEMCD" };

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
            QWK03A_ADTR0404_LST(nPageSize, CurrPage, true);
            devGrid.DataBind();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] key = e.Parameters.Split('|');
            QWK03A_ADTR0404_DETAIL_LST(key);
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
            oParamDic.Add("F_BANCD", (e.NewValues["F_BANCD"] ?? "").ToString());
            oParamDic.Add("F_LINECD", (e.NewValues["F_LINECD"] ?? "").ToString());
            oParamDic.Add("F_ITEMCD", (e.NewValues["F_ITEMCD"] ?? "").ToString());
            oParamDic.Add("F_WORKCD", (e.NewValues["F_WORKCD"] ?? "").ToString());
            oParamDic.Add("F_TSERIALNO", (e.NewValues["F_TSERIALNO"] ?? "").ToString());
            oParamDic.Add("F_WORKDATE", (e.NewValues["F_WORKDATE"] ?? "").ToString());
            oParamDic.Add("F_WORKTIME", (e.NewValues["F_WORKTIME"] ?? "").ToString());
            oParamDic.Add("F_LOTNO", (e.NewValues["F_LOTNO"] ?? "").ToString());
            oParamDic.Add("F_USERID", gsUSERID);
            oParamDic.Add("F_USERNM", gsUSERNM);



            bool bExecute = false;

            using (ADTRBiz biz = new ADTRBiz())
            {
                bExecute = biz.QWK04A_ADTR0404_UPD(oParamDic);
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
                QWK03A_ADTR0404_LST(ucPager.GetPageSize(), CurrPage, false);
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
            int erroridx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            bool bExists = false;
            string errorID = null;

            string reInsert = null;

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {

            }
            #endregion

            #region Batch Update

            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.Keys["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.Keys["F_LINECD"] ?? "").ToString());
                    oParamDic.Add("F_ITEMCD", (Value.Keys["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_WORKCD", (Value.Keys["F_WORKCD"] ?? "").ToString());
                    oParamDic.Add("F_TSERIALNO", (Value.NewValues["F_TSERIALNO"] ?? "").ToString());
                    oParamDic.Add("F_WORKDATE", (Value.NewValues["F_WORKDATE"] ?? "").ToString());
                    oParamDic.Add("F_WORKTIME", (Value.NewValues["F_WORKTIME"] ?? "").ToString());
                    oParamDic.Add("F_LOTNO", (Value.NewValues["F_LOTNO"] ?? "").ToString());


                    oSPs[idx] = "USP_ADTR0404_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            if (idx > 0)
            {
                using (ADTRBiz biz = new ADTRBiz())
                {
                    bExecute = biz.PROC_ADTR0404_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };

                    if (erroridx > 0)
                    {
                        procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다. 나머지는 저장이 완료되었습니다." };
                    }

                    // 사용자목록을 구한다
                    QWK03A_ADTR0404_LST(ucPager.GetPageSize(), CurrPage, false);
                }
            }
            else
            {
                procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다." };
                QWK03A_ADTR0404_LST(ucPager.GetPageSize(), CurrPage, false);
            }


            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            devGrid.JSProperties["cpResultreInert"] = reInsert;
            devGrid.JSProperties["cpResultcount"] = erroridx;
            #endregion

            e.Handled = true;
        }
        #endregion

        #region devGrid_DataBound
        /// <summary>
        /// devGrid_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void devGrid_DataBound(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion
    }
}