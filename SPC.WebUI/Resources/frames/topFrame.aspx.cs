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
    public partial class topFrame : WebUIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string errMsg = String.Empty;
                DataSet ds = null;

                using (CommonBiz biz = new CommonBiz())
                {
                    oParamDic.Clear();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_GROUPCD", gsGROUPCD);
                    oParamDic.Add("F_ISADMIN", gsGROUPCD.Equals("AAC601") ? "1" : "0");
                    oParamDic.Add("F_ISDEV", gsDEV);
                    oParamDic.Add("F_LANGTYPE", gsLANGTP);

                    ds = biz.MENUL_LST(oParamDic, out errMsg);
                }

                rptMenu.DataSource = ds;
                rptMenu.DataBind();
            }
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal literal = e.Item.FindControl("content") as Literal;
                //literal.Text = String.Format("<td class=\"top-menu\"><a href=\"#\" onclick=\"javascript:doSetMenu('{0}');\" style=\"padding:18px 5px;\" class=\"text-nowrap\"><i class=\"fa fa-globe\"></i> {1}</a></td>", DataBinder.Eval(e.Item.DataItem, "F_MODULE1"), DataBinder.Eval(e.Item.DataItem, "F_MODULE1NM"));
                literal.Text = String.Format("<td class=\"top-menu\"><button onclick=\"javascript:doSetMenu('{0}');return false;\" class=\"btn btn-sm btn-primary text-nowrap\" style=\"padding-top:13px;padding-bottom:13px;\"><i class=\"fa fa-ellipsis-v\" style=\"font-size:11pt;\"></i> <span class=\"text\" style=\"font-size:11pt;\">{1}</span></button></td>", DataBinder.Eval(e.Item.DataItem, "F_MODULE1"), DataBinder.Eval(e.Item.DataItem, "F_MODULE1NM"));
            }
        }
    }
}