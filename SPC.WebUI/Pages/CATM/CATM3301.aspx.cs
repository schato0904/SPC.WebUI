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
using SPC.CATM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM
{
    public partial class CATM3301 : WebUIBasePage
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
            get { return Session["CATM3301_Grid"] as DataTable; }
            set { Session["CATM3301_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["CATM3301_Grid1"] as DataTable; }
            set { Session["CATM3301_Grid1"] = value; }
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
            }

            GetRecall();
            GetCust();
           
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
            
            this.BindCombo(srcF_CUSTCD);
            //this.schF_OUTYMD.Date = DateTime.Today;

            this.schF_FROMYMD.Date = DateTime.Today;
            this.schF_TOYMD.Date = DateTime.Today;

            this.schF_FROMYMD2.Date = DateTime.Today;
            this.schF_TOYMD2.Date = DateTime.Today;
            
            
            //this.srcF_OUTYMD2.Date = DateTime.Today; 
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

                devCallback3.JSProperties["cpResultCode"] = "";
                devCallback3.JSProperties["cpResultMsg"] = "";
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid 이벤트 처리

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p1 = this.GetLeftParameter();
                string errMsg = string.Empty;
                this.CachedData = this.GetDataRight(p1, out errMsg);

                devGrid.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid.DataBind();
        }
        #endregion

        #endregion

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
                this.CachedData1 = this.GetDataLeftBody(p1, out errMsg);

                devGrid1.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid1.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid1.DataBind();
        }
        #endregion
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void devCallback_Callback(object source, CallbackEventArgs e)
        {
            string msg = string.Empty;
            string errMsg = string.Empty;
            string _error = string.Empty;
            string pkey = string.Empty;
            bool bResult = false;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            Dictionary<string, string> dicP = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                var param = DeserializeJSON(e.Parameter);
                dicResult["action"] = param["action"];
                dicResult["gridhtml"] = "";
                //var F_ITEMCD = param.GetString("F_ITEMCD", "0");
                switch (param["action"])
                {
                    case "VIEW":
                        // 우측 내용 조회
                        dt = View(param, out errMsg);
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        dicResult["data"] = Dr2EncodeJson(dt);
                        break;
                  
                }
                e.Result = SerializeJSON(dicResult);
            }
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void devCallback2_Callback(object source, CallbackEventArgs e)
        {
            string msg = string.Empty;
            string errMsg = string.Empty;
            string _error = string.Empty;
            string pkey = string.Empty;
            bool bResult = false;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            Dictionary<string, string> dicP = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                var param = DeserializeJSON(e.Parameter);
                dicResult["action"] = param["action"];
                dicResult["gridhtml"] = "";
                //var F_ITEMCD = param.GetString("F_ITEMCD", "0");
                switch (param["action"])
                {
                    case "VIEW":
                        // 우측 내용 조회
                        dt = View2(param, out errMsg);
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        dicResult["data"] = Dr2EncodeJson(dt);
                        break;                   
                }
                e.Result = SerializeJSON(dicResult);
            }
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string errMsg = String.Empty;
            this.Download_Excelfile();
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
        protected Dictionary<string, string> GetLeftParameter()
        {
            var result = new Dictionary<string, string>();

            //result["srcF_OUTYMD"] = this.srcF_OUTYMD2.Text ?? "";

            result["srcF_FROMYMD"] = this.schF_FROMYMD2.Text ?? "";
            result["srcF_TOYMD"] = this.schF_TOYMD2.Text ?? "";

            result["srcF_ITEM"] = this.schF_SEARCHTEXT.Text ?? "";
            //result["srcF_CUSTCD"] = srcF_CUSTCD2.SelectedItem.Value.ToString();

            
            return result;
        }
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            //result["F_OUTYMD"] = this.schF_OUTYMD.Text ?? "";

            result["F_FROMYMD"] = this.schF_FROMYMD.Text ?? "";
            result["F_TOYMD"] = this.schF_TOYMD.Text ?? "";
            

            return result;
        }
    

        // 콤보박스 목록 조회
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>() { { "F_CUSTTYPECD", "02" } };
            //dic["F_ITEMTYPECD"] = (c.ClientInstanceName == "schF_ITEMCD") ? "02" : (c.ClientInstanceName == "schF_MELTCD") ? "01" : "";
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_CUSTNM";
            c.ValueField = "F_CUSTCD";
            c.DataBind();
            if (c.Items.Count > 0) c.SelectedIndex = 0;
            //c.Items.Insert(0, new ListEditItem("--전체--", ""));
            c.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
            //c.SelectedIndex = 0;
        }
        #endregion

        #region 액션 처리

        #region View
        protected DataTable View(Dictionary<string, string> pDic, out string errMsg)
        {
            errMsg = string.Empty;
           

            DataTable dt = this.GetDataRightTop(pDic, out errMsg);
            if (!string.IsNullOrEmpty(errMsg)) return null;
       
            return dt;
        }
        #endregion

        #region View2
        protected DataTable View2(Dictionary<string, string> pDic, out string errMsg)
        {
            errMsg = string.Empty;


            DataTable dt = this.GetDataRightTop2(pDic, out errMsg);
            if (!string.IsNullOrEmpty(errMsg)) return null;

            return dt;
        }
        #endregion


        // 정보 조회
        protected DataTable View(string pkey, out string errMsg)
        {
            errMsg = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["F_OUTORDERNO"] = pkey;

            return this.View(dic, out errMsg);
        }
      

      
        #endregion

        #endregion

        #region DB 처리 함수

        #region 좌측 목록 조회
        /// <summary>
        /// 좌측 목록 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRight(Dictionary<string, string> dic, out string errMsg)
        {
            errMsg = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                //oParamDic["F_OUTYMD"] = dic.GetString("srcF_OUTYMD");

                oParamDic["F_FROMYMD"] = dic.GetString("srcF_FROMYMD");
                oParamDic["F_TOYMD"] = dic.GetString("srcF_TOYMD");

                oParamDic["F_ITEM"] = dic.GetString("srcF_ITEM");
                oParamDic["F_CUSTCD"] = srcF_CUSTCD2.SelectedItem.Value.ToString();//dic.GetString("srcF_CUSTCD");
                oParamDic["F_REASONCD"] = srcF_RECALL2.SelectedItem.Value.ToString();
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_LST3(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
            }
            return dt;
        }
        #endregion

        #region 우측 상단 정보 조회
        /// <summary>
        /// 우측 상단 정보 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRightTop(Dictionary<string, string> dic, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                //oParamDic["F_FIXYMD"] = dic.GetString("F_FIXYMD");
                oParamDic["F_OUTORDERNO"] = dic.GetString("F_OUTORDERNO");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_LST2(oParamDic, out errMsg);
            }

            DataTable dt = null;
            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        // 우측상단 반품으로 조회
        protected DataTable GetDataRightTop2(Dictionary<string, string> dic, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                //oParamDic["F_FIXYMD"] = dic.GetString("F_FIXYMD");
                oParamDic["F_OUTORDERNO"] = dic.GetString("F_OUTORDERNO");
                oParamDic["F_SERIAL"] = dic.GetString("F_SERIAL");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_LST4(oParamDic, out errMsg);
            }

            DataTable dt = null;
            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 우측 내용 조회
        /// <summary>
        /// 우측 내용 조회(목록)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataLeftBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_OUTYMD"] = dic.GetString("F_OUTYMD");
                oParamDic["F_FROMYMD"] = dic.GetString("F_FROMYMD");
                oParamDic["F_TOYMD"] = dic.GetString("F_TOYMD");
                oParamDic["F_ITEMCD"] = GetItemCD();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_LST1(oParamDic, out errMsg);
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
        /// 콤보박스 목록 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataCombo(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_CUSTTYPECD"] = dic.GetString("F_CUSTTYPECD");
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM2001_LST3(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region srcF_RECALL_Callback
        protected void srcF_RECALL_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRecall();
        }
        #endregion

        #region srcF_RECALL2_Callback
        protected void srcF_RECALL2_Callback(object sender, CallbackEventArgsBase e)
        {
            GetRecall();
        }
        #endregion

        #region srcF_CUSTCD2_Callback
        protected void srcF_CUSTCD2_Callback(object sender, CallbackEventArgsBase e)
        {
            GetCust();
        }
        #endregion

        #region GetRecall
        void GetRecall()
        {
            string errMsg = String.Empty;

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_DATE"] = (this.schF_PLANYMD.Text ?? "");             
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_COMBO(oParamDic, out errMsg);
            }

            srcF_RECALL.DataSource = ds;
            srcF_RECALL.DataBind();

            srcF_RECALL2.DataSource = ds;
            srcF_RECALL2.DataBind();
            //srcF_RECALL.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "" });
        }
        #endregion

        #endregion

        #region GetCust
        void GetCust()
        {
            string errMsg = String.Empty;

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_DATE"] = (this.schF_PLANYMD.Text ?? "");             
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                //oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM3301_CUST(oParamDic, out errMsg);
            }

            srcF_CUSTCD2.DataSource = ds;
            srcF_CUSTCD2.DataBind();
            
        }
        #endregion

        #region srcF_RECALL_DataBound
        protected void srcF_RECALL_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            srcF_RECALL.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "" });
        }
        #endregion

        #region srcF_RECALL2_DataBound
        protected void srcF_RECALL2_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            srcF_RECALL2.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #region srcF_CUSTCD2_DataBound
        protected void srcF_CUSTCD2_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            srcF_CUSTCD2.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "" });
        }
        #endregion

        #region devCallback3_Callback
        protected void devCallback3_Callback(object source, CallbackEventArgs e)
        {
            bool result = false;
            string errMsg = String.Empty;

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();

                oParamDic["F_GBN"] = hidGBN.Text.ToString();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_SERIAL"] = hidSERIAL.Text.ToString();
                oParamDic["F_OUTORDERNO"] = srcF_OUTORDERNO.Text.ToString();
                oParamDic["F_REASON"]   = srcF_RECALL.SelectedItem.Value.ToString();
                oParamDic["F_RECALLCOUNT"] = F_RECALLCOUNT.Text.ToString();
                oParamDic["F_MEMO"] = srcF_MEMO.Text.ToString();
                oParamDic["F_USER"] = gsUSERID;



                result = biz.USP_CATM3301_INS_UPD(oParamDic, out errMsg);
            }

            if (errMsg != "")
            {
                // Grid Callback Init
                devCallback3.JSProperties["cpResultCode"] = "0";
                devCallback3.JSProperties["cpResultMsg"] = errMsg;
                //e.Result = errMsg;

            }
            else
            {
                if (hidGBN.Text.ToString() == "3")
                {
                    devCallback3.JSProperties["cpResultCode"] = "";
                    devCallback3.JSProperties["cpResultMsg"] = "삭제 되었습니다.";
                }
                else
                {
                    devCallback3.JSProperties["cpResultCode"] = "";
                    devCallback3.JSProperties["cpResultMsg"] = "저장 되었습니다.";
                }
            }

        }
        #endregion




    }
}