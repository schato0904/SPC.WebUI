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
    public partial class WERD4005 : WebUIBasePage
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
                return (DataTable)Session["WERD4005"];
            }
            set
            {
                Session["WERD4005"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["WERD4005_1"];
            }
            set
            {
                Session["WERD4005_1"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["WERD4005_TYPE"];
            }
            set
            {
                Session["WERD4005_TYPE"] = value;
            }
        }
        DataTable dtChart4
        {
            get
            {
                return (DataTable)Session["WERD4005_2"];
            }
            set
            {
                Session["WERD4005_2"] = value;
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
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";

                /*devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                
                devChart2.JSProperties["cpFunction"] = "resizeTo";
                devChart2.JSProperties["cpChartWidth"] = "0"; */
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
            this.devCombo_Bind(this.srcF_NGTYPECD, "35");
            this.devCombo_Bind(this.srcF_NGCAUSECD, "36");
            this.rdoGBN.SelectedIndex = 0;
            this.srcF_NGTYPECD.SelectedIndex = 0;
            this.srcF_NGCAUSECD.SelectedIndex = 0;
        }
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

        #region 초기값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        {
            //this.AspxCombox_DataBind(this.srcF_PROCCD, "PP", "PPA4");
        }
        #endregion

        #region 사용자 정의 함수

        #region 그리드 목록 조회
        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD4005_LST()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_GBN", (rdoGBN.Value ?? "").ToString());
                if (rdoGBN.Value.ToString() == "35")
                {
                    oParamDic.Add("F_CODE", (srcF_NGTYPECD.Value ?? "").ToString());
                }
                else
                {
                    oParamDic.Add("F_CODE", (srcF_NGCAUSECD.Value ?? "").ToString());
                }

                ds = biz.WERD4005_LST(oParamDic, out errMsg);

            }



            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;

            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";

                    dtChart1 = null;
                    dtChart2 = null;
                    dtChart3 = null;
                    dtChart4 = null;

                }
                else
                {

                    dtChart1 = ds.Tables[0];

                    //dtChart1.Columns.Add("RATE", typeof(Decimal));
                    //dtChart1.Columns.Add("PPM", typeof(Decimal));
                    //dtChart1.Columns.Add("RATE2", typeof(Decimal));
                    //for (int i = 0; i <= dtChart1.Rows.Count - 1; i++)
                    //{
                    //    if (dtChart1.Rows[i]["F_NGCNT"].ToString() == "0")
                    //    {
                    //        dtChart1.Rows[i]["RATE"] = 0;
                    //        dtChart1.Rows[i]["PPM"] = 0;
                    //        dtChart1.Rows[i]["RATE2"] = 0;
                    //    }
                    //    else
                    //    {
                    //        dtChart1.Rows[i]["RATE"] = Math.Round(Convert.ToDecimal(dtChart1.Rows[i]["F_NGCNT"].ToString()) / Convert.ToDecimal(dtChart1.Rows[i]["F_PRODUCTQTY"].ToString()) * 100,2);
                    //        dtChart1.Rows[i]["PPM"] = Math.Round(Convert.ToInt32(dtChart1.Rows[i]["F_NGCNT"].ToString()) * 1000000 / Convert.ToDecimal(dtChart1.Rows[i]["F_PRODUCTQTY"].ToString()), 0);
                    //        dtChart1.Rows[i]["RATE2"] = Math.Round(Convert.ToDecimal(dtChart1.Rows[i]["F_NGCNT"].ToString()) / Convert.ToDecimal(dtChart1.Rows[dtChart1.Rows.Count - 1]["F_NGCNT"].ToString()) * 100,2);
                    //    }

                    //}

                    SetGrid(dtChart1);
                }

            }
        }
        #endregion

        #region SetGrid
        void SetGrid(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;


            int rocnt = ds.Tables[0].Rows.Count;
            int colcnt = ds.Tables[0].Columns.Count;





            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("구분", typeof(String));
            for (int i = 0; i < rocnt; i++)
            {
                dtTemp.Columns.Add(ds.Tables[0].Rows[i]["F_WORKDATE"].ToString(), typeof(String));
            }

            for (int j = 0; j < colcnt - 1; j++)
            {
                dtTemp.Rows.Add();

                try
                {
                    for (int i = 1; i <= rocnt; i++)
                    {
                        if (j == 3)
                            dtTemp.Rows[j][i] = ds.Tables[0].Rows[i - 1][j + 1];
                        else if (j != 3 && ds.Tables[0].Rows[i - 1][j + 1] != null)
                        {
                            dtTemp.Rows[j][i] = (Convert.ToInt32(ds.Tables[0].Rows[i - 1][j + 1])).ToString("#,##0");
                        }
                    }
                }
                catch
                {


                }
            }
            dtTemp.Rows[0][0] = "생산수";
            dtTemp.Rows[1][0] = "검사수";
            dtTemp.Rows[2][0] = "부적합수";
            dtTemp.Rows[3][0] = "부적합률(%)";
            dtTemp.Rows[4][0] = "PPM";




            dtChart2 = dtTemp;
            devGrid.DataSource = dtTemp;
            devGrid.DataBind();

        }
        #endregion

        #endregion

        #region 사용자 이벤트

        #region devcombo_bind
        void devCombo_Bind(DevExpress.Web.ASPxComboBox ComboBoxID, string Codegroup)
        {
            string errMsg = String.Empty;


            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CODEGROUP", Codegroup);

                ds = biz.WERD_SYCOD_LST(oParamDic, out errMsg);
            }

            DevExpress.Web.ASPxComboBox ddlComboBox = ComboBoxID;
            
            ddlComboBox.TextField = "F_CODENMKR";
            ddlComboBox.ValueField = "F_CODE";
            ddlComboBox.DataSource = ds.Tables[0];
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new ListEditItem("전체", ""));

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

        #region devchart1_drawing
        protected void devChart1_Drawing()
        {
            if (dtChart1 != null)
            {
                DataTable cdt = dtChart1.Copy();
                cdt.Rows.Remove(cdt.Rows[cdt.Rows.Count - 1]);

                devChart1.Series.Clear();

                if (rdoBAN.Value.ToString() == "RATE")
                    DevExpressLib.SetChartLineSeries(devChart1, "부적합률(%)", "F_WORKDATE", "F_NGRATE", System.Drawing.Color.Blue,2);
                else
                    DevExpressLib.SetChartLineSeries(devChart1, "PPM", "F_WORKDATE", "F_PPM", System.Drawing.Color.Blue, 2);

                

                devChart1.DataSource = cdt;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                if (rdoBAN.Value.ToString() == "RATE")
                    DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n2}");
                else
                    DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n0}");
                

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;


                //if (rdoGBN.Value.ToString() == "35")
                //{
                //    devChart1.Titles.Clear();
                //    ChartTitle tlt1 = new ChartTitle();
                //    System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                //    tlt1.Text = "부적합유형";
                //    tlt1.Font = font;
                //    devChart1.Titles.Add(tlt1);
                //}
                //else if (rdoGBN.Value.ToString() == "36")
                //{
                //    devChart1.Titles.Clear();
                //    ChartTitle tlt1 = new ChartTitle();
                //    System.Drawing.Font font = new System.Drawing.Font(tlt1.Font.FontFamily.Name, 12);
                //    tlt1.Text = "부적합원인";
                //    tlt1.Font = font;
                //    devChart1.Titles.Add(tlt1);
                //}
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
            WERD4005_LST();
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

        #region devGrid_HtmlDataCellPrepared
        protected void devGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName != "구분")
                e.Cell.HorizontalAlign = HorizontalAlign.Right;
        }
        #endregion

        #endregion

    }
}