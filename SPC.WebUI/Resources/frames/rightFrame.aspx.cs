using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;

namespace SPC.WebUI.Resources.frames
{
    public partial class rightFrame : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
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
                devCallbackPanel.JSProperties["cpResultCode"] = "";
                devCallbackPanel.JSProperties["cpResultMsg"] = "";
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

        #region 트리목록을 구한다
        void TREE_LST()
        {
            string errMsg = String.Empty;
            string resultCode = String.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            UserControl _UserControl = Page.FindControl("ucCommonCodeDDL") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMMONCODECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", GetTreeBanCD());
                oParamDic.Add("F_MACHGUBUN", (ddlMACHGUBUN.Value ?? "").ToString());
                oParamDic.Add("F_LANGTP", gsLANGTP);

                ds = biz.TREE_LST(oParamDic, out errMsg);
            }

            ltlContents.Text = RendorTreeByBan(ds.Tables[0]);

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devCallbackPanel.JSProperties["cpResultCode"] = "0";
                devCallbackPanel.JSProperties["cpResultMsg"] = errMsg;
            }
        }
        #endregion

        #region 트리 Rendoring

        #region 반
        string RendorTreeByBan(DataTable dtTable)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string sPrevCode = String.Empty;

            string[] sTag = new string[dtTable.Rows.Count];
            int idx = 0, lastIdx = 0;

            //sb.Append("<ul id=\"tree\" class=\"treeview\">\x0A");

            foreach (DataRow dr in dtTable.Rows)
            {
                if (!dr["F_BANCD"].ToString().Equals(sPrevCode))
                {
                    sTag[idx] = String.Format("<li class=\"expandable\"><div class=\"hitarea expandable-hitarea\"></div><span class=\"folder\"><a href=\"javascript:fn_OnSetupContentBanTree('{0}|{1}');\"><strong>{1}</strong></a></span>\x0A",
                        dr["F_BANCD"],
                        dr["F_BANNM"]);
                    // 라인정보
                    sTag[idx] += RendorTreeByLine(dtTable, String.Format("F_BANCD='{0}'", dr["F_BANCD"]));
                    sTag[idx] += "</li>\x0A";

                    lastIdx = idx;
                }
                else
                {
                    sTag[idx] = String.Empty;
                }

                sPrevCode = dr["F_BANCD"].ToString();

                idx++;
            }

            if (idx > 0)
            {
                sTag[lastIdx] = sTag[lastIdx]
                    .Replace("<div class=\"hitarea expandable-hitarea\">", "<div class=\"hitarea expandable-hitarea lastExpandable-hitarea\">")
                    .Replace("<li class=\"expandable\">", "<li class=\"expandable lastExpandable\">");
                sb.Append(String.Join("", sTag));
            }

            //sb.Append("</ul>\x0A");

            return sb.ToString();
        }
        #endregion

        #region 라인
        string RendorTreeByLine(DataTable dtTable, string filter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string sPrevCode = String.Empty;

            DataRow[] dtRows = dtTable.Select(filter);
            string[] sTag = new string[dtRows.Length];
            int idx = 0, lastIdx = 0;

            if (dtRows.Length > 0)
            {
                sb.Append("<ul style=\"display: none;\">\x0A");

                foreach (DataRow dr in dtRows)
                {
                    if (!dr["F_LINECD"].ToString().Equals(sPrevCode))
                    {
                        sTag[idx] = String.Format("<li class=\"expandable\"><div class=\"hitarea expandable-hitarea\"></div><span class=\"folder\"><a href=\"javascript:fn_OnSetupContentLineTree('{0}|{1}|{2}|{3}');\">{3}</a></span>\x0A",
                            dr["F_BANCD"],
                            dr["F_BANNM"],
                            dr["F_LINECD"],
                            dr["F_LINENM"]);
                        sTag[idx] += RendorTreeByItem(dtTable, String.Format("F_BANCD='{0}' AND F_LINECD='{1}'", dr["F_BANCD"], dr["F_LINECD"]));
                        sTag[idx] += "</li>\x0A";

                        lastIdx = idx;
                    }
                    else
                    {
                        sTag[idx] = String.Empty;
                    }

                    sPrevCode = dr["F_LINECD"].ToString();

                    idx++;
                }

                if (idx > 0)
                {
                    sTag[lastIdx] = sTag[lastIdx]
                        .Replace("<div class=\"hitarea expandable-hitarea\">", "<div class=\"hitarea expandable-hitarea lastExpandable-hitarea\">")
                        .Replace("<li class=\"expandable\">", "<li class=\"expandable lastExpandable\">");
                    sb.Append(String.Join("", sTag));
                }

                sb.Append("</ul>\x0A");
            }

            return sb.ToString();
        }
        #endregion

        #region 품목
        string RendorTreeByItem(DataTable dtTable, string filter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string sPrevCode = String.Empty;

            DataRow[] dtRows = dtTable.Select(filter);
            string[] sTag = new string[dtRows.Length];
            int idx = 0, lastIdx = 0;

            if (dtRows.Length > 0)
            {
                sb.Append("<ul style=\"display: none;\">\x0A");

                foreach (DataRow dr in dtRows)
                {
                    if (!dr["F_ITEMCD"].ToString().Equals(sPrevCode))
                    {
                        sTag[idx] = String.Format("<li class=\"expandable\"><div class=\"hitarea expandable-hitarea\"></div><span class=\"folder\" style=\"white-space:nowrap;\"><a href=\"javascript:fn_OnSetupContentItemTree('{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}');\">{4}<br/>{5}</a></span>\x0A",
                            dr["F_BANCD"],
                            dr["F_BANNM"],
                            dr["F_LINECD"],
                            dr["F_LINENM"],
                            dr["F_ITEMCD"],
                            dr["F_ITEMNM"],
                            dr["F_MODELCD"],
                            dr["F_MODELNM"]);
                        sTag[idx] += RendorTreeByWork(dtTable, String.Format("F_BANCD='{0}' AND F_LINECD='{1}' AND F_ITEMCD='{2}'", dr["F_BANCD"], dr["F_LINECD"], dr["F_ITEMCD"]));
                        sTag[idx] += "</li>\x0A";

                        lastIdx = idx;
                    }
                    else
                    {
                        sTag[idx] = String.Empty;
                    }

                    sPrevCode = dr["F_ITEMCD"].ToString();

                    idx++;
                }

                if (idx > 0)
                {
                    sTag[lastIdx] = sTag[lastIdx]
                        .Replace("<div class=\"hitarea expandable-hitarea\">", "<div class=\"hitarea expandable-hitarea lastExpandable-hitarea\">")
                        .Replace("<li class=\"expandable\">", "<li class=\"expandable lastExpandable\">");
                    sb.Append(String.Join("", sTag));
                }

                sb.Append("</ul>\x0A");
            }

            return sb.ToString();
        }
        #endregion

        #region 공정
        string RendorTreeByWork(DataTable dtTable, string filter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string sPrevCode = String.Empty;

            DataRow[] dtRows = dtTable.Select(filter);
            string[] sTag = new string[dtRows.Length];
            int idx = 0, lastIdx = 0;

            if (dtRows.Length > 0)
            {
                sb.Append("<ul style=\"display: none;\">\x0A");

                foreach (DataRow dr in dtRows)
                {
                    if (!dr["F_WORKCD"].ToString().Equals(sPrevCode))
                    {
                        sTag[idx] = String.Format("<li class=\"expandable\"><div class=\"hitarea expandable-hitarea\"></div><span class=\"folder\" style=\"white-space:nowrap;\"><a href=\"javascript:fn_OnSetupContentWorkTree('{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}');\">{9}</a></span>\x0A",
                            dr["F_BANCD"],
                            dr["F_BANNM"],
                            dr["F_LINECD"],
                            dr["F_LINENM"],
                            dr["F_ITEMCD"],
                            dr["F_ITEMNM"],
                            dr["F_MODELCD"],
                            dr["F_MODELNM"],
                            dr["F_WORKCD"],
                            dr["F_WORKNM"]);
                        sTag[idx] += RendorTreeByInspection(dtTable, String.Format("F_BANCD='{0}' AND F_LINECD='{1}' AND F_ITEMCD='{2}' AND F_WORKCD='{3}'", dr["F_BANCD"], dr["F_LINECD"], dr["F_ITEMCD"], dr["F_WORKCD"]));
                        sTag[idx] += "</li>\x0A";

                        lastIdx = idx;
                    }
                    else
                    {
                        sTag[idx] = String.Empty;
                    }

                    sPrevCode = dr["F_WORKCD"].ToString();

                    idx++;
                }

                if (idx > 0)
                {
                    sTag[lastIdx] = sTag[lastIdx]
                        .Replace("<div class=\"hitarea expandable-hitarea\">", "<div class=\"hitarea expandable-hitarea lastExpandable-hitarea\">")
                        .Replace("<li class=\"expandable\">", "<li class=\"expandable lastExpandable\">");
                    sb.Append(String.Join("", sTag));
                }

                sb.Append("</ul>\x0A");
            }

            return sb.ToString();
        }
        #endregion

        #region 검사항목
        string RendorTreeByInspection(DataTable dtTable, string filter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string sPrevCode = String.Empty;

            DataRow[] dtRows = dtTable.Select(filter);
            string[] sTag = new string[dtRows.Length];
            int idx = 0, lastIdx = 0;

            if (dtRows.Length > 0)
            {
                sb.Append("<ul style=\"display: none;\">\x0A");

                foreach (DataRow dr in dtRows)
                {
                    if (!dr["F_MEAINSPCD"].ToString().Equals(sPrevCode))
                    {
                        sTag[idx] = String.Format("<li><span class=\"file\" style=\"white-space:nowrap;\"><a href=\"javascript:fn_OnSetupContentInspectionTree('{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}');\">{11}</a></span>\x0A",
                            dr["F_BANCD"],
                            dr["F_BANNM"],
                            dr["F_LINECD"],
                            dr["F_LINENM"],
                            dr["F_ITEMCD"],
                            dr["F_ITEMNM"],
                            dr["F_MODELCD"],
                            dr["F_MODELNM"],
                            dr["F_WORKCD"],
                            dr["F_WORKNM"],
                            dr["F_MEAINSPCD"],
                            dr["F_INSPDETAIL"],
                            dr["F_INSPCD"],
                            dr["F_INSPNM"],
                            dr["F_STANDARD"],
                            dr["F_MAX"],
                            dr["F_MIN"],
                            dr["F_UCLX"],
                            dr["F_LCLX"],
                            dr["F_UCLR"],
                            dr["F_SERIALNO"],
                            dr["F_SIRYO"],
                            dr["F_FREEPOINT"]);
                        sTag[idx] += "</li>\x0A";

                        lastIdx = idx;
                    }
                    else
                    {
                        sTag[idx] = String.Empty;
                    }

                    sPrevCode = dr["F_MEAINSPCD"].ToString();

                    idx++;
                }

                if (idx > 0)
                {
                    sTag[lastIdx] = sTag[lastIdx].Replace("<li>", "<li class=\"last\">");
                    sb.Append(String.Join("", sTag));
                }

                sb.Append("</ul>\x0A");
            }

            return sb.ToString();
        }
        #endregion

        #endregion

        #region 반선택값
        public string GetTreeBanCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = this.FindControl("ucBan") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidBANCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallbackPanel_Callback
        /// <summary>
        /// devCallbackPanelPanel_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void devCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            // 트리목록을 구한다
            TREE_LST();
        }
        #endregion

        #endregion
    }
}