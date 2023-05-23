using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucNaviPopup : WebUIBasePageUserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        protected string Title = String.Empty;
        protected string btnUsed = String.Empty;
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
                Title = Request.QueryString.Get("TITLE") ?? "";
                btnUsed = Request.QueryString.Get("CRUD") ?? "";
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #endregion

        #region 사용자 정의 함수

        #endregion

        #region 사용자이벤트

        #endregion
    }
}