using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;
using SPC.BSIF.Biz;

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0502_GroupNew : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string s_ImageIDX = String.Empty;
        protected string iconURL = String.Empty;
        protected string iconSIZE = String.Empty;
        #endregion

        #region 프로퍼티

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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 하단 설비 목록 조회
                RetrieveList();
            }

            // Grid Columns Sum Width
            // hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                s_ImageIDX = Page.ResolveUrl(String.Format("~/API/Common/Gathering/Download.ashx?code={0}&name={1}&gbn={2}", gsCOMPCD, "monitoring.png", "monitoring"));
                iconSIZE = CommonHelper.GetAppSectionsString("BulletSize");
                iconSIZE = !String.IsNullOrEmpty(iconSIZE) ? iconSIZE : "22";
                iconURL = Page.ResolveUrl(String.Format("~/Resources/icons/{0}",
                    iconSIZE.Equals("14") ? "14x14_green.png" : "green.gif"));
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
        { }
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

        #region 조회

        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.QCDPCNM_WGROUP_LST(oParamDic, out errMsg);
            }

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("MACHIDX", typeof(int));
            dtTemp.Columns.Add("MACHNM", typeof(string));
            dtTemp.Columns.Add("POSX", typeof(long));
            dtTemp.Columns.Add("POSY", typeof(long));
            dtTemp.Columns.Add("STATUS", typeof(bool));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string[] position = !String.IsNullOrEmpty(dr["F_POSITION"].ToString()) ? dr["F_POSITION"].ToString().Split(',') : new string[] { "0", "0" };

                dtTemp.Rows.Add(
                    Convert.ToInt32(dr["F_PCNO"]),
                    dr["F_WGROUPNM"].ToString(),
                    Convert.ToInt64(position[0]),
                    Convert.ToInt64(position[1]),
                    Convert.ToBoolean(dr["F_STATUS"])
                    );
            }

            devGrid.DataSource = dtTemp;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #region 설비위치저장
        private bool QCD_MACH21_POS_UPD(DataTable dtParam, out string resultMsg)
        {
            resultMsg = String.Empty;
            bool bExecute = false;

            List<string> oLists = new List<string>();
            List<Dictionary<string, string>> oParams = new List<Dictionary<string, string>>();

            foreach (DataRow dr in dtParam.Rows)
            {
                oLists.Add("USP_QCDPCNM_WGROUP_UPD");
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_PCNO", dr["MACHIDX"].ToString());
                oParamDic.Add("F_WGROUPNM", dr["MACHNM"].ToString());
                oParamDic.Add("F_POSITION", String.Format("{0},{1}", dr["POSX"], dr["POSY"]));
                oParamDic.Add("F_POSITION_W", "");
                oParamDic.Add("F_STATUS", Convert.ToBoolean(dr["STATUS"]) ? "1" : "0");
                oParamDic.Add("F_USER", gsUSERID);
                oParams.Add(oParamDic);
            }

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCDPCNM_BATCH(oLists.ToArray(), oParams.ToArray(), out resultMsg);
            }

            if (!String.IsNullOrEmpty(resultMsg))
                bExecute = false;

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 하단 그리드 목록 조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USEYN" }, "가동", "비가동", e);
        }
        #endregion

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;   // 오류 메시지
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataTable dtParam = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(e.Parameter); // 웹에서 전달된 파라미터
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            if (!QCD_MACH21_POS_UPD(dtParam, out errMsg))
            {
                result["ISOK"] = false;
                result["MSG"] = errMsg;
            }
            else
            {
                result["ISOK"] = true;
                result["MSG"] = "저장되었습니다.";
            }

            e.Result = jss.Serialize(result);
        }
        #endregion

        #endregion
    }
}