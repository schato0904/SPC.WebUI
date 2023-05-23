using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.FDCK.Biz;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;


namespace SPC.WebUI.Pages.FDCK
{
    public partial class FDCK0107 : WebUIBasePage
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 사용자 정보를 구한다
                //MACH13_LST();
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
        { }
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

        #region 사용자 정보를 구한다
        void MACH10_1_LST()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHCD", GetMachCD());
                oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                oParamDic.Add("F_MEASDATE", GetFromDt());
                oParamDic.Add("F_OKUSER", gsUSERID);
                
                ds = biz.QWK_MACH10_GD1_LST(oParamDic, out errMsg);
            }
            //ds = this.AutoNumber(ds);

            DataTable dt = new DataTable();
            dt.Columns.Add("admin");
            string dtr = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dtr = ds.Tables[0].Rows[i]["DATE"].ToString();
                dt.Columns.Add(dtr);
            }

            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows[0][0] = "관리자확인";
            dt.Rows[1][0] = "관리자";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dtr = ds.Tables[0].Rows[i]["DATE"].ToString();
                dt.Rows[0][i + 1] = dtr.Substring(6, 2);
                dt.Rows[1][i + 1] = ds.Tables[0].Rows[i]["ST"].ToString();
            }

            devGrid.DataSource = dt;
            
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
                devGrid.Columns[0].Width = 150;
            }
        }
        #endregion

        #region 사용자 정보를 구한다
        void MACH10_2_LST()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHCD", GetMachCD());
                oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                oParamDic.Add("F_MEASDATE", GetFromDt());
                oParamDic.Add("F_OKUSER", gsUSERID);

                ds = biz.QWK_MACH10_GD2_LST(oParamDic, out errMsg);
            }

            DataTable dtPivotTable = CTF.Web.Framework.Component.PivotDataTable.GetInversedDataTable(ds.Tables[0], "DATE");

            devGrid1.DataSource = dtPivotTable;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            MACH10_1_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비점검기준이력", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
            if ((e.Column as DevExpress.Web.GridViewDataColumn).FieldName == "F_USEYN") //|| e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;
            string[] oParams = e.Parameter.Split('|');
            bool bExecute = false;
            string medate = oParams[1];

            switch (oParams[0])
            {
                case "ins":  // 담당자확인

                    using (FDCKBiz biz = new FDCKBiz())
                    {
                        oParamDic.Clear();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_MACHCD", GetMachCD());
                        oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                        oParamDic.Add("F_MEASDATE", medate.Substring(0, 4) + "-" + medate.Substring(4, 2) + "-" + medate.Substring(6, 2));
                        oParamDic.Add("F_OKUSER", gsUSERID);


                        bExecute = biz.QWK_MACH10_INS(oParamDic, out errMsg);
                    }
                    
                    break;
            }
            if (!bExecute)
            {
            }
        }
        #endregion

        DataSet AutoNumber(DataSet ds, string NumberColumnName = "NO")
        {
            DataSet returnDs = new DataSet();
            DataTable dt = null;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //dt = ds.Tables[i];
                dt = new DataTable();
                dt.Columns.Add(NumberColumnName, typeof(long));
                dt.Columns[NumberColumnName].AutoIncrement = true;
                dt.Columns[NumberColumnName].AutoIncrementSeed = 1;
                dt.Columns[NumberColumnName].AutoIncrementStep = 1;
                dt.Merge(ds.Tables[i]);
                returnDs.Tables.Add(dt);
            }

            return returnDs;
        }

        #endregion

        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            //if (e.DataColumn.FieldName.Equals("F_FOURNM"))
            //{
                switch (e.CellValue.ToString())
                {
                    case "1" : e.Cell.BackColor = System.Drawing.Color.FromArgb(255, 0, 0); break;
                    case "2" : e.Cell.BackColor = System.Drawing.Color.FromArgb(255, 165, 0); break;
                    case "3": e.Cell.BackColor = System.Drawing.Color.FromArgb(100, 200, 0); break;
                    case "4": e.Cell.BackColor = System.Drawing.Color.FromArgb(55, 10, 100); break;
                }
            //}

                if (e.CellValue.ToString().Length > 0)
                {
                    e.Cell.Attributes.Add("ondblclick", String.Format("fn_OnAdminClick('{0}','{1}')", e.DataColumn.FieldName, e.CellValue));
                }
        }

        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //DevExpressLib.GetBoolString(new string []{devGrid.Columns[1].Name.ToString()}, "V", "", e);
            //if (e.Column.FieldName.Equals("F_UNIT"))
            //{
            if (e.Value.ToString() == "1" || e.Value.ToString() == "2")
                {
                    e.DisplayText = "";
                }
                else if (e.Value.ToString() == "3" || e.Value.ToString() == "4")
                {
                    e.DisplayText = "V";
                }
            //}
        }

        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            MACH10_2_LST();
        }
    }
}