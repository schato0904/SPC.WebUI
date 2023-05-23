using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.WERD.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD5002_DACO : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataSet ds
        {
            get
            {
                return (DataSet)Session["WERD5002_DACO_1"];
            }
            set
            {
                Session["WERD5002_DACO_1"] = value;
            }
        }

        DataSet ds2
        {
            get
            {
                return (DataSet)Session["WERD5002_DACO_2"];
            }
            set
            {
                Session["WERD5002_DACO_2"] = value;
            }
        }

        DataTable dt
        {
            get
            {
                return (DataTable)Session["WERD5002_DACO_3"];
            }
            set
            {
                Session["WERD5002_DACO_3"] = value;
            }
        }
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
                WERD5002_DACO_LST();
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

                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";

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

        #region WERD5002_DACO_LST
        void WERD5002_DACO_LST()
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", GetBanCD());
                oParamDic.Add("F_LINECD", GetLineCD());
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_GBN", cbGBN.SelectedItem.Value.ToString());
                oParamDic.Add("F_USER", gsUSERID);
                ds = biz.WERD5002_DACO_LST(oParamDic, out errMsg);

            }

            devGrid.DataSource = ds.Tables[0];
            txtAUTH.Text = ds.Tables[1].Rows[0][0].ToString();

            if (!String.IsNullOrEmpty(errMsg))
            {
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

        #region WERD5002_DACO_DETAIL_LST
        void WERD5002_DACO_DETAIL_LST(string[] oParams)
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", oParams[0].ToString());
                oParamDic.Add("F_BANCD", oParams[1].ToString());
                oParamDic.Add("F_LINECD", oParams[2].ToString());
                oParamDic.Add("F_WORKCD", oParams[3].ToString());
                oParamDic.Add("F_ITEMCD1", oParams[8].ToString());
                oParamDic.Add("F_ITEMCD2", oParams[10].ToString());
                ds2 = biz.WERD5002_DACO_DETAIL_LST(oParamDic, out errMsg);
            }

            int cnt = ds2.Tables[0].Rows.Count;

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("F_MEAINSPNM");
            //dtTemp.Columns.Add("F_SERIALNO");
            dtTemp.Columns.Add("F_STANDARD");
            dtTemp.Columns.Add("F_DATA1");
            dtTemp.Columns.Add("F_DATA2");
            dtTemp.Columns.Add("F_DATA3");
            dtTemp.Columns.Add("F_DATA4");
            dtTemp.Columns.Add("F_DATA5");
            dtTemp.Columns.Add("F_DATA6");
            dtTemp.Columns.Add("F_DATA7");
            dtTemp.Columns.Add("F_DATA8");
            dtTemp.Columns.Add("F_DATA9");
            dtTemp.Columns.Add("F_DATA10");

            for (int i = 0; i < cnt; i++)
            {
                dtTemp.Rows.Add();
                
                dtTemp.Rows[i]["F_MEAINSPNM"] = ds2.Tables[0].Rows[i]["F_MEAINSPNM"].ToString();
                dtTemp.Rows[i]["F_STANDARD"] = ds2.Tables[0].Rows[i]["F_STANDARD"].ToString();

                int rowcnt = Convert.ToInt16(ds2.Tables[0].Rows[i]["F_TEST"].ToString());

                try
                {
                    for (int j = 0; j < rowcnt; j++)
                    {
                        dtTemp.Rows[i][j + 2] = ds2.Tables[1].Rows[(5 * i) + j]["F_MEASURE"].ToString();
                        


                    }
                }
                catch { }
                try
                {
                    for (int j2 = 0; j2 < rowcnt; j2++)
                    {
                        dtTemp.Rows[i][j2 + 7] = ds2.Tables[2].Rows[(5 * i) + j2]["F_MEASURE"].ToString();                        

                    }
                }
                catch
                { }
            }

            devGrid2.DataSource = dtTemp;
            dt = dtTemp; // REPORT 생성용

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid2.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            WERD5002_DACO_LST();
        }
        #endregion

        #region devGrid2_CustomCallback
        protected void devGrid2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] oParams = e.Parameters.Split('|');

            WERD5002_DACO_DETAIL_LST(oParams);
        }
        #endregion

        #region devCallback_Callback
        protected void devCallback_Callback(object source, CallbackEventArgs e)
        {
            string[] oParams = e.Parameter.Split(',');

            int cnt = oParams.Length;

            for (int i = 0; i < cnt; i++)
            {
                WERD5002_DACO_UPD(oParams[i]);
            }
        }
        #endregion

        #region WERD5002_DACO_UPD
        void WERD5002_DACO_UPD(string _oParams)
        {
            string errMsg = String.Empty;
            string[] oParams = _oParams.Split('|');
            bool bExecute = false;

            oParamDic.Clear();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_WORKDATE", oParams[0]);
            oParamDic.Add("F_BANCD", oParams[1]);
            oParamDic.Add("F_LINECD", oParams[2]);
            oParamDic.Add("F_WORKCD", oParams[3]);
            oParamDic.Add("F_CHK", oParams[4]);
            oParamDic.Add("F_USER", gsUSERID);

            using (WERDBiz biz = new WERDBiz())
            {
                bExecute = biz.WERD5002_DACO_UPD(oParamDic);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "결재 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다." };
            }
            else
            {
                procResult = new string[] { "1", "결재가 완료되었습니다." };

            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
        }
        #endregion

        #endregion
    }
}