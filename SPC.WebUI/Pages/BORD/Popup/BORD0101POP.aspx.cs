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
    public partial class BORD0101POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;

        string[] keyFields = new string[7];
        public string strGbn = "";
        string[] procResult = { "2", "Unknown Error" };
        Int32 chkCOMPcount = 0;
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

            if (!gsVENDOR)
            {
                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_MASTERCHK", "0");
                    ds = biz.QCM01_LST(oParamDic, out errMsg);
                }
                chkCOMPcount = ds.Tables[0].Rows.Count;
                this.chkCOMP.DataSource = ds;
                this.chkCOMP.TextField = "F_COMPNM";
                this.chkCOMP.ValueField = "F_COMPCD";
                this.chkCOMP.DataBind();
            }
            else
            {
                this.chkCOMP.ClientVisible = false;
            }
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
                oParamDic.Add("F_COMPNMKR", keyFields[1]);

                ds = biz.BOARD_DETAIL(oParamDic, out errMsg1);
            }
            this.lblNUMBER.Text = ds.Tables[0].Rows[0]["F_NUMBER"].ToString(); //게시글번호
            this.txtTITLE.Text = ds.Tables[0].Rows[0]["F_TITLE"].ToString(); //제목
            this.txtCONTENTS.Text = ds.Tables[0].Rows[0]["F_CONTENTS"].ToString(); //내용
            this.txtIMAGESEQ.Text = ds.Tables[0].Rows[0]["F_FILE"].ToString();  //파일첨부

            strGbn = ds.Tables[0].Rows[0]["F_GBN"].ToString();
            if (ds.Tables[0].Rows[0]["F_GBN"].ToString() == "0")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "name1", "OptionDisplay();", true);
            }
            else
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_NUMBER", keyFields[0]);
                    ds = biz.BOARD_COMMENTCOUNT(oParamDic, out errMsg1);
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "name4", "OptionDisplay4();", true);
                }

            }

            if(keyFields[4] == gsCOMPCD)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "name2", "OptionDisplay2();", true);
            }else{
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "name3", "OptionDisplay3();", true);
            }
            //F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN
            if (!gsVENDOR && keyFields[5] == "1" )
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_NUMBER", keyFields[0]);
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    ds = biz.BOARD_DETAIL_CHK(oParamDic, out errMsg1);
                }

                for (int i = 0; i < chkCOMPcount; i++)
                {
                    this.chkCOMP.Items[i].Selected = false;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[i]["F_CHK"].ToString() != "")
                        {
                            this.chkCOMP.Items[i].Selected = true;
                        }
                    }
                }
            }
            
            using (BORDBiz biz = new BORDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_NUMBER", keyFields[0]);

                ds = biz.BOARD_COMMENT_LST(oParamDic, out errMsg1);
            }

            string comment = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comment += ds.Tables[0].Rows[i]["F_COMMENTLINE"].ToString() + "\n";
                }
            }

            this.txtCOMMENT.Text = comment;
            
        }
        #endregion

        #region 게시판_삭제권한체크
        void CHK_BOARD_TBL_DUPLICATE(Dictionary<string, string> oDic, out bool bExists)
        {
            using (BORDBiz biz = new BORDBiz())
            {
                bExists = biz.PROC_BOARD_DUPLICATE(oParamDic);
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
        protected void pnlContent_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            var Param = e.Parameter.Replace(";|", "").Split('|');

            if (Param[2] == "comment")
            {
                BOARD_COMMENT_INS(Param);
            }else if(Param[2] == "update"){
                BOARD_DETAIL_UPD(Param);
            }
            else if (Param[2] == "delete")
            {
                BOARD_DETAIL_DEL(Param);
            }

        }


        void BOARD_COMMENT_INS(string[] Param)
        {

            string errMsg1 = String.Empty;

            using (BORDBiz biz = new BORDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_NUMBER", keyFields[0]);
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_COMMENT", Param[1]);
                oParamDic.Add("F_INSUSER", gsUSERID);

                ds = biz.BOARD_COMMENT_INS(oParamDic, out errMsg1);
            }

            using (BORDBiz biz = new BORDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_NUMBER", keyFields[0]);

                ds = biz.BOARD_COMMENT_LST(oParamDic, out errMsg1);

                string comment = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        comment += ds.Tables[0].Rows[i]["F_COMMENTLINE"].ToString() + "\n";
                    }
                }
                this.txtCOMMENT.Text = comment;
            }
            this.txtCOMMENTINS.Text = "";
            this.pnlContent.JSProperties["cpResultCode"] = "comment";
        }

        void BOARD_DETAIL_UPD(string[] Param)
        {

            string errMsg1 = String.Empty;

            using (BORDBiz biz = new BORDBiz())
            {
                oParamDic.Clear();

                oParamDic.Add("F_NUMBER", keyFields[0]);
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_TITLE", Param[1]);
                oParamDic.Add("F_CONTENTS", Param[3]);
                oParamDic.Add("F_FILE", Param[4]);
                oParamDic.Add("F_USER", gsUSERID);

                ds = biz.BOARD_DETAIL_UPD(oParamDic, out errMsg1);
            }
        }

        void BOARD_DETAIL_DEL(string[] Param)
        {

            string errMsg1 = String.Empty;

            bool bExists = false;
            oParamDic.Clear();
            oParamDic.Add("F_NUMBER", keyFields[0]);
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_USER", gsUSERID);
            CHK_BOARD_TBL_DUPLICATE(oParamDic, out bExists);

            if (bExists)
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_NUMBER", keyFields[0]);
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_USER", gsUSERID);
                    ds = biz.BOARD_DETAIL_DEL(oParamDic, out errMsg1);
                    this.pnlContent.JSProperties["cpResultCode"] = "delete";
                }
            }
            else
            {
                this.pnlContent.JSProperties["cpResultCode"] = "delete2";
            }
        }
        #endregion


        #endregion


    }
}