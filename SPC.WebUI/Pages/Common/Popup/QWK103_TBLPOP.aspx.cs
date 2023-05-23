using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;
using SPC.Common.Biz;
using SPC.IPCM.Biz;
using SPC.SYST.Biz;
namespace SPC.WebUI.Pages.IPCM
{
    public partial class QWK103_TBLPOP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;

        string PUBLICNO = String.Empty;
        string[] procResult = { "2", "Unknown Error" };
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
            string errMsg = String.Empty;

            using (IPCMBiz biz = new IPCMBiz())
            {

                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_PUBLICNO", PUBLICNO);

                ds = biz.QWK103POP(oParamDic, out errMsg);
            }

            this.txt_publicno.Text = ds.Tables[0].Rows[0]["F_PUBLICNO"].ToString();
            this.txt_action.Text = ds.Tables[0].Rows[0]["F_ACTION"].ToString();
            this.txt_cause.Text = ds.Tables[0].Rows[0]["F_CAUSE"].ToString();
            this.txt_dechek1.Text = ds.Tables[0].Rows[0]["F_DECHEK1"].ToString();
            this.txt_dechek2.Text = ds.Tables[0].Rows[0]["F_DECHEK2"].ToString();
            this.txtIMAGESEQ2.Text = ds.Tables[0].Rows[0]["F_FILENAME1"].ToString();
            this.txt_mademan.Text = ds.Tables[0].Rows[0]["F_MADEMAN"].ToString();
            this.txt_replydate.Text = ds.Tables[0].Rows[0]["F_REPLYDATE"].ToString();
            
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
        {
            PUBLICNO = Request.QueryString.Get("FILENO") ?? "";
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

    }
}

        #endregion
