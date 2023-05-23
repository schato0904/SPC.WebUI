using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using DevExpress.Web;

namespace SPC.WebUI.API.Common.Popup
{
    public partial class Upload3 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public bool bMultiUpload = false;
        public int FileCnt = 0;
        string sDATA_GBN = String.Empty;
        string sDATA_REVNO = String.Empty;
        string sATTFILENO = String.Empty;
        string sAttachUniqueID = String.Empty;
        public string sCOMPCD = String.Empty;
        object[] oParameters = null;
        string[] sErrorMsg = null;
        int nFileIdx = 0;
        int nErrorCnt = 0;
        private readonly UploadingUtils uploadingUtils = new UploadingUtils();
        Dictionary<string, string> oFileList = new Dictionary<string, string>();
        public string NOTICE = string.Empty;
        #endregion

        #region 프로퍼티
        public string _USERID
        {
            get { return this.gsUSERID; }
        }

        public string ParentCallbackNm { get; private set; }

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

            // 파일목록을 구한다
            GetATTFILE_LST(sATTFILENO);
            hidATTFILECNT.Text = FileCnt.ToString();

            if (!IsCallback) devGrid.DataBind();

            // 멀티업로드 가능여부
            devUploader.AdvancedModeSettings.EnableMultiSelect = bMultiUpload;

            // 업로드 가능 파일
            switch (sDATA_GBN)
            {
                //case "C":
                //    devUploader.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                //    break;
                //case "D":
                //    devUploader.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                //    break;
                case "S":
                    devUploader.ValidationSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png" };
                    break;
                default:
                    //devUploader.ValidationSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".tif", ".tiff", ".doc", ".docx", ".pdf", ".ppt", "pptx", ".xls", ".xlsx", ".zip", ".txt" };
                    //devUploader.ValidationSettings.AllowedFileExtensions = new string[] { "*.*" };
                    break;
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

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid.JSProperties["cpResultCnt"] = "-1";
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
            bMultiUpload = (Request.QueryString.Get("MULTI") ?? "").Equals("T");
            sDATA_GBN = Request.QueryString.Get("GBN") ?? "";
            sATTFILENO = Request.QueryString.Get("FILENO") ?? "";
            hidATTFILENO.Text = sATTFILENO;
            hidDATA_GBN.Text = sDATA_GBN;
            sCOMPCD = gsCOMPCD;
            if (Request.QueryString.Get("COMPCD") != null)
                sCOMPCD = Request.QueryString.Get("COMPCD");
            hidATTFILENO_CtrlId.Text = Request.QueryString.Get("FILENOID") ?? "";
            hidATTFILECNT_CtrlId.Text = Request.QueryString.Get("FILECNTID") ?? "";
            hidParentGridIndex.Text = Request.QueryString.Get("IDX") ?? "";
            ParentCallbackNm = Request.QueryString.Get("parentCallback") ?? "";
            NOTICE = Request["NOTICE"] ?? "";

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
            divDesc.Style["display"] = string.IsNullOrWhiteSpace(this.NOTICE) ? "none" : "inline-block";
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

        #region 파일갯수를 구한다
        Int32 GetATTFILE_CNT()
        {
            Int32 nCnt = -1;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("ATTFILENO", sATTFILENO);
                oParamDic.Add("DATA_GBN", sDATA_GBN);

                nCnt = biz.GetATTFILE_CNT(oParamDic);
            }

            return nCnt;
        }
        #endregion

        #region 파일목록을 구한다
        void GetATTFILE_LST(string ATTFILENO)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("ATTFILENO", ATTFILENO);
                oParamDic.Add("DATA_GBN", sDATA_GBN);
                //oParamDic.Add("USERID", _USERID);

                ds = biz.GetATTFILE_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (ds.Tables[0].Rows.Count > 0)
                FileCnt = ds.Tables[0].Rows.Count;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (IsCallback)
                //    devGrid.DataBind();
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
            sDATA_GBN = Request.QueryString.Get("GBN") ?? "Z";
            sDATA_REVNO = Request.QueryString.Get("REVNO") ?? "0";
            sATTFILENO = Request.QueryString.Get("FILENO");
            if (String.IsNullOrEmpty(sATTFILENO))
                sATTFILENO = UF.Encrypts.GetUniqueKey();

            // 고유키생성
            if (String.IsNullOrEmpty(sAttachUniqueID))
                sAttachUniqueID = sATTFILENO;
            
            FileInfo fileInfo = new FileInfo(uploadedFile.FileName);

            // 저장할 파일 고유명
            string uniqueFileName = String.Concat(Guid.NewGuid().ToString().GetHashCode().ToString("x"), fileInfo.Extension);
            // 저장할 디렉토리
            string default_path = String.Format(System.Configuration.ConfigurationManager.AppSettings.Get("defaultCompSubPath"), sCOMPCD, sDATA_GBN);

            uploadingUtils.BaseDirectory = default_path;
            uploadingUtils.MaxUploadSize = 104857600; // 100MB
            uploadingUtils.overWrite = false;
            uploadingUtils.saveFileName = Path.GetFileNameWithoutExtension(uniqueFileName);

