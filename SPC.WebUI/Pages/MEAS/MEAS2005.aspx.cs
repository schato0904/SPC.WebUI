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

using DevExpress.Web;

namespace SPC.WebUI.Pages.MEAS
{
    public partial class MEAS2005 : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
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
            // Request
            GetRequest();

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
        {
        }
        #endregion

        #region 객체 초기화
        /// <summary>
        /// SetDefaultObject
        /// </summary>
        void SetDefaultObject()
        {
            //this.ucF_RECVDT.Clear();

            GetTeamCodeList(srcF_TEAMCD);
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

        #region 공통, 팀, 반, 공정에 대한 코드, 분류를 구한다

        void GetTeamCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.MEAS1001_TEAM_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_TEAMNM";
            comboBox.ValueField = "F_TEAMCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetBancCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", "");
                oParamDic.Add("F_TEAMCD", (srcF_TEAMCD.Value ?? "").ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS1001_BAN_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_BANNM";
            comboBox.ValueField = "F_BANCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        void GetProcCodeList(DevExpress.Web.ASPxComboBox comboBox)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", "");
                oParamDic.Add("F_TEAMCD", (srcF_TEAMCD.Value ?? "").ToString());
                oParamDic.Add("F_LANGTYPE", gsLANGTP);

                ds = biz.MEAS1001_PROC_LST(oParamDic, out errMsg);
            }

            comboBox.TextField = "F_PROCNM";
            comboBox.ValueField = "F_PROCCD";
            comboBox.DataSource = ds;
            comboBox.DataBind();
            comboBox.Items.Insert(0, new ListEditItem("전체", ""));
        }

        #endregion

        #region 그리드 목록 조회

        /// <summary>
        /// 검교정 신청의뢰 및 접수 목록
        /// </summary>
        void MEAS2005_MST()
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_FROM_REQDT", srcF_REQDT_FROM.Text);
                oParamDic.Add("F_TO_REQDT", srcF_REQDT_TO.Text);
                oParamDic.Add("F_FROM_RECVDT", srcF_RECVDT_FROM.Text);
                oParamDic.Add("F_TO_RECVDT", srcF_RECVDT_TO.Text);
                oParamDic.Add("F_FACTCD",  gsFACTCD);
                oParamDic.Add("F_TEAMCD",  (srcF_TEAMCD.Value ?? "").ToString());
                oParamDic.Add("F_REQNO", srcF_REQNO.Text.Trim());

                ds = biz.MEAS2005_MST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid2.JSProperties["cpResultCode"] = "1";
                devGrid2.JSProperties["cpResultMsg"] = "";
            }
        }

        /// <summary>
        /// 검교정 신청 내역 상세 정보
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2005_DTL(string F_MS02MID)
        {
            string errMsg = String.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_MS02MID", F_MS02MID);

                ds = biz.MEAS2005_DTL(oParamDic, out errMsg);
            }

            devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid2.JSProperties["cpResultCode"] = "1";
                devGrid2.JSProperties["cpResultMsg"] = "";
            }
        }

        void CreateParameter(bool bInsert)
        {
            oParamDic.Clear();
            oParamDic.Add("F_MS02MID_LST", hidF_MS02MID_LST.Text.Trim());
            if (bInsert)
            {
                oParamDic.Add("F_RECVUSER", gsUSERNM);
            }
        }

        /// <summary>
        /// 검교정 신청 내역 상세 저장
        /// </summary>
        /// <param name="FIELDS"></param>
        bool MEAS2005_INS()
        {
            bool bExecute = false;
            var errMsg = string.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                CreateParameter(true);

                bExecute = biz.MEAS2005_INS(oParamDic, out errMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다."};
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };
            }

            return bExecute;
        }

        /// <summary>
        /// 검교정 신청 내역 상세 삭제
        /// </summary>
        /// <param name="FIELDS"></param>
        void MEAS2005_DEL()
        {
            bool bExecute = false;
            string errMsg = string.Empty;

            using (MEASBiz biz = new MEASBiz())
            {
                CreateParameter(false);

                bExecute = biz.MEAS2005_DEL(oParamDic, out errMsg);
            }

            if (!bExecute)
            {
                procResult = new string[] { "0", "삭제 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n\r\n" + errMsg };
            }
            else
            {
                procResult = new string[] { "1", "삭제가 완료되었습니다." };
            }
        }

        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            var result = new Dictionary<string, string>();
            string actType = e.Parameter.ToUpper();

            if (actType == "SAVE")
            {
                MEAS2005_INS();
                result["TYPE"] = "AfterSave";
            }
            else if (actType == "DEL")
            {
                // 삭제 액션
                this.MEAS2005_DEL();                
                result["TYPE"] = "AfterDelete";
            }

            result["CODE"] = procResult[0];
            result["MESSAGE"] = procResult[1];

            e.Result = this.SerializeJSON(result);
        }
        #endregion
        
        #region devGrid_CustomCallback
        /// <summary>
        /// 계측기보유현황 그리드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = (sender) as ASPxGridView;

            var pkey = string.Empty;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                if (e.Parameters.IndexOf("VIEW") >= 0)
                {
                    pkey = e.Parameters.Split(';')[1];
                }

                if (e.Parameters.Equals("clear"))
                {
                    grid.DataSourceID = null;
                    grid.DataSource = null;
                    grid.DataBind();                    
                    grid.JSProperties["cpResultCode"] = "1";
                    grid.JSProperties["cpResultMsg"] = "";

                    return;
                }
            }

            if (grid.ClientInstanceName.Equals("devGrid2"))
            {
                MEAS2005_DTL(pkey);
            }
            else
            {
                MEAS2005_MST();
            }

            grid.DataBind();
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            MEAS2005_MST();
            devGridExporter.GridViewID = "devGrid2";
            devGridExporter.WriteXlsToResponse(String.Format("검교정접수_{0}", DateTime.Today.ToShortDateString()), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
                e.Text = e.Text.Replace("<BR/>", " ");
            }
        }
        #endregion

        protected void devGrid1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            e.Cell.Attributes.Add("onclick", String.Format("fn_OnGridCellClick({0}, '{1}')", e.VisibleIndex, e.DataColumn.FieldName));
        }

        #endregion
    }
}