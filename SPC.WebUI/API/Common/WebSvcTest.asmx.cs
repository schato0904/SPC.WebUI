using Newtonsoft.Json;
using SPC.FDCK.Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace SPC.WebUI.API.Common
{
    /// <summary>
    /// WebSvcTest의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class WebSvcTest : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public void GetJsonData()
        {
            string errMsg = String.Empty;
            DataSet ds = null;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", "09");
                oParamDic.Add("F_FACTCD", "01");
                oParamDic.Add("F_BANCD", "");
                oParamDic.Add("F_LINECD", "");
                oParamDic.Add("F_MACHKIND", "");
                oParamDic.Add("F_MACHNM", "");
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }

            Context.Response.Write(String.Format("{{\"data\":{0}}}", json));
        }
    }
}
