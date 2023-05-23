using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.FDCK
{
    public partial class FDCK0105 : WebUIBasePage
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
                // 하단 설비 목록 조회
                //RetrieveList();
            }

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
            AspxCombox_DataBind(ddlINSPCD, "AAC5"); //검사분류
            AspxCombox_DataBind(ddlCYCLE, "AAG4");  //검사주기
            AspxCombox_DataBind(ddlUNIT, "AAC1");   //단위코드
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

        #region 검사항목 목록을 구한다
        DataSet QCD_MACH10_LST(string INSPCD)
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_INSPCD", INSPCD);

                ds = biz.GetMACH11_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비 조회, 등록, 수정, 삭제

        // 설비 목록 조회
        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", GetFactCD());
                oParamDic.Add("F_MACHCD", this.hidF_MACHCD.Text);
                oParamDic.Add("F_INSPTYPECD", this.hidF_INSPTYPECD.Text);

                ds = biz.GetMACH11_LST(oParamDic, out errMsg);
            }
            ds = this.AutoNumber(ds);
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

        // 선택한 설비 상세정보 조회
        DataSet RetrieveDetail(string f_compcd, string f_factcd, string f_machcd, string f_insptypecd, string f_inspserial, out string errMsg)
        {
            errMsg = String.Empty;
            DataSet tds = new DataSet();
            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", f_compcd);
                oParamDic.Add("F_FACTCD", f_factcd);
                oParamDic.Add("F_MACHCD", f_machcd);
                oParamDic.Add("F_INSPTYPECD", f_insptypecd);
                oParamDic.Add("F_INSPSERIAL", f_inspserial);
                tds = biz.GetMACH11_LST(oParamDic, out errMsg);
            }
            return tds;
        }

        // 점검기준 저장
        bool QCD_MACH01_INS(out string errMsg)
        {
            errMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                //oParamDic.Add("F_MACHCD", GetMachCD());
                //oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                oParamDic.Add("F_MACHCD", this.hidF_MACHCD.Text);
                oParamDic.Add("F_INSPTYPECD", this.hidF_INSPTYPECD.Text);
                oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                oParamDic.Add("F_INSPKINDCD", txtINSPGBN.Text);
                oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                oParamDic.Add("F_MAX", txtMAX.Text);
                oParamDic.Add("F_MIN", txtMIN.Text);
                oParamDic.Add("F_INSPREMARK", txtINSPREMARK.Text);
                oParamDic.Add("F_INSPSTAND", txtINSPSTAND.Text);
                oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                oParamDic.Add("F_INSPORDER", txtINSPORDER.Text);
                oParamDic.Add("F_CYCLECD", txtCYCLE.Text);
                oParamDic.Add("F_UNIT", txtUNIT.Text);
                oParamDic.Add("F_USEYN", rdoUSEYN.Value == null ? "" : rdoUSEYN.Value.ToString());
                oParamDic.Add("F_INSUSER", gsUSERID);
                oParamDic.Add("F_WORKERS", srcWORKERS_USER.Text);

                bExecute = biz.QCD_MACH11_INS(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                bExecute = false;
            }
            return bExecute;
        }

        // 점검기준 수정
        bool QCD_MACH01_UPD(out string errMsg)
        {
            errMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                //oParamDic.Add("F_MACHCD", GetMachCD());
                //oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                oParamDic.Add("F_MACHCD", this.hidF_MACHCD.Text);
                oParamDic.Add("F_INSPTYPECD", this.hidF_INSPTYPECD.Text);
                oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                oParamDic.Add("F_INSPSERIAL", hidSerialno.Text);
                oParamDic.Add("F_INSPKINDCD", txtINSPGBN.Text);
                oParamDic.Add("F_STANDARD", txtSTANDARD.Text);
                oParamDic.Add("F_MAX", txtMAX.Text);
                oParamDic.Add("F_MIN", txtMIN.Text);
                oParamDic.Add("F_INSPREMARK", txtINSPREMARK.Text);
                oParamDic.Add("F_INSPSTAND", txtINSPSTAND.Text);
                oParamDic.Add("F_IMAGESEQ", txtIMAGESEQ.Text);
                oParamDic.Add("F_INSPORDER", txtINSPORDER.Text);
                oParamDic.Add("F_CYCLECD", txtCYCLE.Text);
                oParamDic.Add("F_UNIT", txtUNIT.Text);
                oParamDic.Add("F_USEYN", rdoUSEYN.Value == null ? "" : rdoUSEYN.Value.ToString());
                oParamDic.Add("F_INDUSER", gsUSERID);
                oParamDic.Add("F_WORKERS", srcWORKERS_USER.Text);
                bExecute = biz.QCD_MACH11_UPD(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }

        // 점검기준 삭제
        bool QCD_MACH01_DEL(out string errMsg)
        {
            errMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
                //oParamDic.Add("F_MACHCD", GetMachCD());
                //oParamDic.Add("F_INSPTYPECD", GetMachTypeCD());
                oParamDic.Add("F_MACHCD", this.hidF_MACHCD.Text);
                oParamDic.Add("F_INSPTYPECD", this.hidF_INSPTYPECD.Text);
                oParamDic.Add("F_INSPCD", txtINSPCD.Text);
                oParamDic.Add("F_INSPSERIAL", hidSerialno.Text);
                oParamDic.Add("F_INSPKINDCD", txtINSPGBN.Text);
                oParamDic.Add("F_INDUSER", gsUSERID);
                bExecute = biz.QCD_MACH11_DEL(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                bExecute = false;
            }

            return bExecute;
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

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 하단 그리드 목록 조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USEYN" }, "사용", "사용안함", e);
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
            if ((e.Column as DevExpress.Web.GridViewDataColumn).FieldName == "F_USEYN" ) //|| e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비점검항목", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = string.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값
            
            switch (paramDic["ACTION"])
            {
                case "INSPCD":

                    DataSet inspds = QCD_MACH10_LST(paramDic["INSPCD"].ToString());
                    if (inspds.Tables[0].Rows.Count.Equals(1))
                    {
                        result["INSPCD"] = inspds.Tables[0].Rows[0]["F_INSPCD"].ToString();
                        result["INSPNM"] = inspds.Tables[0].Rows[0]["F_INSPNM"].ToString();
                    }
                    ISOK = true;
                        result["ISOK"] = ISOK;
                    e.Result = jss.Serialize(result);
                    break;

                case "SAVE":
                    if (paramDic["PAGEMODE"] == "NEW")
                    {
                        QCD_MACH01_INS(out errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["PKEY"] = PKEY;
                            result["MSG"] = "저장되었습니다.";
                        }
                    }
                    else if (paramDic["PAGEMODE"] == "EDIT")
                    {
                        QCD_MACH01_UPD(out errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["PKEY"] = PKEY;
                            result["MSG"] = "수정되었습니다.";
                        }
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "DELETE":
                    QCD_MACH01_DEL(out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "삭제되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    //ds.Clear();
                    DataSet ndds = RetrieveDetail(paramDic["F_COMPCD"], paramDic["F_FACTCD"], paramDic["F_MACHCD"], paramDic["F_INSPTYPECD"], paramDic["F_INSPSERIAL"], out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ISOK = false;
                        msg = errMsg;
                    }
                    else if (ndds == null || ndds.Tables.Count == 0 || ndds.Tables[0].Rows.Count == 0)
                    {
                        ISOK = false;
                        msg = "조회된 데이터가 없습니다.";
                    }
                    else
                    {
                        ISOK = true;
                        msg = string.Empty;
                        DataRow dr = ndds.Tables[0].Rows[0];
                        // 조회한 데이터를 Dictionary 형태로 변환
                        PAGEDATA = ndds.Tables[0].Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    }
                    result["ISOK"] = ISOK;
                    result["MSG"] = msg;
                    result["PAGEDATA"] = PAGEDATA;
                    e.Result = jss.Serialize(result);
                    break;
            }
        } 
        #endregion

        #region txtINSPCD_Init
        /// <summary>
        /// txtMEAINSPCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtINSPCD_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupMachInspSearch()");
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
            string param = Request.Params.Get("__CALLBACKPARAM");
            if (!String.IsNullOrEmpty(param))
            {
                if (!param.Contains("CANCELEDIT"))
                {
                    DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
                    devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = devComboBox.ID.Equals("ddlINSPCD") ? "선택하세요" : "선택안함", Value = "", Selected = true });
                }
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
        void AspxCombox_DataBind(DevExpress.Web.ASPxComboBox ddlComboBox, string CommonCode)
        {
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();
        }
        #endregion

        #endregion
    }
}