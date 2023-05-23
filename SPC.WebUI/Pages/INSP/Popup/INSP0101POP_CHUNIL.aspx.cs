using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.INSP.Biz;
using SPC.Common.Biz;

namespace SPC.WebUI.Pages.INSP.Popup
{
    public partial class INSP0101POP_CHUNIL : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Control userControl = null;
        string m_sItemCD = String.Empty;
        string m_sLotNO = String.Empty;
        string m_sWORKDATE = String.Empty;        
        DevExpress.Web.ASPxTextBox textBox = null;
        DevExpress.Web.ASPxMemo memoBox = null;
        DevExpress.Web.ASPxDateEdit dateEdit = null;
        DevExpress.Web.ASPxCheckBoxList checkBoxList = null;
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
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
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
            m_sItemCD = Request.QueryString.Get("ITEMCD") ?? "";
            m_sLotNO = Request.QueryString.Get("LOTNO") ?? "";
            m_sWORKDATE = Request.QueryString.Get("WORKDATE") ?? "";  
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
            ddlCustomer.NullText = "선택하세요";
            ddlCustomer.TextField = "F_CODENM";
            ddlCustomer.ValueField = "F_CODE";
            ddlCustomer.DataSource = CUSTOMER_REPORT_LST();
            ddlCustomer.DataBind();

            //ddlCustomer.Items.Insert(0, new DevExpress.Web.ListEditItem("기본제공양식(CTF)", "--|cybertechfriend"));
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region Load User Control
        void LoadUserControl(string sUserControlID)
        {
            if (sUserControlID.Equals("cybertechfriend") || String.IsNullOrEmpty(sUserControlID))
                userControl = Page.LoadControl("~/Resources/report/head/report.ascx");
            else
            {
                string userControlPath = String.Format("~/Resources/report/head/{0}/{1}.ascx", gsLOGINPGMID, sUserControlID);
                //임시로.....
                //string userControlPath = String.Format("~/Resources/report/head/{0}/{1}.ascx", "hkpi", sUserControlID);

                if (!System.IO.File.Exists(Server.MapPath(userControlPath)))
                    userControl = Page.LoadControl("~/Resources/report/head/report.ascx");
                else
                    userControl = Page.LoadControl(userControlPath);
            }

            userControl.ID = "reportHead";

            pHolder.Controls.Add(userControl);

            // 업체테이블
            DataSet dsCOMP = QCM01_LST();
            if (bExistsDataSet(dsCOMP))
            {
                DataRow drCOMP = dsCOMP.Tables[0].Rows[0];

                if (true == bExistsTextBox("COMPNM"))
                {
                    textBox = userControl.FindControl("txtCOMPNM") as DevExpress.Web.ASPxTextBox;
                    textBox.Text = drCOMP["F_COMPNMKR"].ToString();
                }
            }

            // 품목테이블
            DataSet dsITEM = QCD01_LST();
            if (bExistsDataSet(dsITEM))
            {
                DataRow drITEM = dsITEM.Tables[0].Rows[0];

                if (true == bExistsTextBox("ITEMCD"))
                    SetTextBoxValue(drITEM, "ITEMCD");
                if (true == bExistsTextBox("ITEMNM"))
                    SetTextBoxValue(drITEM, "ITEMNM");
                if (true == bExistsTextBox("MODELCD"))
                    SetTextBoxValue(drITEM, "MODELCD");
                if (true == bExistsTextBox("MODELNM"))
                    SetTextBoxValue(drITEM, "MODELNM");
            }

            // 성적서 헤더 테이블
            DataSet dsHead = INS01_LST(hidCUSTOMCD.Text);

            if (bExistsDataSet(dsHead))
            {
                DataRow drHead = dsHead.Tables[0].Rows[0];

                SetTextBoxValue(drHead, "COMPNM");
                SetTextBoxValue(drHead, "REVNO");
                SetDateEditValue(drHead, "REVDT");
                SetTextBoxValue(drHead, "REVDESC");
                SetTextBoxValue(drHead, "REVMANAGER");
                SetTextBoxValue(drHead, "REVAPPROVER");
                SetTextBoxValue(drHead, "EONO");
                //SetTextBoxValue(drHead, "LOTNO1");
                //SetTextBoxValue(drHead, "LOTNO2");
                //SetTextBoxValue(drHead, "PONO");
                SetTextBoxValue(drHead, "USAGE");
                SetTextBoxValue(drHead, "INSPECTION");
                SetTextBoxValue(drHead, "QUANTITY");
                SetTextBoxValue(drHead, "TYPE");
                SetTextBoxValue(drHead, "PLACE");
                SetTextBoxValue(drHead, "FORMAT");
                // 용도
                SetUsage(drHead);
                // 측정방법
                SetInspection(drHead);
            }

            // 가공LOT
            if (true == bExistsTextBox("LOTNO2"))
            {
                textBox = userControl.FindControl("txtLOTNO2") as DevExpress.Web.ASPxTextBox;
                textBox.Text = m_sLotNO;
            }
        }
        #endregion

