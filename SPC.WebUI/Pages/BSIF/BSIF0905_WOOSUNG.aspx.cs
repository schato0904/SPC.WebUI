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
using SPC.BSIF.Biz;
using DevExpress.XtraCharts;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0905_WOOSUNG : WebUIBasePage
    {

        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        DataSet dt = null;
        private DBHelper spcDB;
        string[] procResult = { "2", "Unknown Error" };

        int a = 0;
        int b = 0;
        #endregion

        #region 프로퍼티



        DataSet dtChart1
        {
            get
            {
                return (DataSet)Session["BSIF0905_WOOSUNG"];
            }
            set
            {
                Session["BSIF0905_WOOSUNG"] = value;
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

                devChart1.JSProperties["cpFunction"] = "resizeTo";
                devChart1.JSProperties["cpChartWidth"] = "0";


            }

            GetYear();
            GetWorker();
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
        { }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            dtChart1 = null;
            this.yearCOMBO.SelectedIndex = 0;
            //this.workerCOMBO.SelectedIndex = 0;




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


        public DataSet BSIF0905_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet dt = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                dt = spcDB.GetDataSet("USP_BSIF0905_LST", out errMsg);
            }

            return dt;
        }


        #region BSIF0905_LST
        void BSIF0905_LST()
        {




            string errMsg = String.Empty;


            devChart1.Series.Clear();


            DataSet dt = null;
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_YY", this.yearCOMBO.SelectedItem.Value.ToString());
            oParamDic.Add("F_WORKMANCD", this.workerCOMBO.SelectedItem.Value.ToString());//ds.Tables[0].Rows[0][0].ToString()
            dt = BSIF0905_LST(oParamDic, out errMsg);


            if (!String.IsNullOrEmpty(errMsg))
            {
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = errMsg;

            }

            else
            {

                if (!bExistsDataSet(dt))
                {
                    devChart1.JSProperties["cpResultCode"] = "0";
                    devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {


                    DataTable dt2 = new DataTable();

                    dt2.Columns.Add("F_MONTH");


                    string[] str = new string[dt.Tables[0].Rows.Count];

                    for (int i = 1; i < 13; i++)
                    {
                        dt2.Rows.Add();
                        dt2.Rows[i - 1][0] = i + "월";
                    }



                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {

                        dt2.Columns.Add(dt.Tables[0].Rows[i][0].ToString(), typeof(Double));
                        str[i] = dt.Tables[0].Rows[i]["F_WORKMAN"].ToString();
                    }






                    for (int i = 0; i < dt.Tables[0].Columns.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Tables[0].Rows.Count; j++)
                        {
                            dt2.Rows[i][j + 1] = dt.Tables[0].Rows[j][i + 1];
                        }
                    }


                    for (int i = 0; i < dt2.Columns.Count - 1; i++)
                    {


                        if (i > 0)
                        {
                            DevExpressLib.SetChartLineSeries(devChart1, str[i - 1], "F_MONTH", dt2.Columns[i].ColumnName, 3);
                        }

                        if (i == 0)
                        {
                            DevExpressLib.SetChartLineSeries(devChart1, str[dt2.Columns.Count - 2], "F_MONTH", dt2.Columns[dt2.Columns.Count - 1].ColumnName, System.Drawing.Color.Black, 6);
                        }
                    }



                    devChart1.DataSource = dt2;
                    devChart1.DataBind();

                    DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, null, null, null, "{V:n0}");
                    XYDiagram diagram = (XYDiagram)devChart1.Diagram;

                    diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 1;
                    diagram.AxisX.GridLines.Visible = true;
                    diagram.AxisY.GridLines.Visible = true;
                    //diagram.AxisX.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;
                }
            }
        }
        #endregion






        #region devChart_ResizeTo
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

        #region BSIF_WORKER_LST
        public DataSet BSIF_WORKER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0905_WOOSUNG_WORKER_LST", out errMsg);
            }
            return ds;
        }
        #endregion

        #region GetWorker
        void GetWorker()
        {
            string errMsg = String.Empty;

            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_YY", this.yearCOMBO.SelectedItem.Value.ToString());
            ds = BSIF_WORKER_LST(oParamDic, out errMsg);

            workerCOMBO.DataSource = ds;
            workerCOMBO.DataBind();
            workerCOMBO.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #region yearCOMBO_Callback
        protected void yearCOMBO_Callback(object sender, CallbackEventArgsBase e)
        {
            GetYear();
        }
        #endregion

        #region workerCOMBO_Callback
        protected void workerCOMBO_Callback(object sender, CallbackEventArgsBase e)
        {
            GetWorker();
        }
        #endregion



        #region devChart1_CustomCallback
        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {


            string[] oParams = e.Parameter.Split('|');



            if (oParams[0] != "" && oParams[0] != "0")
            {
                devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            }
         
                



            BSIF0905_LST();

        }
        #endregion



    }
}