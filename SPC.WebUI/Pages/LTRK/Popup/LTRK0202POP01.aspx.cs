using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.LTRK.Biz;

namespace SPC.WebUI.Pages.LTRK.Popup
{
    public partial class LTRK0202POP01 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string sORDERGROUP = String.Empty;
        protected string sORDERDATE = String.Empty;
        protected string sORDERNO = String.Empty;
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

            // 작업지시 정보 조회
            QPM22_LST();

            // 조회
            QPM23_LST();

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
            sORDERGROUP = Request.Params.Get("pGROUP");
            sORDERDATE = Request.Params.Get("pDATE");
            sORDERNO = Request.Params.Get("pNO");
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

        #region 작업지시 정보 조회
        void QPM22_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERGROUP", sORDERGROUP);
                oParamDic.Add("F_ORDERDATE", sORDERDATE);
                oParamDic.Add("F_ORDERNO", sORDERNO);
                ds = biz.QPM22_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", String.Format("fn_OnLoadError('작업지시 데이터를 구하는 중 장애가 발생하였습니다.\\r{0}');", errMsg.Replace("'", "")), true);
            }
            else if (!bExistsDataSet(ds))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "fn_OnLoadError('작업지시 데이터를 구하는 중 장애가 발생하였습니다.\\r데이터가 없습니다');", true);
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                srcF_ORDERGROUP.Text = sORDERGROUP;
                srcF_ORDERDATE.Text = sORDERDATE;
                srcF_ORDERNO.Text = sORDERNO;
                srcF_ORDERCNT.Value = Convert.ToDecimal(dr["F_ORDERCNT"]);
                srcF_USEDCNT.Value = Convert.ToDecimal(dr["F_USEDCNT"]);
                srcF_UNITNM.Text = dr["F_UNITNM"].ToString();
                srcF_WORKCD.Text = dr["F_WORKCD"].ToString();
                srcF_WORK.Text = String.Format("[{0}] {1}", dr["F_WORKCD"], dr["F_WORKNM"]);
                srcF_EQUIPCD.Text = dr["F_EQUIPCD"].ToString();
                srcF_EQUIP.Text = String.Format("[{0}] {1}", dr["F_EQUIPCD"], dr["F_EQUIPNM"]);
                srcF_ITEMCD.Text = dr["F_ITEMCD"].ToString();
                srcF_ITEM.Text = String.Format("[{0}] {1}", dr["F_ITEMCD"], dr["F_ITEMNM"]);
                srcF_GUBNCD.Text = dr["F_GUBN"].ToString();
                srcF_GUBN.Text = String.Format("[{0}] {1}", dr["F_GUBN"], dr["F_GUBNNM"]);
            }
        }
        #endregion

        #region 조회
        void QPM23_LST()
        {
            string errMsg = String.Empty;

            using (LTRKBiz biz = new LTRKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ORDERNO", srcF_ORDERNO.Text);
                oParamDic.Add("F_ITEMCD", srcF_ITEMCD.Text);
                ds = biz.QPM23_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

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

        #endregion

        #region 사용자이벤트

        #region devGrid_CustomColumnDisplayText
        /// <summary>
        /// devGrid_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_GROUP"))
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
        }
        #endregion

        #endregion
    }
}