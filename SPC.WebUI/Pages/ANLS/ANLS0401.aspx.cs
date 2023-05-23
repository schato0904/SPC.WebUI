using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ANLS.Biz;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0401 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

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
                pnlChart.JSProperties["cpResultCode"] = "";
                pnlChart.JSProperties["cpResultMsg"] = "";
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
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region Data조회
        void ANLS0401_LST(DevExpress.XtraCharts.Web.WebChartControl chart, string arrSerial)
        {
            string errMsg = String.Empty;
            ////F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_SIRYO;F_WORKCD;F_WORKNM
            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", arrSerial.Split('|')[4]);
                oParamDic.Add("F_SERIALNO", arrSerial.Split('|')[1]);
                ds = biz.ANLS0401_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlChart.JSProperties["cpResultCode"] = "0";
                pnlChart.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                chart_Draw(chart, ds, arrSerial.Split('|')[2]);
            }
        }
        #endregion

        protected void pnlChart_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            DevExpress.XtraCharts.Web.WebChartControl[] arrChart = new DevExpress.XtraCharts.Web.WebChartControl[12];
            for (int i = 0; i < 12; i++)
            {
                arrChart[i] = pnlChart.FindControl("devChart" + (i + 1)) as DevExpress.XtraCharts.Web.WebChartControl;
                arrChart[i].Width = new Unit(oParams[0]);
                arrChart[i].Height = new Unit(oParams[1]);
            }

            string[] INSPCD = hidINSPCD.Text.Split(',');

            if (INSPCD[0] != "")
            {
                for (int i = 0; i < INSPCD.Length; i++)
                {
                    ANLS0401_LST(arrChart[i], INSPCD[i]);
                }
            }
        }

        #region ChartDraw
        protected void chart_Draw(DevExpress.XtraCharts.Web.WebChartControl chart, DataSet ds, string strINSPDETAIL)
        {
            if (ds.Tables[0] != null)
            {
                chart.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    if (!String.IsNullOrEmpty(dtRow["F_MAX"].ToString()))
                        maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["F_MAX"]));

                    if (!String.IsNullOrEmpty(dtRow["F_MIN"].ToString()))
                        minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["F_MIN"]));

                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartLineSeries(chart, "xBar", "F_WORKDATE", "F_XBAR", System.Drawing.Color.FromArgb(0, 102, 153));
                DevExpressLib.SetChartLineSeries(chart, "상한", "F_WORKDATE", "F_MAX", System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(chart, "하한", "F_WORKDATE", "F_MIN", System.Drawing.Color.Red);

                chart.DataSource = ds.Tables[0];
                chart.DataBind();
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                
                DevExpress.XtraCharts.ChartTitle ct = new DevExpress.XtraCharts.ChartTitle();
                ct.Text = strINSPDETAIL;
                ct.Font = new System.Drawing.Font("",10);
                
                chart.Titles.Add(ct);
                DevExpressLib.SetCrosshairOptions(chart);
                DevExpressLib.SetChartDiagram(chart, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}",false);
            }
        }
        #endregion

        #endregion
    }
}