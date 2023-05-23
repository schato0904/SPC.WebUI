using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ADTR.Biz;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0103_NSK : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        int[] gradeCnt = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

        DataSet dsGrid1
        {
            get
            {
                return (DataSet)Session["ADTR0103_NSK"];
            }
            set
            {
                Session["ADTR0103_NSK"] = value;
            }
        }

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
                if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
                {
                    //QWK04A_ADTR0103_NSK_LST();
                }

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
            //AspxCombox_DataBind(this.ddlRANK, "ddlGRADE", "AAD2");
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

        #region 라인별 측정 데이터 구한다
        void QWK04A_ADTR0103_NSK_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", Convert.ToDateTime(GetFromDt()).AddDays(1).ToString("yyyy-MM-dd"));
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                oParamDic.Add("F_WORKCD", "");
                oParamDic.Add("F_CPCHK", "1");
                oParamDic.Add("F_GRADE", "");
                oParamDic.Add("F_RANK", "");
                ds = biz.QWK04A_ADTR0103_NSK_LST(oParamDic, out errMsg);
                dsGrid1 = ds;
            }

            devGrid.DataSource = ds;
            
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                {
                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #region 일일점검저장
        void QWK04A_ADTR0103_NSK_INS()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", Convert.ToDateTime(GetFromDt()).AddDays(1).ToString("yyyy-MM-dd"));
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                oParamDic.Add("F_WORKCD", "");
                oParamDic.Add("F_CPCHK", "1");
                oParamDic.Add("F_GRADE", "");
                oParamDic.Add("F_RANK", "");
                oParamDic.Add("F_USERID", gsUSERID);

                ds = biz.QWK04A_ADTR0103_NSK_INS(oParamDic, out errMsg);
                dsGrid1 = ds;
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                {
                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox, string ComboBoxID, string CommonCode)
        {
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
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
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMCD", "F_ITEMNM", "F_WORKNM" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }

            if (e.DataColumn.FieldName.Equals("F_STATUS"))
            {
                if (e.CellValue != null)
                {
                    switch (e.CellValue.ToString())
                    {
                        case "AAE101": e.Cell.BackColor = System.Drawing.Color.FromArgb(26, 174, 136); break;
                        case "AAE102": e.Cell.BackColor = System.Drawing.Color.FromArgb(28, 202, 204); break;
                        case "AAE103": e.Cell.BackColor = System.Drawing.Color.FromArgb(252, 198, 51); break;
                        case "AAE104": e.Cell.BackColor = System.Drawing.Color.FromArgb(227, 50, 68); break;
                    }
                }
            }
        }
        #endregion

        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_STATUS")) return;

            if (e.Column.FieldName.Equals("F_STATUS"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                AspxCombox_DataBind(comboBox, "", "AAE1");
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
            if (e.VisibleRowIndex < 0) return;

            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);

            if (e.Column.FieldName.Equals("F_STATUS"))
            {
                e.DisplayText = GetCommonCodeText(CachecommonCode["AA"]["AAE1"][e.Value.ToString()]);   
            }
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
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                QWK04A_ADTR0103_NSK_INS();
            }
            else
            {
                QWK04A_ADTR0103_NSK_LST();
            }
            
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                //e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
                e.Text = GetCommonCodeText(CachecommonCode["AA"]["AAE1"][e.Value.ToString()]);
                e.TextValue = e.Text;
            }
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
            //QWK04A_ADTR0103_NSK_LST();
            devGrid.DataSource = dsGrid1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 공정능력모니터링정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}