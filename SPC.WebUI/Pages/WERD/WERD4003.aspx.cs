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
using System.Text;
using DevExpress.XtraCharts;



namespace SPC.WebUI.Pages.WERD
{




    public partial class WERD4003 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언

        private DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string strRateGbn = "";
        #endregion

        #region 프로퍼티
        DataSet dtChart1
        {
            get
            {
                return (DataSet)Session["WERD4003"];
            }
            set
            {
                Session["WERD4003"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD4003_1"];
            }
            set
            {
                Session["WERD4003_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD4003_TYPE"];
            }
            set
            {
                Session["WERD4003_TYPE"] = value;
            }
        }
        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["WERD4003_2"];
            }
            set
            {
                Session["WERD4003_2"] = value;
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
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devGrid3.JSProperties["cpResultCode"] = "";
                devGrid3.JSProperties["cpResultMsg"] = "";

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0";
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
        void WERD4003_LST()
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
                ds = biz.WERD4003_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds.Tables[1];
            devGrid3.DataSource = ds.Tables[2];

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;

                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;

                devGrid3.JSProperties["cpResultCode"] = "0";
                devGrid3.JSProperties["cpResultMsg"] = errMsg;

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
                    devGrid.DataSource = SetGrid(ds);
                    devGrid.DataBind();
                    devGrid2.DataBind();
                    devGrid3.DataBind();


                    dtChart1 = ds;
                }

            }





        }
        #endregion

        #region SetGrid

        private DataTable SetGrid(DataSet ds)
        {
            DataTable NewDt = new DataTable();
            if (!(ds.Tables[0].Rows.Count == 0))
            {
                int colcnt = ds.Tables[0].Columns.Count;




                NewDt.Columns.Add("F_GUBUN");
                NewDt.Columns.Add("F_CNT");




                for (int i = 0; i < 5; i++)
                {
                    NewDt.Rows.Add();
                }





                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        string[] arrGubun = { "생산수", "검사수", "부적합수", "부적합률(%)", "PPM" };

                        for (int z = 0; z < 5; z++)
                        {
                            NewDt.Rows[z]["F_GUBUN"] = arrGubun[z];
                        }
                    }


                }

                for (int j = 0; j < colcnt; j++)
                {

                    if (j != 3)
                        NewDt.Rows[j][1] = (Convert.ToInt32(ds.Tables[0].Rows[0][j])).ToString("#,##0");
                    else if (j == 3)
                    {
                        NewDt.Rows[j][1] = ds.Tables[0].Rows[0][j];
                    }
                }





            }

            return NewDt;
        }




        #endregion

        #endregion

        #region 사용자이벤트

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, double Width, double Height)
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
                DataTable cdt = dtChart1.Tables[1].Copy();

                devChart1.Series.Clear();

                DevExpressLib.SetChartBarLineSeries(devChart1, "부적합유형수량", "F_CODENMKR", "F_cnt", System.Drawing.Color.SkyBlue);


                devChart1.DataSource = cdt;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null);

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                diagram.AxisY.NumericScaleOptions.AutoGrid = false;
                diagram.AxisY.NumericScaleOptions.GridSpacing = 1;
                diagram.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;




                devChart1.Titles.Clear();
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "부적합유형";
                tlt1.Font = font;
                devChart1.Titles.Add(tlt1);
            }
        }
        #endregion

        #region devChart2_Drawing
        protected void devChart2_Drawing()
        {
            devChart2.Series.Clear();
            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Tables[2].Copy();
                //cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart2.Series.Clear();

                DevExpressLib.SetChartBarLineSeries(devChart2, "부적합원인수량", "F_CODENMKR", "F_CNT", System.Drawing.Color.LightGreen);

                devChart2.DataSource = cdt;
                devChart2.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart2);
                DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, null, null, null);

                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                diagram.AxisY.NumericScaleOptions.AutoGrid = false;
                diagram.AxisY.NumericScaleOptions.GridSpacing = 1;
                diagram.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;



                devChart2.Titles.Clear();
                ChartTitle tlt1 = new ChartTitle();
                System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                tlt1.Text = "부적합원인";
                tlt1.Font = font;
                devChart2.Titles.Add(tlt1);

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

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD4003_LST();
            //devGrid.DataBind();
        }
        #endregion

        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToDouble(oParams[0]), Convert.ToDouble(oParams[1]));
            devChart1_Drawing();
        }
        #endregion

        #region devChart2_CustomCallback
        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToDouble(oParams[0]), Convert.ToDouble(oParams[1]));
            devChart2_Drawing();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            int row = e.VisibleRowIndex;
            string fieldName = e.Column.FieldName;
            //string fStep = devGrid.GetRowValues(row, "F_TYPE").ToString();


            return;

        }
        #endregion



        #region devGridExporter_RenderBrick
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<br />", String.Empty);
            }

            if (e.RowType == GridViewRowType.Footer)
            {
                e.Text = e.Text.Replace("<br/>", Environment.NewLine);
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            WERD4003_LST();
        }
        #endregion

        protected void devGrid2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD4003_LST();
        }

        #endregion

        protected void devGrid3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD4003_LST();
        }

    }
}