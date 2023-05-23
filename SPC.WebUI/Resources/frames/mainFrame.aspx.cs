using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.frames
{
    public partial class mainFrame : System.Web.UI.Page
    {
        #region 프로퍼티
        private int LastIndex
        {
            get
            {
                if (Session["LastIndex"] == null) Session["LastIndex"] = 0;
                return (int)Session["LastIndex"];
            }
            set { Session["LastIndex"] = value; }
        }
        private SortedDictionary<int, string> OpenTabPagesCollection
        {
            get
            {
                if (Session["TabPages"] == null) Session["TabPages"] = new SortedDictionary<int, string>();
                return (SortedDictionary<int, string>)Session["TabPages"];
            }
            set { Session["TabPages"] = value; }
        }
        #endregion

        #region 이벤트
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                //OpenTabPagesCollection.Add(0, "MM00|MM0000|COMM|COMM0101|공지사항|R|0");
                //OpenTabPagesCollection.Add(0, "MM00|MM0000|COMM|COMM0102|공지사항|RUD|0");
            }

            CreatePages();
            devCallbackPanel.Controls.Add(pageControl);
            Page.DataBind();
        }

        protected void devCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParams = e.Parameter.Split('|');

            if (oParams[0].Equals("new"))
            {
                int newIndex = LastIndex + 1;
                OpenTabPagesCollection.Add(newIndex, e.Parameter.Replace("new|", ""));
                pageControl.TabPages.Clear();
                CreatePages();
                LastIndex++;

                devCallbackPanel.JSProperties["cpTabName"] = oParams[4];
            }
            else if (oParams[0].Equals("del"))
            {
                DeleteTab(oParams[1]);
            }
        }
        #endregion

        #region 사용자 정의 함수
        private void CreatePages()
        {
            foreach (KeyValuePair<int, string> tabPage in OpenTabPagesCollection)
                CreateTabPage(tabPage.Value);
        }

        private void CreateTabPage(string sParam)
        {
            string[] sParams = sParam.Split('|');
            TabPage tp = new TabPage();
            pageControl.TabPages.Add(tp);
            tp.Name = sParams[3];
            tp.Text = sParams[4];
            tp.TabTemplate = new DevTabTemplate(sParams, OpenTabPagesCollection.Keys.Count);
            CreateContent(tp, sParams);
        }

        private void CreateContent(TabPage tp, string[] sParams)
        {
            Literal literal = new Literal()
            {
                ID = "literal" + tp.Name,
                Text = String.Format("<iframe id=\"iframe{1}\" name=\"ifrmContent\" class=\"iframeContent\" frameborder=\"0\" src=\"../../Pages/{0}/{1}.aspx?pParam={2}\"></iframe>", sParams[2], sParams[3], String.Join("|", sParams))
            };
            tp.Controls.Add(literal);
        }

        private void DeleteTab(string tabName)
        {
            int dicIndex = OpenTabPagesCollection.FirstOrDefault(x => x.Value.Contains(tabName)).Key;
            OpenTabPagesCollection.Remove(dicIndex);
            TabPage tabPage = pageControl.TabPages.FindByName(tabName);
            pageControl.TabPages.Remove(tabPage);
        }
        #endregion
    }
}