using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using System.IO;
using DevExpress.Web;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.BSIF.Popup
{
    public partial class BSIF0205IMGPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string oSetParam = String.Empty;
        List<string> errFiles = new List<string>();
        Dictionary<string, string> oFileList = new Dictionary<string, string>();
        private readonly UploadingUtils uploadingUtils = new UploadingUtils();
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 기존 등록된 도면 및 이미지
                QCD014_LST();
            }

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

                callbackControl.JSProperties["cpResultCode"] = "";
                callbackControl.JSProperties["cpResultMsg"] = "";
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
            oSetParam = Request.QueryString.Get("KEYFIELDS") ?? "";
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

        #region 기존 등록된 도면 및 이미지
        private void QCD014_LST()
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("F_TITLE", typeof(string));
            dtNew.Columns.Add("F_PATH", typeof(string));
            dtNew.Columns.Add("F_FILE", typeof(string));
            dtNew.Columns.Add("F_FIELD", typeof(string));
            dtNew.Columns.Add("F_ITEMCD", typeof(string));

            string errMsg = "";

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", oSetParam.Split('|')[0]);
                ds = biz.QCD014_LST(oParamDic, out errMsg);
            }

            if (bExistsDataSet(ds))
            {
                DataRow dtRow = ds.Tables[0].Rows[0];
                if (!String.IsNullOrEmpty(dtRow["F_IMAG01"].ToString()))
                    dtNew.Rows.Add(sTitle("F_IMAG01"), dtRow["F_IMAGPT"], dtRow["F_IMAG01"], "F_IMAG01", dtRow["F_ITEMCD"]);
                if (!String.IsNullOrEmpty(dtRow["F_IMAG02"].ToString()))
                    dtNew.Rows.Add(sTitle("F_IMAG02"), dtRow["F_IMAGPT"], dtRow["F_IMAG02"], "F_IMAG02", dtRow["F_ITEMCD"]);
                if (!String.IsNullOrEmpty(dtRow["F_IMAG03"].ToString()))
                    dtNew.Rows.Add(sTitle("F_IMAG03"), dtRow["F_IMAGPT"], dtRow["F_IMAG03"], "F_IMAG03", dtRow["F_ITEMCD"]);
                if (!String.IsNullOrEmpty(dtRow["F_IMAG04"].ToString()))
                    dtNew.Rows.Add(sTitle("F_IMAG04"), dtRow["F_IMAGPT"], dtRow["F_IMAG04"], "F_IMAG04", dtRow["F_ITEMCD"]);
                if (!String.IsNullOrEmpty(dtRow["F_IMAG05"].ToString()))
                    dtNew.Rows.Add(sTitle("F_IMAG05"), dtRow["F_IMAGPT"], dtRow["F_IMAG05"], "F_IMAG05", dtRow["F_ITEMCD"]);
            }

            devGrid.DataSource = dtNew;

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

        #region 도면삭제
        private void QCD014_DEL(string oParams)
        {
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_ITEMCD", Request.QueryString.Get("KEYFIELDS").Split('|')[0]);
            oParamDic.Add("F_IMAGPT", String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), gsCOMPCD, "DRAWING"));
            oParamDic.Add("F_IMAG01", "");
            oParamDic.Add("F_IMAG02", "");
            oParamDic.Add("F_IMAG03", "");
            oParamDic.Add("F_IMAG04", "");
            oParamDic.Add("F_IMAG05", "");
            oParamDic.Add("F_USER", gsUSERID);

            foreach (string oParam in oParams.Split(','))
            {
                string[] _oParam = oParam.Split('|');
                oParamDic[_oParam[1]] = "delete";
            }

            string errMsg = "";
            bool bExecute = false;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.QCD014_DEL(oParamDic, out errMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r에러내용 : {0}", errMsg) };
                devGrid.JSProperties["cpResultCode"] = procResult[0];
                devGrid.JSProperties["cpResultMsg"] = procResult[1];
            }
            else
            {
                // 기존 등록된 도면 및 이미지
                QCD014_LST();
            }
        }
        #endregion

        #region 컬럼명에 따른 타이틀
        private string sTitle(string col)
        {
            switch (col)
            {
                default:
                    return "";
                case "F_IMAG01":
                    return "도면";
                case "F_IMAG02":
                    return "사진1";
                case "F_IMAG03":
                    return "사진2";
                case "F_IMAG04":
                    return "사진3";
                case "F_IMAG05":
                    return "사진4";
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameters.Split(';');

            if (oParams[0].Equals("select"))
            {
                // 기존 등록된 도면 및 이미지
                QCD014_LST();
            }
            else if (oParams[0].Equals("delete"))
            {
                // 도면삭제
                QCD014_DEL(oParams[1]);
            }
        }
        #endregion

        #region uploadFILEIMAGE_FileUploadComplete
        /// <summary>
        /// uploadFILEIMAGE_FileUploadComplete
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">FileUploadCompleteEventArgs</param>
        protected void uploadFILEIMAGE_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            ASPxUploadControl uploadControl = sender as ASPxUploadControl;
            UploadedFile uploadedFile = e.UploadedFile;
            string callbackData = "";

            if (e.IsValid)
            {
                string default_path = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), gsCOMPCD, "DRAWING");

                string sField = "", sFileName = "";

                FileInfo fileInfo = new FileInfo(uploadedFile.FileName);

                switch (uploadControl.ID)
                {
                    case "uploadFILEIMAGE01":
                        sField = "F_IMAG01";
                        sFileName = String.Concat(String.Format("{0}_IMAG01", Request.QueryString.Get("KEYFIELDS").Split('|')[0]), fileInfo.Extension);
                        break;
                    case "uploadFILEIMAGE02":
                        sField = "F_IMAG02";
                        sFileName = String.Concat(String.Format("{0}_IMAG02", Request.QueryString.Get("KEYFIELDS").Split('|')[0]), fileInfo.Extension);
                        break;
                    case "uploadFILEIMAGE03":
                        sField = "F_IMAG03";
                        sFileName = String.Concat(String.Format("{0}_IMAG03", Request.QueryString.Get("KEYFIELDS").Split('|')[0]), fileInfo.Extension);
                        break;
                    case "uploadFILEIMAGE04":
                        sField = "F_IMAG04";
                        sFileName = String.Concat(String.Format("{0}_IMAG04", Request.QueryString.Get("KEYFIELDS").Split('|')[0]), fileInfo.Extension);
                        break;
                    case "uploadFILEIMAGE05":
                        sField = "F_IMAG05";
                        sFileName = String.Concat(String.Format("{0}_IMAG05", Request.QueryString.Get("KEYFIELDS").Split('|')[0]), fileInfo.Extension);
                        break;
                }

                if (!String.IsNullOrEmpty(sFileName))
                {
                    uploadingUtils.BaseDirectory = default_path;
                    uploadingUtils.MaxUploadSize = 104857600; // 100MB
                    uploadingUtils.overWrite = true;
                    uploadingUtils.saveFileName = Path.GetFileNameWithoutExtension(sFileName);

                    try
                    {
                        // 저장된 파일 전체경로(파일명포함)
                        string saveFilePath = String.Concat(default_path, uploadingUtils.UploadFileWithOutSub(uploadedFile));
                        callbackData = String.Format("{0}|{1}", sField, saveFilePath);
                    }
                    catch (Exception ex)
                    {
                        callbackData = String.Format("ERR|{0}", ex.Message);
                    }
                }
            }

            e.CallbackData = callbackData;
        }
        #endregion

        #region callbackControl_Callback
        /// <summary>
        /// callbackControl_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void callbackControl_Callback(object source, CallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split(';');
            int nExistsFile = 0;

            if (oParams.Length > 0)
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", Request.QueryString.Get("KEYFIELDS").Split('|')[0]);
                oParamDic.Add("F_IMAGPT", String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), gsCOMPCD, "DRAWING"));
                oParamDic.Add("F_IMAG01", "");
                oParamDic.Add("F_IMAG02", "");
                oParamDic.Add("F_IMAG03", "");
                oParamDic.Add("F_IMAG04", "");
                oParamDic.Add("F_IMAG05", "");
                oParamDic.Add("F_USER", gsUSERID);

                foreach (string oParam in oParams)
                {
                    string[] _oParam = oParam.Split('|');
                    if (!_oParam.Equals("ERR"))
                    {
                        oParamDic[_oParam[0]] = Path.GetFileName(_oParam[1]);
                        nExistsFile++;
                    }
                }

                string errMsg = "";

                if (nExistsFile > 0)
                {
                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        biz.QCD014_INS_UPD(oParamDic, out errMsg);
                    }
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    callbackControl.JSProperties["cpResultCode"] = "ERR";
                    callbackControl.JSProperties["cpResultMsg"] = errMsg;
                }
            }
        }
        #endregion

        #endregion
    }
}