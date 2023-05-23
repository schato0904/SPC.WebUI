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
using SPC.ADTR.Biz;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0703 : WebUIBasePage
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
                return (DataTable)Session["ADTR0703_1"];
            }
            set
            {
                Session["ADTR0703_1"] = value;
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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
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

        #endregion

        #region 사용자이벤트

        DataSet ADTR0703_QWK03E_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                ds = biz.ADTR0703_QWK03E_LST(oParamDic, out errMsg);
            }            

            return ds;
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
                
                for (int i = 1; i < dtChart1.Columns.Count; i++)
                {
                    DevExpressLib.SetChartLineSeries(devChart1, dtChart1.Columns[i].ColumnName, "F_WORKDATE", dtChart1.Columns[i].ColumnName);
                    
                }
                
                devChart1.DataSource = dtChart1;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                //DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, maxAxisY, minAxisY, null, "{V:n4}");
                //DevExpressLib.SetChartTitle(devChart1, Convert.ToInt32(txtSIRYO.Text) <= 1 ? "X 관리도" : "X-Bar 관리도", false);
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

        protected DataTable SetGrid(DataSet Ds1)
        {
            DataSet Chartds = Ds1;

            DataTable dt = new DataTable();
            dt.Columns.Add("F_WORKDATE");
            for (int i = 0; i < Chartds.Tables[1].Rows.Count; i++)
            {
                dt.Columns.Add(Chartds.Tables[1].Rows[i]["F_NGDETAIL"].ToString(), typeof(double));
            }

            for (int i = 0; i < Chartds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["F_WORKDATE"] = Chartds.Tables[0].Rows[i]["F_WORKDATE"].ToString();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < Chartds.Tables[2].Rows.Count; j++)
                {
                    if (dt.Rows[i]["F_WORKDATE"].ToString() == Chartds.Tables[2].Rows[j]["F_WORKDATE"].ToString())
                    {
                        for (int x = 0; x < dt.Columns.Count; x++)
                        {
                            if (dt.Columns[x].ColumnName == Chartds.Tables[2].Rows[j]["F_NGDETAIL"].ToString())
                            {
                                dt.Rows[i][dt.Columns[x].ColumnName] = Chartds.Tables[2].Rows[j]["F_NGCOUNT"].ToString();
                            }
                        }
                    }
                }
            }
            dtChart1 = dt;

            return dt;
        }

        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;

            devGrid.JSProperties["cpResult1"] = "";
            devGrid.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", GetCompCD());
            oParamDic.Add("F_FACTCD", GetFactCD());
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());
            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_BANCD", GetBanCD());
            oParamDic.Add("F_LINECD", GetLineCD());

            // 검사규격을 구한다
            using (ADTRBiz biz = new ADTRBiz())
            {
                ds = biz.ADTR0703_QWK03E_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "조회 조건에 맞는 규격데이타가 없습니다";
                }
                else
                {
                    bExecute1 = true;

                    DataTable dtPivotTable = SetGrid(ds);
                    DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = "F_WORKDATE", Caption = "일자", Width = Unit.Parse("100") };
                    devGrid.Columns.Add(column);
                    for (int i = 1; i < dtPivotTable.Columns.Count; i++)
                    {
                        DevExpress.Web.GridViewDataColumn column1 = new DevExpress.Web.GridViewDataColumn() { FieldName = dtPivotTable.Columns[i].ColumnName, Caption = dtPivotTable.Columns[i].ColumnName, Width = Unit.Parse("150") };
                        devGrid.Columns.Add(column1);    
                    }

                    devGrid.DataSource = dtPivotTable;
                    devGrid.DataBind();

                }
            }

        }

        #endregion
    }
}