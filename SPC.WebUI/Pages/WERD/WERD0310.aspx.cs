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
    public partial class WERD0310 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언

        private DataSet ds = null;
        private string[] procResult = { "2", "Unknown Error" };
        private Dictionary<string, ProcTotalInfo> lstKey;
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

                ////ucPager.TotalItems = 0;
                ////ucPager.PagerDataBind();
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
        void SetDefaultButton() { }

        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            //this.rdoGbn.Items.Add("모델별집계", "0");
            //this.rdoGbn.Items.Add("품목별집계", "1");
            //this.rdoGbn.SelectedIndex = 0;
        }

        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts() { }

        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue() { }

        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 컬럼 생성

        void ColumnsClear(int nSchGbn)
        {
            ASPxGridView grid = null;
            string[][] gridColumn = new string[][] { 
                    new string[]{"F_ORDER","F_STEP","F_MODELCD","F_MODELNM","F_TITLE"},
                    new string[]{"F_ORDER","F_STEP","F_MODELCD","F_MODELNM","F_ITEMCD","F_ITEMNM","F_TITLE"}
                };

            if (nSchGbn == 0)
            {
                grid = devGrid1;
            }
            else
            {
                grid = devGrid2;
            }

            grid.Columns.Clear();
            grid.TotalSummary.Clear();

            foreach (string col in gridColumn[nSchGbn])
            {
                if ("F_ORDER" == col)
                    continue;

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column.FieldName = col;

                // set additional column properties
                switch (column.FieldName)
                {
                    case "F_STEP":
                        column.Visible = false;
                        break;
                    case "F_MODELCD":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(100);
                        column.Caption = "모델ID";
                        break;
                    case "F_MODELNM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(150);
                        column.Caption = "모델명";
                        break;
                    case "F_ITEMCD":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(150);
                        column.Caption = "품목코드";
                        break;
                    case "F_ITEMNM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.Width = Unit.Pixel(200);
                        column.Caption = "품목명";
                        column.FooterCellStyle.VerticalAlign = VerticalAlign.Top;
                        break;
                    case "F_TITLE":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(100);
                        column.Caption = "구분";
                        break;
                    case "SUM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        column.Caption = "합계";
                        break;
                    default:
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                }

                grid.Columns.Add(column);

                if (!"F_STEP,F_MODELCD,F_ITEMCD".Contains(column.FieldName))
                {
                    ASPxSummaryItem summaryItem = new ASPxSummaryItem() { FieldName = column.FieldName, SummaryType = DevExpress.Data.SummaryItemType.Custom };
                    grid.TotalSummary.Add(summaryItem);
                }
            }

            GridViewDataTextColumn emptyColumn = new GridViewDataTextColumn() { Width = Unit.Percentage(100) };
            grid.Columns.Add(emptyColumn);

            if (nSchGbn == 0)
            {
                new ASPxGridViewCellMerger(grid, "F_MODELNM");
            }
            else
            {
                new ASPxGridViewCellMerger(grid, "F_MODELNM|F_ITEMCD|F_ITEMNM");
            }
        }

        DataTable ReCreateColumns(int nSchGbn, ASPxGridView grid, DataTable dt)
        {
            grid.Columns.Clear();
            grid.TotalSummary.Clear();

            foreach (DataColumn dataColumn in dt.Columns)
            {
                if ("F_ORDER" == dataColumn.ColumnName)
                    continue;

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column.FieldName = dataColumn.ColumnName;

                // set additional column properties
                switch (column.FieldName)
                {
                    case "F_STEP":
                        column.Visible = false;
                        break;
                    case "F_MODELCD":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.Width = Unit.Pixel(100);
                        column.Caption = "모델ID";
                        break;
                    case "F_MODELNM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.Width = Unit.Pixel(150);
                        column.Caption = "모델명";
                        column.FooterCellStyle.VerticalAlign = VerticalAlign.Top;
                        break;
                    case "F_ITEMCD":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(150);
                        column.Caption = "품목코드";
                        break;
                    case "F_ITEMNM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.CellStyle.VerticalAlign = VerticalAlign.Top;
                        column.Width = Unit.Pixel(200);
                        column.Caption = "품목명";
                        column.FooterCellStyle.VerticalAlign = VerticalAlign.Top;
                        break;
                    case "F_TITLE":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        column.FixedStyle = GridViewColumnFixedStyle.Left;
                        column.Width = Unit.Pixel(100);
                        column.Caption = "구분";
                        break;
                    case "SUM":
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        column.Caption = "합계";
                        break;
                    default:
                        column.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                }

                grid.Columns.Add(column);

                if (!"F_STEP,F_MODELCD,F_ITEMCD".Contains(column.FieldName))
                {
                    ASPxSummaryItem summaryItem = new ASPxSummaryItem() { FieldName = column.FieldName, SummaryType = DevExpress.Data.SummaryItemType.Custom };
                    grid.TotalSummary.Add(summaryItem);
                }
            }

            if ((nSchGbn == 0 && ds.Tables[0].Columns.Count == 5)
                || (nSchGbn == 1 && ds.Tables[0].Columns.Count == 7))
            {
                GridViewDataTextColumn column = new GridViewDataTextColumn() { Width = Unit.Percentage(100) };
                grid.Columns.Add(column);
            }

            if (nSchGbn == 0)
            {
                new ASPxGridViewCellMerger(grid, "F_MODELNM");
            }
            else
            {
                new ASPxGridViewCellMerger(grid, "F_MODELNM|F_ITEMCD|F_ITEMNM");
            }

            return dt;
        }

        #endregion

        #region 그리드 목록 조회(품목)

        /// <summary>
        /// 품목 목록 조회
        /// </summary>
        /// <param name="FIELDS"></param>
        void WERD0310_LST(int nSchGbn, bool bPrint = false)
        {
            string errMsg = String.Empty;
            string title = string.Empty;
            ASPxGridView grid = null;

            if (nSchGbn == 0)
            {
                grid = devGrid1;
                title = "모델별집계";
                devGridExporter.GridViewID = "devGrid1";
            }
            else
            {
                grid = devGrid2;
                title = "모델및품목별집계";
                devGridExporter.GridViewID = "devGrid2";
            }

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", this.ucDate.GetFromDt() + "-01");
                oParamDic.Add("F_TODT", this.ucDate.GetToDt() + "-31");
                oParamDic.Add("F_ITEMCD", GetItemCD1());
                oParamDic.Add("F_MODELCD", GetModelCD());

                if (nSchGbn == 0)
                {
                    ds = biz.WERD0310_MODEL_LST(oParamDic, out errMsg);
                }
                else
                {
                    ds = biz.WERD0310_ITEM_LST(oParamDic, out errMsg);
                }
            }

            grid.DataSource = ReCreateColumns(nSchGbn, grid, setGrid(nSchGbn));

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                grid.JSProperties["cpResultCode"] = "0";
                grid.JSProperties["cpResultMsg"] = errMsg;
            }
            else if (bPrint)
            {
                devGridExporter.WriteXlsToResponse(String.Format("년{0}[{1} ~ {2}]", title, ucDate.GetFromDt(), ucDate.GetToDt())
                                                 , new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                return;
            }

            grid.DataBind();
        }
        #endregion

        private DataTable setGrid(int gbn)
        {
            DataTable NewDt = new DataTable();
            if (gbn == 0)
            {
                NewDt.Columns.Add("F_ORDER");
                NewDt.Columns.Add("F_STEP");
                NewDt.Columns.Add("F_MODELNM");
                NewDt.Columns.Add("F_TITLE");
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    NewDt.Columns.Add(dr["F_WORKDATE"].ToString());
                }
                NewDt.Columns.Add("SUM");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();

                        NewDt.Rows[0]["F_TITLE"] = "생산수량";
                        NewDt.Rows[1]["F_TITLE"] = "검사수량";
                        NewDt.Rows[2]["F_TITLE"] = "부적합수량";
                        NewDt.Rows[3]["F_TITLE"] = "불량률(%)";
                        NewDt.Rows[4]["F_TITLE"] = "PPM";
                        NewDt.Rows[5]["F_TITLE"] = "손실금액(원)";

                        for (int z = 0; z < 6; z++)
                        {
                            NewDt.Rows[z]["F_ORDER"] = i + 1;
                            NewDt.Rows[z]["F_STEP"] = z + 1;
                            NewDt.Rows[z]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        }

                        NewDt.Rows[NewDt.Rows.Count - 6][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PRODUCTQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 5][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_INSPQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 4][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_NGQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 3][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = ds.Tables[0].Rows[i]["F_NGRATE"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PPM"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 1][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_LOSSAMT"]).ToString("#,##0");
                    }
                    else
                    {
                        string strNextItem = strNextItem = NewDt.Rows[NewDt.Rows.Count - 1]["F_MODELNM"].ToString();
                        string strItemcd = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();

                        if (strItemcd != strNextItem)
                        {
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();

                            NewDt.Rows[NewDt.Rows.Count - 6]["F_TITLE"] = "생산수량";
                            NewDt.Rows[NewDt.Rows.Count - 5]["F_TITLE"] = "검사수량";
                            NewDt.Rows[NewDt.Rows.Count - 4]["F_TITLE"] = "부적합수량";
                            NewDt.Rows[NewDt.Rows.Count - 3]["F_TITLE"] = "불량률(%)";
                            NewDt.Rows[NewDt.Rows.Count - 2]["F_TITLE"] = "PPM";
                            NewDt.Rows[NewDt.Rows.Count - 1]["F_TITLE"] = "손실금액(원)";
                        }

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_ORDER"] = i + 1;

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_STEP"] = 1;
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_STEP"] = 2;
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_STEP"] = 3;
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_STEP"] = 4;
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_STEP"] = 5;
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_STEP"] = 6;

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();

                        NewDt.Rows[NewDt.Rows.Count - 6][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PRODUCTQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 5][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_INSPQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 4][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_NGQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 3][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = ds.Tables[0].Rows[i]["F_NGRATE"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PPM"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 1][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_LOSSAMT"]).ToString("#,##0");
                    }
                }
            }
            else
            {
                NewDt.Columns.Add("F_ORDER");
                NewDt.Columns.Add("F_STEP");
                NewDt.Columns.Add("F_MODELNM");
                NewDt.Columns.Add("F_ITEMCD");
                NewDt.Columns.Add("F_ITEMNM");
                NewDt.Columns.Add("F_TITLE");
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    NewDt.Columns.Add(dr["F_WORKDATE"].ToString());
                }
                NewDt.Columns.Add("SUM");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();
                        NewDt.Rows.Add();

                        NewDt.Rows[0]["F_TITLE"] = "생산수량";
                        NewDt.Rows[1]["F_TITLE"] = "검사수량";
                        NewDt.Rows[2]["F_TITLE"] = "부적합수량";
                        NewDt.Rows[3]["F_TITLE"] = "불량률(%)";
                        NewDt.Rows[4]["F_TITLE"] = "PPM";
                        NewDt.Rows[5]["F_TITLE"] = "손실금액(원)";

                        for (int z = 0; z < 6; z++)
                        {
                            NewDt.Rows[z]["F_ORDER"] = i + 1;
                            NewDt.Rows[z]["F_STEP"] = z + 1;
                            NewDt.Rows[z]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                            NewDt.Rows[z]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                            NewDt.Rows[z]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        }

                        NewDt.Rows[NewDt.Rows.Count - 6][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PRODUCTQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 5][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_INSPQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 4][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_NGQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 3][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = ds.Tables[0].Rows[i]["F_NGRATE"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PPM"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 1][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_LOSSAMT"]).ToString("#,##0");
                    }
                    else
                    {
                        string strNextItem = strNextItem = NewDt.Rows[NewDt.Rows.Count - 1]["F_ITEMCD"].ToString();
                        string strItemcd = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();

                        if (strItemcd != strNextItem)
                        {
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();
                            NewDt.Rows.Add();

                            NewDt.Rows[NewDt.Rows.Count - 6]["F_TITLE"] = "생산수량";
                            NewDt.Rows[NewDt.Rows.Count - 5]["F_TITLE"] = "검사수량";
                            NewDt.Rows[NewDt.Rows.Count - 4]["F_TITLE"] = "부적합수량";
                            NewDt.Rows[NewDt.Rows.Count - 3]["F_TITLE"] = "불량률(%)";
                            NewDt.Rows[NewDt.Rows.Count - 2]["F_TITLE"] = "PPM";
                            NewDt.Rows[NewDt.Rows.Count - 1]["F_TITLE"] = "손실금액(원)";
                        }

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_ORDER"] = i + 1;
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_ORDER"] = i + 1;

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_STEP"] = 1;
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_STEP"] = 2;
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_STEP"] = 3;
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_STEP"] = 4;
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_STEP"] = 5;
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_STEP"] = 6;

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_MODELNM"] = ds.Tables[0].Rows[i]["F_MODELNM"].ToString();

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_ITEMCD"] = ds.Tables[0].Rows[i]["F_ITEMCD"].ToString();

                        NewDt.Rows[NewDt.Rows.Count - 6]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 5]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 4]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 3]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 2]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();
                        NewDt.Rows[NewDt.Rows.Count - 1]["F_ITEMNM"] = ds.Tables[0].Rows[i]["F_ITEMNM"].ToString();

                        NewDt.Rows[NewDt.Rows.Count - 6][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PRODUCTQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 5][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_INSPQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 4][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_NGQTY"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 3][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = ds.Tables[0].Rows[i]["F_NGRATE"].ToString() ;
                        NewDt.Rows[NewDt.Rows.Count - 2][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_PPM"]).ToString("#,##0");
                        NewDt.Rows[NewDt.Rows.Count - 1][ds.Tables[0].Rows[i]["F_WORKDATE"].ToString()] = Convert.ToInt32(ds.Tables[0].Rows[i]["F_LOSSAMT"]).ToString("#,##0");
                    }
                }
            }
            

            return NewDt;
        }

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
            int nSchGbn = Convert.ToInt32(rdoGbn.SelectedItem.Value);
            bool bReturn = false;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string[] oParams = new string[2];
                foreach (string oParam in e.Parameters.Split(';'))
                {
                    oParams = oParam.Split('=');
                    if (oParams[0].Equals("clear"))
                    {
                        ColumnsClear(0);
                        ColumnsClear(1);
                        bReturn = true;
                    }
                }

                if (bReturn)
                    return;
            }

            WERD0310_LST(nSchGbn);
        }

        #endregion

        protected void devGrid_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxSummaryItem item = e.Item as ASPxSummaryItem;
            int currRow = e.RowHandle;
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                if ((grid.ID == "devGrid1" && item.FieldName == "F_MODELNM")
                    || (grid.ID == "devGrid2" && item.FieldName == "F_ITEMNM"))
                {
                    lstKey = new Dictionary<string, ProcTotalInfo>();
                }
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                if (!"F_STEP,F_ITEMCD,F_MODELCD,F_MODELNM,F_ITEMNM,F_TITLE".Contains(item.FieldName))
                {
                    if (!lstKey.ContainsKey(item.FieldName))
                        lstKey.Add(item.FieldName, new ProcTotalInfo());

                    int fStep = Convert.ToInt32(grid.GetRowValues(currRow, "F_STEP"));

                    string strValue = string.Empty;
                    if (e.FieldValue == null || e.FieldValue.ToString() == "")
                    {
                        strValue = "0";
                    }
                    else
                    {
                        strValue = e.FieldValue.ToString();
                    }

                    switch (fStep)
                    {
                        case 1:
                            lstKey[item.FieldName].TotalPrcCnt += Convert.ToInt32(strValue.Replace(",", ""));
                            break;
                        case 2:
                            lstKey[item.FieldName].TotalInspCnt += Convert.ToInt32(strValue.Replace(",", ""));
                            break;
                        case 3:
                            lstKey[item.FieldName].TotalNgCnt += Convert.ToInt32(strValue.Replace(",", ""));
                            break;
                        case 6:
                            lstKey[item.FieldName].TotalUnitPrice += Convert.ToInt32(strValue.Replace(",", ""));
                            break;
                    }
                }
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                if (grid.VisibleRowCount > 0)
                {
                    if ((grid.ID == "devGrid1" && item.FieldName == "F_MODELNM")
                   || (grid.ID == "devGrid2" && item.FieldName == "F_ITEMNM"))
                    {
                        e.TotalValue = "<span>합계</span>";
                    }

                    if (item.FieldName == "F_TITLE")
                    {
                        e.TotalValue = "생산수량<br/>검사수량<br/>부적합수량<br/>부적합률(%)<br/>PPM<br/>손실금액(원)";
                    }

                    if (!"F_STEP,F_MODELCD,F_MODELNM,F_ITEMCD,F_ITEMNM,F_TITLE".Contains(item.FieldName))
                    {
                        if (lstKey == null) return;
                        if (lstKey.Count == 0) return;

                        if (lstKey.ContainsKey(item.FieldName))
                        {
                            lstKey[item.FieldName].TotalNgRate = Math.Round(lstKey[item.FieldName].TotalNgCnt * 100.0 / lstKey[item.FieldName].TotalPrcCnt,2);
                            lstKey[item.FieldName].TotalPPM = Math.Round(lstKey[item.FieldName].TotalNgCnt * 1000000.0 / lstKey[item.FieldName].TotalPrcCnt, 0);

                            e.TotalValue = string.Format("<div align='right'> {0:n0}<br/>{1:n0}<br/>{2:n0}<br/>{3:##0.##}<br/>{4:n0}<br/>{5:n0}",
                                                        lstKey[item.FieldName].TotalPrcCnt,
                                                        lstKey[item.FieldName].TotalInspCnt,
                                                        lstKey[item.FieldName].TotalNgCnt,
                                                        double.IsNaN(lstKey[item.FieldName].TotalNgRate) ? 0 : lstKey[item.FieldName].TotalNgRate,
                                                        double.IsNaN(lstKey[item.FieldName].TotalPPM) ? 0 : lstKey[item.FieldName].TotalPPM,
                                                        lstKey[item.FieldName].TotalUnitPrice);
                        }
                    }
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
            int nSchGbn = Convert.ToInt32(rdoGbn.SelectedItem.Value);

            WERD0310_LST(nSchGbn, true);
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

            if (e.RowType == GridViewRowType.Footer)
            {
                e.Text = e.Text.Replace("<br/>", Environment.NewLine);
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        protected void devGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            int row = e.VisibleRowIndex;
            string fieldName = e.Column.FieldName;
            string fStep = grid.GetRowValues(row, "F_STEP").ToString();

            if ("F_ORDER,F_STEP,F_MODELCD,F_MODELNM,F_ITEMCD,F_ITEMNM,F_TITLE".Contains(fieldName))
                return;

            if (fStep == "4")
            {
                e.Column.PropertiesEdit.DisplayFormatString = null;
            }
            else
            {
                e.Column.PropertiesEdit.DisplayFormatString = "n0";
            }
        }

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].CellStyle.VerticalAlign = VerticalAlign.Middle;
            }

        }

        #endregion
    }
}