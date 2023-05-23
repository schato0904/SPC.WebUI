using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.WERD.Biz;
using Newtonsoft.Json;
using DevExpress.Web;
using SPC.BSIF.Biz;
using System.Text;

namespace SPC.WebUI.Pages.WERD
{
    public partial class WERD0201 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        public static string strKey = "";
        public string captionF_VALUE = "X1";
        public int headerColCnt = 5;
        public int maxColCnt = 20;
        public int rowCnt = 0;
        public const int unitWidth = 60;
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

                // Grid Callback Init                
                devGrid.JSProperties["cpResultCode"] = "";
                devGrid.JSProperties["cpResultMsg"] = "";
                devGrid.JSProperties["cpResult"] = "";
                devCallback.JSProperties["cpResultCode"] = "";
                devCallback.JSProperties["cpResultMsg"] = "";
                devCallback.JSProperties["cpResult"] = "";
                devCallback.JSProperties["cpResultAction"] = "";
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
            //srcF_WORKDATE.UseMaskBehavior = true;
            //srcF_WORKDATE.EditFormat = EditFormat.Custom;
            //srcF_WORKDATE.EditFormatString = "dd/MM/yyyy hh:mm tt";
            //srcF_WORKDATE.TimeSectionProperties.Visible = true;
            //srcF_WORKDATE.TimeSectionProperties.Adaptive = true;
            //srcF_WORKDATE.TimeEditProperties.EditFormat = EditFormat.Custom;
            //srcF_WORKDATE.TimeEditProperties.EditFormatString = "hh:mm tt";
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

