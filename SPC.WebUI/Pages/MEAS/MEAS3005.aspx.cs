using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.MEAS.Biz;
using System.Text;
using System.Net;
using System.IO;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS3005 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티

       System.Drawing.Image dtimg
        {
            get
            {
                return (System.Drawing.Image)Session["MEAS3005_IMG"];
            }
            set
            {
                Session["MEAS3005_IMG"] = value;
            }
        }

        DataTable dtChart
        {
            get
            {
                return (DataTable)Session["MEAS3005_0"];
            }
            set
            {
                Session["MEAS3005_0"] = value;
            }
        }

        DataTable dtChart1
        {
            get
            {
                return (DataTable)Session["MEAS3005_1"];
            }
            set
            {
                Session["MEAS3005_1"] = value;
            }
        }

        DataTable dtChart2
        {
            get
            {
                return (DataTable)Session["MEAS3005_2"];
            }
            set
            {
                Session["MEAS3005_2"] = value;
            }
        }

        DataTable dtChart3
        {
            get
            {
                return (DataTable)Session["MEAS3005_3"];
            }
            set
            {
                Session["MEAS3005_3"] = value;
            }
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
                devGrid.JSProperties["cpResultSB"] = "";
                devGrid4.JSProperties["cpResultCode"] = "";
                devGrid4.JSProperties["cpResultMsg"] = "";
                devGrid4.JSProperties["cpResultSB"] = "";
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
        {
            AspxCombox_DataBind(shcF_EQUIPDIVCD, "SS", "SS01", "전체");
            shcF_EQUIPDIVCD.SelectedIndex = 0;

            AspxCombox_DataBind(shcF_FIXTYPECD, "SS", "SS04", "전체");
            shcF_FIXTYPECD.SelectedIndex = 0;
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

       
        #region 계측기리스트
        void MS01M_MEAS3005_LST()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();                
                oParamDic.Add("F_EQUIPNO", shcF_EQUIPNO.Text);
                oParamDic.Add("F_EQUIPDIVCD", (shcF_EQUIPDIVCD.Value??"").ToString());
                oParamDic.Add("F_FIXTYPECD", (shcF_FIXTYPECD.Value??"").ToString());

                ds = biz.MS01M_MEAS3005_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
                devGrid.JSProperties["cpResultSB"] = "";
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            MS01M_MEAS3005_LST();
            devGrid.DataBind();
        }
        #endregion

        protected void EQUIPCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_EQUIPNO", shcF_EQUIPNO.Text);

                ds = biz.MS01M_TBL_POPUP_LST(oParamDic, out errMsg);
            }

            string EQUIPNO = String.Empty;
            string EQUIPNM = String.Empty;

            if (true == bExistsDataSet(ds))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        EQUIPNO = (string)dtRow["F_EQUIPNO"];
                        EQUIPNM = (string)dtRow["F_EQUIPNM"];
                    }
                }
            }

            EQUIPCallback.JSProperties["cpEQUIPNO"] = EQUIPNO;
            EQUIPCallback.JSProperties["cpEQUIPNM"] = EQUIPNM;
        }

        #endregion

        protected void devGrid3_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", e.Parameters);

                ds = biz.MS01D4_LST(oParamDic, out errMsg);
            }

            devGrid3.DataSource = ds;
            dtChart2 = ds.Tables[0].Copy();

            //if (!String.IsNullOrEmpty(errMsg))
            //{
            //    // Grid Callback Init
            //    devGrid3.JSProperties["cpResultCode"] = "0";
            //    devGrid3.JSProperties["cpResultMsg"] = errMsg;
            //}

            devGrid3.DataBind();
        }

        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", e.Parameters);

                ds = biz.MS01D3_LST(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            dtChart1 = ds.Tables[0].Copy();

            //if (!String.IsNullOrEmpty(errMsg))
            //{
            //    // Grid Callback Init
            //    devGrid2.JSProperties["cpResultCode"] = "0";
            //    devGrid2.JSProperties["cpResultMsg"] = errMsg;
            //}

            devGrid2.DataBind();
        }

        protected void devGrid4_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", e.Parameters);

                ds = biz.MS01D1_LST(oParamDic, out errMsg);
            }

            dtChart3 = ds.Tables[0].Copy();
            dtChart = ds.Tables[1].Copy();

            dtimg = null;

            if (ds.Tables[2].Rows.Count > 0)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["defaultCompSubPath"];
                string fullpath = string.Format(path, gsCOMPCD, "M") + ds.Tables[2].Rows[0][0].ToString();
                dtimg = System.Drawing.Image.FromFile(fullpath);
            }
            else {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                System.Drawing.Image blankImg = System.Drawing.Image.FromFile(context.Server.MapPath("~/Resources/images/blank.png"));
                dtimg = blankImg;
            }

            devGrid4.DataSource = ds;
            devGrid4.DataBind();

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS01MID", e.Parameters);

                ds = biz.MS01M_MEAS3005_LST(oParamDic, out errMsg);
            }


            StringBuilder sb = new StringBuilder();
            DataTable dt = ds.Tables[0].Copy();
            if (dt.Rows.Count > 0)
            {
                DataRow dtRow = dt.Rows[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append("|");
                    sb.Append(dtRow[i].ToString());
                }

                devGrid4.JSProperties["cpResultSB"] = sb.ToString();
            }
            else
            {
                devGrid4.JSProperties["cpResultSB"] = "";
            }
        }
    }
}