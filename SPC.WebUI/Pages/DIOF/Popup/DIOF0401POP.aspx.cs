using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.DIOF.Popup
{
    public partial class DIOF0401POP : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        string sRowKeys = String.Empty;
        string sMEASYMD = String.Empty;
        string sMACHIDX = String.Empty;
        string sMACHCD = String.Empty;
        string sMACHNM = String.Empty;
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

            // 점검기준조회
            Retrieve();

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
            sMEASYMD = Request.QueryString.Get("MEASYMD");
            sMACHIDX = Request.QueryString.Get("MACHIDX");
            sMACHCD = Request.QueryString.Get("MACHCD");
            sMACHNM = Request.QueryString.Get("MACHNM");
            sRowKeys = Request.QueryString.Get("KEYS");

            srcF_MEASYMD.Text = sMEASYMD;
            srcF_MACHIDX.Text = sMACHIDX;
            srcF_MACHCD.Text = sMACHCD;
            srcF_MACHNM.Text = sMACHNM;
            srcF_ROWKEYS.Text = sRowKeys;
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
        {
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 점검기준조회
        void Retrieve()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_MEASYMD", GetFromDt());
                oParamDic.Add("F_LANGTP", gsLANGTP);
                ds = biz.QCD_MACH23_MACH26_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (!bExistsDataSet(ds))
                { }
                else
                {
                    DataTable dtRetrieve = ds.Tables[0];
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("F_INSPIDX", typeof(string));
                    dtTemp.Columns.Add("F_INSPNM", typeof(string));
                    dtTemp.Columns.Add("F_NUMBER", typeof(string));
                    dtTemp.Columns.Add("F_NGREMK", typeof(string));
                    dtTemp.Columns.Add("F_RESPTYPE", typeof(string));
                    dtTemp.Columns.Add("F_RESPREMK", typeof(string));
                    dtTemp.Columns.Add("F_RESPDT", typeof(DateTime));
                    dtTemp.Columns.Add("F_RESPUSER", typeof(string));
                    dtTemp.Columns.Add("F_STATUS", typeof(string));


                    string[] aRowKey = new string[4];

                    foreach(string sRowKey in sRowKeys.Split('$'))
                    {
                        aRowKey = sRowKey.Split('|');
                        foreach (DataRow dr in dtRetrieve.Select(String.Format("F_INSPIDX='{0}' AND F_NUMBER='{1}'", aRowKey[0], aRowKey[1])))
                        {
                            dtTemp.Rows.Add(
                                dr["F_INSPIDX"].ToString(),
                                dr["F_INSPNM"].ToString(),
                                dr["F_NUMBER"].ToString(),
                                null, null, null, null, null, null
                                );
                        }
                    }

                    devGrid.DataSource = dtTemp;
                }
                
                if (IsCallback)
                    devGrid.DataBind();
            }
        }
        #endregion

        #region EditForm AspxComboBox DataBind
        /// <summary>
        /// AspxCombox_DataBind
        /// </summary>
        /// <param name="grid">ASPxGridView</param>
        /// <param name="ComboBoxID">string</param>
        /// <param name="CommonCode">string</param>
        void AspxCombox_DataBind(object comboBox, string CommonCode)
        {
            DevExpress.Web.ASPxComboBox ddlComboBox = comboBox as DevExpress.Web.ASPxComboBox;
            ddlComboBox.TextField = String.Format("COMMNM{0}", gsLANGTP);
            ddlComboBox.ValueField = "COMMCD";
            ddlComboBox.DataSource = CachecommonCode["AA"][CommonCode].codeGroup.Values;
            ddlComboBox.DataBind();

            ddlComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid_CellEditorInitialize
        /// <summary>
        /// devGrid_CellEditorInitialize
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewEditorEventArgs</param>
        protected void devGrid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("F_STATUS"))
                AspxCombox_DataBind(e.Editor, "AAG9");
            else if (e.Column.FieldName.Equals("F_RESPTYPE"))
                AspxCombox_DataBind(e.Editor, "AAG8");
            else
                return;
        }
        #endregion

        #endregion
    }
}