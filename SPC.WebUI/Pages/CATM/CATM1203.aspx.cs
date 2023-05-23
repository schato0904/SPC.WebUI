using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using DevExpress.Web;
using SPC.WebUI.Common;
using SPC.CATM.Biz;
using SPC.Common.Biz;
using SPC.WebUI.Common.Library;

namespace SPC.WebUI.Pages.CATM
{
    public partial class CATM1203 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        // 좌측목록
        DataTable CachedData
        {
            get { return Session["CATM1203_Grid"] as DataTable; }
            set { Session["CATM1203_Grid"] = value; }
        }

        // 우측목록
        DataTable CachedData1
        {
            get { return Session["CATM1203_Grid1"] as DataTable; }
            set { Session["CATM1203_Grid1"] = value; }
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
            // 파라미터 처리
            if (!IsPostBack && !string.IsNullOrEmpty(Request["oSetParam"]))
            {
                var pm = this.DeserializeJSON(Request["oSetParam"]);

            }
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
        {

        }
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
        {
        }
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

            if (!this.IsPostBack)
            {
                this.CachedData = null;
                this.CachedData1 = null;
            }
            //BindCombo(srcF_MACHCD);
            //BindCombo(srcF_MELTCD);
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

                devCallback2.JSProperties["cpResultCode"] = "";
                devCallback2.JSProperties["cpResultMsg"] = "";
            }
            GetMach();
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid 이벤트 처리

        #region devGrid_DataBinding
        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            this.devGrid.DataSource = this.CachedData;
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p1 = this.GetRightParameter();
                string errMsg = string.Empty;
                this.CachedData = this.GetDataRight(p1, out errMsg);

                devGrid.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid.DataBind();
        }
        #endregion

        #endregion

        #region devGrid1 이벤트 처리

        #region devGrid1_DataBinding
        protected void devGrid1_DataBinding(object sender, EventArgs e)
        {
            this.devGrid1.DataSource = this.CachedData1;
        }
        #endregion

        #region devGrid1 CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "GET")
            {
                var p1 = this.GetLeftParameter();
                string errMsg = string.Empty;
                this.CachedData1 = this.GetDataLeftBody(p1, out errMsg);

                devGrid1.JSProperties["cpResultCode"] = string.IsNullOrEmpty(errMsg) ? "" : "0";
                devGrid1.JSProperties["cpResultMsg"] = string.IsNullOrEmpty(errMsg) ? "" : errMsg;
            }
            devGrid1.DataBind();
        }
        #endregion

        #endregion

        #endregion

        #region 사용자 정의 함수

        #region GetRightParameter() 우측 그리드 조회 시 사용
        protected Dictionary<string, string> GetRightParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_PLANYMD"] = (this.schF_PLANYMD.Text ?? "");
            result["F_MACHCD"] = hidMACH.Text.ToString();


            return result;
        }
        #endregion

        #region GetLeftParameter() 좌측 그리드 조회 시 사용
        protected Dictionary<string, string> GetLeftParameter()
        {
            var result = new Dictionary<string, string>();

            result["F_PLANYMD"] = (this.schF_PLANYMD.Text ?? "");



            return result;
        }
        #endregion

        #region GetMach
        void GetMach()
        {
            string errMsg = String.Empty;

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                //oParamDic["F_DATE"] = (this.schF_PLANYMD.Text ?? "");
                oParamDic["F_USEYN"] = "1";
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1203_LST2(oParamDic, out errMsg);
            }

            srcF_MACHCD.DataSource = ds;
            srcF_MACHCD.DataBind();
            srcF_MACHCD.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "" });
        }
        #endregion

        #region srcF_MACHCD_Callback
        protected void srcF_MACHCD_Callback(object sender, CallbackEventArgsBase e)
        {
            GetMach();
        }
        #endregion

        #endregion

        #region DB 처리 함수

        #region 우측 그리드 조회
        /// <summary>
        /// 우측 그리드 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataRight(Dictionary<string, string> dic, out string errMsg)
        {
            errMsg = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_DATE"] = dic.GetString("F_PLANYMD");
                oParamDic["F_MACHCD"] = hidMACH.Text.ToString();
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1203_LST3(oParamDic, out errMsg);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = this.AutoNumberTable(ds.Tables[0]);
            }
            return dt;
        }
        #endregion

        #region 좌측 그리드 조회
        /// <summary>
        /// 좌측 그리드 조회
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected DataTable GetDataLeftBody(Dictionary<string, string> dic, out string errMsg)
        {
            DataTable dt = new DataTable();

            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_PLANYMD"] = dic.GetString("F_PLANYMD");
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;
                oParamDic["F_LANGTYPE"] = gsLANGTP;
                ds = biz.USP_CATM1203_LST1(oParamDic, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg) && ds != null && ds.Tables.Count > 0)
            {
                dt = AutoNumberTable(ds.Tables[0]);
            }

            return dt;
        }
        #endregion

        #region devCallback2_Callback
        protected void devCallback2_Callback(object source, CallbackEventArgs e)
        {
            bool result = false;
            string errMsg = String.Empty;


            using (CATMBiz biz = new CATMBiz())
            {
                oParamDic.Clear();
                oParamDic["F_DATE"] = hidDATE.Text.ToString();
                oParamDic["F_MACHCD"] = hidMACH2.Text.ToString();

                oParamDic["F_WORKNO"] = hidPK.Text.ToString();
                oParamDic["F_OP"] = e.Parameter;
                oParamDic["F_COMPCD"] = gsCOMPCD;
                oParamDic["F_FACTCD"] = gsFACTCD;

                result = biz.USP_CATM1203_UPD1(oParamDic, out errMsg);


            }

            if (errMsg != "")
            {
                // Grid Callback Init
                devCallback2.JSProperties["cpResultCode"] = "0";
                devCallback2.JSProperties["cpResultMsg"] = errMsg;
                //e.Result = errMsg;

            }
            else
            {
                devCallback2.JSProperties["cpResultCode"] = "";
                devCallback2.JSProperties["cpResultMsg"] = "정상 처리되었습니다.";
            }





            //return result;

        }
        #endregion

        #endregion
    }
}