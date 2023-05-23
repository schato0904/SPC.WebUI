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

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0204 : WebUIBasePage
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
                return (DataTable)Session["ANLS0204_1"];
            }
            set
            {
                Session["ANLS0204_1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0204_2"];
            }
            set
            {
                Session["ANLS0204_2"] = value;
            }
        }

        DataTable dtGrid
        {
            get
            {
                return (DataTable)Session["ANLS0204"];
            }
            set
            {
                Session["ANLS0204"] = value;
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
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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
            this.dtChart1 = null;
            this.dtChart2 = null;
            this.dtGrid = null;
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

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                maxAxisY = Math.Max(Convert.ToDecimal(dtChart1.Compute("MAX(F_DATA2)", ""))
                    , Convert.ToDecimal(dtChart1.Compute("MAX(F_DATA3)", "")));

                minAxisY = Math.Min(Convert.ToDecimal(dtChart1.Compute("MIN(F_DATA2)", ""))
                    , Convert.ToDecimal(dtChart1.Compute("MIN(F_DATA3)", "")));

                if (maxAxisY == minAxisY)
                {
                    maxAxisY = 0;
                    minAxisY = 0;
                }
                else
                {
                    maxAxisY += (maxAxisY - minAxisY) / 10m;
                    minAxisY -= (maxAxisY - minAxisY) / 10m;
                }

                //maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                //minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                //maxAxisY = 0;
                //minAxisY = 0;

                DevExpressLib.SetChartLineSeries(devChart1, "집단1", "F_SEQ", "F_DATA2", System.Drawing.Color.Blue);
                DevExpressLib.SetChartLineSeries(devChart1, "집단2", "F_SEQ", "F_DATA3", System.Drawing.Color.Red);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
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

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                string maxColumn = String.Empty;
                string minColumn = String.Empty;

                maxAxisY = Math.Max(Convert.ToDecimal(dtChart2.Compute("MAX(F_SIRYO1)", ""))
                    , Convert.ToDecimal(dtChart2.Compute("MAX(F_SIRYO2)", "")));

                minAxisY = Math.Max(Convert.ToDecimal(dtChart2.Compute("MIN(F_SIRYO1)", ""))
                    , Convert.ToDecimal(dtChart2.Compute("MIN(F_SIRYO2)", "")));

                //if (maxAxisY == minAxisY)
                //{
                //    if (maxAxisY == 0m)
                //    {
                //        maxAxisY = 11;
                //        minAxisY = -1;
                //    }
                //    else
                //    {
                //        maxAxisY = maxAxisY * 1.1m;
                //        minAxisY = 0m;
                //    }
                //}
                //else
                //{
                //    maxAxisY += (maxAxisY - minAxisY) / 10m;
                //    minAxisY -= (maxAxisY - minAxisY) / 10m;
                //}

                minAxisY = minAxisY > 0m ? 0m : minAxisY;
                maxAxisY = maxAxisY == 0m ? 11m : (maxAxisY * 1.1m);

                //maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                //minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartManhattanBarSeries(devChart2, "집단1", "F_GBNNM", "F_SIRYO1", System.Drawing.Color.Blue);
                DevExpressLib.SetChartManhattanBarSeries(devChart2, "집단2", "F_GBNNM", "F_SIRYO2", System.Drawing.Color.Red);
                
                devChart2.DataSource = dtChart2;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram3D(devChart2, false, 0, 0, 0, 0, null, "{V}");
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

        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            StringBuilder sb = null;

            devGrid1.JSProperties["cpResult1"] = "";
            devGrid1.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("P_COMPCD", gsCOMPCD);
            oParamDic.Add("P_FACTCD", gsFACTCD);
            oParamDic.Add("P_KIND", "1");
            oParamDic.Add("P_FRDT1", this.GetFromDt1());
            oParamDic.Add("P_TODT1", this.GetToDt1());
            oParamDic.Add("P_FRDT2", this.GetFromDt2());
            oParamDic.Add("P_TODT2", this.GetToDt2());

            oParamDic.Add("P_ITEMCD1", this.GetItemCD1());
            oParamDic.Add("P_ITEMCD2", this.GetItemCD2());
            oParamDic.Add("P_WORKCD1", this.GetWorkPOPCD1());
            oParamDic.Add("P_WORKCD2", this.GetWorkPOPCD2());
            oParamDic.Add("P_SERIALNO1", this.txtSERIALNO1.Text);
            oParamDic.Add("P_SERIALNO2", this.txtSERIALNO2.Text);
            //oParamDic.Add("F_SERIALNO1", );
            //oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            //oParamDic.Add("F_GBN", chk_reject.Checked ? "1" : "0");

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0204_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid1.JSProperties["cpResultCode"] = "0";
                    devGrid1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds.Tables[0].Copy();

                    DataRow dtRow = dt1.Rows[0];

                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append("|");
                        sb.Append(dtRow[i].ToString());
                    }

                    devGrid1.JSProperties["cpResult1"] = sb.ToString();
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1)
            {
                devGrid1.DataSource = dt1;
                devGrid1.DataBind();

                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart1 = ds.Tables[1];
                }
                else
                    dtChart1 = null;
            }
        }

        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            StringBuilder sb = null;

            devGrid1.JSProperties["cpResult1"] = "";
            devGrid1.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("P_COMPCD", gsCOMPCD);
            oParamDic.Add("P_FACTCD", gsFACTCD);
            oParamDic.Add("P_KIND", "2");
            oParamDic.Add("P_FRDT1", this.GetFromDt1());
            oParamDic.Add("P_TODT1", this.GetToDt1());
            oParamDic.Add("P_FRDT2", this.GetFromDt2());
            oParamDic.Add("P_TODT2", this.GetToDt2());

            oParamDic.Add("P_ITEMCD1", this.GetItemCD1());
            oParamDic.Add("P_ITEMCD2", this.GetItemCD2());
            oParamDic.Add("P_WORKCD1", this.GetWorkPOPCD1());
            oParamDic.Add("P_WORKCD2", this.GetWorkPOPCD2());
            oParamDic.Add("P_SERIALNO1", this.txtSERIALNO1.Text);
            oParamDic.Add("P_SERIALNO2", this.txtSERIALNO2.Text);

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0204_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid1.JSProperties["cpResultCode"] = "0";
                    devGrid1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    sb = new StringBuilder();

                    dt1 = ds.Tables[0].Copy();

                    DataRow dtRow = dt1.Rows[0];

                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append("|");
                        sb.Append(dtRow[i].ToString());
                    }

                    devGrid1.JSProperties["cpResult1"] = sb.ToString();
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1)
            {
                devGrid2.DataSource = dt1;
                devGrid2.DataBind();

                if (dt1.Rows.Count > 0)
                    dtGrid = dt1.Copy();
                else
                    dtGrid = null;

                // 검사규격을 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    oParamDic.Remove("P_KIND");
                    ds = biz.ANLS0204_CHART_LST(oParamDic, out errMsg);
                    if (!bExistsDataSet(ds))
                    {
                        dtChart2 = null;
                    }
                    else
                    {
                        dtChart2 = ds.Tables[0].Copy();
                    }
                }
            }
        }
        #endregion
    }
}