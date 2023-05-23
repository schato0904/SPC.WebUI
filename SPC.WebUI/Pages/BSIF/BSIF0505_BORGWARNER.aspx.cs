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

namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0505_BORGWARNER : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
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
            if (!Page.IsCallback)
            {
                MEAINSP_LST();
                MEAINSP_SEL();
            }
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
        
        #region 사용자 정의 함수

        #region 저장 된 항목 세팅
        void MEAINSP_SEL()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LOCATIONGBN", cbLOCATION.SelectedItem.Value.ToString());

                ds = biz.BSIF0505_BORGWARNER_LST(oParamDic, out errMsg);
            }
            if (ds.Tables.Count > 0)
            {
                string d1 = ds.Tables[0].Rows[0]["F_M11"].ToString();
                string d2 = ds.Tables[0].Rows[0]["F_M21"].ToString();
                string d3 = ds.Tables[0].Rows[0]["F_M31"].ToString();
                string d4 = ds.Tables[0].Rows[0]["F_M32"].ToString();
                string d5 = ds.Tables[0].Rows[0]["F_M33"].ToString();
                string d6 = ds.Tables[0].Rows[0]["F_M41"].ToString();
                string d7 = ds.Tables[0].Rows[0]["F_M42"].ToString();
                string d8 = ds.Tables[0].Rows[0]["F_M43"].ToString();
                string d9 = ds.Tables[0].Rows[0]["F_M51"].ToString();
                string d10 = ds.Tables[0].Rows[0]["F_M52"].ToString();
                string d11 = ds.Tables[0].Rows[0]["F_M53"].ToString();
                string d12 = ds.Tables[0].Rows[0]["F_M61"].ToString();

                cbM11.SelectedItem = cbM11.Items.FindByValue(d1);
                cbM21.SelectedItem = cbM21.Items.FindByValue(d2);
                cbM31.SelectedItem = cbM31.Items.FindByValue(d3);
                cbM32.SelectedItem = cbM32.Items.FindByValue(d4);
                cbM33.SelectedItem = cbM33.Items.FindByValue(d5);
                cbM41.SelectedItem = cbM41.Items.FindByValue(d6);
                cbM42.SelectedItem = cbM42.Items.FindByValue(d7);
                cbM43.SelectedItem = cbM43.Items.FindByValue(d8);
                cbM51.SelectedItem = cbM51.Items.FindByValue(d9);
                cbM52.SelectedItem = cbM52.Items.FindByValue(d10);
                cbM53.SelectedItem = cbM53.Items.FindByValue(d11);
                cbM61.SelectedItem = cbM61.Items.FindByValue(d12);
            }
            else
            {
                cbM11.Value = "";
                cbM21.Value = "";
                cbM31.Value = "";
                cbM32.Value = "";
                cbM33.Value = "";
                cbM41.Value = "";
                cbM42.Value = "";
                cbM43.Value = "";
                cbM51.Value = "";
                cbM52.Value = "";
                cbM53.Value = "";
                cbM61.Value = "";
            }
        }
        #endregion

        #region 지정 된 공정의 항목 조회
        void MEAINSP_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LOCATIONGBN", hidLOCATION.Text.ToString());

                ds = biz.BSIF0505_BORGWARNER_MEAINSP_LST(oParamDic, out errMsg);
            }

            if (ds.Tables.Count > 0)
            {
                cbM11.DataSource = ds;
                cbM21.DataSource = ds;
                cbM31.DataSource = ds;
                cbM32.DataSource = ds;
                cbM33.DataSource = ds;
                cbM41.DataSource = ds;
                cbM42.DataSource = ds;
                cbM43.DataSource = ds;
                cbM51.DataSource = ds;
                cbM52.DataSource = ds;
                cbM53.DataSource = ds;
                cbM61.DataSource = ds;

                cbM11.DataBind();
                cbM21.DataBind();
                cbM31.DataBind();
                cbM32.DataBind();
                cbM33.DataBind();
                cbM41.DataBind();
                cbM42.DataBind();
                cbM43.DataBind();
                cbM51.DataBind();
                cbM52.DataBind();
                cbM53.DataBind();
                cbM61.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region 저장 Callback
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_M11", (cbM11.Value ?? "").ToString());
                oParamDic.Add("F_M21", (cbM21.Value ?? "").ToString());
                oParamDic.Add("F_M31", (cbM31.Value ?? "").ToString());
                oParamDic.Add("F_M32", (cbM32.Value ?? "").ToString());
                oParamDic.Add("F_M33", (cbM33.Value ?? "").ToString());
                oParamDic.Add("F_M41", (cbM41.Value ?? "").ToString());
                oParamDic.Add("F_M42", (cbM42.Value ?? "").ToString());
                oParamDic.Add("F_M43", (cbM43.Value ?? "").ToString());
                oParamDic.Add("F_M51", (cbM51.Value ?? "").ToString());
                oParamDic.Add("F_M52", (cbM52.Value ?? "").ToString());
                oParamDic.Add("F_M53", (cbM53.Value ?? "").ToString());
                oParamDic.Add("F_M61", (cbM61.Value ?? "").ToString());
                oParamDic.Add("F_LOCATIONGBN", cbLOCATION.Value.ToString());

                biz.BSIF0505_BORGWARNER_UPD(oParamDic, out errMsg);

                if (!String.IsNullOrEmpty(errMsg))
                {
                    // Grid Callback Init
                    pnlChart.JSProperties["cpResultCode"] = "0";
                    pnlChart.JSProperties["cpResultMsg"] = errMsg;
                }

            }
        }
        #endregion

        #region 항목 콤보박스 패널 Callback
        protected void cbM_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            MEAINSP_LST();
            MEAINSP_SEL();
        }
        #endregion

        #endregion
    }
}