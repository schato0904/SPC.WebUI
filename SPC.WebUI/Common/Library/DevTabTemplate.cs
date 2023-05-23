using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPC.WebUI.Common
{
    public class DevTabTemplate : ITemplate
    {
        private string[] tabParam;
        private int tabCount;

        public DevTabTemplate(string[] tabParam, int tabCount)
        {
            this.tabParam = tabParam;
            this.tabCount = tabCount;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Label lblText = new Label() { ID = "tabPage" + container.ID, Text = this.tabParam[4], Height = 25 };
            lblText.Style.Add("font-weight", "bold;");
            lblText.Style.Add("padding-top", "7px;");
            lblText.Style.Add("padding-left", "3px;");
            lblText.Style.Add("padding-right", "3px;");
            Image closeBtn = new Image() { ID = "closeBtn" + container.ID, ImageUrl = "~/Resources/Images/closeBtn.jpg", ImageAlign = ImageAlign.Right };
            closeBtn.Style.Add("margin-top", "3px;");
            closeBtn.Style.Add("margin-right", "3px;");
            closeBtn.Style.Add("cursor", "pointer");
            closeBtn.Attributes.Add("onclick", String.Format("closeActiveTab('{0}')", this.tabParam[3]));

            container.Controls.Add(lblText);
            container.Controls.Add(closeBtn);
        }
    }
}
