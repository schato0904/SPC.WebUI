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
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using DevExpress.Web;


namespace SPC.WebUI.Pages.BSIF
{
    public partial class BSIF0503_ANDON : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        #endregion

        #region 프로퍼티
        DataTable dt1
        {
            get
            {
                return (DataTable)Session["BSIF0503_ANDON_1"];
            }
            set
            {
                Session["BSIF0503_ANDON_1"] = value;
            }
        }

        DataTable dt2
        {
            get
            {
                return (DataTable)Session["BSIF0503_ANDON_2"];
            }
            set
            {
                Session["BSIF0503_ANDON_2"] = value;
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

            //if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            //{
            //    // 사용자 정보를 구한다
            //    ANDON_LST();
            //}


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
            //if (!String.IsNullOrEmpty(hidGridAction2.Text) || !hidGridAction2.Text.Equals("false"))
            //{
            //    DETAIL_LST();
            //}

            if (!IsPostBack)
            {
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";

                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
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
            dt1 = null;
            dt2 = null; 
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

        #region 반코드 정보를 불러온다
        DataTable QCD72_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_STATUS", "1");

                ds = biz.GetQCD72_LST(oParamDic, out errMsg);
            }

            return ds.Tables[0];
        }
        #endregion

        #region 사용자 정보를 구한다
        void ANDON_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.SYUSR01_ANDON_LST(oParamDic, out errMsg);
            }

            dt1 = ds.Tables[0];
            //devGrid.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (IsCallback)
                //    devGrid.DataBind();
            }
        }
        #endregion

        #region 상세조회
        void DETAIL_LST()
        {
            string errMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_USERID", hidUserID.Text);
                //oParamDic.Add("F_USERID", "YJ0018");

                ds = biz.SYUSR01_ANDON_DETAIL_LST(oParamDic, out errMsg);
            }

            dt2 = ds.Tables[0];
            //devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (IsCallback)
                //    devGrid2.DataBind();
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid2_CellEditorInitialize
        /// <summary>
        /// devGrid2_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewEditorEventArgs</param>
        protected void devGrid2_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
        }
        #endregion

        #region devGrid BatchUpdate
        /// <summary>
        /// devGrid2_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid2_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;
            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            #region Batch Insert
            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_JUYAKIND", (Value.NewValues["F_JUYAKIND"] ?? "").ToString());
                    oParamDic.Add("F_ANDONCD", (Value.NewValues["F_ANDONCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_USERID", hidUserID.Text);
                    oParamDic.Add("F_INSUSER", gsUSERID);

                    oSPs[idx] = "USP_ANDON_INS";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Update
            if (e.UpdateValues.Count > 0)
            {
                foreach (var Value in e.UpdateValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.NewValues["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_JUYAKIND", (Value.NewValues["F_JUYAKIND"] ?? "").ToString());
                    oParamDic.Add("F_ANDONCD", (Value.NewValues["F_ANDONCD"] ?? "").ToString());
                    oParamDic.Add("F_STATUS", (Value.NewValues["F_STATUS"] ?? "").ToString());
                    oParamDic.Add("F_USERID", hidUserID.Text);
                    oParamDic.Add("F_INSUSER", gsUSERID);

                    oSPs[idx] = "USP_ANDON_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Batch Delete
            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_BANCD", (Value.Values["F_BANCD"] ?? "").ToString());
                    oParamDic.Add("F_JUYAKIND", (Value.Values["F_JUYAKIND"] ?? "").ToString());
                    oParamDic.Add("F_ANDONCD", (Value.Values["F_ANDONCD"] ?? "").ToString());
                    oParamDic.Add("F_USERID", hidUserID.Text);

                    oSPs[idx] = "USP_ANDON_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCD012_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 사용자 정보를 구한다
                ANDON_LST();
                DETAIL_LST();
            }

            devGrid2.JSProperties["cpResultCode"] = procResult[0];
            devGrid2.JSProperties["cpResultMsg"] = procResult[1];
            #endregion

            e.Handled = true;
        }
        #endregion

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 사용자 정보를 구한다
            ANDON_LST();
            devGrid.DataBind();
        }
        #endregion

        #region devGrid2 CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            DETAIL_LST();
            devGrid2.DataBind();
        }
        #endregion

        protected void devGrid2_DataBinding(object sender, EventArgs e)
        {
            devGrid2.DataSource = dt2;
        }

        protected void devGrid_DataBinding(object sender, EventArgs e)
        {
            devGrid.DataSource = dt1;
        }

        #region devGrid2_InitNewRow
        /// <summary>
        /// devGrid2_InitNewRow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataInitNewRowEventArgs</param>
        protected void devGrid2_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["F_STATUS"] = 1;
        }
        #endregion

        protected void devGrid2_DataBound(object sender, EventArgs e)
        {
            var combo = devGrid2.Columns["F_BANCD"] as DevExpress.Web.GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.TextField = "F_BANNM";
            combo.PropertiesComboBox.ValueField = "F_BANCD";
            combo.PropertiesComboBox.DataSource = QCD72_LST();

            var combo2 = devGrid2.Columns["F_JUYAKIND"] as DevExpress.Web.GridViewDataComboBoxColumn;
            combo2.PropertiesComboBox.Items.Clear();
            combo2.PropertiesComboBox.Items.Add("주간", "0");
            combo2.PropertiesComboBox.Items.Add("야간", "1");

            var combo3 = devGrid2.Columns["F_ANDONCD"] as DevExpress.Web.GridViewDataComboBoxColumn;
            combo3.PropertiesComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            combo3.PropertiesComboBox.ValueField = "COMMCD";
            combo3.PropertiesComboBox.DataSource = CachecommonCode["AA"]["AAH1"].codeGroup.Values;
        }

        #endregion
    }
}