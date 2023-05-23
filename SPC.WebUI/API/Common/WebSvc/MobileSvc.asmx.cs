using Newtonsoft.Json;
using SPC.Common.Biz;
using SPC.FDCK.Biz;
using SPC.SYST.Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace SPC.WebUI.API.Common.WebSvc
{
    /// <summary>
    /// MobileSvc의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    [System.Web.Script.Services.ScriptService]
    public class MobileSvc : System.Web.Services.WebService
    {
        DataSet ds = null;
        SPC.WebUI.Common.WebUIBasePage wb = new WebUI.Common.WebUIBasePage();

        #region Convert JSON
        /// <summary>
        /// 기능명 : Convert JSON
        /// 작성자 : KIM S
        /// 작성일 : 2016-09-26
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>object</returns>
        public object DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(list);
        }
        #endregion

        #region 반 조회
        /// <summary>
        /// 기능명 : 반 조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "QCD72_LST")]
        public string QCD72_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);

                ds = biz.GetQCD72_LST(oParamDic, out errMsg);
            }

            return DataTableToJSON(ds.Tables[0]).ToString();
        }
        #endregion

        #region 라인 조회
        /// <summary>
        /// 기능명 : 라인 조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "QCD73_LST")]
        public string QCD73_LST(string BANCD)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_BANCD", BANCD);

                ds = biz.QCD73_LST(oParamDic, out errMsg);
            }

            return DataTableToJSON(ds.Tables[0]).ToString();
        }

        #endregion

        #region 설비 리스트조회
        /// <summary>
        /// 기능명 : 설비 리스트조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "MACH21_LST_BY_USR")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MACH21_LST_BY_USR(string BANCD, string LINECD)
        {
            string errMsg = String.Empty;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_BANCD", BANCD);
                oParamDic.Add("F_LINECD", LINECD);
                oParamDic.Add("F_USERID", wb.gsUSERID);

                ds = biz.QCD_MACH21_LST_BY_USR(oParamDic, out errMsg);
            }

            //string returnStr = DataTableToJSON(ds.Tables[0]).ToString();


            //Context.Response.Write(String.Format("{{\"data:\"{0}}}", returnStr));

            if (ds != null && ds.Tables.Count > 0)
            {
                json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }

            Context.Response.Write(String.Format("{{\"data\":{0}}}", json));
            //return String.Format("{{\"data\":{0}}}", json);
        }
        #endregion

        #region 설비 리스트조회
        /// <summary>
        /// 기능명 : 설비 리스트조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "MACH21_LST")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MACH21_LST(string MACHIDX)
        {
            string errMsg = String.Empty;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_MACHIDX", MACHIDX);

                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }

            return DataTableToJSON(ds.Tables[0]).ToString();
        }
        #endregion

        #region 점검기준조회
        /// <summary>
        /// 기능명 : 점검기준조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "MACH23_MACH26_LST")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MACH23_MACH26_LST(string MACHIDX, string DATE)
        {
            string errMsg = String.Empty;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_MACHIDX", MACHIDX);
                oParamDic.Add("F_MEASYMD", DATE);
                //oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH23_MACH26_LST(oParamDic, out errMsg);
            }

            return DataTableToJSON(ds.Tables[0]).ToString();
        }
        #endregion

        #region 점검기준조회 For DIOF0301
        /// <summary>
        /// 기능명 : 점검기준조회 For DIOF0301
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "MACH23_MACH26_LST_SHEET")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MACH23_MACH26_LST_SHEET(string MACHIDX, string DATE)
        {
            string errMsg = String.Empty;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_MACHIDX", MACHIDX);
                oParamDic.Add("F_MEASYMD", DATE);
                //oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH23_MACH26_LST(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }

            Context.Response.Write(String.Format("{{\"data\":{0}}}", json));
        }
        #endregion

        #region 점검결과등록
        /// <summary>
        /// 기능명 : 점검결과등록
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "MACH23_INS")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MACH23_INS(string JSON)
        {
            List<Dictionary<string, string>> json = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(JSON);
            List<string> oSPs = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();
            Dictionary<string, string> oParamDic;
            string resultMsg = String.Empty;
            string returnStr = String.Empty;
            string strMachidx = "";
            bool bExecute = true;
            string uniqueID = wb.UF.Encrypts.GetUniqueKey();
            int usnqieIDX = 0;

            #region Update
            if (json.Count > 0)
            {
                foreach (Dictionary<string, string> Value in json)
                {
                    usnqieIDX++;

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                    oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                    oParamDic.Add("F_MEASIDX", Value["F_MEASIDX"]);
                    oParamDic.Add("F_INSPIDX", Value["F_INSPIDX"]);
                    oParamDic.Add("F_MACHIDX", Value["F_MACHIDX"]);
                    oParamDic.Add("F_MEASYMD", Value["F_MEASYMD"]);
                    oParamDic.Add("F_CHASU", Value["F_NUMBER"]);
                    oParamDic.Add("F_BANCD", Value["F_BANCD"]);
                    oParamDic.Add("F_LINECD", Value["F_LINECD"]);
                    oParamDic.Add("F_MEASURE", Value["F_INPUTMEASURE"]);
                    oParamDic.Add("F_JUDGE", Value["F_INPUTJUDGE"]);
                    oParamDic.Add("F_USERID", wb.gsUSERID);
                    oParamDic.Add("F_USERNM", wb.gsUSERNM);
                    oParams.Add(oParamDic);
                    oSPs.Add("USP_QWK_MACH23_INS_UPD");


                    if (Value["F_INPUTJUDGE"] == "AAG702")
                    {
                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                        oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                        oParamDic.Add("F_REMEDYSEQ", uniqueID);
                        oParamDic.Add("F_REMEDYIDX", usnqieIDX.ToString());
                        oParamDic.Add("F_INSPIDX", Value["F_INSPIDX"]);
                        oParamDic.Add("F_MACHIDX", Value["F_MACHIDX"]);
                        oParamDic.Add("F_NUMBER", Value["F_NUMBER"]);
                        oParamDic.Add("F_NGTYPE", Value["F_NGTYPE"]);
                        oParamDic.Add("F_NGREMK", Value["F_NGREMK"]);
                        oParamDic.Add("F_STATUS", Value["F_STATUS"]);
                        oParamDic.Add("F_RESPTYPE", "");
                        oParamDic.Add("F_RESPREMK", "");
                        oParamDic.Add("F_RESPDT", "");
                        oParamDic.Add("F_RESPUSER", "");
                        oParams.Add(oParamDic);
                        oSPs.Add("USP_QWK_MACH24_TEMP_INS");
                    }

                    strMachidx = Value["F_MACHIDX"];
                }

                // 1. 임시테이블에서 기본테이블로 데이터 이동
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_REMEDYSEQ", uniqueID);
                oParamDic.Add("F_MACHIDX", strMachidx);
                oParamDic.Add("F_USER", wb.gsUSERID);
                oParams.Add(oParamDic);
                oSPs.Add("USP_QWK_MACH24_COPY");

                // 2. 임시테이블에서 정보삭제
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_REMEDYSEQ", uniqueID);
                oParamDic.Add("F_MACHIDX", strMachidx);
                oParams.Add(oParamDic);
                oSPs.Add("USP_QWK_MACH24_TEMP_DEL");

            }
            #endregion

            #region Database Execute
            if (oSPs.Count > 0 && oParams.Count > 0 && oSPs.Count == oParams.Count)
            {
                using (FDCKBiz biz = new FDCKBiz())
                {
                    bExecute = biz.PROC_QCD_MACH_MULTI(oSPs.ToArray(), oParams.ToArray(), out resultMsg);
                }

                if (!String.IsNullOrEmpty(resultMsg))
                    bExecute = false;
            }

            if (!bExecute)
            {
                returnStr = "error : " + resultMsg;
            }
            else
            {
                resultMsg = "저장완료";
            }

            #endregion

            return JsonConvert.SerializeObject(resultMsg);
        }
        #endregion

        #region 이상유형조회
        /// <summary>
        /// 기능명 : 이상유형조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "SYCOD01_LST")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SYCOD01_LST(string CODEGROUP)
        {
            string errMsg = String.Empty;

            using (SYSTBiz biz = new SYSTBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_CODEGROUP", CODEGROUP);
                oParamDic.Add("F_LANGTYPE", wb.gsLANGTP);

                ds = biz.SYCOD01_LST(oParamDic, out errMsg);
            }

            return DataTableToJSON(ds.Tables[0]).ToString();
        }
        #endregion

        #region 월점검시트 조회
        /// <summary>
        /// 기능명 : 월점검시트 조회
        /// 작성자 : KIM S
        /// 작성일 : 2017-11-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="strUserid">string</param>
        /// <param name="strUserpwd">string</param>
        /// <returns>string</returns>
        [WebMethod(Description = "QWK_MACH23_LST")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QWK_MACH23_LST(string MONTH, string MACHIDX)
        {
            string errMsg = String.Empty;
            string json = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Clear();
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", wb.gsCOMPCD);
                oParamDic.Add("F_FACTCD", wb.gsFACTCD);
                oParamDic.Add("F_MONTH", MONTH);
                oParamDic.Add("F_MACHIDX", MACHIDX);
                ds = biz.QWK_MACH23_LST(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }

            Context.Response.Write(String.Format("{{\"data\":{0}}}", json));
        }
        #endregion
    }
}
