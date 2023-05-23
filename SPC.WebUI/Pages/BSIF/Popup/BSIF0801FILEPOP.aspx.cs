using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using System.IO;
using SPC.BSIF.Biz;
using System.Data.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.BSIF.Popup
{
    public partial class BSIF0801FILEPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        string[] procResult = { "2", "Unknown Error" };
        private readonly UploadingUtils uploadingUtils = new UploadingUtils();
        string[] keyFields = new string[3];
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
                // devGrid.JSProperties["cpResultCode"] = "";
                // devGrid.JSProperties["cpResultMsg"] = "";
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
            keyFields = Request.QueryString.Get("keyFields").Split('|');
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

        #endregion

        #region 사용자이벤트


        #region uploadFILEIMAGE_FileUploadComplete
        /// <summary>
        /// uploadFILEIMAGE_FileUploadComplete
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">FileUploadCompleteEventArgs</param>
        protected void uploadFILEIMAGE_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
                keyFields = Request.QueryString.Get("keyFields").Split('|');
                string fileLabel = String.Empty;
                bool bSaveFile = false;

                try
                {
                    bSaveFile = true;

                    string ErrMsg = String.Empty;
                    bool bExecute = false;

                    BinaryReader b = new BinaryReader(e.UploadedFile.FileContent);
                    byte[] imageContents = b.ReadBytes(Convert.ToInt32(e.UploadedFile.ContentLength));

                    //FileStream ss = new FileStream(saveFilePath, FileMode.CreateNew, FileAccess.Write);
                    //BinaryWriter br = new BinaryWriter(ss);

                    using (BSIFBiz biz = new BSIFBiz())
                    {
                        DbParameter[] oParamDB = new DbParameter[3];
                        oParamDB[0] = new System.Data.SqlClient.SqlParameter("@F_PLANT", keyFields[0]);
                        oParamDB[1] = new System.Data.SqlClient.SqlParameter("@F_NO", keyFields[1]);
                        oParamDB[2] = new System.Data.SqlClient.SqlParameter("@F_IMAGE", e.UploadedFile.FileBytes);

                        string[] oSPs = new string[] { "USP_QCDLEVEL_IMAGE_UPD" };
                        object[] _oParamDic = new object[1];
                        _oParamDic[0] = (object)oParamDB;
                        bExecute = biz.QCDLEVEL_IMAGE_UPD(oSPs, _oParamDic, out ErrMsg);
                    }

                    if (!String.IsNullOrEmpty(ErrMsg))
                    {
                        throw new Exception(ErrMsg);
                    }
                    else if (!bExecute)
                    {
                        throw new Exception("도면을 DB에 저장중 알수 없는 장애가 발생하였습니다.");
                    }
                }
                catch (Exception exp)
                {
                    fileLabel = String.Format("ERROR|{0}", exp.Message);
                }

                e.CallbackData = fileLabel;
            }
        }
        #endregion

        protected void callbackFileDelete_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string fileName = e.Parameter;
            string resultMsg = String.Empty;
            bool bDelDB = false;

            // 저장된 디렉토리
            string default_path = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), gsCOMPCD, "D");
            string saveFilePath = String.Concat(default_path, fileName);

            try
            {
                string[] oSPs = new string[] { "USP_QCDLEVEL_IMAGE_UPD" };
                string ErrMsg = String.Empty;
                bool bExecute = false;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_PLANT", keyFields[0]);
                    oParamDic.Add("F_NO", keyFields[1]);
                    oParamDic.Add("F_IMAGE", null);
                    //bExecute = biz.QCDLEVEL_IMAGE_UPD(oSPs, oParamDic, out ErrMsg);
                }

                if (!String.IsNullOrEmpty(ErrMsg))
                {
                    throw new Exception(ErrMsg);
                }
                else if (!bExecute)
                {
                    throw new Exception("도면을 DB에 저장중 알수 없는 장애가 발생하였습니다.");
                }

                bDelDB = true;

                File.Delete(saveFilePath);

                resultMsg = "ok";
            }
            catch (Exception exp)
            {
                if (true == bDelDB)
                    resultMsg = String.Format("ERROR|{0}", exp.Message);
                else
                    resultMsg = "ok";
            }

            e.Result = resultMsg;
        }

        #endregion
    }
}