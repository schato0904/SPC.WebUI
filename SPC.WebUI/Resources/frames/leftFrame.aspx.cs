using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.Common.Biz;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.frames
{
    public partial class leftFrame : WebUIBasePage
    {
        #region 변수설정
        protected string m_sTopMenuCD = String.Empty;
        protected string m_sTopMenuNM = String.Empty;
        protected string m_sLastJoinInfo = String.Empty;
        DataTable dtTable = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            m_sTopMenuCD = GlobalFunction.GetParam("topMenuCD");
            m_sTopMenuNM = GetCommonCodeText(m_sTopMenuCD);

            if (!IsPostBack && !m_sTopMenuCD.Equals(""))
            {
                // 메뉴중분류를 구한다
                BindMidMenu();
            }

            // 사이트별 즐겨찾기를 구한다
            SetFavoritesUserControl();

            if (Session["LASTJOIN"] == null || Session["IPADDRESS"] == null)
                SYUSRLOGINLOG_GET();

            if (Session["LASTJOIN"] != null && Session["IPADDRESS"] != null)
            {
                if (!String.IsNullOrEmpty(Session["LASTJOIN"].ToString()) && !String.IsNullOrEmpty(Session["IPADDRESS"].ToString()))
                    m_sLastJoinInfo = Session["LASTJOIN"].Equals("최초접속") ? "Last access information<br />최초접속" : String.Format("Last access information<br />{0} (IP:{1})", Session["LASTJOIN"], Session["IPADDRESS"]);
                else
                    m_sLastJoinInfo = String.Empty;
            }
        }

        #region Grouping DataTable
        private DataTable GetGroupedBy(DataTable dt, string columnNamesInDt, string groupByColumnNames, string typeOfCalculation)
        {
            //Return its own if the column names are empty
            if (columnNamesInDt == string.Empty || groupByColumnNames == string.Empty)
            {
                return dt;
            }

            //Once the columns are added find the distinct rows and group it bu the numbet
            DataTable _dt = dt.DefaultView.ToTable(true, groupByColumnNames.Split(','));

            //The column names in data table
            string[] _columnNamesInDt = columnNamesInDt.Split(',');

            for (int i = 0; i < _columnNamesInDt.Length; i++)
            {
                if (!groupByColumnNames.Contains(_columnNamesInDt[i]))
                {
                    _dt.Columns.Add(_columnNamesInDt[i]);
                }
            }

            //Gets the collection and send it back
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                for (int j = 0; j < _columnNamesInDt.Length; j++)
                {
                    if (!groupByColumnNames.Contains(_columnNamesInDt[j]))
                    {
                        _dt.Rows[i][j] = dt.Compute(String.Format("{0}({1})", typeOfCalculation, _columnNamesInDt[j]), String.Format("{0} = '{1}'", groupByColumnNames.Split(',')[0], _dt.Rows[i][groupByColumnNames.Split(',')[0]]));
                    }
                }
            }

            return _dt;
        }
        #endregion

        #region 메뉴중분류를 구한다
        void BindMidMenu()
        {
            string errMsg = String.Empty;
            DataSet ds = null;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_GROUPCD", gsGROUPCD);
                oParamDic.Add("F_MODULE1", m_sTopMenuCD);
                oParamDic.Add("F_ISADMIN", gsGROUPCD.Equals("AAC601") ? "1" : "0");
                oParamDic.Add("F_ISDEV", gsDEV);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MENUM_LST(oParamDic, out errMsg);
            }

            dtTable = ds.Tables[0];
            DataTable dtMidMenu = new DataTable();
            dtMidMenu.Columns.Add("F_MODULECD", typeof(string));
            dtMidMenu.Columns.Add("F_MODULENM", typeof(string));
            dtMidMenu.Columns.Add("F_SUBCOUNT", typeof(int));
            foreach (DataRow dtRow in dtTable.Rows)
            {
                dtMidMenu.Rows.Add(dtRow["F_MODULE2"], dtRow["F_MODULE2NM"], 0);
            }

            rpt_Menu1.DataSource = CTF.Web.Framework.Helper.StaticFunctions.staticData.GetGroupedBy(dtMidMenu, "F_MODULECD,F_MODULENM,F_SUBCOUNT", "F_MODULECD,F_MODULENM", "Count");
            rpt_Menu1.DataBind();
        }
        #endregion

        #region 중메뉴 Tag
        string GetMidMenuTag(string code, string text, int subCount)
        {
            string tag = "";
            tag += "<a href=\"#\" class=\"auto\" onclick=\"doSetMidMenu('{0}',encodeURIComponent('{1}'));\">";
            tag += "<span class=\"pull-right text-muted\">";
            tag += "<i class=\"i i-circle-sm-o text\"></i>";
            tag += "<i class=\"i i-circle-sm text-active\"></i>";
            tag += "</span>";
            tag += "<b class=\"badge bg-danger pull-right\">{2}</b>";
            tag += "<i class=\"i i-folder-plus icon\"></i>";
            tag += "<span class=\"font-bold\">{1}</span>";
            tag += "</a>";

            System.Text.StringBuilder sb_tag = new System.Text.StringBuilder();
            sb_tag.AppendFormat(tag, code, text, subCount);

            return sb_tag.ToString();
        }
        #endregion

        #region 소메뉴 Tag
        string GetSmallMenuTag(string F_MODULECD, string F_MODULENM, string F_PGMID, string F_PGMNM, string F_LINK, string F_TOOLBAR, string F_AUTHORITYCD)
        {
            string tag = "";
            tag += "<li>";
            tag += "<a href=\"javascript:doCreateTab('{0}|{1}|{2}|{3}|{4}|{5}|{6}');\" class=\"auto\">";
            tag += "<i class=\"i i-dot\"></i>";
            tag += "<span>{4}</span>";
            tag += "</a>";
            tag += "</li>";

            System.Text.StringBuilder sb_tag = new System.Text.StringBuilder();
            sb_tag.AppendFormat(tag,
                Request.QueryString.Get("topMenuCD"),
                F_MODULECD,
                F_LINK,
                F_PGMID,
                F_PGMNM,
                F_TOOLBAR,
                F_AUTHORITYCD);

            return sb_tag.ToString();
        }
        #endregion

        #region 사이트별 즐겨찾기를 구한다
        void SetFavoritesUserControl()
        {
            Control userControl = null;

            if (String.IsNullOrEmpty(gsLOGINPGMID))
            {
                userControl = Page.LoadControl("~/Resources/controls/login/favorites.ascx");
                userControl.ID = "favoritesControl";
            }
            else
            {
                if (System.IO.File.Exists(Server.MapPath(String.Format("~/Resources/controls/login/{0}/favorites.ascx", gsLOGINPGMID))))
                    userControl = Page.LoadControl(String.Format("~/Resources/controls/login/{0}/favorites.ascx", gsLOGINPGMID));
                else
                    userControl = Page.LoadControl("~/Resources/controls/login/favorites.ascx");
                userControl.ID = "favoritesControl";
            }

            pHolderFavorites.Controls.Add(userControl);
        }
        #endregion

        #region 최종접속정보를 구한다
        void SYUSRLOGINLOG_GET()
        {
            DataSet ds = null;
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_USERID", gsUSERID.Equals("cyber") ? String.Concat(gsUSERID, gsCOMPCD) : gsUSERID);
                oParamDic.Add("F_TARGET", gsCOMPCD);

                ds = biz.SYUSRLOGINLOG_GET(oParamDic, out errMsg);

                if (bExistsDataSet(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Session["LASTJOIN"] = dr["F_WORKDT"].ToString();
                        Session["IPADDRESS"] = dr["F_IPADDR"].ToString();
                    }
                }
            }
        }
        #endregion

        #region 이벤트

        #region 중메뉴 ItemDataBound
        protected void rpt_Menu1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpt_Menu2 = e.Item.FindControl("rpt_Menu2") as Repeater;

                Literal literal = e.Item.FindControl("literal1") as Literal;
                literal.Text = GetMidMenuTag(DataBinder.Eval(e.Item.DataItem, "F_MODULECD").ToString(), DataBinder.Eval(e.Item.DataItem, "F_MODULENM").ToString(), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "F_SUBCOUNT").ToString()));

                DataTable dtClone = dtTable.Clone();
                foreach (DataRow dtRow in dtTable.Select(String.Format("F_MODULE2='{0}'", DataBinder.Eval(e.Item.DataItem, "F_MODULECD"))))
                {
                    dtClone.ImportRow(dtRow);
                }
                rpt_Menu2.ItemDataBound += rpt_Menu2_ItemDataBound;
                rpt_Menu2.DataSource = dtClone;
                rpt_Menu2.DataBind();
            }
        }
        #endregion

        #region 소메뉴 ItemDataBound
        void rpt_Menu2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal literal = e.Item.FindControl("literal2") as Literal;
                literal.Text = GetSmallMenuTag(
                    DataBinder.Eval(e.Item.DataItem, "F_MODULE2").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_MODULE2NM").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_PGMID").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_PGMNM").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_LINK").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_TOOLBAR").ToString(),
                    DataBinder.Eval(e.Item.DataItem, "F_AUTHORITYCD").ToString());
            }
        }
        #endregion

        #endregion
    }
}