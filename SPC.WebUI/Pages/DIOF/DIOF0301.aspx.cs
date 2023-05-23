using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using System.Text;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0301 : WebUIBasePage
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
                devGridInsp.JSProperties["cpResultCode"] = "";
                devGridInsp.JSProperties["cpResultMsg"] = "";
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

        #region 설비조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHNM", schF_MACHNM.Text);
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
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

        #region 점검기준조회
        void RetrieveInspList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH26_LST(oParamDic, out errMsg);
            }

            devGridInsp.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridInsp.JSProperties["cpResultCode"] = "0";
                devGridInsp.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridInsp.DataBind();
            }
        }
        #endregion

        #region 뷰(선택한 점검항목 상세정보 조회)
        DataSet RetrieveInspDetail(string inspIDX, out string errMsg)
        {
            errMsg = String.Empty;
            if (ds != null) ds.Clear();
            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_INSPIDX", inspIDX);
                ds = biz.QCD_MACH26_GET(oParamDic, out errMsg);
            }
            return ds;
        }
        #endregion

        #region 저장
        private bool QCD_MACH26_INS(out string inspIDX, out string resultMsg)
        {
            inspIDX = String.Empty;
            resultMsg = String.Empty;
            bool bExecute = false;

            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();

            using (FDCKBiz biz = new FDCKBiz())
            {
                // 등록
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_INSPIDX", "OUTPUT");
                oParamDic.Add("F_OLDINSPIDX", "0");
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_INSPCD", "0");
                oParamDic.Add("F_INSPNM", srcF_INSPNM.Text);
                oParamDic.Add("F_INSPKINDCD", srcF_INSPKINDCD.GetValue());
                oParamDic.Add("F_STAND", srcF_STAND.Text);
                oParamDic.Add("F_VIEWSTAND", srcF_VIEWSTAND.Text);
                oParamDic.Add("F_MAX", srcF_MAX.Text);
                oParamDic.Add("F_MIN", srcF_MIN.Text);
                oParamDic.Add("F_INSPREMARK", srcF_INSPREMARK.Text);
                oParamDic.Add("F_INSPWAY", srcF_INSPWAY.Text);
                oParamDic.Add("F_IMAGESEQ", !String.IsNullOrEmpty(txtIMAGESEQ.Text) ? txtIMAGESEQ.Text : UF.Encrypts.GetUniqueKey());
                oParamDic.Add("F_INSPORDER", srcF_INSPORDER.Text);
                oParamDic.Add("F_CYCLECD", srcF_CYCLECD.GetValue());
                oParamDic.Add("F_INSPNO", srcF_INSPNO.Text);
                oParamDic.Add("F_CHASU", srcF_CYCLECD.GetValue().Equals("AAG407") ? "2" : srcF_CHASU.Text);
                oParamDic.Add("F_UNIT", srcF_UNIT.GetValue());
                oParamDic.Add("F_USEYN", !srcF_USEYN.Checked ? "0" : "1");
                oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                oParamDic.Add("F_USESTDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                oParamDic.Add("F_USEEDDT", "9999.12.31");
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_HISTTPCD", "AAG501");
                oParams.Add(oParamDic);
                oSPs.Add("USP_QCD_MACH26_INS");

                bExecute = biz.QCD_MACH26_INS(oParamDic, out oOutParamDic, out resultMsg);

                if (bExecute == true)
                {
                    foreach (KeyValuePair<string, object> oOutDic in oOutParamDic)
                    {
                        if (oOutDic.Key.Equals("F_INSPIDX"))
                            inspIDX = oOutDic.Value.ToString();
                    }
                }
            }

            if (!String.IsNullOrEmpty(resultMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #region 수정
        private bool QCD_MACH26_UPD(out string inspIDX, out string resultMsg)
        {
            inspIDX = srcF_INSPIDX.Text;
            resultMsg = String.Empty;
            bool bExecute = false;

            string updateMode = "";

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_INSPIDX", inspIDX);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_INSPKINDCD", srcF_INSPKINDCD.GetValue());
                oParamDic.Add("F_STAND", srcF_STAND.Text);
                oParamDic.Add("F_MAX", srcF_MAX.Text);
                oParamDic.Add("F_MIN", srcF_MIN.Text);
                oParamDic.Add("F_CYCLECD", srcF_CYCLECD.GetValue());

                updateMode = biz.QCD_MACH26_CHK(oParamDic);
            }

            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();

            switch (updateMode)
            {
                case "U":   // 일반업데이트
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_INSPIDX", inspIDX);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_INSPCD", srcF_INSPCD.Text);
                    oParamDic.Add("F_INSPNM", srcF_INSPNM.Text);
                    oParamDic.Add("F_INSPKINDCD", srcF_INSPKINDCD.GetValue());
                    oParamDic.Add("F_STAND", srcF_STAND.Text);
                    oParamDic.Add("F_VIEWSTAND", srcF_VIEWSTAND.Text);
                    oParamDic.Add("F_MAX", srcF_MAX.Text);
                    oParamDic.Add("F_MIN", srcF_MIN.Text);
                    oParamDic.Add("F_INSPREMARK", srcF_INSPREMARK.Text);
                    oParamDic.Add("F_INSPWAY", srcF_INSPWAY.Text);
                    oParamDic.Add("F_IMAGESEQ", !String.IsNullOrEmpty(txtIMAGESEQ.Text) ? txtIMAGESEQ.Text : UF.Encrypts.GetUniqueKey());
                    oParamDic.Add("F_INSPORDER", srcF_INSPORDER.Text);
                    oParamDic.Add("F_CYCLECD", srcF_CYCLECD.GetValue());
                    oParamDic.Add("F_INSPNO", srcF_INSPNO.Text);
                    oParamDic.Add("F_CHASU", srcF_CYCLECD.GetValue().Equals("AAG407") ? "2" : srcF_CHASU.Text);
                    oParamDic.Add("F_UNIT", srcF_UNIT.GetValue());
                    oParamDic.Add("F_USEYN", !srcF_USEYN.Checked ? "0" : "1");
                    oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_USESTDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                    oParamDic.Add("F_USEEDDT", "9999.12.31");
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH26_UPD");

                    // 이력등록
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_HISTTPCD", "AAG502");
                    oParamDic.Add("F_REASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_INSPIDX_OLD", inspIDX);
                    oParamDic.Add("F_INSPIDX_NEW", inspIDX);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH27_INS");
                    break;
                case "D":   // 삭제 후 인서트
                    // 삭제
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_INSPIDX", inspIDX);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_USEEDDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                    oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_USERDELYN", "0");
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH26_DEL");

                    // 저장
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_INSPIDX", "OUTPUT");
                    oParamDic.Add("F_OLDINSPIDX", inspIDX);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_INSPCD", "0");
                    oParamDic.Add("F_INSPNM", srcF_INSPNM.Text);
                    oParamDic.Add("F_INSPKINDCD", srcF_INSPKINDCD.GetValue());
                    oParamDic.Add("F_STAND", srcF_STAND.Text);
                    oParamDic.Add("F_VIEWSTAND", srcF_VIEWSTAND.Text);
                    oParamDic.Add("F_MAX", srcF_MAX.Text);
                    oParamDic.Add("F_MIN", srcF_MIN.Text);
                    oParamDic.Add("F_INSPREMARK", srcF_INSPREMARK.Text);
                    oParamDic.Add("F_INSPWAY", srcF_INSPWAY.Text);
                    oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                    oParamDic.Add("F_INSPORDER", srcF_INSPORDER.Text);
                    oParamDic.Add("F_CYCLECD", srcF_CYCLECD.GetValue());
                    oParamDic.Add("F_INSPNO", srcF_INSPNO.Text);
                    oParamDic.Add("F_CHASU", srcF_CYCLECD.GetValue().Equals("AAG407") ? "2" : srcF_CHASU.Text);
                    oParamDic.Add("F_UNIT", srcF_UNIT.GetValue());
                    oParamDic.Add("F_USEYN", !srcF_USEYN.Checked ? "0" : "1");
                    oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_USESTDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                    oParamDic.Add("F_USEEDDT", "9999.12.31");
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_HISTTPCD", "AAG502");
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH26_INS");
                    break;
                case "R":   // 리비전 후 인서트
                    // 리비전
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_INSPIDX", inspIDX);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_USEEDDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                    oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH26_REV");

                    // 저장
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_INSPIDX", "OUTPUT");
                    oParamDic.Add("F_OLDINSPIDX", inspIDX);
                    oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                    oParamDic.Add("F_INSPCD", srcF_INSPCD.Text);
                    oParamDic.Add("F_INSPNM", srcF_INSPNM.Text);
                    oParamDic.Add("F_INSPKINDCD", srcF_INSPKINDCD.GetValue());
                    oParamDic.Add("F_STAND", srcF_STAND.Text);
                    oParamDic.Add("F_VIEWSTAND", srcF_VIEWSTAND.Text);
                    oParamDic.Add("F_MAX", srcF_MAX.Text);
                    oParamDic.Add("F_MIN", srcF_MIN.Text);
                    oParamDic.Add("F_INSPREMARK", srcF_INSPREMARK.Text);
                    oParamDic.Add("F_INSPWAY", srcF_INSPWAY.Text);
                    oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                    oParamDic.Add("F_INSPORDER", srcF_INSPORDER.Text);
                    oParamDic.Add("F_CYCLECD", srcF_CYCLECD.GetValue());
                    oParamDic.Add("F_INSPNO", srcF_INSPNO.Text);
                    oParamDic.Add("F_CHASU", srcF_CYCLECD.GetValue().Equals("AAG407") ? "2" : srcF_CHASU.Text);
                    oParamDic.Add("F_UNIT", srcF_UNIT.GetValue());
                    oParamDic.Add("F_USEYN", !srcF_USEYN.Checked ? "0" : "1");
                    oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                    oParamDic.Add("F_USESTDT", UF.Date.TryParse(srcF_USESTDT.Text, DateTime.Today.ToString("yyyy.MM.dd")));
                    oParamDic.Add("F_USEEDDT", "9999.12.31");
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_HISTTPCD", "AAG504");
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QCD_MACH26_INS");
                    break;
            }

            if (oSPs.Count > 0 && oParams.Count > 0 && oSPs.Count == oParams.Count)
            {
                using (FDCKBiz biz = new FDCKBiz())
                {
                    bExecute = biz.PROC_QCD_MACH_MULTI(oSPs.ToArray(), oParams.ToArray(), out oOutParamList, out resultMsg);

                    if (bExecute == true)
                    {
                        foreach (object _oOutParamDic in oOutParamList)
                        {
                            foreach (KeyValuePair<string, object> oOutDic in (Dictionary<string, object>)_oOutParamDic)
                            {
                                if (oOutDic.Key.Equals("F_INSPIDX"))
                                    inspIDX = oOutDic.Value.ToString();
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(resultMsg))
                    bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        private bool QCD_MACH26_DEL(out string resultMsg)
        {
            resultMsg = String.Empty;
            bool bExecute = false;

            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();

            // 삭제
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_INSPIDX", srcF_INSPIDX.Text);
            oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
            oParamDic.Add("F_USEEDDT", DateTime.Today.ToString("yyyy.MM.dd"));
            oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
            oParamDic.Add("F_USERDELYN", "1");
            oParamDic.Add("F_USER", gsUSERID);
            oParams.Add(oParamDic);
            oSPs.Add("USP_QCD_MACH26_DEL");

            // 이력등록
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_HISTTPCD", "AAG502");
            oParamDic.Add("F_REASON", srcF_MODREASON.Text);
            oParamDic.Add("F_INSPIDX_OLD", srcF_INSPIDX.Text);
            oParamDic.Add("F_INSPIDX_NEW", srcF_INSPIDX.Text);
            oParamDic.Add("F_USER", gsUSERID);
            oParams.Add(oParamDic);
            oSPs.Add("USP_QCD_MACH27_INS");

            if (oSPs.Count > 0 && oParams.Count > 0 && oSPs.Count == oParams.Count)
            {
                using (FDCKBiz biz = new FDCKBiz())
                {
                    bExecute = biz.PROC_QCD_MACH_MULTI(oSPs.ToArray(), oParams.ToArray(), out resultMsg);
                }

                if (!String.IsNullOrEmpty(resultMsg))
                    bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 설비조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USEYN" }, "가동", "비가동", e);
        }
        #endregion

        #region devGridInsp_CustomCallback
        /// <summary>
        /// devGridInsp_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridInsp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 점검기준조회
            RetrieveInspList();
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            string inspIDX = String.Empty;  // 설비고유번호(IDENTITY)
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"])
            {
                case "SAVE":
                    if (paramDic["PAGEMODE"] == "C")
                    {
                        if (!QCD_MACH26_INS(out inspIDX, out errMsg))
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
                            PKEY["F_INSPIDX"] = inspIDX;
                            result["PKEY"] = PKEY;
                        }
                    }
                    else if (paramDic["PAGEMODE"] == "U")
                    {
                        if (!QCD_MACH26_UPD(out inspIDX, out errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["MSG"] = "수정되었습니다.";
                            PKEY["F_INSPIDX"] = inspIDX;
                            result["PKEY"] = PKEY;
                        }
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    if (ds != null) ds.Clear();
                    ds = RetrieveInspDetail(paramDic["F_INSPIDX"], out errMsg);
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
                case "DELETE":
                    if (!QCD_MACH26_DEL(out errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "삭제되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #endregion
    }
}