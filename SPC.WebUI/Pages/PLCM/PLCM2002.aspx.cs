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
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts.Native;
using System.Text;


namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM2002 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

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
            GetMach();
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
            PLCM2002_LST();
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비별작업조회", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {

        }

        #region schF_RECIPEID_Callback
        protected void schF_RECIPEID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRECIPID();
        }
        #endregion

        #region schF_MASTERID_Callback
        protected void schF_MASTERID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetMaster();
        }
        #endregion

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            PLCM2002_LST();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region PLCM2002_LST
        void PLCM2002_LST()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_MACHCD", (this.schF_MACHCD.Value ?? "").ToString());
                oParamDic.Add("F_RECIPEID", (this.schF_RECIPEID.Value ?? "").ToString());
                oParamDic.Add("F_MASTERID", (this.schF_MASTERID.Value ?? "").ToString());

                ds = biz.USP_PLCM2002_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                devGrid.DataSource = AutoNumberTable(ds.Tables[0]);
            }

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

        #region GetDataCombo
        void GetMach()
        {
            string errMsg = String.Empty;

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

            schF_MACHCD.DataSource = ds;
            schF_MACHCD.TextField = "F_MACHNM";
            schF_MACHCD.ValueField = "F_MACHCD";
            schF_MACHCD.DataBind();
            schF_MACHCD.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });
        }
        #endregion

        #region GetRECIPID
        void GetRECIPID()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                oParamDic["F_FROMDT"] = GetFromDt();
                oParamDic["F_TODT"] = GetToDt();
                ds = biz.PLC04_LST(oParamDic, out errMsg);
            }
            schF_RECIPEID.DataSource = ds;
            schF_RECIPEID.TextField = "tRecipeID";
            schF_RECIPEID.ValueField = "tRecipeID";
            schF_RECIPEID.DataBind();
            schF_RECIPEID.SelectedIndex = 0;
            schF_RECIPEID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #region 마스터ID구하기
        void GetMaster()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                oParamDic["F_FROMDT"] = GetFromDt();
                oParamDic["F_TODT"] = GetToDt();

                ds = biz.PLC02_LST_2(oParamDic, out errMsg);
            }
            schF_MASTERID.DataSource = ds;
            schF_MASTERID.TextField = "tMasterNM";
            schF_MASTERID.ValueField = "tMasterNM";
            schF_MASTERID.DataBind();
            schF_MASTERID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        

        #endregion
    }
}