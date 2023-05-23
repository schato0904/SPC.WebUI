using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;
using SPC.SYST.Biz;

namespace SPC.WebUI.Pages.LTRK
{
    public partial class LTRK0402 : WebUIBasePage
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

            // 거래처 조회
            QCM99_LST();

            // 품목분류를 구한다
            SYCOD01_LST();

            // 조회
            QPM12_LST();

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

        #region 거래처 조회
        void QCM99_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_VENDORTP", "AAE401");

                ds = biz.QCM99_LST(oParamDic, out errMsg);
            }

            ddlF_GUBN.Items.Clear();

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_VENDORCD", typeof(string));
            dtTable.Columns.Add("F_VENDORNM", typeof(string));

            dtTable.Rows.Add("", "선택");

            if (String.IsNullOrEmpty(errMsg) && bExistsDataSet(ds))
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    dtTable.Rows.Add(dtRow["F_VENDORCD"].ToString(), dtRow["F_VENDORNM"].ToString());
                }
            }

            ddlF_VENDOR.ValueField = "F_VENDORCD";
            ddlF_VENDOR.TextField = "F_VENDORNM";
            ddlF_VENDOR.DataSource = dtTable;
            ddlF_VENDOR.DataBind();
            ddlF_VENDOR.SelectedIndex = 0;
        }
        #endregion

        #region 품목분류 조회
        void SYCOD01_LST()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);
                oParamDic.Add("F_CODEGROUP", "24");
                oParamDic.Add("F_IPADDRESS", "AAE503");
                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            ddlF_GUBN.Items.Clear();

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("F_CODE", typeof(string));
            dtTable.Columns.Add("F_CODENM", typeof(string));

            dtTable.Rows.Add("", "선택");

            if (String.IsNullOrEmpty(errMsg) && bExistsDataSet(ds))
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    dtTable.Rows.Add(dtRow["F_CODE"].ToString(), dtRow["F_CODENM"].ToString());
                }
            }

            ddlF_GUBN.ValueField = "F_CODE";
            ddlF_GUBN.TextField = "F_CODENM";
            ddlF_GUBN.DataSource = dtTable;
            ddlF_GUBN.DataBind();
            ddlF_GUBN.SelectedIndex = 0;
        }
        #endregion

        #region 조회
        void QPM12_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ISMATERIAL", "0");
                oParamDic.Add("F_GUBN", (ddlF_GUBN.SelectedItem.Value ?? "").ToString());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_VENDORCD", (ddlF_VENDOR.SelectedItem.Value ?? "").ToString());
                oParamDic.Add("F_OUTLOTNO", srcF_OUTLOTNO.Text);
                oParamDic.Add("F_ISENABLED", chkISENABLED.Checked ? "1" : "0");
                ds = biz.QPM12_LST(oParamDic, out errMsg);
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
                    devGrid.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 조회
            QPM12_LST();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_STATUS" }, "사용", "삭제", e);
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
            if (devGridDataColumn != null
                && devGridDataColumn.FieldName.Equals("F_STATUS")
                && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0} ~ {1}]{2} 완제품기간별출고현황", GetFromDt(), GetToDt(), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}