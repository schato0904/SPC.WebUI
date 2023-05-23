using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;

namespace SPC.WebUI.API.Common.WinSvc
{
    /// <summary>
    /// SPCnTx의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class SPCnTx : System.Web.Services.WebService
    {
        [WebMethod(Description = "Win API nTx")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJson()
        {
            string result = String.Format("{{\"resultCode\":\"{0}\",\"resultMsg\":\"{1}\"}}", "00", "성공");

            Context.Response.Clear();
            Context.Response.ClearHeaders();
            Context.Response.ClearContent();
            Context.Response.Charset = "utf-8";
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.ContentType = "application/json;charset=utf-8";
            Context.Response.Write(result);
            Context.Response.End();
        }

        [WebMethod(Description = "Win API nTx")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDataJson(string sp, string jsonParam, string encode)
        {
            string resultCode = "Success";
            string resultMsg = "조회요청이 성공하였습니다";
            
            string errMsg = String.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string jsonResult = String.Empty;
            Dictionary<string, string> jsonparamDic = null;
            bool bConvertJson = false;
            int nColumnCount = 0;

            try
            {
                jsonparamDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Web.HttpUtility.UrlDecode(jsonParam));
                bConvertJson = true;
            }
            catch (Exception e)
            {
                resultCode = "Failure";
                resultMsg = e.Message;
            }

            if (bConvertJson)
            {
                using (SPC.Common.Biz.CommonBiz biz = new SPC.Common.Biz.CommonBiz())
                {
                    ds = biz.WinAPInTx(sp, jsonparamDic, out errMsg);
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    resultCode = "Failure";
                    resultMsg = errMsg;
                }
                else
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        try
                        {
                            dt = ds.Tables[0];
                            nColumnCount = dt.Columns.Count;
                            jsonResult = JsonConvert.SerializeObject(ds.Tables[0], Newtonsoft.Json.Formatting.Indented);
                        }
                        catch (Exception e)
                        {
                            resultCode = "Failure";
                            resultMsg = e.Message;
                        }
                    }
                }
            }

            string result = String.Format("{{\"resultCode\":\"{0}\",\"resultMsg\":\"{1}\",\"colCount\":{2},\"data\":{3}}}", resultCode, resultMsg, nColumnCount, jsonResult);

            if (String.IsNullOrEmpty(encode))
                encode = "utf-8";
            
            Context.Response.Clear();
            Context.Response.ClearHeaders();
            Context.Response.ClearContent();
            Context.Response.Charset = encode;
            Context.Response.ContentEncoding = Encoding.GetEncoding(encode);
            Context.Response.Write(result);
            Context.Response.End();
        }
    }
}
