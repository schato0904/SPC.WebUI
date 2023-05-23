using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.INSP.Biz;
using System.Collections;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSP0303POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Dictionary<string, string> PopParam = new Dictionary<string, string>();
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

            new ASPxGridViewCellMerger(devGrid1, "F_TSERIALNO");
            new ASPxGridViewCellMerger(devGrid2, "F_WORKNM|F_WORKDATE|F_TSERIALNO");

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
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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
            PopParam = DeserializeJSON(Request.QueryString.Get("PopParam"));

            hidQWK13IMID.Text = GetDicValue(PopParam, "F_QWK13IMID");
            hidREPORTTP.Text = GetDicValue(PopParam, "F_REPORTTP");
            hidITEMCD.Text = GetDicValue(PopParam, "F_ITEMCD");
            hidWORKCD.Text = GetDicValue(PopParam, "F_WORKCD");
            hidWORKDATE.Text = GetDicValue(PopParam, "F_WORKDATE");
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
            string sToday = DateTime.Today.ToString("yyyy-MM-dd");
            srcF_INDATE.Text = sToday;
            srcF_ESTDATE.Text = sToday;
            srcF_INJDATE.Text = sToday;
            srcF_MAKEDATE.Text = sToday;

            srcF_REPORTTP.TextField = String.Format("COMMNM{0}", gsLANGTP);
            srcF_REPORTTP.ValueField = "COMMCD";
            srcF_REPORTTP.DataSource = CachecommonCode["AA"]["AAI1"].codeGroup.Values;
            srcF_REPORTTP.DataBind();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 마스터 정보(팝업)
        DataSet QWK13IM_GET(Dictionary<string, object> paramDic, out string errMsg)
        {
            if (ds != null) ds.Clear();
            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13IMID", GetDicValue(paramDic, "F_QWK13IMID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_WORKDATE", GetDicValue(paramDic, "F_WORKDATE"));

                ds = biz.QWK13IM_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상세목록
        void QWK13ID_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13IMID", GetDicValue(paramDic, "F_QWK13IMID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_WORKDATE", GetDicValue(paramDic, "F_WORKDATE"));


                ds = biz.QWK13ID_LST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #region 선택한 중간검사(EF) 샘플검사목록
        void QWK03A_INSP0302_SUB_GET_LST(List<object> oList, out string errMsg)
        {
            int i = 0;
            Dictionary<string, object> oJsonDic = null;
            DataTable dtTemp = new DataTable();
            errMsg = String.Empty;

            foreach (object oJsonDataList in oList)
            {
                oJsonDic = (Dictionary<string, object>)oJsonDataList;

                using (INSPBiz biz = new INSPBiz())
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_ITEMCD", GetDicValue(oJsonDic, "F_ITEMCD"));
                    oParamDic.Add("F_WORKCD", GetDicValue(oJsonDic, "F_WORKCD"));
                    oParamDic.Add("F_WORKDATE", GetDicValue(oJsonDic, "F_WORKDATE"));
                    oParamDic.Add("F_TSERIALNO", GetDicValue(oJsonDic, "F_TSERIALNO"));

                    ds = biz.QWK03A_INSP0302_SUB_GET_LST(oParamDic, out errMsg);
                }

                if (!bExistsDataSet(ds))
                {
                    errMsg = "조회된 데이터가 없습니다";
                }
                else
                {
                    if (i.Equals(0))
                    {
                        dtTemp = ds.Tables[0].Copy();
                        dtTemp.DefaultView.Sort = "F_WORKDATE ASC, F_TSERIALNO ASC, F_DISPLAYNO ASC, F_NUMBER";
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dtTemp.ImportRow(dr);
                        }
                    }

                    i++;
                }
            }

            if (i > 0)
            {
                devGrid2.DataSource = dtTemp;
                devGrid2.DataBind();
            }
        }
        #endregion

        #region 중간검사(EF) 샘플검사목록
        void QWK13IS_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13IMID", GetDicValue(paramDic, "F_QWK13IMID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_WORKDATE", GetDicValue(paramDic, "F_WORKDATE"));


                ds = biz.QWK13IS_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

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

        #region 마스터 및 검사기준 신규 저장
        bool QWK13IM_EDIT(Dictionary<string, object> paramDic, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            // 트랜잭션 처리
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                do
                {
                    // 마스터 저장 호출 및 키값 반환
                    if (!QWK13IM_INS_UPD(paramDic, out errMsg, out pkey)) break;
                    // 중간검사(EF)샘플검사 저장호출
                    if (GetDicValue(paramDic, "F_REPORTTP") == "AAI123" && !String.IsNullOrEmpty(GetDicValue(paramDic, "F_EFITEMS")))
                    {
                        if (!PROC_BATCH(pkey, paramDic, out errMsg)) break;
                    }
                    // DB처리 모두 성공시 커밋 처리
                    scope.Complete();
                    bExecute = true;
                } while (false);
            }

            return bExecute;
        }

        bool QWK13IM_INS_UPD(Dictionary<string, object> paramDic, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_QWK13IMID"] = GetDicValue(paramDic, "F_QWK13IMID");
                oParamDic["F_REPORTTP"] = GetDicValue(paramDic, "F_REPORTTP");
                oParamDic["F_WORKDATE"] = GetDicValue(paramDic, "F_WORKDATE");
                oParamDic["F_ITEMCD"] = GetDicValue(paramDic, "F_ITEMCD");
                oParamDic["F_WORKCD"] = GetDicValue(paramDic, "F_WORKCD");
                oParamDic["F_LOTNO"] = GetDicValue(paramDic, "F_LOTNO");
                oParamDic["F_MOLDNO"] = GetDicValue(paramDic, "F_MOLDNO");
                oParamDic["F_WORKMAN"] = GetDicValue(paramDic, "F_WORKMAN");
                oParamDic["F_KSINFO"] = GetDicValue(paramDic, "F_KSINFO");
                oParamDic["F_TITLE"] = GetDicValue(paramDic, "F_TITLE").Replace("\\n", Environment.NewLine);
                oParamDic["F_WRITER"] = GetDicValue(paramDic, "F_WRITER");
                oParamDic["F_REVIEWER"] = GetDicValue(paramDic, "F_REVIEWER");
                oParamDic["F_APPROVER"] = GetDicValue(paramDic, "F_APPROVER");
                oParamDic["F_LOTCNT"] = GetDicValue(paramDic, "F_LOTCNT");
                oParamDic["F_UNIT"] = GetDicValue(paramDic, "F_UNIT");
                oParamDic["F_JUDGE"] = GetDicValue(paramDic, "F_JUDGE");
                oParamDic["F_MATERIAL"] = GetDicValue(paramDic, "F_MATERIAL");
                oParamDic["F_INCNT"] = GetDicValue(paramDic, "F_INCNT");
                oParamDic["F_INUNIT"] = GetDicValue(paramDic, "F_INUNIT");
                oParamDic["F_INDATE"] = GetDicValue(paramDic, "F_INDATE");
                oParamDic["F_INITEMTITLE"] = GetDicValue(paramDic, "F_INITEMTITLE");
                oParamDic["F_INCUST"] = GetDicValue(paramDic, "F_INCUST");
                oParamDic["F_INBASENO"] = GetDicValue(paramDic, "F_INBASENO");
                oParamDic["F_ESTDATE"] = GetDicValue(paramDic, "F_ESTDATE");
                oParamDic["F_INJCUST"] = GetDicValue(paramDic, "F_INJCUST");
                oParamDic["F_INJDATE"] = GetDicValue(paramDic, "F_INJDATE");
                oParamDic["F_THERMICOUT"] = GetDicValue(paramDic, "F_THERMICOUT");
                oParamDic["F_THERMICUNIT"] = GetDicValue(paramDic, "F_THERMICUNIT");
                oParamDic["F_COVEROUT"] = GetDicValue(paramDic, "F_COVEROUT");
                oParamDic["F_COVERUNIT"] = GetDicValue(paramDic, "F_COVERUNIT");
                oParamDic["F_INTYPE"] = GetDicValue(paramDic, "F_INTYPE");
                oParamDic["F_INMATERIAL"] = GetDicValue(paramDic, "F_INMATERIAL");
                oParamDic["F_DRAWNO"] = GetDicValue(paramDic, "F_DRAWNO");
                oParamDic["F_INITEMNM"] = GetDicValue(paramDic, "F_INITEMNM");
                oParamDic["F_INTEMP"] = GetDicValue(paramDic, "F_INTEMP");
                oParamDic["F_CYCEL"] = GetDicValue(paramDic, "F_CYCEL");
                oParamDic["F_CYCELUNIT"] = GetDicValue(paramDic, "F_CYCELUNIT");
                oParamDic["F_MACHINE"] = GetDicValue(paramDic, "F_MACHINE");
                oParamDic["F_DISTRIBUTE"] = GetDicValue(paramDic, "F_DISTRIBUTE");
                oParamDic["F_THERMICCNT"] = GetDicValue(paramDic, "F_THERMICCNT");
                oParamDic["F_COATING"] = GetDicValue(paramDic, "F_COATING");
                oParamDic["F_COVERING"] = GetDicValue(paramDic, "F_COVERING");
                oParamDic["F_RAWLOTNO"] = GetDicValue(paramDic, "F_RAWLOTNO");
                oParamDic["F_INJMACHINE"] = GetDicValue(paramDic, "F_INJMACHINE");
                oParamDic["F_WIRELOTNO"] = GetDicValue(paramDic, "F_WIRELOTNO");
                oParamDic["F_MAKEDATE"] = GetDicValue(paramDic, "F_MAKEDATE");
                oParamDic["F_DAYNIGHT"] = GetDicValue(paramDic, "F_DAYNIGHT");
                oParamDic["F_CONFIRM"] = GetDicValue(paramDic, "F_CONFIRM");
                oParamDic["F_USER"] = gsUSERID;
                oParamDic["PKEY"] = "OUTPUT";

                bExecute = biz.QWK13IM_INS_UPD(oParamDic, out errMsg, out pkey);
            }

            return bExecute;
        }

        bool PROC_BATCH(string F_QWK13IMID, Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            List<string> oSPs = new List<string>();
            List<object> oParams = new List<object>();
            Dictionary<string, object> oJsonDic = null;

            oParamDic = new Dictionary<string, string>();
            oParamDic["F_COMPCD"] = gsCOMPCD;
            oParamDic["F_FACTCD"] = gsFACTCD;
            oParamDic["F_QWK13IMID"] = F_QWK13IMID;
            oParamDic["F_REPORTTP"] = GetDicValue(paramDic, "F_REPORTTP");
            oParamDic["F_ITEMCD"] = GetDicValue(paramDic, "F_ITEMCD");
            oSPs.Add("USP_QWK13IS_DEL");
            oParams.Add(oParamDic);

            foreach (object oJsonDataList in (ArrayList)paramDic["F_EFITEMS"])
            {
                oJsonDic = (Dictionary<string, object>)oJsonDataList;

                oParamDic = new Dictionary<string, string>();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_QWK13IMID"] = F_QWK13IMID;
                oParamDic["F_REPORTTP"] = GetDicValue(paramDic, "F_REPORTTP");
                oParamDic["F_WORKDATE"] = GetDicValue(oJsonDic, "F_WORKDATE");
                oParamDic["F_ITEMCD"] = GetDicValue(oJsonDic, "F_ITEMCD");
                oParamDic["F_WORKCD"] = GetDicValue(oJsonDic, "F_WORKCD");
                oParamDic["F_QWK13ISID"] = "0";
                oParamDic["F_TSERIALNO"] = GetDicValue(oJsonDic, "F_TSERIALNO");
                oParamDic["F_USER"] = gsUSERID;
                oSPs.Add("USP_QWK13IS_INS_UPD");
                oParams.Add(oParamDic);
            }

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 확정
        bool QWK13IM_CFM(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13IMID", GetDicValue(paramDic, "F_QWK13IMID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_ITEMCD", GetDicValue(paramDic, "F_ITEMCD"));
                oParamDic.Add("F_WORKCD", GetDicValue(paramDic, "F_WORKCD"));
                oParamDic.Add("F_WORKDATE", GetDicValue(paramDic, "F_WORKDATE"));
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13IM_CFM(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid1_CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK13ID_LST(paramDic);
        }
        #endregion

        #region devGrid1_CustomColumnDisplayText
        /// <summary>
        /// devGrid1_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid1_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (e.Column.FieldName.Equals("F_NGOKCHK"))
            {
                e.EncodeHtml = false;
                switch (e.Value.ToString())
                {
                    case "0": e.DisplayText = @"<span style='color:blue;'>OK</span>"; break;
                    case "1": e.DisplayText = @"<span style='color:red;'>NG</span>"; break;
                    case "2": e.DisplayText = @"<span style='color:dimgray;'>OC</span>"; break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region devGrid2_CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sParam = String.Empty;
            string errMsg = String.Empty;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                sParam = e.Parameters;
                if (sParam.Substring(0, 5).Equals("RESET"))
                {
                    sParam = sParam.Substring(5);
                    List<object> oList = jss.Deserialize<List<object>>(sParam); // 웹에서 전달된 파라미터
                    QWK03A_INSP0302_SUB_GET_LST(oList, out errMsg);

                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        // Grid Callback Init
                        devGrid2.JSProperties["cpResultCode"] = "0";
                        devGrid2.JSProperties["cpResultMsg"] = errMsg;
                    }
                }
                else
                {
                    Dictionary<string, object> paramDic = jss.Deserialize<Dictionary<string, object>>(sParam); // 웹에서 전달된 파라미터
                    QWK13IS_LST(paramDic);
                }
            }
            else
            {
                QWK13IS_LST(new Dictionary<string, object>());
            }
        }
        #endregion

        #region devGrid2_CustomColumnDisplayText
        /// <summary>
        /// devGrid2_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid2_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            if (e.Column.FieldName.Equals("F_NGOKCHK"))
            {
                e.EncodeHtml = false;
                switch (e.Value.ToString())
                {
                    case "0": e.DisplayText = @"<span style='color:blue;'>OK</span>"; break;
                    case "1": e.DisplayText = @"<span style='color:red;'>NG</span>"; break;
                    case "2": e.DisplayText = @"<span style='color:dimgray;'>OC</span>"; break;
                    default:
                        break;
                }
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
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            bool bExecute = false;
            Dictionary<string, object> oDataDic = null;
            string errMsg = String.Empty;   // 오류 메시지
            string pkey = String.Empty;
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"].ToString())
            {
                case "EDIT":
                    if (!QWK13IM_EDIT(paramDic, out errMsg, out pkey))
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
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    ds = QWK13IM_GET(paramDic, out errMsg);
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
                        msg = "";
                        DataTable dtTemp = ds.Tables[0].Copy();
                        // 조회한 데이터를 Dictionary 형태로 변환
                        DataRow dr = dtTemp.Rows[0];
                        PAGEDATA = dtTemp.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    }
                    result["ISOK"] = ISOK;
                    result["MSG"] = msg;
                    result["PAGEDATA"] = PAGEDATA;
                    e.Result = jss.Serialize(result);
                    break;
                case "CFM":
                    if (!QWK13IM_CFM(paramDic, out errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "확정되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #endregion
    }
}