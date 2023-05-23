using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.MEAS.Biz;

using DevExpress.Web;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS2004 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 프로퍼티
        DataTable Cached_Grid1
        {
            get { return Session["MEAS2004_Grid1"] as DataTable; }
            set { Session["MEAS2004_Grid1"] = value; }
        }
        #endregion

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
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
            //string currmonth_yn = "N";
            string oSetParam = Request["oSetParam"] ?? "";
            Dictionary<string, string> dicSetParam = null;

            // 파라미터 처리
            if (!string.IsNullOrEmpty(oSetParam))
            {
                dicSetParam = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<Dictionary<string, string>>(oSetParam);

                // 첫 로드시에만 처리
                if (!IsPostBack)
                {
                    if (dicSetParam.ContainsKey("CURRMONTH_YN") && dicSetParam["CURRMONTH_YN"] == "Y")
                    {
                        DateTime currmonth_1stDay = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-01"));
                        DateTime fromdt = currmonth_1stDay;
                        if (dicSetParam.ContainsKey("FROMFIXDT"))
                        {
                            fromdt = DateTime.Parse(dicSetParam["FROMFIXDT"]);
                        }

                        // ucF_FIXPLANDT.SetValue(fromdt, currmonth_1stDay.AddMonths(1).AddDays(-1));                      
                        //ucF_REQDT.SetValue(DateTime.Today, DateTime.Today);
                        this.srcF_RECVYNNM.Text = "";
                        this.srcF_REGUSER.Text = this.gsUSERNM;

                        devGrid_CustomCallback(this.devGrid1, new ASPxGridViewCustomCallbackEventArgs(""));
                    }
                }
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
        {
        }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            //     SSA1	계측기분류코드 F_EQUIPDIVCD
            GetCommonCodeList("SS01", schF_EQUIPDIVCD);
            //     SSA3	계측기구분코드 F_EQUIPTYPECD
            GetCommonCodeList("SS03", schF_EQUIPTYPECD);
            //     SSA4	검교정구분코드 F_FIXTYPECD
            GetCommonCodeList("SS04", schF_FIXTYPECD);
            //     SSA6	교정기관코드 F_FIXGRPCD
            GetCommonCodeList("SS06", schF_FIXGRPCD);
            GetTeamCodeList(schF_TEAMCD);
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

        #region 공통, 팀, 반, 공정에 대한 코드, 분류를 구한다

        void GetCommonCodeList(string groupCD, DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            if (!String.IsNullOrEmpty(groupCD))
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_GROUPCD", groupCD);
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);

                    ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                }
            }
            else
                ds = null;

            comboBox.TextField = "F_COMMNM";
            comboBox.ValueField = "F_COMMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetTeamCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.MEAS1001_TEAM_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_TEAMNM";
            comboBox.ValueField = "F_TEAMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetBancCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", "");
                oParamDic.Add("F_TEAMCD", (schF_TEAMCD.Value ?? "").ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS1001_BAN_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_BANNM";
            comboBox.ValueField = "F_BANCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        #endregion

        #region 그리드 목록 조회

        /// <summary>
        /// 검교정 신청 의뢰 목록 및 의뢰 신청 내역 상세 정보
        /// </summary>
        /// <param name="FIELDS"></param>
        DataSet MEAS2004_MST_LST(out string errMsg, string F_REQNO = "")
        {
            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_FROMDT", (string.IsNullOrEmpty(F_REQNO)) ? schF_REQDT_FROM.Text : "");
                oParamDic.Add("F_TODT", (string.IsNullOrEmpty(F_REQNO)) ? schF_REQDT_TO.Text : "");
                oParamDic.Add("F_REQNO", (string.IsNullOrEmpty(F_REQNO)) ?  schF_REQNO.Text.Trim() : F_REQNO);

                return biz.MEAS2004_MST_LST(oParamDic, out errMsg);
            }
        }

        /// <summary>
        /// 검교정 신청 내역 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        DataSet MEAS2004_DTL_LST(out string errMsg, string F_REQNO = "")
        {
            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_FROMDT", (string.IsNullOrEmpty(F_REQNO)) ? srcF_FIXPLANDT_FROM.Text : "");
                oParamDic.Add("F_TODT", (string.IsNullOrEmpty(F_REQNO)) ? srcF_FIXPLANDT_TO.Text : "");
                oParamDic.Add("F_EQUIPDIVCD", (string.IsNullOrEmpty(F_REQNO)) ? (schF_EQUIPDIVCD.Value ?? "").ToString() : "");
                oParamDic.Add("F_EQUIPTYPECD", (string.IsNullOrEmpty(F_REQNO)) ? (schF_EQUIPTYPECD.Value ?? "").ToString() : "");
                oParamDic.Add("F_FIXTYPECD", (string.IsNullOrEmpty(F_REQNO)) ? (schF_FIXTYPECD.Value ?? "").ToString() : "");
                oParamDic.Add("F_FIXGRPCD", (string.IsNullOrEmpty(F_REQNO)) ? (schF_FIXGRPCD.Value ?? "").ToString() : "");
                oParamDic.Add("F_FACTCD", (string.IsNullOrEmpty(F_REQNO)) ? ucFact.GetFactCD() : "");
                oParamDic.Add("F_TEAMCD", (string.IsNullOrEmpty(F_REQNO)) ? (schF_TEAMCD.Value ?? "").ToString() : "");
                oParamDic.Add("F_REQNO", (string.IsNullOrEmpty(F_REQNO)) ? "" : F_REQNO);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                return biz.MEAS2004_DTL_LST(oParamDic, out errMsg);
            }
        }

        /// <summary>
        /// 검교정 신청 의뢰 목록
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2004_LST()
        {
            string errMsg = String.Empty;

            devGrid2.DataSource = MEAS2004_MST_LST(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else {
                devGrid2.JSProperties["cpResultCode"] = "1";
                devGrid2.JSProperties["cpResultMsg"] = "";
            }
        }

        /// <summary>
        /// 검교정 신청 내역 상세 정보
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2004_MST(string F_REQNO = "")
        {
            string errMsg = String.Empty;

            ds = MEAS2004_MST_LST(out errMsg, F_REQNO);

            DataTable dt = ds.Tables[0];
            if(dt.Rows.Count > 0) {
                this.srcF_REQNO.Text = dt.Rows[0]["F_REQNO"].ToString();
                this.srcF_REQDT.Text = dt.Rows[0]["F_REQDT"].ToString();
                this.srcF_REQCNT.Text = dt.Rows[0]["F_REQCNT"].ToString();
                this.srcF_REGUSER.Text = dt.Rows[0]["F_REGUSER"].ToString();
                this.srcF_REMARK.Text = dt.Rows[0]["F_REMARK"].ToString();
                this.srcF_RECVYNNM.Text = dt.Rows[0]["F_RECVYNNM"].ToString();
                this.srcF_ENDCNT.Text = dt.Rows[0]["F_ENDCNT"].ToString();
                this.srcF_REMNCNT.Text = dt.Rows[0]["F_REMNCNT"].ToString();
            }
        }
        
        
        /// <summary>
        /// 검교정 신청 내역 상세 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2004_DTL(string F_REQNO = "")
        {
            string errMsg = String.Empty;

            var ds = MEAS2004_DTL_LST(out errMsg, F_REQNO);
            DataTable dt = ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : null;
            this.Cached_Grid1 = dt;
            devGrid1.DataSource = dt;

            if (!String.IsNullOrEmpty(errMsg)) {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else {
                devGrid1.JSProperties["cpResultCode"] = (F_REQNO == "") ? "1" : "2";
                devGrid1.JSProperties["cpResultMsg"] = "";
            }
        }

        void CreateParameter(bool bInsert)
        {
            oParamDic.Clear();

            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_REQDT", srcF_REQDT.Text);
            oParamDic.Add("F_REGUSER", srcF_REGUSER.Text.Trim());
            oParamDic.Add("F_REMARK", srcF_REMARK.Text.Trim());
            
            if (bInsert)
            {
                oParamDic.Add("F_REQCNT", srcF_REQCNT.Text.Trim());
                oParamDic.Add("F_MS01MID_LST", hidF_MS01MID_LST.Text.Trim());
                oParamDic.Add("F_REQNO", "OUTPUT");
            }
            else
            {
                oParamDic.Add("F_REQNO", srcF_REQNO.Text.Trim());
            }            
        }

        /// <summary>
        /// 검교정 신청 내역 상세 저장
        /// </summary>
        /// <param name="FIELDS"></param>
        bool MEAS2004_INS(out string pkey)
        {
            bool bExecute = false;
            var errMsg = string.Empty;

            CreateParameter(true);

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.MEAS2004_INS(oParamDic, out errMsg, out pkey);
            }

            return bExecute;
        }

        /// <summary>
        /// 검교정 신청 내역 상세 수정
        /// </summary>
        /// <param name="FIELDS"></param>
        bool MEAS2004_UPD(out string pkey, out string errMsg)
        {
            bool bExecute = false;

            CreateParameter(false);

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.MEAS2004_UPD(oParamDic, out errMsg);
            }

            pkey = oParamDic["F_REQNO"];

            return bExecute;
        }

        /// <summary>
        /// 검교정 신청 내역 상세 삭제
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2004_DEL(out string[] procResult)
        {
            bool bExecute = false;
            string errMsg = string.Empty;
            string F_REQNO = srcF_REQNO.Text;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_REQNO", F_REQNO);

                bExecute = biz.MEAS2004_DEL(oParamDic, out errMsg);
            }

            string key = string.Empty;
            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n\r\n" + errMsg, "" };
            }
            else
            {
                DataSet ds = MEAS2004_MST_LST(out errMsg, F_REQNO);
                key = string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? F_REQNO : string.Empty;
                procResult = new string[] { "1", "삭제가 완료되었습니다.", key };
            }
        }

        #endregion

        #endregion

        #region 사용자이벤트

        #region schF_BANCD_Callback

        protected void schF_BANCD_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            GetBancCodeList(devComboBox);
            devComboBox.SelectedIndex = 0;
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
            var result = new Dictionary<string, string>();
            var saveMode = hidPageMode.Text; // NEW:신규, EDIT:수정
            var ActionNm = new Dictionary<string, string>() { 
                { "NEW", "신규저장" }, 
                { "EDIT", "수정" }
            };

            bool bResult = false;
            string pkey = string.Empty;
            string errMsg = string.Empty;
            string actType = e.Parameter.ToUpper();

            if (actType == "SAVE")
            {
                // 저장 액션
                if (saveMode == "NEW")
                {
                    bResult = MEAS2004_INS(out pkey);
                }
                else if (saveMode == "EDIT")
                {
                    bResult = MEAS2004_UPD(out pkey, out errMsg);
                }

                if (!bResult)
                {
                    procResult = new string[] { "0", "{0} 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n\r\n" + errMsg };
                }
                else
                {
                    procResult = new string[] { "1", "{0}이 완료되었습니다." };
                }

                result["MESSAGE"] = string.Format(procResult[1], ActionNm[saveMode]);
                result["PKEY"] = pkey;
                result["TYPE"] = "AfterSave";
            }
            else if (actType == "DEL")
            {
                // 삭제 액션
                this.MEAS2004_DEL(out procResult);
                result["MESSAGE"] = procResult[1];
                result["TYPE"] = "AfterDelete";
                result["KEY"] = procResult[2];
            }
            //else if (actType.StartsWith("VIEW;"))
            //{
            //    string F_REQNO = actType.Split(';')[1];
            //    if (!string.IsNullOrEmpty(F_REQNO))
            //    {
            //        MEAS2004_MST(F_REQNO);
            //        MEAS2004_DTL(F_REQNO);
            //        devGrid1.DataBind();

            //        procResult = new string[] { "1" };
            //        result["TYPE"] = "AfterView";
            //    }
            //}

            result["CODE"] = procResult[0];
            e.Result = this.SerializeJSON(result);
        }
        #endregion

        #region cbpContent_Callback

        protected void cbpContent_Callback(object sender, CallbackEventArgsBase e)
        {
            string F_REQNO = e.Parameter;

            if (!string.IsNullOrEmpty(F_REQNO))
            {
                MEAS2004_MST(F_REQNO);
            }
        }

        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// 계측기보유현황 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = (sender) as ASPxGridView;

            var pkey = string.Empty;
            
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                if (e.Parameters.IndexOf("VIEW") >= 0)
                {
                    pkey = e.Parameters.Split(';')[1];
                }

                if (e.Parameters.Equals("clear"))
                {
                    grid.DataSourceID = null;
                    grid.DataSource = null;
                    grid.DataBind();
                    //this.devGrid1.DataSourceID = null;
                    //this.devGrid1.DataSource = null;
                    //this.devGrid1.DataBind();
                    //this.devGrid2.DataSourceID = null;
                    //this.devGrid2.DataSource = null;
                    //this.devGrid2.DataBind();

                    grid.JSProperties["cpResultCode"] = "1";
                    grid.JSProperties["cpResultMsg"] = "";

                    return;
                }
            }

            if (grid.ClientInstanceName.Equals("devGrid2"))
            {
                MEAS2004_LST();
            }
            else {
                MEAS2004_DTL(pkey);
            }

            grid.DataBind();
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            MEAS2004_LST();
            devGridExporter.GridViewID = "devGrid1";
            devGridExporter.WriteXlsToResponse(String.Format("검교정신청목록_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        #endregion

        #region btnExportResult_Click
        /// <summary>
        /// btnExportResult_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExportResult_Click(object sender, EventArgs e)
        {

            devGrid1.DataSource = this.Cached_Grid1;
            devGridExporter.GridViewID = "devGrid1";
            devGridExporter.WriteXlsToResponse(String.Format("검교정신청대상_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });     

            //string F_REQNO = srcF_REQNO.Text.Trim();

            //if (!string.IsNullOrEmpty(F_REQNO))
            //{
            //    MEAS2004_DTL(F_REQNO);
            //    devGridExporter.GridViewID = "devGrid1";
            //    devGridExporter.WriteXlsToResponse(String.Format("검교정신청대상_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });                
            //}
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<BR/>", " ");
            }
        }
        #endregion

        #endregion

        protected void devGrid1_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;

            if (grid.VisibleRowCount == 0) return;

            for (int i = 0; i < grid.VisibleRowCount; i++) {

                if (grid.GetRowValues(i, "F_SEQNO") != DBNull.Value && grid.GetRowValues(i, "F_SEQNO") != null)
                {   
                    grid.Selection.SetSelection(i, true);
                }
                else {
                    grid.Selection.SetSelection(i, false);
                }
            }

            GridViewCommandColumn colunm = grid.Columns[0] as GridViewCommandColumn;

            if (grid.Selection.Count == grid.VisibleRowCount)
            {
                colunm.ShowSelectCheckbox = false;
                ASPxCheckBox chkAll = grid.FindHeaderTemplateControl(grid.Columns[0], "chkAll") as ASPxCheckBox;
                if (chkAll != null)
                {
                    chkAll.Checked = true;
                    chkAll.Enabled = false;
                }
            }
            else {
                colunm.ShowSelectCheckbox = true;
            }
        }
    }
}