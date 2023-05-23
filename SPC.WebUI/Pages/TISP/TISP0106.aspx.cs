using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.TISP.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0106 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sTCNT = String.Empty;
        string sNCNT = String.Empty;
        string sNGRATE = String.Empty;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["TISP0106"];
            }
            set
            {
                Session["TISP0106"] = value;
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
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
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
        void QWK08A_TISP0106_LST_FOR_NSUMHOUR()
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_TYPE", ddlTYPE.SelectedItem.Value.ToString());
                oParamDic.Add("F_STHOUR", rdoSTHOUR.SelectedItem.Value.ToString());
                oParamDic.Add("F_ISONLYNG", !chkOnlyNG.Checked ? "0" : "1");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                ds = biz.QWK08A_TISP0106_LST_FOR_NSUMHOUR(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devChart1.JSProperties["cpFunction"] = "";
                devChart1.JSProperties["cpChartWidth"] = errMsg;
            }
            else
            {
                dtChart1 = ds.Tables[0].Copy();

                if (dtChart1 != null && dtChart1.Rows.Count > 0)
                {
                    DataRow drResult = ds.Tables[1].Rows[0];
                    devChart1.JSProperties["cpFunction"] = "setResult";
                    devChart1.JSProperties["cpChartWidth"] = String.Join("|",
                        Convert.ToInt32(drResult["F_TCNT"]).ToString("0,##0"),
                        Convert.ToInt32(drResult["F_NCNT"]).ToString("0,##0"),
                        drResult["F_NGRATE"].ToString());
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devChart1_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (oParams[2] == "1" || (oParams[2] == "0" && dtChart1 == null))
            {
                dtChart1 = null;
                QWK08A_TISP0106_LST_FOR_NSUMHOUR();
            }

            devChart1.Series.Clear();

            DevExpressLib.SetChartLineSeries(devChart1, "LINE", "F_WORKDATE", "F_NGRATE", System.Drawing.Color.FromArgb(0, 102, 153), 1, false);
            DevExpressLib.SetChartPointSeries(devChart1, "불량율(%)", "F_WORKDATE", "F_NGRATE", System.Drawing.Color.Red, 4);
            DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_WORKDATE", "F_CL", System.Drawing.Color.FromArgb(51, 204, 51));
            DevExpressLib.SetChartStepLineSeries(devChart1, "UCL", "F_WORKDATE", "F_UCL", System.Drawing.Color.FromArgb(90, 171, 183));
            DevExpressLib.SetChartStepLineSeries(devChart1, "LCL", "F_WORKDATE", "F_LCL", System.Drawing.Color.FromArgb(90, 171, 183));


            devChart1.DataSource = dtChart1;
            devChart1.DataBind();

            DevExpressLib.SetCrosshairOptions(devChart1);
            DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n4}");
            DevExpressLib.SetChartTitle(devChart1, "P 관리도", true);

            XYDiagram diagram = (XYDiagram)devChart1.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            //diagram.AxisY.AutoScaleBreaks.Enabled = true;
            //diagram.AxisY.AutoScaleBreaks.MaxCount = 8;
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