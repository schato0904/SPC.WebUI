using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ADTR.Biz;
using System.Text;
using System.Xml;
using System.Web.Script.Services;


namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0101 : WebUIBasePage
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
        public DataSet ADTR0101_LST(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRBiz biz = new ADTRBiz())
            {
                DateTime dtNow = DateTime.Now;
                int Time = dtNow.Hour;

                string strFromdt = dtNow.ToString("yyyy-MM-dd");
                string strTodt = dtNow.AddDays(1).ToString("yyyy-MM-dd");
                if (Time < 8)
                {
                    strFromdt = dtNow.AddDays(-1).ToString("yyyy-MM-dd");
                    strTodt = dtNow.ToString("yyyy-MM-dd");
                }

                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", strFromdt);
                oParamDic.Add("F_TODT", strTodt);
                ds = biz.ADTR0101_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        protected void pnlChart_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string errMsg = String.Empty;

            DataSet ds = ADTR0101_LST(out errMsg);

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
                        pnlChart.JSProperties["cpResultMsg"] = "해당 Data가 없습니다";
                    }
                    else
                    {
                        SetGrid(ds);
                    }
                }
            }
        }

        protected void SetGrid(DataSet dsGrid)
        {
            string[] strHeader = new string[] { "PC명","합격수","규격이탈","관리이탈","불량율","상태" };
            string strTable1 = "";
            string strTable2 = "";
            strTable1 = "<tbody>";
            strTable2 = "<tbody>";

            int columnCnt = 6;//테이블에 표시할 PC수량
            int maxCnt = dsGrid.Tables[0].Rows.Count;
            if (maxCnt < columnCnt + 1)
            {
                for (int i = 0; i < dsGrid.Tables[0].Columns.Count; i++)
                {
                    strTable1 += "<tr>";
                    strTable1 += string.Format("<td style='background-color: #336699; color: #ffd800;text-align:center; width:10%; font-size:18pt;font-weight: bold;'>{0}</td>", strHeader[i]);
                    strTable2 += "<tr>";
                    strTable2 += string.Format("<td style='background-color: #336699; color: #ffd800;text-align:center; width:10%; font-size:18pt;font-weight: bold;'>{0}</td>", strHeader[i]);
                    for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
                    {
                        if (i == 4)
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0:#,##0.#0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;'>&nbsp;</td>");
                        }
                        else if (i == 5)
                        {
                            strTable1 += string.Format("<td style='width:10%;text-align:center; cursor:pointer;'  onclick='fn_MonitoringClick(\"{1}\")'><span><img src='../../Resources/images/{0}.gif' /></span></td>", dsGrid.Tables[0].Rows[j][5], dsGrid.Tables[0].Rows[j][0]);
                            strTable2 += string.Format("<td style='width:10%;text-align:center'><span>&nbsp;</span></td>");
                        }
                        else if (i == 0)
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:center'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;'>&nbsp;</td>");
                        }
                        else
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;'>&nbsp;</td>");
                        }
                    }
                    strTable1 += "</tr>";
                    strTable2 += "</tr>";
                }
                strTable1 += "</tbody>";
                strTable2 += "</tbody>";
            }
            else
            {
                for (int i = 0; i < dsGrid.Tables[0].Columns.Count; i++)
                {
                    strTable1 += "<tr>";
                    strTable1 += string.Format("<td style='background-color: #336699; color: #ffd800;text-align:center; width:10%; font-size:18pt;font-weight: bold;'>{0}</td>", strHeader[i]);
                    for (int j = 0; j < columnCnt; j++)
                    {
                        if (i == 4)
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0:#,##0.#0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                        else if (i == 5)
                        {
                            strTable1 += string.Format("<td style='width:10%;text-align:center; cursor:pointer;'  onclick='fn_MonitoringClick(\"{1}\")'><span><img src='../../Resources/images/{0}.gif' /></span></td>", dsGrid.Tables[0].Rows[j][5], dsGrid.Tables[0].Rows[j][0]);
                        }
                        else if (i == 0)
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:center'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                        else
                        {
                            strTable1 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                    }
                    strTable1 += "</tr>";
                }
                strTable1 += "</tbody>";

                for (int i = 0; i < dsGrid.Tables[0].Columns.Count; i++)
                {
                    strTable2 += "<tr>";
                    strTable2 += string.Format("<td style='background-color: #336699; color: #ffd800;text-align:center; width:10%; font-size:18pt;font-weight: bold;'>{0}</td>", strHeader[i]);
                    for (int j = columnCnt; j < maxCnt; j++)
                    {
                        if (i == 4)
                        {
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0:#,##0.#0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                        else if (i == 5)
                        {
                            strTable2 += string.Format("<td style='width:10%;text-align:center; cursor:pointer;'  onclick='fn_MonitoringClick(\"{1}\")'><span><img src='../../Resources/images/{0}.gif' /></span></td>", dsGrid.Tables[0].Rows[j][5], dsGrid.Tables[0].Rows[j][0]);
                        }
                        else if (i == 0)
                        {
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:center'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                        else
                        {
                            strTable2 += string.Format("<td style='width:10%;padding-left:5px;font-size:18pt; font-weight:bold;color:{1}; text-align:right'>{0}</td>", dsGrid.Tables[0].Rows[j][i], dsGrid.Tables[0].Rows[j][5]);
                        }
                    }
                    if (maxCnt <= (columnCnt*2))
                    {
                        for (int x = 0; x < (columnCnt*2) - maxCnt; x++)
                        {
                            strTable2 += string.Format("<td style='width:10%;'>&nbsp;</td>");   
                        }                        
                    }
                    strTable2 += "</tr>";
                }
                strTable2 += "</tbody>";
            }

            

            txtTABLE1.Text = strTable1;
            txtTABLE2.Text = strTable2;
        }

        #endregion
    }
}