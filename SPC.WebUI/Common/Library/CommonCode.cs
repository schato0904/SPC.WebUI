using System;
using System.Collections.Generic;
using System.Data;

using SPC.Common.Biz;

namespace SPC.WebUI.Common
{
    public class CommonCode
    {
        #region 생성자
        public CommonCode() { }
        #endregion

        #region 변수설정
        private Dictionary<string, CodeDic> codeGroups = new Dictionary<string, CodeDic>();
        #endregion

        #region 검색
        public CodeDic this[string code]
        {
            get
            {
                CodeDic codeGroup;
                return codeGroups.TryGetValue(code, out codeGroup) ? codeGroup : null;
            }
        }

        public CodeDic this[object code]
        {
            get
            {
                return this[code.ToString()];
            }
        }
        #endregion

        #region CodeDict Class
        public class CodeDic
        {
            public CodeDic() { }

            public string FULLCD { get; set; }
            public string GROUPCD { get; set; }
            public string COMMCD { get; set; }
            public string COMMNMKR { get; set; }
            public string COMMNMUS { get; set; }
            public string COMMNMCN { get; set; }
            public string PARAM01 { get; set; }
            public string PARAM02 { get; set; }
            public string PARAM03 { get; set; }
            public string PARAM04 { get; set; }
            public string PARAM05 { get; set; }
            public string REMARK1 { get; set; }
            public string REMARK2 { get; set; }
            public Dictionary<string, CodeDic> codeGroup;

            public CodeDic this[string code]
            {
                get
                {
                    CodeDic codeSubGroup;
                    return codeGroup.TryGetValue(code, out codeSubGroup) ? codeSubGroup : null;
                }
            }
        }
        #endregion

        #region 공통코드를 구한다
        public void LoadCommonCode()
        {
            string errMsg = String.Empty;

            Dictionary<string, string> oParamDic = new Dictionary<string, string>();
            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Add("F_STATUS", "1");
                DataSet ds = biz.GetCommonCodeList(oParamDic, out errMsg);
                DataTable dtCode = ds.Tables[0];

                foreach (DataRow dtRow in dtCode.Select("F_GROUPCD='99'", "F_SORTNO ASC"))
                {
                    CodeDic codeDic = new CodeDic();
                    codeDic.GROUPCD = dtRow["F_GROUPCD"].ToString();
                    codeDic.COMMCD = dtRow["F_COMMCD"].ToString();
                    codeDic.FULLCD = String.Concat(codeDic.GROUPCD, codeDic.COMMCD);
                    codeDic.COMMNMKR = dtRow["F_COMMNMKR"].ToString();
                    codeDic.COMMNMUS = dtRow["F_COMMNMUS"].ToString();
                    codeDic.COMMNMCN = dtRow["F_COMMNMCN"].ToString();
                    codeDic.PARAM01 = dtRow["F_PARAM01"].ToString();
                    codeDic.PARAM02 = dtRow["F_PARAM02"].ToString();
                    codeDic.PARAM03 = dtRow["F_PARAM03"].ToString();
                    codeDic.PARAM04 = dtRow["F_PARAM04"].ToString();
                    codeDic.PARAM05 = dtRow["F_PARAM05"].ToString();
                    codeDic.REMARK1 = dtRow["F_REMARK1"].ToString();
                    codeDic.REMARK2 = dtRow["F_REMARK2"].ToString();
                    codeDic.codeGroup = new Dictionary<string, CodeDic>();

                    SetSubCommonCode(ds, codeDic, codeDic.COMMCD);

                    codeGroups.Add(codeDic.COMMCD, codeDic);
                }
            }
        }

        void SetSubCommonCode(DataSet ds, CodeDic codeDic, string GroupCode)
        {
            DataTable dt = ds.Tables[0];

            foreach (DataRow dtRow in dt.Select(String.Format("F_GROUPCD='{0}'", GroupCode), "F_SORTNO ASC"))
            {
                CodeDic codeSubDic = new CodeDic();
                codeSubDic.GROUPCD = dtRow["F_GROUPCD"].ToString();
                codeSubDic.COMMCD = dtRow["F_COMMCD"].ToString();
                codeSubDic.FULLCD = String.Concat(codeSubDic.GROUPCD, codeSubDic.COMMCD);
                codeSubDic.COMMNMKR = dtRow["F_COMMNMKR"].ToString();
                codeSubDic.COMMNMUS = dtRow["F_COMMNMUS"].ToString();
                codeSubDic.COMMNMCN = dtRow["F_COMMNMCN"].ToString();
                codeSubDic.PARAM01 = dtRow["F_PARAM01"].ToString();
                codeSubDic.PARAM02 = dtRow["F_PARAM02"].ToString();
                codeSubDic.PARAM03 = dtRow["F_PARAM03"].ToString();
                codeSubDic.PARAM04 = dtRow["F_PARAM04"].ToString();
                codeSubDic.PARAM05 = dtRow["F_PARAM05"].ToString();
                codeSubDic.REMARK1 = dtRow["F_REMARK1"].ToString();
                codeSubDic.REMARK2 = dtRow["F_REMARK2"].ToString();
                codeSubDic.codeGroup = new Dictionary<string, CodeDic>();

                SetSubCommonCode(ds, codeSubDic, codeSubDic.COMMCD);

                codeDic.codeGroup.Add(codeSubDic.COMMCD, codeSubDic);
            }
        }
        #endregion
    }
}
