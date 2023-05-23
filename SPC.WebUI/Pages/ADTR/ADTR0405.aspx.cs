using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.ADTR.Biz;
using DevExpress.Web;

namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0405 : WebUIBasePage
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

        #region 데이터조회
        void ADTR0405_LST()
        {
            // 그리드 컬럼 초기화
            devGrid.Columns.Clear();

            // 기본 컬럼생성
            AddColumn("F_WORKDATE", "검사일자", "", HorizontalAlign.Center, 100);
            AddColumn("F_WORKTIME", "검사시간", "", HorizontalAlign.Center, 100);
            AddColumn("F_TSERIALNO", "검사차수", "", HorizontalAlign.Center, 100);
            AddColumn("F_NUMBER", "검사순번", "", HorizontalAlign.Center, 70);
            AddColumn("F_LOTNO", "Lot No.", "", HorizontalAlign.Center, 150);

            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STDT", GetFromDt());
                oParamDic.Add("F_EDDT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_LOTNO", txtLOTNO.Text);

                ds = biz.ADTR0405_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else if (!bExistsDataSet(ds))
            {
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = "조회된 데이터가 없습니다.";
            }
            else
            {
                DataTable dtGroup = ds.Tables[0];
                DataTable dtDatas = ds.Tables[1];
                DataTable dtColms = ds.Tables[2];

                // 바인딩용 데이터테이블 생성
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("F_WORKDATE", typeof(string));
                dtTemp.Columns.Add("F_WORKTIME", typeof(string));
                dtTemp.Columns.Add("F_TSERIALNO", typeof(string));
                dtTemp.Columns.Add("F_NUMBER", typeof(string));
                dtTemp.Columns.Add("F_LOTNO", typeof(string));

                // 항목컬럼
                foreach (DataRow dr in dtColms.Rows)
                {
                    AddColumn(String.Format("{0}|DATA", dr["F_COLCD"]), dr["F_COLNM"].ToString(), dr["F_COLCAPTION"].ToString(), HorizontalAlign.Right, 120);
                    AddColumn(String.Format("{0}|JUDGE", dr["F_COLCD"]), "", "", HorizontalAlign.Right, 0, false);
                    dtTemp.Columns.Add(String.Format("{0}|DATA", dr["F_COLCD"]), typeof(string));
                    dtTemp.Columns.Add(String.Format("{0}|JUDGE", dr["F_COLCD"]), typeof(string));
                }

                // 그룹
                foreach (DataRow drGroup in dtGroup.Rows)
                {
                    DataRow dtNewRow = dtTemp.NewRow();
                    dtNewRow["F_WORKDATE"] = drGroup["F_WORKDATE"].ToString();
                    dtNewRow["F_WORKTIME"] = drGroup["F_WORKTIME"].ToString();
                    dtNewRow["F_TSERIALNO"] = drGroup["F_TSERIALNO"].ToString();
                    dtNewRow["F_NUMBER"] = drGroup["F_NUMBER"].ToString();
                    dtNewRow["F_LOTNO"] = drGroup["F_LOTNO"].ToString();

                    // 데이터
                    foreach (DataRow drDatas in dtDatas.Select(String.Format("F_WORKDATE='{0}' AND F_WORKTIME='{1}' AND F_TSERIALNO='{2}' AND F_NUMBER='{3}' AND F_LOTNO='{4}'",
                        drGroup["F_WORKDATE"], drGroup["F_WORKTIME"], drGroup["F_TSERIALNO"], drGroup["F_NUMBER"], drGroup["F_LOTNO"])))
                    {
                        dtNewRow[String.Format("{0}|DATA", drDatas["F_COLCD"])] = drDatas["F_MEASURE"].ToString();
                        dtNewRow[String.Format("{0}|JUDGE", drDatas["F_COLCD"])] = drDatas["F_NGOKCHK"].ToString();
                    }

                    dtTemp.Rows.Add(dtNewRow);
                }

                devGrid.DataSource = dtTemp;
                devGrid.DataBind();
            }
        }
        #endregion

        #region 컬럼생성
        void AddColumn(string FieldNM, string Caption, string Tooltip, HorizontalAlign align, int width, bool bVisible = true)
        {
            int basicWidth = 14;

            GridViewDataTextColumn column = new GridViewDataTextColumn();
            column.FieldName = FieldNM;
            column.Caption = Caption;
            if (!String.IsNullOrEmpty(Tooltip))
                column.ToolTip = Tooltip;
            column.CellStyle.HorizontalAlign = align;
            if (Caption.Length * basicWidth > width)
                column.Width = (Caption.Length * 14) + 10;
            column.Visible = bVisible;
            devGrid.Columns.Add(column);
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ADTR0405_LST();
        }
        #endregion

        #region devGrid_HtmlDataCellPrepared
        /// <summary>
        /// devGrid_HtmlDataCellPrepared
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewTableDataCellEventArgs</param>
        protected void devGrid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName.Contains("|DATA"))
            {
                if (e.CellValue != null)
                {
                    DevExpress.Web.ASPxGridView grid = sender as DevExpress.Web.ASPxGridView;
                    string sJUDGE = grid.GetRowValues(e.VisibleIndex, e.DataColumn.FieldName.Replace("DATA", "JUDGE")).ToString();

                    switch (sJUDGE)
                    {
                        default:
                            e.Cell.ForeColor = System.Drawing.Color.Black;
                            break;
                        case "1":
                            e.Cell.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "2":
                            e.Cell.ForeColor = System.Drawing.Color.Blue;
                            break;
                    }
                }
            }
            else
                return;
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
            ADTR0405_LST();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 검사그룹별 Data조회", GetItemCD(), hidWORKNM.Text), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
            GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && dataColumn != null && dataColumn.FieldName.Contains("|DATA"))
            {
                switch(e.GetValue(dataColumn.FieldName.Replace("DATA","JUDGE")).ToString())
                {
                    default:
                        e.BrickStyle.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "1":
                        e.BrickStyle.ForeColor = System.Drawing.Color.Red;
                        break;
                    case "2":
                        e.BrickStyle.ForeColor = System.Drawing.Color.Blue;
                        break;
                }
            }
        }
        #endregion

        #endregion
    }
}