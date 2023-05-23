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
    public partial class TISP01POP_NEW : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP01POP_NEW"];
            }
            set
            {
                Session["TISP01POP_NEW"] = value;
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
                devCallbackPanel1.JSProperties["cpResultCode"] = "";
                devCallbackPanel1.JSProperties["cpResultMsg"] = "";
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
            oSetParam = Request.QueryString.Get("oSetParam") ?? "";
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
            dtChart1 = null;

            chk_reject.Checked = String.IsNullOrEmpty(oSetParam);

            //if (!String.IsNullOrEmpty(oSetParam) && !gsVENDOR)
            //{
            //    string[] oSetParams = oSetParam.Split('|');
            //    ucComp.compParam = oSetParams[16];
            //    ucFact.factParam = oSetParams[17];
            //}
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

        #region Chart 조회
        void QWK08A_TISP01POP_NEW_CHART()
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_BANCD", GetWorkPOPCD().Substring(0, 2));
                oParamDic.Add("F_LINECD", GetWorkPOPCD().Substring(2, 2));
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_INSPCD", "AAC501");
                oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                oParamDic.Add("F_FREEPOINT", txtFREEPOINT.Text);
                oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");
                ds = biz.QWK08A_TISP01POP_NEW_CHART(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallbackPanel1.JSProperties["cpResultCode"] = "0";
                devCallbackPanel1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                dtChart1 = ds.Tables[0].Copy();
            }
        }
        #endregion

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

            if (oParams[2] == "1" || (oParams[2] == "0" && dtChart1 == null))
            {
                dtChart1 = null;
                QWK08A_TISP01POP_NEW_CHART();
            }

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxImage image = (ASPxImage)panel.FindControl("devImage1");
            image.Width = new Unit(oParams[0]);
            image.Height = new Unit(oParams[1]);
            image.ImageUrl = String.Format("~/API/Common/Chart/GetChartImage.ashx?tbl={0}&siryo={1}&calc={2}&type={3}&w={4}&h={5}&memb={6}&dummy={7}&db={8}",
                "TISP01POP_NEW",
                txtSIRYO.Text,
                chk_calc.SelectedItem.Value.ToString(),
                "xbar",
                oParams[0],
                oParams[1],
                "F_TSERIALNO",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                "08");
        }
        #endregion

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="Width">Int32</param>
        /// <param name="Height">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height)
        {
            if (Width >= 0 && Height >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }
        #endregion

        #endregion
    }
}