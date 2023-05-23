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
    public partial class MNTR0901 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string sPopup = String.Empty;
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
        {
            sPopup = Request.QueryString.Get("bPopup") ?? "false";
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

        #region 모니터링 > 협력사 SPC 현황
        public DataSet MONITORING_MNTR0901(string[] sParam, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("F_DATETP", sParam[0]);
                oParamDic.Add("F_DATEBT", sParam[1]);
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MONITORING_MNTR0901(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상태별 Bullet Icon 구하기
        string GetBulletICon(Int32 ALLCNT, Int32 OCCNT, Int32 NGCNT)
        {
            if (NGCNT > 0)
                return "animate_bullet_red02.gif";
            else if (OCCNT > 0)
                return "animate_bullet_blue02.gif";
            else if (ALLCNT > 0)
                return "animate_bullet_green02.gif";
            else
                return "gray02.gif";
        }
        #endregion

        #region DataRow Insert
        void AppendDataRow(DataRow dtNewRow, DataRow[] drTemp, bool bUsed, string typeIndex)
        {
            Int32 OKCNT = 0;
            Int32 OCCNT = 0;
            Int32 NGCNT = 0;
            Int32 ALLCNT = 0;

            if (bUsed)
            {
                if (drTemp.Length > 0)
                {
                    foreach (DataRow dtRow in drTemp)
                    {
                        OKCNT = Convert.ToInt32(dtRow["OK_CNT"]);
                        OCCNT = Convert.ToInt32(dtRow["OC_CNT"]);
                        NGCNT = Convert.ToInt32(dtRow["NG_CNT"]);
                        ALLCNT = OKCNT + OCCNT + NGCNT;

                        dtNewRow[String.Format("F_STATUS{0}", typeIndex)] = String.Format("<img src=\"{0}\" />", Page.ResolveUrl(String.Format("~/Resources/images/{0}", GetBulletICon(ALLCNT, OCCNT, NGCNT))));
                        dtNewRow[String.Format("F_OKCNT{0}", typeIndex)] = OKCNT.ToString("#,##0");
                        dtNewRow[String.Format("F_OCCNT{0}", typeIndex)] = OCCNT.ToString("#,##0");
                        dtNewRow[String.Format("F_NGCNT{0}", typeIndex)] = NGCNT.ToString("#,##0");
                        dtNewRow[String.Format("F_ALLCNT{0}", typeIndex)] = ALLCNT.ToString("#,##0");
                    }
                }
                else
                {
                    OKCNT = Convert.ToInt32(0);
                    OCCNT = Convert.ToInt32(0);
                    NGCNT = Convert.ToInt32(0);
                    ALLCNT = OKCNT + OCCNT + NGCNT;

                    dtNewRow[String.Format("F_STATUS{0}", typeIndex)] = String.Format("<img src=\"{0}\" />", Page.ResolveUrl("~/Resources/images/gray02.gif"));
                    dtNewRow[String.Format("F_OKCNT{0}", typeIndex)] = OKCNT.ToString("#,##0");
                    dtNewRow[String.Format("F_OCCNT{0}", typeIndex)] = OCCNT.ToString("#,##0");
                    dtNewRow[String.Format("F_NGCNT{0}", typeIndex)] = NGCNT.ToString("#,##0");
                    dtNewRow[String.Format("F_ALLCNT{0}", typeIndex)] = ALLCNT.ToString("#,##0");
                }
            }
            else
            {
                OKCNT = Convert.ToInt32(0);
                OCCNT = Convert.ToInt32(0);
                NGCNT = Convert.ToInt32(0);
                ALLCNT = OKCNT + OCCNT + NGCNT;

                bool bNotUsedShow = chkNotUsedShow.Checked;

                dtNewRow[String.Format("F_STATUS{0}", typeIndex)] = !bNotUsedShow ? "" : String.Format("<img src=\"{0}\" />", Page.ResolveUrl("~/Resources/images/black02.gif"));
                dtNewRow[String.Format("F_OKCNT{0}", typeIndex)] = !bNotUsedShow ? "" : "N/A";
                dtNewRow[String.Format("F_OCCNT{0}", typeIndex)] = !bNotUsedShow ? "" : "N/A";
                dtNewRow[String.Format("F_NGCNT{0}", typeIndex)] = !bNotUsedShow ? "" : "N/A";
                dtNewRow[String.Format("F_ALLCNT{0}", typeIndex)] = !bNotUsedShow ? "" : "N/A";
            }
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

            DataSet ds = MONITORING_MNTR0901(e.Parameters.Split('|'), out errMsg);

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
                    DataTable dtList = new DataTable();
                    dtList.Columns.Add("F_COMPCD", typeof(String));
                    dtList.Columns.Add("F_COMPNM", typeof(String));
                    dtList.Columns.Add("F_FACTCD", typeof(String));
                    dtList.Columns.Add("F_STATUS1", typeof(String));
                    dtList.Columns.Add("F_ALLCNT1", typeof(String));
                    dtList.Columns.Add("F_OKCNT1", typeof(String));
                    dtList.Columns.Add("F_OCCNT1", typeof(String));
                    dtList.Columns.Add("F_NGCNT1", typeof(String));
                    dtList.Columns.Add("F_STATUS2", typeof(String));
                    dtList.Columns.Add("F_ALLCNT2", typeof(String));
                    dtList.Columns.Add("F_OKCNT2", typeof(String));
                    dtList.Columns.Add("F_OCCNT2", typeof(String));
                    dtList.Columns.Add("F_NGCNT2", typeof(String));
                    dtList.Columns.Add("F_STATUS3", typeof(String));
                    dtList.Columns.Add("F_ALLCNT3", typeof(String));
                    dtList.Columns.Add("F_OKCNT3", typeof(String));
                    dtList.Columns.Add("F_OCCNT3", typeof(String));
                    dtList.Columns.Add("F_NGCNT3", typeof(String));
                    dtList.Columns.Add("F_STATUS4", typeof(String));
                    dtList.Columns.Add("F_ALLCNT4", typeof(String));
                    dtList.Columns.Add("F_OKCNT4", typeof(String));
                    dtList.Columns.Add("F_OCCNT4", typeof(String));
                    dtList.Columns.Add("F_NGCNT4", typeof(String));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dtNewRow = dtList.NewRow();
                        dtNewRow["F_COMPCD"] = dr["F_COMPCD"];
                        dtNewRow["F_COMPNM"] = dr["F_COMPNM"];
                        dtNewRow["F_FACTCD"] = "01";

                        // 자주
                        AppendDataRow(dtNewRow, ds.Tables[1].Select(String.Format("F_COMPCD='{0}' AND F_MACHGUBUN='1'", dr["F_COMPCD"])), Boolean.Parse(dr["자주"].ToString()), "1");
                        // 3차원
                        AppendDataRow(dtNewRow, ds.Tables[1].Select(String.Format("F_COMPCD='{0}' AND F_MACHGUBUN='4'", dr["F_COMPCD"])), Boolean.Parse(dr["3차원"].ToString()), "2");
                        // 치형
                        AppendDataRow(dtNewRow, ds.Tables[1].Select(String.Format("F_COMPCD='{0}' AND F_MACHGUBUN='5'", dr["F_COMPCD"])), Boolean.Parse(dr["치형"].ToString()), "3");
                        // 전수
                        AppendDataRow(dtNewRow, ds.Tables[1].Select(String.Format("F_COMPCD='{0}' AND F_MACHGUBUN='3'", dr["F_COMPCD"])), Boolean.Parse(dr["전수"].ToString()), "4");

                        dtList.Rows.Add(dtNewRow);
                    }

                    devGrid.DataSource = dtList;
                    devGrid.DataBind();
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
            if (!e.Column.FieldName.Contains("F_STATUS")) return;

            e.EncodeHtml = false;
        }
        #endregion

        #endregion
    }
}