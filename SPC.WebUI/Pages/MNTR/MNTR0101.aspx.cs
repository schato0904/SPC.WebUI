using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0101 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dtGrid1
        {
            get
            {
                return (DataTable)Session["MNTR0101"];
            }
            set
            {
                Session["MNTR0101"] = value;
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

        #region 모니터링 > 생산량조회
        public DataSet MONITORING_PRODUCTION_VOLUMN(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("T_COMPCD", GetCompCD());
                oParamDic.Add("T_FACTCD", GetFactCD());
                oParamDic.Add("F_DATE", GetFromDt());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_PRODUCTION_VOLUMN(oParamDic, out errMsg);
            }

            return ds;
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
            string errMsg = String.Empty;

            DataSet ds = MONITORING_PRODUCTION_VOLUMN(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                {
                    // Grid Callback Init
                    devGrid.JSProperties["cpResultCode"] = "0";
                    devGrid.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    DataRow[] drInsp1, drInsp2;

                    DataTable dtList = new DataTable();
                    dtList.Columns.Add("F_COMPCD", typeof(String));
                    dtList.Columns.Add("F_COMPNM", typeof(String));
                    dtList.Columns.Add("F_FACTCD", typeof(String));
                    dtList.Columns.Add("ALL_CNT1", typeof(String));
                    dtList.Columns.Add("OK_CNT1", typeof(String));
                    dtList.Columns.Add("OC_CNT1", typeof(String));
                    dtList.Columns.Add("NG_CNT1", typeof(String));
                    dtList.Columns.Add("LAST_TIME1", typeof(String));
                    dtList.Columns.Add("ALL_CNT2", typeof(String));
                    dtList.Columns.Add("OK_CNT2", typeof(String));
                    dtList.Columns.Add("OC_CNT2", typeof(String));
                    dtList.Columns.Add("NG_CNT2", typeof(String));
                    dtList.Columns.Add("LAST_TIME2", typeof(String));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dtNewRow = dtList.NewRow();
                        dtNewRow["F_COMPCD"] = dr["F_COMPCD"];
                        dtNewRow["F_COMPNM"] = dr["F_COMPNM"];
                        dtNewRow["F_FACTCD"] = "01";

                        // 자주
                        drInsp1 = ds.Tables[1].Select(String.Format("F_COMPCD='{0}'", dr["F_COMPCD"]));
                        if (drInsp1.Length > 0)
                        {
                            foreach (DataRow dr1 in drInsp1)
                            {
                                dtNewRow["LAST_TIME1"] = Convert.ToDateTime(dr1["LAST_TIME"]).ToString("HH시 mm분 ss초");
                                dtNewRow["OK_CNT1"] = Convert.ToInt32(dr1["OK_CNT"]).ToString("#,##0");
                                dtNewRow["OC_CNT1"] = Convert.ToInt32(dr1["OC_CNT"]).ToString("#,##0");
                                dtNewRow["NG_CNT1"] = Convert.ToInt32(dr1["NG_CNT"]).ToString("#,##0");
                                dtNewRow["ALL_CNT1"] = Convert.ToInt32(Convert.ToInt32(dr1["OK_CNT"]) + Convert.ToInt32(dr1["OC_CNT"]) + Convert.ToInt32(dr1["NG_CNT"])).ToString("#,##0");
                            }
                        }
                        else
                        {
                            dtNewRow["LAST_TIME1"] = "";
                            dtNewRow["OK_CNT1"] = "0";
                            dtNewRow["OC_CNT1"] = "0";
                            dtNewRow["NG_CNT1"] = "0";
                            dtNewRow["ALL_CNT1"] = "0";
                        }

                        // 전수
                        drInsp2 = ds.Tables[2].Select(String.Format("F_COMPCD='{0}'", dr["F_COMPCD"]));
                        if (drInsp2.Length > 0)
                        {
                            foreach (DataRow dr2 in drInsp2)
                            {
                                dtNewRow["LAST_TIME2"] = Convert.ToDateTime(dr2["LAST_TIME"]).ToString("HH시 mm분 ss초");
                                dtNewRow["OK_CNT2"] = Convert.ToInt32(dr2["OK_CNT"]).ToString("#,##0");
                                dtNewRow["OC_CNT2"] = Convert.ToInt32(dr2["OC_CNT"]).ToString("#,##0");
                                dtNewRow["NG_CNT2"] = Convert.ToInt32(dr2["NG_CNT"]).ToString("#,##0");
                                dtNewRow["ALL_CNT2"] = Convert.ToInt32(Convert.ToInt32(dr2["OK_CNT"]) + Convert.ToInt32(dr2["OC_CNT"]) + Convert.ToInt32(dr2["NG_CNT"])).ToString("#,##0");
                            }
                        }
                        else
                        {
                            dtNewRow["LAST_TIME2"] = "";
                            dtNewRow["OK_CNT2"] = "0";
                            dtNewRow["OC_CNT2"] = "0";
                            dtNewRow["NG_CNT2"] = "0";
                            dtNewRow["ALL_CNT2"] = "0";
                        }

                        dtList.Rows.Add(dtNewRow);
                    }

                    dtGrid1 = dtList;
                    devGrid.DataSource = dtList;
                    devGrid.DataBind();
                }
            }
        }
        #endregion

        #region btn03A_Init
        /// <summary>
        /// btn03A_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btn03A_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnCustomButtonClick('{0}', 'btn03A'); }}", rowVisibleIndex);

        }
        #endregion

        #region btn08A_Init
        /// <summary>
        /// btn08A_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btn08A_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnCustomButtonClick('{0}', 'btn08A'); }}", rowVisibleIndex);
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
            DevExpress.Web.GridViewDataColumn devGridDataColumn = e.Column as DevExpress.Web.GridViewDataColumn;
            if (devGridDataColumn != null && devGridDataColumn.FieldName.Equals("F_STATUS") && e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                e.Text = e.Text.Replace(@"<span style='color:red;'>중단</span>", "중단").Replace(@"<span style='color:blue;'>사용</span>", "사용");
            }
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
            //QWK04A_ADTR0103_LST();
            devGrid.DataSource = dtGrid1;
            devGrid.DataBind();
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 생산량조회정보", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}