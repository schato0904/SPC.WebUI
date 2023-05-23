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
    public partial class ucMachTypeSearch : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sFACTCD = String.Empty;
        #endregion

        #region 프로퍼티
        private string _ClientInstanceName = string.Empty;
        public string ClientInstanceName { get { return _ClientInstanceName; } set { this._ClientInstanceName = value; } }
        private string _OnSelectItem = string.Empty;
        public string OnSelectItem { get { return this._OnSelectItem; } set { this._OnSelectItem = value; } }
        public DataTable CachedData
        {
            get { return Session["DT_" + this.ClientID] as DataTable; }
            set { Session["DT_" + this.ClientID] = value; }
        }
        public DevExpress.Web.ASPxGridView Grid
        {
            get { return this.devGrid; }
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

            if (!Page.IsCallback)
            {
                //hidFACTCD.Text = !Page.gsVENDOR ? String.Empty : Page.gsFACTCD;

                //// 공장목록을 구한다
                //QCM02_LST();
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
            //sFACTCD = Request.QueryString.Get("FACTCD") ?? "";
            //hidFACTCD.Text = sFACTCD;
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
            //this.devGridLookup.GridView.CustomCallback += devGrid_CustomCallback;
            //this.devGridLookup.GridView.DataBinding += devGrid_DataBinding;
            //this.devGridLookup.GridView.Width = Unit.Pixel(540);
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
            this.ClientInstanceName = string.IsNullOrEmpty(this.ClientInstanceName) ? this.ClientID : this.ClientInstanceName;
            this.devGrid.ClientInstanceName = string.Format("{0}_devGrid", this.ClientInstanceName);
            this.schF_BANCD.ClientInstanceName = string.Format("{0}_schF_BANCD", this.ClientInstanceName);
            this.schF_LINECD.ClientInstanceName = string.Format("{0}_schF_LINECD", this.ClientInstanceName);
            this.schF_MACHNM.ClientInstanceName = string.Format("{0}_schF_MACHNM", this.ClientInstanceName);
            this.btnSearch.ClientInstanceName = string.Format("{0}_btnSearch", this.ClientInstanceName);
            this.hidSelectedValues.ClientInstanceName = string.Format("{0}_hidSelectedValues", this.ClientInstanceName);

            this.schF_BANCD.targetCtrls = this.schF_LINECD.ClientInstanceName;

            this.CachedData = null;
            //this.hidFACTCD.ClientInstanceName = string.Format("{0}_hidFACTCD", this.ClientInstanceName);
            //this.ddlFACT.ClientInstanceName = string.Format("{0}_ddlFACT", this.ClientInstanceName);
            this.LoadGridDatasource();
            //this.devGridLookup.GridView.DataSource = this.CachedData;
            //this.devGridLookup.GridView.DataBind();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공장목록을 구한다
        void LoadGridDatasource()
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
                Page.oParamDic.Add("F_FACTCD", Page.gsFACTCD);
                Page.oParamDic.Add("F_BANCD", this.schF_BANCD.GetValue());
                Page.oParamDic.Add("F_LINECD", this.schF_LINECD.GetValue());
                Page.oParamDic.Add("F_MACHNM", this.schF_MACHNM.Text);
                
                ds = biz.GetMACH14_POPUP_LST(Page.oParamDic, out errMsg);
            }

            this.CachedData = ds != null && ds.Tables.Count > 0 ? AutoNumber(ds).Tables[0] : null; 
        }
        #endregion

        #region AutoNumber
        DataSet AutoNumber(DataSet ds, string NumberColumnName = "NO")
        {
            DataSet returnDs = new DataSet();
            DataTable dt = null;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //dt = ds.Tables[i];
                dt = new DataTable();
                dt.Columns.Add(NumberColumnName, typeof(long));
                dt.Columns[NumberColumnName].AutoIncrement = true;
                dt.Columns[NumberColumnName].AutoIncrementSeed = 1;
                dt.Columns[NumberColumnName].AutoIncrementStep = 1;
                dt.Merge(ds.Tables[i]);
                returnDs.Tables.Add(dt);
            }

            return returnDs;
        } 
        #endregion

        #region GetValues
        Dictionary<string, string> GetValues()
        {
            if (string.IsNullOrEmpty(this.hidSelectedValues.Text))
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                this.devGrid.KeyFieldName.Split(';').ToList().ForEach(x => result.Add(x, ""));
                return result;
            }
            return (Dictionary<string, string>)(new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize(this.hidSelectedValues.Text, typeof(Dictionary<string, string>));
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView; 
            this.LoadGridDatasource();
            grid.DataBind();
        } 
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            var grid = sender as DevExpress.Web.ASPxGridView; 
            grid.DataSource = this.CachedData;
        } 
        #endregion

        #endregion
    }
}