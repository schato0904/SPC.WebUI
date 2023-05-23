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
using System.Text;
using DevExpress.Web;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS3001 : WebUIBasePage
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
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
                devCallback.JSProperties["cpResult"] = "";
                devCallback.JSProperties["cpResultGbn"] = "";

                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid.JSProperties["cpResult"] = "";
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
        {
            //     AAFC 상태구분코드 F_STATUSCD
            GetCommonCodeList("SS09", srcF_STATUSCD);
            GetCommonCodeList("SS09", shcF_STATUSCD);
            GetCommonCodeList("SS10", srcF_ABNORMALCD);
            GetTeamCodeList(srcF_TEAMCD, gsFACTCD);
            GetCustCodeList(srcF_CUSTID);
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공통, 팀, 반, 공정에 대한 코드, 분류를 구한다

        void GetCommonCodeList(string groupCD, DevExpress.Web.ASPxComboBox comboBox, string firstText = "선택")
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

        void GetCustCodeList(DevExpress.Web.ASPxComboBox comboBox, string firstText = "선택")
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.MEAS9007_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_CUSTNM";
            comboBox.ValueField = "F_CUSTID";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem(firstText, ""));
        }

        #endregion

    

        #region 계측기 이력 목록 조회
        void MS01D4_GRID_LST()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_STATUSCD", (shcF_STATUSCD.Value ?? "").ToString());
                oParamDic.Add("F_EQUIPNO", shcF_EQUIPNO.Text);

                ds = biz.MS01D4_GRID_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region 생산팀 리스트
        protected void srcF_TEAMCD_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            GetTeamCodeList(devComboBox, gsFACTCD);
            devComboBox.SelectedIndex = 0;
        }
        #endregion

        #region 저장
        void MS01D4_UPD()
        {
            int idx = 0;
            int count = 1;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_MS01MID", this.srcF_MS01MID.Text);
            oParamDic.Add("F_STATUSCD", (srcF_STATUSCD.Value??"").ToString());
            oParamDic.Add("F_ABNORMALCD", (srcF_ABNORMALCD.Value??"").ToString());
            oParamDic.Add("F_ONDT", this.srcF_ONDT.Text);
            oParamDic.Add("F_TEAMCD", (this.srcF_TEAMCD.Value??"").ToString());
            oParamDic.Add("F_USER", this.srcF_USER.Text);
            oParamDic.Add("F_ATTFILENO", this.srcF_ATTFILENO.Text);
            oParamDic.Add("F_RETNPLANDT", this.srcF_RETNPLANDT.Text);
            oParamDic.Add("F_RETNDT", this.srcF_RETNDT.Text);
            oParamDic.Add("F_CONTENTS", this.srcF_CONTENTS.Text);
            oParamDic.Add("F_CUSTID", (srcF_CUSTID.Value??"").ToString());
            oParamDic.Add("F_PROCDATE", this.srcF_PROCDATE.Text);
            oParamDic.Add("PKEY", "OUTPUT");

            oSPs[idx] = "USP_MS01D4_INS";

            if (this.srcF_MS01D4ID.Text != "")
            {
                oParamDic.Add("F_MS01D4ID", this.srcF_MS01D4ID.Text);
                oSPs[idx] = "USP_MS01D4_UPD";
            }
            oParameters[idx] = (object)oParamDic;

            idx++;

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
                devCallback.JSProperties["cpResultGbn"] = resultMsg;
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
        }
        #endregion

        #region 저장
        void MS01D4_DEL()
        {
            int idx = 0;
            int count = 1;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_MS01D4ID", this.srcF_MS01D4ID.Text);
            
            oSPs[idx] = "USP_MS01D4_DEL";

            oParameters[idx] = (object)oParamDic;

            idx++;

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "삭제가 완료되었습니다." };
                devCallback.JSProperties["cpResultGbn"] = resultMsg;
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
        }
        #endregion

        #region devCallback CustomCallback
        /// <summary>
        /// devCallback_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string[] strParam = e.Parameter.Split(';');
            if (strParam[0] == "SELECT")
            {
            }
            else if (strParam[0] == "UPDATE")
            {
                MS01D4_UPD();
            }
            else if (strParam[0] == "DELETE")
            {
                MS01D4_DEL();
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
            MS01D4_GRID_LST();
            devGrid.DataBind();
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("F_PROCFLG").ToString();

            if (strJudge == "O")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }
        #endregion


        #endregion

        protected void EQUIPCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;
                        
            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_EQUIPNO", shcF_EQUIPNO.Text);

                ds = biz.MS01M_TBL_POPUP_LST(oParamDic, out errMsg);
            }

            string EQUIPNO = String.Empty;
            string EQUIPNM = String.Empty;

            if (true == bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        EQUIPNO = (string)dtRow["F_EQUIPNO"];
                        EQUIPNM = (string)dtRow["F_EQUIPNM"];
                    }
                }
            }

            EQUIPCallback.JSProperties["cpEQUIPNO"] = EQUIPNO;
            EQUIPCallback.JSProperties["cpEQUIPNM"] = EQUIPNM;
        }   
    }
}