using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.SYST.Biz;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.SYST
{
    public partial class SYST0103 : WebUIBasePage
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

            if (!IsCallback)
            {
                rdoSTATUS.SelectedItem = rdoSTATUS.Items.FindByValue("1");
            }

            // 사용자목록을 구한다
            SYUSR01_LST();

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
        }
        #endregion

        #region 유저아이디 중복체크
        void CHK_SYUSR01_DUPLICATE(Dictionary<string, string> oDic, out bool bExists)
        {
            using (CommonBiz biz = new CommonBiz())
            {
                bExists = biz.PROC_SYUSR01_DUPLICATE(oParamDic);
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

        #region 사용자목록을 구한다
        void SYUSR01_LST()
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_USERID", txtUSERID.Text);
                oParamDic.Add("F_USERNM", txtUSERNM.Text);
                if (!String.IsNullOrEmpty(rdoSTATUS.SelectedItem.Value.ToString()))
                    oParamDic.Add("F_STATUS", rdoSTATUS.SelectedItem.Value.ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYUSR01_LST(oParamDic, out errMsg);
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

        #region 코드목록을 구한다
        void SYCOD01_LST(DevExpress.Web.ASPxComboBox comboBox, string F_CODEGROUP)
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CODEGROUP", F_CODEGROUP);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_CODENM";
            comboBox.ValueField = "F_CODE";
            comboBox.DataSource = ds;
        }
        #endregion

        #region 공장목록을 구한다
        void QCM02_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);

                ds = biz.QCM02_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_FACTNM";
            comboBox.ValueField = "F_FACTCD";
            comboBox.DataSource = ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = true;
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
            if (!e.Column.FieldName.Equals("F_GRADECD") && !e.Column.FieldName.Equals("F_DEPARTCD") && !e.Column.FieldName.Equals("F_GROUPCD") && !e.Column.FieldName.Equals("F_FACTCD")) return;

            if (e.Column.FieldName.Equals("F_GRADECD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                SYCOD01_LST(comboBox, "21");
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }

            if (e.Column.FieldName.Equals("F_DEPARTCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                SYCOD01_LST(comboBox, "20");
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }

            if (e.Column.FieldName.Equals("F_GROUPCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                comboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
                comboBox.ValueField = "COMMCD";
                comboBox.DataSource = CachecommonCode["AA"]["AAC6"].codeGroup.Values;
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }

            if (e.Column.FieldName.Equals("F_FACTCD"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                QCM02_LST(comboBox);
                comboBox.DataBind();

                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
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
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);

            if (e.VisibleRowIndex < 0) return;

            DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);

            if (e.Column.FieldName.Equals("F_GRADECD"))
            {
                e.DisplayText = dtRow["F_GRADENM"].ToString();
            }

            if (e.Column.FieldName.Equals("F_DEPARTCD"))
            {
                e.DisplayText = dtRow["F_DEPARTNM"].ToString();
            }

            if (e.Column.FieldName.Equals("F_GROUPCD"))
            {
                e.DisplayText = dtRow["F_GROUPNM"].ToString();
            }

            if (e.Column.FieldName.Equals("F_FACTCD"))
            {
                e.DisplayText = dtRow["F_FACTNM"].ToString();
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
            // 사용자목록을 구한다
            SYUSR01_LST();
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
            int erroridx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            bool bExists = false;
            string errorID = null;

            string reInsert = null;
            

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_USERID", (Value.NewValues["F_USERID"] ?? "").ToString());
                    CHK_SYUSR01_DUPLICATE(oParamDic, out bExists);

                    if (!bExists)
                    {
                        #region 비밀번호 암호화(SHA256)
                        string ChanPW = !Convert.ToBoolean(Convert.ToInt32(gsENCRTPTPW)) ? (Value.NewValues["F_USERPW"] ?? "").ToString() : UF.Encrypts.ComputeHash((Value.NewValues["F_USERPW"] ?? "").ToString(), new System.Security.Cryptography.SHA256CryptoServiceProvider());
                        #endregion

                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                        oParamDic.Add("F_USERID", (Value.NewValues["F_USERID"] ?? "").ToString());
                        oParamDic.Add("F_USERNM", (Value.NewValues["F_USERNM"] ?? "").ToString());
                        oParamDic.Add("F_DEPARTCD", (Value.NewValues["F_DEPARTCD"] ?? "").ToString());
                        oParamDic.Add("F_GRADECD", (Value.NewValues["F_GRADECD"] ?? "").ToString());
                        oParamDic.Add("F_USERPW", ChanPW);
                        oParamDic.Add("F_GROUPCD", (Value.NewValues["F_GROUPCD"] ?? "").ToString());
                        oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                        oParamDic.Add("F_EMAIL", (Value.NewValues["F_EMAIL"] ?? "").ToString());
                        oParamDic.Add("F_MOBILENO", (Value.NewValues["F_MOBILENO"] ?? "").ToString());
                        oParamDic.Add("F_USER", gsUSERID);

                        oSPs[idx] = "USP_SYUSR01_INS";
                        oParameters[idx] = (object)oParamDic;

                        idx++;
                    }
                    else
                    {
                        string errorIDtemp = null;
                        string reInserttemp = null;

                        if (erroridx == 0)
                        {
                            errorID = (String)Value.NewValues["F_USERID"].ToString();
                            reInsert = (String)(Value.NewValues["F_USERNM"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_GRADECD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_DEPARTCD"] ?? "").ToString()
                                       + '|' + (String)(Value.NewValues["F_GROUPCD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_USERID"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_USERPW"] ?? "").ToString()
                                       + '|' + (String)(Value.NewValues["F_EMAIL"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_FACTCD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_STATUS"] ?? "").ToString() + '^';
                        } 
                        else
                        {
                            errorIDtemp = (String)Value.NewValues["F_USERID"].ToString();
                            errorID = errorID + "," + errorIDtemp;

                            reInserttemp = (String)(Value.NewValues["F_USERNM"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_GRADECD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_DEPARTCD"] ?? "").ToString()
                                       + '|' + (String)(Value.NewValues["F_GROUPCD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_USERID"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_USERPW"] ?? "").ToString()
                                       + '|' + (String)(Value.NewValues["F_EMAIL"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_FACTCD"] ?? "").ToString() + '|' + (String)(Value.NewValues["F_STATUS"] ?? "").ToString() + '^';
                            reInsert = reInsert + reInserttemp;
                        }
                        erroridx++;
                    }
                }
            }
            #endregion

            #region Batch Update

            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    #region 비밀번호 암호화(SHA256)
                    string ChanPW = !Convert.ToBoolean(Convert.ToInt32(gsENCRTPTPW)) ? (Value.NewValues["F_USERPW"] ?? "").ToString() : UF.Encrypts.ComputeHash((Value.NewValues["F_USERPW"] ?? "").ToString(), new System.Security.Cryptography.SHA256CryptoServiceProvider());
                    #endregion

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_USERID", (Value.NewValues["F_USERID"] ?? "").ToString());
                    oParamDic.Add("F_USERNM", (Value.NewValues["F_USERNM"] ?? "").ToString());
                    oParamDic.Add("F_DEPARTCD", (Value.NewValues["F_DEPARTCD"] ?? "").ToString());
                    oParamDic.Add("F_GRADECD", (Value.NewValues["F_GRADECD"] ?? "").ToString());
                    oParamDic.Add("F_USERPW", ChanPW);
                    oParamDic.Add("F_GROUPCD", (Value.NewValues["F_GROUPCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_EMAIL", (Value.NewValues["F_EMAIL"] ?? "").ToString());
                    oParamDic.Add("F_MOBILENO", (Value.NewValues["F_MOBILENO"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);

                    oSPs[idx] = "USP_SYUSR01_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            if (idx>0)
            {
                using (SYSTBiz biz = new SYSTBiz())
                {
                    bExecute = biz.PROC_SYUSR01_BATCH(oSPs, oParameters, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };

                    if (erroridx>0)
                    {
                        procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다. 나머지는 저장이 완료되었습니다." };
                    }

                    // 사용자목록을 구한다
                    SYUSR01_LST();
                }
            }
            else
            {
                procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다." };
                SYUSR01_LST();
            }


            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            devGrid.JSProperties["cpResultreInert"] = reInsert;
            devGrid.JSProperties["cpResultcount"] = erroridx;
            #endregion

            e.Handled = true;
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
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = e.Text.Replace(@"<br/>", "");
            }
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
            //QWK04A_ADTR0103_LST();
            //devGrid.DataSource = dsGrid;
            //devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 사용자정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}