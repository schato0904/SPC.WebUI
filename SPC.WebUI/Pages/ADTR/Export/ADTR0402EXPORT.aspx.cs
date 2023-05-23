using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.ADTR.Biz;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.ADTR.Export
{
    public partial class ADTR0402EXPORT : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        string sCOMPCD = String.Empty;
        string sFACTCD = String.Empty;
        string sSTDT = String.Empty;
        string sEDDT = String.Empty;
        string sJUDGE = String.Empty;
        string sINSPCD = String.Empty;
        string sITEMCD = String.Empty;
        string sWORKCD = String.Empty;
        string sLOTNO = String.Empty;
        string sMEAINSPCD = String.Empty;
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
                // Data조회
                QWK04A_ADTR0402_LST();
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
            sCOMPCD = Request.QueryString.Get("pCOMPCD") ?? "";
            sFACTCD = Request.QueryString.Get("pFACTCD") ?? "";
            sSTDT = Request.QueryString.Get("pSTDT") ?? "";
            sEDDT = Request.QueryString.Get("pEDDT") ?? "";
            sJUDGE = Request.QueryString.Get("pJUDGE") ?? "";
            sINSPCD = Request.QueryString.Get("pINSPCD") ?? "";
            sITEMCD = Request.QueryString.Get("pITEMCD") ?? "";
            sWORKCD = Request.QueryString.Get("pWORKCD") ?? "";
            sLOTNO = Request.QueryString.Get("pLOTNO") ?? "";
            sMEAINSPCD = Request.QueryString.Get("pMEAINSPCD") ?? "";
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

        #region Data총갯수
        Int32 QWK04A_ADTR0402_CNT()
        {
            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                if (!this.gsVENDOR)  // 마스터
                {
                    oParamDic.Add("S_COMPCD", gsCOMPCD);
                    oParamDic.Add("S_FACTCD", gsFACTCD);
                    oParamDic.Add("F_COMPCD", sCOMPCD);
                    oParamDic.Add("F_FACTCD", sFACTCD);
                }
                else                 // 협력사
                {
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                }
                oParamDic.Add("F_FROMDT", sSTDT);
                oParamDic.Add("F_TODT", sEDDT);
                oParamDic.Add("F_JUDGE", sJUDGE);
                oParamDic.Add("F_INSPCD", sINSPCD);
                oParamDic.Add("F_ITEMCD", sITEMCD);
                oParamDic.Add("F_WORKCD", sWORKCD);
                oParamDic.Add("F_LOTNO", sLOTNO);
                oParamDic.Add("F_MEAINSPCD", sMEAINSPCD);
                if (!this.gsVENDOR)  // 마스터
                {
                    totalCnt = biz.QWK04A_ADTR0402_CNT_MST(oParamDic);
                }
                else                 // 협력사
                {
                    totalCnt = biz.QWK04A_ADTR0402_CNT(oParamDic);
                }

            }

            return totalCnt;
        }
        #endregion

        #region Data조회
        void QWK04A_ADTR0402_LST()
        {
            string errMsg = String.Empty;
            Int32 nCount = QWK04A_ADTR0402_CNT();

            if (nCount > 0)
            {
                using (ADTRBiz biz = new ADTRBiz())
                {
                    oParamDic.Clear();
                    if (!this.gsVENDOR)  // 마스터
                    {
                        oParamDic.Add("S_COMPCD", gsCOMPCD);
                        oParamDic.Add("S_FACTCD", gsFACTCD);
                        oParamDic.Add("F_COMPCD", sCOMPCD);
                        oParamDic.Add("F_FACTCD", sFACTCD);
                    }
                    else                 // 협력사
                    {
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                    }
                    oParamDic.Add("F_FROMDT", sSTDT);
                    oParamDic.Add("F_TODT", sEDDT);
                    oParamDic.Add("F_JUDGE", sJUDGE);
                    oParamDic.Add("F_INSPCD", sINSPCD);
                    oParamDic.Add("F_ITEMCD", sITEMCD);
                    oParamDic.Add("F_WORKCD", sWORKCD);
                    oParamDic.Add("F_LOTNO", sLOTNO);
                    oParamDic.Add("F_PAGESIZE", nCount.ToString());
                    oParamDic.Add("F_CURRPAGE", "1");
                    oParamDic.Add("F_MEAINSPCD", sMEAINSPCD);
                    if (!this.gsVENDOR)  // 마스터
                    {
                        ds = biz.QWK04A_ADTR0402_LST_MST(oParamDic, out errMsg);
                    }
                    else                 // 협력사
                    {
                        ds = biz.QWK04A_ADTR0402_LST(oParamDic, out errMsg);
                    }
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    Response.Write("<script type=\"text/javascript\">alert('측정데이터를 엑셀로 변화하는 중 장애가 발생하였습니다');self.opener = self;window.close();</script>");
                    Response.End();
                }
                else
                {
                    DataTable dtTable = ds.Tables[0];
                    dtTable.Columns.Add("F_NGOKCHKTEXT", typeof(String));

                    string sNGOKCHKTEXT = String.Empty;

                    foreach (DataRow dtRow in dtTable.Rows)
                    {
                        switch (dtRow["F_NGOKCHK"].ToString())
                        {
                            case "0":
                                sNGOKCHKTEXT = "OK";
                                break;
                            case "1":
                                sNGOKCHKTEXT = "NG";
                                break;
                            case "2":
                                sNGOKCHKTEXT = "OC";
                                break;
                        }

                        dtRow["F_NGOKCHKTEXT"] = sNGOKCHKTEXT;
                    }

                    devGridForXls.DataSource = dtTable;

                    devGridExporter.WriteXlsToResponse(String.Format("측정데이터({0}~{1})",
                        Convert.ToDateTime(sSTDT).ToString("yyyy.MM.dd"), Convert.ToDateTime(sEDDT).ToString("yyyy.MM.dd")),
                        new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('조회조건에 맞는 측정데이터가 없습니다');self.opener = self;window.close();</script>");
                Response.End();
            }
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            //DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            //if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_NGOKCHK") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            //{
            //    switch (e.Text)
            //    {
            //        case "0": e.Text = "OK"; break;
            //        case "1": e.Text = "NG"; break;
            //        case "2": e.Text = "OC"; break;
            //        default:
            //            break;
            //    }

            //}

        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //devGridExporter.WriteXlsToResponse(String.Format("측정데이터({0}~{1})",
            //            Convert.ToDateTime(sSTDT).ToString("yyyy.MM.dd"), Convert.ToDateTime(sEDDT).ToString("yyyy.MM.dd")),
            //            new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #endregion
    }
}