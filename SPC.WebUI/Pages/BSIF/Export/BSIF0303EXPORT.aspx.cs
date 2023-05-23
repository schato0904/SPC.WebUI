using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.BSIF.Biz;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.BSIF.Export
{
    public partial class BSIF0303EXPORT : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string sBANCD = String.Empty;
        string sITEMCD = String.Empty;
        string sWORKCD = String.Empty;
        string sINSPCD = String.Empty;
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
                // 검사기준목록을 구한다
                GetQCD34_LST();
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
            sBANCD = Request.QueryString.Get("pBANCD");
            sITEMCD = Request.QueryString.Get("pITEMCD") ?? "";
            sWORKCD = Request.QueryString.Get("pWORKCD");
            sINSPCD = Request.QueryString.Get("pINSPCD");
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

        #region 검사기준 전체 갯수를 구한다
        Int32 GetQCD34_CNT()
        {
            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");
                oParamDic.Add("F_BANCD", sBANCD);
                oParamDic.Add("F_ITEMCD", sITEMCD);
                oParamDic.Add("F_INSPCD", sINSPCD);
                oParamDic.Add("F_WORKCD", sWORKCD);
                totalCnt = biz.GetQCD34_CNT(oParamDic);
            }

            return totalCnt;
        }
        #endregion

        #region 검사기준목록을 구한다
        void GetQCD34_LST()
        {
            Int32 nCount = GetQCD34_CNT();

            if (nCount > 0)
            {
                string errMsg = String.Empty;

                using (BSIFBiz biz = new BSIFBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_STATUS", "1");
                    oParamDic.Add("F_BANCD", sBANCD);
                    oParamDic.Add("F_ITEMCD", sITEMCD);
                    oParamDic.Add("F_INSPCD", sINSPCD);
                    oParamDic.Add("F_WORKCD", sWORKCD);
                    oParamDic.Add("F_PAGESIZE", nCount.ToString());
                    oParamDic.Add("F_CURRPAGE", "1");
                    ds = biz.GetQCD34_LST(oParamDic, out errMsg);
                }

                devGridForXls.DataSource = ds;

                devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 검사기준", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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

        #region devGrid HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            // Tooltip 출력
            string[] sTooltipFields = { "F_ITEMNM", "F_BANNM", "F_LINENM", "F_WORKNM" };

            foreach (string sTooltipField in sTooltipFields)
            {
                if (e.DataColumn.FieldName.Equals(sTooltipField))
                {
                    if (e.CellValue != null)
                        e.Cell.ToolTip = e.CellValue.ToString();
                }
            }
        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;

            // Zero Setting
            if (e.Column.FieldName.Equals("F_ZERO"))
            {
                e.DisplayText = e.Value.ToString().Equals("1") ? "Yes" : "No";
                return;
            }
        }
        #endregion

        #endregion
    }
}