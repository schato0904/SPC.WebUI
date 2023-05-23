using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.SYST.Biz;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;


namespace SPC.WebUI.Pages.SYST
{
    public partial class SYST0106 : WebUIBasePage
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
                SYUSR01_QCD013_LST();
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
            if (!String.IsNullOrEmpty(hidGridAction2.Text) || !hidGridAction2.Text.Equals("false"))
            {
                QCD013_LST_EDIT();
            }

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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

        #region 반코드 정보를 불러온다
        void QCD72_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.GetQCD72_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_BANNM";
            comboBox.ValueField = "F_BANCD";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 라인코드 정보를 불러온다
        void QCD73_LST(string strBancd, DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", strBancd);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.QCD73_LST(oParamDic, out errMsg);
            }
            
            comboBox.TextField = "F_LINENM";
            comboBox.ValueField = "F_LINECD";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 사용자 정보를 구한다
        void SYUSR01_QCD013_LST()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                //oParamDic.Add("F_BANCD", GetBanCD());


                ds = biz.SYUSR01_QCD013_LST(oParamDic, out errMsg);
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

        #region 사용자별 라인 정보를 구한다
        void QCD013_LST_EDIT()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_USERID", hidUserID.Text);
                oParamDic.Add("F_USERNM", hidUserNm.Text);                                
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_GBN", hidGbn.Text);

                ds = biz.QCD013_LST_EDIT(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid2.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (!e.Column.FieldName.Equals("F_BANCD") && !e.Column.FieldName.Equals("F_LINECD")) return;

            if (e.Column.FieldName.Equals("F_BANCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                QCD72_LST(comboBox);
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_USERID", (Value.NewValues["F_USERID"] ?? "").ToString());
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.NewValues["F_LINECD"] ?? "").ToString());                    
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD013_INS";
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
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_USERID", (Value.NewValues["F_USERID"] ?? "").ToString());
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.NewValues["F_LINECD"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_QCD013_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_USERID", (Value.Values["F_USERID"] ?? "").ToString());
                    oParamDic.Add("F_BANCD", (Value.Values["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_LINECD", (Value.Values["F_LINECD"] ?? "").ToString());

                    oSPs[idx] = "USP_QCD013_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                bExecute = biz.PROC_QCD013_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 사용자 정보를 구한다
                SYUSR01_QCD013_LST();                
                QCD013_LST_EDIT();
            }

            devGrid2.JSProperties["cpResultCode"] = procResult[0];
            devGrid2.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
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
            // 사용자 정보를 구한다
            SYUSR01_QCD013_LST();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            
            var Param = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<System.Collections.Generic.Dictionary<string, string>>(Server.UrlDecode(e.Parameters));

            QCD013_LST_EDIT();
        }
        #endregion

        #region ddlLINEEdit_DataBound
        /// <summary>
        /// ddlLINEEdit_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlLINEEdit_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #region ddlLINEEdit_Callback
        /// <summary>
        /// ddlLINEEdit_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlLINEEdit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParam = e.Parameter.Split('|');
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Clear();
            QCD73_LST(oParam[0], comboBox);
            comboBox.DataBind();

            if (!String.IsNullOrEmpty(oParam[1]))
                comboBox.SelectedIndex = comboBox.Items.FindByValue(oParam[1]).Index;
            else
                comboBox.SelectedIndex = 0;
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
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
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
            //this.ExcelFileDownLoad();
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            link1.Component = devGridExporter;
            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            link2.Component = devGridExporter2;

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1, link2});

            compositeLink.CreatePageForEachLink();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = String.Format("[{0}]{1} 사용자별라인정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM);
                XlsxExportOptions options = new XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);

                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "application/xls");
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", HttpUtility.UrlEncode(file_name, new System.Text.UTF8Encoding()).Replace("+", "%20")));
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            ps.Dispose();

            //devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 사용자별라인정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            //devGridExporter2.WriteXlsToResponse(String.Format("[{0}]{1} 사용자별라인정보상세", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}