using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using DevExpress.Web;
using SPC.LTRK.Biz;
using System.IO;
using CTF.Web.Framework.Helper;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.LTRK
{
    public partial class LTRK0201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        bool bMultiUpload = false;
        string sDATA_GBN = String.Empty;
        string sDATA_REVNO = String.Empty;
        string sATTFILENO = String.Empty;
        string sAttachUniqueID = String.Empty;
        string sCOMPCD = String.Empty;
        string strSaveFile = String.Empty;
        object[] oParameters = null;
        string[] sErrorMsg = null;
        int nFileIdx = 0;
        int nErrorCnt = 0;
        private readonly UploadingUtils uploadingUtils = new UploadingUtils();
        Dictionary<string, string> oFileList = new Dictionary<string, string>();
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

            // 조회
            QPM21_LST();

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
        { }
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

        #region 조회
        void QPM21_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_STATUS", ddlSTATUS.SelectedItem.Value.ToString());
                ds = biz.QPM21_LST(oParamDic, out errMsg);
            }

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
        #endregion

        #region 저장
        bool QPM21_INS(string Parameters)
        {
            bool bExecute = false;
            string errMsg = String.Empty;

            string[] aParams = Parameters.Split('|');

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERDATE", aParams[4]);
                oParamDic.Add("F_ATTFILENO", aParams[2]);
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QPM21_INS(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region btnOneDownload_Init
        protected void btnOneDownload_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "ATTFILENO", "ATTFILESEQ", "DATA_GBN", "DATA_ORIGIN_NAME") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.Text = rowValues[3].ToString();
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnSelDownload('{0}', '{1}', '{2}', '{3}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2],
                gsCOMPCD);
        }
        #endregion

        #region btnLink_Init
        /// <summary>
        /// btnLink_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnLink_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxButton btnLink = (DevExpress.Web.ASPxButton)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)btnLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid.GetRowValues(rowVisibleIndex, "F_ORDERGROUP", "F_ORDERDATE", "F_STATUS") as object[];
            string sTITLE = String.Empty;
            switch (rowValues[2].ToString())
            {
                default:
                    sTITLE = "등록현황";
                    break;
                case "8":
                    sTITLE = "등록진행";
                    break;
                case "2":
                    sTITLE = "등록전취소";
                    break;
            }
            btnLink.Text = sTITLE;
            btnLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnLink('{0}', '{1}', '{2}'); }}",
                rowValues[0],
                rowValues[1],
                rowValues[2]);
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_INSDT"))
            {
                e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (e.Column.FieldName.Equals("F_STATUS"))
            {
                string sStatus = String.Empty;
                switch (e.Value.ToString())
                {
                    case "0":
                        sStatus = @"<span style='color:red;'>등록취소</span>";
                        break;
                    case "1":
                        sStatus = @"<span style='color:blue;'>등록완료</span>";
                        break;
                    case "2":
                        sStatus = @"<span style='color:red;'>등록전취소</span>";
                        break;
                    case "8":
                        sStatus = @"<span style='color:orange;'>임시등록</span>";
                        break;
                    case "9":
                        sStatus = @"등록중";
                        break;
                }

                e.EncodeHtml = false;
                e.DisplayText = sStatus;
            }
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                // 파일 업로드 완료
                if (!QPM21_INS(e.Parameters))
                {
                    procResult = new string[] { "0", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "", "" };
                }

                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }

            // 조회
            QPM21_LST();
        }
        #endregion

        #region devUploader_FileUploadComplete
        protected void devUploader_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                e.CallbackData = SavePostedFiles(e.UploadedFile);
            }
            catch (Exception ex)
            {
                sErrorMsg[nErrorCnt] = ex.Message;
                nErrorCnt++;
                e.IsValid = false;
            }
        }
        #endregion

        #region devUploader_FilesUploadComplete
        protected void devUploader_FilesUploadComplete(object sender, DevExpress.Web.FilesUploadCompleteEventArgs e)
        {
            int cnt = oParameters.Length;

            // 오류가 난 경우 파일을 삭제한다.
            if (nErrorCnt > 0)
            {
                foreach (KeyValuePair<string, string> pair in oFileList)
                {
                    uploadingUtils.RemoveFileWithDelay(pair.Key, pair.Value, 5);
                }

                e.CallbackData = String.Format("Error|{0}", String.Join("^", sErrorMsg));
            }
            else
            {
                bool bExecute = false;

                using (CommonBiz biz = new CommonBiz())
                {
                    bExecute = biz.PROC_ATTFILE_INS(oParameters);
                }

                e.CallbackData = String.Format("Success|{0}|{1}", sAttachUniqueID, strSaveFile);
            }
        }
        #endregion

        #region 파일 저장 및 DB Parameter Collection Add
        string SavePostedFiles(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return String.Empty;

            if (oParameters == null)
                oParameters = new object[devUploader.UploadedFiles.Length];

            if (sErrorMsg == null)
                sErrorMsg = new string[devUploader.UploadedFiles.Length];

            sCOMPCD = Request.QueryString.Get("COMPCD") ?? gsCOMPCD;
            sDATA_GBN = Request.QueryString.Get("GBN") ?? "W";
            sDATA_REVNO = Request.QueryString.Get("REVNO") ?? "0";
            sATTFILENO = Request.QueryString.Get("FILENO");
            if (String.IsNullOrEmpty(sATTFILENO))
                sATTFILENO = UF.Encrypts.GetUniqueKey();

            // 고유키생성
            if (String.IsNullOrEmpty(sAttachUniqueID))
                sAttachUniqueID = sATTFILENO;

            FileInfo fileInfo = new FileInfo(uploadedFile.FileName);

            // 저장할 파일 고유명 
            string uniqueFileName = string.Format("{0}_{1}", DateTime.Today.ToString("yyyyMMdd"), String.Concat(Guid.NewGuid().ToString().GetHashCode().ToString("x"), fileInfo.Extension));
            // 저장할 디렉토리
            string default_path = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), sCOMPCD, sDATA_GBN);

            uploadingUtils.BaseDirectory = default_path;
            uploadingUtils.MaxUploadSize = 104857600; // 100MB
            uploadingUtils.overWrite = false;
            uploadingUtils.saveFileName = Path.GetFileNameWithoutExtension(uniqueFileName);

            // 저장된 파일 전체경로(파일명포함)
            strSaveFile = String.Concat(default_path, uploadingUtils.UploadFileWithOutSub(uploadedFile));
            string fileLabel = Path.GetFileName(strSaveFile);
            string fileLength = uploadingUtils.GetFileSize(uploadedFile.ContentLength);

            // 저장을 위한 파라미터 생성
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("ATTFILENO", sAttachUniqueID);
            oParamDic.Add("DATA_NAME", fileLabel);
            oParamDic.Add("DATA_ORIGIN_NAME", string.Format("{0}_{1}", DateTime.Today.ToString("yyyyMMdd"), fileInfo.Name));
            oParamDic.Add("DATA_EXTN", fileInfo.Extension);
            oParamDic.Add("ATTFILESIZE", uploadedFile.ContentLength.ToString());
            oParamDic.Add("INPUTID", gsUSERID);
            oParamDic.Add("DATA_GBN", sDATA_GBN);
            oParamDic.Add("DATA_REVNO", sDATA_REVNO);
            oParameters[nFileIdx] = (object)oParamDic;
            nFileIdx++;

            // 오류시 파일 삭제를 위한 Dictionary
            oFileList.Add(fileLabel, strSaveFile);

            return string.Format("{0} ({1})|{2}|{3}", fileLabel, fileLength, fileInfo.Name, strSaveFile);
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            //string errMsg = String.Empty;   // 오류 메시지
            //var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            //Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            //bool ISOK = false;  // 처리 결과
            //string msg = string.Empty;  // 처리 메시지
            //Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            //Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            //switch (paramDic["ACTION"])
            //{
            //    case "D":   // 삭제
            //        // 삭제를 위한 파라미터 생성
            //        oParamDic = new Dictionary<string, string>();
            //        oParamDic.Add("F_COMPCD", gsCOMPCD);
            //        oParamDic.Add("F_FACTCD", gsFACTCD);
            //        oParamDic.Add("F_WORKDATE", paramDic["F_WORKDATE"]);
            //        oParamDic.Add("ATTFILENO", paramDic["ATTFILENO"]);
            //        oParamDic.Add("F_WORKER", gsUSERID);
            //        if (!QCD34_EXCEL_DEL(oParamDic, out errMsg))
            //        {
            //            ISOK = false;
            //            result["ISOK"] = ISOK;
            //            result["MSG"] = errMsg;
            //        }
            //        else
            //        {
            //            ISOK = true;
            //            result["ISOK"] = ISOK;
            //            result["MSG"] = "삭제되었습니다.";
            //        }
            //        e.Result = jss.Serialize(result);
            //        break;
            //}
        }
        #endregion

        #endregion
    }
}