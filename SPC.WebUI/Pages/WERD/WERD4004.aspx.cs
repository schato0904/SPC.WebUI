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
    public partial class WERD4004 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string strRateGbn = "";
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["WERD4004"];
            }
            set
            {
                Session["WERD4004"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD4004_1"];
            }
            set
            {
                Session["WERD4004_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD4004_TYPE"];
            }
            set
            {
                Session["WERD4004_TYPE"] = value;
            }
        }
        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["WERD4004_2"];
            }
            set
            {
                Session["WERD4004_2"] = value;
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
            strRateGbn = GetCommonCodeText("AAG301") == "RATE" ? "부적합률(%)" : "PPM";
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
                devGrid.JSProperties["cpResult"] = "";

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
            WERD4004_LST();


            dtChart1 = null;
            dtChart2 = null;
            dtChart3 = null;
            dtChart4 = null;
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
            //this.AspxCombox_DataBind(this.srcF_PROCCD, "PP", "PPA4");
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 그리드 목록 조회(품목)
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD4004_LST()
        {
            string errMsg = String.Empty;

            bool bExecute1 = false; //추가



            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetFromDt() + "-31");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());

                ds = biz.WERD4004_LST(oParamDic, out errMsg);
            }



            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;
                devChart2.JSProperties["cpResultCode"] = "0";
                devChart2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart1 = null;
                    dtChart2 = null;
                    dtChart3 = null;
                    dtChart4 = null;
                }
                else
                {

                    SetGrid(ds);

                    dtChart1 = ds.Tables[0];
                }
            }




        }
        #endregion

        #region SetGrid
        void SetGrid(DataSet ds)
        {
            if (ds == null)
                return;

            int rocnt = ds.Tables[0].Rows.Count;
            int colcnt = ds.Tables[0].Columns.Count;

            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("구분", typeof(String));

            for (int i = 0; i < rocnt; i++)
            {
                dtTemp.Columns.Add(ds.Tables[0].Rows[i]["F_NAME"].ToString(), typeof(String));

            }
            for (int j = 0; j < colcnt - 1; j++)
            {
                dtTemp.Rows.Add();
                for (int i = 1; i <= rocnt; i++)
                {
                    if (j != 3)
                        dtTemp.Rows[j][i] = string.Format("{0:N0}", ds.Tables[0].Rows[i - 1][j + 1]);
                    else if (j == 3)
                        dtTemp.Rows[j][i] = ds.Tables[0].Rows[i - 1][j + 1];

                }
            }
            dtTemp.Rows[0][0] = "생산수";
            dtTemp.Rows[1][0] = "검사수";
            dtTemp.Rows[2][0] = "부적합수";
            dtTemp.Rows[3][0] = "부적합률(%)";
            dtTemp.Rows[4][0] = "PPM";
            dtTemp.Rows[5][0] = "손실금액(원)";

            dtTemp.Rows.Add();
            dtTemp.Rows[6][0] = "점유율(%)";

            dtTemp.Rows[6][dtTemp.Columns.Count - 1] = "100";

            for (int a = 1; a <= dtTemp.Columns.Count - 2; a++)
            {
                if (Convert.ToDecimal(dtTemp.Rows[2][dtTemp.Columns.Count - 1].ToString()) == 0)
                {
                    dtTemp.Rows[6][a] = 0;
                }
                else
                {
                    dtTemp.Rows[6][a] = string.Format("{0:N2}", (Convert.ToDecimal(dtTemp.Rows[2][a].ToString()) / Convert.ToDecimal(dtTemp.Rows[2][dtTemp.Columns.Count - 1].ToString())) * 100);
                }
                //(Convert.ToInt32(Math.Round(Convert.ToDecimal(dtTemp.Rows[2][a].ToString()) / Convert.ToDecimal(dtTemp.Rows[2][dtTemp.Columns.Count - 1].ToString()) * 10000) / 100));
            }

            dtChart2 = dtTemp;
            devGrid.DataSource = dtChart2;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlComboBox_DataBound





        //self




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

        #region devChart1_Drawing
        protected void devChart1_Drawing()
        {
            devChart1.Series.Clear();
            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Copy();
                cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart1.Series.Clear();
                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_NAME"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_NGQTY"]) }));
                }

                series.Label.TextPattern = "{A}: {VP:p2}";

                devChart1.Titles.Clear();
                devChart1.Series.Add(series);
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "공정별 점유율";
                tlt1.Font = font;
                devChart1.Titles.Add(tlt1);
                devChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart1.DataSource = cdt;
                devChart1.DataBind();

            }
        }
        #endregion
        #region devChart2_Drawing
        protected void devChart2_Drawing()
        {
            devChart2.Series.Clear();
            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Copy();
                cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart2.Series.Clear();
                Series series = new Series("", ViewType.Doughnut);
                ((DoughnutSeriesView)series.View).HoleRadiusPercent = 30;

                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(cdt.Rows[i]["F_NAME"].ToString(), new double[] { Convert.ToDouble(cdt.Rows[i]["F_LOSSAMT"]) }));
                }

                series.Label.TextPattern = "{A}: {VP:p2}";

                devChart2.Titles.Clear();
                devChart2.Series.Add(series);

                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "공정별 점유율(손실금액)";
                tlt1.Font = font;
                devChart2.Titles.Add(tlt1);
                devChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                devChart2.DataSource = cdt;
                devChart2.DataBind();

            }
        }
        #endregion

        #region devGrid_HtmlRowPrepared
        protected void devGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }



        }
        #endregion

        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD4004_LST();
            devGrid.DataBind();
        }

        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            if (devGrid.Columns.Count > 0)
            {
                devGrid.Columns[0].FixedStyle = GridViewColumnFixedStyle.Left;
                devGrid.Columns[0].Width = 100;
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

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart2_Drawing();
        }
        #endregion

        #region devChart2_CustomDrawSeriesPoint
        protected void devChart2_CustomDrawSeriesPoint(object sentder, CustomDrawSeriesPointEventArgs e)
        {
            PieDrawOptions options = (PieDrawOptions)e.SeriesDrawOptions;
            GradientFillOptionsBase gradientOptions = ((GradientFillOptionsBase)options.FillStyle.Options);
            options.FillStyle.FillMode = FillMode.Gradient;
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        protected void devGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {




            if (e.DataColumn.FieldName != "구분")
            {
                e.Cell.HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                e.Cell.HorizontalAlign = HorizontalAlign.Left;
            }
        }
        #endregion

        #endregion


    }
}