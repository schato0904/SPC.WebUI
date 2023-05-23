using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using SPC.WebUI.Common;
using SPC.MEAS.Biz;
using SPC.Common.Biz;


namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS9004 : WebUIBasePage
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
            // 목록 조회
            MEAS9004_LST();
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
            MEAS9004_LST();
        }
        #endregion

        #endregion

        #region 페이지 기본 함수

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

        #region 회사코드 정보를 불러온다
        void COMPCODE_LST(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                ds = biz.MEAS9004_FACTCHK(oParamDic, out errMsg);
            }

            FACT_GBN.TextField = "F_FACTNMKR";
            FACT_GBN.ValueField = "F_FACTCD";
            FACT_GBN.DataSource = ds;
            FACT_GBN.DataBind();
            FACT_GBN.DataBindItems();
            FACT_GBN.SelectedIndex = 0;
        }
        #endregion

        #region 공정정보를 구한다
        void MEAS9004_LST()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", (FACT_GBN.Value ?? "").ToString());
                ds = biz.MEAS9004_LST(oParamDic, out errMsg);
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
                devGrid.DataBind();
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
            //e.NewValues["F_USEYN"] = true;
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
            if (!e.Column.FieldName.Equals("F_FACTNMKR")) return;

            if (e.Column.FieldName.Equals("F_FACTNMKR"))
            {
                DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;
                COMPCODE_LST(comboBox);

                comboBox.TextField = "F_FACTNMKR";
                comboBox.ValueField = "F_FACTCD";
                comboBox.DataSource = ds;
                comboBox.DataBind();
                comboBox.DataBindItems();
                comboBox.SelectedIndex = 0;
                comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            }

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
        /// devGrid_BatchUpdate
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxDataBatchUpdateEventArgs</param>
        protected void devGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
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
                    oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTNMKR"] ?? "").ToString());
                    oParamDic.Add("F_TEAMCD", (Value.NewValues["F_TEAMCD"] ?? "").ToString());
                    oParamDic.Add("F_TEAMNM", (Value.NewValues["F_TEAMNM"] ?? "").ToString());
                    oParamDic.Add("F_USEYN", (Value.NewValues["F_USEYN"] ?? "").ToString());
                    oParamDic.Add("F_SORTNO", (Value.NewValues["F_SORTNO"] ?? "").ToString());
                    
                    oSPs[idx] = "USP_MEAS9004_INS";
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
                    oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_TEAMCD", (Value.NewValues["F_TEAMCD"] ?? "").ToString());
                    oParamDic.Add("F_TEAMNM", (Value.NewValues["F_TEAMNM"] ?? "").ToString());
                    oParamDic.Add("F_USEYN", (Value.NewValues["F_USEYN"] ?? "").ToString());
                    oParamDic.Add("F_SORTNO", (Value.NewValues["F_SORTNO"] ?? "").ToString());

                    oSPs[idx] = "USP_MEAS9004_UPD";
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
                    oParamDic.Add("F_FACTCD", (Value.Values["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_TEAMCD", (Value.Values["F_TEAMCD"] ?? "").ToString());

                    oSPs[idx] = "USP_MEAS9004_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                bExecute = biz.PROC_SYPGM01_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                // 프로그래목록을 구한다
                MEAS9004_LST();
            }

            devGrid.JSProperties["cpResultCode"] = procResult[0];
            devGrid.JSProperties["cpResultMsg"] = procResult[1];
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
            // 공정목록을 구한다
            MEAS9004_LST();
        }
        #endregion

        #region ddlLINEEdit_DataBound
        /// <summary>
        /// ddlLINEEdit_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlLINEEdit_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #region ddlLINEEdit_Callback
        /// <summary>
        /// ddlLINEEdit_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CallbackEventArgsBase</param>
        protected void ddlLINEEdit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] oParam = e.Parameter.Split('|');
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Clear();
            //QCD73_LST(oParam[0], comboBox);
            comboBox.DataBind();

            if (!String.IsNullOrEmpty(oParam[1]))
                comboBox.SelectedIndex = comboBox.Items.FindByValue(oParam[1]).Index;
            else
                comboBox.SelectedIndex = 0;
        }
        #endregion

        #endregion
    }
}