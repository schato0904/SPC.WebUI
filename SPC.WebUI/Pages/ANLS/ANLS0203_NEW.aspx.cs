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
using SPC.WebUI.Common.Library;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.ANLS
{
    public partial class ANLS0203_NEW : WebUIBasePage
    {

        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        private DBHelper spcDB;
        #endregion

        #region 프로퍼티
        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["ANLS0203_NEW1"];
            }
            set
            {
                Session["ANLS0203_NEW1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["ANLS0203_NEW2"];
            }
            set
            {
                Session["ANLS0203_NEW2"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["ANLS0203_NEW3"];
            }
            set
            {
                Session["ANLS0203_NEW3"] = value;
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

            }

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

            if (oParams[2] == "resize")
                devChart1.JSProperties["cpResult1"] = "";
            else
            {

                ChartData();
                DrawHistogram();
            }
        }
        #endregion
        
        #region GetDataHistogram
        public DataSet GetDataHistogram(Dictionary<string, string> dicParam)
        {
            DataSet ds = new DataSet();
            string errMsg;
            
            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(dicParam);
                ds = spcDB.GetDataSet("USP_ANLS0203_1", out errMsg);
            }
            return ds;
        }
        #endregion
        
        #region DrawHistogram
        void DrawHistogram()
        {
            bool bExecute1 = false;
            bool bExecute2 = false;
            StringBuilder sb = null;
            DataTable dt3 = new DataTable();
            string errMsg = String.Empty;

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", "0");
                        
            SPCChart sc = new SPCChart();
            DataSet ds = GetDataHistogram(oParamDic);
            var c = sc.GetHistogram(this.devChart1, ds);
            
            if (c == null)
            {
                devChart1.JSProperties["cpResultCode"] = "0";
                devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 데이터가 없습니다.";
            }
            else
            {
                this.devChart1 = c;
            }            
        }
        #endregion
        
        #region ChartData
        void ChartData()
        {
            string errMsg = String.Empty;
            bool bExecute1 = false;
            bool bExecute2 = false;
            bool bExecute3 = false;
            StringBuilder sb = null;

            devChart1.JSProperties["cpResult1"] = "";
            devChart1.JSProperties["cpResult2"] = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_STDT", GetFromDt());
            oParamDic.Add("F_EDDT", GetToDt());

            oParamDic.Add("F_ITEMCD", GetItemCD());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD());
            oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
            oParamDic.Add("F_SIRYO", txtSIRYO.Text);
            oParamDic.Add("F_GBN", "0");

            // 검사규격을 구한다
            using (ANLSBiz biz = new ANLSBiz())
            {
                ds = biz.ANLS0101_1(oParamDic, out errMsg);
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
                // 검사분석자료를 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0101_4(oParamDic, out errMsg);
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
                        devChart1.JSProperties["cpResultMsg"] = "조회 조건에 맞는 분석데이타가 없습니다";
                    }
                    else
                    {
                        bExecute2 = true;

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

            if (true == bExecute1)
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());

                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_SERIALNO", txtSERIALNO.Text);
                oParamDic.Add("F_SIRYO", txtSIRYO.Text);
                oParamDic.Add("F_GBN", "0");

                // 히스토그램을 구한다
                using (ANLSBiz biz = new ANLSBiz())
                {
                    ds = biz.ANLS0101_3(oParamDic, out errMsg);
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
                    }
                }
            }
            
            // 모든 데이타가 구성되면 그리드를 그린다
            if (bExecute1 && bExecute2)
            {
                if (dt1.Rows.Count > 0)
                {
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