using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.frames
{
    public partial class tabFrame : WebUIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }
    }
}