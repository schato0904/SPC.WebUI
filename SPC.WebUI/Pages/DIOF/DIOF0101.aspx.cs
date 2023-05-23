using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SPC.WebUI.Common;
using SPC.FDCK.Biz;

namespace SPC.WebUI.Pages.DIOF
{
    public partial class DIOF0101 : WebUIBasePage
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

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // 하단 설비 목록 조회
                RetrieveList();
            }

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

        #region 조회
        // 출력시 사용할 기본순번
        DataSet AutoNumber(DataSet ds, string NumberColumnName = "NO")
        {
            DataSet returnDs = new DataSet();
            DataTable dt = null;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //dt = ds.Tables[i];
                dt = new DataTable();
                dt.Columns.Add(NumberColumnName, typeof(long));
                dt.Columns[NumberColumnName].AutoIncrement = true;
                dt.Columns[NumberColumnName].AutoIncrementSeed = 1;
                dt.Columns[NumberColumnName].AutoIncrementStep = 1;
                dt.Merge(ds.Tables[i]);
                returnDs.Tables.Add(dt);
            }

            return returnDs;
        }

        void RetrieveList()
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_BANCD", schF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", schF_LINECD.GetValue());
                oParamDic.Add("F_MACHKIND", schF_MACHKIND.GetValue());
                oParamDic.Add("F_MACHNM", schF_MACHNM.Text);
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }

            devGrid.DataSource = this.AutoNumber(ds);

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

        #region 뷰(선택한 설비 상세정보 조회)
        DataSet RetrieveDetail(string machIDX, out string errMsg)
        {
            errMsg = String.Empty;
            if (ds != null) ds.Clear();
            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", machIDX);
                ds = biz.QCD_MACH21_LST(oParamDic, out errMsg);
            }
            return ds;
        }
        #endregion

        #region 저장
        private bool QCD_MACH21_INS(out string machIDX, out string resultMsg)
        {
            machIDX = String.Empty;
            resultMsg = String.Empty;
            bool bExecute = false;
            bool bExistsMachCD = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHCD", srcF_MACHCD.Text);
                oParamDic.Add("F_MACHNM", srcF_MACHNM.Text);
                oParamDic.Add("F_MACHKIND", srcF_MACHKIND.GetValue());
                oParamDic.Add("F_BANCD", srcF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", srcF_LINECD.GetValue());
                oParamDic.Add("F_SORTNO", srcF_SORTNO.Text);
                oParamDic.Add("F_USEYN", (srcF_USEYN.Value ?? "").ToString());
                oParamDic.Add("F_REASON", srcF_REASON.GetValue());
                oParamDic.Add("F_MAKER", srcF_MAKER.Text);
                oParamDic.Add("F_INDATE", UF.Date.TryParse(srcF_INDATE.Text, ""));
                oParamDic.Add("F_SELLER", srcF_SELLER.Text);
                oParamDic.Add("F_PRICE", srcF_PRICE.Text);
                oParamDic.Add("F_SPEC", srcF_SPEC.Text);
                oParamDic.Add("F_SUBPART", srcF_SUBPART.Text);
                oParamDic.Add("F_IMAGENO", !String.IsNullOrEmpty(txtIMAGESEQ.Text) ? txtIMAGESEQ.Text : UF.Encrypts.GetUniqueKey());
                oParamDic.Add("F_REMARK", srcF_REMARK.Text);
                oParamDic.Add("F_POINTX", srcF_POINTX.Text);
                oParamDic.Add("F_POINTY", srcF_POINTY.Text);
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                oParamDic.Add("F_MACHIDX", "OUTPUT");
                oParamDic.Add("F_MACHCDEXISTS", "OUTPUT");

                bExecute = biz.QCD_MACH21_INS(oParamDic, out oOutParamDic, out resultMsg);

                if (bExecute == true)
                {
                    foreach (KeyValuePair<string, object> oOutDic in oOutParamDic)
                    {
                        if (oOutDic.Key.Equals("F_MACHIDX"))
                            machIDX = oOutDic.Value.ToString();
                        else if (oOutDic.Key.Equals("F_MACHCDEXISTS"))
                        {
                            bExistsMachCD = oOutDic.Value.ToString().Equals("1");
                            if (bExistsMachCD == true)
                                resultMsg = String.Format("입력한 설비코드 [{0}] 은(는) 이미 사용중입니다", srcF_MACHCD.Text);
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(resultMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #region 수정
        private bool QCD_MACH21_UPD(out string machIDX, out string resultMsg)
        {
            machIDX = srcF_MACHIDX.Text;
            resultMsg = String.Empty;
            bool bExecute = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", machIDX);
                oParamDic.Add("F_MACHCD", srcF_MACHCD.Text);
                oParamDic.Add("F_MACHNM", srcF_MACHNM.Text);
                oParamDic.Add("F_MACHKIND", srcF_MACHKIND.GetValue());
                oParamDic.Add("F_BANCD", srcF_BANCD.GetValue());
                oParamDic.Add("F_LINECD", srcF_LINECD.GetValue());
                oParamDic.Add("F_SORTNO", srcF_SORTNO.Text);
                oParamDic.Add("F_USEYN", (srcF_USEYN.Value ?? "").ToString());
                oParamDic.Add("F_REASON", srcF_REASON.GetValue());
                oParamDic.Add("F_MAKER", srcF_MAKER.Text);
                oParamDic.Add("F_INDATE", UF.Date.TryParse(srcF_INDATE.Text, ""));
                oParamDic.Add("F_SELLER", srcF_SELLER.Text);
                oParamDic.Add("F_PRICE", srcF_PRICE.Text);
                oParamDic.Add("F_SPEC", srcF_SPEC.Text);
                oParamDic.Add("F_SUBPART", srcF_SUBPART.Text);
                oParamDic.Add("F_IMAGENO", !String.IsNullOrEmpty(txtIMAGESEQ.Text) ? txtIMAGESEQ.Text : UF.Encrypts.GetUniqueKey());
                oParamDic.Add("F_REMARK", srcF_REMARK.Text);
                oParamDic.Add("F_POINTX", srcF_POINTX.Text);
                oParamDic.Add("F_POINTY", srcF_POINTY.Text);
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);

                bExecute = biz.QCD_MACH21_UPD(oParamDic, out resultMsg);
            }

            if (!String.IsNullOrEmpty(resultMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        private bool QCD_MACH21_DEL(out string resultMsg)
        {
            resultMsg = String.Empty;
            bool bExecute = false;
            bool bUsedMachIDX = false;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_MACHIDX", srcF_MACHIDX.Text);
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic.Add("F_MODREASON", srcF_MODREASON.Text);
                oParamDic.Add("F_RSTMSG", "OUTPUT");

                bExecute = biz.QCD_MACH21_DEL(oParamDic, out oOutParamDic, out resultMsg);

                if (bExecute == true)
                {
                    foreach (KeyValuePair<string, object> oOutDic in oOutParamDic)
                    {
                        if (oOutDic.Key.Equals("F_RSTMSG"))
                        {
                            bUsedMachIDX = !String.IsNullOrEmpty(oOutDic.Value.ToString());
                            if (bUsedMachIDX == true)
                                resultMsg = String.Format("선택한 설비코드 [{0}] 은(는) 이미 사용중이므로 삭제하실 수 없습니다", srcF_MACHCD.Text);
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(resultMsg))
            {
                bExecute = false;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devCallback_Callback
        /// <summary>
        /// devCallback_Callback
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="e">DevExpress.Web.CallbackEventArgs</param>
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string errMsg = String.Empty;   // 오류 메시지
            string machIDX = String.Empty;  // 설비고유번호(IDENTITY)
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> paramDic = jss.Deserialize<Dictionary<string, string>>(e.Parameter); // 웹에서 전달된 파라미터
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값

            switch (paramDic["ACTION"])
            {
                case "SAVE":
                    if (paramDic["PAGEMODE"] == "C")
                    {
                        if (!QCD_MACH21_INS(out machIDX, out errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["MSG"] = "저장되었습니다.";
                            PKEY["F_MACHIDX"] = machIDX;
                            result["PKEY"] = PKEY;
                        }
                    }
                    else if (paramDic["PAGEMODE"] == "U")
                    {
                        if (!QCD_MACH21_UPD(out machIDX, out errMsg))
                        {
                            ISOK = false;
                            result["ISOK"] = ISOK;
                            result["MSG"] = errMsg;
                        }
                        else
                        {
                            ISOK = true;
                            result["ISOK"] = ISOK;
                            result["MSG"] = "수정되었습니다.";
                            PKEY["F_MACHIDX"] = machIDX;
                            result["PKEY"] = PKEY;
                        }
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();
                    if (ds != null) ds.Clear();;
                    ds = RetrieveDetail(paramDic["F_MACHIDX"], out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ISOK = false;
                        msg = errMsg;
                    }
                    else if (!bExistsDataSet(ds))
                    {
                        ISOK = false;
                        msg = "조회된 데이터가 없습니다.";
                    }
                    else
                    {
                        ISOK = true;
                        msg = string.Empty;
                        DataRow dr = ds.Tables[0].Rows[0];
                        // 조회한 데이터를 Dictionary 형태로 변환
                        PAGEDATA = ds.Tables[0].Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    }
                    result["ISOK"] = ISOK;
                    result["MSG"] = msg;
                    result["PAGEDATA"] = PAGEDATA;
                    e.Result = jss.Serialize(result);
                    break;
                case "DELETE":
                    if (!QCD_MACH21_DEL(out errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "삭제되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #region devGrid_CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 하단 그리드 목록 조회
            RetrieveList();
        }
        #endregion

        #region devGrid_CustomColumnDisplayText
        protected void devGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USEYN" }, "가동", "비가동", e);
        }
        #endregion

        #region devGridExporter_RenderBrick
        /// <summary>
        /// devGridExporter_RenderBrick
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewExportRenderingEventArgs</param>
        protected void devGridExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            if ((e.Column as DevExpress.Web.GridViewDataColumn).FieldName == "F_USEYN") //|| e.RowType == DevExpress.Web.GridViewRowType.Header)
            {
                e.Text = GlobalFunction.StripHtml(e.Text);
            }
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            devGridExporter.WriteXlsToResponse(String.Format("[{0}]{1} 설비관리", DateTime.Today.ToString("yyyyMMdd"), gsCOMPNM), new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        #endregion

        #endregion
    }
}