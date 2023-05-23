using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using System.Collections;
using DevExpress.Web;
using SPC.SYST.Biz;
using SPC.BSIF.Biz;
using System.Text;

namespace SPC.WebUI.Pages.DIOF.Popup
{
    public partial class DIOF0501POP_cheongmyung : WebUIBasePage
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

            // 관리자유무체크
            QCD_MACH23_CHK();

            // 점검기준조회
            RetrieveInspList();

            // 불량처리조회
            RetrieveRespList();
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
                devGridResp.JSProperties["cpResultCode"] = "";
                devGridResp.JSProperties["cpResultMsg"] = "";
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
            srcF_MACHIDX.Text = Request.QueryString.Get("KEY");
            srcF_MACHCD.Text = Request.QueryString.Get("CODE");
            srcF_MACHNM.Text = Request.QueryString.Get("TEXT");
            srcF_DATE.Text = Request.QueryString.Get("DATE");
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 관리자유무체크
        void QCD_MACH23_CHK()
        {
            string bManager = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_USERID", gsUSERID);
                bManager = biz.QCD_MACH23_CHK(oParamDic);
            }

            txtManager.Text = bManager;
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
                oParamDic.Add("F_MEASYMD", srcF_DATE.Text);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH23_MACH26_LST(oParamDic, out errMsg);
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
                //if (isCallback)
                devGrid.DataBind();
            }
        }
        #endregion

        #region 설비이상조치조회
        void RetrieveRespList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_DATE", srcF_DATE.Text);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QWK_MACH24_LST2_CM(oParamDic, out errMsg);
            }

            devGridResp.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridResp.JSProperties["cpResultCode"] = "0";
                devGridResp.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridResp.DataBind();
            }
        }
        #endregion

        #region 선택일체크정보조회
        DataSet RetrieveConfirmDS(out string errMsg)
        {
            errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_MEASYM", srcF_DATE.Text);
                oParamDic.Add("F_DAYIDX", srcF_DAYIDX.Text);
                ds = biz.QWK_MACH22_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인 목록을 구한다
        DataSet SYCOD01_LST(string strCodegroup)
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CODEGROUP", strCodegroup);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택일 관리자확인
        private bool QWK_MACH22_INS_UPD(string sCNFMYN, out string dayIDX, out string resultMsg)
        {
            dayIDX = String.Empty;
            resultMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_MEASYM", srcF_DATE.Text);
                oParamDic.Add("F_DAYIDX", !String.IsNullOrEmpty(srcF_DAYIDX.Text) ? srcF_DAYIDX.Text : "0");
                oParamDic.Add("F_USERID", gsUSERID);
                oParamDic.Add("F_USERNM", gsUSERNM);
                oParamDic.Add("F_CNFMYN", sCNFMYN);
                oParamDic.Add("F_DAYIDXOUT", "OUTPUT");

                bExecute = biz.QWK_MACH22_INS_UPD(oParamDic, out oOutParamDic, out resultMsg);

                if (bExecute == true)
                {
                    foreach (KeyValuePair<string, object> oOutDic in oOutParamDic)
                    {
                        if (oOutDic.Key.Equals("F_DAYIDXOUT"))
                            dayIDX = oOutDic.Value.ToString();
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
            // 점검기준조회
            RetrieveInspList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_CYCLENM"))
            {
                string sValue = "";
                object obj = devGrid.GetRowValues(e.VisibleRowIndex, "F_CYCLECD", "F_NUMBER", "F_CHASU");

                if (obj != null)
                {
                    string[] rowValues = ((IEnumerable)obj).Cast<object>().Select(x => x.ToString()).ToArray();

                    switch (rowValues[0])
                    {
                        case "AAG401":
                            if (rowValues[2].Equals("1")) sValue = "1일"; else sValue = String.Format("{0}차", rowValues[1]);
                            break;
                        case "AAG407":
                            if (rowValues[1].Equals("1")) sValue = "주간"; else sValue = "야간";
                            break;
                        default:
                            sValue = e.Value.ToString();
                            break;
                    }
                }

                e.DisplayText = sValue;
            }
            else if (e.Column.FieldName.Equals("F_JUDGE"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
            else
                return;
        }
        #endregion

        #region devGridResp_CustomCallback
        /// <summary>
        /// devGridResp_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridResp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 불량처리조회
            RetrieveRespList();
        }
        #endregion

        #region devGridResp_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGridResp_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_INSPNM", "F_NGREMK", "F_RESPREMK" };

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

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string dayIDX = String.Empty;
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            if (paramDic["ACTION"] == "SAVE")
            {
                if (!QWK_MACH22_INS_UPD(paramDic["CONFIRM"], out dayIDX, out errMsg))
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
                    PKEY["F_DAYIDX"] = dayIDX;
                    result["PKEY"] = PKEY;
                }
                e.Result = jss.Serialize(result);
            }
            else if (paramDic["ACTION"] == "GET")
            {
                Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                if (ds != null) ds.Clear();
                ds = RetrieveConfirmDS(out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    ISOK = false;
                    msg = errMsg;
                }
                else if (!bExistsDataSet(ds))
                {
                    ISOK = false;
                    msg = "NO DATA";
                }
                else
                {
                    ISOK = true;
                    msg = string.Empty;
                    DataRow dr = ds.Tables[0].Rows[0];
                    PAGEDATA["F_DAYIDX"] = dr["F_DAYIDX"].ToString();
                    PAGEDATA["F_CNFMYN"] = dr["F_CNFMYN"].ToString();
                    PAGEDATA["F_CONFIRM"] = String.Format("[{0}] {1} {2}",
                        DateTime.Parse(dr["F_CNFMDT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                        dr["F_USERNM"],
                        !bool.Parse(dr["F_CNFMYN"].ToString()) ? "확인 취소되었습니다" : "확인 처리되었습니다");
                }
                result["ISOK"] = ISOK;
                result["MSG"] = msg;
                result["PAGEDATA"] = PAGEDATA;
                e.Result = jss.Serialize(result);
            }
        }
        #endregion

        protected void devGridResp_DataBound(object sender, EventArgs e)
        {
            GridViewDataComboBoxColumn combo = devGridResp.Columns["F_NGTYPE"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.TextField = "F_CODENMKR";
            combo.PropertiesComboBox.ValueField = "F_CODE";
            combo.PropertiesComboBox.DataSource = SYCOD01_LST("41");


            GridViewDataComboBoxColumn combo1 = devGridResp.Columns["F_RESPTYPE"] as GridViewDataComboBoxColumn;
            combo1.PropertiesComboBox.TextField = "F_CODENMKR";
            combo1.PropertiesComboBox.ValueField = "F_CODE";
            combo1.PropertiesComboBox.DataSource = SYCOD01_LST("43");

            GridViewDataComboBoxColumn combo2 = devGridResp.Columns["F_STATUS"] as GridViewDataComboBoxColumn;
            combo2.PropertiesComboBox.TextField = String.Format("COMMNMKR", gsLANGTP);
            combo2.PropertiesComboBox.ValueField = "COMMCD";
            combo2.PropertiesComboBox.DataSource = CachecommonCode["AA"]["AAG9"].codeGroup.Values;

        }

        #endregion

        protected void devGridResp_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + (e.DeleteValues.Count * 4);

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Update

            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);

                    oParamDic.Add("F_REMEDYIDX", Value.Keys["F_REMEDYIDX"].ToString());
                    oParamDic.Add("F_INSPIDX", Value.Keys["F_INSPIDX"].ToString());
                    oParamDic.Add("F_MACHIDX", Value.Keys["F_MACHIDX"].ToString());
                    oParamDic.Add("F_NUMBER", Value.Keys["F_NUMBER"].ToString());
                    oParamDic.Add("F_WORKDATE", Value.Keys["F_OCCURDT"].ToString());
                    oParamDic.Add("F_NGTYPE", Value.NewValues["F_NGTYPE"] == null ? "" : Value.NewValues["F_NGTYPE"].ToString());
                    oParamDic.Add("F_NGREMK", Value.NewValues["F_NGREMK"] == null ? "" : Value.NewValues["F_NGREMK"].ToString());
                    oParamDic.Add("F_RESPTYPE", Value.NewValues["F_RESPTYPE"] == null ? "" : Value.NewValues["F_RESPTYPE"].ToString());
                    oParamDic.Add("F_RESPREMK", Value.NewValues["F_RESPREMK"] == null ? "" : Value.NewValues["F_RESPREMK"].ToString());
                    oParamDic.Add("F_STATUS", Value.NewValues["F_STATUS"] == null ? "" : Value.NewValues["F_STATUS"].ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QWK_MACH24_INS";
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
                    bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    StringBuilder sb_OutMsg = new StringBuilder();
                    foreach (Dictionary<string, object> _oOutParamDic in oOutParamList)
                    {
                        foreach (KeyValuePair<string, object> _oOutPair in _oOutParamDic)
                        {
                            if (!String.IsNullOrEmpty(_oOutPair.Value.ToString()))
                                sb_OutMsg.AppendFormat("{0}", _oOutPair.Value);
                        }
                    }

                    if (!String.IsNullOrEmpty(sb_OutMsg.ToString()))
                    {
                        procResult = new string[] { "2", sb_OutMsg.ToString() };
                    }
                    else
                    {
                        procResult = new string[] { "1", "저장이 완료되었습니다." };
                    }

                    //GetQCD34_LST(ucPager.GetPageSize(), ucPager.GetCurrPage(), false);
                }
            }


            devGridResp.JSProperties["cpResultCode"] = procResult[0];
            devGridResp.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
            RetrieveRespList();
            e.Handled = true;
        }
    }
}