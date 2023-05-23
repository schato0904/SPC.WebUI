using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.MNTR.Biz;
using DevExpress.XtraCharts;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.MNTR
{
    public partial class MNTR0906 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
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
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                pnlChart.JSProperties["cpResultCode"] = "";
                pnlChart.JSProperties["cpResultMsg"] = "";

            }

            //ddlRefreshMinute.Attributes.Add("onchange", "fn_ResetTimer();");
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
            string sStartDate = CommonHelper.GetAppSectionsString("startDate");
            string sEndDate = CommonHelper.GetAppSectionsString("endDate");
            DateTime dtStart, dtEnd;

            bool ConvertSTDT = DateTime.TryParse(sStartDate, out dtStart);
            bool ConvertEDDT = DateTime.TryParse(sEndDate, out dtEnd);

            dtStart = !ConvertSTDT || !ConvertEDDT ? DateTime.Now : dtStart;
            dtEnd = !ConvertSTDT || !ConvertEDDT ? DateTime.Now : dtEnd;

            txtDATE.Date = dtStart;
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

        #region 모니터링 > 생산량조회
        public DataSet MNTR0906_LST(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRBiz biz = new MNTRBiz())
            {
                int Time = Convert.ToDateTime(this.txtDATE.Text).Hour;
                string Date = this.txtDATE.Text; 
                if (Time < 8)
                {
                    Date = Convert.ToDateTime(this.txtDATE.Text).AddDays(-1).ToString();
                }
                
                oParamDic.Clear();
                oParamDic.Add("S_COMPCD", gsCOMPCD);
                oParamDic.Add("S_FACTCD", gsFACTCD);
                oParamDic.Add("F_DATE", Date);
                oParamDic.Add("F_DATE2", this.txtDATE.Text);
                //oParamDic.Add("F_DATE", "2015-01-08");
                //oParamDic.Add("F_DATE2", "2015-01-08");
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.MNTR0906_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion
        
        #endregion

        #region 사용자이벤트
        
        protected void pnlChart_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string errMsg = String.Empty;

            DataSet ds = MNTR0906_LST(out errMsg);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlChart.JSProperties["cpResultCode"] = "0";
                pnlChart.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSetWhitoutCount(ds))
                {
                    // Grid Callback Init
                    pnlChart.JSProperties["cpResultCode"] = "0";
                    pnlChart.JSProperties["cpResultMsg"] = "데이타 조회 중 장애가 발생하였습니다.\r계속해서 장애가 발생하는 경우 관리자에게 문의 바랍니다.";
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        // Grid Callback Init
                        pnlChart.JSProperties["cpResultCode"] = "0";
                        pnlChart.JSProperties["cpResultMsg"] = "당일 가동된 전수라인이 없습니다.";
                    }
                    else
                        SetGrid(ds);
                }
            }
        }

        protected void SetGrid(DataSet dsGrid)
        {
            DataView dv1, dv2;

            int cnt = dsGrid.Tables[0].Rows.Count;
            int dicnt = Convert.ToInt32(cnt / 2);

            dv1 = new DataView(dsGrid.Tables[0], string.Format("F_NUMBER <= {0}", dicnt), "F_NUMBER", DataViewRowState.CurrentRows);
            dv2 = new DataView(dsGrid.Tables[0], string.Format("F_NUMBER > {0}", dicnt), "F_NUMBER", DataViewRowState.CurrentRows);

            this.txtTABLE1.Text = "<tbody style='max-height: 600px; overflow: auto;'>";
            this.txtTABLE2.Text = "<tbody style='max-height: 600px; overflow: auto;'>";

            foreach (DataRowView drv in dv1)
            {
                this.txtTABLE1.Text += string.Format("<tr id='tr2' style='height: 40px; color:{0}'>", drv["F_COLOR"]);
                this.txtTABLE1.Text += string.Format("<td style='color: white; '>{0}</td>", drv["F_COMPNM"]);
                this.txtTABLE1.Text += string.Format("<td style='color: white; '>{0}</td>", drv["F_LINENM"]);
                this.txtTABLE1.Text += string.Format("<td style='color: white; '>{0}</td>", drv["F_ITEMCD"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: center;'><img src='../../Resources/images/{0}.gif' /></td>", drv["F_COLOR"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATEGOODQTY"]);
                //this.txtTABLE1.Text += string.Format("<td style='text-align: right;cursor:pointer' onclick=fn_tdClick('{1}','{2}','{3}','{4}')>{0:#,##0}</td>", drv["F_LATEREJQTY"], drv["F_COMPCD"], drv["F_FACTCD"], drv["F_LINECD"], drv["F_ITEMCD"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATEREJQTY"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATESHARTCNT"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0.#0}</td>", drv["F_LATERATE"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_GOODQTY"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_REJQTY"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_SHARTCNT"]);
                this.txtTABLE1.Text += string.Format("<td style='text-align: right;'>{0:#,##0.#0}</td>", drv["F_RATE"]);
                this.txtTABLE1.Text += string.Format("</tr>");
            }

            foreach (DataRowView drv in dv2)
            {
                this.txtTABLE2.Text += string.Format("<tr id='tr2' style='height: 40px; color:{0}'>", drv["F_COLOR"]);
                this.txtTABLE2.Text += string.Format("<td style=' color: white; '>{0}</td>", drv["F_COMPNM"]);
                this.txtTABLE2.Text += string.Format("<td style='color: white; '>{0}</td>", drv["F_LINENM"]);
                this.txtTABLE2.Text += string.Format("<td style='color: white; '>{0}</td>", drv["F_ITEMCD"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: center;'><img src='../../Resources/images/{0}.gif' /></td>", drv["F_COLOR"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATEGOODQTY"]);
                //this.txtTABLE2.Text += string.Format("<td style='text-align: right;cursor:pointer' onclick=fn_tdClick('{1}','{2}','{3}','{4}')>{0:#,##0}</td>", drv["F_LATEREJQTY"], drv["F_COMPCD"], drv["F_FACTCD"], drv["F_LINECD"], drv["F_ITEMCD"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATEREJQTY"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_LATESHARTCNT"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0.#0}</td>", drv["F_LATERATE"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_GOODQTY"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_REJQTY"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0}</td>", drv["F_SHARTCNT"]);
                this.txtTABLE2.Text += string.Format("<td style='text-align: right;'>{0:#,##0.#0}</td>", drv["F_RATE"]);
                this.txtTABLE2.Text += string.Format("</tr>");
            }

            this.txtTABLE1.Text += "</tbody>";
            this.txtTABLE2.Text += "</tbody>";
        }

        #endregion
    }
}