        #region 사용자입력값 설정
        void SetTextBoxValue(DataRow dr, string controlID)
        {
            if (true == bExistsTextBox(controlID))
            {
                textBox = userControl.FindControl(String.Format("txt{0}", controlID)) as DevExpress.Web.ASPxTextBox;
                textBox.Text = dr[String.Format("F_{0}", controlID)].ToString();
            }
        }

        void SetDateEditValue(DataRow dr, string controlID)
        {
            if (true == bExistsTextBox(controlID))
            {
                dateEdit = userControl.FindControl(String.Format("txt{0}", controlID)) as DevExpress.Web.ASPxDateEdit;
                DateTime oDateTime = DateTime.Now;
                string sDateTime = dr[String.Format("F_{0}", controlID)].ToString();
                if (DateTime.TryParse(sDateTime, out oDateTime))
                    dateEdit.Date = oDateTime;
            }
        }

        void SetUsage(DataRow dr)
        {
            string sUSAGE = dr["F_USAGE"].ToString();
            if (!String.IsNullOrEmpty(sUSAGE))
            {
                if (true == bExistsTextBox("USAGE") && true == bExistsCheckBoxList("USAGE"))
                {
                    string[] sValues = sUSAGE.Split(',');
                    string[] sItems = sValues[0].Split('|');

                    checkBoxList = userControl.FindControl("chkUSAGE") as DevExpress.Web.ASPxCheckBoxList;

                    foreach (DevExpress.Web.ListEditItem oItems in checkBoxList.Items)
                    {
                        oItems.Selected = false;
                        foreach (string sItem in sItems)
                        {
                            if (sItem == oItems.Value.ToString())
                                oItems.Selected = true;
                        }
                    }

                    if (sValues.Length == 2)
                    {
                        textBox = userControl.FindControl("txtUSAGE") as DevExpress.Web.ASPxTextBox;
                        textBox.Text = sValues[1];
                    }
                }
                else if (true == bExistsTextBox("USAGE"))
                {
                    textBox = userControl.FindControl("txtUSAGE") as DevExpress.Web.ASPxTextBox;
                    textBox.Text = dr["F_USAGE"].ToString();
                }
                else if (true == bExistsCheckBoxList("USAGE"))
                {
                    string[] sValues = sUSAGE.Split(',');

                    checkBoxList = userControl.FindControl("chkUSAGE") as DevExpress.Web.ASPxCheckBoxList;

                    foreach (DevExpress.Web.ListEditItem oItems in checkBoxList.Items)
                    {
                        oItems.Selected = false;
                        foreach (string sItem in sValues)
                        {
                            if (sItem == oItems.Value.ToString())
                                oItems.Selected = true;
                        }
                    }
                }
            }
        }

