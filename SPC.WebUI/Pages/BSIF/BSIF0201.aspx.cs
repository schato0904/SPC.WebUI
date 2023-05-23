using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using System.Text;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0201 : WebUIBasePage
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
                // 반목록을 구한다
                QCD72_LST();
            }

            // Grid Columns Sum Width
            hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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

        #region 마스터 협력업체를 구한다
        DataSet QCM01_LST2()
        {
            DataSet dsMaster = null;

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MASTERCHK", "1");
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                dsMaster = biz.QCM01_LST2(oParamDic, out errMsg);
            }

            return dsMaster;

        }
        #endregion

        #region 반별 납품업체 목록
        DataSet QCD00_BAN_LST()
        {
            DataSet dsBan = null;

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD_V", gsCOMPCD);
                oParamDic.Add("F_FACTCD_V", gsFACTCD);

                dsBan = biz.QCD00_BAN_LST(oParamDic, out errMsg);
            }

            return dsBan;

        }
        #endregion

        #region 반목록을 구한다
        void QCD72_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.QCD72_LST(oParamDic, out errMsg);
            }

            DataTable dtList = ds.Tables[0].Copy();

            // 현재 협력업체이며 마스터업체가 존재하는 경우
            // 마스트업체 밴드를 추가한다
            DataSet dsMaster = QCM01_LST2();
            if (bExistsDataSet(dsMaster))
            {
                DevExpress.Web.GridViewBandColumn bandExists = devGrid.Columns["F_COMPLIST"] as DevExpress.Web.GridViewBandColumn;
                DevExpress.Web.GridViewBandColumn band = null;
                if (bandExists == null)
                {
                    band = new DevExpress.Web.GridViewBandColumn() { Name = "F_COMPLIST", Caption = "납품업체" };
                    devGrid.Columns.Add(band);
                }
                string[] columns = new string[dsMaster.Tables[0].Rows.Count];
                int idx = 0;
                foreach (DataRow dtRow in dsMaster.Tables[0].Rows)
                {
                    if (bandExists == null)
                    {
                        DevExpress.Web.GridViewDataCheckColumn column = new DevExpress.Web.GridViewDataCheckColumn() { FieldName = String.Format("F_COMP_{0}", dtRow["F_COMPCD"]), Caption = dtRow["F_COMPNM"].ToString(), Width = Unit.Parse("120") };
                        band.Columns.Add(column);
                    }
                    columns[idx] = String.Format("F_COMP_{0}", dtRow["F_COMPCD"]);
                    dtList.Columns.Add(columns[idx], typeof(Boolean));
                    idx++;
                }

                // 반별 납품업체목록
                DataTable dtBan = QCD00_BAN_LST().Tables[0];

                // 반목록 별로 납품업체를 바인딩한다
                foreach (DataRow dtRow in dtList.Rows)
                {
                    // 일단 초기화
                    foreach (string column in columns)
                    {
                        dtRow[column] = false;
                    }

                    foreach (DataRow dr in dtBan.Select(String.Format("F_BANCD_V='{0}'", dtRow["F_BANCD"])))
                    {
                        dtRow[String.Format("F_COMP_{0}", dr["F_COMPCD_P"])] = true;
                    }
                }
            }

            devGrid.DataSource = dtList;

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

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = true;
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.ClientSideEvents.Init = "fn_OnControlDisableBox";
            //((DevExpress.Web.GridViewDataSpinEditColumn)devGrid.DataColumns["F_SEQNO"]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
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
            //DataRow dtRow = devGrid.GetDataRow(e.VisibleRowIndex);
            //if (e.Column.FieldName.Equals("F_BANNM"))
            //{
            //    e.DisplayText = dtRow["F_BANNM"].ToString();
            //}
        }
        #endregion

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
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
            // 마스터 협력업체
            DataTable dtMaster = QCM01_LST2().Tables[0];
            Int32 nTimes = dtMaster.Rows.Count > 0 ? dtMaster.Rows.Count + 1 : 1;

            int idx = 0;
            int count = (e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count) * nTimes;

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
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_BANNM", (Value.NewValues["F_BANNM"] ?? "").ToString());
                    oParamDic.Add("F_SORTNO", (Value.NewValues["F_SORTNO"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);
                    //oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[idx] = "USP_QCD72_INS";
                    oParameters[idx] = (object)oParamDic;
                    idx++;

                    foreach (DataRow dtRow in dtMaster.Rows)
                    {
                        if (Convert.ToBoolean(Value.NewValues[String.Format("F_COMP_{0}", dtRow["F_COMPCD"])]))
                        {
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD_P", dtRow["F_COMPCD"].ToString());
                            oParamDic.Add("F_FACTCD_P", "01");
                            oParamDic.Add("F_BANCD_V", (Value.NewValues["F_BANCD"] ?? "").ToString());
                            oParamDic.Add("F_COMPCD_V", gsCOMPCD);
                            oParamDic.Add("F_FACTCD_V", gsFACTCD);
                            oParamDic.Add("F_MODE", "INS");
                            //oParamDic.Add("F_OUTMSG", "OUTPUT");

                            oSPs[idx] = "USP_BAN_TARGET_COMP_PROC";
                            oParameters[idx] = (object)oParamDic;
                        }
                        else
                        {
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD_P", dtRow["F_COMPCD"].ToString());
                            oParamDic.Add("F_FACTCD_P", "01");
                            oParamDic.Add("F_BANCD_V", (Value.NewValues["F_BANCD"] ?? "").ToString());
                            oParamDic.Add("F_COMPCD_V", gsCOMPCD);
                            oParamDic.Add("F_FACTCD_V", gsFACTCD);
                            oParamDic.Add("F_MODE", "DEL");
                            //oParamDic.Add("F_OUTMSG", "OUTPUT");

                            oSPs[idx] = "USP_BAN_TARGET_COMP_PROC";
                            oParameters[idx] = (object)oParamDic;
                        }

                        idx++;
                    }
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
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_BANNM", (Value.NewValues["F_BANNM"] ?? "").ToString());
                    oParamDic.Add("F_SORTNO", (Value.NewValues["F_SORTNO"] ?? "").ToString());
                    oParamDic.Add("F_USER", gsUSERID);
                    //oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[idx] = "USP_QCD72_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;

                    foreach (DataRow dtRow in dtMaster.Rows)
                    {
                        if (Convert.ToBoolean(Value.NewValues[String.Format("F_COMP_{0}", dtRow["F_COMPCD"])]))
                        {
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD_P", dtRow["F_COMPCD"].ToString());
                            oParamDic.Add("F_FACTCD_P", "01");
                            oParamDic.Add("F_BANCD_V", (Value.NewValues["F_BANCD"] ?? "").ToString());
                            oParamDic.Add("F_COMPCD_V", gsCOMPCD);
                            oParamDic.Add("F_FACTCD_V", gsFACTCD);
                            oParamDic.Add("F_MODE", "INS");
                            //oParamDic.Add("F_OUTMSG", "OUTPUT");

                            oSPs[idx] = "USP_BAN_TARGET_COMP_PROC";
                            oParameters[idx] = (object)oParamDic;
                        }
                        else
                        {
                            oParamDic = new Dictionary<string, string>();
                            oParamDic.Add("F_COMPCD_P", dtRow["F_COMPCD"].ToString());
                            oParamDic.Add("F_FACTCD_P", "01");
                            oParamDic.Add("F_BANCD_V", (Value.NewValues["F_BANCD"] ?? "").ToString());
                            oParamDic.Add("F_COMPCD_V", gsCOMPCD);
                            oParamDic.Add("F_FACTCD_V", gsFACTCD);
                            oParamDic.Add("F_MODE", "DEL");
                            //oParamDic.Add("F_OUTMSG", "OUTPUT");

                            oSPs[idx] = "USP_BAN_TARGET_COMP_PROC";
                            oParameters[idx] = (object)oParamDic;
                        }

                        idx++;
                    }
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
                    oParamDic.Add("F_BANCD", (Value.Values["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_OUTMSG", "OUTPUT");

                    oSPs[idx] = "USP_QCD72_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;
            string[] outMsg = new string[oSPs.Length];

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCD72_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                StringBuilder sb_OutMsg = new StringBuilder();
                foreach(Dictionary<string,object> _oOutParamDic in oOutParamList)
                {
                    foreach (KeyValuePair<string, object> _oOutPair in _oOutParamDic)
                    {
                        if (!String.IsNullOrEmpty(_oOutPair.Value.ToString()))
                            sb_OutMsg.AppendFormat("반코드 {0}는 검사기준에서 사용중이므로 삭제할 수 없습니다.\r", _oOutPair.Value);
                    }
                }
                //for (int i = 0; i < outMsg.Length; i++)
                //{
                //    msg += outMsg[i];
                //    if (msg != "") break;
                //}

                if (!String.IsNullOrEmpty(sb_OutMsg.ToString()))
                {
                    procResult = new string[] { "2", sb_OutMsg.ToString() };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                }

                // 반목록을 구한다
                QCD72_LST();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
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
            // 반목록을 구한다
            QCD72_LST();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 반정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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

        #endregion
    }
}