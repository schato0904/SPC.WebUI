using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0303NR_MULTI : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        int nCurrPage = 0;
        int nPageSize = 0;
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

                ucPager.PagerDataBind();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
            }

            // Request
            GetRequest();

            nPageSize = int.Parse(hdnPageSize.Text);
            nCurrPage = int.Parse(hdnCurrPage.Text);

            if ((String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false")))
            {
                // 검사기준목록을 구한다
                GetQCD34_LST(nPageSize > 0 ? nPageSize : ucPager.GetPageSize(), nCurrPage > 0 ? nCurrPage : 1, false);
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

        #region 검사기준 전체 갯수를 구한다
        Int32 GetQCD34_CNT()
        {
            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_INSPCD", GetInspectionCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());
                totalCnt = biz.GetQCD34_BATCH_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 검사기준목록을 구한다
        void GetQCD34_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_INSPCD", GetInspectionCD());
                oParamDic.Add("F_WORKCD", GetWorkCD());
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.GetQCD34_BATCH_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            if (bCallback)
                devGrid.DataBind();

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
                    //ucPager.TotalItems = GetQCD34_CNT();
                    //ucPager.PagerDataBind();
                }
                else
                {
                    devGrid.JSProperties["cpResultCode"] = "pager";
                    devGrid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, GetQCD34_CNT());
                }
            }
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(object comboBox, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
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
            e.NewValues["F_ZERO"] = "0";
            e.NewValues["F_MEASYESNO"] = "0";
            e.NewValues["F_IMPORT"] = "0";
            e.NewValues["F_RESULTGUBUN"] = "0";
            e.NewValues["F_GETTYPE"] = "0";
            e.NewValues["F_SAMPLECHK"] = "0";
            e.NewValues["F_ACCEPT_SEQ"] = "AAC202";
            e.NewValues["F_SIRYO"] = "1";
            e.NewValues["F_RANK"] = "A";
            e.NewValues["F_DEFECTS_N"] = "1";
        }
        #endregion

        #region devGrid HtmlEditFormCreated
        /// <summary>
        /// devGrid_HtmlEditFormCreated
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_INSPCD") && !e.Column.FieldName.Equals("F_QCYCLECD") && !e.Column.FieldName.Equals("F_JCYCLECD")
                && !e.Column.FieldName.Equals("F_RANK") && !e.Column.FieldName.Equals("F_SINGLECHK") && !e.Column.FieldName.Equals("F_UNIT")
                && !e.Column.FieldName.Equals("F_ACCEPT_SEQ") && !e.Column.FieldName.Equals("F_AIRCK") && !e.Column.FieldName.Equals("F_PORT")
                && !e.Column.FieldName.Equals("F_GETDATA") && !e.Column.FieldName.Equals("F_LOADTF")
                && !e.Column.FieldName.Equals("F_ITEMCD") && !e.Column.FieldName.Equals("F_MEAINSPCD") && !e.Column.FieldName.Equals("F_WORKCD")) return;

            string groupCode = String.Empty;

            switch (e.Column.FieldName)
            {
                default:
                    groupCode = String.Empty;
                    break;
                case "F_INSPCD":        // 검사분류
                    groupCode = "AAC5";
                    break;
                case "F_QCYCLECD":      // QC검사주기
                case "F_JCYCLECD":      // 현장검사주기
                    groupCode = "AAC3";
                    break;
                case "F_RANK":          // 품질수준
                    groupCode = "AAD2";
                    break;
                case "F_SINGLECHK":     // 상,하한편측
                    groupCode = "AAD7";
                    break;
                case "F_UNIT":          // 공차기호
                    groupCode = "AAC1";
                    break;
                case "F_ACCEPT_SEQ":    // 관리한계기준
                    groupCode = "AAC2";
                    break;
                case "F_AIRCK":         // 계측기
                    groupCode = "AAD8";
                    break;
                case "F_PORT":          // 측정포트
                    groupCode = "AAD4";
                    break;
                case "F_GETDATA":       // 측정방법
                    groupCode = "AAD5";
                    break;
                case "F_LOADTF":// 설비구분
                    groupCode = "AAD6";
                    break;
            }

            if (!String.IsNullOrEmpty(groupCode))
                AspxCombox_DataBind(e.Editor, groupCode);
            else
            {
                DevExpress.Web.ASPxTextBox txtBox = e.Editor as DevExpress.Web.ASPxTextBox;
                if (e.Column.FieldName.Equals("F_ITEMCD"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupItemSearch('INS')");
                else if (e.Column.FieldName.Equals("F_MEAINSPCD"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupMeainspSearchForm()");
                else if (e.Column.FieldName.Equals("F_WORKCD"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupWorkSearchForm()");
            }
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

            //hidPageSize.Text = nPageSize.ToString();
            //hidCurrPage.Text = nCurrPage.ToString();

            // 검사기준목록을 구한다
            GetQCD34_LST(nPageSize, nCurrPage, true);
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
            string[] sTooltipFields = { "F_ITEMNM", "F_INSPDETAIL", "F_WORKNM", "F_MEASURE", "F_AIRCK" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                    {
                        e.Cell.ToolTip = e.CellValue.ToString();
                        return;
                    }
                }
            }
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

            // 검사분류, QC검사주기, 현장검사주기
            // 품질수준, 편측, 공차기호
            // 관리한계기준, 계측기, 측정포트
            // 측정방법, 설비구분
            if ((e.Column.FieldName.Equals("F_INSPCD") || e.Column.FieldName.Equals("F_QCYCLECD") || e.Column.FieldName.Equals("F_JCYCLECD")
                || e.Column.FieldName.Equals("F_RANK") || e.Column.FieldName.Equals("F_SINGLECHK") || e.Column.FieldName.Equals("F_UNIT")
                || e.Column.FieldName.Equals("F_ACCEPT_SEQ") || e.Column.FieldName.Equals("F_AIRCK") || e.Column.FieldName.Equals("F_PORT")
                || e.Column.FieldName.Equals("F_GETDATA") || e.Column.FieldName.Equals("F_LOADTF"))
                && !String.IsNullOrEmpty(e.Value.ToString()))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
                return;
            }

            // Zero Setting, 측정제외, 중요항목, 성적서출력, 수작업, 초중종관리
            if (e.Column.FieldName.Equals("F_ZERO") || e.Column.FieldName.Equals("F_MEASYESNO")
                || e.Column.FieldName.Equals("F_IMPORT") || e.Column.FieldName.Equals("F_RESULTGUBUN")
                 || e.Column.FieldName.Equals("F_GETTYPE") || e.Column.FieldName.Equals("F_SAMPLECHK"))
            {
                e.DisplayText = e.Value.ToString().Equals("1") ? "Yes" : "No";
                return;
            }
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
            string errMsg = String.Empty;
            string[] oParams = e.Parameter.Split('|');
            bool bExecute = false;

            devCallback.JSProperties["cpBANCD"] = "";
            devCallback.JSProperties["cpLINECD"] = "";
            devCallback.JSProperties["cpMODEL"] = "";
            devCallback.JSProperties["cpMODELNM"] = "";

            switch (oParams[0])
            {
                case "ITEM":  // 품목
                    devCallback.JSProperties["cpIDCD"] = "ITEMCD";
                    devCallback.JSProperties["cpIDNM"] = "ITEMNM";
                    devCallback.JSProperties["cpMODELNM"] = "MODELNM";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_ITEMCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD01_POPUP_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_ITEMCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_ITEMNM"].ToString();
                        devCallback.JSProperties["cpMODEL"] = ds.Tables[0].Rows[0]["F_MODELNM"].ToString();

                        bExecute = true;
                    }
                    break;
                case "MEAINSP":  // 검상항목
                    devCallback.JSProperties["cpIDCD"] = "MEAINSPCD";
                    devCallback.JSProperties["cpIDNM"] = "INSPDETAIL";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_MEAINSPCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD33_POPUP_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count.Equals(1))
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_MEAINSPCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_INSPDETAIL"].ToString();

                        bExecute = true;
                    }
                    break;
                case "WORK":      // 공정
                    devCallback.JSProperties["cpIDCD"] = "WORKCD";
                    devCallback.JSProperties["cpIDNM"] = "WORKNM";

                    using (CommonBiz biz = new CommonBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_WORKCD", oParams[1]);
                        oParamDic.Add("F_STATUS", "1");
                        ds = biz.GetQCD74_LST(oParamDic, out errMsg);
                    }

                    if (ds.Tables[0].Rows.Count.Equals(1))
                    {
                        devCallback.JSProperties["cpCODE"] = ds.Tables[0].Rows[0]["F_WORKCD"].ToString();
                        devCallback.JSProperties["cpTEXT"] = ds.Tables[0].Rows[0]["F_WORKNM"].ToString();
                        devCallback.JSProperties["cpBANCD"] = ds.Tables[0].Rows[0]["F_BANCD"].ToString();
                        devCallback.JSProperties["cpLINECD"] = ds.Tables[0].Rows[0]["F_LINECD"].ToString();

                        bExecute = true;
                    }
                    break;
                case "EXCEL":
                    devCallback.JSProperties["cpIDCD"] = "";
                    devCallback.JSProperties["cpIDNM"] = "";

                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(String.Format("./Export/{0}EXPORT.aspx?pBANCD={1}&pITEMCD={2}&pWORKCD={3}&pINSPCD={4}",
                        oParams[1],
                        GetBanCD(),
                        GetItemCD(),
                        GetWorkCD(),
                        GetInspectionCD()));
                    break;
            }

            if (!bExecute)
            {
                devCallback.JSProperties["cpCODE"] = "";
                devCallback.JSProperties["cpTEXT"] = "";
            }
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
            int count = e.InsertValues.Count + e.UpdateValues.Count + (e.DeleteValues.Count * 4);

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>(); oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.NewValues["F_LINECD"] ?? "").ToString());
                    oParamDic.Add("F_ITEMCD", (Value.NewValues["F_ITEMCD"] ?? "").ToString());
                    oParamDic.Add("F_INSPCD", (Value.NewValues["F_INSPCD"] ?? "").ToString());
                    oParamDic.Add("F_STANDARD", (Value.NewValues["F_STANDARD"] ?? "").ToString());
                    oParamDic.Add("F_RANK", (Value.NewValues["F_RANK"] ?? "").ToString());
                    oParamDic.Add("F_MIN", (Value.NewValues["F_MIN"] ?? "").ToString());
                    oParamDic.Add("F_MAX", (Value.NewValues["F_MAX"] ?? "").ToString());
                    oParamDic.Add("F_MEAINSPCD", (Value.NewValues["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_WORKCD", (Value.NewValues["F_WORKCD"] ?? "").ToString());
                    oParamDic.Add("F_MACHCD", "");    // 사용안함
                    oParamDic.Add("F_UCLX", (Value.NewValues["F_UCLX"] ?? "").ToString());
                    oParamDic.Add("F_LCLX", (Value.NewValues["F_LCLX"] ?? "").ToString());
                    oParamDic.Add("F_UCLR", (Value.NewValues["F_UCLR"] ?? "").ToString());
                    oParamDic.Add("F_LCLR", "");
                    oParamDic.Add("F_SIRYO", (Value.NewValues["F_SIRYO"].ToString() ?? "").ToString());
                    oParamDic.Add("F_ZERO", (Value.NewValues["F_ZERO"] ?? "").ToString());
                    oParamDic.Add("F_UNIT", (Value.NewValues["F_UNIT"] ?? "").ToString());
                    oParamDic.Add("F_ACCEPT_SEQ", (Value.NewValues["F_ACCEPT"] ?? "").ToString());
                    oParamDic.Add("F_GETDATA", (Value.NewValues["F_GETDATA"] ?? "").ToString());
                    //oParamDic.Add("F_GETCNT", "");    // 사용안함
                    oParamDic.Add("F_PORT", (Value.NewValues["F_PORT"] ?? "").ToString());
                    oParamDic.Add("F_CHANNEL", (Value.NewValues["F_CHANNEL"] ?? "").ToString());
                    oParamDic.Add("F_GETTIME", "0");    // 사용안함
                    oParamDic.Add("F_GETTYPE", (Value.NewValues["F_GETTYPE"] ?? "").ToString());
                    oParamDic.Add("F_ZIG", (Value.NewValues["F_ZIG"] ?? "").ToString());
                    oParamDic.Add("F_DATAUNIT", "0");
                    //oParamDic.Add("F_SINGLE", "");    // 사용안함
                    //oParamDic.Add("F_HOMSU", "");    // 사용안함
                    oParamDic.Add("F_DEFECTS_N", (Value.NewValues["F_DEFECTS_N"] ?? "").ToString());
                    //oParamDic.Add("F_DISPLAYNO", e.NewValues["F_DISPLAYNO"] == null ? "" : e.NewValues["F_DISPLAYNO"].ToString());
                    //oParamDic.Add("F_PRINT", "");    // 사용안함
                    oParamDic.Add("F_AIRCK", (Value.NewValues["F_AIRCK"] ?? "").ToString());
                    //oParamDic.Add("F_DANGA", "");    // 사용안함
                    oParamDic.Add("F_LOADTF", (Value.NewValues["F_LOADTF"] ?? "").ToString());
                    //oParamDic.Add("F_BUHO", "");    // 사용안함
                    oParamDic.Add("F_MEASYESNO", (Value.NewValues["F_MEASYESNO"] ?? "").ToString());
                    oParamDic.Add("F_SAMPLECHK", (Value.NewValues["F_SAMPLECHK"] ?? "").ToString());    // 사용안함
                    //oParamDic.Add("F_SAMPLENO", (Value.NewValues["F_SAMPLENO"] ?? "").ToString());
                    oParamDic.Add("F_RESULTGUBUN", (Value.NewValues["F_RESULTGUBUN"] ?? "").ToString());
                    oParamDic.Add("F_IMPORT", (Value.NewValues["F_IMPORT"] ?? "").ToString());
                    oParamDic.Add("F_IMAGESEQ", (Value.NewValues["F_IMAGESEQ"] ?? "").ToString());
                    //oParamDic.Add("F_POINTX", "");    // 사용안함
                    //oParamDic.Add("F_POINTY", "");    // 사용안함
                    oParamDic.Add("F_JCYCLECD", (Value.NewValues["F_JCYCLECD"] ?? "").ToString());
                    oParamDic.Add("F_QCYCLECD", (Value.NewValues["F_QCYCLECD"] ?? "").ToString());
                    oParamDic.Add("F_HCOUNT", (Value.NewValues["F_HCOUNT"] ?? "").ToString());
                    oParamDic.Add("F_SINGLECHK", (Value.NewValues["F_SINGLECHK"] ?? "").ToString());
                    oParamDic.Add("F_MEASCD1", (Value.NewValues["F_MEASCD1"] ?? "").ToString());
                    oParamDic.Add("F_MEASURE", (Value.NewValues["F_MEASURE"] ?? "").ToString());
                    oParamDic.Add("F_RESULTSTAND", (Value.NewValues["F_RESULTSTAND"] ?? "").ToString());
                    oParamDic.Add("F_TMAX", (Value.NewValues["F_TMAX"] ?? "").ToString());
                    oParamDic.Add("F_TMIN", (Value.NewValues["F_TMIN"] ?? "").ToString());
                    oParamDic.Add("F_HIPISNG", "0");
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD34_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
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
                    oParamDic.Add("F_BANCD", Value.NewValues["F_BANCD"] == null ? "" : Value.NewValues["F_BANCD"].ToString());
                    oParamDic.Add("F_LINECD", Value.NewValues["F_LINECD"] == null ? "" : Value.NewValues["F_LINECD"].ToString());
                    oParamDic.Add("F_ITEMCD", Value.NewValues["F_ITEMCD"] == null ? "" : Value.NewValues["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", Value.NewValues["F_INSPCD"] == null ? "" : Value.NewValues["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", Value.NewValues["F_SERIALNO"] == null ? "" : Value.NewValues["F_SERIALNO"].ToString());
                    oParamDic.Add("F_STANDARD", Value.NewValues["F_STANDARD"] == null ? "" : Value.NewValues["F_STANDARD"].ToString());
                    oParamDic.Add("F_RANK", Value.NewValues["F_RANK"] == null ? "" : Value.NewValues["F_RANK"].ToString());
                    oParamDic.Add("F_MIN", Value.NewValues["F_MIN"] == null ? "" : Value.NewValues["F_MIN"].ToString());
                    oParamDic.Add("F_MAX", Value.NewValues["F_MAX"] == null ? "" : Value.NewValues["F_MAX"].ToString());
                    oParamDic.Add("F_MEAINSPCD", Value.NewValues["F_MEAINSPCD"] == null ? "" : Value.NewValues["F_MEAINSPCD"].ToString());
                    oParamDic.Add("F_WORKCD", Value.NewValues["F_WORKCD"] == null ? "" : Value.NewValues["F_WORKCD"].ToString());
                    oParamDic.Add("F_MACHCD", "");    // 사용안함
                    oParamDic.Add("F_UCLX", Value.NewValues["F_UCLX"] == null ? "" : Value.NewValues["F_UCLX"].ToString());
                    oParamDic.Add("F_LCLX", Value.NewValues["F_LCLX"] == null ? "" : Value.NewValues["F_LCLX"].ToString());
                    oParamDic.Add("F_UCLR", Value.NewValues["F_UCLR"] == null ? "" : Value.NewValues["F_UCLR"].ToString());
                    oParamDic.Add("F_LCLR", Value.NewValues["F_LCLR"] == null ? "" : Value.NewValues["F_LCLR"].ToString());
                    oParamDic.Add("F_SIRYO", Value.NewValues["F_SIRYO"] == null ? "" : Value.NewValues["F_SIRYO"].ToString());
                    oParamDic.Add("F_ZERO", Value.NewValues["F_ZERO"] == null ? "" : Value.NewValues["F_ZERO"].ToString());
                    oParamDic.Add("F_UNIT", Value.NewValues["F_UNIT"] == null ? "" : Value.NewValues["F_UNIT"].ToString());
                    oParamDic.Add("F_ACCEPT_SEQ", Value.NewValues["F_ACCEPT_SEQ"] == null ? "" : Value.NewValues["F_ACCEPT_SEQ"].ToString());
                    oParamDic.Add("F_GETDATA", Value.NewValues["F_GETDATA"] == null ? "" : Value.NewValues["F_GETDATA"].ToString());
                    //oParamDic.Add("F_GETCNT", "");    // 사용안함
                    oParamDic.Add("F_PORT", Value.NewValues["F_PORT"] == null ? "" : Value.NewValues["F_PORT"].ToString());
                    oParamDic.Add("F_CHANNEL", Value.NewValues["F_CHANNEL"] == null ? "" : Value.NewValues["F_CHANNEL"].ToString());
                    oParamDic.Add("F_GETTIME", "0");    // 사용안함
                    oParamDic.Add("F_GETTYPE", Value.NewValues["F_GETTYPE"] == null ? "" : Value.NewValues["F_GETTYPE"].ToString());
                    oParamDic.Add("F_ZIG", Value.NewValues["F_ZIG"] == null ? "" : Value.NewValues["F_ZIG"].ToString());
                    oParamDic.Add("F_DATAUNIT", Value.NewValues["F_DATAUNIT"] == null ? "" : Value.NewValues["F_DATAUNIT"].ToString());
                    //oParamDic.Add("F_SINGLE", "");    // 사용안함
                    //oParamDic.Add("F_HOMSU", "");    // 사용안함
                    oParamDic.Add("F_DEFECTS_N", Value.NewValues["F_DEFECTS_N"] == null ? "" : Value.NewValues["F_DEFECTS_N"].ToString());
                    oParamDic.Add("F_DISPLAYNO", Value.NewValues["F_DISPLAYNO"] == null ? "" : Value.NewValues["F_DISPLAYNO"].ToString());
                    //oParamDic.Add("F_PRINT", "");    // 사용안함
                    oParamDic.Add("F_AIRCK", Value.NewValues["F_AIRCK"] == null ? "" : Value.NewValues["F_AIRCK"].ToString());
                    //oParamDic.Add("F_DANGA", "");    // 사용안함
                    oParamDic.Add("F_LOADTF", Value.NewValues["F_LOADTF"] == null ? "" : Value.NewValues["F_LOADTF"].ToString());
                    //oParamDic.Add("F_BUHO", "");    // 사용안함
                    oParamDic.Add("F_MEASYESNO", Value.NewValues["F_MEASYESNO"] == null ? "" : Value.NewValues["F_MEASYESNO"].ToString());
                    oParamDic.Add("F_SAMPLECHK", Value.NewValues["F_SAMPLECHK"] == null ? "" : Value.NewValues["F_SAMPLECHK"].ToString());
                    oParamDic.Add("F_SAMPLENO", Value.NewValues["F_SAMPLENO"] == null ? "" : Value.NewValues["F_SAMPLENO"].ToString());
                    oParamDic.Add("F_RESULTGUBUN", Value.NewValues["F_RESULTGUBUN"] == null ? "" : Value.NewValues["F_RESULTGUBUN"].ToString());
                    oParamDic.Add("F_IMPORT", Value.NewValues["F_IMPORT"] == null ? "" : Value.NewValues["F_IMPORT"].ToString());
                    oParamDic.Add("F_IMAGESEQ", Value.NewValues["F_IMAGESEQ"] == null ? "" : Value.NewValues["F_IMAGESEQ"].ToString());
                    //oParamDic.Add("F_POINTX", "");    // 사용안함
                    //oParamDic.Add("F_POINTY", "");    // 사용안함
                    oParamDic.Add("F_JCYCLECD", Value.NewValues["F_JCYCLECD"] == null ? "" : Value.NewValues["F_JCYCLECD"].ToString());
                    oParamDic.Add("F_QCYCLECD", Value.NewValues["F_QCYCLECD"] == null ? "" : Value.NewValues["F_QCYCLECD"].ToString());
                    oParamDic.Add("F_HCOUNT", Value.NewValues["F_HCOUNT"] == null ? "" : Value.NewValues["F_HCOUNT"].ToString());
                    oParamDic.Add("F_SINGLECHK", Value.NewValues["F_SINGLECHK"] == null ? "" : Value.NewValues["F_SINGLECHK"].ToString());
                    oParamDic.Add("F_MEASCD1", Value.NewValues["F_MEASCD1"] == null ? "" : Value.NewValues["F_MEASCD1"].ToString());
                    oParamDic.Add("F_MEASURE", Value.NewValues["F_MEASURE"] == null ? "" : Value.NewValues["F_MEASURE"].ToString());
                    oParamDic.Add("F_RESULTSTAND", Value.NewValues["F_RESULTSTAND"] == null ? "" : Value.NewValues["F_RESULTSTAND"].ToString());
                    oParamDic.Add("F_TMAX", Value.NewValues["F_TMAX"] == null ? "" : Value.NewValues["F_TMAX"].ToString());
                    oParamDic.Add("F_TMIN", Value.NewValues["F_TMIN"] == null ? "" : Value.NewValues["F_TMIN"].ToString());
                    oParamDic.Add("F_HIPISNG", Value.NewValues["F_HIPISNG"] == null ? "" : Value.NewValues["F_HIPISNG"].ToString());

                    oSPs[idx] = "USP_QCD34_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", Value.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", Value.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", Value.Values["F_SERIALNO"].ToString());

                    oSPs[idx] = "USP_QCD34_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", Value.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", Value.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", Value.Values["F_SERIALNO"].ToString());

                    oSPs[idx] = "USP_QCD34B_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", Value.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INSPCD", Value.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", Value.Values["F_SERIALNO"].ToString());
                    oParamDic.Add("F_KEMARK", String.Format("Data Deleted by {0}", gsUSERNM));
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD34A_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", Value.Values["F_ITEMCD"].ToString());
                    oParamDic.Add("F_WORKCD", Value.Values["F_WORKCD"].ToString());
                    oParamDic.Add("F_INSPCD", Value.Values["F_INSPCD"].ToString());
                    oParamDic.Add("F_SERIALNO", Value.Values["F_SERIALNO"].ToString());
                    oParamDic.Add("F_KEMARK", String.Format("Data Deleted by {0}", gsUSERNM));
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QWK03A_DEL";
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
                using (BSIFBiz biz = new BSIFBiz())
                {
                    bExecute = biz.PROC_QCD34_BATCH(oSPs, oParameters, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };

                    GetQCD34_LST(ucPager.GetPageSize(), ucPager.GetCurrPage(), false);
                }
            }


            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
        }
        #endregion

        #endregion
    }
}