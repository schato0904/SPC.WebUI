using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucNgCauseTable : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        // 결재유형별 사용자 목록 조회해온 뒤 세션에 담아 사용
        string[] strParam;
        public string idListStr
        {
            get { return hidIdListStr.Text; }
            set { hidIdListStr.Text = value; }
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
            if (!Page.IsCallback)
            {
                // Request
                GetRequest();
            }
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
            //parentCallback = Request.QueryString.Get("parentCallback") ?? "";
            //Type = Request.QueryString.Get("TYPE") ?? "";
            //txtDOCTYPECD.Text = string.IsNullOrWhiteSpace(Request["DOCTYPECD"]) ? "" : Request["DOCTYPECD"];
            //txtAPPRNO.Text = string.IsNullOrWhiteSpace(Request["APPRNO"]) ? "" : Request["APPRNO"];
            //txtMACHGUBUN.Text = Request.QueryString.Get("MACHGUBUN") ?? "";
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
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 목록 조회
        DataSet NGCAUSE_LST(out string errMsg)
        {
            DataSet ds = null;
            errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                Page.oParamDic.Clear();
                if (strParam.Length > 0)
                {
                    Page.oParamDic.Add("F_COMPCD", Page.gsCOMPCD);
                    Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                    Page.oParamDic.Add("F_WORKDATE", strParam[0]);
                    Page.oParamDic.Add("F_GUBUN", strParam[1]);
                    Page.oParamDic.Add("F_ITEMCD", strParam[2]);
                    Page.oParamDic.Add("F_WORKCD", strParam[3]);
                    Page.oParamDic.Add("F_DAYPRODUCTNO", strParam[4]);
                }
                ds = biz.NGCAUSE_LST(Page.oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region ucNgCausePanel_Init
        protected void ucNgCausePanel_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxCallbackPanel).ClientInstanceName = (sender as Control).UniqueID;
        }
        #endregion

        #region ucNgCausePanel_Callback
        protected void ucNgCausePanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            var param = e.Parameter.Split('|');
            if (param[0] == "select")
            {
                strParam = param[1].Split(';');
                string errMsg = string.Empty;
                DataSet ds = NGCAUSE_LST(out errMsg);
                DataTable dt = (ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : null);
                List<string> idList = new List<string>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        DataRow dr = dt.Rows[i];
                        var ctrl = (SPC.WebUI.Resources.controls.userControl.ucNgCauseRow)Page.LoadControl("~/Resources/controls/userControl/ucNgCauseRow.ascx");
                        ctrl.ID = string.Format("ucNgCauseRow{0}", i);
                        ctrl.CallerId = this.UniqueID;
                        ctrl.F_NGCAUSECD = dr["F_NGCAUSECD"].ToString();
                        ctrl.F_NGCAUSENM = dr["F_NGCAUSENM"].ToString();
                        ctrl.F_CNT = dr["F_CNT"].ToString();
                        this.pHolder1.Controls.Add(ctrl);
                        idList.Add(ctrl.UniqueID);
                    }
                }
                idListStr = string.Join("|", idList.ToArray());
            }
            else idListStr = "";
        }
        #endregion

        #region hidIdListStr_Init
        protected void hidIdListStr_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).ClientInstanceName = (sender as DevExpress.Web.ASPxTextBox).UniqueID;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #endregion
    }
}