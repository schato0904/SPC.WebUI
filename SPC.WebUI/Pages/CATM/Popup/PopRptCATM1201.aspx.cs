using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.WebUI.Common.Library;
using SPC.WebUI.Pages.CATM.Report;
using SPC.CATM.Biz;

namespace SPC.WebUI.Pages.CATM.Popup
{
    public partial class PopRptCATM1201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        //string F_IS01MID = String.Empty;
        //string F_PRJNO = String.Empty;
        //string F_LANGTYPE = string.Empty;
        Dictionary<string, string> popParam = new Dictionary<string, string>();

        #endregion

        #region 프로퍼티
        //int F_CM13WMID
        //{
        //    get { return (int)(Session["F_CM13WMID_PopRptINME9101"] ?? 0); }
        //    set { Session["F_CM13WMID_PopRptINME9101"] = value; }
        //}
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
            // Request
            GetRequest();

            //RptCATM1201 report = new RptCATM1201(this.gsCOMPCD, this.gsFACTCD, this.popParam.GetString("F_WORKNO"), this.gsLANGTP);
            string errMsg = string.Empty;
            var dic = this.GetData(this.popParam.GetString("F_WORKNO"), out errMsg);
            if (!string.IsNullOrWhiteSpace(errMsg)) throw new Exception(errMsg);
            RptCATM1211_2 report = new RptCATM1211_2(dic);
            this.devDocument.Report = report;
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
            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                // devGrid.JSProperties["cpResultCode"] = "";
                // devGrid.JSProperties["cpResultMsg"] = "";
            }
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

        #region Request
        /// <summary>
        /// GetRequest
        /// </summary>
        void GetRequest()
        {
            string p = Request["PopParam"];
            if (!string.IsNullOrEmpty(p))
            {
                popParam = DeserializeJSON(p);
                this.hidPopParam.Text = p;
            }

            ////string apprno = Request["APPRNO"];
            //string F_CM13WMID = popParam.ContainsKey("F_CM13WMID") ? popParam["F_CM13WMID"] : (!string.IsNullOrEmpty(Request["F_CM13WMID"]) ? Request["F_CM13WMID"] : "0");
            //int tmp = int.TryParse(F_CM13WMID, out tmp) ? tmp : 0;
            //this.F_CM13WMID = tmp;
        }
        #endregion

        #region Web Init
        /// <summary>
        /// Web_Init
        /// </summary>
        void Web_Init()
        {
            // DefaultButton 세팅
            SetDefaultButton();

            // 객체 초기화
            SetDefaultObject();

            // 클라이언트 스크립트
            SetClientScripts();

            // 서버 컨트럴 객체에 기초값 세팅
            SetDefaultValue();
        }
        #endregion

        #region DefaultButton 세팅
        /// <summary>
        /// SetDefaultButton
        /// </summary>
        void SetDefaultButton()
        { }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        { }
        #endregion

        #region 클라이언트 스크립트
        /// <summary>
        /// SetClientScripts
        /// </summary>
        void SetClientScripts()
        { }
        #endregion

        #region 서버 컨트럴 객체에 기초값 세팅
        /// <summary>
        /// SetDefaultValue
        /// </summary>
        void SetDefaultValue()
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #endregion

        #region 사용자이벤트

        #endregion

        #region DB 처리 함수

        #region 데이터 조회
        /// <summary>
        /// 데이터 조회
        /// </summary>
        /// <param name="F_WORKNO"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetData(string F_WORKNO, out string errMsg)
        {
            errMsg = string.Empty;
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_WORKNO"] = F_WORKNO;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1201_LST4(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
                result = DataTable2Dictionary(dt);
            }
            return result.Count > 0 ? result[0] : null;
        }
        
        // 데이터테이블을 json 문자열로 반환(DataTable -> Json)
        public List<Dictionary<string, string>> DataTable2Dictionary(DataTable dt)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.AsEnumerable().Select(dr => dr.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => dr[c].ToString())).ToList();
                //result = SerializeJSON(dict);
            }
            return result;
        }
        #endregion
        #endregion
    }
}