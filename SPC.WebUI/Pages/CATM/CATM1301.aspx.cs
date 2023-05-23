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
    public partial class CATM1301 : WebUIBasePage
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
            get { return Session["CATM1301_Grid"] as DataTable; }
            set { Session["CATM1301_Grid"] = value; }
        }

        // 우측목록1
        DataTable CachedData1
        {
            get { return Session["CATM1301_Grid1"] as DataTable; }
            set { Session["CATM1301_Grid1"] = value; }
        }

        // 우측목록2
        DataTable CachedData2
        {
            get { return Session["CATM1301_Grid2"] as DataTable; }
            set { Session["CATM1301_Grid2"] = value; }
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
                this.CachedData2 = null;
            }
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
                        dicResult["data"] = Dr2EncodeJson(dt);
                        dicResult["gridhtml"] = this.GetGridHtml();
                        break;
                    case "SAVE":
                        bResult = this.Save(this.GetRightData(), out pkey, out errMsg);
                        if (bResult && string.IsNullOrWhiteSpace(errMsg))
                        {
                            dt = View(param, out errMsg);
                            dicResult["data"] = Dr2EncodeJson(dt);
                            dicResult["gridhtml"] = this.GetGridHtml();
                        }
                        else
                        {
                            dicResult["data"] = "";
                            dicResult["gridhtml"] = "";
                        }
                        dicResult["error"] = errMsg;
                        dicResult["msg"] = msg;
                        //dicResult["data"] = Dr2EncodeJson(dt);
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

            result["F_PLANYMD"] = (this.schF_PLANYMD.Text ?? "");

            return result;
        }
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_PLANYMD"] =(this.schF_PLANYMD.Text ?? "");

            return result;
        }
        // 저장 시 사용
        protected Dictionary<string, string> GetRightData()
        {
            var result = new Dictionary<string, string>();

            result["F_WORKNO"] = this.srcF_WORKNO.Text ?? "";
            result["F_ITEMCD"] = this.srcF_ITEMCD.Text ?? "";
            result["F_PRODCOUNT"] = this.srcF_PRODCOUNT.Text ?? "";

            return result;
        }
        // 저장/업데이트 시 사용 : 불량유형별 수량
        protected List<Dictionary<string, string>> GetTargetGridData1()
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var oParams = js.Deserialize<Dictionary<string, string>[]>(HttpUtility.UrlDecode(this.hidGridData1.Text));
            int rowcnt = oParams.Length;
            for (int i = 0; i < rowcnt; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["F_ERRORCD"] = oParams[i]["F_ERRORCD"];
                dic["F_ERRORCOUNT"] = oParams[i]["F_ERRORCOUNT"];
                results.Add(dic);
            }
            return results;
        }
        // 저장/업데이트 시 사용 : 유실유형별 시간
        protected List<Dictionary<string, string>> GetTargetGridData2()
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var oParams = js.Deserialize<Dictionary<string, string>[]>(HttpUtility.UrlDecode(this.hidGridData2.Text));
            int rowcnt = oParams.Length;
            for (int i = 0; i < rowcnt; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["F_LOSSCD"] = oParams[i]["F_LOSSCD"];
                dic["F_LOSSORD"] = (i + 1).ToString();
                dic["F_FROMDT"] = oParams[i]["F_FROMDT"];
                dic["F_TODT"] = oParams[i]["F_TODT"];
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

            DataSet ds = this.GetDataRight(pDic, out errMsg);
            if (!string.IsNullOrEmpty(errMsg)) return null;
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            if (ds != null && ds.Tables.Count > 1) this.CachedData1 = this.AutoNumberTable(ds.Tables[1]);
            if (ds != null && ds.Tables.Count > 2) this.CachedData2 = this.AutoNumberTable(ds.Tables[2]);

            return dt;
        }
        // 정보 조회
        protected DataTable View(string pkey, out string errMsg)
        {
            errMsg = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["F_WORKNO"] = pkey;
            return this.View(dic, out errMsg);
        }

        // 저장
        protected bool Save(Dictionary<string, string> p1, out string pkey, out string pErr)
        {
            List<Dictionary<string, string>> p2 = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> p3 = new List<Dictionary<string, string>>();
            string errMsg = string.Empty;
            pErr = string.Empty;
            bool bResult = false;

            //p1 = this.GetTargetData();
            p2 = this.GetTargetGridData1();
            p3 = this.GetTargetGridData2();
            pkey = p1["F_WORKNO"];
            #region 불량수량 집계 파라미터 처리
            int errcnt = 0;
            int tmp = 0;
            foreach (var p in p2)
            {
                if (int.TryParse(p["F_ERRORCOUNT"], out tmp))
                {
                    errcnt += tmp;
                }
            }
            p1["F_ERRCOUNT"] = errcnt.ToString(); 
            #endregion

            bResult = this.InsertData(p1, p2, p3, out errMsg);

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
            dic.Add("html2", this.EscapeDataString(this.GetGridHtml2()));
            //dic.Add("html2", this.EscapeDataString(lefthtml));

            return dic;
        }

        #region 불량유형별 수량 그리드 HTML
        /// <summary>
        /// 불량유형별 수량 그리드 html 반환
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

            // 불량유형별 수량 정보
            DataTable dt1 = this.CachedData1.Copy();
            #endregion

            #region html 생성
            // HEADER
            sb.AppendLine("<table id='_cTab1' class='_cTab' border='0'>");
            sb.AppendLine("	<thead>");
            sb.AppendLine("	<tr class='_cTrH'>");
            sb.AppendLine("		<th class='_cTdH wNo' >No</th>");
            sb.AppendLine("		<th class='_cTdH wF_ERRORNM' >불량유형</th>");
            sb.AppendLine("		<th class='_cTdH wF_ERRORCOUNT' >수량</th>");
            sb.AppendLine("	</tr>");
            sb.AppendLine("	</thead>");
            sb.AppendLine("	<tbody>");

            // DETAIL
            int rowIdx = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                int No = (int)dr["No"];
                string F_ERRORCD = dr["F_ERRORCD"].ToString();
                string F_ERRORNM = dr["F_ERRORNM"].ToString();
                string F_ERRORCOUNT = dr["F_ERRORCOUNT"].ToString();

                string jsonData = this.Dr2EncodeJson(dr);

                // DATA ROW <TR>
                sb.AppendFormat("	<tr class='_cTrD' id='_cTrD_{0}'>", rowIdx).AppendLine();
                sb.AppendFormat("		<td class='_cTdD wNo'>{0}", No).AppendLine();
                sb.AppendFormat("			<input type='hidden' id='_JSONDATA1_{0}' value='{1}' /> ", rowIdx, jsonData).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();

                // 불량유형
                sb.AppendFormat("		<td class='_cTdD wF_ERRORNM'>").AppendLine();
                sb.AppendFormat("			{0}", F_ERRORNM).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();
                // 수량
                sb.AppendFormat("		<td class='_cTdD wF_ERRORCOUNT'>").AppendLine();
                sb.AppendFormat("			<input type='text' id='_F_ERRORCOUNT_{0}' value='{1}' onkeypress='return IsNumber(event)' />", rowIdx, F_ERRORCOUNT).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();

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

        #region 유실유형별 시간 그리드 HTML
        /// <summary>
        /// 유실유형별 시간 그리드 html 반환
        /// </summary>
        /// <param name="F_JG21MID"></param>
        /// <returns></returns>
        protected string GetGridHtml2()
        {
            #region 변수 및 기초값 설정
            //lefthtml = string.Empty;
            string result = string.Empty;
            string errMsg = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // 유실유형별 수량 정보
            DataTable dt2 = this.CachedData2.Copy();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("F_LOSSCD", typeof(string));
            dt1.Columns.Add("F_LOSSNM", typeof(string));
            dt1.Columns.Add("F_LOSSHOUR", typeof(string));
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    string CD = dr["F_LOSSCD"].ToString();
            //    if (dt1.AsEnumerable().Any(x => x["F_LOSSCD"].ToString() == CD)) continue;
            //    string NM = dr["F_LOSSNM"].ToString();
            //    string HH = dt2.AsEnumerable().Where(x => x["F_LOSSCD"].ToString() == CD).Sum(x => (decimal)x["F_LOSSHOUR"]).ToString("0.##");
            //    dt1.Rows.Add(CD, NM, HH);
            //}
            dt1 = dt2.AsEnumerable().GroupBy(r => (string)r["F_LOSSCD"])
                .Select(g =>
                {
                    DataRow dr = dt1.NewRow();
                    dr["F_LOSSCD"] = g.Key;
                    dr["F_LOSSNM"] = g.First()["F_LOSSNM"];
                    dr["F_LOSSHOUR"] = g.Sum(r => (decimal)r["F_LOSSHOUR"]).ToString("0.##");
                    return dr;
                }).CopyToDataTable();
            dt1 = this.AutoNumberTable(dt1);
            #endregion

            #region html 생성
            // HEADER
            sb.AppendLine("<table id='_cTab2' class='_cTab' border='0'>");
            sb.AppendLine("	<thead>");
            sb.AppendLine("	<tr class='_cTrH'>");
            sb.AppendLine("		<th class='_cTdH wNo' >No</th>");
            sb.AppendLine("		<th class='_cTdH wF_LOSSNM' >유실유형</th>");
            sb.AppendLine("		<th class='_cTdH wF_LOSSHOUR' >유실시간</th>");
            sb.AppendLine("	</tr>");
            sb.AppendLine("	</thead>");
            sb.AppendLine("	<tbody>");

            // DETAIL
            int rowIdx = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                int No = (int)dr["No"];
                string F_LOSSCD = dr["F_LOSSCD"].ToString();
                string F_LOSSNM = dr["F_LOSSNM"].ToString();
                string F_LOSSHOUR = dr["F_LOSSHOUR"].ToString();

                string jsonData = this.Dr2EncodeJson(dr);
                string jsonDataDetail = this.DataTable2EncodeJson(dt2.AsEnumerable().Where(x => x["F_LOSSCD"].ToString() == F_LOSSCD).CopyToDataTable());

                // DATA ROW <TR>
                sb.AppendFormat("	<tr class='_cTrD' id='_cTrD_{0}' onclick='trLOSSCD_OnClick({0})'>", rowIdx).AppendLine();
                sb.AppendFormat("		<td class='_cTdD wNo'>{0}", No).AppendLine();
                sb.AppendFormat("			<input type='hidden' id='_JSONDATA2_{0}' value='{1}' /> ", rowIdx, jsonData).AppendLine();
                sb.AppendFormat("			<input type='hidden' id='_JSONDATADETAIL_{0}' value='{1}' /> ", rowIdx, jsonDataDetail).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();

                // 유실유형
                sb.AppendFormat("		<td class='_cTdD wF_LOSSNM'>").AppendLine();
                sb.AppendFormat("			{0}", F_LOSSNM).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();
                // 유실시간
                sb.AppendFormat("		<td class='_cTdD wF_LOSSHOUR'>").AppendLine();
                sb.AppendFormat("			{0}", F_LOSSHOUR).AppendLine();
                sb.AppendFormat("		</td>").AppendLine();

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
                oParamDic["F_PLANYMD"] = dic.GetString("F_PLANYMD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1301_LST1(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
            }
            return dt;
        }
        #endregion

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
                oParamDic["F_WORKNO"] = dic.GetString("F_WORKNO");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1301_LST2(oParamDic, out errMsg);
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
        protected bool InsertData(Dictionary<string, string> dic, List<Dictionary<string, string>> dic2, List<Dictionary<string, string>> dic3, out string errMsg)
        {
            bool result = false;
            string F_WORKNO = dic.GetString("F_WORKNO");
            using (CATMBiz biz = new CATMBiz())
            {
                // 생산실적 파라미터
                var p1 = new Dictionary<string, string>();
                p1["F_WORKNO"] = F_WORKNO;
                p1["F_PRODCOUNT"] = dic.GetString("F_PRODCOUNT", "0");
                p1["F_ERRCOUNT"] = dic.GetString("F_ERRCOUNT", "0");
                p1["F_COMPCD"] = gsCOMPCD;
                p1["F_FACTCD"] = gsFACTCD;
                p1["F_LOGINUSER"] = gsUSERID;
                p1["F_LANGTYPE"] = gsLANGTP;

                // 불량유형별 수량 파라미터 목록
                var p2s = new List<Dictionary<string, string>>();
                foreach (var tmp in dic2)
                {
                    var tmp1 = new Dictionary<string, string>();
                    tmp1["F_WORKNO"] = F_WORKNO;
                    tmp1["F_ERRORCD"] = tmp.GetString("F_ERRORCD");
                    tmp1["F_ERRORCOUNT"] = tmp.GetString("F_ERRORCOUNT");
                    tmp1["F_COMPCD"] = gsCOMPCD;
                    tmp1["F_FACTCD"] = gsFACTCD;
                    tmp1["F_LOGINUSER"] = gsUSERID;
                    tmp1["F_LANGTYPE"] = gsLANGTP;
                    p2s.Add(tmp1);
                }

                // 유실유형별 시간 파라미터 목록
                var p3s = new List<Dictionary<string, string>>();
                foreach (var tmp in dic3)
                {
                    var tmp1 = new Dictionary<string, string>();
                    tmp1["F_WORKNO"] = F_WORKNO;
                    tmp1["F_LOSSCD"] = tmp.GetString("F_LOSSCD");
                    tmp1["F_LOSSORD"] = tmp.GetString("F_LOSSORD");
                    tmp1["F_FROMDT"] = tmp.GetString("F_FROMDT");
                    tmp1["F_TODT"] = tmp.GetString("F_TODT");
                    tmp1["F_COMPCD"] = gsCOMPCD;
                    tmp1["F_FACTCD"] = gsFACTCD;
                    tmp1["F_LOGINUSER"] = gsUSERID;
                    tmp1["F_LANGTYPE"] = gsLANGTP;
                    p3s.Add(tmp1);
                }

                result = biz.USP_CATM1301_INSERT(p1, p2s, p3s, out errMsg);
            }
            return result;
        }
        #endregion
        #endregion
    }
}