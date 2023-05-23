using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;
using SPC.SYST.Biz;

namespace SPC.WebUI.Pages.LTRK
{
    public partial class LTRK0404 : WebUIBasePage
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

            srcF_DATE.Date = DateTime.Today;

            // 품목분류를 구한다
            SYCOD01_LST();

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
            }

            // 조회
            QPM13_LST_FOR_DATE();
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

        #region 품목분류 조회
        void SYCOD01_LST()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                oParamDic.Add("F_CODEGROUP", "24");
                oParamDic.Add("F_IPADDRESS", "AAE503");
                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            ddlF_GUBN.Items.Clear();

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_CODE", typeof(string));
            dtTable.Columns.Add("F_CODENM", typeof(string));

            dtTable.Rows.Add("", "선택");

            if (String.IsNullOrEmpty(errMsg) && bExistsDataSet(ds))
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    dtTable.Rows.Add(dtRow["F_CODE"].ToString(), dtRow["F_CODENM"].ToString());
                }
            }

            ddlF_GUBN.ValueField = "F_CODE";
            ddlF_GUBN.TextField = "F_CODENM";
            ddlF_GUBN.DataSource = dtTable;
            ddlF_GUBN.DataBind();
            ddlF_GUBN.SelectedIndex = 0;
        }
        #endregion

        #region 조회
        void QPM13_LST_FOR_DATE()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_DATE", srcF_DATE.Text);
                oParamDic.Add("F_ISMATERIAL", "0");
                oParamDic.Add("F_GUBN", (ddlF_GUBN.SelectedItem.Value ?? "").ToString());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_ISREAMIN", (ddlF_REAMIN.SelectedItem.Value ?? "").ToString());
                ds = biz.QPM13_LST_FOR_DATE(oParamDic, out errMsg);
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

        #region 해당월 마감여부 체크
        bool QPM01_CLOSE_CHK(string sYYYYMM, out bool isClosed, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_YYYYMM", sYYYYMM);
                ds = biz.QPM01_CLOSE_CHK(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                bExecute = false;
                isClosed = false;
            }
            else
            {
                bExecute = true;
                isClosed = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnLink_Init
        /// <summary>
        /// btnLink_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnLink_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;
            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ITEMCD", "F_ITEMNM", "F_REMAINCNT", "F_UNITNM") as object[];
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnPopupLTRKINOUTPOP('{0}', '{1}', '{2}', '{3}'); }}",
                rowValues[0],
                rowValues[1],
                "0",
                rowValues[2],
                rowValues[3]);
        }
        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 조회
            QPM13_LST_FOR_DATE();
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
            if (e.DataColumn.FieldName.Equals("F_REMAINCNT"))
            {
                decimal F_REMAINCNT = Convert.ToDecimal(e.CellValue);
                decimal F_DANGA = Convert.ToDecimal(devGrid.GetRowValues(e.VisibleIndex, "F_DANGA"));

                if (F_REMAINCNT > F_DANGA)
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                else if (F_REMAINCNT < F_DANGA)
                    e.Cell.ForeColor = System.Drawing.Color.Red;
                else
                    e.Cell.ForeColor = System.Drawing.Color.Blue;
            }
        }
        #endregion

        #region devGrid_BatchUpdate
        /// <summary>
        /// devGrid_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

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
                    oParamDic.Add("F_GROUPCD", "AAE503");
                    oParamDic.Add("F_INPUTDATE", srcF_DATE.Text);
                    oParamDic.Add("F_ITEMCD", Value.Keys["F_ITEMCD"].ToString());
                    oParamDic.Add("F_INOUTNO", "");
                    oParamDic.Add("F_INOUTTP", "AAE704");
                    oParamDic.Add("F_CNT", (Convert.ToDecimal(Value.NewValues["F_CHANGECNT"]) - Convert.ToDecimal(Value.Keys["F_REMAINCNT"])).ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs.Add("USP_QPM13_INS");
                    oParameters.Add((object)oParamDic);

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_GROUPCD", "AAE503");
                    oParamDic.Add("F_INNO", "");
                    oParamDic.Add("F_ITEMCD", Value.Keys["F_ITEMCD"].ToString());
                    oParamDic.Add("F_CNT", (Convert.ToDecimal(Value.NewValues["F_CHANGECNT"]) - Convert.ToDecimal(Value.Keys["F_REMAINCNT"])).ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs.Add("USP_QPM14_INS_UPD");
                    oParameters.Add((object)oParamDic);
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                bExecute = biz.PROC_QPM13_BATCH(oSPs.ToArray(), oParameters.ToArray(), out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 조회
                QPM13_LST_FOR_DATE();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
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

            if (paramDic["ACTION"] == "CLOSECHK")
            {
                bool isClosed = true;
                if (!QPM01_CLOSE_CHK(paramDic["F_DATE"], out isClosed, out errMsg))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = errMsg;
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = isClosed ? "이미 마감된 월입니다" : "";
                }
                e.Result = jss.Serialize(result);
            }
        }
        #endregion

        #endregion
    }
}