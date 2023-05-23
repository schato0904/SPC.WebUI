using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;
using SPC.LTRK.Biz;
using System.IO;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using SPC.SYST.Biz;
using System.Text;

namespace SPC.WebUI.Pages.LTRK.Popup
{
    public partial class LTRK0201POP01 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string sORDERGROUP = String.Empty;
        protected string sORDERDATE = String.Empty;
        DataTable dtList = new DataTable();
        DataTable dtQCD01 = new DataTable();
        DataTable dtQCD74 = new DataTable();
        DataTable dtQCD75 = new DataTable();
        DataTable dtGUBN = new DataTable();
        DataTable dtGROUP = new DataTable();
        DataTable dtUNIT = new DataTable();
        DataTable dtQPM22 = new DataTable();
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

            // 조회
            EXCEL_LST();

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
            sORDERGROUP = Request.Params.Get("code");
            sORDERDATE = Request.Params.Get("date");
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

        #region 품목 목록을 구한다
        void QCD01_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.GetQCD01_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('품목정보를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('품목정보를 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                }
                else
                {
                    dtQCD01 = ds.Tables[0].Copy();
                }
            }
        }
        #endregion

        #region 공정정보를 구한다
        void QCD74_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_LINECD", "");

                ds = biz.QCD74_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('공정정보를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('공정정보를 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                }
                else
                {
                    dtQCD74 = ds.Tables[0].Copy();
                }
            }
        }
        #endregion

        #region 설비정보를 구한다
        void QCD75_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.QCD75_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('설비정보를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('설비정보를 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                }
                else
                {
                    dtQCD75 = ds.Tables[0].Copy();
                }
            }
        }
        #endregion

        #region 단위를 구한다
        void SYCOD01_LST(string sCODEGROUP, out DataTable dtTable)
        {
            string errMsg = String.Empty;
            dtTable = null;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CODEGROUP", sCODEGROUP);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('단위정보를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('단위정보를 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                }
                else
                {
                    dtTable = ds.Tables[0].Copy();
                }
            }
        }
        #endregion

        #region 기존 등록된 작업지시목록을 구한다(중복체크용)
        void QPM22_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERDATE", sORDERDATE);

                ds = biz.QPM22_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('작업지시정보를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else
            {
                if (!bExistsDataSetWhitoutCount(ds))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시정보를 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                }
                else
                {
                    dtQPM22 = ds.Tables[0].Copy();
                }
            }
        }
        #endregion

        #region 조회
        void EXCEL_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", sORDERDATE);
                oParamDic.Add("F_EDDT", sORDERDATE);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                ds = biz.QPM21_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('작업지시 마스터 데이터를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else if (!bExistsDataSet(ds))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시 마스터 데이터를 구하는 중 장애가 발생하였습니다.\\r데이터가 없습니다');", true);
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                srcF_ORDERDATE.Text = dr["F_ORDERDATE"].ToString();
                srcF_INSDT.Text = Convert.ToDateTime(dr["F_INSDT"]).ToString("yyyy-MM-dd HH:mm:ss");
                srcDATA_ORIGIN_NAME.Text = dr["DATA_ORIGIN_NAME"].ToString();
                srcF_USERNM.Text = dr["F_USERNM"].ToString();
                hidF_STATUS.Text = dr["F_STATUS"].ToString();
                switch (hidF_STATUS.Text)
                {
                    case "0":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Red;
                        srcF_STATUS.Text = "등록취소";
                        break;
                    case "1":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Blue;
                        srcF_STATUS.Text = "등록완료";
                        break;
                    case "2":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Red;
                        srcF_STATUS.Text = "등록전취소";
                        break;
                    case "8":
                        srcF_STATUS.ForeColor = System.Drawing.Color.Orange;
                        srcF_STATUS.Text = "임시등록";
                        break;
                    case "9":
                        srcF_STATUS.Text = "등록중";
                        break;
                }

                // 엑셀로드
                string sPath = String.Concat(String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), gsCOMPCD, dr["DATA_GBN"]), dr["DATA_NAME"].ToString());

                if (!File.Exists(sPath))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시 마스터 데이터를 구하는 중 장애가 발생하였습니다.\\r엑셀파일이 없습니다');", true);
                }
                else
                {
                    DataSet dsExcel = UF.Excels.RetrieveExcelFile(sPath, new string[] { "작업지시" });

                    if (!bExistsDataSet(dsExcel))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시 마스터를 데이터 구하는 중 장애가 발생하였습니다.\\r엑셀파일에 데이터가 없습니다');", true);
                    }
                    else
                    {
                        dtList.Columns.Add("F_IDX", typeof(int));
                        dtList.Columns.Add("F_RESULT", typeof(bool));
                        dtList.Columns.Add("F_RESULTMSG", typeof(string));
                        dtList.Columns.Add("F_SINGLE", typeof(bool));
                        dtList.Columns.Add("F_ITEMCD", typeof(string));
                        dtList.Columns.Add("F_ITEMNM", typeof(string));
                        dtList.Columns.Add("F_GUBN", typeof(string));
                        dtList.Columns.Add("F_GROUP", typeof(string));
                        dtList.Columns.Add("F_WORKCD", typeof(string));
                        dtList.Columns.Add("F_WORKNM", typeof(string));
                        dtList.Columns.Add("F_EQUIPCD", typeof(string));
                        dtList.Columns.Add("F_EQUIPNM", typeof(string));
                        dtList.Columns.Add("F_ORDERCNT", typeof(decimal));
                        dtList.Columns.Add("F_UNITNM", typeof(string));

                        // 품목 목록을 구한다
                        QCD01_LST();

                        // 공정정보를 구한다
                        QCD74_LST();

                        // 설비정보를 구한다
                        QCD75_LST();

                        // 품목구분을 구한다
                        SYCOD01_LST("24", out dtGUBN);

                        // 단위를 구한다
                        SYCOD01_LST("23", out dtUNIT);

                        // 기존 등록된 작업지시목록을 구한다(중복체크용)
                        QPM22_LST();

                        DataRow[] drTemps = null;
                        string sITEMCD = String.Empty;
                        string sWORKCD = String.Empty;
                        string sUNITCD = String.Empty;
                        string sGUBN = String.Empty;
                        string sGROUP = String.Empty;
                        string sEQUIPCD = String.Empty;
                        int idx = 0;
                        StringBuilder sbResult = null;

                        foreach (DataRow drs in dsExcel.Tables[0].Rows)
                        {
                            sbResult = new StringBuilder();

                            DataRow dtRow = dtList.NewRow();
                            // 순번
                            dtRow["F_IDX"] = idx++;
                            // 품목
                            sITEMCD = drs["품번"].ToString();
                            dtRow["F_ITEMCD"] = sITEMCD;
                            drTemps = dtQCD01.Select(String.Format("F_ITEMCD='{0}'", dtRow["F_ITEMCD"]));
                            if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                            sbResult.Append(drTemps.Length > 0 ? "" : "품목없음");
                            dtRow["F_ITEMNM"] = drTemps.Length > 0 ? drTemps[0]["F_ITEMNM"].ToString() : "품목오류";

                            sUNITCD = drTemps.Length > 0 ? drTemps[0]["F_UNIT"].ToString() : "";
                            sGUBN = drTemps.Length > 0 ? drTemps[0]["F_GUBN"].ToString() : "";
                            sGROUP = drTemps.Length > 0 ? drTemps[0]["F_GROUP"].ToString() : "";
                            
                            // 품목구분
                            if (!String.IsNullOrEmpty(sGUBN))
                            {
                                drTemps = dtGUBN.Select(String.Format("F_CODE='{0}'", sGUBN));
                                if (drTemps.Length > 0)
                                    dtRow["F_GUBN"] = drTemps[0]["F_CODENM"].ToString();
                                else
                                {
                                    dtRow["F_GUBN"] = "품목구분없음";
                                    if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                                    sbResult.Append(drTemps.Length > 0 ? "" : "품목구분없음");
                                }
                            }
                            else
                            {
                                dtRow["F_GUBN"] = "";
                            }

                            // 품목그룹
                            if (!String.IsNullOrEmpty(sGROUP))
                            {
                                sGROUP = GetCommonCodeText(sGROUP);
                                if (!String.IsNullOrEmpty(sGROUP))
                                    dtRow["F_GROUP"] = sGROUP;
                                else
                                {
                                    dtRow["F_GROUP"] = "품목구분오류";
                                    if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                                    sbResult.Append(drTemps.Length > 0 ? "" : "품목그룹없음");
                                }
                            }
                            else
                            {
                                dtRow["F_GROUP"] = "";
                            }

                            // 공정
                            sWORKCD = drs["공정"].ToString().Substring(1, 8);
                            dtRow["F_WORKCD"] = sWORKCD;
                            drTemps = dtQCD74.Select(String.Format("F_WORKCD='{0}'", dtRow["F_WORKCD"]));
                            if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                            sbResult.Append(drTemps.Length > 0 ? "" : "공정없음");
                            dtRow["F_WORKNM"] = drTemps.Length > 0 ? drTemps[0]["F_WORKNM"].ToString() : "공정오류";
                            
                            // 설비
                            sEQUIPCD = drTemps.Length > 0 ? drTemps[0]["F_EQUIPCD"].ToString() : "";
                            if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                            sbResult.Append(drTemps.Length > 0 ? "" : "설비없음");
                            dtRow["F_EQUIPCD"] = !String.IsNullOrEmpty(sEQUIPCD) ? sEQUIPCD : "설비오류";
                            drTemps = dtQCD75.Select(String.Format("F_EQUIPCD='{0}'", sEQUIPCD));
                            dtRow["F_EQUIPNM"] = drTemps.Length > 0 ? drTemps[0]["F_EQUIPNM"].ToString() : "설비오류";
                            
                            // 지시수량
                            dtRow["F_ORDERCNT"] = Convert.ToDecimal(drs["지시수량"]);

                            // 단위
                            if (!String.IsNullOrEmpty(sUNITCD))
                            {
                                drTemps = dtUNIT.Select(String.Format("F_CODE='{0}'", sUNITCD));
                                if (drTemps.Length > 0)
                                    dtRow["F_UNITNM"] = drTemps[0]["F_CODENM"].ToString();
                                else
                                {
                                    dtRow["F_UNITNM"] = "품목단위오류";
                                    if (sbResult.Length > 0) sbResult.Append(Environment.NewLine);
                                    sbResult.Append(drTemps.Length > 0 ? "" : "품목단위없음");
                                }
                            }
                            else
                            {
                                dtRow["F_UNITNM"] = "";
                            }
                            dtRow["F_RESULT"] = sbResult.Length <= 0;
                            dtRow["F_RESULTMSG"] = sbResult.ToString();

                            // 중복체크
                            drTemps = dtQPM22.Select(String.Format("F_ITEMCD='{0}' AND F_WORKCD='{1}' AND F_STATUS IN('AAE601', 'AAE602', 'AAE603', 'AAE604') AND F_ISDELETE=0", sITEMCD, sWORKCD));
                            dtRow["F_SINGLE"] = drTemps.Length <= 0;

                            dtList.Rows.Add(dtRow);
                        }

                        devGrid.DataSource = dtList;
                    }
                }
            }
        }
        #endregion

        #region 작업지시저장
        bool PROC_QPM22_BATCH(out string[] procResult)
        {
            bool bExecute = false;
            string errMsg = String.Empty;
            procResult = new string[] { "2", "Unknown Error" };
            int nOKCnt = 0;

            foreach (DataRow dr in dtList.Rows)
            {
                if (Convert.ToBoolean(dr["F_RESULT"]))
                {
                    nOKCnt++;
                }
            }

            if (nOKCnt > 0)
            {
                List<string> oSPs = new List<string>();
                List<object> oParameters = new List<object>();

                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                oParamDic.Add("F_ORDERDATE", sORDERDATE);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_USER", gsUSERID);

                oSPs.Add("USP_QPM21_STATUS_CHG");
                oParameters.Add(oParamDic);

                foreach (DataRow dr in dtList.Rows)
                {
                    if (Convert.ToBoolean(dr["F_RESULT"]))
                    {
                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                        oParamDic.Add("F_ORDERDATE", sORDERDATE);
                        oParamDic.Add("F_ITEMCD", dr["F_ITEMCD"].ToString());
                        oParamDic.Add("F_WORKCD", dr["F_WORKCD"].ToString());
                        oParamDic.Add("F_ORDERCNT", dr["F_ORDERCNT"].ToString());
                        oParamDic.Add("F_STATUS", "AAE603");
                        oParamDic.Add("F_USER", gsUSERID);

                        oSPs.Add("USP_QPM22_INS");
                        oParameters.Add(oParamDic);
                    }
                }

                using (LTRKBiz biz = new LTRKBiz())
                {
                    bExecute = biz.PROC_QPM22_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", errMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                }
            }
            else
            {
                if (!QPM21_STATUS_CHG("2", out errMsg))
                {
                    bExecute = false;
                    procResult = new string[] { "0", String.Format("저장 할 대상이 없어 취소 처리 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", errMsg) };
                }
                else
                {
                    bExecute = true;
                    procResult = new string[] { "1", "저장할 대상이 존재하지 않습니다. 전체 취소 처리되었습니다" };
                }
            }

            return bExecute;
        }
        #endregion

        #region 작업지시그룹상태변경
        bool QPM21_STATUS_CHG(string sSTATUS, out string errMsg)
        {
            bool bExecute = false;

            errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                oParamDic.Add("F_ORDERDATE", sORDERDATE);
                oParamDic.Add("F_STATUS", sSTATUS);
                oParamDic.Add("F_USER", gsUSERID);

                bExecute = biz.QPM21_STATUS_CHG(oParamDic, out errMsg);
            }

            return bExecute;
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
            // 조회
            EXCEL_LST();
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
            DevExpressLib.GetBoolString(new string[] { "F_RESULT" }, "검증성공", "검증실패", e);
            DevExpressLib.GetBoolString(new string[] { "F_SINGLE" }, "중복안됨", "중복됨", e);
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
            if (e.VisibleIndex < 0) return;

            if (e.DataColumn.FieldName.Equals("F_RESULT") && !Convert.ToBoolean(e.CellValue))
            {
                e.Cell.ToolTip = devGrid.GetRowValues(e.VisibleIndex, "F_RESULTMSG").ToString();
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
            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                DataRow drTemp = null;

                foreach (var Value in e.UpdateValues)
                {
                    drTemp = dtList.Select(String.Format("F_IDX={0}", Value.Keys["F_IDX"]))[0];
                    drTemp["F_ORDERCNT"] = Convert.ToDecimal(Value.NewValues["F_ORDERCNT"]);
                }
            }
            #endregion

            // 작업지시저장
            bool bExecute = PROC_QPM22_BATCH(out procResult);

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];

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

            if (paramDic["ACTION"] == "CONFIRM")
            {
                if (!PROC_QPM22_BATCH(out procResult))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = procResult[1];
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = procResult[1];
                }
                e.Result = jss.Serialize(result);
            }
            else if (paramDic["ACTION"] == "CANCEL")
            {
                if (!QPM21_STATUS_CHG("0", out errMsg))
                {
                    ISOK = false;
                    result["ISOK"] = ISOK;
                    result["MSG"] = errMsg;
                }
                else
                {
                    ISOK = true;
                    result["ISOK"] = ISOK;
                    result["MSG"] = "취소되었습니다.";
                }
                e.Result = jss.Serialize(result);
            }
        }
        #endregion

        #endregion
    }
}