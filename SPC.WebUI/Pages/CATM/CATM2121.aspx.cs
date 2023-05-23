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
    public partial class CATM2121 : WebUIBasePage
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
            get { return Session["CATM2121_Grid"] as DataTable; }
            set { Session["CATM2121_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["CATM2121_Grid1"] as DataTable; }
            set { Session["CATM2121_Grid1"] = value; }
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
            //this.CachedData = null;
            //this.CachedData1 = null;
            //this.CachedData2 = null;
            //this.CachedData3 = null;
            //this.CachedData4 = null;
            //this.CachedData5 = null;
            //this.CachedData6 = null;

            //schF_LINECD.DataBind();
            //schF_DEPTCD.DataBind();
            //srcF_TERM.DataBind();
            this.BindCombo(srcF_CUSTCD);
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
                this.CachedData = this.GetDataLeft(p1, out errMsg);

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
                this.CachedData1 = this.GetDataRightBody(p1, out errMsg);

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
                    case "SAVE":
                        bResult = this.Save(this.GetRightData(), out pkey, out errMsg);
                        if (bResult && string.IsNullOrWhiteSpace(errMsg))
                        {
                            dt = View(pkey, out errMsg);
                            dicResult["data"] = Dr2EncodeJson(dt);
                        }
                        else
                        {
                            dicResult["data"] = "";
                        }
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        //dicResult["data"] = Dr2EncodeJson(dt);
                        break;
                    case "UPDATE":
                        bResult = this.Update(this.GetRightData(), out pkey, out errMsg);
                        if (bResult && string.IsNullOrWhiteSpace(errMsg))
                        {
                            dt = View(pkey, out errMsg);
                            dicResult["data"] = Dr2EncodeJson(dt);
                        }
                        else
                        {
                            dicResult["data"] = "";
                        }
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        //dicResult["data"] = Dr2EncodeJson(dt);
                        break;
                    case "DELETE":
                        // 우측 내용 조회
                        bResult = this.Delete(this.GetRightData(), out errMsg);
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        dicResult["data"] = "";
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

            result["F_SEARCHTEXT"] = this.schF_SEARCHTEXT.Text ?? "";

            return result;
        }
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_ITEMCD"] = this.srcF_ITEMCD.Text ?? "";

            return result;
        }
        // 저장 시 사용
        protected Dictionary<string, string> GetRightData()
        {
            var result = new Dictionary<string, string>();

            result["F_INSTOCKNO"] = this.srcF_INSTOCKNO.Text ?? "";
            result["F_INTYPECD"] = (this.srcF_INTYPECD.Value ?? "").ToString();
            result["F_INYMD"] = this.srcF_INDATE.Text ?? "";
            result["F_INDATE"] = this.srcF_INDATE.Text ?? "";
            result["F_CUSTCD"] = (this.srcF_CUSTCD.Value ?? "").ToString();
            result["F_ITEMCD"] = this.srcF_ITEMCD.Text ?? "";
            result["F_INCNT"] = this.srcF_INCNT.Text ?? "0";
            result["F_MEMO"] = this.srcF_MEMO.Text ?? "";
            result["F_LOTNO"] = this.srcF_LOTNO.Text ?? "";

            return result;
        }

        // 콤보박스 목록 조회
        void BindCombo(ASPxComboBox c)
        {
            var dic = new Dictionary<string, string>() { {"F_CUSTTYPECD", "01"} };
            //dic["F_ITEMTYPECD"] = (c.ClientInstanceName == "schF_ITEMCD") ? "02" : (c.ClientInstanceName == "schF_MELTCD") ? "01" : "";
            string errMsg = string.Empty;
            DataTable dt = this.GetDataCombo(dic, out errMsg);
            c.DataSource = dt;
            c.TextField = "F_CUSTNM";
            c.ValueField = "F_CUSTCD";
            c.DataBind();
            if (c.Items.Count > 0) c.SelectedIndex = 0;
            //c.Items.Insert(0, new ListEditItem("--전체--", ""));
            //c.SelectedIndex = 0;
        }
        #endregion

        #region 액션 처리
        //// 정보 조회
        //protected DataTable View(out string errMsg)
        //{
        //    //return this.View(this.srcF_PJ10MID.Text, out errMsg);
        //    errMsg = string.Empty;
        //    return null;
        //}
        // 정보 조회
        protected DataTable View(Dictionary<string, string> pDic, out string errMsg)
        {
            errMsg = string.Empty;
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //DataSet ds = this.GetDataRightTop(pDic, out errMsg);
            //if (!string.IsNullOrEmpty(errMsg)) return null;
            //this.CachedData1 = ds.Tables[0];

            DataTable dt = this.GetDataRightTop(pDic, out errMsg);
            if (!string.IsNullOrEmpty(errMsg)) return null;
            //this.CachedData1 = dt;

            //this.CachedData2 = AutoNumberTable(ds.Tables[0]);
            //this.CachedData3 = AutoNumberTable(ds.Tables[1]);

            //return this.CachedData1.Copy();
            return dt;
        }
        // 정보 조회
        protected DataTable View(string pkey, out string errMsg)
        {
            errMsg = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["F_INSTOCKNO"] = pkey;

            return this.View(dic, out errMsg);
        }

        // 저장/업데이트
        protected bool Save(Dictionary<string, string> p1, out string pkey, out string pErr)
        {
            //Dictionary<string, string> p1 = new Dictionary<string, string>();
            //List<Dictionary<string, string>> p2 = new List<Dictionary<string, string>>();
            string errMsg = string.Empty;
            pErr = string.Empty;
            bool bResult = false;

            //p1 = this.GetTargetData();
            //p2 = this.GetTargetGridData();
            //pkey = p1["F_INSTOCKNO"];

            bResult = this.InsertData(p1, out errMsg, out pkey);

            pErr += errMsg;

            return bResult;
        }

        // 저장/업데이트
        protected bool Update(Dictionary<string, string> p1, out string pkey, out string pErr)
        {
            //Dictionary<string, string> p1 = new Dictionary<string, string>();
            //List<Dictionary<string, string>> p2 = new List<Dictionary<string, string>>();
            string errMsg = string.Empty;
            pErr = string.Empty;
            bool bResult = false;

            //p1 = this.GetTargetData();
            //p2 = this.GetTargetGridData();
            pkey = p1["F_INSTOCKNO"];

            bResult = this.UpdateData(p1, out errMsg);

            pErr += errMsg;

            return bResult;
        }

        // 저장/업데이트
        protected bool Delete(Dictionary<string, string> p1, out string pErr)
        {
            //Dictionary<string, string> p1 = new Dictionary<string, string>();
            //List<Dictionary<string, string>> p2 = new List<Dictionary<string, string>>();
            string errMsg = string.Empty;
            pErr = string.Empty;
            bool bResult = false;

            bResult = this.DeleteData(p1, out errMsg);

            pErr = errMsg;

            return bResult;
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
        protected DataTable GetDataLeft(Dictionary<string, string> dic, out string errMsg)
        {
            errMsg = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_SEARCHTEXT"] = dic.GetString("F_SEARCHTEXT");
                oParamDic["F_GUBUN"] = this.srcF_GUBUNCD.GetValue();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM2121_LST1(oParamDic, out errMsg);
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
                oParamDic["F_INSTOCKNO"] = dic.GetString("F_INSTOCKNO");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM2121_LST2(oParamDic, out errMsg);
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
        protected DataTable GetDataRightBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM2121_LST3(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region 데이터 저장
        /// <summary>
        /// 데이터 저장
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected bool InsertData(Dictionary<string, string> dic, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool result = false;
            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();

                //oParamDic["F_INSTOCKNO"] = dic.GetString("F_INSTOCKNO");
                oParamDic["F_INTYPECD"] = dic.GetString("F_INTYPECD");
                oParamDic["F_INYMD"] = dic.GetString("F_INYMD");
                oParamDic["F_INDATE"] = dic.GetString("F_INDATE");
                oParamDic["F_CUSTCD"] = dic.GetString("F_CUSTCD");
                oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                oParamDic["F_INCNT"] = dic.GetString("F_INCNT");
                oParamDic["F_MEMO"] = dic.GetString("F_MEMO");
                oParamDic["F_LOTNO"] = dic.GetString("F_LOTNO");

                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LOGINUSER"] = gsUSERID;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                result = biz.USP_CATM2121_INS1(oParamDic, out errMsg, out pkey);
            }
            return result;
        }
        #endregion

        #region 데이터 삭제
        /// <summary>
        /// 데이터 삭제
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected bool DeleteData(Dictionary<string, string> dic, out string errMsg)
        {
            bool result = false;
            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_INSTOCKNO"] = dic.GetString("F_INSTOCKNO");

                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LOGINUSER"] = gsUSERID;
                oParamDic["F_LANGTYPE"] = gsLANGTP;

                result = biz.USP_CATM2121_DEL1(oParamDic, out errMsg);
            }
            return result;
        }
        #endregion

        #region 데이터 업데이트
        /// <summary>
        /// 데이터 업데이트
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected bool UpdateData(Dictionary<string, string> dic, out string errMsg)
        {
            bool result = false;
            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();

                oParamDic["F_INSTOCKNO"] = dic.GetString("F_INSTOCKNO");
                oParamDic["F_INTYPECD"] = dic.GetString("F_INTYPECD");
                oParamDic["F_INYMD"] = dic.GetString("F_INYMD");
                oParamDic["F_INDATE"] = dic.GetString("F_INDATE");
                oParamDic["F_CUSTCD"] = dic.GetString("F_CUSTCD");
                oParamDic["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                oParamDic["F_INCNT"] = dic.GetString("F_INCNT");
                oParamDic["F_MEMO"] = dic.GetString("F_MEMO");
                oParamDic["F_LOTNO"] = dic.GetString("F_LOTNO");

                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LOGINUSER"] = gsUSERID;
                oParamDic["F_LANGTYPE"] = gsLANGTP;

                result = biz.USP_CATM2121_UPD1(oParamDic, out errMsg);
            }
            return result;
        }
        #endregion

        #region 사입처 목록 조회
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
        #endregion
    }
}