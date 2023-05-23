using System;
using System.Collections.Generic;
using System.Web;

namespace SPC.WebUI.Common
{
    public class GlobalFunction
    {
        //================================================================================
        // 함수명	: GetParam()
        // 설  명	: Request변수(get or form방식)를 받는다.
        // 작성일	: 2007년 11월 14일
        // 작성자	: 류원규
        // 수정일	:
        // 수정자	:
        //--------------------------------------------------------------------------------
        // INPUT  PARAMETER
        //		string		param   : 파라미터 변수 병
        // RETURN TYPE
        //		STRING
        //================================================================================
        public static string GetParam(string param)
        {
            if (HttpContext.Current.Request.QueryString[param] == null && HttpContext.Current.Request.Form[param] == null)
                return "";
            else if (HttpContext.Current.Request.QueryString[param] == null)
                return HttpContext.Current.Request.Form[param];
            else if (HttpContext.Current.Request.Form[param] == null)
                return HttpContext.Current.Request.QueryString[param];
            else
                return "";
        }

        //================================================================================
        // 함수명	: GetParamQuery()
        // 설  명	: Request변수(get방식)를 받는다.
        // 작성일	: 2007년 9월 18일
        // 작성자	: 류원규
        // 수정일	:
        // 수정자	:
        //--------------------------------------------------------------------------------
        // INPUT  PARAMETER
        //		string		param   : 파라미터 변수 병
        // RETURN TYPE
        //		STRING
        //================================================================================
        public static string GetParamQuery(string param)
        {
            if (HttpContext.Current.Request.QueryString[param] == null)
                return "";
            else
                return HttpContext.Current.Request.QueryString[param];
        }

        //================================================================================
        // 함수명	: GetParamQuery()
        // 설  명	: Request변수(get방식)를 받는다.
        // 작성일	: 2007년 9월 18일
        // 작성자	: 류원규
        // 수정일	:
        // 수정자	:
        //--------------------------------------------------------------------------------
        // INPUT  PARAMETER
        //		string		param   : 파라미터 변수 병
        // RETURN TYPE
        //		STRING
        //================================================================================
        public static string GetParamForm(string param)
        {
            if (HttpContext.Current.Request.Form[param] == null)
                return "";
            else
                return HttpContext.Current.Request.Form[param];
        }

        #region HTML에서 텍스트추출
        //HTML 에서 Text만 추출한다.
        public static string StripHtml(string Html)
        {
            string output;
            //get rid of HTML tags
            output = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
            //get rid of multiple blank lines
            output = System.Text.RegularExpressions.Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
            return output;
        }
        #endregion

        #region JSON 문자열로 변환
        /// <summary>
        /// JSON 문자열로 변환
        /// </summary>
        /// <param name="serializedJSONString"></param>
        /// <returns></returns>
        public static string SerializeJSON(object obj)
        {
            return (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(obj);
        }
        #endregion

        #region DataSet의 첫번째 DataRow를 Dictionary로 반환
        /// <summary>
        /// 데이터셋의 첫번째 Row를 Dictionary로 반환(column명으로)
        /// </summary>
        /// <param name="ds">결과 데이터셋</param>
        /// <param name="hasOnlyOneRow">Row가 하나밖에 없을때만 결과생성</param>
        /// <returns></returns>
        public static Dictionary<string, string> FirstRowToDictionary(System.Data.DataSet ds, bool hasOnlyOneRow = true)
        {
            var result = new Dictionary<string, string>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0
                && (!hasOnlyOneRow || ds.Tables[0].Rows.Count == 1))
            {
                foreach (System.Data.DataColumn col in ds.Tables[0].Columns)
                {
                    result.Add(col.ColumnName, ds.Tables[0].Rows[0][col.ColumnName].ToString());
                }
            }

            return result;
        }
        #endregion
    }
}
