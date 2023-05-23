using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0302 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언

        private DataSet ds = null;
        private string[] procResult = { "2", "Unknown Error" };
        private int totalCnt = 0;
        private int totalPrdCnt = -1;
        private int totalUnitPrice = -1;
        private double totalRate = 0;
        private int totalInspCnt = 0;
        private int totalNgTypeCnt = 0;
        private double totalNgrate = 0;
        private List<string> lstCOMMCD = new List<string>();

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
            //new ASPxGridViewCellMerger(devGrid1, "F_ITEMCD|F_ITEMNM");
            new ASPxGridViewCellMerger(devGrid2, "F_ITEMCD|F_ITEMNM|F_WORKNM|F_PRODUCTQTY|F_INSPQTY|F_NGQTY|F_NGRATE|F_PPM|F_LOSSAMT");
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

                ucPager1.TotalItems = 0;
                ucPager1.PagerDataBind();
                ucPager2.TotalItems = 0;
                ucPager2.PagerDataBind();
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

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
        {
        }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            string errMsg = string.Empty;

            //AspxCombox_DataBind(ucPROCCD, "PP", "PPA4");
            //this.ucPROCCD.Items.Insert(0, new DevExpress.Web.ListEditItem("전체", "")); ;
            //this.ucPROCCD.SelectedIndex = 0;

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

        #region 검사기준이력조회 총 갯수

        Int32 WERD0302_CNT(int nSchGbn)
        {
            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());

                if (nSchGbn == 0)
                {
                    totalCnt = biz.WERD0302_WORK_CNT(oParamDic);
                }
                else
                {
                    totalCnt = biz.WERD0302_NG_CNT(oParamDic);
                }
            }

            return totalCnt;
        }

        #endregion

        #region 그리드 목록 조회(품목)

        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD0302_LST(int nPageSize, int nCurrPage, int nSchGbn, bool bCallback, bool bPrint = false)
        {
            string errMsg = String.Empty;
            ASPxGridView grid = null;
            Resources.controls.userControl.ucPagerMulti ucPager = null;

            if (nSchGbn == 0)
            {
                grid = devGrid1;
                ucPager = ucPager1;
                this.ucPager1.targetCtrls = "devGrid1";

            }
            else
            {
                grid = devGrid2;
                //ucPager = ucPager2;
                ucPager = ucPager1;
                this.ucPager1.targetCtrls = "devGrid2";
            }

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_PAGESIZE", nPageSize.ToString());
                oParamDic.Add("F_CURRPAGE", nCurrPage.ToString());
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());

                if (nSchGbn == 0)
                {
                    ds = biz.WERD0302_WORK_LST(oParamDic, out errMsg);
                }
                else
                {
                    ds = biz.WERD0302_NG_LST(oParamDic, out errMsg);
                }
            }


            grid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                grid.JSProperties["cpResultCode"] = "0";
                grid.JSProperties["cpResultMsg"] = errMsg;
            }
            else if (bPrint)
            {
                string title = string.Empty;
                if (nSchGbn == 0)
                {
                    title = "공정별집계";
                    devGridExporter.GridViewID = "devGrid1";
                }
                else
                {
                    title = "부적합수량집계";
                    devGridExporter.GridViewID = "devGrid2";
                }

                devGridExporter.WriteXlsToResponse(String.Format("품목별부적합집계({0})[{1} ~ {2}]", title, ucDate.GetFromDt(), ucDate.GetToDt())
                                                 , new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                return;
            }
            else
            {
                if (!bCallback)
                {
                    // Pager Setting
                    ucPager.TotalItems = 0;
                    ucPager.PagerDataBind();
                }
                else
                {
                    grid.JSProperties["cpResultCode"] = "pager";
                    grid.JSProperties["cpPagerTarget"] = nSchGbn;
                    grid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", nPageSize, nCurrPage, WERD0302_CNT(nSchGbn));
                }
            }

            grid.DataBind();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback

        /// <summary>
        /// 품목 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            int nCurrPage = 0;
            int nPageSize = 0;
            int nSchGbn = 0;
            bool bReturn = false;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string[] oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("PAGESIZE"))
                        nPageSize = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("CURRPAGE"))
                        nCurrPage = Convert.ToInt32(oParams[1]);
                    else if (oParams[0].Equals("clear"))
                    {
                        this.devGrid1.DataSourceID = null;
                        this.devGrid1.DataSource = null;
                        this.devGrid1.DataBind();
                        this.devGrid2.DataSourceID = null;
                        this.devGrid2.DataSource = null;
                        this.devGrid2.DataBind();
                    }
                    else if (oParams[0].Equals("gbn"))
                    {
                        ASPxGridView grid = sender as ASPxGridView;
                        grid.JSProperties["cpResultCode"] = "pager";
                        grid.JSProperties["cpPagerTarget"] = oParams[1];
                        grid.JSProperties["cpResultMsg"] = String.Format("PAGESIZE={0};CURRPAGE={1};ITEMSCNT={2}", 50, 1, 0);
                        bReturn = true;
                    }
                }

                if (bReturn)
                    return;

            }
            else
            {
                nCurrPage = 1;
                nPageSize = ucPager1.GetPageSize();
            }

            //if (!string.IsNullOrEmpty(Request[rdoGbn.UniqueID]))
                nSchGbn = Convert.ToInt32(rdoGbn.SelectedItem.Value);

            WERD0302_LST(nPageSize, nCurrPage, nSchGbn, true);

        }

        #endregion

        protected void devGrid_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxSummaryItem item = e.Item as ASPxSummaryItem;
            int currRow = e.RowHandle;
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                if (item.FieldName == "F_NUMBER")
                {
                    lstCOMMCD = new List<string>();
                    totalPrdCnt = 0;
                    totalUnitPrice = 0;
                    totalInspCnt = 0;
                    totalNgTypeCnt = 0;
                    totalNgrate = 0;
                }
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                if (item.FieldName == "F_NUMBER")
                {
                    string commCd = e.FieldValue.ToString();//grid.GetRowValues(currRow, "F_COMMCD").ToString();

                    // MergeCell 일 경우  키값 중복체크.
                    if (!lstCOMMCD.Contains(commCd))
                    {
                        lstCOMMCD.Add(commCd);
                        int prdcnt = Convert.ToInt32(grid.GetRowValues(currRow, "F_PRODUCTQTY"));
                        totalPrdCnt += prdcnt;
                        totalUnitPrice += Convert.ToInt32(grid.GetRowValues(currRow, "F_LOSSAMT"));
                        totalInspCnt += Convert.ToInt32(grid.GetRowValues(currRow, "F_INSPQTY"));
                        totalNgTypeCnt += Convert.ToInt32(grid.GetRowValues(currRow, "F_NGQTY"));
                        totalNgrate += Convert.ToDouble(grid.GetRowValues(currRow, "F_NGRATE"));
                    }
                }
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                if (lstCOMMCD.Count == 0) return;

                if (item.FieldName == "F_PRODUCTQTY")
                {
                    e.TotalValue = totalPrdCnt;
                }

                if (item.FieldName == "F_INSPQTY")
                {
                    e.TotalValue = totalInspCnt;
                }

                if (item.FieldName == "F_NGQTY")
                {
                    e.TotalValue = totalNgTypeCnt;
                }

                if (item.FieldName == "F_NGRATE")
                {
                    totalRate = totalNgTypeCnt * 100.0 / totalPrdCnt;

                    if (!double.IsNaN(totalNgrate))
                        e.TotalValue = Math.Round(totalRate,2);
                }

                if (item.FieldName == "F_PPM")
                {
                    totalRate = Math.Round((totalNgTypeCnt * 1000000.0) / totalPrdCnt, 0);

                    if (!double.IsNaN(totalNgrate))
                        e.TotalValue = Math.Round(totalRate,0);
                }

                if (item.FieldName == "F_LOSSAMT")
                {
                    e.TotalValue = totalUnitPrice;
                }
            }
        }

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int nSchGbn = 0;
            if (!string.IsNullOrEmpty(Request[rdoGbn.UniqueID]))
                nSchGbn = Convert.ToInt32(Request[rdoGbn.UniqueID]);

            WERD0302_LST(WERD0302_CNT(nSchGbn), 1, nSchGbn, false, true);
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<br />", String.Empty);
            }
        }
        #endregion

        #endregion
    }
}