        void SetInspection(DataRow dr)
        {
            string sUSAGE = dr["F_INSPECTION"].ToString();
            if (!String.IsNullOrEmpty(sUSAGE))
            {
                string[] sValues = sUSAGE.Split(',');

                if (true == bExistsCheckBoxList("INSPECTION"))
                {
                    checkBoxList = userControl.FindControl("chkINSPECTION") as DevExpress.Web.ASPxCheckBoxList;

                    foreach (DevExpress.Web.ListEditItem oItems in checkBoxList.Items)
                    {
                        oItems.Selected = false;
                        foreach (string sItem in sValues)
                        {
                            if (sItem == oItems.Value.ToString())
                                oItems.Selected = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 고객사목록
        DataSet CUSTOMER_REPORT_LST()
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.CUSTOMER_REPORT_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 업체정보를 구한다
        DataSet QCM01_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_MASTERCHK", "0");
                ds = biz.QCM01_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목 정보를 구한다
        DataSet QCD01_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_ITEMCD", m_sItemCD);

                ds = biz.GetQCD01_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 헤더 정보를 구한다(최근 1개)
        DataSet INS01_LST(string sCUSTOMERCD)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_CUSTOMCD", sCUSTOMERCD);

                ds = biz.INS01_LST(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서저장
        void SaveReport()
        {
            // 마스터 키를 구한다
            string sGROUPCD = String.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), UF.String.GetRandomString()).Substring(0, 20);

            ContentPlaceHolder cph = Page.Master.FindControl("cphBody") as ContentPlaceHolder;

            List<string> oSPs = new List<string>();
            List<object> oParameters = new List<object>();

            #region 검사성적서 헤더
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_GROUPCD", sGROUPCD);
            oParamDic.Add("F_CUSTOMCD", hidCUSTOMCD.Text);
            oParamDic.Add("F_REPORTCD", hidREPORTCD.Text);
            oParamDic.Add("F_ITEMCD", m_sItemCD);
            oParamDic.Add("F_EONO", memoETC.Text);
            oParamDic.Add("F_USER", gsUSERID);
            oParamDic.Add("F_DATE", m_sWORKDATE);
            AddHeadParameter(oParamDic, cph, "COMPNM");
            AddHeadParameter(oParamDic, cph, "ORDER");
            AddHeadParameter(oParamDic, cph, "REVNO");
            AddHeadParameter(oParamDic, cph, "REVDT");
            AddHeadParameter(oParamDic, cph, "REVDESC");
            AddHeadParameter(oParamDic, cph, "REVMANAGER");
            AddHeadParameter(oParamDic, cph, "REVAPPROVER");
            //AddHeadParameter(oParamDic, cph, "EONO");
            AddHeadParameter(oParamDic, cph, "LOTNO1");
            AddHeadParameter(oParamDic, cph, "LOTNO2");
            AddHeadParameter(oParamDic, cph, "PONO");
            AddHeadParameter(oParamDic, cph, "USAGE");
            AddHeadParameter(oParamDic, cph, "INSPECTION");
            AddHeadParameter(oParamDic, cph, "QUANTITY");
            AddHeadParameter(oParamDic, cph, "TYPE");
            AddHeadParameter(oParamDic, cph, "PLACE");
            //AddHeadParameter(oParamDic, cph, "DATE");
            AddHeadParameter(oParamDic, cph, "FORMAT");
            oSPs.Add("USP_INS01_INS_CHUNIL");
            oParameters.Add(oParamDic);
            #endregion

            #region 검사성적서그룹
            foreach (string Value in hidKEY.Text.Split(','))
            {
                string[] oParam = Value.Split('|');
                oParamDic = new Dictionary<string, string>();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_GROUPCD", sGROUPCD);
                oParamDic.Add("F_WORKDATE", oParam[0]);
                oParamDic.Add("F_BANCD", oParam[1]);
                oParamDic.Add("F_LINECD", oParam[2]);
                oParamDic.Add("F_ITEMCD", oParam[3]);
                oParamDic.Add("F_WORKCD", oParam[4]);
                oParamDic.Add("F_TSERIALNO", oParam[5]);
                oParamDic.Add("F_EXTCD", oParam[7]);
                //oParamDic.Add("F_WORKTIME", oParam[8]);
                oParamDic.Add("F_FIRSTITEM", oParam[8]);
                oParamDic.Add("F_TAGAKNO", oParam[9]);
                oParamDic.Add("F_WORKMAN", oParam[10]);
                oSPs.Add("USP_INS02_INS_CHUNIL");
                oParameters.Add(oParamDic);
            }
            #endregion

            #region 검사성적서데이터
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_GROUPCD", sGROUPCD);
            oParamDic.Add("F_LANGCD", gsLANGCD);

            oSPs.Add("USP_INS03_INS_CHUNIL");
            oParameters.Add(oParamDic);
            #endregion

            #region Database Execute
            bool bExecute = false;
            string resultMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                bExecute = biz.PROC_INSPECTION_BATCH(oSPs.ToArray(), oParameters.ToArray(), out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            #endregion
        }

        void AddHeadParameter(Dictionary<string, string> oParamDictionary, ContentPlaceHolder cph, string columnID)
        {
            
            //if (columnID == "EONO")
            //{
            //    memoBox = cph.FindControl(String.Format("hid{0}", columnID)) as DevExpress.Web.ASPxMemo;
            //    oParamDictionary.Add(String.Format("F_{0}", columnID), memoBox.Text);
            //}
            //else
            //{
                textBox = cph.FindControl(String.Format("hid{0}", columnID)) as DevExpress.Web.ASPxTextBox;
                oParamDictionary.Add(String.Format("F_{0}", columnID), textBox.Text);
            //}
        }
        #endregion

        #region Check Exists Contron In UserControl
        bool bExistsTextBox(string controlID)
        {
            return userControl.FindControl(String.Format("txt{0}", controlID)) != null;
        }
        bool bExistsCheckBoxList(string controlID)
        {
            return userControl.FindControl(String.Format("chk{0}", controlID)) != null;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region callbackPanel_Callback
        /// <summary>
        /// callbackPanel_Callback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgsBase</param>
        protected void callbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string sValue = e.Parameter;

            if (!String.IsNullOrEmpty(sValue))
            {
                string[] sValues = sValue.Split('|');
                hidCUSTOMCD.Text = sValues[0];
                hidREPORTCD.Text = sValues[1];
                LoadUserControl(sValues[2]);
            }
            else
                pHolder.Controls.Remove(userControl);
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
            SaveReport();
        }
        #endregion

        #endregion
    }
}