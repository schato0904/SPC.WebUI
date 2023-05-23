using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.SYST.Biz;
using SPC.Common.Biz;
using SPC.WERD.Biz;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0107 : WebUIBasePage
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
            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                WERD0107_LST();
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
            //QCM02_LST(srcF_FACTCD);
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

        #region 조회
        void WERD0107_LST()
        {
            string errMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.WERD0107_LST(oParamDic, out errMsg);
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

        #region 공장목록
        void QCM02_LST(DevExpress.Web.ASPxComboBox combo)
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);

                ds = biz.QCM02_LST(oParamDic, out errMsg);
            }

            combo.DataSource = ds;
            combo.ValueField = "F_FACTCD";
            combo.TextField = "F_FACTNM";
            combo.DataBind();

        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region ddlComboBox_DataBound
        /// <summary>
        /// ddlComboBox_DataBound
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void ddlComboBox_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "전체", Value = "", Selected = true });
        }
        #endregion

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
            //if (e.Column.FieldName.Equals("F_FACTCD"))
            //{
            //    DevExpress.Web.ASPxComboBox comboBox = e.Editor as DevExpress.Web.ASPxComboBox;

            //    QCM02_LST(comboBox);
            //    comboBox.SelectedIndex = 0;
            //    comboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
            //}
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
            //DevExpressLib.GetUsedString(new string[] { "F_USEYN" }, e);
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
                    oParamDic.Add("F_PLANYEAR", (Value.NewValues["F_PLANYEAR"] ?? "").ToString());
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    //oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_RATIOYEAR", (Value.NewValues["F_RATIOYEAR"] ?? "").ToString());
                    oParamDic.Add("F_M01", (Value.NewValues["F_M01"] ?? "").ToString());
                    oParamDic.Add("F_M02", (Value.NewValues["F_M02"] ?? "").ToString());
                    oParamDic.Add("F_M03", (Value.NewValues["F_M03"] ?? "").ToString());
                    oParamDic.Add("F_M04", (Value.NewValues["F_M04"] ?? "").ToString());
                    oParamDic.Add("F_M05", (Value.NewValues["F_M05"] ?? "").ToString());
                    oParamDic.Add("F_M06", (Value.NewValues["F_M06"] ?? "").ToString());
                    oParamDic.Add("F_M07", (Value.NewValues["F_M07"] ?? "").ToString());
                    oParamDic.Add("F_M08", (Value.NewValues["F_M08"] ?? "").ToString());
                    oParamDic.Add("F_M09", (Value.NewValues["F_M09"] ?? "").ToString());
                    oParamDic.Add("F_M10", (Value.NewValues["F_M10"] ?? "").ToString());
                    oParamDic.Add("F_M11", (Value.NewValues["F_M11"] ?? "").ToString());
                    oParamDic.Add("F_M12", (Value.NewValues["F_M12"] ?? "").ToString());

                    oSPs[idx] = "USP_QCD7403_INS";
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
                    oParamDic.Add("F_PLANYEAR", (Value.NewValues["F_PLANYEAR"] ?? "").ToString());
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    //oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_RATIOYEAR", (Value.NewValues["F_RATIOYEAR"] ?? "").ToString());
                    oParamDic.Add("F_M01", (Value.NewValues["F_M01"] ?? "").ToString());
                    oParamDic.Add("F_M02", (Value.NewValues["F_M02"] ?? "").ToString());
                    oParamDic.Add("F_M03", (Value.NewValues["F_M03"] ?? "").ToString());
                    oParamDic.Add("F_M04", (Value.NewValues["F_M04"] ?? "").ToString());
                    oParamDic.Add("F_M05", (Value.NewValues["F_M05"] ?? "").ToString());
                    oParamDic.Add("F_M06", (Value.NewValues["F_M06"] ?? "").ToString());
                    oParamDic.Add("F_M07", (Value.NewValues["F_M07"] ?? "").ToString());
                    oParamDic.Add("F_M08", (Value.NewValues["F_M08"] ?? "").ToString());
                    oParamDic.Add("F_M09", (Value.NewValues["F_M09"] ?? "").ToString());
                    oParamDic.Add("F_M10", (Value.NewValues["F_M10"] ?? "").ToString());
                    oParamDic.Add("F_M11", (Value.NewValues["F_M11"] ?? "").ToString());
                    oParamDic.Add("F_M12", (Value.NewValues["F_M12"] ?? "").ToString());

                    oSPs[idx] = "USP_QCD7403_UPD";
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
                    oParamDic.Add("F_PLANYEAR", (Value.Values["F_PLANYEAR"] ?? "").ToString());
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    //oParamDic.Add("F_FACTCD", (Value.NewValues["F_FACTCD"] ?? "").ToString());
                    oParamDic.Add("F_FACTCD", gsFACTCD);

                    oSPs[idx] = "USP_QCD7403_DEL";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };

                WERD0107_LST();
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
            WERD0107_LST();
        }
        #endregion

        protected void devGrid_DataBound(object sender, EventArgs e)
        {
            //var combo = devGrid.Columns["F_FACTCD"] as DevExpress.Web.GridViewDataComboBoxColumn;

            //string errMsg = String.Empty;

            //using (CommonBiz biz = new CommonBiz())
            //{
            //    oParamDic.Clear();
            //    oParamDic.Add("F_COMPCD", gsCOMPCD);

            //    ds = biz.QCM02_LST(oParamDic, out errMsg);
            //}

            //combo.PropertiesComboBox.DataSource = ds;
            //combo.PropertiesComboBox.ValueField = "F_FACTCD";
            //combo.PropertiesComboBox.TextField = "F_FACTNM";
        }


        #endregion
    }
}