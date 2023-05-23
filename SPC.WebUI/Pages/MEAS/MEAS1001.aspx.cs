using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

using SPC.WebUI.Common;
using SPC.MEAS.Biz;
using SPC.Common.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS1001 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] _fiedList = new string[] { "F_MS01MID", "F_EQUIPNO", "F_EQUIPNM", "F_PRODNO",
                                            "F_STAND", "F_MODEL", "F_INDT", "F_PICNO",
                                            "F_IMGATTFILENO", "F_ATTFILENO", "F_UNQNO","F_FIXDIVCD",
                                            "F_ETC", "F_USER", "F_FIXNO", "F_LASTFIXDT",
                                            "F_TERMMONTH", "F_FIXPLANDT", "F_REMARK", "F_PART_INFO",
                                            "F_EQUIPDIVCD", "F_MAKERCD", "F_GRADECD", "F_EQUIPTYPECD",
                                            "F_FACTCD", "F_TEAMCD", "F_BANCD", "F_PROCCD",
                                            "F_STATUSCD", "F_ABNORMALCD", "F_JUDGECD", "F_FIXTYPECD",
                                            "F_FIXGRPCD", "F_ATTFILECNT", "F_IMGATTFILECNT", "F_PRICE", "F_MEMO" };

        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

        public string FieldList {
            get {
                return string.Join(";", _fiedList);
            }
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
            // Request
            GetRequest();

            if (!IsCallback)
            {
                //rdoSTATUS.SelectedItem = rdoSTATUS.Items.FindByValue("1");
            }

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
            string errMsg = String.Empty;

            srcF_COMPCD.Text = gsCOMPCD;

            //     SSA1	계측기분류코드 F_EQUIPDIVCD
            GetCommonCodeList("SS01", srcF_EQUIPDIVCD);

            //      AAFB 검교정판정구분 코드 F_JUDGECD
            GetCommonCodeList("SS02", srcF_JUDGECD);

            //     AAFC 상태구분코드 F_STATUSCD
            GetCommonCodeList("SS09", srcF_STATUSCD);
            
            //     AAFD 이상처리구분코드 F_ABNORMALCD
            GetCommonCodeList("SS10", srcF_ABNORMALCD);

            //     SSA2	계측기등급코드 F_GRADECD -> 측정단위
            GetCommonCodeList("SS08", srcF_GRADECD);

            //     SSA3	계측기구분코드 F_EQUIPTYPECD
            GetCommonCodeList("SS03", srcF_EQUIPTYPECD);

            //     SSA4	검교정구분코드 F_FIXTYPECD
            GetCommonCodeList("SS04", srcF_FIXTYPECD);

            //     SSA5	교정분야코드 F_FIXDIVCD
            GetCommonCodeList("SS05", srcF_FIXDIVCD);

            //     SSA6	교정기관코드 F_FIXGRPCD
            GetCommonCodeList("SS06", srcF_FIXGRPCD);

            //     SSA8	제조회사코드 F_MAKERCD
            //GetCommonCodeList("SS08", srcF_MAKERCD);

            // 검색조건의 계측기 분류를 셋팅.
            GetCommonCodeList("SS01", srcF_EQUIPDIVCD_2, "전체"); 

            // 검색조건의 상태구분 분류를 셋팅.
            GetCommonCodeList("SS09", srcF_STATUSCD_2, "전체");

            //  검색조건의 제조회사코드 F_MAKERCD
            //GetCommonCodeList("SSA8", srcF_MAKERCD_2, "전체");

            // 검색조건의 팀 분류를 셋팅.
            GetTeamCodeList(srcF_TEAMCD_2, "", "전체");            
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
            // 파라미터 처리
            if (!IsPostBack && !string.IsNullOrEmpty(Request["oSetParam"]))
            {
                var pm = this.DeserializeJSON(Request["oSetParam"]);
                srcF_MS01MID.Text = pm["F_MS01MID"];

                int f_MS01MID = -1;

                if (Int32.TryParse(srcF_MS01MID.Text, out f_MS01MID))
                {
                    MEAS1001_INF(f_MS01MID);
                    hidPageMode.Text = "EDIT";
                }
            }
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공통, 팀, 반, 공정에 대한 코드, 분류를 구한다

        void GetCommonCodeList(string groupCD, DevExpress.Web.ASPxComboBox comboBox, string firstText = "선택", string param1="")
        {
            string errMsg = String.Empty;

            if (!String.IsNullOrEmpty(groupCD))
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_GROUPCD", groupCD);
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);
                    if (param1 != "") {
                        oParamDic.Add("F_PARAM01", param1);
                    }
                    ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                }
            }
            else
                ds = null;

            comboBox.TextField = "F_COMMNM";
            comboBox.ValueField = "F_COMMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem(firstText, ""));
            comboBox.SelectedIndex = 0;
        }

        void GetTeamCodeList(DevExpress.Web.ASPxComboBox comboBox, string factCd, string firstText = "선택")
        {
            string errMsg = String.Empty;

            if (comboBox == srcF_TEAMCD && string.IsNullOrEmpty(factCd))
            {
                ds = null;
            }
            else
            {
                using (MEASBiz biz = new MEASBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);

                    ds = biz.MEAS1001_TEAM_LST(oParamDic, out errMsg);
                }
            }

            comboBox.TextField = "F_TEAMNM";
            comboBox.ValueField = "F_TEAMCD";
            comboBox.DataSource = ds;            
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem(firstText, ""));
        }

        #endregion

        #region 계측기 정보관리 목록/상세를 가져온다.

        void MEAS1001_LST()
        {
            string errMsg = string.Empty;
            
            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_EQUIPDIVCD", (srcF_EQUIPDIVCD_2.Value ?? "").ToString());
                oParamDic.Add("F_EQUIPNO", schF_EQUIPNO_2.Text);
                oParamDic.Add("F_EQUIPNM", schF_EQUIPNM_2.Text);
                oParamDic.Add("F_STATUSCD", (srcF_STATUSCD_2.Value ?? "").ToString());
                oParamDic.Add("F_MAKERCD", srcF_MAKERCD_2.Text);
                oParamDic.Add("F_TEAMCD", (srcF_TEAMCD_2.Value ?? "").ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                devGrid.DataSource = biz.MEAS1001_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
        }

        void MEAS1001_INF(int F_MS01MID)
        {
            string errMsg = string.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", F_MS01MID.ToString());

                ds = biz.MEAS1001_INF(oParamDic, out errMsg);
            }

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0) {
                srcF_MS01MID.Text = dt.Rows[0]["F_MS01MID"].ToString();
                srcF_PART_INFO.Text = dt.Rows[0]["F_PART_INFO"].ToString();
                srcF_COMPCD.Text = dt.Rows[0]["F_COMPCD"].ToString();
                srcF_EQUIPNO.Text = dt.Rows[0]["F_EQUIPNO"].ToString();
                srcF_EQUIPNM.Text = dt.Rows[0]["F_EQUIPNM"].ToString();
                srcF_MEMO.Text = dt.Rows[0]["F_MEMO"].ToString();

                //     SSA1	계측기분류코드 F_EQUIPDIVCD
                GetCommonCodeList("SS01", srcF_EQUIPDIVCD);
                srcF_EQUIPDIVCD.Value = dt.Rows[0]["F_EQUIPDIVCD"].ToString();

                srcF_PRODNO.Text = dt.Rows[0]["F_PRODNO"].ToString();
                srcF_STAND.Text = dt.Rows[0]["F_STAND"].ToString();
                srcF_MODEL.Text = dt.Rows[0]["F_MODEL"].ToString();

                //     SSA8	제조회사코드 F_MAKERCD
                //GetCommonCodeList("SSA8", srcF_MAKERCD);
                srcF_MAKERCD.Text = dt.Rows[0]["F_MAKERCD"].ToString();

                srcF_INDT.Text = dt.Rows[0]["F_INDT"].ToString();
                srcF_PICNO.Text = dt.Rows[0]["F_PICNO"].ToString();

                //     SSA2	계측기등급코드 F_GRADECD -> 측정단위
                GetCommonCodeList("SS08", srcF_GRADECD);
                srcF_GRADECD.Value = dt.Rows[0]["F_GRADECD"].ToString();

                //     SSA3	계측기구분코드 F_EQUIPTYPECD
                GetCommonCodeList("SS03", srcF_EQUIPTYPECD);
                srcF_EQUIPTYPECD.Value = dt.Rows[0]["F_EQUIPTYPECD"].ToString();

                srcF_IMGATTFILENO.Text = dt.Rows[0]["F_IMGATTFILENO"].ToString();
                srcF_IMGATTFILECNT.Text = dt.Rows[0]["F_IMGATTFILECNT"].ToString();
                srcF_ATTFILENO.Text = dt.Rows[0]["F_ATTFILENO"].ToString();
                srcF_ATTFILECNT.Text = dt.Rows[0]["F_ATTFILECNT"].ToString();
	           
                GetTeamCodeList(srcF_TEAMCD, dt.Rows[0]["F_FACTCD"].ToString());
                srcF_TEAMCD.Value = dt.Rows[0]["F_TEAMCD"].ToString();
                srcF_TEAMCD.ClientEnabled = false;

                srcF_ETC.Text = dt.Rows[0]["F_ETC"].ToString();

                srcF_USER.Text = dt.Rows[0]["F_USER"].ToString();
                srcF_USER.ClientEnabled = false;

                //     AAFC 상태구분코드 F_STATUSCD
                GetCommonCodeList("SS09", srcF_STATUSCD);
                srcF_STATUSCD.Value = dt.Rows[0]["F_STATUSCD"].ToString();
                srcF_STATUSCD.ClientEnabled = false;

                //     AAFD 이상처리구분코드 F_ABNORMALCD
                GetCommonCodeList("SS10", srcF_ABNORMALCD);
                srcF_ABNORMALCD.Value = dt.Rows[0]["F_ABNORMALCD"].ToString();
                srcF_ABNORMALCD.ClientEnabled = false;

                //      AAFB 검교정판정구분 코드 F_JUDGECD
                GetCommonCodeList("SS02", srcF_JUDGECD);
                srcF_JUDGECD.Value = dt.Rows[0]["F_JUDGECD"].ToString();

                //     SSA4	검교정구분코드 F_FIXTYPECD
                GetCommonCodeList("SS04", srcF_FIXTYPECD);
                srcF_FIXTYPECD.Value = dt.Rows[0]["F_FIXTYPECD"].ToString();

                //     SSA5	교정분야코드 F_FIXDIVCD
                GetCommonCodeList("SS05", srcF_FIXDIVCD);
                srcF_FIXDIVCD.Value = dt.Rows[0]["F_FIXDIVCD"].ToString();

                srcF_FIXNO.Text = dt.Rows[0]["F_FIXNO"].ToString();

                //     SSA6	교정기관코드 F_FIXGRPCD
                GetCommonCodeList("SS06", srcF_FIXGRPCD);
                srcF_FIXGRPCD.Value = dt.Rows[0]["F_FIXGRPCD"].ToString();

                srcF_LASTFIXDT.Text = dt.Rows[0]["F_LASTFIXDT"].ToString();
                srcF_TERMMONTH.Text = dt.Rows[0]["F_TERMMONTH"].ToString();
                srcF_FIXPLANDT.Text = dt.Rows[0]["F_FIXPLANDT"].ToString();
                srcF_REMARK.Text = dt.Rows[0]["F_REMARK"].ToString();
                srcF_PRICE.Text = dt.Rows[0]["F_PRICE"].ToString();
            }
        }

        void CreateParameter(bool bInsert) {
            oParamDic.Clear();
            var fieldList = this.GetPanelData(cbpContent);            
            _fiedList.Where(x => fieldList.ContainsKey(x)).ToList().ForEach(x => oParamDic.Add(x, fieldList[x]));

            if (oParamDic.ContainsKey("F_COMPCD"))
            {
                oParamDic["F_COMPCD"] = gsCOMPCD;
            }
            else
            {
                oParamDic.Add("F_COMPCD", gsCOMPCD);
            }

            if (bInsert)
            {
                if (oParamDic.ContainsKey("F_MS01MID"))
                {
                    oParamDic["F_MS01MID"] = "OUTPUT";
                }
                else
                {
                    oParamDic.Add("F_MS01MID", "OUTPUT");
                }
            }

            if (oParamDic.ContainsKey("F_FACTCD"))
            {
                oParamDic["F_FACTCD"] = gsFACTCD;
            }
            else
            {
                oParamDic.Add("F_FACTCD", gsFACTCD);
            }

            if (oParamDic.ContainsKey("F_ATTFILECNT"))
            {
                oParamDic.Remove("F_ATTFILECNT");
            }

            if (oParamDic.ContainsKey("F_IMGATTFILECNT"))
            {
                oParamDic.Remove("F_IMGATTFILECNT");
            }
        }

        bool MEAS1001_INS(out string pkey)
        {
            bool bExecute = false;
            var errMsg = string.Empty;

            CreateParameter(true);

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.MEAS1001_INS(oParamDic, out errMsg);
            }
            pkey = oParamDic["F_MS01MID"];

            return bExecute;
        }

        bool MEAS1001_UPD(out string pkey)
        {
            bool bExecute = false;
            var errMsg = string.Empty;

            CreateParameter(false);

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.MEAS1001_UPD(oParamDic, out errMsg);
            }

            pkey = oParamDic["F_MS01MID"];

            return bExecute;
        }

        void MEAS1001_DEL(out string[] procResult)
        {
            bool bExecute = false;
            string errMsg = string.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", srcF_MS01MID.Text);

                bExecute = biz.MEAS1001_DEL(oParamDic, out errMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "삭제가 완료되었습니다." };
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
            
            if (e.Parameters.IndexOf("AfterSave") > 0)
            {
                pkey = e.Parameters.Split(';')[1];
                devGrid.JSProperties["cpResultEtc"] = "AfterSave";
                devGrid.JSProperties["cpResultPKEY"] = pkey;

                var visibleIndex = devGrid.FindVisibleIndexByKeyValue(int.Parse(pkey));
                devGrid.JSProperties["cpResultIndex"] = visibleIndex.ToString();
            }

            //devGrid.DataBind();
            
        }
        #endregion

        #region srcF_TEAMCD_Callback
        protected void srcF_TEAMCD_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            GetTeamCodeList(devComboBox, gsFACTCD);
            devComboBox.SelectedIndex = 0;
        } 
        #endregion

        #region srcF_EQUIPDIVCD_Callback
        protected void srcF_EQUIPDIVCD_Callback(object sender, CallbackEventArgsBase e)
        {
            GetCommonCodeList("SS03", srcF_EQUIPTYPECD, "선택", e.Parameter.ToString());
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
            string actType = e.Parameter.ToUpper();
            
            if (actType == "SAVE") {                
                // 저장 액션
                if (saveMode == "NEW")
                {
                    bResult = MEAS1001_INS(out pkey);
                }
                else if(saveMode == "EDIT")
                {
                    bResult = MEAS1001_UPD(out pkey);
                }

                if (!bResult)
                {
                    procResult = new string[] { "0", "{0} 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "1", "{0}이 완료되었습니다." };
                }

                result["MESSAGE"] = string.Format(procResult[1], ActionNm[saveMode]);
                result["PKEY"] = pkey;
                result["TYPE"] = "AfterSave";
                
            }
            else if (actType == "DEL") {
                // 삭제 액션
                this.MEAS1001_DEL(out procResult);
                result["MESSAGE"] = procResult[1];
                result["TYPE"] = "AfterDelete";
            }
            
            result["CODE"] = procResult[0];
            e.Result = this.SerializeJSON(result);
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
            // 그리드정보를 구한다
            //this.IN01M_LST(GetSearchCondition());
            devGridExporter.WriteXlsToResponse(String.Format("계측기정보관리_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region cbpContent_Callback
        protected void cbpContent_Callback(object sender, CallbackEventArgsBase e)
        {
            int f_MS01MID = -1;

            if (Int32.TryParse(e.Parameter, out f_MS01MID))
            {
                MEAS1001_INF(f_MS01MID);
            }
        } 
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            MEAS1001_LST();
        } 
        #endregion

        #endregion
        
    }
}