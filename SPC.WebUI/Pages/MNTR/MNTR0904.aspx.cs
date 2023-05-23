using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0904 : WebUIBasePage
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
                return (DataTable)Session["MNTR090401"];
            }
            set
            {
                Session["MNTR090401"] = value;
            }
        }
        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["MNTR090402"];
            }
            set
            {
                Session["MNTR090402"] = value;
            }
        }
        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["MNTR090403"];
            }
            set
            {
                Session["MNTR090403"] = value;
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
            // hidGridColumnsWidth.Text = DevExpressLib.devGrid1ColumnWidth(devGrid1).ToString();
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
                devGrid3.JSProperties["cpResultCode"] = "";
                devGrid3.JSProperties["cpResultMsg"] = "";
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

        #region 모니터링 > 생산량조회(업체별)
        public DataSet MONITORING_MNTR0904_COMP(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                string ProcessType = ddlPROCESS.SelectedItem.Value.ToString();
                
                oParamDic.Clear();
                oParamDic.Add("F_PROCTP", ProcessType);
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                if (ProcessType.Equals("03"))
                {
                    oParamDic.Add("F_STDT", GetFromDt());
                    oParamDic.Add("F_EDDT", GetToDt());
                }
                else if (ProcessType.Equals("08"))
                {
                    oParamDic.Add("F_STDT", GetFromDt1());
                }
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_MNTR0904_COMP(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 생산량조회(품목별)
        public DataSet MONITORING_MNTR0904_ITEM(string oParam, out string errMsg)
        {
            string[] oParams = oParam.Split('|');
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                string ProcessType = ddlPROCESS.SelectedItem.Value.ToString();

                oParamDic.Clear();
                oParamDic.Add("F_PROCTP", ProcessType);
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("T_COMPCD", oParams[0]);
                oParamDic.Add("T_FACTCD", oParams[1]);
                if (ProcessType.Equals("03"))
                {
                    oParamDic.Add("F_STDT", GetFromDt());
                    oParamDic.Add("F_EDDT", GetToDt());
                }
                else if (ProcessType.Equals("08"))
                {
                    oParamDic.Add("F_STDT", GetFromDt1());
                }
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_MNTR0904_ITEM(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 생산량조회(항목별)
        public DataSet MONITORING_MNTR0904_INSP(string oParam, out string errMsg)
        {
            string[] oParams = oParam.Split('|');
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                string ProcessType = ddlPROCESS.SelectedItem.Value.ToString();

                oParamDic.Clear();
                oParamDic.Add("F_PROCTP", ProcessType);
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("T_COMPCD", oParams[0]);
                oParamDic.Add("T_FACTCD", oParams[1]);
                oParamDic.Add("T_ITEMCD", oParams[2]);
                if (ProcessType.Equals("03"))
                {
                    oParamDic.Add("F_STDT", GetFromDt());
                    oParamDic.Add("F_EDDT", GetToDt());
                }
                else if (ProcessType.Equals("08"))
                {
                    oParamDic.Add("F_STDT", GetFromDt1());
                }
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_MNTR0904_INSP(oParamDic, out errMsg);
            }

            return ds;
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

            if (dtChart1 != null)
            {
                devChart1.Series.Clear();

                Decimal maxAxisY = Decimal.MinValue;
                Decimal minAxisY = Decimal.MaxValue;

                foreach (DataRow dtRow in dtChart1.Rows)
                {
                    maxAxisY = Math.Max(maxAxisY, Convert.ToDecimal(dtRow["NG_RATE"]));
                    minAxisY = Math.Min(minAxisY, Convert.ToDecimal(dtRow["NG_RATE"]));
                }

                maxAxisY = maxAxisY == Decimal.MinValue ? 0 : maxAxisY;
                minAxisY = minAxisY == Decimal.MaxValue ? 0 : minAxisY;

                DevExpressLib.SetChartBarLineSeries(devChart1, "NG(%)", "F_COMPNM", "NG_RATE", System.Drawing.Color.LightBlue);

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetChartLegend(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n0}%");
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

                DevExpressLib.SetChartPieSeries(devChart2, "NG(%)", "F_ITEMCD", "NG_RATE", "{A}: {V:n4}%");

                devChart2.DataSource = dtChart2;
                devChart2.DataBind();

                DevExpressLib.SetChartLegend(devChart2);
                DevExpressLib.SetChartTitle(devChart2, String.Format("{0} 품목별 Worst 10", hidCOMPNM.Text));
                //DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, 0, 0, null, "{V:n0}%");
            }
            else
            {
                devChart2.Series.Clear();
                devChart2.Titles.Clear();

                dtChart2 = null;
                devChart2.DataSource = dtChart2;
                devChart2.DataBind();
            }
        }
        #endregion

        #region devChart3_CustomCallback
        /// <summary>
        /// devChart3_CustomCallback
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

                DevExpressLib.SetChartPieSeries(devChart3, "NG(%)", "F_INSPNM", "NG_RATE", "{A}: {V:n4}%");

                devChart3.DataSource = dtChart3;
                devChart3.DataBind();

                DevExpressLib.SetChartLegend(devChart3);
                DevExpressLib.SetChartTitle(devChart3, String.Format("{0} 항목별 Worst 10", hidITEMNM.Text));
                //DevExpressLib.SetChartDiagram(devChart2, false, 0, 0, 0, 0, null, "{V:n0}%");
            }
            else
            {
                devChart3.Series.Clear();
                devChart3.Titles.Clear();

                dtChart3 = null;
                devChart3.DataSource = dtChart2;
                devChart3.DataBind();
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

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            DataSet ds = MONITORING_MNTR0904_COMP(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    // Grid Callback Init
                    devGrid1.JSProperties["cpResultCode"] = "0";
                    devGrid1.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    DataRow[] drInsp;

                    DataTable dtList = new DataTable();
                    dtList.Columns.Add("F_COMPCD", typeof(String));
                    dtList.Columns.Add("F_COMPNM", typeof(String));
                    dtList.Columns.Add("F_FACTCD", typeof(String));
                    dtList.Columns.Add("ALL_CNT", typeof(Int32));
                    dtList.Columns.Add("OK_CNT", typeof(Int32));
                    dtList.Columns.Add("OC_CNT", typeof(Int32));
                    dtList.Columns.Add("NG_CNT", typeof(Int32));
                    dtList.Columns.Add("NG_RATE", typeof(Decimal));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dtNewRow = dtList.NewRow();
                        dtNewRow["F_COMPCD"] = dr["F_COMPCD"];
                        dtNewRow["F_COMPNM"] = dr["F_COMPNM"];
                        dtNewRow["F_FACTCD"] = "01";

                        // 자주
                        drInsp = ds.Tables[1].Select(String.Format("F_COMPCD='{0}'", dr["F_COMPCD"]));
                        if (drInsp.Length > 0)
                        {
                            foreach (DataRow dr1 in drInsp)
                            {
                                dtNewRow["ALL_CNT"] = Convert.ToInt32(dr1["OK_CNT"]) + Convert.ToInt32(dr1["OC_CNT"]) + Convert.ToInt32(dr1["NG_CNT"]);
                                dtNewRow["OK_CNT"] = Convert.ToInt32(dr1["OK_CNT"]);
                                dtNewRow["OC_CNT"] = Convert.ToInt32(dr1["OC_CNT"]);
                                dtNewRow["NG_CNT"] = Convert.ToInt32(dr1["NG_CNT"]);
                                dtNewRow["NG_RATE"] = Math.Round((Convert.ToDecimal(dr1["NG_CNT"]) * 100) / Convert.ToDecimal(dtNewRow["ALL_CNT"]), 4);
                            }
                        }
                        else
                        {
                            dtNewRow["ALL_CNT"] = 0;
                            dtNewRow["OK_CNT"] = 0;
                            dtNewRow["OC_CNT"] = 0;
                            dtNewRow["NG_CNT"] = 0;
                            dtNewRow["NG_RATE"] = 0;
                        }

                        dtList.Rows.Add(dtNewRow);
                    }

                    dtChart1 = dtList;

                    devGrid1.DataSource = dtList;
                    devGrid1.DataBind();
                }
            }
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("clear"))
            {
                dtChart2 = null;
                
                devGrid2.DataSource = null;
                devGrid2.DataBind();
            }
            else
            {
                string errMsg = String.Empty;

                DataSet ds = MONITORING_MNTR0904_ITEM(e.Parameters, out errMsg);

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        // Grid Callback Init
                        devGrid2.JSProperties["cpResultCode"] = "0";
                        devGrid2.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                    }
                    else
                    {
                        DataTable dtList = new DataTable();
                        dtList.Columns.Add("F_COMPCD", typeof(String));
                        dtList.Columns.Add("F_FACTCD", typeof(String));
                        dtList.Columns.Add("F_ITEMCD", typeof(String));
                        dtList.Columns.Add("F_ITEMNM", typeof(String));
                        dtList.Columns.Add("ALL_CNT", typeof(Int32));
                        dtList.Columns.Add("OK_CNT", typeof(Int32));
                        dtList.Columns.Add("OC_CNT", typeof(Int32));
                        dtList.Columns.Add("NG_CNT", typeof(Int32));
                        dtList.Columns.Add("NG_RATE", typeof(Decimal));

                        int i = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (i > 9) break;

                            DataRow dtNewRow = dtList.NewRow();
                            dtNewRow["F_COMPCD"] = dr["F_COMPCD"];
                            dtNewRow["F_FACTCD"] = dr["F_FACTCD"];
                            dtNewRow["F_ITEMCD"] = dr["F_ITEMCD"];
                            dtNewRow["F_ITEMNM"] = dr["F_ITEMNM"];
                            dtNewRow["ALL_CNT"] = Convert.ToInt32(dr["ALL_CNT"]);
                            dtNewRow["OK_CNT"] = Convert.ToInt32(dr["OK_CNT"]);
                            dtNewRow["OC_CNT"] = Convert.ToInt32(dr["OC_CNT"]);
                            dtNewRow["NG_CNT"] = Convert.ToInt32(dr["NG_CNT"]);
                            dtNewRow["NG_RATE"] = Math.Round(Convert.ToDecimal(dr["NG_RATE"]), 4);
                            dtList.Rows.Add(dtNewRow);

                            i++;
                        }

                        dtChart2 = dtList;

                        devGrid2.DataSource = ds.Tables[0];
                        devGrid2.DataBind();
                    }
                }
            }
        }
        #endregion

        #region devGrid3 CustomCallback
        /// <summary>
        /// devGrid3_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid3_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("clear"))
            {
                dtChart3 = null;

                devGrid3.DataSource = null;
                devGrid3.DataBind();
            }
            else
            {
                string errMsg = String.Empty;

                DataSet ds = MONITORING_MNTR0904_INSP(e.Parameters, out errMsg);

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGrid3.JSProperties["cpResultCode"] = "0";
                    devGrid3.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (!bExistsDataSet(ds))
                    {
                        // Grid Callback Init
                        devGrid3.JSProperties["cpResultCode"] = "0";
                        devGrid3.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                    }
                    else
                    {
                        DataTable dtList = new DataTable();
                        dtList.Columns.Add("F_COMPCD", typeof(String));
                        dtList.Columns.Add("F_FACTCD", typeof(String));
                        dtList.Columns.Add("F_ITEMCD", typeof(String));
                        dtList.Columns.Add("F_ITEMNM", typeof(String));
                        dtList.Columns.Add("F_INSPCD", typeof(String));
                        dtList.Columns.Add("F_INSPNM", typeof(String));
                        dtList.Columns.Add("ALL_CNT", typeof(Int32));
                        dtList.Columns.Add("OK_CNT", typeof(Int32));
                        dtList.Columns.Add("OC_CNT", typeof(Int32));
                        dtList.Columns.Add("NG_CNT", typeof(Int32));
                        dtList.Columns.Add("NG_RATE", typeof(Decimal));

                        int i = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (i > 9) break;

                            DataRow dtNewRow = dtList.NewRow();
                            dtNewRow["F_COMPCD"] = dr["F_COMPCD"];
                            dtNewRow["F_FACTCD"] = dr["F_FACTCD"];
                            dtNewRow["F_ITEMCD"] = dr["F_ITEMCD"];
                            dtNewRow["F_ITEMNM"] = dr["F_ITEMNM"];
                            dtNewRow["F_INSPCD"] = dr["F_INSPCD"];
                            dtNewRow["F_INSPNM"] = dr["F_INSPNM"];
                            dtNewRow["ALL_CNT"] = Convert.ToInt32(dr["ALL_CNT"]);
                            dtNewRow["OK_CNT"] = Convert.ToInt32(dr["OK_CNT"]);
                            dtNewRow["OC_CNT"] = Convert.ToInt32(dr["OC_CNT"]);
                            dtNewRow["NG_CNT"] = Convert.ToInt32(dr["NG_CNT"]);
                            dtNewRow["NG_RATE"] = Math.Round(Convert.ToDecimal(dr["NG_RATE"]), 4);
                            dtList.Rows.Add(dtNewRow);

                            i++;
                        }

                        dtChart3 = dtList;

                        devGrid3.DataSource = ds.Tables[0];
                        devGrid3.DataBind();
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}