        #region 공정검사 목록을 구한다
        void QWK110_GRID_LST()
        {
            string errMsg = string.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FRDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());
                oParamDic.Add("F_ITEMCD", GetItemCD());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD());
                oParamDic.Add("F_LOTNO", this.srcF_LOTNO_SEARCH.Text);

                ds = biz.WERD0201_QWK110_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = ds;

            if (!string.IsNullOrEmpty(errMsg))
            {
                // grid callback init
                devGrid.JSProperties["cpResultCode"] = "0";
                devGrid.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                //if (IsCallback)
                devGrid.DataBind();
                devCallback.JSProperties["cpResultCode"] = "";
            }
        }
        #endregion

        #region 공정검사목록
        protected void QWK011_DETAIL_LST(string[] strParams)
        {
            string errMsg = string.Empty;

            using (WERDBiz biz = new WERDBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", strParams[0]);
                oParamDic.Add("F_GUBUN", strParams[1]);
                oParamDic.Add("F_ITEMCD", strParams[2]);
                oParamDic.Add("F_WORKCD", strParams[3]);
                oParamDic.Add("F_DAYPRODUCTNO", strParams[4]);
                

                ds = biz.WERD0201_DETAIL_LST(oParamDic, out errMsg);
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                // grid callback init
                devCallback.JSProperties["cpResultCode"] = "0";
                devCallback.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    result = GlobalFunction.FirstRowToDictionary(ds);

                    devCallback.JSProperties["cpResult"] = GlobalFunction.SerializeJSON(result);
                    devCallback.JSProperties["cpResultAction"] = "SELECT";
                }
                else
                {
                    devCallback.JSProperties["cpResult"] = "";
                }
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region txtITEMCD_Init
        /// <summary>
        /// txtITEMCD_Init
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void txtITEMCD_Init(object sender, EventArgs e)
        {
            //(sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("{0}.fn_OnPopupUCITEMSearch()", this.ClientInstanceID));
            //(sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.LostFocus = string.Format("{0}.fn_OnUCITEMLostFocus", this.ClientInstanceID);
            //(sender as DevExpress.Web.ASPxTextBox).ClientSideEvents.KeyUp = string.Format("{0}.fn_OnUCITEMKeyUp", this.ClientInstanceID);
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", string.Format("fn_PopupitemSEARCH();"));
        }
        #endregion

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

        protected void ddlComboBox_DataBound2(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox devComboBox = sender as DevExpress.Web.ASPxComboBox;
            devComboBox.Items.Insert(0, new DevExpress.Web.ListEditItem() { Text = "선택하세요", Value = "", Selected = true });
        }

        #endregion

        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string[] strParams = e.Parameter.Split(';');
            if (strParams[0] == "SELECT")
            {
                QWK011_DETAIL_LST(strParams[1].Split('|'));
            }
            else if (strParams[0] == "UPDATE")
            {
                if (this.srcF_DAYPRODUCTNO.Text == "" || this.srcF_DAYPRODUCTNO.Text == null)
                {
                    QWK110_INS(strParams);
                }
                else
                {
                    QWK110_UPD(strParams);
                }
                
            }
            else
            {
                PR01M_DEL();
            }
        }

        void PR01M_DEL()
        {
            int count = 1;
            int idx = 0;

            string[] oSPs = new string[count];
            object[] oParameters = new object[count];

            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
            oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
            oParamDic.Add("F_ITEMCD", GetItemCD1());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
            oParamDic.Add("F_DAYPRODUCTNO", this.srcF_DAYPRODUCTNO.Text);

            oParameters[idx] = (object)oParamDic;
            oSPs[idx] = "USP_QWK110_DEL";
            idx++;

            bool bExecute = false;
            string resultMsg = String.Empty;
            using (WERDBiz biz = new WERDBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                devCallback.JSProperties["cpResultGbn"] = "0";
            }
            else
            {
                procResult = new string[] { "1", "삭제 되었습니다." };
                devCallback.JSProperties["cpResultGbn"] = "1";
                //QWK110_GRID_LST();
            }
            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            devCallback.JSProperties["cpResultAction"] = "DELETE";
        }

        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            QWK110_GRID_LST();
        }

        void QWK110_INS(string[] strParams)
        {

            int idx = 0;
            string[] oSPs = new string[1];
            object[] oParameters = new object[1];

            #region 검사내용 등록
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
            oParamDic.Add("F_JOBSTDT", this.srcF_JOBSTDT.Text);
            oParamDic.Add("F_JOBENDT", this.srcF_JOBENDT.Text);
            oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
            oParamDic.Add("F_ITEMCD", GetItemCD1());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
            oParamDic.Add("F_PLANNO", (this.srcF_PLANNO.Value ?? "").ToString());
            oParamDic.Add("F_LOTNO", this.srcF_LOTNO.Text);
            oParamDic.Add("F_PRODUCTQTY", this.srcF_PRODUCTQTY.Text);
            oParamDic.Add("F_INSPQTY", this.srcF_INSPQTY.Text);
            oParamDic.Add("F_NGQTY", this.srcF_NGQTY.Text);            
            oParamDic.Add("F_USERID", ucUser.GetValue());
            oParamDic.Add("F_OUTMSG", "OUTPUT");

            oSPs[idx] = "USP_QWK110_INS";
            oParameters[idx] = (object)oParamDic;
            #endregion

            bool bExecute = false;
            string resultMsg = String.Empty;
            using (BSIFBiz biz = new BSIFBiz())
            {
                bExecute = biz.PROC_QCD74_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.") };
                devCallback.JSProperties["cpResultGbn"] = "0";
            }
            else
            {
                string strDayproductno = "";
                StringBuilder sb_OutMsg = new StringBuilder();
                foreach (Dictionary<string, object> _oOutParamDic in oOutParamList)
                {
                    foreach (KeyValuePair<string, object> _oOutPair in _oOutParamDic)
                    {
                        if (!String.IsNullOrEmpty(_oOutPair.Value.ToString()))
                            strDayproductno = _oOutPair.Value.ToString();
                    }
                }

                if (!String.IsNullOrEmpty(strDayproductno))
                {
                    idx = 0;
                    var typeParam = strParams[1].Replace(",{", "@{").Split('@');
                    var causeParam = strParams[2].Replace(",{", "@{").Split('@');
                    oSPs = new string[typeParam.Length + causeParam.Length];
                    oParameters = new object[typeParam.Length + causeParam.Length];

                    if (typeParam.Length > 0 && typeParam[0] != "")
                    {
                        foreach (var param in typeParam)
                        {
                            oParamDic = new Dictionary<string, string>();
                            oParamDic = DeserializeJSON(param.ToString());
                            oParamDic.Add("F_COMPCD", gsCOMPCD);
                            oParamDic.Add("F_FACTCD", gsFACTCD);
                            oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
                            oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
                            oParamDic.Add("F_ITEMCD", GetItemCD1());
                            oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                            oParamDic.Add("F_DAYPRODUCTNO", strDayproductno);

                            oParamDic.Remove("F_NGTYPENM");

                            oSPs[idx] = "USP_QWK111_TYPE_UPD";
                            oParameters[idx] = (object)oParamDic;

                            idx++;
                        }
                    }
                    foreach (var param in causeParam)
                    {
                        oParamDic = new Dictionary<string, string>();
                        oParamDic = DeserializeJSON(param.ToString());
                        oParamDic.Add("F_COMPCD", gsCOMPCD);
                        oParamDic.Add("F_FACTCD", gsFACTCD);
                        oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
                        oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
                        oParamDic.Add("F_ITEMCD", GetItemCD1());
                        oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                        oParamDic.Add("F_DAYPRODUCTNO", strDayproductno);

                        oParamDic.Remove("F_NGCAUSENM");

                        oSPs[idx] = "USP_QWK111_CAUSE_UPD";
                        oParameters[idx] = (object)oParamDic;

                        idx++;
                    }

                    bExecute = false;
                    resultMsg = String.Empty;
                    using (WERDBiz biz = new WERDBiz())
                    {
                        bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
                    }


                    if (!bExecute)
                    {
                        procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                        devCallback.JSProperties["cpResultGbn"] = "0";
                    }
                    else
                    {
                        procResult = new string[] { "1", "저장이 완료되었습니다." };
                        devCallback.JSProperties["cpResultGbn"] = "1";
                        devCallback.JSProperties["cpPkey"] = "";
                    }
                }

                
                //QWK110_GRID_LST();
            }
            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            devCallback.JSProperties["cpResultAction"] = "SAVE";
        }

        void QWK110_UPD(string[] strParams)
        {
            var typeParam = strParams[1].Replace(",{", "@{").Split('@');
            var causeParam = strParams[2].Replace(",{", "@{").Split('@');
            string[] oSPs = new string[typeParam.Length + causeParam.Length + 1];
            object[] oParameters = new object[typeParam.Length + causeParam.Length + 1];

            int idx = 0;

            if (typeParam.Length > 0 && typeParam[0] != "")
            {
                foreach (var param in typeParam)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic = DeserializeJSON(param.ToString());
                    oParamDic.Add("F_COMPCD", gsCOMPCD);
                    oParamDic.Add("F_FACTCD", gsFACTCD);
                    oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
                    oParamDic.Add("F_DAYPRODUCTNO", this.srcF_DAYPRODUCTNO.Text);
                    oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
                    oParamDic.Add("F_ITEMCD", GetItemCD1());
                    oParamDic.Add("F_WORKCD", GetWorkPOPCD1());

                    oParamDic.Remove("F_NGTYPENM");

                    oSPs[idx] = "USP_QWK111_TYPE_UPD";
                    oParameters[idx] = (object)oParamDic;

                    idx++;
                }
            }

            foreach (var param in causeParam)
            {
                oParamDic = new Dictionary<string, string>();
                oParamDic = DeserializeJSON(param.ToString());
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
                oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
                oParamDic.Add("F_ITEMCD", GetItemCD1());
                oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
                oParamDic.Add("F_DAYPRODUCTNO", this.srcF_DAYPRODUCTNO.Text);
                oParamDic.Remove("F_NGCAUSENM");

                oSPs[idx] = "USP_QWK111_CAUSE_UPD";
                oParameters[idx] = (object)oParamDic;

                idx++;
            }

            #region 검사내용 등록
            oParamDic = new Dictionary<string, string>();
            oParamDic.Add("F_COMPCD", gsCOMPCD);
            oParamDic.Add("F_FACTCD", gsFACTCD);
            oParamDic.Add("F_DAYPRODUCTNO", this.srcF_DAYPRODUCTNO.Text);
            oParamDic.Add("F_WORKDATE", this.srcF_WORKDATE.Text);
            oParamDic.Add("F_JOBSTDT", this.srcF_JOBSTDT.Text);
            oParamDic.Add("F_JOBENDT", this.srcF_JOBENDT.Text);
            oParamDic.Add("F_GUBUN", (this.srcF_GUBUN.Value ?? "").ToString());
            oParamDic.Add("F_ITEMCD", GetItemCD1());
            oParamDic.Add("F_WORKCD", GetWorkPOPCD1());
            oParamDic.Add("F_PLANNO", (this.srcF_PLANNO.Value ?? "").ToString());
            oParamDic.Add("F_LOTNO", this.srcF_LOTNO.Text);
            oParamDic.Add("F_PRODUCTQTY", this.srcF_PRODUCTQTY.Text);
            oParamDic.Add("F_INSPQTY", this.srcF_INSPQTY.Text);
            oParamDic.Add("F_NGQTY", this.srcF_NGQTY.Text);
            oParamDic.Add("F_USERID", ucUser.GetValue());

            oSPs[idx] = "USP_QWK110_UPD";
            oParameters[idx] = (object)oParamDic;
            #endregion

            bool bExecute = false;
            string resultMsg = String.Empty;
            using (WERDBiz biz = new WERDBiz())
            {
                bExecute = biz.PROC_BATCH(oSPs, oParameters, out resultMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
                devCallback.JSProperties["cpResultGbn"] = "0";
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
                devCallback.JSProperties["cpResultGbn"] = "1";
                // 저장후 신규 모드로 이행하기에 저장 키값 전달하지 않음
                //devCallback.JSProperties["cpPkey"] = this.srcF_PR01MID.Text;
                devCallback.JSProperties["cpPkey"] = "";
            }
            devCallback.JSProperties["cpResultCode"] = procResult[0];
            devCallback.JSProperties["cpResultMsg"] = procResult[1];
            devCallback.JSProperties["cpResultAction"] = "SAVE";
        }

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            QWK110_GRID_LST();
            devGridExporter.WriteXlsToResponse(String.Format("공정불량등록_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<br />", String.Empty);
            }
        }
        #endregion
        #endregion
    }
}
