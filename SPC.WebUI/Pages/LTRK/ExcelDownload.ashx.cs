using System;
using System.Collections.Generic;
using System.Web;

using System.IO;
using System.Data;
using CTF.Web.Framework.Helper;
using SPC.LTRK.Biz;
using SPC.SYST.Biz;

namespace SPC.WebUI.Pages.LTRK
{
    /// <summary>
    /// ExcelDownload의 요약 설명입니다.
    /// </summary>
    public class ExcelDownload : IHttpHandler
    {
        private Functions.Excels Excels = new Functions.Excels();
        private DataSet dsTemp = null;
        private DataTable dtWork = null;
        private DataTable dtUnit = null;
        private Dictionary<string, string> oParamDic = new Dictionary<string, string>();
        private List<string> oQuery = new List<string>();
        //private List<string> oCol01 = new List<string>();   // 공정
        //private List<string> oCol02 = new List<string>();   // 단위
        private string errMsg = String.Empty;

        public void ProcessRequest(HttpContext context)
        {
            string basicFileDIR = String.Concat(String.Format(CommonHelper.GetAppSectionsString("subPath"), "COMP"), "WorkOrder.xlsx");
            string sCompCD = context.Request.QueryString.Get("pCOMPCD");
            string defaultDIR = String.Format(CommonHelper.GetAppSectionsString("defaultCompPath"), sCompCD);
            string fullFileDIR = String.Concat(defaultDIR, String.Format("{0}_WorkOrder.xlsx", DateTime.Today.ToString("yyyy.MM.dd")));

            // 업체별 폴더 존재 유무 확인
            if (!Directory.Exists(defaultDIR))
                Directory.CreateDirectory(defaultDIR);

            // 기본양식 복사(항상)
            File.Copy(basicFileDIR, fullFileDIR, true);

            // 공정
            QCD74_LTRK0201_LST(sCompCD);
            foreach (DataRow dr in dtWork.Rows)
            {
                oQuery.Add(String.Format("INSERT INTO [공정$] (공정) VALUES('[{0}]{1},설비:[{2}]{3},설비분류:[{4}]{5}')",
                    dr["F_WORKCD"],
                    dr["F_WORKNM"],
                    dr["F_EQUIPCD"],
                    dr["F_EQUIPNM"],
                    dr["F_EQUIPTPCD"],
                    dr["F_EQUIPTPNM"]));
            }

            //// 단위
            //SYCOD01_LST(sCompCD);
            //foreach (DataRow dr in dtUnit.Rows)
            //{
            //    oQuery.Add(String.Format("INSERT INTO [단위$] (단위) VALUES('[{0}]{1}')",
            //        dr["F_CODE"],
            //        dr["F_CODENM"]));
            //}

            // 업데이트
            if (!String.IsNullOrEmpty(errMsg))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();

                response.Write(String.Format("<script type=\"text/javascript\">alert('파일생성실패\\r\\n{0}'); window.open('', '_self', ''); window.close();</script>", errMsg.Replace("'", "\"")));
            }
            else
            {
                if (!Excels.UpdateExecFile(fullFileDIR, oQuery, out errMsg))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();

                    response.Write(String.Format("<script type=\"text/javascript\">alert('파일생성실패\\r\\n{0}'); window.open('', '_self', ''); window.close();</script>", errMsg.Replace("'", "\"")));
                }
                else
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = CommonHelper.GetMimeType("xlsx");
                    response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}_{1}.xlsx;", DateTime.Today.ToString("yyyy.MM.dd"), HttpContext.Current.Server.UrlEncode("작업지시서")));
                    response.TransmitFile(fullFileDIR);
                    response.Flush();
                    response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        // 공정
        private void QCD74_LTRK0201_LST(string sCompCD)
        {
            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", sCompCD);
                oParamDic.Add("F_FACTCD", "01");
                dsTemp = biz.QCD74_LTRK0201_LST(oParamDic, out errMsg);
            }

            if (String.IsNullOrEmpty(errMsg) && dsTemp != null && dsTemp.Tables.Count > 0)
                dtWork = dsTemp.Tables[0];
        }

        // 단위
        private void SYCOD01_LST(string sCompCD)
        {
            using (SYSTBiz biz = new SYSTBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", sCompCD);
                oParamDic.Add("F_FACTCD", "01");
                oParamDic.Add("F_LANGTYPE", "KR");
                oParamDic.Add("F_CODEGROUP", "23");
                dsTemp = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            if (String.IsNullOrEmpty(errMsg) && dsTemp != null && dsTemp.Tables.Count > 0)
                dtUnit = dsTemp.Tables[0];
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}