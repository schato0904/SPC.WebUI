using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace SPC.WebUI.API.Common.WinSvc
{
    /// <summary>
    /// SPCTx의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class SPCTx : System.Web.Services.WebService
    {
        [WebMethod(Description = "Win API nTx")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveDataSingle(string sp, string jsonParam, string encode = "utf-8")
        {
            string resultCode = "Success";
            string resultMsg = "요청이 성공하였습니다";

            string errMsg = String.Empty;
            bool bExecute = false;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, string> jsonparamDic = null;
            bool bConvertJson = false;

            try
            {
                jsonparamDic = jss.Deserialize<Dictionary<string, string>>(jsonParam);
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
                    bExecute = biz.WinAPITxSingle(sp, jsonparamDic, out errMsg);
                }

                if (!bExecute)
                {
                    resultCode = "Failure";
                    resultMsg = !String.IsNullOrEmpty(errMsg) ? errMsg : "서버에서 알 수 없는 장애발생";
                }
            }

            string result = String.Format("{{\"resultCode\":\"{0}\",\"resultMsg\":\"{1}\"}}", resultCode, resultMsg);

            Context.Response.Clear();
            Context.Response.ClearHeaders();
            Context.Response.ClearContent();
            Context.Response.Charset = encode;
            Context.Response.ContentEncoding = Encoding.GetEncoding(encode);
            Context.Response.Write(result);
            Context.Response.End();
        }

        [WebMethod(Description = "Win API nTx")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveDataMultiple(string jsonData, string encode)
        {
            string resultCode = "Success";
            string resultMsg = "요청이 성공하였습니다";

            string errMsg = String.Empty;
            bool bExecute = false;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<object> jsonDataDic = null;
            Dictionary<string, object> jsonParamDic = null;
            Dictionary<string, string> resultParamDic = null;
            List<string> oSp = new List<string>();
            List<object> oParam = new List<object>();
            bool bConvertJson = false;

            try
            {
                jsonDataDic = jss.Deserialize<List<object>>(jsonData);
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
                    foreach (object oDataDic in jsonDataDic)
                    {
                        foreach (KeyValuePair<string, object> pairList in (Dictionary<string, object>)oDataDic)
                        {
                            if (pairList.Key.Equals("sp"))
                                oSp.Add(pairList.Value.ToString());
                            else if (pairList.Key.Equals("data"))
                            {
                                resultParamDic = new Dictionary<string, string>();
                                foreach(object oParamDic in (Array)pairList.Value)
                                {
                                    jsonParamDic = (Dictionary<string, object>)oParamDic;
                                    foreach (KeyValuePair<string, object> pairParam in jsonParamDic)
                                    {
                                        resultParamDic.Add(pairParam.Key, pairParam.Value.ToString());
                                    }
                                }
                                oParam.Add(resultParamDic);
                            }
                        }
                    }

                    if (oSp.Count > 0 && oParam.Count > 0)
                        bExecute = biz.WinAPITxMultiple(oSp.ToArray(), oParam.ToArray(), out errMsg);
                    else
                        errMsg = String.Format("프로시저({0}개) 또는 파라미터({1}개)가 없습니다", oSp.Count, oParam.Count);
                }

                if (!bExecute)
                {
                    resultCode = "Failure";
                    resultMsg = !String.IsNullOrEmpty(errMsg) ? errMsg : "서버에서 알 수 없는 장애발생";
                }
            }

            string result = String.Format("{{\"resultCode\":\"{0}\",\"resultMsg\":\"{1}\"}}", resultCode, resultMsg);

            Context.Response.Clear();
            Context.Response.ClearHeaders();
            Context.Response.ClearContent();
            Context.Response.Charset = encode;
            Context.Response.ContentEncoding = Encoding.GetEncoding(encode);
            Context.Response.Write(result);
            Context.Response.End();
        }

        [WebMethod(Description = "Win API nTx")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveDataMultipleForDic(string jsonData, string encode)
        {
            string resultCode = "Success";
            string resultMsg = "요청이 성공하였습니다";

            string errMsg = String.Empty;
            bool bExecute = false;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> jsonDataDic = null;
            Dictionary<string, string> resultParamDic = null;
            List<string> oSp = new List<string>();
            List<object> oParam = new List<object>();
            bool bConvertJson = false;

            try
            {
                jsonDataDic = jss.Deserialize<Dictionary<string, object>>(jsonData);
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
                    foreach (KeyValuePair<string, object> oDataDic in jsonDataDic)
                    {
                        foreach (KeyValuePair<string, object> pairList in (Dictionary<string, object>)oDataDic.Value)
                        {
                            if (pairList.Key.Equals("sp"))
                                oSp.Add(pairList.Value.ToString());
                            else if (pairList.Key.Equals("data"))
                            {
                                resultParamDic = new Dictionary<string, string>();
                                foreach (KeyValuePair<string, object> oParamDic in (Dictionary<string, object>)pairList.Value)
                                {
                                    resultParamDic.Add(oParamDic.Key, oParamDic.Value.ToString());
                                }
                                oParam.Add(resultParamDic);
                            }
                        }
                    }

                    if (oSp.Count > 0 && oParam.Count > 0)
                        bExecute = biz.WinAPITxMultiple(oSp.ToArray(), oParam.ToArray(), out errMsg);
                    else
                        errMsg = String.Format("프로시저({0}개) 또는 파라미터({1}개)가 없습니다", oSp.Count, oParam.Count);
                }

                if (!bExecute)
                {
                    resultCode = "Failure";
                    resultMsg = !String.IsNullOrEmpty(errMsg) ? errMsg : "서버에서 알 수 없는 장애발생";
                }
            }

            string result = String.Format("{{\"resultCode\":\"{0}\",\"resultMsg\":\"{1}\"}}", resultCode, resultMsg);

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
