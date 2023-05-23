using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.INSP.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSP0301POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sTYPECD = String.Empty;
        string sREPORTTP = String.Empty;
        string sTYPENM = String.Empty;
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
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGridSub1.JSProperties["cpResultCode"] = "";
                devGridSub1.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devGridSub2.JSProperties["cpResultCode"] = "";
                devGridSub2.JSProperties["cpResultMsg"] = "";
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
            sTYPECD = Request.QueryString.Get("CODE");
            sREPORTTP = Request.QueryString.Get("TYPE");
            sTYPENM = Request.QueryString.Get("NAME");

            srcF_TYPECD.Text = sTYPECD;
            srcF_REPORTTP.Text = sREPORTTP;
            srcF_REPORTTPNM.Text = GetCommonCodeText(sREPORTTP);
            srcF_TYPENM.Text = sTYPENM;
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
        {
            rdoF_ASSORTMENT.TextField = String.Format("COMMNM{0}", gsLANGTP);
            rdoF_ASSORTMENT.ValueField = "COMMCD";
            rdoF_ASSORTMENT.DataSource = CachecommonCode["AA"]["AAI2"].codeGroup.Values;
            rdoF_ASSORTMENT.DataBind();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 항목분류 조회
        void QWK13A_LST(ASPxGridView gridView, string sDIVISIONIDX)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPECD", sTYPECD);
                oParamDic.Add("F_DIVISIONIDX", sDIVISIONIDX);

                ds = biz.QWK13A_LST(oParamDic, out errMsg);
            }

            gridView.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                gridView.JSProperties["cpResultCode"] = "0";
                gridView.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    gridView.DataBind();
            }
        }
        #endregion

        #region 항목분류 정보
        DataSet QWK13A_GET(string sTYPECD, string sDIVISIONIDX, out string errMsg)
        {
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPECD", sTYPECD);
                oParamDic.Add("F_DIVISIONIDX", sDIVISIONIDX);

                ds = biz.QWK13A_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목분류 저장
        bool QWK13A_INS_UPD(Dictionary<string, string> oParam, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.QWK13A_INS_UPD(oParam, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 항목분류 출력순서변경
        bool QWK13A_SRT(Dictionary<string, string> oParam, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.QWK13A_SRT(oParam, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 항목분류 검사기준 조회
        void QWK13B_LST(ASPxGridView gridView, string sDIVISIONIDX, string sINSPIDX)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPECD", sTYPECD);
                oParamDic.Add("F_DIVISIONIDX", sDIVISIONIDX);
                oParamDic.Add("F_INSPIDX", sINSPIDX);

                ds = biz.QWK13B_LST(oParamDic, out errMsg);
            }

            gridView.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                gridView.JSProperties["cpResultCode"] = "0";
                gridView.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    gridView.DataBind();
            }
        }
        #endregion

        #region 항목분류 검사기준 정보
        DataSet QWK13B_GET(string sTYPECD, string sDIVISIONIDX, string sINSPIDX, out string errMsg)
        {
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_TYPECD", sTYPECD);
                oParamDic.Add("F_DIVISIONIDX", sDIVISIONIDX);
                oParamDic.Add("F_INSPIDX", sINSPIDX);

                ds = biz.QWK13B_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목분류 검사기준 저장
        bool QWK13B_INS_UPD(Dictionary<string, string> oParam, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.QWK13B_INS_UPD(oParam, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 항목분류 출력순서변경
        bool QWK13B_SRT(Dictionary<string, string> oParam, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.QWK13B_SRT(oParam, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnSortDown_Init
        protected void btnSortDownA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid1.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('A', '-', '{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnSortUp_Init
        protected void btnSortUpA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid1.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('A', '+', '{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnAdd_Init
        protected void btnAddA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid1.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONNM") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnAddInspection('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnHistory_Init
        protected void btnHistoryA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            string v = devGrid1.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX").ToString();
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnViewHistory('A', '{0}', '{1}'); }}",
                v,
                rowVisibleIndex);
        }
        #endregion

        #region devGrid1_CustomCallback
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 항목분류 조회
            QWK13A_LST(sender as ASPxGridView, "0");
        }
        #endregion

        #region devGrid1_CustomColumnDisplayText
        protected void devGrid1_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (!e.Column.FieldName.Equals("F_STATUS") && !e.Column.FieldName.Equals("F_ASSORTMENT")) return;

            e.EncodeHtml = false;
            
            if (e.Column.FieldName.Equals("F_STATUS"))
            {
                e.DisplayText = !(bool)e.Value ? @"<span style='color:red;'>중단</span>" : @"<span style='color:blue;'>사용</span>";
            }
            else if (e.Column.FieldName.Equals("F_ASSORTMENT"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
        }
        #endregion

        #region devGridSub1_CustomCallback
        protected void devGridSub1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            // 항목분류 조회
            QWK13A_LST(sender as ASPxGridView, e.Parameters);
        }
        #endregion

        #region btnSortDownB_Init
        protected void btnSortDownB_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid2.GetRowValues(rowVisibleIndex, "F_INSPIDX", "F_INSPNO") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('B', '-', '{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnSortUpB_Init
        protected void btnSortUpB_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid2.GetRowValues(rowVisibleIndex, "F_INSPIDX", "F_INSPNO") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('B', '+', '{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnHistoryB_Init
        protected void btnHistoryB_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            string v = devGrid2.GetRowValues(rowVisibleIndex, "F_INSPIDX").ToString();
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnViewHistory('B', '{0}', '{1}'); }}",
                v,
                rowVisibleIndex);
        }
        #endregion

        #region devGrid2_CustomCallback
        protected void devGrid2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            // 항목분류 검사기준 조회
            QWK13B_LST(sender as ASPxGridView, e.Parameters, "0");
        }
        #endregion

        #region devGrid2_CustomColumnDisplayText
        protected void devGrid2_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion

        #region devGridSub2_CustomCallback
        protected void devGridSub2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameters.Split('|');
            // 항목분류 검사기준 조회
            QWK13B_LST(sender as ASPxGridView, oParams[0], oParams[1]);
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            bool bExecute = false;
            string errMsg = String.Empty;   // 오류 메시지
            Dictionary<string, object> oDataDic = null;
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameter);
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["TYPE"].ToString())
            {
                case "A":
                    switch (paramDic["ACTION"].ToString())
                    {
                        case "I":
                        case "U":
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD", gsCOMPCD);
                            oParamDic.Add("F_FACTCD", gsFACTCD);
                            oParamDic.Add("F_TYPECD", paramDic["F_TYPECD"].ToString());
                            oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                            oParamDic.Add("F_DIVISIONNM", paramDic["F_DIVISIONNM"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_METHOD", paramDic["F_METHOD"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_EQUIPMENT", paramDic["F_EQUIPMENT"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_ASSORTMENT", paramDic["F_ASSORTMENT"].ToString());
                            oParamDic.Add("F_DIVISIONSORT", paramDic["F_DIVISIONSORT"].ToString());
                            oParamDic.Add("F_REVSTATUS", paramDic["F_REVSTATUS"].ToString());
                            oParamDic.Add("F_DIVISIONREV", paramDic["F_DIVISIONREV"].ToString());
                            oParamDic.Add("F_REVSTDT", paramDic["F_REVSTDT"].ToString());
                            oParamDic.Add("F_STATUS", paramDic["F_STATUS"].ToString());
                            oParamDic.Add("F_USER", gsUSERID);
                            if (!QWK13A_INS_UPD(oParamDic, out errMsg))
                            {
                                ISOK = false;
                                result["ISOK"] = ISOK;
                                result["MSG"] = errMsg;
                            }
                            else
                            {
                                ISOK = true;
                                result["ISOK"] = ISOK;
                                result["MSG"] = "저장되었습니다.";
                                result["PKEY"] = PKEY;
                            }
                            e.Result = jss.Serialize(result);
                            break;
                        case "G":
                            Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                            ds = QWK13A_GET(paramDic["F_TYPECD"].ToString(), paramDic["F_DIVISIONIDX"].ToString(), out errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                ISOK = false;
                                msg = errMsg;
                            }
                            else if (!bExistsDataSet(ds))
                            {
                                ISOK = false;
                                msg = "조회된 데이터가 없습니다.";
                            }
                            else
                            {
                                ISOK = true;
                                msg = string.Empty;
                                DataRow dr = ds.Tables[0].Rows[0];
                                // 조회한 데이터를 Dictionary 형태로 변환
                                PAGEDATA = ds.Tables[0].Columns.Cast<DataColumn>()
                                    .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                            }
                            result["ISOK"] = ISOK;
                            result["MSG"] = msg;
                            result["PAGEDATA"] = PAGEDATA;
                            e.Result = jss.Serialize(result);
                            break;
                        case "S":
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD", gsCOMPCD);
                            oParamDic.Add("F_FACTCD", gsFACTCD);
                            oParamDic.Add("F_TYPECD", paramDic["F_TYPECD"].ToString());
                            oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                            oParamDic.Add("F_DIVISIONSORT", paramDic["F_DIVISIONSORT"].ToString());
                            oParamDic.Add("F_DIRECTION", paramDic["F_DIRECTION"].ToString());
                            oParamDic.Add("F_USER", gsUSERID);
                            if (!QWK13A_SRT(oParamDic, out errMsg))
                            {
                                ISOK = false;
                                result["ISOK"] = ISOK;
                                result["MSG"] = errMsg;
                            }
                            else
                            {
                                ISOK = true;
                                result["ISOK"] = ISOK;
                                result["MSG"] = "변경되었습니다.";
                                result["PKEY"] = PKEY;
                            }
                            e.Result = jss.Serialize(result);
                            break;
                    }
                    break;
                case "B":
                    switch (paramDic["ACTION"].ToString())
                    {
                        case "I":
                        case "U":
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD", gsCOMPCD);
                            oParamDic.Add("F_FACTCD", gsFACTCD);
                            oParamDic.Add("F_TYPECD", paramDic["F_TYPECD"].ToString());
                            oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                            oParamDic.Add("F_INSPIDX", paramDic["F_INSPIDX"].ToString());
                            oParamDic.Add("F_INSPNO", paramDic["F_INSPNO"].ToString());
                            oParamDic.Add("F_INSPNM", paramDic["F_INSPNM"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_STANDARD", paramDic["F_STANDARD"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_TERM", paramDic["F_TERM"].ToString().Replace("\\n", Environment.NewLine));
                            oParamDic.Add("F_TRANSPARENT", paramDic["F_TRANSPARENT"].ToString());
                            oParamDic.Add("F_ISEXCEPT", !Convert.ToBoolean(paramDic["F_ISEXCEPT"].ToString()) ? "0" : "1");
                            oParamDic.Add("F_INSPREVSTATUS", paramDic["F_INSPREVSTATUS"].ToString());
                            oParamDic.Add("F_INSPREV", paramDic["F_INSPREV"].ToString());
                            oParamDic.Add("F_REVSTDT", paramDic["F_REVSTDT"].ToString());
                            oParamDic.Add("F_STATUS", paramDic["F_STATUS"].ToString());
                            oParamDic.Add("F_USER", gsUSERID);
                            if (!QWK13B_INS_UPD(oParamDic, out errMsg))
                            {
                                ISOK = false;
                                result["ISOK"] = ISOK;
                                result["MSG"] = errMsg;
                            }
                            else
                            {
                                ISOK = true;
                                result["ISOK"] = ISOK;
                                result["MSG"] = "저장되었습니다.";
                                result["PKEY"] = PKEY;
                            }
                            e.Result = jss.Serialize(result);
                            break;
                        case "G":
                            Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                            ds = QWK13B_GET(paramDic["F_TYPECD"].ToString(), paramDic["F_DIVISIONIDX"].ToString(), paramDic["F_INSPIDX"].ToString(), out errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                ISOK = false;
                                msg = errMsg;
                            }
                            else if (!bExistsDataSet(ds))
                            {
                                ISOK = false;
                                msg = "조회된 데이터가 없습니다.";
                            }
                            else
                            {
                                ISOK = true;
                                msg = string.Empty;
                                DataRow dr = ds.Tables[0].Rows[0];
                                // 조회한 데이터를 Dictionary 형태로 변환
                                PAGEDATA = ds.Tables[0].Columns.Cast<DataColumn>()
                                    .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                            }
                            result["ISOK"] = ISOK;
                            result["MSG"] = msg;
                            result["PAGEDATA"] = PAGEDATA;
                            e.Result = jss.Serialize(result);
                            break;
                        case "S":
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD", gsCOMPCD);
                            oParamDic.Add("F_FACTCD", gsFACTCD);
                            oParamDic.Add("F_TYPECD", paramDic["F_TYPECD"].ToString());
                            oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                            oParamDic.Add("F_INSPIDX", paramDic["F_INSPIDX"].ToString());
                            oParamDic.Add("F_INSPNO", paramDic["F_INSPNO"].ToString());
                            oParamDic.Add("F_DIRECTION", paramDic["F_DIRECTION"].ToString());
                            oParamDic.Add("F_USER", gsUSERID);
                            if (!QWK13B_SRT(oParamDic, out errMsg))
                            {
                                ISOK = false;
                                result["ISOK"] = ISOK;
                                result["MSG"] = errMsg;
                            }
                            else
                            {
                                ISOK = true;
                                result["ISOK"] = ISOK;
                                result["MSG"] = "변경되었습니다.";
                                result["PKEY"] = PKEY;
                            }
                            e.Result = jss.Serialize(result);
                            break;
                    }
                    break;
            }
        }
        #endregion

        #endregion
    }
}