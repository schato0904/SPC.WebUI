using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using DevExpress.Web;
using SPC.WebUI.Common;
using SPC.PLCM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM1003 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        // 좌측목록
        DataTable CachedData
        {
            get { return Session["PLCM1003_Grid"] as DataTable; }
            set { Session["PLCM1003_Grid"] = value; }
        }

        // 스텝
        DataTable CachedData1
        {
            get { return Session["PLCM1003_RE"] as DataTable; }
            set { Session["PLCM1003_RE"] = value; }
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
            // 파라미터 처리
            if (!IsPostBack && !string.IsNullOrEmpty(Request["oSetParam"]))
            {
                var pm = this.DeserializeJSON(Request["oSetParam"]);
                //srcF_PJ10MID.Text = pm["F_PJ10MID"];
                //schF_MNGUSER1.Text = pm["F_MNGUSER"];
            }
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

            if (!this.IsPostBack)
            {
                this.CachedData = null;
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
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            devGrid.DataSource = CachedData;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 검사기준조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        void Download_Excelfile()
        {
            DevExpress.XtraPrinting.PrintingSystemBase ps = new DevExpress.XtraPrinting.PrintingSystemBase();

            DevExpress.XtraPrintingLinks.PrintableComponentLinkBase link1 = new DevExpress.XtraPrintingLinks.PrintableComponentLinkBase(ps);
            link1.Component = devGridExporter;

            DevExpress.XtraPrintingLinks.CompositeLinkBase compositeLink = new DevExpress.XtraPrintingLinks.CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1 });
            compositeLink.CreateDocument();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = Uri.EscapeUriString(string.Format("{0}_{1}.xlsx", this.Request["pParam"].Split('|')[4], DateTime.Now.ToString("yyyyMMddHHmmss")));
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;

                compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "application/xlsx");
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", file_name));
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            ps.Dispose();
        }
        #endregion

        #region devGrid1Exporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 컨트롤 데이터 조회
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();

            return result;
        }
        // 콤보박스 목록 조회
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>();
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_MACHNM";
            c.ValueField = "F_MACHCD";
            c.DataBind();
            c.Items.Insert(0, new ListEditItem("--선택--", ""));
            c.SelectedIndex = 0;
        }
        #endregion

        #region 항목조회
        void GET_INSP()
        { 
            var p1 = this.GetRightParameter();
            string errMsg = string.Empty;
            this.CachedData = this.GetDataLeftBody(p1, out errMsg);
            devGrid.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
            devGrid.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            devGrid.DataBind();
        }

        #endregion

        #region devGrid BatchUpdate
        /// <summary>
        /// devGrid_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();

                    oParamDic.Add("F_MACHCD", (Value.NewValues["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_RECIPID", (Value.NewValues["F_RECIPID"] ?? "").ToString());
                    oParamDic.Add("F_STEP", (Value.NewValues["F_STEP"] ?? "").ToString());
                    oParamDic.Add("F_SERIALNO", (Value.NewValues["F_SERIALNO"] ?? "").ToString());
                    oParamDic.Add("F_STANDARD", (Value.NewValues["F_STANDARD"] ?? "").ToString());
                    oParamDic.Add("F_MIN", (Value.NewValues["F_MIN"] ?? "").ToString());
                    oParamDic.Add("F_MAX", (Value.NewValues["F_MAX"] ?? "").ToString());
                    oParamDic.Add("F_UCLX", (Value.NewValues["F_UCLX"] ?? "").ToString());
                    oParamDic.Add("F_LCLX", (Value.NewValues["F_LCLX"] ?? "").ToString());
                    oParamDic.Add("F_UCLR", (Value.NewValues["F_UCLR"] ?? "").ToString());
                    oParamDic.Add("F_UNIT", (Value.NewValues["F_UNIT"] ?? "").ToString());
                    oParamDic.Add("F_ZIG", (Value.NewValues["F_ZIG"] ?? "").ToString());
                    oParamDic.Add("F_ZERO", (Value.NewValues["F_ZERO"] ?? "").ToString());
                    oParamDic.Add("F_RANK", (Value.NewValues["F_RANK"] ?? "").ToString());
                    oParamDic.Add("F_MEASCD1", (Value.NewValues["F_MEASCD1"] ?? "").ToString());
                    oParamDic.Add("F_RESULTSTAND", (Value.NewValues["F_RESULTSTAND"] ?? "").ToString());
                    oParamDic.Add("F_DISPLAYNO", (Value.NewValues["F_DISPLAYNO"] ?? "").ToString());
                    oParamDic.Add("F_USERID", gsUSERID);

                    oSPs[idx] = "USP_PLCM1003_UPD1";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();

                    oParamDic.Add("F_MACHCD", (Value.Keys["F_MACHCD"] ?? "").ToString());
                    oParamDic.Add("F_RECIPID", (Value.Keys["F_RECIPID"] ?? "").ToString());
                    oParamDic.Add("F_STEP", (Value.Keys["F_STEP"] ?? "").ToString());
                    oParamDic.Add("F_SERIALNO", (Value.Keys["F_SERIALNO"] ?? "").ToString());
                    oParamDic.Add("F_MEAINSPCD", (Value.Keys["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_STANDARD", (Value.NewValues["F_STANDARD"] ?? "").ToString());
                    oParamDic.Add("F_MIN", (Value.NewValues["F_MIN"] ?? "").ToString());
                    oParamDic.Add("F_MIN_OLD", (Value.OldValues["F_MIN"] ?? "").ToString());
                    oParamDic.Add("F_MAX", (Value.NewValues["F_MAX"] ?? "").ToString());
                    oParamDic.Add("F_MAX_OLD", (Value.OldValues["F_MAX"] ?? "").ToString());
                    oParamDic.Add("F_UCLX", (Value.NewValues["F_UCLX"] ?? "").ToString());
                    oParamDic.Add("F_UCLX_OLD", (Value.OldValues["F_UCLX"] ?? "").ToString());
                    oParamDic.Add("F_LCLX", (Value.NewValues["F_LCLX"] ?? "").ToString());
                    oParamDic.Add("F_LCLX_OLD", (Value.OldValues["F_LCLX"] ?? "").ToString());
                    oParamDic.Add("F_UCLR", (Value.NewValues["F_UCLR"] ?? "").ToString());
                    oParamDic.Add("F_UNIT", (Value.NewValues["F_UNIT"] ?? "").ToString());
                    oParamDic.Add("F_ZIG", (Value.NewValues["F_ZIG"] ?? "").ToString());
                    oParamDic.Add("F_ZERO", (Value.NewValues["F_ZERO"] ?? "").ToString());
                    oParamDic.Add("F_RANK", (Value.NewValues["F_RANK"] ?? "").ToString());
                    oParamDic.Add("F_MEASCD1", (Value.NewValues["F_MEASCD1"] ?? "").ToString());
                    oParamDic.Add("F_RESULTSTAND", (Value.NewValues["F_RESULTSTAND"] ?? "").ToString());
                    oParamDic.Add("F_DISPLAYNO", (Value.NewValues["F_DISPLAYNO"] ?? "").ToString());
                    oParamDic.Add("F_USERID", gsUSERID);

                    oSPs[idx] = "USP_PLCM1003_UPD1";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion
         
            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                bExecute = biz.PROC_BATCH_UPDATE(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                // 공정목록을 구한다
                GET_INSP();
                procResult[0] = "1";
                procResult[1] = "저장이 완료되었습니다.";
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
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
            if (e.VisibleRowIndex < 0) return;

            // 검사분류, QC검사주기, 현장검사주기
            // 품질수준, 편측, 공차기호
            // 관리한계기준, 계측기, 측정포트
            // 측정방법, 설비구분
            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);

            if (e.Column.FieldName.Equals("F_RANK") || e.Column.FieldName.Equals("F_UNIT") && !String.IsNullOrEmpty(e.Value.ToString()))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
                return;
            }
            else if (e.Column.FieldName.Equals("F_STEP") && !String.IsNullOrEmpty(e.Value.ToString())) {

                e.DisplayText = dtRow["F_STEPNM"].ToString();
                return;
            }
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(object comboBox, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind2(object comboBox)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = "F_STEPNM";
            ddlComboBox.ValueField = "F_STEP";
            ddlComboBox.DataSource = CachedData1;
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "", Selected = true });
        }
        #endregion

        #endregion

        #region DB 처리 함수

        #region STEP구하기
        void GetSTEP()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                ds = biz.PLC06_LST(oParamDic, out errMsg);
            }
            CachedData1 = ds.Tables[0];
            schF_STEP.DataSource = CachedData1;
            schF_STEP.TextField = "F_STEPNM";
            schF_STEP.ValueField = "F_STEP";
            schF_STEP.DataBind();
            schF_STEP.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });

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
                ds = biz.PLC07_LST(oParamDic, out errMsg);
            }
            schF_RECIPID.DataSource = ds;
            schF_RECIPID.TextField = "tRecipeID";
            schF_RECIPID.ValueField = "tRecipeID";
            schF_RECIPID.DataBind();
            schF_RECIPID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
            schF_RECIPID.SelectedIndex = 0;
        }
        #endregion

        #region 좌측 내용 조회 검사항목리스트
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataLeftBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_RECIPID"] = (this.schF_RECIPID.Value ?? "").ToString();
                oParamDic["F_STEP"] = (this.schF_STEP.Value ?? "").ToString();
                ds = biz.USP_PLCM1003_LST1(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 설비 목록 조회
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHTYPECD"] = "";
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.PLC01_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #endregion

        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GET_INSP();
        }

        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        }

        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_RANK"] = "A";
        }

        protected void devGrid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_RANK") && !e.Column.FieldName.Equals("F_UNIT") && !e.Column.FieldName.Equals("F_MACHCD") && !e.Column.FieldName.Equals("F_STEP") 
                && !e.Column.FieldName.Equals("F_MACHNM") && !e.Column.FieldName.Equals("F_MEAINSPCD") && !e.Column.FieldName.Equals("F_INSPDETAIL")) return;
            string groupCode = String.Empty;

            switch (e.Column.FieldName)
            {
                default:
                    groupCode = String.Empty;
                    break;
                case "F_RANK":          // 품질수준
                    groupCode = "AAD2";
                    break;
                case "F_UNIT":          // 공차기호
                    groupCode = "AAC1";
                    break;
                case "F_STEP":          // 공차기호
                    groupCode = "STEP";
                    break;
            }

            if (!String.IsNullOrEmpty(groupCode))
            {
                if (groupCode == "STEP")
                {
                    AspxCombox_DataBind2(e.Editor);
                }
                else
                {
                    AspxCombox_DataBind(e.Editor, groupCode);
                }
            }
            else
            {
                DevExpress.Web.ASPxTextBox txtBox = e.Editor as DevExpress.Web.ASPxTextBox;
                if (e.Column.FieldName.Equals("F_MACHCD"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupMeainspSearchForm()");
                else if (e.Column.FieldName.Equals("F_MACHNM"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupMeainspSearchForm()");
                else if (e.Column.FieldName.Equals("F_MEAINSPCD"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupMeainspSearchForm()");
                else if (e.Column.FieldName.Equals("F_INSPDETAIL"))
                    txtBox.Attributes.Add("ondblclick", "fn_OnPopupMeainspSearchForm()");
            }
        }

        protected void schF_RECIPID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRECIPID();
        }

        protected void schF_STEP_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSTEP();
        }
    }
}