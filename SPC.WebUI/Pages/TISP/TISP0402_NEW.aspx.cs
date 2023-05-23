using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.TISP.Biz;
using DevExpress.XtraCharts;

namespace SPC.WebUI.Pages.TISP
{
    public partial class TISP0402_NEW : WebUIBasePage
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
                return (DataTable)Session["TISP0402"];
            }
            set
            {
                Session["TISP0402"] = value;
            }
        }
        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["TISP0402_1"];
            }
            set
            {
                Session["TISP0402_1"] = value;
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

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                //ucPager.TotalItems = 0;
                //ucPager.PagerDataBind();
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
            //AspxCombox_DataBind(this.ddlINSPCD, "AAC5");
            //AspxCombox_DataBind(this.ddlJUDGE, "AAE2");
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
        void QWK08A_TISP0402_LST_FOR_NSUMDATE()
        {
            string errMsg = String.Empty;

            using (TISPBiz biz = new TISPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMYM", GetFromDt());
                oParamDic.Add("F_TOYM", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Text);
                ds = biz.QWK08A_TISP0402_LST_FOR_NSUMDATE(oParamDic, out errMsg);
            }

            DataTable dtTemp = ds.Tables[0].Clone();

            string sFormat = "0";
            int nPoint = 0;
            DataRow dtNewRow = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                nPoint = Convert.ToInt32(dr["F_FREEPOINT"]);
                sFormat = "0";

                if (nPoint > 0)
                {
                    sFormat += ".";
                    for (int i = 0; i < nPoint - 1; i++)
                    {
                        sFormat += "#";
                    }
                    sFormat += "0";
                }

                dtNewRow = dtTemp.NewRow();

                dtNewRow["구분"] = dr["구분"];

                if (dr.IsNull("XBAR")
                    || dr.IsNull("R")
                    || dr.IsNull("CP")
                    || dr.IsNull("CPK")
                    || dr.IsNull("수율(%)")
                    || dr.IsNull("부적합율")
                    || dr.IsNull("PPM"))
                {
                    dtNewRow["XBAR"] = DBNull.Value;
                    dtNewRow["R"] = DBNull.Value;
                    dtNewRow["CP"] = DBNull.Value;
                    dtNewRow["CPK"] = DBNull.Value;
                    dtNewRow["수율(%)"] = DBNull.Value;
                    dtNewRow["부적합율"] = DBNull.Value;
                    dtNewRow["PPM"] = DBNull.Value;
                }
                else
                {
                    dtNewRow["XBAR"] = Convert.ToDecimal(dr["XBAR"]).ToString(sFormat);
                    dtNewRow["R"] = Convert.ToDecimal(dr["R"]).ToString(sFormat);
                    dtNewRow["CP"] = dr["CP"];
                    dtNewRow["CPK"] = dr["CPK"];
                    dtNewRow["수율(%)"] = Convert.ToDecimal(dr["수율(%)"]).ToString(sFormat);
                    dtNewRow["부적합율"] = Convert.ToDecimal(dr["부적합율"]).ToString(sFormat);
                    dtNewRow["PPM"] = Convert.ToDecimal(dr["PPM"]).ToString("0,##0");
                }
                dtTemp.Rows.Add(dtNewRow);
            }

            dtTemp.Columns.Remove("F_FREEPOINT");

            dtChart1 = dtTemp.Copy();

            // Pivot Fill
            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(dtTemp, "구분");

            dtGrid1 = dtPivotTable;
            devGrid.DataSource = dtPivotTable;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid.DataBind();
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

            if (dtChart1 == null)
            {
                QWK08A_TISP0402_LST_FOR_NSUMDATE();
            }

            if (dtChart1 != null)
            {
                devChart1.Series.Clear();
                if (oParams.Length > 2)
                {
                    switch (oParams[2].ToString())
                    {
                        case "0":
                            DevExpressLib.SetChartLineSeries(devChart1, "XBAR", "구분", "XBAR", System.Drawing.Color.LightCoral, 2, null);
                            break;
                        case "1":
                            DevExpressLib.SetChartLineSeries(devChart1, "R", "구분", "R", System.Drawing.Color.Orange, 2, null);
                            break;
                        case "2":
                            DevExpressLib.SetChartLineSeries(devChart1, "CP", "구분", "CP", System.Drawing.Color.Blue, 2, null);
                            break;
                        case "3":
                            DevExpressLib.SetChartLineSeries(devChart1, "CPk", "구분", "CPk", System.Drawing.Color.CadetBlue, 2, null);
                            break;
                        case "4":
                            DevExpressLib.SetChartLineSeries(devChart1, "수율(%)", "구분", "수율(%)", System.Drawing.Color.BlueViolet, 2, null);
                            break;
                        case "5":
                            DevExpressLib.SetChartLineSeries(devChart1, "부적합율", "구분", "부적합율", System.Drawing.Color.Brown, 2, null);
                            break;
                        case "6":
                            DevExpressLib.SetChartLineSeries(devChart1, "PPM", "구분", "PPM", System.Drawing.Color.Green, 2, null);
                            break;
                        default:
                            DevExpressLib.SetChartLineSeries(devChart1, "CP", "구분", "CP", System.Drawing.Color.Blue, 2, null);
                            break;
                    }
                }
                else
                {
                    DevExpressLib.SetChartLineSeries(devChart1, "CP", "구분", "CP", System.Drawing.Color.Blue, 2, null);
                }

                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);

                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
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

        #region txtMEAINSPCD_Init
        /// <summary>
        /// txtMEAINSPCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtMEAINSPCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupMeainspSearch()");
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK08A_TISP0402_LST_FOR_NSUMDATE();
        }

        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dtGrid1 == null)
            {
                QWK08A_TISP0402_LST_FOR_NSUMDATE();
            }

            devGrid.DataSource = dtGrid1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 월별경향분석", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}