using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTF.Web.Framework.Helper;
using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.COMM.Popup
{
    public partial class COMPPOP : WebUIBasePage
    {

        #region 변수, 기본
        DataSet ds = null;
        private DBHelper spcDB;

        protected void Page_Init(object sender, EventArgs e)
        {

            {
                // Request
                GetRequest();

                // 품목검색 목록
                //GetQCD01_POPUP_LST();
            }
        }

        void GetRequest()
        {
            COMPNM.Text = Request.QueryString.Get("COMPNM") ?? "";

        }

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

        #region 고객사 검색 목록
        void COMPPOP_LST()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();
            oParamDic.Add("F_COMPNM", COMPNM.Text);


            ds = COMPPOP_LST(oParamDic, out errMsg);


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

        #region devGrid
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            COMPPOP_LST();
        }
        #endregion

        #region dbhelper

        public DataSet COMPPOP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = COMPPOP_LST2(oParams, out errMsg);


            return ds;
        }
        public DataSet COMPPOP_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_COMPPOP_LST", out errMsg);
            }

            return ds;
        }

        #endregion


    }
}