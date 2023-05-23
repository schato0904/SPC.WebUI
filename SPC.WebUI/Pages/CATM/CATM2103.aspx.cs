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
    public partial class CATM2103 : WebUIBasePage
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
            get { return Session["CATM2103_Grid"] as DataTable; }
            set { Session["CATM2103_Grid"] = value; }
        }

        //// 우측목록1
        //DataTable CachedData1
        //{
        //    get { return Session["CATM2103_Grid1"] as DataTable; }
        //    set { Session["CATM2103_Grid1"] = value; }
        //}

        //// 우측목록2
        //DataTable CachedData2
        //{
        //    get { return Session["CATM2103_Grid2"] as DataTable; }
        //    set { Session["CATM2103_Grid2"] = value; }
        //}
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
                //this.CachedData1 = null;
            }
            //this.CachedData1 = null;
            //this.CachedData2 = null;
            //BindCombo(srcF_MACHCD);
            //BindCombo(srcF_MELTCD);
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
                //var F_MACHCD = param.GetString("F_MACHCD", "0");
                switch (param["action"])
                {
                    case "VIEW":
                        // 우측 내용 조회
                        dt = View(param, out errMsg);
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        dicResult["data"] = "";// Dr2EncodeJson(dt);
                        dicResult["gridhtml"] = this.GetGridHtml();
                        break;
                    case "SAVE":
                        bResult = this.Save(this.GetRightData(), out pkey, out errMsg);
                        if (bResult && string.IsNullOrWhiteSpace(errMsg))
                        {
                            dt = View(param, out errMsg);
                            //dicResult["data"] = Dr2EncodeJson(dt);
                            dicResult["gridhtml"] = this.GetGridHtml();
                        }
                        else
                        {
                            //dicResult["data"] = "";
                            dicResult["gridhtml"] = "";
                        }
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        //dicResult["data"] = Dr2EncodeJson(dt);
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
            //DevExpress.XtraPrinting.PrintingSystemBase ps = new DevExpress.XtraPrinting.PrintingSystemBase();

            //DevExpress.XtraPrintingLinks.PrintableComponentLinkBase link1 = new DevExpress.XtraPrintingLinks.PrintableComponentLinkBase(ps);
            //link1.Component = devGrid1Exporter;

            //DevExpress.XtraPrintingLinks.CompositeLinkBase compositeLink = new DevExpress.XtraPrintingLinks.CompositeLinkBase(ps);
            //compositeLink.Links.AddRange(new object[] { link1 });
            //compositeLink.CreateDocument();

            //using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            //{
            //    string file_name = Uri.EscapeUriString(string.Format("{0}_{1}.xlsx", this.Request["pParam"].Split('|')[4], DateTime.Now.ToString("yyyyMMddHHmmss")));
            //    DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
            //    options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;

            //    compositeLink.PrintingSystemBase.ExportToXlsx(stream, options);
            //    Response.Clear();
            //    Response.Buffer = false;
            //    Response.AppendHeader("Content-Type", "application/xlsx");
            //    Response.AppendHeader("Content-Transfer-Encoding", "binary");
            //    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", file_name));
            //    Response.BinaryWrite(stream.ToArray());
            //    Response.End();
            //}
            //ps.Dispose();
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

            //result["F_PLANYMD"] = (this.schF_PLANYMD.Text ?? "");

            return result;
        }
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            //result["F_PLANYMD"] =(this.schF_PLANYMD.Text ?? "");

            return result;
        }
        // 저장 시 사용
        protected Dictionary<string, string> GetRightData()
        {
            var result = new Dictionary<string, string>();

            //result["F_WORKNO"] = this.srcF_WORKNO.Text ?? "";
            //result["F_ITEMCD"] = this.srcF_ITEMCD.Text ?? "";
            //result["F_PRODCOUNT"] = this.srcF_PRODCOUNT.Text ?? "";

            return result;
        }
        // 저장/업데이트 시 사용 : 불량유형별 수량
        protected List<Dictionary<string, string>> GetTargetGridData()
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var oParams = js.Deserialize<Dictionary<string, string>[]>(HttpUtility.UrlDecode(this.hidGridData.Text));
            int rowcnt = oParams.Length;
            for (int i = 0; i < rowcnt; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["F_ITEMCD"] = oParams[i]["F_ITEMCD"];
                dic["F_INCNT"] = oParams[i]["F_FIXCNT"];
                dic["F_MEMO"] = oParams[i]["F_MEMO"];
                results.Add(dic);
            }
            return results;
        }
        #endregion

        #region 액션 처리
        // 정보 조회
        protected DataTable View(Dictionary<string, string> pDic, out string errMsg)
        {
            errMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(pDic.GetString("F_INYMD"))) pDic["F_INYMD"] = this.schF_INYMD.Text;
            DataSet ds = this.GetDataRight(pDic, out errMsg);
            if (!string.IsNullOrEmpty(errMsg)) return null;
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
                this.CachedData = dt.Copy();
            }
            //if (ds != null && ds.Tables.Count > 1) this.CachedData1 = this.AutoNumberTable(ds.Tables[1]);
            //if (ds != null && ds.Tables.Count > 2) this.CachedData2 = this.AutoNumberTable(ds.Tables[2]);

            return dt;
        }
        // 정보 조회
        protected DataTable View(string pkey, out string errMsg)
        {
            errMsg = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["F_INYMD"] = pkey;
            return this.View(dic, out errMsg);
        }

        // 저장
        protected bool Save(Dictionary<string, string> p1, out string pkey, out string pErr)
        {
            List<Dictionary<string, string>> p2 = new List<Dictionary<string, string>>();
            //List<Dictionary<string, string>> p3 = new List<Dictionary<string, string>>();
            string errMsg = string.Empty;
            pErr = string.Empty;
            bool bResult = false;

            //p1 = this.GetTargetData();
            p2 = this.GetTargetGridData();
            //p3 = this.GetTargetGridData2();
            pkey = this.schF_INYMD.Text;
            p1["F_INYMD"] = pkey;

            bResult = this.InsertData(p1, p2, out errMsg);

            pErr += errMsg;

            return bResult;
        }
        #endregion

        #region 그리드 html생성
        /// <summary>
        /// 그리드 html 생성
        /// </summary>
        /// <param name="F_JG21MID"></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetGridHtml()
        {
            string result = string.Empty;

            Dictionary<string, string> dic = new Dictionary<string, string>();

            //dic.Add("html1", HttpUtility.UrlPathEncode(this.GetGridHtml1()));
            string lefthtml = string.Empty;
            dic.Add("html1", this.EscapeDataString(this.GetGridHtml1()));
            //dic.Add("html2", this.EscapeDataString(this.GetGridHtml2()));
            //dic.Add("html2", this.EscapeDataString(lefthtml));

            return dic;
        }

        #region 그리드 HTML
        /// <summary>
        /// 그리드 html 반환
        /// </summary>
        /// <param name="F_JG21MID"></param>
        /// <returns></returns>
        protected string GetGridHtml1()
        {
            #region 변수 및 기초값 설정
            //lefthtml = string.Empty;
            string result = string.Empty;
            string errMsg = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (this.CachedData == null) return result;

            // 불량유형별 수량 정보
            DataTable dt1 = this.CachedData.Copy();
            #endregion

            #region html 생성
            // HEADER
            sb.AppendLine("<table id='_cTab1' class='_cTab' border='0'>");
            sb.AppendLine("	<thead>");
            sb.AppendLine("	<tr class='_cTrH'>");
            sb.AppendLine("		<th class='_cTdH wNo' >No</th>");
            sb.AppendLine("		<th class='_cTdH wF_ITEMCD' >품목코드</th>");
            sb.AppendLine("		<th class='_cTdH wF_ITEMNM' >품목명</th>");
            sb.AppendLine("		<th class='_cTdH wF_REMAINS' >현재고(조정일)</th>");
            sb.AppendLine("		<th class='_cTdH wF_REALCNT' >실사수량</th>");
            sb.AppendLine("		<th class='_cTdH wF_FIXCNT' >조정수량</th>");
            sb.AppendLine("		<th class='_cTdH wF_UNIT' >단위</th>");
            sb.AppendLine("		<th class='_cTdH wF_MEMO' >비고</th>");
            sb.AppendLine("	</tr>");
            sb.AppendLine("	</thead>");
            sb.AppendLine("	<tbody>");

            // DETAIL
            int rowIdx = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                int No = (int)dr["No"];
                string F_ITEMCD = dr["F_ITEMCD"].ToString();
                string F_ITEMNM = dr["F_ITEMNM"].ToString();
                string F_REMAINS = ((int)dr["F_REMAINS"]).ToString("#,0");
                string F_REALCNT = dr["F_REALCNT"].ToString();
                string F_FIXCNT = dr["F_FIXCNT"].ToString();
                string F_NEEDCOUNT = dr["F_NEEDCOUNT"].ToString();
                string F_UNIT = dr["F_UNIT"].ToString();
                string F_MEMO = dr["F_MEMO"].ToString();

                string jsonData = this.Dr2EncodeJson(dr);

                // DATA ROW <TR>
                sb.AppendFormat("	<tr class='_cTrD' id='_cTrD_{0}'>", rowIdx).AppendLine();
                sb.AppendFormat("		<td class='_cTdD wNo'>{0}", No).AppendLine();
                sb.AppendFormat("			<input type='hidden' id='_JSONDATA_{0}' value='{1}' /> ", rowIdx, jsonData).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();

                sb.AppendFormat("       <td class='_cTdD wF_ITEMCD'>{0}</td>", F_ITEMCD).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_ITEMNM'>{0}</td>", F_ITEMNM).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_REMAINS'>{0}</td>", F_REMAINS).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_REALCNT'><input type='text' id='_F_REALCNT_{0}' value='{1}' onblur='F_REALCNT_OnBlur({0});' onfocus='this.select();' /></td>", rowIdx, F_REALCNT).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_FIXCNT'><input type='text' id='_F_FIXCNT_{0}' value='{1}' onblur='F_FIXCNT_OnBlur({0});' onfocus='this.select();' /></td>", rowIdx, F_FIXCNT).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_UNIT'>{0}</td>", F_UNIT).AppendLine();
                sb.AppendFormat("       <td class='_cTdD wF_MEMO'><input type='text' id='_F_MEMO_{0}' value='{1}' /></td>", rowIdx, F_MEMO).AppendLine();

                sb.AppendFormat("	</tr>").AppendLine();
                rowIdx++;
            }
            sb.AppendLine("	</tbody>");
            sb.AppendLine("</table>");
            #endregion

            result = sb.ToString();

            return result;
        } 
        #endregion
        #endregion

        #endregion

        #region DB 처리 함수

        #region 우측 정보 조회
        /// <summary>
        /// 우측 상단 정보 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataSet GetDataRight(Dictionary<string, string> dic, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_INYMD"] = dic.GetString("F_INYMD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM2103_LST1(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 데이터 저장
        /// <summary>
        /// 데이터 저장
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected bool InsertData(Dictionary<string, string> dic, List<Dictionary<string, string>> dic2, out string errMsg)
        {
            bool result = false;
            string F_INYMD = dic.GetString("F_INYMD");
            using (CATMBiz biz = new CATMBiz())
            {
                // 생산실적 파라미터
                var p1 = new Dictionary<string, string>();
                //p1["F_INTYPECD"] = "03";
                p1["F_INYMD"] = F_INYMD;
                //p1["F_CUSTCD"] = "";
                //p1["F_ITEMCD"] = dic.GetString("F_ITEMCD");
                //p1["F_ERRCOUNT"] = dic.GetString("F_ERRCOUNT", "0");
                //p1["F_COMPCD"] = gsCOMPCD;
                //p1["F_FACTCD"] = gsFACTCD;
                //p1["F_LOGINUSER"] = gsUSERID;
                //p1["F_LANGTYPE"] = gsLANGTP;

                // 품목별 실사조정 파라미터 목록
                var p2s = new List<Dictionary<string, string>>();
                foreach (var tmp in dic2)
                {
                    var tmp1 = new Dictionary<string, string>();
                    tmp1["F_INTYPECD"] = "03";
                    tmp1["F_INYMD"] = F_INYMD;
                    tmp1["F_CUSTCD"] = "";
                    tmp1["F_ITEMCD"] = tmp.GetString("F_ITEMCD");
                    tmp1["F_INCNT"] = tmp.GetString("F_INCNT", "0");
                    tmp1["F_MEMO"] = tmp.GetString("F_MEMO");
                    tmp1["F_COMPCD"] = gsCOMPCD;
                    tmp1["F_FACTCD"] = gsFACTCD;
                    tmp1["F_LOGINUSER"] = gsUSERID;
                    tmp1["F_LANGTYPE"] = gsLANGTP;
                    p2s.Add(tmp1);
                }

                result = biz.USP_CATM2103_INSERT(p2s, out errMsg);
            }
            return result;
        }
        #endregion
        #endregion
    }
}