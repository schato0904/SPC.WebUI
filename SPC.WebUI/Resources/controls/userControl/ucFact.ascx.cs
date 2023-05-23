using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucFact : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sFACTCD = String.Empty;
        #endregion

        #region 프로퍼티
        public string targetCtrls { get; set; }
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        private string _factParam = String.Empty;
        public string factParam { get { return this._factParam; } set { this._factParam = value; } }
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

            if (!Page.IsCallback)
            {
                hidFACTCD.Text = !Page.gsVENDOR ? String.Empty : Page.gsFACTCD;

                // 공장목록을 구한다
                QCM02_LST();
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
            sFACTCD = Request.QueryString.Get("FACTCD") ?? "";
            hidFACTCD.Text = sFACTCD;
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

        #region 공장목록을 구한다
        void QCM02_LST()
        {
            string errMsg = String.Empty;
            string sCOMPCD = GetCompCD();
            if (Page.gsVENDOR)
                sCOMPCD = Page.gsCOMPCD;
            else
            {
                string pParam = Request.QueryString.Get("pParam") ?? "";
                if (pParam.Contains("SYST0102") || pParam.Contains("SYST0103") || pParam.Contains("SYST0105"))
                    sCOMPCD = Page.gsCOMPCD;
            }

            using (CommonBiz biz = new CommonBiz())
            {
                Page.oParamDic.Clear();
                Page.oParamDic.Add("F_COMPCD", sCOMPCD);

                ds = biz.QCM02_LST(Page.oParamDic, out errMsg);
            }

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_FACTCD", typeof(String));
            dtTable.Columns.Add("F_FACTNM", typeof(String));
            dtTable.Rows.Add("", "선택");

            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                dtTable.Rows.Add(dtRow["F_FACTCD"].ToString(), dtRow["F_FACTNM"].ToString());
            }

            ddlFACT.DataSource = dtTable;
            //ddlFACT.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlFACT DataBound
        /// <summary>
        /// ddlFACT_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlFACT_DataBound(object sender, EventArgs e)
        {
            //DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            //ddlFACT.Items.Insert(0, item);

            //ddlFACT.SelectedIndex = ddlFACT.Items.FindByValue(sFACTCD).Index;
        }
        #endregion

        #region ddlFACT Callback
        /// <summary>
        /// ddlFACT_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlFACT_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            QCM02_LST();
            ddlFACT.DataBind();
        }
        #endregion

        #endregion
    }
}