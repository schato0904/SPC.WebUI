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
using SPC.ANLS.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0105_DACO : WebUIBasePage
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
                return (DataTable)Session["TISP0105_1_DACO"];
            }
            set
            {
                Session["TISP0105_1_DACO"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["TISP0105_2_DACO"];
            }
            set
            {
                Session["TISP0105_2_DACO"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["TISP0105_3_DACO"];
            }
            set
            {
                Session["TISP0105_3_DACO"] = value;
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
                this.rdoGBN.SelectedIndex = 0;
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

        void ChartData()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;

            devChart1.JSProperties["cpResult1"] = "";
            devChart1.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_FROMDT", GetFromDt());
            oParamDic.Add("F_TODT", GetToDt());
            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);

            using (TISPBiz biz = new TISPBiz())
            {
                ds = biz.TISP0105_ANAL_DACO(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
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

                    devChart1.JSProperties["cpResult1"] = sb.ToString();
                }
            }

            if (true == bExecute1)
            {
                using (TISPBiz biz = new TISPBiz())
                {
                    ds = biz.TISP0105_CHART_DACO(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        devChart1.JSProperties["cpResultCode"] = "0";
                        devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 측정데이타가 없습니다";
                    }
                    else
                    {
                        bExecute2 = true;
                        dt2 = ds.Tables[0].Copy();

                        Decimal maxAxisY = Decimal.MinValue;
                        Decimal minAxisY = Decimal.MaxValue;

                        foreach (DataRow dtRow in dt2.Rows)
                        {
                            if (maxAxisY < Convert.ToDecimal(dtRow["F_DATA2"]))
                                maxAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);

                            if (minAxisY > Convert.ToDecimal(dtRow["F_DATA2"]))
                                minAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);
                        }

                        maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                        minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                        dt2.Columns.Add("F_STANDARD", typeof(Double));

                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (i == 5 || i == 50 || i == 95)
                            {
                                dt2.Rows[i]["F_STANDARD"] = maxAxisY;
                            }
                        }
                    }
                }
            }

            if (true == bExecute2)
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_CPCPK", this.rdoGBN.SelectedItem.Value.ToString());
                oParamDic.Add("F_VALUE", dt1.Rows[0][this.rdoGBN.SelectedItem.Value.ToString().Equals("1") ? "F_CP" : "F_CPK"].ToString());
                // 검사측정자료를 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.QCD14_LST(oParamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        devChart1.JSProperties["cpResultCode"] = "0";
                        devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 측정데이타가 없습니다";
                    }
                    else
                    {
                        sb = new StringBuilder();

                        dt3 = ds.Tables[0].Copy();

                        DataRow dtRow = dt3.Rows[0];

                        for (int i = 0; i < dt3.Columns.Count; i++)
                        {
                            if (i > 0) sb.Append("|");
                            sb.Append(dtRow[i].ToString());
                        }

                        devChart1.JSProperties["cpResult2"] = sb.ToString();
                    }
                }
            }

            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1 && bExecute2)
            {
                if (dt1.Rows.Count > 0)
                {
                    // 1번 차트에 데이타 전달
                    dtChart2 = dt1.Copy();
                }
                else
                    dtChart2 = null;

                if (dt2.Rows.Count > 0)
                {
                    dtChart1 = dt2.Copy();
                }
                else
                    dtChart1 = null;

                if (dt3.Rows.Count > 0)
                {
                    dtChart3 = dt3.Copy();
                }
                else
                    dtChart3 = null;
            }
        }

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

            devChart1.JSProperties["cpResult1"] = "";

            if (oParams[2] == "resize")
            {
                if (dtChart1 != null)
                {
                    devChart1.Series.Clear();

                    Decimal maxAxisY = Decimal.MinValue;
                    Decimal minAxisY = Decimal.MaxValue;

                    foreach (DataRow dtRow in dtChart1.Rows)
                    {
                        if (maxAxisY < Convert.ToDecimal(dtRow["F_DATA2"]))
                            maxAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);

                        if (minAxisY > Convert.ToDecimal(dtRow["F_DATA2"]))
                            minAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);
                    }

                    maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                    minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                    DevExpressLib.SetChartSplineSeries(devChart1, "", "F_DATA1", "F_DATA2", System.Drawing.Color.Green, null, DevExpress.XtraCharts.ScaleType.Qualitative);
                    DevExpressLib.SetChartBarLineSeries(devChart1, "", "F_DATA1", "F_STANDARD", System.Drawing.Color.Khaki, 0.1);

                    devChart1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;



                    devChart1.DataSource = dtChart1;
                    devChart1.DataBind();

                    //DevExpressLib.SetCrosshairOptions(devChart1);
                    DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}", false, false);
                }

                return;
            }
            else
            {
                ChartData();

                if (dtChart1 != null)
                {
                    devChart1.Series.Clear();

                    Decimal maxAxisY = Decimal.MinValue;
                    Decimal minAxisY = Decimal.MaxValue;

                    foreach (DataRow dtRow in dtChart1.Rows)
                    {
                        if (maxAxisY < Convert.ToDecimal(dtRow["F_DATA2"]))
                            maxAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);

                        if (minAxisY > Convert.ToDecimal(dtRow["F_DATA2"]))
                            minAxisY = Convert.ToDecimal(dtRow["F_DATA2"]);
                    }

                    maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                    minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                    DevExpressLib.SetChartSplineSeries(devChart1, "", "F_DATA1", "F_DATA2", System.Drawing.Color.Green, null, DevExpress.XtraCharts.ScaleType.Qualitative);
                    DevExpressLib.SetChartBarLineSeries(devChart1, "", "F_DATA1", "F_STANDARD", System.Drawing.Color.Khaki, 0.1);

                    devChart1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;



                    devChart1.DataSource = dtChart1;
                    devChart1.DataBind();

                    //DevExpressLib.SetCrosshairOptions(devChart1);
                    DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}", false, false);
                }
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