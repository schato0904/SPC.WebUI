using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.BORD.Biz;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.BORD
{
    public partial class BORD0102 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
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

                this.rdoGbn.Items.Add("공지사항", "0");
                this.rdoGbn.Items.Add("요청사항", "1");

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_MASTERCHK", "0");
                    ds = biz.QCM01_LST(oParamDic, out errMsg);
                }

                this.chkCOMP.DataSource = ds;
                this.chkCOMP.TextField = "F_COMPNM";
                this.chkCOMP.ValueField = "F_COMPCD";
                this.chkCOMP.DataBind();
            }
            else
            {
                this.chkCOMP.ClientVisible = false;

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_MASTERCHK", "1");
                    ds = biz.QCM01_LST(oParamDic, out errMsg);
                }

                this.rdoGbn.DataSource = ds;
                this.rdoGbn.TextField = "F_COMPNM";
                this.rdoGbn.ValueField = "F_COMPCD";
                this.rdoGbn.DataBind();
                this.rdoGbn.Items.Add("공지사항", gsCOMPCD);

                
            }

            this.rdoGbn.SelectedIndex = 0;
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

        #region 등록
        void BOARD_INS(string[] Param)
        {
            int idx = 0;
            int count = Param[4].Split(';').Length;

            if (Param[0] == "0")
                count = 1;

            string[] selectList = Param[4].Split(';');
            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            if (Param[0] == "0")
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_GBN", Param[0]);
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_VENDORCOMPCD", "");
                oParamDic.Add("F_TITLE", Param[1]);
                oParamDic.Add("F_CONTENTS", Param[2]);
                oParamDic.Add("F_FILE", Param[3]);
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_IDX", idx.ToString());

                oSPs[idx] = "USP_BOARD_INS";
                oParameters[idx] = (object)oParamDic;

                idx++;
            }
            else
            {
                foreach (string Value in selectList)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_GBN", Param[0]);
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_VENDORCOMPCD", Value);
                    oParamDic.Add("F_TITLE", Param[1]);
                    oParamDic.Add("F_CONTENTS", Param[2]);
                    oParamDic.Add("F_FILE", Param[3]);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("F_IDX", idx.ToString());

                    oSPs[idx] = "USP_BOARD_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BORDBiz biz = new BORDBiz())
            {
                bExecute = biz.BOARD_INS(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            pnlContent.JSProperties["cpResultCode"] = procResult[0];
            pnlContent.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
        }
        #endregion

        #region MyRegion
        void BOARD_VENDOR_INS(string[] Param)
        {
            int idx = 0;
            int count = 1;
            string strGbn = "";

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            if (Param[0] == "01" || Param[0] == "02")
            {
                strGbn = "1";
            }
            else
            {
                strGbn = "0"; 
            }

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_GBN", strGbn);
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_VENDORCOMPCD", Param[0]);
            oParamDic.Add("F_TITLE", Param[1]);
            oParamDic.Add("F_CONTENTS", Param[2]);
            oParamDic.Add("F_FILE", Param[3]);
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_IDX", idx.ToString());

            oSPs[idx] = "USP_BOARD_INS";
            oParameters[idx] = (object)oParamDic;

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BORDBiz biz = new BORDBiz())
            {
                bExecute = biz.BOARD_INS(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            pnlContent.JSProperties["cpResultCode"] = procResult[0];
            pnlContent.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
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
            if(gsVENDOR){
                var Param = e.Parameter.Replace(";|", "").Split('|');
                BOARD_VENDOR_INS(Param);
            }else{
                var Param = e.Parameter.Replace(";|", "|").Split('|');
                BOARD_INS(Param);
            }
        }
        #endregion
        #endregion

        
    }
}