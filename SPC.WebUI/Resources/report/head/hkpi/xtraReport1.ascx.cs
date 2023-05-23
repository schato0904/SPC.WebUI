using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.report.head.hkpi
{
    public partial class xtraReport1 : WebUIBasePageUserControl
    {
        #region Page Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtDATE.DisplayFormatString = "yyyy.MM.dd";
            this.txtDATE.EditFormatString = "yyyy.MM.dd";
            this.txtDATE.Date = DateTime.Now;

            this.txtREVDT.DisplayFormatString = "yy.MM.dd";
            this.txtREVDT.EditFormatString = "yy.MM.dd";
            this.txtREVDT.Date = DateTime.Now;
        }
        #endregion
    }
}