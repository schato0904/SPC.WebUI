using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;

namespace SPC.WebUI.Pages.TISP.Popup
{
    public partial class TISPXRSPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string[] keyFields;
        #endregion

        #region 프로퍼티

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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
            }

            SetGrid((DataTable)Session[String.Format("{0}_1", keyFields[0])], (DataTable)Session[String.Format("{0}_3", keyFields[0])]);

            // Grid Columns Sum Width
            //hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
        {
            keyFields = Request.QueryString.Get("keyFields").Split('|');
        }
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

        #region 그리드
        void SetGrid(DataTable dt1, DataTable dt3)
        {
            Int32   idx = 0,
                    siryo = Convert.ToInt32(keyFields[1]),
                    digit = Convert.ToInt32(keyFields[2]);

            // 데이타 구성용 DataTable
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("검사일자", typeof(String));
            dtTemp.Columns.Add("검사시간", typeof(String));
            dtTemp.Columns.Add("Xbar", typeof(String));
            dtTemp.Columns.Add("R", typeof(String));
            for (idx = 0; idx < siryo; idx++)
            {
                dtTemp.Columns.Add(String.Format("X{0}", idx + 1), typeof(String));
            }

            foreach (DataRow dtRow1 in dt1.Rows)
            {
                // 데이타 구성용 DataRow
                idx = 0;
                DataRow dtNewRow = dtTemp.NewRow();
                dtNewRow["검사일자"] = String.Format("{0}({1})", DateTime.Parse(dtRow1["F_WORKDATE"].ToString()).ToString("MM/dd"), dtRow1["F_TSERIALNO"]);
                dtNewRow["검사시간"] = dtRow1["F_WORKTIME"].ToString();
                dtNewRow["Xbar"] = Math.Round(Convert.ToDecimal(dtRow1["F_XBAR"].ToString()), digit + 1).ToString();
                dtNewRow["R"] = Math.Round(Convert.ToDecimal(dtRow1["F_XRANGE"].ToString()), digit + 1).ToString();
                foreach (DataRow dtRow3 in dt3.Select(String.Format("F_WORKDATE='{0}' AND F_TSERIALNO='{1}'", dtRow1["F_WORKDATE"], dtRow1["F_TSERIALNO"])))
                {
                    dtNewRow[String.Format("X{0}", ++idx)] = Math.Round(Convert.ToDecimal(dtRow3["F_MEASURE"].ToString()), digit).ToString();
                }
                dtTemp.Rows.Add(dtNewRow);
            }

            devGrid.DataSource = dtTemp;
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} {2} {3} 전수 X-Rs DATA 목록", keyFields[3], keyFields[4], keyFields[5], keyFields[6]), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}