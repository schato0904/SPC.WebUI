using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SPC.WebUI.Common;
using SPC.MEAS.Biz;
using DevExpress.Web;
using System.Data.OleDb;
using System.IO;

namespace SPC.WebUI.Pages.MEAS.Popup
{
    public partial class MEAS4002POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언        
        DataSet ds = null;
        DataTable ndt = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

        DataTable exdt
        {
            get
            {
                return (DataTable)Session["CLMM1001"];
            }
            set
            {
                Session["CLMM1001"] = value;
            }
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
            GetRequest();

            if (!IsCallback)
            {
                exdt = null;
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
            // Request
            GetRequest();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                ucPager.TotalItems = 0;
                ucPager.PagerDataBind();

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
        {
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 의뢰번호 검색 목록

        void MEAS4002_POP_LST()
        {
            string errMsg = String.Empty;

            if (exdt != null)
            {
                devGrid.DataSource = exdt;
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

        #region 엑셀저장

        string InsertEsdt()
        {
            int idx = 0;
            int count = exdt.Rows.Count;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            string errorID = null;

            string reInsert = null;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_FIXREQNO", exdt.Rows[i]["의뢰번호"].ToString());
                    oParamDic.Add("F_MS01MID", exdt.Rows[i]["계측기ID"].ToString());
                    oParamDic.Add("F_FIXNO", exdt.Rows[i]["교정번호"].ToString());
                    oParamDic.Add("F_FIXDT", exdt.Rows[i]["교정일자(XXXX-XX-XX)"].ToString());
                    oParamDic.Add("F_ENDYNN", exdt.Rows[i]["완료(완료,미완료)"].ToString());
                    oParamDic.Add("F_JUDGECDN", exdt.Rows[i]["판정(합격,불합격)"].ToString());
                    oParamDic.Add("F_ATTFILENO", "");
                    oParamDic.Add("F_TEMP", exdt.Rows[i]["온도"].ToString());
                    oParamDic.Add("F_HYGRO", exdt.Rows[i]["습도"].ToString());
                    oParamDic.Add("F_REGUSER", exdt.Rows[i]["작성자"].ToString());
                    oParamDic.Add("F_CNFMUSER", exdt.Rows[i]["승인자"].ToString());
                    oParamDic.Add("F_LASTFIXDT", exdt.Rows[i]["최종교정일"].ToString());

                    oSPs[idx] = "USP_MEAS4001_EXINS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            if (idx > 0)
            {
                using (MEASBiz biz = new MEASBiz())
                {
                    bExecute = biz.PROC_BATCH_NO(oSPs, oParameters, out resultMsg);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                }
                else
                {
                    procResult = new string[] { "1", "저장이 완료되었습니다." };
                }
            }
            else
            {
                procResult = new string[] { "1", errorID + " 아이디가 중복되었습니다." };
            }

            #endregion

            return procResult[1];
        }

        #endregion

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
            MEAS4002_POP_LST();
            //devGrid.DataBind();
        }
        #endregion

        protected void srcF_FILE_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            //// 쿼리에 시트명 사용 Sheet1$
            //// 시트 뒤에 읽어올 범위 지정 Sheet1$C9:F 이면 C9부터 F열까지 읽어 들임
            //string qry = "select * from [Sheet1$A4:AD]";
            //dt = new DataTable();
            //OleDbDataAdapter da = new OleDbDataAdapter(qry, conn);
            //da.Fill(dt);

            string savepath = System.Configuration.ConfigurationManager.AppSettings["defaultPath"];
            DirectoryInfo directoryInfo = new DirectoryInfo(savepath);
            if (directoryInfo.Exists != true)
            {
                directoryInfo.Create();
            }

            string fn = Path.GetFileNameWithoutExtension(e.UploadedFile.FileName);
            string ex = Path.GetExtension(e.UploadedFile.FileName);
            string fp = System.IO.Path.Combine(savepath, e.UploadedFile.FileName);
            while (File.Exists(fp))
            {

                fn = fn + "_1";
                fp = savepath + fn + ex;
            }
            e.UploadedFile.SaveAs(fp);

            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=1;IMEX=1'";
            connString = string.Format(connString, fp);
            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            // 엑셀 파일 시트명 정보
            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            conn.Close();

            //DataTable dataTable = new DataTable();
            exdt = null;
            exdt = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}A0:AD]", dt.Rows[0]["TABLE_NAME"]), connString);
            adapter.Fill(exdt);

        }

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
            DataTable dt = new DataTable();

            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                var param = DeserializeJSON(e.Parameter);
                dicResult["action"] = param["action"];
                switch (param["action"])
                {
                    case "SAVE":
                        dicResult["msg"] = InsertEsdt();
                        break;
                }
                e.Result = SerializeJSON(dicResult);
            }
        }
        #endregion

        #endregion
    }
}