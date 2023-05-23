using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.TISP.Biz;
using DevExpress.XtraCharts;
using DevExpress.Web;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0104 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP0104"];
            }
            set
            {
                Session["TISP0104"] = value;
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
                chk_calc.SelectedIndex = 0;
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
                callbackControl.JSProperties["cpResultCode"] = "";
                callbackControl.JSProperties["cpResultMsg"] = "";
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
            dtChart1 = null;
            this.chk_reject.Checked = true;
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

        #endregion

        #region 사용자이벤트

        #region devCallbackPanel1_Callback
        /// <summary>
        /// devCallbackPanel1_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            System.Threading.Thread.Sleep(2000);

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage1");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP0104",
                txtSIRYO.Text,
                chk_calc.SelectedItem.Value.ToString(),
                "xbar",
                oParams[0],
                oParams[1],
                "F_MEMBER",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08");
        }
        #endregion

        #region devCallbackPanel2_Callback
        /// <summary>
        /// devCallbackPanel2_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel2_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            System.Threading.Thread.Sleep(2000);

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage2");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP0104",
                txtSIRYO.Text,
                chk_calc.SelectedItem.Value.ToString(),
                "r",
                oParams[0],
                oParams[1],
                "F_MEMBER",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08");
        }
        #endregion

        #region callbackControl_Callback
        /// <summary>
        /// callbackControl_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void callbackControl_Callback(object source, CallbackEventArgs e)
        {
            string errMsg = String.Empty;

            callbackControl.JSProperties["cpResult1"] = "";
            callbackControl.JSProperties["cpResult2"] = "";

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetFromDt());
            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (TISPBiz biz = new TISPBiz())
            {
                ds = biz.TISP0104_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                callbackControl.JSProperties["cpResultCode"] = "0";
                callbackControl.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    callbackControl.JSProperties["cpResultCode"] = "0";
                    callbackControl.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    dtChart1 = ds.Tables[0];
                }
            }
        }
        #endregion

        #endregion

        
    }
}