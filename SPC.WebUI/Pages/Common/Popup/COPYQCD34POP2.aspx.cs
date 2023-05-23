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
using DevExpress.Web;

namespace SPC.WebUI.Pages.Common.Popup
{
    public partial class COPYQCD34POP2 : WebUIBasePage
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

        #endregion

        #region 사용자이벤트

        #region txtITEMCDS_Init
        /// <summary>
        /// txtITEMCDS_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCDS_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('S')");
        }
        #endregion

        #region txtITEMCDT_Init
        /// <summary>
        /// txtITEMCDT_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCDT_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupItemSearch('T')");
        }
        #endregion

        #region txtWORKCDS_Init
        /// <summary>
        /// txtWORKCDS_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtWORKCDS_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('S')");
        }
        #endregion

        #region txtWORKCDT_Init
        /// <summary>
        /// txtWORKCDT_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtWORKCDT_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupWorkSearch('T')");
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            bool bExecute = false;
            string oParam = e.Parameter;

            int i = 0;
            //int j = 1;
            string str = txtWORKCDT.Text;
            string str2 = txtDISPLAYNO.Text;
            string[] array = str.Split(new char[] { '|' });
            string[] array4 = new string[array.Length -1];

            int a = 0;

            for (a = 0; a < array4.Length; a++)
            {
                //string[] array3 = array[a].Split(new char[] { '|' });
                array4[a] = array[a];
            }

            string[] array2 = str2.Split(new char[] { '|' });

            for (i = 0; i < array4.Length; i++)
            {

                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("S_ITEMCD", txtITEMCDS.Text);
                    oParamDic.Add("S_WORKCD", txtWORKCDS.Text);
                    oParamDic.Add("T_ITEMCD", txtITEMCDT.Text);
                    oParamDic.Add("T_WORKCD", array4[i]);
                    oParamDic.Add("F_USER", gsUSERID);
                    oParamDic.Add("S_DISPLAYNO", array2[1]);
                    bExecute = biz.QCD34_PROC_COPY2(oParamDic);
                }

                if (!bExecute)
                {
                    devCallback.JSProperties["cpProcType"] = "Copy";
                    devCallback.JSProperties["cpResultCode"] = "0";
                    devCallback.JSProperties["cpResultMsg"] = "검사기준 복사중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    devCallback.JSProperties["cpProcType"] = "Copy";
                    devCallback.JSProperties["cpResultCode"] = "1";
                    devCallback.JSProperties["cpResultMsg"] = "검사기준 복사가 완료되었습니다.";
                }
            }
        }
        #endregion

        #region devGrid2_CustomCallback
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            GetQCD34_LST();
        }
        #endregion

        #region devGrid_CustomCallback
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            GetQCD33_POPUP_LST(e.Parameters);
        }
        #endregion

        #region GetQCD34_LST
        void GetQCD34_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", txtITEMCDS.Text);
                oParamDic.Add("F_WORKCD", txtWORKCDS.Text);

                ds = biz.GetQCD34_LST2(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;



            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid2.DataBind();
            }

        }
        #endregion

        #region GetQCD33_POPUP_LST
        void GetQCD33_POPUP_LST(string parentParams)
        {
            string errMsg = String.Empty;

            string str = txtINSPCD.Text;

            string str2 = parentParams;

            string[] array = str2.Split(new char[] { '|' });

            oParamDic.Clear();
            if (gsVENDOR)
            {
                oParamDic.Add("F_COMPCD", GetCompCD());
                oParamDic.Add("F_FACTCD", GetFactCD());
            }
            else
            {
                string[] parentParam = parentParams.Split('|');
                oParamDic.Add("F_COMPCD", parentParam[0]);
                oParamDic.Add("F_FACTCD", parentParam[1]);
            }


            {
                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Add("F_ITEMCD", txtITEMCDT.Text);
                    oParamDic.Add("F_STATUS", "1");
                    oParamDic.Add("F_GUBUN", "1");
                    oParamDic.Add("F_INSPCD", array[0]);
                    ds = biz.QCD74_LST2(oParamDic, out errMsg);
                }                
            }

            devGrid.DataSource = ds;

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
       
        #endregion

    }
}