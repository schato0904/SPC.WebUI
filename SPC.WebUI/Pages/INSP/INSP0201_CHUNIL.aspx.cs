using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SPC.WebUI.Common;
using SPC.INSP.Biz;

namespace SPC.WebUI.Pages.INSP
{
    public partial class INSP0201_CHUNIL : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet dsGrid
        {
            get
            {
                return (DataSet)Session["INSP0201_CHUNIL"];
            }
            set
            {
                Session["INSP0201_CHUNIL"] = value;
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

            if (!IsCallback && !IsPostBack)
            {
                // 검사성적서목록
                INS01_INSP0201_LST();
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
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
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
            //ddlCustomer.NullText = "선택하세요";
            //ddlCustomer.TextField = "F_CODENM";
            //ddlCustomer.ValueField = "F_CODE";
            //ddlCustomer.DataSource = CUSTOMER_REPORT_LST();
            //ddlCustomer.DataBind();

            //ddlCustomer.Items.Insert(0, new DevExpress.Web.ListEditItem("기본양식", "00|cybertechfriend"));
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 고객사목록
        DataSet CUSTOMER_REPORT_LST()
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.CUSTOMER_REPORT_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회
        void INS01_INSP0201_LST()
        {
            string errMsg = String.Empty;

            string sCUSCOMCD = hidCUSTOMCD.Text;
            if (!String.IsNullOrEmpty(sCUSCOMCD))
            {
                string[] sValues = sCUSCOMCD.Split('|');
                sCUSCOMCD = sValues[0];
            }

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_CUSTOMCD", sCUSCOMCD);
                oParamDic.Add("F_LOTNO2", "");
                oParamDic.Add("F_EONO", "");
                oParamDic.Add("F_LANGTP", gsLANGTP);
                oParamDic.Add("F_ITEMCD", GetItemCD());

                ds = biz.INSP0201_LST_CHUNIL(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;
            dsGrid = ds;

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

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 검사성적서목록
            INS01_INSP0201_LST();
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            bool bResult = false;
            string errMsg = string.Empty;
            string msg = string.Empty;
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            bool result = false;

            string a = e.Parameter.ToString();


            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_GROUPCD", e.Parameter.ToString());

                result = biz.INSP0201_DEL_CHUNIL(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = "검사 성적서가 삭제 되었습니다.";
            }
        }
        #endregion


     

     


        #endregion
    }
}