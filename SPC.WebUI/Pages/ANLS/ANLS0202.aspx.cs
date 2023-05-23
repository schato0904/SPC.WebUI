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
    public partial class ANLS0202 : WebUIBasePage
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
                return (DataTable)Session["ANLS0202"];
            }
            set
            {
                Session["ANLS0202"] = value;
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
                // Grid Callback Init
                devGridWork.JSProperties["cpResultCode"] = "";
                devGridWork.JSProperties["cpResultMsg"] = "";
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
            this.rdoGBN.SelectedIndex = 0;
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
        {            
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공정리스트조회
        void ANLS0202_WORK_LST()
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());                
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                ds = biz.ANLS0202_WORK_LST(oParamDic, out errMsg);
            }

            devGridWork.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGridWork.JSProperties["cpResultCode"] = "0";
                devGridWork.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGridWork.DataBind();
            }
        }
        #endregion

        #region 공정능력조회
        void ANLS0202_LST()
        {
            string errMsg = String.Empty;

            using (ANLSBiz biz = new ANLSBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_SERIALNO", this.txtSERIALNO.Value.ToString());
                oParamDic.Add("F_CNT", this.txtCNT.Value.ToString());
                ds = biz.ANLS0202_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
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
                    DataTable dt = PivotDataSet(ds);


                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = dt.Columns[i].ColumnName, Caption = "검사항목", Width = Unit.Parse("80"), Visible = false };
                            devGrid.Columns.Add(column);
                        }
                        else if (i == 1)
                        {
                            DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = dt.Columns[i].ColumnName, Caption = "검사항목", Width = Unit.Parse("80") };
                            devGrid.Columns.Add(column);
                        }
                        else
                        {
                            DevExpress.Web.GridViewDataColumn column = new DevExpress.Web.GridViewDataColumn() { FieldName = dt.Columns[i].ColumnName, Caption = dt.Columns[i].ColumnName.Substring(2), Width = Unit.Parse("80") };
                            devGrid.Columns.Add(column);
                        }
                    }

                    devGrid.DataSource = dt;

                    devGrid.DataBind();
                    dtChart1 = dt;
                }            
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region PivotDataSet
        DataTable PivotDataSet(DataSet ds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("F_SERIALNO");
            dt.Columns.Add("F_INSPDETAIL");

            string strDate = "";
            int RowCnt = ds.Tables[1].Rows.Count;
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["F_SERIALNO"] = ds.Tables[1].Rows[i]["F_SERIALNO"].ToString();
                dt.Rows[i]["F_INSPDETAIL"] = ds.Tables[1].Rows[i]["F_INSPDETAIL"].ToString();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {                
                if (i == 0)
                {
                    dt.Columns.Add("dt" + ds.Tables[0].Rows[i]["F_WORKDATE"]);
                    dt.Rows[i % RowCnt]["dt" + ds.Tables[0].Rows[i]["F_WORKDATE"]] = ds.Tables[0].Rows[i][this.rdoGBN.SelectedItem.Value.ToString().Equals("1") ? "F_CP" : "F_CPK"].ToString();
                }
                else
                {
                    strDate = ds.Tables[0].Rows[i-1]["F_WORKDATE"].ToString();

                    if (strDate == ds.Tables[0].Rows[i]["F_WORKDATE"].ToString())
                    {
                        dt.Rows[i % RowCnt]["dt" + ds.Tables[0].Rows[i]["F_WORKDATE"]] = ds.Tables[0].Rows[i][this.rdoGBN.SelectedItem.Value.ToString().Equals("1") ? "F_CP" : "F_CPK"].ToString();
                    }
                    else
                    {
                        dt.Columns.Add("dt" + ds.Tables[0].Rows[i]["F_WORKDATE"]);
                        dt.Rows[i % RowCnt]["dt" + ds.Tables[0].Rows[i]["F_WORKDATE"]] = ds.Tables[0].Rows[i][this.rdoGBN.SelectedItem.Value.ToString().Equals("1") ? "F_CP" : "F_CPK"].ToString();
                    }
                }
            }
            return dt;
        }
        #endregion

        #region devChart2_CustomCallback
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
                DataTable dt = new DataTable();
                dt.Columns.Add("F_WORKDATE");

                int CoumnCnt = dtChart1.Columns.Count;
                int RowCnt = dtChart1.Rows.Count;
                string[] strInspdetail = new string[RowCnt];

                for (int i = 0; i < RowCnt; i++)
                {
                    dt.Columns.Add("F_SERIALNO"+i, typeof(Double));
                    strInspdetail[i] = dtChart1.Rows[i]["F_INSPDETAIL"].ToString();
                }

                for (int i = 0; i < CoumnCnt - 2; i++)
                {
                    dt.Rows.Add();
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if(j==0){
                            dt.Rows[i][j] = dtChart1.Columns[i + 2].ColumnName.Substring(2);
                        }
                        else {
                            if (dtChart1.Rows[j-1][i + 2].ToString() == "")
                                dt.Rows[i][j] = DBNull.Value;
                            else
                                dt.Rows[i][j] = dtChart1.Rows[j-1][i + 2].ToString();
                        }
                    }                    
                }

                devChart1.Series.Clear();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if(i > 0)
                        DevExpressLib.SetChartLineSeries(devChart1, strInspdetail[i-1], "F_WORKDATE", dt.Columns[i].ColumnName);
                }
                
                devChart1.DataSource = dt;
                devChart1.DataBind();

                DevExpressLib.SetCrosshairOptions(devChart1);
                DevExpressLib.SetChartDiagram(devChart1, false, 0, 0, 0, 0, null, "{V:n4}");
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

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            
        }
        #endregion

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
          
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
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
            ANLS0202_LST();
        }
        #endregion       

        #region devGridWork CustomCallback
        /// <summary>
        /// devGridWork_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGridWork_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ANLS0202_WORK_LST();
        }
        #endregion   

        #endregion
    }
}