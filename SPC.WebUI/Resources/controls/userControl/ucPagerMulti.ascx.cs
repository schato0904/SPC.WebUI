using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucPagerMulti : System.Web.UI.UserControl, IDisposable
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        private Int32 _PageSize = 50;
        private Int32 _BlockSize = 15;
        private Int32 _TotalItems = 0;
        private Int32 _TotalPages = 0;
        private Int32 _CurrPage = 1;
        #endregion

        #region 프로퍼티
        // 페이지별 아이템 갯수
        public Int32 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        // 블럭별 페이지 갯수
        public Int32 BlockSize
        {
            get { return _BlockSize; }
            set { _BlockSize = value; }
        }
        // 전체 아이템 갯수
        public Int32 TotalItems
        {
            get { return _TotalItems; }
            set { _TotalItems = value; }
        }
        // 전체 페이지 갯수
        public Int32 TotalPages
        {
            get { return _TotalPages; }
            set { _TotalPages = value; }
        }
        // 현재페이지
        public Int32 CurrPage
        {
            get { return _CurrPage; }
            set { _CurrPage = value; }
        }
        // Callback 실행할 컨트럴들
        public string targetCtrls { get; set; }
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
            this.pagerPanel.ClientInstanceName = this.pagerPanel.UniqueID;
            this.hidCurrPage.ClientInstanceName = this.hidCurrPage.UniqueID;
            this.ddlPageSize.ClientInstanceName = this.ddlPageSize.UniqueID;

            //<ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_PageSizeSelectedIndexChanged" />

            this.ddlPageSize.ClientSideEvents.Init = "function(s,e){{ fn_OnControlDisable(s,e); }}";
            this.ddlPageSize.ClientSideEvents.SelectedIndexChanged = string.Format("function(s,e){{ {0}.fn_PageSizeSelectedIndexChanged(s,e); }}", this.ID);
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

        #region 사용자 정의 함수

        #region 사용자가 선택한 PageSize를 구한다
        public Int32 GetPageSize()
        {
            return Convert.ToInt32(ddlPageSize.SelectedItem.Value);
        }
        #endregion

        #region 페이지 네비게이션 Rendering
        void PagerRendering()
        {
            int nStartPage = (CurrPage % BlockSize == 0) ? CurrPage / BlockSize - 1 : CurrPage / BlockSize;
            nStartPage = (nStartPage * BlockSize) + 1;
            int nEndPage = nStartPage + BlockSize - 1;
            nEndPage = nEndPage > TotalPages ? TotalPages : nEndPage;
            int PageBlock = (CurrPage % BlockSize == 0) ? CurrPage / BlockSize : (CurrPage / BlockSize) + 1;
            int TotalBlocks = (TotalPages % BlockSize == 0) ? TotalPages / BlockSize : (TotalPages / BlockSize) + 1;

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"pagination pagination-sm\" style=\"margin-top: -5px;\">");
            if (PageBlock.Equals(1))
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0);\"><i class=\"fa fa-angle-double-left\"></i></a></li>");
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0);\"><i class=\"fa fa-angle-left\"></i></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{1}.fn_gotoPage('{0}');\"><i class=\"fa fa-angle-double-left\"></i></a></li>", "1",this.ID);
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{1}.fn_gotoPage('{0}');\"><i class=\"fa fa-angle-left\"></i></a></li>", nStartPage - BlockSize, this.ID);
            }

            for (int idx = nStartPage; idx <= nEndPage; idx++)
            {
                if (this.CurrPage.Equals(idx))
                    sb.AppendFormat("<li class=\"disabled\"><a href=\"javascript:void(0);\" style=\"border-color: #eaeef1;background-color: #f2f4f8;\">{0}</a></li>", idx);
                else
                    sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{1}.fn_gotoPage('{0}');\">{0}</a></li>", idx,this.ID);
            }

            if (PageBlock.Equals(TotalBlocks))
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0);\"><i class=\"fa fa-angle-right\"></i></a></li>");
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0);\"><i class=\"fa fa-angle-double-right\"></i></a></li>");
            }
            else
            {
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{1}.fn_gotoPage('{0}');\"><i class=\"fa fa-angle-right\"></i></a></li>", nEndPage + 1, this.ID);
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{1}.fn_gotoPage('{0}');\"><i class=\"fa fa-angle-double-right\"></i></a></li>", TotalPages, this.ID);
            }
            sb.Append("</ul>");
            divPageNavigation.InnerHtml = sb.ToString();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region 페이지 데이타 바인딩
        public void PagerDataBind()
        {
            // 사용자가 입력한 페이지 사이즈가 없을 때 처리
            if (ddlPageSize.Items.FindByValue(PageSize.ToString()) == null)
            {
                int nIndex = 0;

                foreach (DevExpress.Web.ListEditItem listItem in ddlPageSize.Items)
                {
                    if (Convert.ToInt32(listItem.Value) < PageSize)
                    {
                        nIndex++;
                        continue;
                    }
                    else
                        break;
                }

                ddlPageSize.Items.Insert(nIndex, new DevExpress.Web.ListEditItem(PageSize.ToString(), PageSize.ToString()));
            }

            // 기본페이지 사이즈 정의
            ddlPageSize.SelectedIndex = ddlPageSize.Items.FindByValue(PageSize.ToString()).Index;

            // 현재페이지 정의
            hidCurrPage.Text = CurrPage.ToString();

            // 전체 사이즈 계산
            int nTotalPageCnt = 1;
            nTotalPageCnt = this.TotalItems / this.PageSize;
            nTotalPageCnt += (this.TotalItems % this.PageSize) == 0 ? 0 : 1;
            nTotalPageCnt = nTotalPageCnt > 0 ? nTotalPageCnt : 1;
            TotalPages = nTotalPageCnt;

            // 페이지정보
            lblPageInfo.Text = String.Format("Page {0} of {1} ({2} items)", CurrPage, TotalPages, TotalItems);

            // 페이지 네비게이션 Rendering
            PagerRendering();
        }
        #endregion

        #region pagerPanel_Callback
        /// <summary>
        /// pagerPanel_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void pagerPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            int nCurrPage = CurrPage;
            int nPageSize = PageSize;
            int nItemsCnt = TotalItems;
            string[] oParams = new string[2];
            foreach (string oParam in e.Parameter.Split(';'))
            {
                oParams = oParam.Split('=');
                if (oParams[0].Equals("PAGESIZE"))
                    nPageSize = Convert.ToInt32(oParams[1]);
                else if (oParams[0].Equals("CURRPAGE"))
                    nCurrPage = Convert.ToInt32(oParams[1]);
                else if (oParams[0].Equals("ITEMSCNT"))
                    nItemsCnt = Convert.ToInt32(oParams[1]);
            }

            CurrPage = nCurrPage;
            PageSize = nPageSize;
            TotalItems = nItemsCnt;

            // 페이지 데이타 바인딩
            PagerDataBind();
        }
        #endregion

        #endregion
    }
}