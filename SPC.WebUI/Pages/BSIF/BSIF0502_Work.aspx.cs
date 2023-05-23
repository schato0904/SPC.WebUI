using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.BSIF.Biz;
using System.Text;
using System.Xml;
using System.Web.Script.Services;
using CTF.Web.Framework.Helper;


namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0502_Work : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string ImageURL = String.Empty;
        protected string iconURL = String.Empty;
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

            //if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            //{
            //    // PC정보를 구한다
            //    QCDPCNM_WORK_LST();
            //}
            
            // Grid Columns Sum Width
            //hidGridColumnsWidth.Text = DevExpressLib.devGridColumnWidth(devGrid).ToString();
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

                ImageURL = Page.ResolveUrl(String.Format("~/API/Common/Gathering/Download.ashx?code={0}&name={1}&gbn={2}", gsCOMPCD, "monitoring.png", "monitoring"));
                iconURL = Page.ResolveUrl(String.Format("~/Resources/icons/{0}",
                    CommonHelper.GetAppSectionsString("BulletSize").Equals("14") ? "14x14_green.png" : "green.gif"));

                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "";
                pnlSearch.JSProperties["cpResultMsg"] = "";
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

        #region PC정보를 구한다
        void QCDPCNM_WORK_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "true");


                ds = biz.QCDPCNM_WORK_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "0";
                pnlSearch.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }


                pnlSearch.JSProperties["cpTable"] = serializer.Serialize(rows);
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_InitNewRow
        /// <summary>
        /// devGrid_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = true;            
        }
        #endregion


        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {

        }
        #endregion

        #region devGrid CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetUsedString(new string[] { "F_STATUS" }, e);
        }
        #endregion

        #region devGrid BatchUpdate
        /// <summary>
        /// pnlSearch_Update
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void pnlSearch_Update(DataTable dt)
        {
            int idx = 0;
            int count = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["F_GBN"].ToString() == "y")
                {
                    count++;
                }
            }            

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Update
            if (count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["F_GBN"].ToString() == "y"){
                        oParamDic = new Dictionary<string, string>();
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_PCNO", (dr["F_PCNO"] ?? "").ToString());
                        oParamDic.Add("F_WORKCD", (dr["F_WORKCD"] ?? "").ToString());
                        oParamDic.Add("F_WORKNM", (dr["F_WORKNM"] ?? "").ToString());
                        oParamDic.Add("F_POSITION", (dr["F_POSITION"] ?? "").ToString());
                        oParamDic.Add("F_STATUS", (dr["F_STATUS"] ?? "").ToString());
                        oParamDic.Add("F_USER", gsUSERID);

                        oSPs[idx] = "USP_QCDPCNM_WORK_UPD";
                        oParameters[idx] = (object)oParamDic;

                        idx++;
                    }
                    
                }
            }
            #endregion


            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCDPCNM_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // PC정보를 구한다
                QCDPCNM_WORK_LST();
            }

            pnlSearch.JSProperties["cpResultCode"] = procResult[0];
            pnlSearch.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            //e.Handled = true;
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void pnlSearch_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string strParam = e.Parameter.ToString();
            //var Param = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<DataTable>(Server.UrlDecode(strParam));
            //var Param = (new System.Web.Script.Serialization.JavaScriptSerializer()).DeserializeObject<DataTable>(strParam.ToString());

            var Param = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strParam);
            
            
            if (Param == null )
            {
                QCDPCNM_WORK_LST();
            }
            else 
            {
                pnlSearch_Update(Param);
            }            
        }
        #endregion

        #endregion
    }
}