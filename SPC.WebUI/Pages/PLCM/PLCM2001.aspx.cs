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
    public partial class PLCM2001 : WebUIBasePage
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
            get { return Session["PLCM2001_Grid"] as DataTable; }
            set { Session["PLCM2001_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["PLCM2001_Grid1"] as DataTable; }
            set { Session["PLCM2001_Grid1"] = value; }
        }

        // 레시피
        DataTable CachedData2
        {
            get { return Session["PLCM2001_RE"] as DataTable; }
            set { Session["PLCM2001_RE"] = value; }
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
                this.CachedData1 = null;
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

        #region devGrid1 이벤트 처리
        #region devGrid1_DataBinding
        protected void devGrid1_DataBinding(object sender, EventArgs e)
        {
            this.devGrid1.DataSource = this.CachedData1;
        }
        #endregion

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p1 = this.GetRightParameter();
                string errMsg = string.Empty;
                this.CachedData1 = this.GetDataRightBody(p1, out errMsg);

                devGrid1.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid1.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            else if (e.Parameters.Equals("clear"))
            {
                CachedData1 = null;
                this.devGrid1.DataSourceID = null;
                this.devGrid1.DataSource = null;
                this.devGrid1.DataBind();
                return;
            }
            devGrid1.DataBind();
            devGrid1.Columns["TIME"].Width = 100;
            if (devGrid1.Columns.Count > 0)
            {
                for (int i = 1; i < devGrid1.Columns.Count; i++)
                {
                    devGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }
        #endregion
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //string errMsg = String.Empty;
            //this.Download_Excelfile();
            devGrid1.DataSource = CachedData1;
            devGrid1.DataBind();
            devGrid1Exporter.WriteXlsToResponse(String.Format("[{0}]{1}_{2}", schF_MACHCD.Text, DateTime.Today.ToString("yyyyMMdd"), schF_MASTERID.Text), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        void Download_Excelfile()
        {
            DevExpress.XtraPrinting.PrintingSystemBase ps = new DevExpress.XtraPrinting.PrintingSystemBase();

            DevExpress.XtraPrintingLinks.PrintableComponentLinkBase link1 = new DevExpress.XtraPrintingLinks.PrintableComponentLinkBase(ps);
            link1.Component = devGrid1Exporter;

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
        protected void devGrid1Exporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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

            result["F_FROMYMD"] = schF_FROMYMD.Text;
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
            
            DataBind();
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
                    oParamDic.Add("F_MEAINSPCD", (Value.Keys["F_MEAINSPCD"] ?? "").ToString());
                    oParamDic.Add("F_USEYN", (Value.NewValues["F_USEYN"] ?? "").ToString());
                    oParamDic.Add("F_USERID", gsUSERID);

                    oSPs[idx] = "USP_PLC03_UPD";
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
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
        }
        #endregion

        #endregion

        #region DB 처리 함수

        #region 레시피ID구하기
        void GetRECIPID()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                oParamDic["F_FROMDT"] = schF_FROMYMD.Text;
                ds = biz.PLC04_LST(oParamDic, out errMsg);
            }
            schF_RECIPID.DataSource = ds;
            schF_RECIPID.TextField = "tRecipeID";
            schF_RECIPID.ValueField = "tRecipeID";
            schF_RECIPID.DataBind();
            schF_RECIPID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
            schF_RECIPID.SelectedIndex = 0;
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
                oParamDic["F_FROMDT"] = schF_FROMYMD.Text;
                ds = biz.PLC02_LST(oParamDic, out errMsg);
            }
            schF_MASTERID.DataSource = ds;
            schF_MASTERID.TextField = "tMasterNM";
            schF_MASTERID.ValueField = "tID";
            schF_MASTERID.DataBind();
            schF_MASTERID.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "" });
        }
        #endregion

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
            schF_STEP.DataSource = ds;
            schF_STEP.TextField = "F_STEPNM";
            schF_STEP.ValueField = "F_STEP";
            schF_STEP.DataBind();
            schF_STEP.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });
            
        }
        #endregion

        #region STEP2구하기
        void GetSTEP2()
        {
            string errMsg = String.Empty;

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_MACHCD"] = (this.schF_MACHCD.Value ?? "").ToString();
                ds = biz.PLC06_LST(oParamDic, out errMsg);
            }
            schF_STEP2.DataSource = ds;
            schF_STEP2.TextField = "F_STEPNM";
            schF_STEP2.ValueField = "F_STEP";
            schF_STEP2.DataBind();
            schF_STEP2.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택", Value = "" });

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
                oParamDic["F_USERID"] = gsUSERID;
                oParamDic["F_STEP"] = (this.schF_STEP.Value ?? "").ToString();
                oParamDic["F_STEP2"] = (this.schF_STEP2.Value ?? "").ToString();
                ds = biz.PLC03_LST(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 우측 내용 조회
        /// <summary>
        /// 우측 내용 조회(추진일정 목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRightBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();

            using (PLCMBiz biz = new PLCMBiz())
            {
                oParamDic.Clear();
                
                oParamDic["F_MACHCD"] = dic.GetString("F_MACHCD");
                oParamDic["F_FROMDT"] = dic.GetString("F_FROMYMD");
                oParamDic["F_TID"] = schF_MASTERID.Value.ToString();
                oParamDic["F_USERID"] = gsUSERID;
                oParamDic["F_STEP"] = (this.schF_STEP.Value ?? "").ToString();
                oParamDic["F_STEP2"] = (this.schF_STEP2.Value ?? "").ToString();
                ds = biz.USP_PLCM2001_LST1(oParamDic, out errMsg);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                dtTemp.Columns.Add("TIME", typeof(String));
                dtTemp.Columns.Add("STEP", typeof(String));
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++) {
                    dtTemp.Columns.Add(ds.Tables[1].Rows[i]["F_INSPDETAIL"].ToString(), typeof(Double));
                }

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++) {
                    List<string> ts = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["tTime"].ToString()).ToList<string>();
                    List<string> sp = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["tStep"].ToString()).ToList<string>();
                    List<string> ls = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["tData"].ToString()).ToList<string>();
                    for (int a = 0; a < ls.Count; a++) {
                        if (dtTemp.Rows.Count <= a)
                        {
                            dtTemp.Rows.Add();
                        }
                        dtTemp.Rows[a][0] = ts[a];
                        dtTemp.Rows[a][1] = sp[a];
                        dtTemp.Rows[a][i+2] = ls[a];
                    }
                }
            }
            return dtTemp;
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

        protected void schF_MASTERID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetMaster();
        }
        #endregion

        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GET_INSP();
        }

        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        }

        protected void schF_STEP_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSTEP();
        }

        protected void schF_STEP2_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSTEP2();
        }

        protected void schF_RECIPID_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRECIPID();
        }
    }
}