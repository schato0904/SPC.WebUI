using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using SPC.WebUI.Common;
using SPC.ANLS.Biz;
using DevExpress.XtraCharts;
using DevExpress.Web;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0101_WORK : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        Int32 totalCnt = 0;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0101_WORK_1"];
            }
            set
            {
                Session["ANLS0101_WORK_1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0101_WORK_2"];
            }
            set
            {
                Session["ANLS0101_WORK_2"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0101_WORK_3"];
            }
            set
            {
                Session["ANLS0101_WORK_3"] = value;
            }
        }

        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["ANLS0101_WORK_4"];
            }
            set
            {
                Session["ANLS0101_WORK_4"] = value;
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
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
                devChart3.JSProperties["cpFunction"] = "resizeTo";
                devChart3.JSProperties["cpChartWidth"] = "0";
                devChart4.JSProperties["cpFunction"] = "resizeTo";
                devChart4.JSProperties["cpChartWidth"] = "0";

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();
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
            dtChart2 = null;
            dtChart3 = null;
            dtChart4 = null;

            chk_reject.Checked = String.IsNullOrEmpty(oSetParam);
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
        #region Data총갯수
        Int32 ANLS0101_WORK_CNT()
        {
            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

                totalCnt = biz.ANLS0101_WORK_CNT(oParamDic);

            }

            return totalCnt;
        }
        #endregion

        #region Data조회
        void ANLS0101_WORK_LST(int nPageSize, int nCurrPage, bool bCallback)
        {
            string errMsg = String.Empty;

            dtChart1 = null;
            dtChart2 = null;
            dtChart3 = null;
            dtChart4 = null;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                ds = biz.ANLS0101_WORK_LST(oParamDic, out errMsg);
            }

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                dtChart1 = ds.Tables[0].Select(string.Format("F_SERIALNO = '{0}'", ds.Tables[1].Rows[0]["F_SERIALNO"].ToString()), "F_WORKDATE", DataViewRowState.CurrentRows).CopyToDataTable();

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 1)
                dtChart2 = ds.Tables[0].Select(string.Format("F_SERIALNO = '{0}'", ds.Tables[1].Rows[1]["F_SERIALNO"].ToString()), "F_WORKDATE", DataViewRowState.CurrentRows).CopyToDataTable();

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 2)
                dtChart3 = ds.Tables[0].Select(string.Format("F_SERIALNO = '{0}'", ds.Tables[1].Rows[2]["F_SERIALNO"].ToString()), "F_WORKDATE", DataViewRowState.CurrentRows).CopyToDataTable();

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 3)
                dtChart4 = ds.Tables[0].Select(string.Format("F_SERIALNO = '{0}'", ds.Tables[1].Rows[3]["F_SERIALNO"].ToString()), "F_WORKDATE", DataViewRowState.CurrentRows).CopyToDataTable();

            
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bCallback)
                {
                    // Pager Setting
                    ucPager.TotalItems = 0;
                    ucPager.PagerDataBind();

                }
                else
                {
                    devCallback.JSProperties["cpResultCode"] = "pager";
                    devCallback.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, ANLS0101_WORK_CNT());
                }
            }
        }
        #endregion
        #endregion

        #region 사용자이벤트

        protected void devCallback_Callback(object source, CallbackEventArgs e)
        {
            
            int nCurrPage = 0;
            int nPageSize = 0;

            if (!String.IsNullOrEmpty(e.Parameter))
            {
                string[] oParams = new string[2];
                foreach (string oParam in e.Parameter.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                }
            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager.GetPageSize();
            }

            ANLS0101_WORK_LST(nPageSize, nCurrPage, true);

        }

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

            if (dtChart1 != null)
            {
                devChart1.Series.Clear();


                DevExpressLib.SetChartLineSeries(devChart1, "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2,4);
                DevExpressLib.SetChartLineSeries(devChart1, "CL", "F_MEMBER", "F_CLX", System.Drawing.Color.FromArgb(51, 204, 51),1);
                DevExpressLib.SetChartLineSeries(devChart1, "UCL", "F_MEMBER", "F_UCLX", System.Drawing.Color.FromArgb(90, 171, 183), 1);
                DevExpressLib.SetChartLineSeries(devChart1, "LCL", "F_MEMBER", "F_LCLX", System.Drawing.Color.FromArgb(90, 171, 183), 1);
                DevExpressLib.SetChartLineSeries(devChart1, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red, 1);
                DevExpressLib.SetChartLineSeries(devChart1, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red, 1);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart1, dtChart1.Rows[0]["F_INSPDETAIL"].ToString(), false);

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
            else
            {
                DevExpressLib.SetChartTitle(devChart1, "", false);
            }
        }
        #endregion

        #region devChart2_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart2 != null)
            {
                devChart2.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart2, "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2,4);
                DevExpressLib.SetChartLineSeries(devChart2, "CL", "F_MEMBER", "F_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart2, "UCL", "F_MEMBER", "F_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart2, "LCL", "F_MEMBER", "F_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart2, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart2, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart2.DataSource = dtChart2;
                devChart2.DataBind();


                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart2, dtChart2.Rows[0]["F_INSPDETAIL"].ToString(), false);

                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
            else
            {
                DevExpressLib.SetChartTitle(devChart2, "", false);
            }
        }
        #endregion

        #region devChart3_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart3 != null)
            {
                devChart3.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart3, "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2,4);
                DevExpressLib.SetChartLineSeries(devChart3, "CL", "F_MEMBER", "F_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart3, "UCL", "F_MEMBER", "F_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart3, "LCL", "F_MEMBER", "F_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart3, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart3, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart3.DataSource = dtChart3;
                devChart3.DataBind();


                DevExpressLib.SetCrosshairOptions(devChart3);
                DevExpressLib.SetChartDiagram(devChart3, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart3, dtChart3.Rows[0]["F_INSPDETAIL"].ToString(), false);

                XYDiagram diagram = (XYDiagram)devChart3.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
            else
            {
                DevExpressLib.SetChartTitle(devChart3, "", false);
            }
        }
        #endregion

        #region devchart4_CustomCallback
        /// <summary>
        /// devChart1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CustomCallbackEventArgs</param>
        protected void devchart44_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            if (dtChart4 != null)
            {
                devChart4.Series.Clear();

                DevExpressLib.SetChartLineSeries(devChart4, "xBar", "F_MEMBER", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153), 2,4);
                DevExpressLib.SetChartLineSeries(devChart4, "CL", "F_MEMBER", "F_CLX", System.Drawing.Color.FromArgb(51, 204, 51));
                DevExpressLib.SetChartLineSeries(devChart4, "UCL", "F_MEMBER", "F_UCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart4, "LCL", "F_MEMBER", "F_LCLX", System.Drawing.Color.FromArgb(90, 171, 183));
                DevExpressLib.SetChartLineSeries(devChart4, "상한", "F_MEMBER", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart4, "하한", "F_MEMBER", "F_MIN", System.Drawing.Color.Red);

                devChart4.DataSource = dtChart4;
                devChart4.DataBind();


                DevExpressLib.SetCrosshairOptions(devChart4);
                DevExpressLib.SetChartDiagram(devChart4, false, 0, 0, null, null, null, "{V:n4}");
                DevExpressLib.SetChartTitle(devChart4, dtChart4.Rows[0]["F_INSPDETAIL"].ToString(), false);

                XYDiagram diagram = (XYDiagram)devChart4.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            }
            else
            {
                DevExpressLib.SetChartTitle(devChart4, "", false);
            }
        }
        #endregion

        #region chart ResizeTo
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
        #endregion
    }
}