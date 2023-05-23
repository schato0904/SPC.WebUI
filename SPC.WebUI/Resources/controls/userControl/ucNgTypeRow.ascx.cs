using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucNgTypeRow : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public string CallerId = string.Empty;
        #endregion

        #region 프로퍼티
        public string F_NGTYPECD
        {
            get { return txtF_NGTYPECD.Text; }
            set { txtF_NGTYPECD.Text = value; }
        }  // 부적합유형코드
        public string F_NGTYPENM
        {
            get { return txtF_NGTYPENM.Text; }
            set { txtF_NGTYPENM.Text = value; }
        }  // 부적합유형명
        public string F_CNT
        {
            get { return txtF_CNT.Text; }
            set { txtF_CNT.Text = value; }
        }  // 부적합수량
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
            //this.txtF_NGTYPECD.Text = this.F_NGTYPECD;
            //this.txtF_NGTYPENM.Text = this.F_NGTYPENM;
            //this.txtF_CNT.Text = this.F_CNT;
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

        protected void txtF_CNT_Init(object sender, EventArgs e)
        {
            var txt = sender as DevExpress.Web.ASPxSpinEdit;
            txt.ClientSideEvents.NumberChanged = string.Format("function(s,e){{ CTF.UserControl.NgTypeRow['{0}'].F_CNT = s.GetText();}}", this.UniqueID);
            txt.ClientSideEvents.Init = string.Format("function(s,e){{ {0}.ucNgTypeRowInit('{1}', '{2}', '{3}', '{4}');}}", this.CallerId, this.UniqueID, this.F_NGTYPECD, this.F_NGTYPENM, this.F_CNT);
            txt.ClientSideEvents.LostFocus = string.Format("function(s,e){{ {0}.ValueChange();}}", this.CallerId);
        }

        #endregion

        #region 사용자 정의 함수

        #endregion

        #region 사용자이벤트

        #endregion
    }
}