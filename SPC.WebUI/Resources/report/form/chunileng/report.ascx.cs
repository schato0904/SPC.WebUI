﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.report.form.chunileng
{
    public partial class report : WebUIBasePageUserControl
    {
        #region 생성자(파라미터 전달 받기위해)
        public report() { }
        public report(DataTable _dtHead, DataTable _dtGroup, DataTable _dtData)
        {
            dtHead = _dtHead;
            dtGroup = _dtGroup;
            dtData = _dtData;
            
        }
        #endregion

        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataTable dtHead = null;
        DataTable dtGroup = null;
        DataTable dtData = null;
        DataTable dtx = null;
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
            devDocument.ReportTypeName = "SPC.WebUI.Resources.report.form.chunileng.xtraReport";

            DevExpress.XtraReports.UI.XtraReport report = new xtraReport(dtHead, dtGroup, dtData);
            devDocument.Report = report;
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
        }
        #endregion

        #endregion
    }
}