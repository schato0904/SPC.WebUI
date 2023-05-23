using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using DevExpress.Web;
using SPC.WERD.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD_DACO5001 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        private string _nullText = "전체";
        string sLINECD = String.Empty;
        public string nullText { get { return this._nullText; } set { this._nullText = value; } }
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["WERD_DACO5001"];
            }
            set
            {
                Session["WERD_DACO5001"] = value;
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

            if (!Page.IsCallback)
            {
                // 반목록을 구한다
                GetQWK110_LST();
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

                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                GetYear();
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

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
        {
        }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            dtChart1 = null;
            this.yearCOMBO.SelectedIndex = 0;
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

        #region 초기값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        {
        }
        #endregion

        #region 사용자 정의 함수

        #region GetYear
        void GetYear()
        {
            string[] Y1 = new string[20];
            for (int a = 0; a < 20; a++)
            {
                Y1[a] = DateTime.Now.AddYears(-a).ToString("yyyy");
            }
            yearCOMBO.DataSource = Y1;
            yearCOMBO.DataBind();
        }
        #endregion

        #region GetQWK110_LST
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

        #region 그리드 목록 조회
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD_DACO5001_LST()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_YY", (this.yearCOMBO.Value ?? "").ToString());
                oParamDic.Add("F_COMPANYCD", hidCOMP.Text.ToString());
                ds = biz.WERD_DACO5001_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devChart1.JSProperties["cpResultCode"] = "0";                
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                    devGrid.JSProperties["cpResultCode"] = "0";                    
                    devChart1.JSProperties["cpResultCode"] = "0";

                    dtChart1 = null;
                }
                else
                {
                    dtChart1 = ds.Tables[1];
                    devGrid.DataSource = ds.Tables[0];
                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #endregion

        #region 사용자 이벤트

        #region yearCOMBO_Callback
        protected void yearCOMBO_Callback(object sender, CallbackEventArgsBase e)
        {
            GetYear();
        }
        #endregion

        #region chart_resizeto
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
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

        #region devchart1_drawing
        protected void devChart1_Drawing()
        {
            if (dtChart1 != null)
            {
                devChart1.Series.Clear();
                DevExpressLib.SetChartBarLineSeries(devChart1, "월별집계", "F_WORKDATE", "F_NGCOUNT", System.Drawing.Color.LightBlue);
                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetChartLegend(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, true, 0, 0, 0, 0, "{V}월", null);
            }
        }
        #endregion

        #region devgrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }
        }
        #endregion

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD_DACO5001_LST();
            devGrid.DataBind();
        }
        #endregion

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            if (devGrid.Columns.Count > 0)
            {
                devGrid.Columns[0].FixedStyle = GridViewColumnFixedStyle.Left;
                devGrid.Columns[0].Width = 130;
                devGrid.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Left;
            }
        }
        #endregion

        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart1_Drawing();
        }
        #endregion                

        #endregion

    }
}