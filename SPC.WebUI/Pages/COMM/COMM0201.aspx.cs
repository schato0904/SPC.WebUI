using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.COMM
{
    public partial class COMM0201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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

                // Callback Init
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

        #endregion

        #region 사용자이벤트

        #region callbackControl_Callback
        /// <summary>
        /// callbackControl_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void callbackControl_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string[] loginResult = { "2", "Unknown Error" };
            string resultCode = String.Empty;

            #region 비밀번호 암호화(SHA1)
            //string CurrPW = !Convert.ToBoolean(gsENCRTPTPW) ? txtCurrPass.Text : UF.Encrypts.HashPasswordToString(txtCurrPass.Text, "SHA1");
            //string ChanPW = !Convert.ToBoolean(gsENCRTPTPW) ? txtChngPass.Text : UF.Encrypts.HashPasswordToString(txtChngPass.Text, "SHA1");
            #endregion

            #region 비밀번호 암호화(SHA256)
            string CurrPW = !Convert.ToBoolean(gsENCRTPTPW) ? txtCurrPass.Text : UF.Encrypts.ComputeHash(txtCurrPass.Text, new System.Security.Cryptography.SHA256CryptoServiceProvider());
            string ChanPW = !Convert.ToBoolean(gsENCRTPTPW) ? txtChngPass.Text : UF.Encrypts.ComputeHash(txtChngPass.Text, new System.Security.Cryptography.SHA256CryptoServiceProvider());
            #endregion

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_USERID", gsUSERID);
                oParamDic.Add("F_PASS01", CurrPW);
                oParamDic.Add("F_PASS02", ChanPW);

                resultCode = biz.COMM0201_CHANGE_PASSWORD(oParamDic);
            }

            if (resultCode.Equals("0"))
            {
                loginResult = new string[] { "0", "입력하신 현재 비밀번호가 올바르지 않습니다" };
            }
            else if (resultCode.Equals("1"))
            {
                loginResult = new string[] { "1", "비밀번호 변경이 완료되었습니다\r\n자동으로 로그아웃됩니다" };
            }

            callbackControl.JSProperties["cpResultCode"] = loginResult[0];
            callbackControl.JSProperties["cpResultMsg"] = loginResult[1];
        }
        #endregion

        #endregion
    }
}