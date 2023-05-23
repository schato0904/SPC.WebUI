using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using SPC.WebUI.Common;
using SPC.WERD.Biz;
using SPC.SYST.Biz;
using SPC.Common.Biz;
using SPC.BSIF.Biz;


namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0101 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public string F_DKINDCD = ""; // 기술표준
        #endregion

        #region 프로퍼티
        protected DataTable sessionDt
        {
            set { this.Session["WERD0101_DT"] = value; }
            get { return this.Session["WERD0101_DT"] as DataTable; }
        }

        protected DataTable sessionDt1
        {
            set { this.Session["WERD0101_DT1"] = value; }
            get { return this.Session["WERD0101_DT1"] as DataTable; }
        }

        protected DataTable sessionDt2
        {
            set { this.Session["WERD0101_DT2"] = value; }
            get { return this.Session["WERD0101_DT2"] as DataTable; }
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
            // Request
            GetRequest();


            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid.JSProperties["cpResultEtc"] = "";
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGrid1.JSProperties["cpResultEtc"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultEtc"] = "";
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
        {
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 그리드 목록 조회
        /// <summary>
        /// 거래처 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void PROC_LST(Dictionary<string, string> FIELDS = null)
        {
            var WORKNM = schF_PROCNM.Text;

            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_WORKNM", WORKNM);

            // 히스토그램을 구한다
            using (CommonBiz biz = new CommonBiz())
            {
                ds = biz.GetQCD74_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!IsCallback)
                devGrid.DataBind();
        }
        #endregion

        #region 그리드 목록 조회(거래처별 품목)
        /// <summary>
        /// 거래처별 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void CM16M_LST_2()
        {
            string errMsg = String.Empty;
            var WORKCD = schF_PROCCD.Text;
            if (string.IsNullOrWhiteSpace(WORKCD)) return;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKCD", WORKCD);

                ds = biz.QCD7402_LST(oParamDic, out errMsg);
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
                sessionDt1 = (ds.Tables.Count > 0 ? ds.Tables[0] : null);
                if (!IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #region 그리드 목록 조회(거래처별 품목 정보: 저장전)
        /// <summary>
        /// 거래처별 품목 목록 조회 (저장전)
        /// </summary>
        /// <param name="F_ITEMCDVALUES">선택 품목 목록 문자열(구분자 ',')</param>
        /// <returns></returns>
        DataTable CM16M_LST_1(string F_ITEMCDVALUES)
        {
            string errMsg = String.Empty;
            var F_PROCCD = schF_PROCCD.Text;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCDLIST", F_ITEMCDVALUES);
                oParamDic.Add("F_WORKCD", F_PROCCD);

                ds = biz.QCD7402_MERGE_LST(oParamDic, out errMsg);
            }

            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }
        #endregion

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void CM10M_LST()
        {
            string errMsg = String.Empty;
            var F_PROCCD = schF_PROCCD.Text;
            if (string.IsNullOrWhiteSpace(F_PROCCD)) return;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", schF_ITEMCD.Text);
                oParamDic.Add("F_ITEMNM", schF_ITEMNM.Text);
                oParamDic.Add("F_WORKCD", F_PROCCD);
                //oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.GetDatSet(oParamDic, out errMsg, "USP_QCD01_WORK_LST");
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
                sessionDt2 = (ds.Tables.Count > 0 ? ds.Tables[0] : null);
                var keys = string.Empty;
                if (this.sessionDt1 != null && this.sessionDt1.Rows.Count > 0)
                {
                    this.sessionDt1.Rows.Cast<DataRow>().ToList().ForEach(x => keys += string.Format((string.IsNullOrEmpty(keys) ? "" : ",") + "{0}", x["F_ITEMCD"].ToString()));

                    var itemcdValues = keys;
                    var filterExpression = string.Format("F_ITEMCD NOT IN ('{0}')", itemcdValues.Replace(",", "', '"));

                    DataRow[] drs = this.sessionDt2.Select(filterExpression);
                    DataSet tempds = new DataSet();
                    tempds.Merge(drs);

                    this.sessionDt2 = tempds.Tables.Count > 0 ? tempds.Tables[0] : null;
                }

                devGrid2.DataSource = this.sessionDt2;

                if (!IsCallback)
                    devGrid2.DataBind();
            }
        }
        #endregion

        #region 저장
        void CM16M_INS(string lossamtvalues, out string[] procResult)
        {
            bool bExecute = false;
            var errMsg = string.Empty;
            procResult = new string[] { "", "" };

            string itemcdvalues = string.Empty;
            string workcdvalues = string.Empty;

            if (string.IsNullOrWhiteSpace(this.schF_PROCCD.Text)) return;

            if (this.sessionDt1 == null || this.sessionDt1.Rows.Count == 0)
            {
                itemcdvalues = "";
                workcdvalues = "";
            }
            else
            {
                this.sessionDt1.Rows.Cast<DataRow>().ToList().ForEach(x => itemcdvalues += string.Format((string.IsNullOrEmpty(itemcdvalues) ? "" : ",") + "{0}", x["F_ITEMCD"].ToString()));
                this.sessionDt1.Rows.Cast<DataRow>().ToList().ForEach(x => workcdvalues += string.Format((string.IsNullOrEmpty(workcdvalues) ? " {0}" : ",{0}"), x["F_WORKCD"].ToString()));
                workcdvalues = workcdvalues.Trim();
            }

            string[] oSPs = new string[1];
            object[] oParameters = new object[1];
            string resultMsg = String.Empty; 

            var oDic = new Dictionary<string, string>();
            oDic.Add("F_COMPCD", gsCOMPCD);
            oDic.Add("F_FACTCD", gsFACTCD);
            oDic.Add("F_ITEMCDVALUES", itemcdvalues);
            oDic.Add("F_LOSSAMTVALUES", lossamtvalues);
            oDic.Add("F_WORKCDVALUES", schF_PROCCD.Text);
            oDic.Add("F_USER", gsUSERNM);

            oSPs[0] = "USP_QCD7402_INS";
            oParameters[0] = (object)oDic;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCD73_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
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
            var pkey = string.Empty;
            // 그리드정보를 구한다
            if (e.Parameters == "clear")
            {
                devGrid.DataSource = null;
            }
            else
            {
                PROC_LST(); //거래처목록 조회
            }
            devGrid.DataBind();
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
            DevExpressLib.GetUsedString(new string[] { "F_USEYN" }, e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// 저장 처리
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            var result = new Dictionary<string, string>();
            var btnType = e.Parameter;
        }
        #endregion

        #region devGrid1_CustomCallback
        /// <summary>
        /// 거래처별 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            var param = e.Parameters.Split('|');
            if (param[0] == "clear")
            {
                this.sessionDt1 = null;
                devGrid1.DataSource = this.sessionDt1;
                devGrid1.DataBind();
            }
            else if (param[0] == "select")
            {
                CM16M_LST_2();
                devGrid1.DataBind();
            }
            else if (param[0] == "insert" && param.Length > 1)
            {
                var itemcdValues = param[1];
                string minStr = string.Empty;
                if (this.sessionDt1 == null)
                    minStr = "";
                else
                    minStr = (this.sessionDt1.Compute("MIN(F_ITEMCD)", "") ?? "").ToString();
                int minCM16MID = int.TryParse(minStr, out minCM16MID) ? minCM16MID : 0;
                var dt = CM16M_LST_1(itemcdValues);
                if (dt != null)
                {
                    this.sessionDt1.Merge(dt);
                }
                devGrid1.DataSource = this.sessionDt1;
                devGrid1.DataBind();
            }
            else if (param[0] == "delete" && param.Length > 1)
            {
                var itemcdValues = param[1];//.Split(',').ToList<string>();
                var filterExpression = string.Format("F_ITEMCD NOT IN ('{0}')", itemcdValues.Replace(",", "', '"));

                DataRow[] drs = this.sessionDt1.Select(filterExpression);
                if (drs.Length > 0)
                {
                    DataSet ds = new DataSet();
                    ds.Merge(drs);
                    this.sessionDt1 = ds.Tables[0];
                }
                else
                {
                    this.sessionDt1 = null;
                }

                devGrid1.DataSource = this.sessionDt1;
                devGrid1.DataBind();
            }
            else if (param[0] == "SAVE")
            {
                string[] procResult = default(string[]);
                var lossamtvalues = param[1];

                this.CM16M_INS(lossamtvalues, out procResult);

                devGrid1.JSProperties["cpResultCode"] = procResult[0];
                devGrid1.JSProperties["cpResultMsg"] = procResult[1];
                devGrid1.JSProperties["cpResultEtc"] = "AfterSave";
            }
        }
        #endregion

        #region devGrid1_CustomColumnDisplayText
        protected void devGrid1_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        #endregion

        #region devGrid1_InitNewRow
        protected void devGrid1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }
        #endregion

        #region devGrid2_CustomCallback
        /// <summary>
        /// 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
           

            var param = e.Parameters.Split('|');
            if (param[0] == "clear")
            {
                this.sessionDt2 = null;
                devGrid2.DataSource = this.sessionDt2;
                devGrid2.DataBind();
            }
            else if (param[0] == "select")
            {
                CM10M_LST();
                devGrid2.DataBind();
            }
            else if (param[0] == "delete" && param.Length > 1)
            {
                DataSet ds = new DataSet();
                var itemcdValues = param[1];
                var filterExpression = string.Format("F_ITEMCD NOT IN ('{0}')", itemcdValues.Replace(",", "', '"));
                if (itemcdValues.Replace(",", "', '") != null && itemcdValues.Replace(",", "', '") != "")
                {
                    DataRow[] drs = this.sessionDt2.Select(filterExpression);
                    ds.Merge(drs);
                }                
                
                this.sessionDt2 = ds.Tables.Count > 0 ? ds.Tables[0] : null;

                devGrid2.DataSource = this.sessionDt2;
                devGrid2.DataBind();
            }
        }
        #endregion

        #endregion

    }
}