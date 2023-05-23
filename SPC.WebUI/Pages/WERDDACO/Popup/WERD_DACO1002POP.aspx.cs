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

namespace SPC.WebUI.Pages.WERDDACO.Popup
{
    public partial class WERD_DACO1002POP : WebUIBasePage
    {
        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        protected string oSetParam = String.Empty;
        string sLINECD = String.Empty;
        private string _nullText = "전체";
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        string[] keyFields = new string[5];
        #endregion

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
                // 반목록을 구한다
                GetQWK110_LST();
                WERD_DACO1002POP_LST();
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

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
            }
        }
        #endregion


        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            keyFields = Request.QueryString.Get("keyFields").Split('|');
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
        }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        {
        }
        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        {
            txtFROMDT.Text = keyFields[0];
            txtTODT.Text = keyFields[1];
            hidCOMP.Text = keyFields[2];
            ddlCOMP.Value = keyFields[2];
            txtITEMNM.Text = keyFields[4];
        }
        #endregion

        #region Function

        #region WERD_DACO1002POP_LST
        void WERD_DACO1002POP_LST()
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", keyFields[0]);
                oParamDic.Add("F_TODT", keyFields[1]);
                oParamDic.Add("F_COMPANYCD", keyFields[2]);
                oParamDic.Add("F_ITEMCD", keyFields[3]);
                ds = biz.WERD_DACO1002POP_LST(oParamDic, out errMsg);
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
            }
        }
        #endregion

        #region 대륙 협력사 조회용
        void GetQWK110_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());


                ds = biz.GetQWK110_LST(oParamDic, out errMsg);
            }

            ddlCOMP.DataSource = ds;
            ddlCOMP.DataBind();
        }
        #endregion

        #endregion

        #region Event

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD_DACO1002POP_LST();
            devGrid.DataBind();
        }
        #endregion

        #region ddlLINE_DataBound
        protected void ddlLINE_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ListEditItem item = new DevExpress.Web.ListEditItem() { Text = nullText, Value = "" };
            ddlCOMP.Items.Insert(0, item);

            ddlCOMP.SelectedIndex = ddlCOMP.Items.FindByValue(sLINECD).Index;
        }
        #endregion

        #region ddlLINE_Callback
        protected void ddlLINE_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetQWK110_LST();
        }
        #endregion

        #region devGridExporter_RenderBrick
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
        }
        #endregion

        #endregion
    }
}