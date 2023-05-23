using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.FDCK.Biz;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;


namespace SPC.WebUI.Pages.FDCK
{
    public partial class FDCK0110 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        public DataTable CachedData
        {
            get { return Session["CachedData_FDCK0110"] as DataTable; }
            set { Session["CachedData_FDCK0110"] = value; }
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
                // 사용자 정보를 구한다
                //MACH13_LST();
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
            this.CachedData = null;
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

        #region 전체 설비 점검현황 조회
        void QWK_MACH10_LST()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MEASMONTH", GetFromDt());
                oParamDic.Add("F_USER", gsUSERID);

                ds = biz.QWK_MACH10_2_LST(oParamDic, out errMsg);
            }

            ds = this.AutoNumber(ds);
            //devGrid.DataSource = ds;
                        
            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;                
            }
            else
            {
                this.CachedData = ds != null && ds.Tables.Count > 0 ? this.RebuildData(ds.Tables[0]) : null;
            //    if (IsCallback)
            //        devGrid.DataBind();
            }
        }
        #endregion

        #region 데이터 재구성
        DataTable RebuildData(DataTable dt)
        {
            DataTable rdt = new DataTable();
            DateTime t = DateTime.ParseExact(GetFromDt() + "-01", "yyyy-MM-dd", null);
            int endDay = t.AddMonths(1).AddDays(-1).Day;
            rdt.Columns.AddRange(new DataColumn[]{
                new DataColumn("NO", typeof(int)),
                new DataColumn("F_COMPCD", typeof(string)),
                new DataColumn("F_FACTCD", typeof(string)),
                new DataColumn("F_BANCD", typeof(string)),
                new DataColumn("F_BANNM", typeof(string)),
                new DataColumn("F_LINECD", typeof(string)),
                new DataColumn("F_LINENM", typeof(string)),
                new DataColumn("F_MACHCD", typeof(string)),
                new DataColumn("F_MACHNM", typeof(string)),
                new DataColumn("F_INSPTYPECD", typeof(string)),
                new DataColumn("F_INSPTYPENM", typeof(string)),
            });

            // 일자별 컬럼 명명 규칙 : F_FIELD{일자(2자리)}
            for (int i = 1; i <= 31; ++i)
            {
                rdt.Columns.Add(new DataColumn(string.Format("F_FIELD{0:00}", i), typeof(string)));
            }

            DataTable dt1 = new DataView(dt, "", "F_COMPCD, F_FACTCD, F_BANCD, F_LINECD, F_MACHCD, F_INSPTYPECD, F_MEASDATE", DataViewRowState.CurrentRows).ToTable();

            Func<DataTable, DataRow, DataRow> GetRow = (srcTable, targetRow) =>
            {
                DataRow r = null;

                try
                {
                    r = srcTable.AsEnumerable().First(x =>
                        x["F_COMPCD"].ToString() == targetRow["F_COMPCD"].ToString()
                        && x["F_FACTCD"].ToString() == targetRow["F_FACTCD"].ToString()
                        && x["F_MACHCD"].ToString() == targetRow["F_MACHCD"].ToString()
                        && x["F_INSPTYPECDCD"].ToString() == targetRow["F_INSPTYPECDCD"].ToString());
                }
                catch (Exception)
                {
                    r = null;
                }

                return r;
            };

            DataRow ndr = null;
            bool isNewRow = false;
            string fName = string.Empty;
            string strOK = "O";
            foreach (DataRow dr in dt1.Rows)
            {
                ndr = GetRow(rdt, dr);
                // 없을 경우, 새 Row 생성
                if (ndr == null)
                {
                    ndr = rdt.NewRow();
                    isNewRow = true;
                    // 기본값 설정
                    foreach (DataColumn c in dt1.Columns)
                    {
                        if (rdt.Columns.Contains(c.ColumnName))
                        {
                            ndr[c.ColumnName] = dr[c.ColumnName];
                        }
                    }
                }

                fName = string.Format("F_FIELD{0}", dr["F_MEASDATE"].ToString().Substring(8, 2));

                if (rdt.Columns.Contains(fName))
                {
                    ndr[fName] = strOK;
                }

                if (isNewRow) rdt.Rows.Add(ndr);
            }

            return rdt;
        }
        #endregion

        #region 데이터셋 자동 넘버링
        DataSet AutoNumber(DataSet ds, string NumberColumnName = "NO")
        {
            DataSet returnDs = new DataSet();
            DataTable dt = null;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //dt = ds.Tables[i];
                dt = new DataTable();
                dt.Columns.Add(NumberColumnName, typeof(long));
                dt.Columns[NumberColumnName].AutoIncrement = true;
                dt.Columns[NumberColumnName].AutoIncrementSeed = 1;
                dt.Columns[NumberColumnName].AutoIncrementStep = 1;
                dt.Merge(ds.Tables[i]);
                returnDs.Tables.Add(dt);
            }

            return returnDs;
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
            QWK_MACH10_LST();
            this.devGrid.DataBind();
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
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비점검현황", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
            if ((e.Column as DevExpress.Web.GridViewDataColumn).FieldName == "F_USEYN") //|| e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #region devGrid_DataBound
        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            DateTime t = DateTime.ParseExact(GetFromDt() + "-01", "yyyy-MM-dd", null);
            int endDay = t.AddMonths(1).AddDays(-1).Day;
            string fName = string.Empty;

            // 조회월의 날짜에 맞게 일자별 visible 처리
            for (int i = 1; i <= 31; ++i)
            {
                fName = string.Format("F_FIELD{0:00}", i);
                if (devGrid.DataColumns.AsEnumerable().Any(x => x.FieldName == fName))
                {
                    devGrid.DataColumns[fName].Visible = (i <= endDay);
                }
            }
        } 
        #endregion

        #region devGaid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        } 
        #endregion

        #endregion

    }
}