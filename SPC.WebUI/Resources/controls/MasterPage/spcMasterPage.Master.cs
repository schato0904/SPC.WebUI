using System;
using System.Collections.Generic;
using SPC.WebUI.Common;

namespace SPC.WebUI.Resources.controls
{
    public partial class spcMasterPage : WebUIBasePageMaster
    {
        protected string sPopup = String.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            string oParam = Request.QueryString.Get("pParam") ?? "";
            string[] oParams = oParam.Split('|');

            // 2015-01-19 jnlee ..권한체크 통과 부분 추가(공지사항) 
            if (!isNoCheckPage(oParams) && !oParams[6].Equals("1") && !oParams[6].Equals("2"))
                Response.Redirect("~/Pages/ERROR/403.aspx");

            sPopup = Request.QueryString.Get("bPopup") ?? "false";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 권한 체크 없이 사용될 페이지 확인
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        protected bool isNoCheckPage(string[] oParams)
        {
            // 권한 체크 없이 사용될 페이지 리스트 : 내부 리스트로 관리
            var noCheckPageList = new List<string>() { "COMM0101","COMM0102" };
            var pageNm = oParams[3];

            return noCheckPageList.Contains(pageNm);
        }
    }
}