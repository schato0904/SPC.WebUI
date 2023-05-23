using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.Common.Export
{
    public partial class MEASUREMENTEXPORT : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string sSTDT = String.Empty;
        string sEDDT = String.Empty;
        string sITEMCD = String.Empty;
        string sWORKCD = String.Empty;
        string sSERIALNO = String.Empty;
        const string KEY = "spckey";
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

            if (!IsCallback)
            {
                // 측정목록(팝업용)
                QWK03A_POPUP_LST();
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
            sSTDT = Request.QueryString.Get("pSTDT");
            sEDDT = Request.QueryString.Get("pEDDT");
            sITEMCD = Request.QueryString.Get("pITEMCD");
            sWORKCD = Request.QueryString.Get("pWORKCD");
            sSERIALNO = Request.QueryString.Get("pSERIALNO");
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

        #region 측정갯수(팝업용)
        Int32 QWK03A_POPUP_CNT()
        {
            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", sSTDT);
                oParamDic.Add("F_EDDT", sEDDT);
                oParamDic.Add("F_ITEMCD", sITEMCD);
                oParamDic.Add("F_WORKCD", sWORKCD);
                oParamDic.Add("F_INSPCD", "AAC501");
                oParamDic.Add("F_SERIALNO", sSERIALNO);
                totalCnt = biz.QWK03A_POPUP_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 측정목록(팝업용)
        void QWK03A_POPUP_LST()
        {
            Int32 nCount = QWK03A_POPUP_CNT();

            if (nCount > 0)
            {
                string errMsg = String.Empty;

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_STDT", sSTDT);
                    oParamDic.Add("F_EDDT", sEDDT);
                    oParamDic.Add("F_ITEMCD", sITEMCD);
                    oParamDic.Add("F_WORKCD", sWORKCD);
                    oParamDic.Add("F_INSPCD", "AAC501");
                    oParamDic.Add("F_SERIALNO", sSERIALNO);
                    oParamDic.Add("F_PAGESIZE", nCount.ToString());
                    oParamDic.Add("F_CURRPAGE", "1");
                    ds = biz.QWK03A_POPUP_LST(oParamDic, out errMsg);
                }

                devGridForXls.DataSource = ds;

                devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 측정 DATA 목록", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('조회조건에 맞는 검사기준이 없습니다');self.opener = self;window.close();</script>");
                Response.End();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid HtmlRowPrepared
        /// <summary>
        /// devGrid_HtmlRowPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableRowEventArgs</param>
        protected void devGridForXls_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            {
                return;
            }

            string strJudge = e.GetValue("F_NGOKCHK").ToString();

            if (strJudge == "1")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (strJudge == "2")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
        }

        #endregion

        #endregion
    }
}