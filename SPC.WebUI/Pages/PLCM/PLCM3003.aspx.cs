using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.PLCM.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM3003 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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
            BindCombo(schF_MACHCD);
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
        void PLCM3003_LST()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", GetFromDt());
                oParamDic.Add("F_MACHCD", (this.schF_MACHCD.Value ?? "").ToString());
                oParamDic.Add("F_RECIPEID", (this.schF_RECIPID.Value ?? "").ToString());
                ds = biz.USP_PLCM3003_LST(oParamDic, out errMsg);
            }


            int datecnt = ds.Tables[0].Rows.Count;
            int rowcnt = ds.Tables[1].Rows.Count;

            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(ds.Tables[0], "F_DATE");

            for (int i = 0; i < rowcnt; i++)
            {
                dtPivotTable.Rows.Add();
            }

            for (int i = 0; i < datecnt; i++)
            {
                for (int j = 0; j < rowcnt; j++)
                {
                    dtPivotTable.Rows[j][i] = ds.Tables[1].Rows[j][i].ToString();
                }
            }

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

                devGrid.Columns["F_DATE"].Caption = "No";
                devGrid.Columns["F_DATE"].Width = 35;
                devGrid.Columns["F_DATE"].FixedStyle = GridViewColumnFixedStyle.Left;

                for (int i = 0; i < datecnt; i++)
                {
                    devGrid.Columns[i + 1].Width = 100;
                }

            }
        }
        #endregion

        #region Data조회(엑셀용)
        void PLCM3003_LST2()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", GetFromDt());
                oParamDic.Add("F_MACHCD", (this.schF_MACHCD.Value ?? "").ToString());
                oParamDic.Add("F_RECIPEID", (this.schF_RECIPID.Value ?? "").ToString());
                ds = biz.USP_PLCM3003_LST(oParamDic, out errMsg);
            }

            int datecnt = ds.Tables[0].Rows.Count;
            int rowcnt = ds.Tables[1].Rows.Count;

            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(ds.Tables[0], "F_DATE");

            for (int i = 0; i < rowcnt; i++)
            {
                dtPivotTable.Rows.Add();
            }

            for (int i = 0; i < datecnt; i++)
            {
                for (int j = 0; j < rowcnt; j++)
                {
                    dtPivotTable.Rows[j][i] = ds.Tables[1].Rows[j][i].ToString();
                }
            }

            devGrid.DataSource = dtPivotTable;
        }
        #endregion

        #region BindCombo
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>();
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_MACHNM";
            c.ValueField = "F_MACHCD";
            c.DataBind();
            c.Items.Insert(0, new ListEditItem("선택", ""));
            c.SelectedIndex = 0;
        }
        #endregion

        #region 설비목록
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                oParamDic["F_MACHYN"] = "1";
                ds = biz.PLC01_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 레시피ID구하기
        void GetRECIPID()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                oParamDic["F_FROMDT"] = GetFromDt() + "-01";
                oParamDic["F_TODT"] = GetFromDt() + "-31";
                ds = biz.PLC04_LST(oParamDic, out errMsg);
            }
            schF_RECIPID.DataSource = ds;
            schF_RECIPID.TextField = "tRecipeID";
            schF_RECIPID.ValueField = "tRecipeID";
            schF_RECIPID.DataBind();
            schF_RECIPID.SelectedIndex = 0;
            schF_RECIPID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            var grid = sender as ASPxGridView;
            string fieldname = e.DataColumn.FieldName;

            if (fieldname != "F_DATE")
            {
                string strJudge = e.CellValue.ToString();
                if (strJudge != "")
                {
                    string[] sp = strJudge.Split('|');
                    e.Cell.Text = sp[0] + "<br />" + sp[1] + "<br />" + sp[2] + "<br />" + sp[3];
                }
            }
            
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
            PLCM3003_LST();
        }
        #endregion

        #region schF_RECIPID_Callback
        protected void schF_RECIPID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRECIPID();
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            PLCM3003_LST2();
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비별 월 작업현황", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}