using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.IPCM.Biz;
using SPC.BORD.Biz;

namespace SPC.WebUI.Pages.COMM
{
    public partial class COMM0101 : WebUIBasePage
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
            //// 이상통보
            //COMM0101_LST();

            //// 품질이상제기
            //QWK100_NOPAGING_LST();

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devGrid3.JSProperties["cpResultCode"] = "";
                devGrid3.JSProperties["cpResultMsg"] = "";
                devGrid4.JSProperties["cpResultCode"] = "";
                devGrid4.JSProperties["cpResultMsg"] = "";
                devGrid5.JSProperties["cpResultCode"] = "";
                devGrid5.JSProperties["cpResultMsg"] = "";
                devGrid6.JSProperties["cpResultCode"] = "";
                devGrid6.JSProperties["cpResultMsg"] = "";
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
            if (!gsVENDOR)
            {
                devGrid3.Visible = true;
                devGrid6.Visible = Convert.ToBoolean(gsUSEBOARD);
            }
            else
            {
                devGrid2.Visible = true;
                devGrid4.Visible = Convert.ToBoolean(gsUSEBOARD);
                devGrid5.Visible = Convert.ToBoolean(gsUSEBOARD);
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 공정이상통보 데이터 조회
        void COMM0101_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.GetQWKWRONGREPORT_COMM0101_LST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #region 품질이상제기 목록을 구한다
        void QWK100_NOPAGING_LST(string sPROGRESS)
        {
            string errMsg = String.Empty;

            using (IPCMBiz biz = new IPCMBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_STDATE", DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd"));
                oParamDic.Add("F_EDDATE", DateTime.Today.ToString("yyyy-MM-dd"));
                if (!gsVENDOR)
                {
                    oParamDic.Add("F_COMPCD", "");
                    oParamDic.Add("F_FACTCD", "");
                }
                else
                {
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                }
                oParamDic.Add("F_RQCPCD", gsCOMPCD);
                oParamDic.Add("F_RQFTCD", gsFACTCD);
                oParamDic.Add("F_PROGRESS", sPROGRESS);
                oParamDic.Add("F_PROGRESSTP", "RQ");
                oParamDic.Add("F_BVENDOR", !gsVENDOR ? "0" : "1");
                oParamDic.Add("F_PROGRESSST", !gsVENDOR ? "2" : "1");
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.QWK100_NOPAGING_LST(oParamDic, out errMsg);
            }

            if (!gsVENDOR)
            {
                devGrid3.DataSource = ds;

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGrid3.JSProperties["cpResultCode"] = "0";
                    devGrid3.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (IsCallback)
                        devGrid3.DataBind();
                }
            }
            else
            {
                devGrid2.DataSource = ds;

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (IsCallback)
                        devGrid2.DataBind();
                }
            }
        }
        #endregion

        #region 공지사항 목록을 구한다
        void BOARD_TBL_LST(string sPROGRESS)
        {
            string errMsg = String.Empty;

            if (sPROGRESS == "4")
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_GBN", "1");
                    ds = biz.BOARD_MAIN_LST(oParamDic, out errMsg);
                }
                devGrid4.DataSource = ds;


                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (IsCallback)
                        devGrid4.DataBind();
                }
            }
            else if (sPROGRESS == "5")
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_GBN", "2");
                    ds = biz.BOARD_MAIN_LST(oParamDic, out errMsg);
                }
                devGrid5.DataSource = ds;


                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (IsCallback)
                        devGrid5.DataBind();
                }
            }
            else
            {
                using (BORDBiz biz = new BORDBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_GBN", "3");
                    ds = biz.BOARD_MAIN_LST(oParamDic, out errMsg);
                }
                devGrid6.DataSource = ds;


                if (!String.IsNullOrEmpty(errMsg))
                {
                    devGrid2.JSProperties["cpResultCode"] = "0";
                    devGrid2.JSProperties["cpResultMsg"] = errMsg;
                }
                else
                {
                    if (IsCallback)
                        devGrid6.DataBind();
                }
            }

        }
        #endregion


        #endregion

        #region 사용자이벤트

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            COMM0101_LST();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK100_NOPAGING_LST("1");
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid3_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK100_NOPAGING_LST("3");
        }

        protected void devGrid4_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            BOARD_TBL_LST("4");
        }

        protected void devGrid5_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            BOARD_TBL_LST("5");
        }
        #endregion

        protected void devGrid6_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            BOARD_TBL_LST("6");
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid2_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_RQRCDT"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sRSDATE = grid.GetRowValues(e.VisibleIndex, "F_RSDATE").ToString();
                    if (String.IsNullOrEmpty(sRSDATE))
                        sRSDATE = DateTime.Today.ToString("yyyy-MM-dd");

                    int nDateDiff = UF.Date.DateDiff(e.CellValue.ToString(), sRSDATE);

                    if (nDateDiff <= 0)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                        e.Cell.Font.Bold = true;
                    }
                    else if (nDateDiff <= 7)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                        e.Cell.Font.Bold = true;
                    }
                }
            }
            else
                return;
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid3_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Equals("F_RQRCDT"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sRSDATE = grid.GetRowValues(e.VisibleIndex, "F_RSDATE").ToString();
                    if (String.IsNullOrEmpty(sRSDATE))
                        sRSDATE = DateTime.Today.ToString("yyyy-MM-dd");

                    int nDateDiff = UF.Date.DateDiff(e.CellValue.ToString(), sRSDATE);

                    if (nDateDiff <= 0)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                        e.Cell.Font.Bold = true;
                    }
                    else if (nDateDiff <= 7)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Blue;
                        e.Cell.Font.Bold = true;
                    }
                }
            }
            else
                return;
        }
        #endregion


    }
}