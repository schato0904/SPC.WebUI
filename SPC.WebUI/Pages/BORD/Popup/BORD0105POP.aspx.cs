using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BORD.Biz;
using System.Text;


namespace SPC.WebUI.Pages.BORD.Popup
{
    public partial class BORD0105POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;


        string[] keyFields = new string[7];
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        protected string oSetParam = String.Empty;
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

                BOARD_DETAIL_LST();
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
            //F_NUMBER;F_GBNNM;F_TITLE;F_COMPNMKR;F_INSUSER;F_CONTENTS;F_FILE
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
        {
            string errMsg = String.Empty;

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

        #region 사용자 정의 함수

        #region 상세보기 조회
        void BOARD_DETAIL_LST(){

            string errMsg1 = String.Empty;

                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_NUMBER", keyFields[0]);
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    ds = biz.BOARD_REPLY_LST(oParamDic, out errMsg1);
                }

            this.txtREUSER.Text = ds.Tables[0].Rows[0]["F_COMPNMKR"].ToString() + "  " + ds.Tables[0].Rows[0]["F_INDUSER"].ToString(); //유저
            this.txtRECOMMENT.Text = ds.Tables[0].Rows[0]["F_CONTENTS"].ToString(); //내용
            this.txtIMAGESEQ.Text = ds.Tables[0].Rows[0]["F_FILE"].ToString();  //파일첨부
        }
        #endregion

        #region 게시판_삭제권한체크
        void CHK_BOARD_REPLY_TBL_DUPLICATE(Dictionary<string, string> oDic, out bool bExists)
        {
            using (BORDBiz biz = new BORDBiz())
            {
                bExists = biz.PROC_BOARD_REPLY_DUPLICATE(oParamDic);
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region pnlContent_Callback
        /// <summary>
        /// pnlContent_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">pnlContent_Callback</param>
        #region pnlContent_Callback
        /// <summary>
        /// pnlContent_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">pnlContent_Callback</param>
        protected void pnlContent_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string errMsg1 = String.Empty;
            var Param = e.Parameter.Replace(";|", "").Split('|');

            bool bExists = false;
            oParamDic.Clear();
            oParamDic.Add("F_NUMBER", keyFields[0]);
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_USER", gsUSERID);
            CHK_BOARD_REPLY_TBL_DUPLICATE(oParamDic, out bExists);

            if (bExists)
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_NUMBER", keyFields[0]);
                    oParamDic.Add("F_COMPCD", gsCOMPCD); //작성자가 속한회사
                    oParamDic.Add("F_CONTENTS", Param[0]);
                    oParamDic.Add("F_FILE", Param[1]);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_GBN", "0");
                    ds = biz.BOARD_REPLY(oParamDic, out errMsg1);
                }
            }
            else
            {
                this.pnlContent.JSProperties["cpResultCode"] = "delete2";
            }

        }


        #endregion



    #endregion
        #endregion


    }
}