            // 저장된 파일 전체경로(파일명포함)
            string saveFilePath = String.Concat(default_path, uploadingUtils.UploadFileWithOutSub(uploadedFile));
            string fileLabel = Path.GetFileName(saveFilePath);
            string fileLength = uploadingUtils.GetFileSize(uploadedFile.ContentLength);

            // 저장을 위한 파라미터 생성
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("ATTFILENO", sAttachUniqueID);
            oParamDic.Add("DATA_NAME", fileLabel);
            oParamDic.Add("DATA_ORIGIN_NAME", fileInfo.Name);
            oParamDic.Add("DATA_EXTN", fileInfo.Extension);
            oParamDic.Add("ATTFILESIZE", uploadedFile.ContentLength.ToString());
            oParamDic.Add("INPUTID", gsUSERID);
            oParamDic.Add("DATA_GBN", sDATA_GBN);
            oParamDic.Add("DATA_REVNO", sDATA_REVNO);
            oParameters[nFileIdx] = (object)oParamDic;
            nFileIdx++;
            
            // 오류시 파일 삭제를 위한 Dictionary
            oFileList.Add(fileLabel, saveFilePath);

            return string.Format("{0} ({1})|{2}", fileLabel, fileLength, fileInfo.Name);
        } 
        #endregion

        #endregion

        #region 사용자이벤트

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
                    //Array.ForEach<Dictionary<string, string>>((oParameters as Dictionary<string, string>[]), x => x.Add("USERID", this.gsUSERID));
                    //oParameters.Cast<Dictionary<string, string>>().ToList().ForEach(x => x.Add("USERID", this.gsUSERID));
                    //oParameters.Cast<Dictionary<string, string>>().ToList().ForEach(x => x.Add("F_COMPCD", sCOMPCD));
                    bExecute = biz.PROC_ATTFILE_INS(oParameters);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "등록 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                    e.CallbackData = "Error|데이타베이스 작업 중 장애가 발생하였습니다";
                }
                else
                {
                    e.CallbackData = String.Format("Success|{0}|{1}", sAttachUniqueID, devGrid.VisibleRowCount + cnt);
                }
            }
        }

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (!e.Column.FieldName.Equals("ATTFILESIZE")) return;
            if (e.VisibleRowIndex < 0) return;

            e.DisplayText = uploadingUtils.GetFileSize(Convert.ToDouble(e.Value));
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
            Int32 nFileCount = -1;
            string ATTFILENO = String.Empty;
            string[] oParam = e.Parameters.Split(';');
            string sAction = oParam[0];

            if (sAction.Equals("delete"))
            {
                // 저장된 디렉토리
                string default_path = String.Format(System.Configuration.ConfigurationManager.AppSettings.Get("defaultCompSubPath"), sCOMPCD, sDATA_GBN);
                string saveFilePath = String.Empty;

                oParam = oParam[1].Split(',');
                oParameters = new object[oParam.Length];

                foreach (string oParams in oParam)
                {
                    string[] oInputParams = oParams.Split('|');

                    ATTFILENO = oInputParams[0];

                    // 삭제를 위한 파라미터 생성
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("ATTFILENO", oInputParams[0]);
                    oParamDic.Add("ATTFILESEQ", oInputParams[1]);
                    oParamDic.Add("DATA_GBN", oInputParams[2]);
                    oParameters[nFileIdx] = (object)oParamDic;
                    nFileIdx++;

                    saveFilePath = String.Concat(default_path, oInputParams[3]);

                    // 실제 파일 삭제를 위한 Dictionary
                    oFileList.Add(oInputParams[3], saveFilePath);
                }

                bool bExecute = false;

                using (CommonBiz biz = new CommonBiz())
                {
                    bExecute = biz.PROC_ATTFILE_DEL(oParameters);
                }

                if (!bExecute)
                {
                    procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
                }
                else
                {
                    procResult = new string[] { "1", "삭제가 완료되었습니다." };

                    // 전체파일갯수에 따라 처리를 위해(파일이 없는 경우 부모창의 Unique Key 제거)
                    // 파일갯수를 구한다
                    nFileCount = GetATTFILE_CNT();

                    devGrid.JSProperties["cpResultCnt"] = nFileCount.ToString();

                    if (nFileCount == 0)
                    {
                        using (CommonBiz biz = new CommonBiz())
                        {
                            bExecute = biz.ATTFILE_ATTFILENO_UPD(oParameters);
                        }
                    }
                    
                    // 실제파일 삭제
                    foreach (KeyValuePair<string, string> pair in oFileList)
                    {
                        uploadingUtils.RemoveFileWithDelay(pair.Key, pair.Value, 5);
                    }
                }
                devGrid.JSProperties["cpATTFILENO"] = ATTFILENO;
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                ATTFILENO = oParam[1];
            }

            devGrid.JSProperties["cpResultCnt"] = nFileCount.ToString();

            // 파일목록을 구한다
            GetATTFILE_LST(ATTFILENO);
            devGrid.DataBind();
        }
        #endregion

        #endregion
    }
}