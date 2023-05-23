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
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraPrinting;

namespace SPC.WebUI.Pages.PLCM
{
    public partial class PLCM2004 : WebUIBasePage
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
            get { return Session["PLCM2004_Grid"] as DataTable; }
            set { Session["PLCM2004_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["PLCM2004_Grid1"] as DataTable; }
            set { Session["PLCM2004_Grid1"] = value; }
        }

        // 레시피
        DataTable CachedData2
        {
            get { return Session["PLCM2004_RE"] as DataTable; }
            set { Session["PLCM2004_RE"] = value; }
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
            this.Download_Excelfile();
            //devGrid1.DataSource = CachedData1;
            //devGrid1.DataBind();
            //devGrid1Exporter.WriteXlsToResponse(String.Format("[{0}]{1}_{2}_검사항목별Data", schF_MACHCD.Text, DateTime.Today.ToString("yyyyMMdd"), schF_MASTERID.Text), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        void Download_Excelfile()
        {
            PrintingSystemBase ps = new PrintingSystemBase();
            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
            link1.Component = devGrid1Exporter;

            PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
            int w = (int)this.hidChartWidth.Number;
            int h = (int)this.hidChartHeight.Number;
            devChart1.Width = Unit.Pixel(w);
            devChart1.Height = Unit.Pixel(h);
            link2.Component = ((IChartContainer)devChart1).Chart;
            devChart1.DataSource = CachedData1;
            devChart1.DataBind();

            PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
            devChart2.Width = Unit.Pixel(w);
            devChart2.Height = Unit.Pixel(h);
            link3.Component = ((IChartContainer)devChart2).Chart;
            if (CachedData1.Columns.Count > 5)
            {
                devChart2.DataSource = CachedData1;
                devChart2.DataBind();
            }

            PrintableComponentLinkBase link4 = new PrintableComponentLinkBase(ps);
            devChart3.Width = Unit.Pixel(w);
            devChart3.Height = Unit.Pixel(h);
            link4.Component = ((IChartContainer)devChart3).Chart;
            if (CachedData1.Columns.Count > 8)
            {
                devChart3.DataSource = CachedData1;
                devChart3.DataBind();
            }

            PrintableComponentLinkBase link5 = new PrintableComponentLinkBase(ps);
            devChart4.Width = Unit.Pixel(w);
            devChart4.Height = Unit.Pixel(h);
            link5.Component = ((IChartContainer)devChart4).Chart;
            if (CachedData1.Columns.Count > 11)
            {
                devChart4.DataSource = CachedData1;
                devChart4.DataBind();
            }

            PrintableComponentLinkBase link6 = new PrintableComponentLinkBase(ps);
            devChart5.Width = Unit.Pixel(w);
            devChart5.Height = Unit.Pixel(h);
            link6.Component = ((IChartContainer)devChart5).Chart;
            if (CachedData1.Columns.Count > 14)
            {
                devChart5.DataSource = CachedData1;
                devChart5.DataBind();
            }

            PrintableComponentLinkBase link7 = new PrintableComponentLinkBase(ps);
            devChart6.Width = Unit.Pixel(w);
            devChart6.Height = Unit.Pixel(h);
            link7.Component = ((IChartContainer)devChart6).Chart;
            if (CachedData1.Columns.Count > 17)
            {
                devChart6.DataSource = CachedData1;
                devChart6.DataBind();
            }

            PrintableComponentLinkBase link8 = new PrintableComponentLinkBase(ps);
            devChart7.Width = Unit.Pixel(w);
            devChart7.Height = Unit.Pixel(h);
            link8.Component = ((IChartContainer)devChart7).Chart;
            if (CachedData1.Columns.Count > 20)
            {
                devChart7.DataSource = CachedData1;
                devChart7.DataBind();
            }

            PrintableComponentLinkBase link9 = new PrintableComponentLinkBase(ps);
            devChart8.Width = Unit.Pixel(w);
            devChart8.Height = Unit.Pixel(h);
            link9.Component = ((IChartContainer)devChart8).Chart;
            if (CachedData1.Columns.Count > 23)
            {
                devChart8.DataSource = CachedData1;
                devChart8.DataBind();
            }

            PrintableComponentLinkBase link10 = new PrintableComponentLinkBase(ps);
            devChart9.Width = Unit.Pixel(w);
            devChart9.Height = Unit.Pixel(h);
            link10.Component = ((IChartContainer)devChart9).Chart;
            if (CachedData1.Columns.Count > 26)
            {
                devChart9.DataSource = CachedData1;
                devChart9.DataBind();
            }

            PrintableComponentLinkBase link11 = new PrintableComponentLinkBase(ps);
            devChart10.Width = Unit.Pixel(w);
            devChart10.Height = Unit.Pixel(h);
            link11.Component = ((IChartContainer)devChart10).Chart;
            if (CachedData1.Columns.Count > 29)
            {
                devChart10.DataSource = CachedData1;
                devChart10.DataBind();
            }

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link2, link3, link4, link5, link6, link7, link8, link9, link10, link11, link1 });
            compositeLink.CreateDocument();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                string file_name = Uri.EscapeUriString(string.Format("[{0}]{1}_{2}_검사항목별Data.xlsx", schF_MACHCD.Text, DateTime.Today.ToString("yyyyMMdd"), schF_MASTERID.Text));
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
                ds = biz.USP_PLCM2004_LST1(oParamDic, out errMsg);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                dtTemp.Columns.Add("TIME", typeof(String));
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++) {
                    dtTemp.Columns.Add(ds.Tables[1].Rows[i]["F_INSPDETAIL"].ToString(), typeof(float));
                    dtTemp.Columns.Add(ds.Tables[1].Rows[i]["F_INSPDETAIL"].ToString()+"MAX", typeof(float));
                    dtTemp.Columns.Add(ds.Tables[1].Rows[i]["F_INSPDETAIL"].ToString()+"MIN", typeof(float));
                }

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++) {
                    List<string> ts = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["tTime"].ToString()).ToList<string>();
                    List<string> ls = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["tData"].ToString()).ToList<string>();
                    List<string> mx = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["F_MAX"].ToString()).ToList<string>();
                    List<string> mn = ds.Tables[0].AsEnumerable().Where(x => x["tTag"].ToString() == ds.Tables[1].Rows[i]["F_MEASCD1"].ToString()).Select(x => x["F_MIN"].ToString()).ToList<string>();
                    for (int a = 0; a < ls.Count; a++) {
                        if (dtTemp.Rows.Count <= a)
                        {
                            dtTemp.Rows.Add();
                        }
                        dtTemp.Rows[a][0] = ts[a];
                        dtTemp.Rows[a][i*3 + 1] = ls[a];
                        if (mx[a].ToString() != "")
                        {
                            dtTemp.Rows[a][i*3 + 2] = mx[a];
                        }
                        if (mn[a].ToString() != "")
                        {
                            dtTemp.Rows[a][i*3 + 3] = mn[a];
                        }
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

        protected void devChart1_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));

            devChart1.DataSource = null;

            if (CachedData1 != null)
            {
                devChart1.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart1, CachedData1.Columns[1].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[1].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart1, CachedData1.Columns[2].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[2].ColumnName, System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart1, CachedData1.Columns[3].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[3].ColumnName, System.Drawing.Color.Red);
                devChart1.DataSource = CachedData1;
                devChart1.DataBind();
                DevExpressLib.SetChartLegend(devChart1);
                XYDiagram diagram = (XYDiagram)devChart1.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart1, CachedData1.Columns[1].ColumnName, false);
            }
        }

        protected void devChart2_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart2.DataSource = null;
            if (CachedData1 != null && CachedData1.Columns.Count > 5)
            {
                devChart2.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart2, CachedData1.Columns[4].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[4].ColumnName, System.Drawing.Color.SkyBlue);//LightCoral
                DevExpressLib.SetChartLineSeries(devChart2, CachedData1.Columns[5].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[5].ColumnName, System.Drawing.Color.Red);//LightCoral
                DevExpressLib.SetChartLineSeries(devChart2, CachedData1.Columns[6].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[6].ColumnName, System.Drawing.Color.Red);//LightCoral
                devChart2.DataSource = CachedData1;
                devChart2.DataBind();
                DevExpressLib.SetChartLegend(devChart2);
                XYDiagram diagram = (XYDiagram)devChart2.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart2, CachedData1.Columns[4].ColumnName, false);
            }
        }

        protected void devChart3_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart3.DataSource = null;
            if (CachedData1 != null && CachedData1.Columns.Count > 8)
            {
                devChart3.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart3, CachedData1.Columns[7].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[7].ColumnName, System.Drawing.Color.SkyBlue);//DarkGreen
                DevExpressLib.SetChartLineSeries(devChart3, CachedData1.Columns[8].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[8].ColumnName, System.Drawing.Color.Red);//DarkGreen
                DevExpressLib.SetChartLineSeries(devChart3, CachedData1.Columns[9].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[9].ColumnName, System.Drawing.Color.Red);//DarkGreen
                devChart3.DataSource = CachedData1;
                devChart3.DataBind();
                DevExpressLib.SetChartLegend(devChart3);
                XYDiagram diagram = (XYDiagram)devChart3.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart3, CachedData1.Columns[7].ColumnName, false);
            }
        }

        protected void devChart4_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart4.DataSource = null;
            if (CachedData1 != null && CachedData1.Columns.Count > 11)
            {
                devChart4.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart4, CachedData1.Columns[10].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[10].ColumnName, System.Drawing.Color.SkyBlue);//Gold
                DevExpressLib.SetChartLineSeries(devChart4, CachedData1.Columns[11].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[11].ColumnName, System.Drawing.Color.Red);//Gold
                DevExpressLib.SetChartLineSeries(devChart4, CachedData1.Columns[12].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[12].ColumnName, System.Drawing.Color.Red);//Gold
                devChart4.DataSource = CachedData1;
                devChart4.DataBind();
                DevExpressLib.SetChartLegend(devChart4);
                XYDiagram diagram = (XYDiagram)devChart4.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart4, CachedData1.Columns[10].ColumnName, false);
            }
        }

        protected void devChart5_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart5.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count > 14)
            {
                devChart5.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart5, CachedData1.Columns[13].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[13].ColumnName, System.Drawing.Color.SkyBlue);//Violet
                DevExpressLib.SetChartLineSeries(devChart5, CachedData1.Columns[14].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[14].ColumnName, System.Drawing.Color.Red);//Violet
                DevExpressLib.SetChartLineSeries(devChart5, CachedData1.Columns[15].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[15].ColumnName, System.Drawing.Color.Red);//Violet
                devChart5.DataSource = CachedData1;
                devChart5.DataBind();
                DevExpressLib.SetChartLegend(devChart5);
                XYDiagram diagram = (XYDiagram)devChart5.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart5, CachedData1.Columns[13].ColumnName, false);
            }
        }

        protected void devChart6_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart6.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count > 17)
            {
                devChart6.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart6, CachedData1.Columns[16].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[16].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart6, CachedData1.Columns[17].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[17].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart6, CachedData1.Columns[18].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[18].ColumnName, System.Drawing.Color.SkyBlue);
                devChart6.DataSource = CachedData1;
                devChart6.DataBind();
                DevExpressLib.SetChartLegend(devChart6);
                XYDiagram diagram = (XYDiagram)devChart6.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart6, CachedData1.Columns[16].ColumnName, false);
            }
        }

        protected void devChart7_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart7.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count >20)
            {
                devChart7.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart7, CachedData1.Columns[19].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[19].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart7, CachedData1.Columns[20].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[20].ColumnName, System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart7, CachedData1.Columns[21].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[21].ColumnName, System.Drawing.Color.Red);
                devChart7.DataSource = CachedData1;
                devChart7.DataBind();
                DevExpressLib.SetChartLegend(devChart7);
                XYDiagram diagram = (XYDiagram)devChart7.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart7, CachedData1.Columns[19].ColumnName, false);
            }
        }

        protected void devChart8_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart8.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count > 23)
            {
                devChart8.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart8, CachedData1.Columns[22].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[22].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart8, CachedData1.Columns[23].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[23].ColumnName, System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart8, CachedData1.Columns[24].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[24].ColumnName, System.Drawing.Color.Red);
                devChart8.DataSource = CachedData1;
                devChart8.DataBind();
                DevExpressLib.SetChartLegend(devChart8);
                XYDiagram diagram = (XYDiagram)devChart8.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart8, CachedData1.Columns[22].ColumnName, false);
            }
        }

        protected void devChart9_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart9.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count > 26)
            {
                devChart9.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart9, CachedData1.Columns[25].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[25].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart9, CachedData1.Columns[26].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[26].ColumnName, System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart9, CachedData1.Columns[27].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[27].ColumnName, System.Drawing.Color.Red);
                devChart9.DataSource = CachedData1;
                devChart9.DataBind();
                DevExpressLib.SetChartLegend(devChart9);
                XYDiagram diagram = (XYDiagram)devChart9.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart9, CachedData1.Columns[25].ColumnName, false);
            }
        }

        protected void devChart10_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split('|');
            devChart_ResizeTo(sender, Convert.ToInt32(oParams[0]), Convert.ToInt32(oParams[1]));
            devChart10.DataSource = null;

            if (CachedData1 != null && CachedData1.Columns.Count > 29)
            {
                devChart10.Series.Clear();
                DevExpressLib.SetChartLineSeries(devChart10, CachedData1.Columns[28].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[28].ColumnName, System.Drawing.Color.SkyBlue);
                DevExpressLib.SetChartLineSeries(devChart10, CachedData1.Columns[29].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[29].ColumnName, System.Drawing.Color.Red);
                DevExpressLib.SetChartLineSeries(devChart10, CachedData1.Columns[30].ColumnName, CachedData1.Columns[0].ColumnName, CachedData1.Columns[30].ColumnName, System.Drawing.Color.Red);
                devChart10.DataSource = CachedData1;
                devChart10.DataBind();
                DevExpressLib.SetChartLegend(devChart10);
                XYDiagram diagram = (XYDiagram)devChart10.Diagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisY.VisualRange.Auto = true;
                diagram.AxisY.WholeRange.Auto = true;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                DevExpressLib.SetChartTitle(devChart10, CachedData1.Columns[28].ColumnName, false);
            }
        }

        #region chart ResizeTo
        /// <summary>
        /// devChart_ResizeTo
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="Width">Int32</param>
        void devChart_ResizeTo(object sender, Int32 Width, Int32 Height)
        {
            if (Width >= 0 && Height >= 0)
            {
                DevExpress.XtraCharts.Web.WebChartControl chart = sender as DevExpress.XtraCharts.Web.WebChartControl;
                chart.Width = new Unit(Width);
                chart.Height = new Unit(Height);

                chart.JSProperties["cpFunction"] = "resizeTo";
                chart.JSProperties["cpChartWidth"] = Width.ToString();
            }
        }
        #endregion
    }
}