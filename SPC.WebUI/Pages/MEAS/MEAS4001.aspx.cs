using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DevExpress.Web;
using SPC.Common.Biz;
using SPC.MEAS.Biz;
using SPC.WebUI.Common;
using System.Web.UI.HtmlControls;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS4001 : WebUIBasePage
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

                devGrid.DataBind();

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

        #region 검교정실적 목록을 구한다
        void MEAS4001_LST()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_REQNO", srcF_REQNO.Text);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS4001_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
        }

        void MEAS4001_LST_EXCEL()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_REQNO", srcF_REQNO.Text);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS4001_LST(oParamDic, out errMsg);
            }

            devGridExcel.DataSource = ds;
            //devGridExcel.DataBind();
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
            //if (e.InsertValues.Count > 0)
            //{
            //    foreach (var Value in e.InsertValues)
            //    {
            //        oParamDic = new Dictionary<string, string>();
            //        oParamDic.Add("F_FIXREQNO", Value.NewValues["F_FIXREQNO"].ToString());
            //        oParamDic.Add("F_MS01MID", Value.NewValues["F_MS01MID"].ToString());                    
            //        oParamDic.Add("F_FIXNO", (Value.NewValues["F_FIXNO"] ?? "").ToString());
            //        oParamDic.Add("F_ENDYN", (Value.NewValues["F_ENDYN"] ?? "").ToString());
            //        oParamDic.Add("F_JUDGECD", (Value.NewValues["F_JUDGECD"] ?? "").ToString());
            //        oParamDic.Add("F_TEMP", srcF_TEMP.Text.Trim());
            //        oParamDic.Add("F_HYGRO", srcF_HYGRO.Text.Trim());
            //        oParamDic.Add("F_REGUSER", srcF_REGUSER.Text.Trim());
            //        oParamDic.Add("F_CNFMUSER", srcF_CNFMUSER.Text.Trim());

            //        oSPs[idx] = "USP_MEAS4001_INS";
            //        oParameters[idx] = (object)oParamDic;

            //        idx++;
            //    }
            //}
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_MS01D5ID", (Value.OldValues["F_MS01D5ID"] ?? "").ToString());
                    oParamDic.Add("F_FIXREQNO", Value.OldValues["F_FIXREQNO"].ToString());
                    oParamDic.Add("F_MS01MID", Value.OldValues["F_MS01MID"].ToString());                    
                    oParamDic.Add("F_FIXNO", (Value.NewValues["F_FIXNO"] ?? "").ToString());
                    oParamDic.Add("F_FIXDT", (Value.NewValues["F_FIXDT"] ?? "").ToString());
                    oParamDic.Add("F_ENDYN", (Value.NewValues["F_ENDYN"] ?? "").ToString());
                    oParamDic.Add("F_JUDGECD", (Value.NewValues["F_JUDGECD"] ?? "").ToString());
                    oParamDic.Add("F_ATTFILENO", (Value.NewValues["F_ATTFILENO"] ?? "").ToString());
                    oParamDic.Add("F_TEMP", srcF_TEMP.Text.Trim());
                    oParamDic.Add("F_HYGRO", srcF_HYGRO.Text.Trim());
                    oParamDic.Add("F_REGUSER", srcF_REGUSER.Text.Trim());
                    oParamDic.Add("F_CNFMUSER", srcF_CNFMUSER.Text.Trim());

                    if (string.IsNullOrEmpty(oParamDic["F_MS01D5ID"]))
                    {
                        oParamDic.Remove("F_MS01D5ID");
                        oSPs[idx] = "USP_MEAS4001_INS"; 
                    }
                    else {
                        oSPs[idx] = "USP_MEAS4001_UPD";
                    }

                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            //if (e.DeleteValues.Count > 0)
            //{
            //    foreach (var Value in e.DeleteValues)
            //    {
            //        oParamDic = new Dictionary<string, string>();
            //        oParamDic.Add("F_MS01D5ID", (Value.Values["F_MS01D5ID"] ?? "").ToString());
            //        oParamDic.Add("F_MS01MID", (Value.Values["F_MS01MID"] ?? "").ToString());

            //        oSPs[idx] = "USP_MEAS4001_DEL";
            //        oParameters[idx] = (object)oParamDic;

            //        idx++;
            //    }
            //}
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.MEAS4001_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 메뉴목록을 구한다
                MEAS4001_LST();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
        }

        void MEAS4001_DEL(out string[] procResult) {

            bool bExecute = false;
            string errMsg = string.Empty;
            var arrF_MS01D5ID = devGrid.GetSelectedFieldValues("F_MS01D5ID");
            var arrF_MS01MID = devGrid.GetSelectedFieldValues("F_MS01MID");

            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

            if (arrF_MS01D5ID.Count > 0) {
                for(int i = 0; i < arrF_MS01D5ID.Count; i++) {
                    if (string.IsNullOrEmpty(arrF_MS01D5ID[i].ToString())) continue;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MS01D5ID", arrF_MS01D5ID[i].ToString());
                    oParamDic.Add("F_MS01MID", arrF_MS01MID[i].ToString());

                    oSPs.Add("USP_MEAS4001_DEL");
                    oParameters.Add(oParamDic);
                }
            }

            if (oSPs.Count > 0)
            {
                using (MEASBiz biz = new MEASBiz())
                {
                    bExecute = biz.MEAS4001_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "1", "삭제가 완료되었습니다." };
                }
            }
            else {
                procResult = new string[] { "0", "등록되지 않은 실적정보는 삭제 대상이 아닙니다." }; 
            }
        }

        void MEAS4001_UPD(out string[] procResult)
        {

            bool bExecute = false;
            string errMsg = string.Empty;
            var arrF_MS01D5ID = devGrid.GetSelectedFieldValues("F_MS01D5ID");
            var arrF_MS01MID = devGrid.GetSelectedFieldValues("F_MS01MID");
            var arrF_FIXREQNO = devGrid.GetSelectedFieldValues("F_FIXREQNO");
            var arrF_FIXNO = devGrid.GetSelectedFieldValues("F_FIXNO");
            var arrF_FIXDT = devGrid.GetSelectedFieldValues("F_FIXDT");
            var arrF_ENDYN = devGrid.GetSelectedFieldValues("F_ENDYN");
            var arrF_JUDGECD = devGrid.GetSelectedFieldValues("F_JUDGECD");
            var arrF_ATTFILENO = devGrid.GetSelectedFieldValues("F_ATTFILENO");

            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

            if (arrF_MS01D5ID.Count > 0)
            {
                for (int i = 0; i < arrF_MS01D5ID.Count; i++)
                {
                    if (string.IsNullOrEmpty(arrF_MS01D5ID[i].ToString())) continue;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MS01D5ID", arrF_MS01D5ID[i].ToString());
                    oParamDic.Add("F_MS01MID", arrF_MS01MID[i].ToString());

                    oParamDic.Add("F_FIXREQNO", arrF_FIXREQNO[i].ToString());
                    oParamDic.Add("F_FIXNO", arrF_FIXNO[i].ToString());
                    oParamDic.Add("F_FIXDT", arrF_FIXDT[i].ToString());
                    oParamDic.Add("F_ENDYN", arrF_ENDYN[i].ToString());
                    oParamDic.Add("F_JUDGECD", arrF_JUDGECD[i].ToString());
                    oParamDic.Add("F_ATTFILENO", arrF_ATTFILENO.ToString());
                    oParamDic.Add("F_TEMP", srcF_TEMP.Text.Trim());
                    oParamDic.Add("F_HYGRO", srcF_HYGRO.Text.Trim());
                    oParamDic.Add("F_REGUSER", srcF_REGUSER.Text.Trim());
                    oParamDic.Add("F_CNFMUSER", srcF_CNFMUSER.Text.Trim());

                    oSPs.Add("USP_MEAS4001_UPD");
                    oParameters.Add(oParamDic);
                }
            }

            if (oSPs.Count > 0)
            {
                using (MEASBiz biz = new MEASBiz())
                {
                    bExecute = biz.MEAS4001_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "1", "삭제가 완료되었습니다." };
                }
            }
            else
            {
                procResult = new string[] { "0", "등록되지 않은 실적정보는 삭제 대상이 아닙니다." };
            }
        }

        void MEAS4001_INS(out string[] procResult)
        {

            bool bExecute = false;
            string errMsg = string.Empty;
            var arrF_MS01D5ID = devGrid.GetSelectedFieldValues("F_MS01D5ID");
            var arrF_MS01MID = devGrid.GetSelectedFieldValues("F_MS01MID");
            var arrF_FIXREQNO = devGrid.GetSelectedFieldValues("F_FIXREQNO");
            var arrF_FIXNO = devGrid.GetSelectedFieldValues("F_FIXNO");
            var arrF_FIXDT = devGrid.GetSelectedFieldValues("F_FIXDT");
            var arrF_ENDYN = devGrid.GetSelectedFieldValues("F_ENDYN");
            var arrF_JUDGECD = devGrid.GetSelectedFieldValues("F_JUDGECD");
            var arrF_ATTFILENO = devGrid.GetSelectedFieldValues("F_ATTFILENO");

            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

            if (arrF_MS01D5ID.Count > 0)
            {
                for (int i = 0; i < arrF_MS01D5ID.Count; i++)
                {
                    if (string.IsNullOrEmpty(arrF_MS01D5ID[i].ToString())) continue;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_MS01D5ID", arrF_MS01D5ID[i].ToString());
                    oParamDic.Add("F_MS01MID", arrF_MS01MID[i].ToString());

                    oParamDic.Add("F_FIXREQNO", arrF_FIXREQNO[i].ToString());
                    oParamDic.Add("F_FIXNO", arrF_FIXNO[i].ToString());
                    oParamDic.Add("F_FIXDT", arrF_FIXDT[i].ToString());
                    oParamDic.Add("F_ENDYN", arrF_ENDYN[i].ToString());
                    oParamDic.Add("F_JUDGECD", arrF_JUDGECD[i].ToString());
                    oParamDic.Add("F_ATTFILENO", arrF_ATTFILENO.ToString());
                    oParamDic.Add("F_TEMP", srcF_TEMP.Text.Trim());
                    oParamDic.Add("F_HYGRO", srcF_HYGRO.Text.Trim());
                    oParamDic.Add("F_REGUSER", srcF_REGUSER.Text.Trim());
                    oParamDic.Add("F_CNFMUSER", srcF_CNFMUSER.Text.Trim());

                    oSPs.Add("USP_MEAS4001_INS");
                    oParameters.Add(oParamDic);
                }
            }

            if (oSPs.Count > 0)
            {
                using (MEASBiz biz = new MEASBiz())
                {
                    bExecute = biz.MEAS4001_BATCH(oSPs.ToArray(), oParameters.ToArray(), out errMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "1", "삭제가 완료되었습니다." };
                }
            }
            else
            {
                procResult = new string[] { "0", "등록되지 않은 실적정보는 삭제 대상이 아닙니다." };
            }
        }

        #endregion

        #region devGrid DataBinding
        /// <summary>
        /// devGrid_DataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            // 메뉴목록을 구한다
            MEAS4001_LST();

        }
        #endregion

        protected void devCallback_Callback(object source, CallbackEventArgs e)
        {
            var result = new Dictionary<string, string>();

            string pkey = string.Empty;
            string actType = e.Parameter.ToUpper();
            if (actType == "DEL")
            {
                // 삭제 액션
                this.MEAS4001_DEL(out procResult);
                result["MESSAGE"] = procResult[1];
                result["TYPE"] = "AfterDelete";
            }
            else if (actType == "UPD")
            {
                // 삭제 액션
                this.MEAS4001_UPD(out procResult);
                result["MESSAGE"] = procResult[1];
                result["TYPE"] = "AfterDelete";
            } 
            else if (actType == "INS")
            {
                // 삭제 액션
                this.MEAS4001_INS(out procResult);
                result["MESSAGE"] = procResult[1];
                result["TYPE"] = "AfterDelete";
            }

            result["CODE"] = procResult[0];
            e.Result = this.SerializeJSON(result);
        }

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //if (e.Parameters.Equals("clear"))
            //{
            //    devGrid.DataSourceID = null;
            //    devGrid.DataSource = null;
            //}
        }
        #endregion

        #region devGrid RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<BR/>", " ");
            }
        }
        #endregion

        #endregion

        #region btnExport Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            MEAS4001_LST_EXCEL();
            devGridExporter.GridViewID = "devGridExcel";
            devGridExcel.DataBind();
            
            devGridExporter.WriteXlsToResponse(String.Format("검교정실적등록_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            GridViewDataComboBoxColumn combo = devGrid.Columns["F_JUDGECD"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            combo.PropertiesComboBox.ValueField = "COMMCD";
            combo.PropertiesComboBox.DataSource = CachecommonCode["SS"]["SS02"].codeGroup.Values;
        }
    }
}