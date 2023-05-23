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
    public partial class CATM4101 : WebUIBasePage
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
            get { return Session["CATM4101_Grid"] as DataTable; }
            set { Session["CATM4101_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["CATM4101_Grid1"] as DataTable; }
            set { Session["CATM4101_Grid1"] = value; }
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

                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";

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
            devGrid1.DataBind();
        }
        #endregion

        #region devGrid1_HtmlDataCellPrepared
        protected void devGrid1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {

            if (e.GetValue("F_COUNTER") != null)
            {
                int strJudge1 = Convert.ToInt16(e.GetValue("F_LIMITCOUNT").ToString());
                int strJudge2 = Convert.ToInt16(e.GetValue("F_COUNTER").ToString());
                if (strJudge2 >= strJudge1)
                {
                    //e.Cell.ForeColor = Color.Red;
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(235, 0, 0);
                }
                //else if (strJudge2 < strJudge1 && strJudge2 >= strJudge1 * 0.9)
                //{
                    //e.Cell.ForeColor = Color.FromArgb(220,220,0);
                //    e.Cell.BackColor = System.Drawing.Color.FromArgb(235, 235, 0);
                //}
                else
                {
                    return;
                }
            }


            if (e.VisibleIndex >= 0 && e.DataColumn.FieldName == "F_COUNTER")
            {
                int F_LIMITCOUNT = int.TryParse(devGrid1.GetRowValues(e.VisibleIndex, "F_LIMITCOUNT").ToString(), out F_LIMITCOUNT) ? F_LIMITCOUNT : 0;
                int F_COUNTER = int.TryParse((e.CellValue ?? "").ToString(), out F_COUNTER) ? F_COUNTER : 0;
                if (F_COUNTER > F_LIMITCOUNT)
                {
                    //e.Cell.ForeColor = Color.Red;
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
            string errMsg = String.Empty;
            this.Download_Excelfile();
        }
        #endregion

        #region Download_Excelfile
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

        #region devGrid1_BatchUpdate
        protected void devGrid1_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int erroridx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            bool bExists = false;
            string errorID = null;

            string reInsert = null;

            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_MOLDNO", (Value.NewValues["F_MOLDNO"] ?? "").ToString());
                    oParamDic.Add("F_MOLDNTH", (Value.NewValues["F_MOLDNTH"] ?? "").ToString());
                    oParamDic.Add("F_COUNTER", (Value.NewValues["F_COUNTER"] ?? "").ToString());


                    oSPs[idx] = "USP_CATM4101_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }


            bool bExecute = false;
            string resultMsg = String.Empty;
            if (idx > 0)
            {
                using (CATMBiz biz = new CATMBiz())
                {
                    bExecute = biz.PROC_CATM0401_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                    
                    if (erroridx > 0)
                    {
                        procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다. 나머지는 저장이 완료되었습니다." };
                    }


                }

            }


            devGrid1.JSProperties["cpResultCode"] = procResult[0];
            devGrid1.JSProperties["cpResultMsg"] = procResult[1];
            devGrid1.JSProperties["cpResultreInert"] = reInsert;
            devGrid1.JSProperties["cpResultcount"] = erroridx;



            e.Handled = true;
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 컨트롤 데이터 조회
        // 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            //result["F_FROMYMD"] = (this.schF_FROMYMD.Text ?? "");
            //result["F_TOYMD"] = (this.schF_TOYMD.Text ?? "");
            //result["F_CUSTCD"] = (this.schF_CUSTCD.Value ?? "").ToString();
            //result["F_ITEMCD"] = (this.schF_ITEMCD.Text ?? "");
            result["F_MOLDNO"] = (this.schF_MOLDNO.Text ?? "");

            return result;
        }
     
        #endregion

        #endregion

        #region DB 처리 함수

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

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
              
                oParamDic["F_MOLDNO"] = dic.GetString("F_MOLDNO");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM4101_LST1(oParamDic, out errMsg);
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