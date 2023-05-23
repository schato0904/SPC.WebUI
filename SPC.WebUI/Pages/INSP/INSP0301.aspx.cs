using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPC.WebUI.Common;
using SPC.INSP.Biz;

namespace SPC.WebUI.Pages.INSP
{
    public partial class INSP0301 : WebUIBasePage
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
                devGrid1.JSProperties["cpResultCode"] = "";
                devGrid1.JSProperties["cpResultMsg"] = "";
                devGrid2.JSProperties["cpResultCode"] = "";
                devGrid2.JSProperties["cpResultMsg"] = "";
                devGrid3.JSProperties["cpResultCode"] = "";
                devGrid3.JSProperties["cpResultMsg"] = "";
                devGrid4.JSProperties["cpResultCode"] = "";
                devGrid4.JSProperties["cpResultMsg"] = "";
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
        {
            srcF_REPORTTP.TextField = String.Format("COMMNM{0}", gsLANGTP);
            srcF_REPORTTP.ValueField = "COMMCD";
            srcF_REPORTTP.DataSource = CachecommonCode["AA"]["AAI1"].codeGroup.Values;
            srcF_REPORTTP.DataBind();

            srcF_JUDGETP.TextField = String.Format("COMMNM{0}", gsLANGTP);
            srcF_JUDGETP.ValueField = "COMMCD";
            srcF_JUDGETP.DataSource = CachecommonCode["AA"]["AAI3"].codeGroup.Values;
            srcF_JUDGETP.DataBind();

            rdoF_ASSORTMENT.TextField = String.Format("COMMNM{0}", gsLANGTP);
            rdoF_ASSORTMENT.ValueField = "COMMCD";
            rdoF_ASSORTMENT.DataSource = CachecommonCode["AA"]["AAI2"].codeGroup.Values;
            rdoF_ASSORTMENT.DataBind();
        }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 마스터정보

        #region 마스터 조회
        void QWK13M_LST()
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);

                ds = biz.QWK13M_LST(oParamDic, out errMsg);
            }

            devGrid1.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid1.DataBind();
            }
        }
        #endregion

        #region 마스터 개정이력 조회
        void QWK13M_REV_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));

                ds = biz.QWK13M_REV_LST(oParamDic, out errMsg);
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
                if (IsCallback)
                    devGrid2.DataBind();
            }
        }
        #endregion

        #region 마스터 정보
        DataSet QWK13M_GET(Dictionary<string, object> paramDic, out string errMsg)
        {
            if (ds != null) ds.Clear();

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());

                ds = biz.QWK13M_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 정보 등록여부확인
        bool QWK13M_CHK_EXISTS(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExists = false;

            if (ds != null) ds.Clear();

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());

                ds = biz.QWK13M_CHK_EXISTS(oParamDic, out errMsg);
            }

            if (bExistsDataSet(ds))
                bExists = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

            return bExists;
        }
        #endregion

        #region 마스터 저장
        bool QWK13M_INS_UPD(Dictionary<string, object> paramDic, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_REVDATE", paramDic["F_REVDATE"].ToString());
                oParamDic.Add("F_JUDGETP", paramDic["F_JUDGETP"].ToString());
                oParamDic.Add("F_TYPENM", paramDic["F_TYPENM"].ToString());
                oParamDic.Add("F_ITEMNM", paramDic["F_ITEMNM"].ToString());
                oParamDic.Add("F_CONDITION", paramDic["F_CONDITION"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_MATERIAL", paramDic["F_MATERIAL"].ToString());
                oParamDic.Add("F_SDR", paramDic["F_SDR"].ToString());
                oParamDic.Add("F_SHAPE", paramDic["F_SHAPE"].ToString());
                oParamDic.Add("F_DOCNUM", paramDic["F_DOCNUM"].ToString());
                oParamDic.Add("F_REMARK", paramDic["F_REMARK"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_CONFIRM", paramDic["F_CONFIRM"].ToString());
                oParamDic.Add("F_STATUS", paramDic["F_STATUS"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic["PKEY"] = "OUTPUT";
                bExecute = biz.QWK13M_INS_UPD(oParamDic, out errMsg, out pkey);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 확정
        bool QWK13M_CFM(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13M_CFM(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 개정
        bool QWK13M_REV(Dictionary<string, object> paramDic, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_REVDATE", paramDic["F_REVDATE"].ToString());
                oParamDic.Add("F_JUDGETP", paramDic["F_JUDGETP"].ToString());
                oParamDic.Add("F_TYPENM", paramDic["F_TYPENM"].ToString());
                oParamDic.Add("F_ITEMNM", paramDic["F_ITEMNM"].ToString());
                oParamDic.Add("F_CONDITION", paramDic["F_CONDITION"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_MATERIAL", paramDic["F_MATERIAL"].ToString());
                oParamDic.Add("F_SDR", paramDic["F_SDR"].ToString());
                oParamDic.Add("F_SHAPE", paramDic["F_SHAPE"].ToString());
                oParamDic.Add("F_DOCNUM", paramDic["F_DOCNUM"].ToString());
                oParamDic.Add("F_REMARK", paramDic["F_REMARK"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_CONFIRM", paramDic["F_CONFIRM"].ToString());
                oParamDic.Add("F_STATUS", paramDic["F_STATUS"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                oParamDic["PKEY"] = "OUTPUT";
                bExecute = biz.QWK13M_REV(oParamDic, out errMsg, out pkey);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 마스터 항목 정보

        #region 마스터 항목 조회
        void QWK13A_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", GetDicValue(paramDic, "F_QWK13MID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_REVNO", GetDicValue(paramDic, "F_REVNO"));

                ds = biz.QWK13A_LST(oParamDic, out errMsg);
            }

            devGrid3.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid3.JSProperties["cpResultCode"] = "0";
                devGrid3.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid3.DataBind();
            }
        }
        #endregion

        #region 마스터 항목 정보
        DataSet QWK13A_GET(Dictionary<string, object> paramDic, out string errMsg)
        {
            if (ds != null) ds.Clear();

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());

                ds = biz.QWK13A_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 항목 저장
        bool QWK13A_INS_UPD(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                oParamDic.Add("F_DIVISIONNM", paramDic["F_DIVISIONNM"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_METHOD", paramDic["F_METHOD"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_EQUIPMENT", paramDic["F_EQUIPMENT"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_ASSORTMENT", paramDic["F_ASSORTMENT"].ToString());
                oParamDic.Add("F_DIVISIONSORT", paramDic["F_DIVISIONSORT"].ToString());
                oParamDic.Add("F_USESTATUS", paramDic["F_USESTATUS"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13A_INS_UPD(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 항목 출력순서변경
        bool QWK13A_SRT(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                oParamDic.Add("F_DIVISIONSORT", paramDic["F_DIVISIONSORT"].ToString());
                oParamDic.Add("F_DIRECTION", paramDic["F_DIRECTION"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13A_SRT(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 마스터 항목 기준 정보

        #region 마스터 항목 기준 조회
        void QWK13B_LST(Dictionary<string, object> paramDic)
        {
            string errMsg = String.Empty;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", GetDicValue(paramDic, "F_QWK13MID"));
                oParamDic.Add("F_REPORTTP", GetDicValue(paramDic, "F_REPORTTP"));
                oParamDic.Add("F_REVNO", GetDicValue(paramDic, "F_REVNO"));
                oParamDic.Add("F_DIVISIONIDX", GetDicValue(paramDic, "F_DIVISIONIDX"));

                ds = biz.QWK13B_LST(oParamDic, out errMsg);
            }

            devGrid4.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid4.JSProperties["cpResultCode"] = "0";
                devGrid4.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                if (IsCallback)
                    devGrid4.DataBind();
            }
        }
        #endregion

        #region 마스터 항목 기준 정보
        DataSet QWK13B_GET(Dictionary<string, object> paramDic, out string errMsg)
        {
            if (ds != null) ds.Clear();

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                oParamDic.Add("F_INSPIDX", paramDic["F_INSPIDX"].ToString());

                ds = biz.QWK13B_GET(oParamDic, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 항목 기준 저장
        bool QWK13B_INS_UPD(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                oParamDic.Add("F_INSPIDX", paramDic["F_INSPIDX"].ToString());
                oParamDic.Add("F_INSPNM", paramDic["F_INSPNM"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_STANDARD", paramDic["F_STANDARD"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_TERM", paramDic["F_TERM"].ToString().Replace("\\n", Environment.NewLine));
                oParamDic.Add("F_TRANSPARENT", paramDic["F_TRANSPARENT"].ToString());
                oParamDic.Add("F_ISEXCEPT", paramDic["F_ISEXCEPT"].ToString());
                oParamDic.Add("F_INSPSORT", paramDic["F_INSPSORT"].ToString());
                oParamDic.Add("F_USESTATUS", paramDic["F_USESTATUS"].ToString());
                oParamDic.Add("F_GROUPCNT", paramDic["F_GROUPCNT"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13B_INS_UPD(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 항목 기준 출력순서변경
        bool QWK13B_SRT(Dictionary<string, object> paramDic, out string errMsg)
        {
            bool bExecute = false;

            using (INSPBiz biz = new INSPBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_QWK13MID", paramDic["F_QWK13MID"].ToString());
                oParamDic.Add("F_REPORTTP", paramDic["F_REPORTTP"].ToString());
                oParamDic.Add("F_REVNO", paramDic["F_REVNO"].ToString());
                oParamDic.Add("F_DIVISIONIDX", paramDic["F_DIVISIONIDX"].ToString());
                oParamDic.Add("F_INSPIDX", paramDic["F_INSPIDX"].ToString());
                oParamDic.Add("F_INSPSORT", paramDic["F_INSPSORT"].ToString());
                oParamDic.Add("F_DIRECTION", paramDic["F_DIRECTION"].ToString());
                oParamDic.Add("F_USER", gsUSERID);
                bExecute = biz.QWK13B_SRT(oParamDic, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid1_CustomCallback
        /// <summary>
        /// devGrid1_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 마스터 조회
            QWK13M_LST();
        }
        #endregion

        #region devGrid1_CustomColumnDisplayText
        /// <summary>
        /// devGrid1_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid1_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_REPORTTP"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
            if (!e.Column.FieldName.Equals("F_CONFIRM")) return;
            DevExpressLib.GetBoolString(new string[] { "F_CONFIRM" }, "확정", "등록중", e);
        }
        #endregion

        #region devGrid2_CustomCallback
        /// <summary>
        /// devGrid2_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewCustomCallbackEventArgs</param>
        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 마스터 개정이력 조회
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK13M_REV_LST(paramDic);
        }
        #endregion

        #region devGrid2_CustomColumnDisplayText
        /// <summary>
        /// devGrid2_CustomColumnDisplayText
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs</param>
        protected void devGrid2_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_REPORTTP"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
            else if (e.Column.FieldName.Equals("F_STATUS"))
            {
                e.EncodeHtml = false;
                switch (e.Value.ToString())
                {
                    case "0":
                        e.DisplayText = "<span style='color:red;'>등록중</span>";
                        break;
                    case "1":
                        e.DisplayText = "<span style='color:blue;'>현재사용</span>";
                        break;
                    case "8":
                        e.DisplayText = "<span style='color:blue;'>개정중</span>";
                        break;
                    case "9":
                        e.DisplayText = "<span style='color:gray;'>이력</span>";
                        break;
                }
            }
        }
        #endregion

        #region btnSortDown_Init
        protected void btnSortDownA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid3.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('A', '-', '{0}', '{1}', '{2}'); return false; }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnSortUp_Init
        protected void btnSortUpA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid3.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('A', '+', '{0}', '{1}', '{2}'); return false; }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnAdd_Init
        protected void btnAddA_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid3.GetRowValues(rowVisibleIndex, "F_DIVISIONIDX", "F_DIVISIONNM") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnAddInspection('{0}', '{1}', '{2}'); return false; }}",
                rowValues[0],
                rowValues[1].ToString().Replace(Environment.NewLine, ""),
                rowVisibleIndex);
        }
        #endregion

        #region devGrid3_CustomCallback
        protected void devGrid3_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 마스터 항목 조회
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK13A_LST(paramDic);
        }
        #endregion

        #region devGrid3_CustomColumnDisplayText
        protected void devGrid3_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex < 0) return;
            if (e.Column.FieldName.Equals("F_ASSORTMENT"))
            {
                e.DisplayText = GetCommonCodeText(e.Value.ToString());
            }
            DevExpressLib.GetBoolString(new string[] { "F_USESTATUS" }, "사용", "중단", e);
        }
        #endregion

        #region btnSortDownB_Init
        protected void btnSortDownB_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid4.GetRowValues(rowVisibleIndex, "F_INSPIDX", "F_INSPSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('B', '-', '{0}', '{1}', '{2}'); return false; }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region btnSortUpB_Init
        protected void btnSortUpB_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxHyperLink hyperLink = (DevExpress.Web.ASPxHyperLink)sender;

            DevExpress.Web.GridViewDataItemTemplateContainer templateContainer = (DevExpress.Web.GridViewDataItemTemplateContainer)hyperLink.NamingContainer;

            int rowVisibleIndex = templateContainer.VisibleIndex;
            object[] rowValues = devGrid4.GetRowValues(rowVisibleIndex, "F_INSPIDX", "F_INSPSORT") as object[];
            hyperLink.NavigateUrl = "javascript:void(0);";
            hyperLink.ClientSideEvents.Click = String.Format("function(s, e) {{ fn_OnChangeSort('B', '+', '{0}', '{1}', '{2}'); return false; }}",
                rowValues[0],
                rowValues[1],
                rowVisibleIndex);
        }
        #endregion

        #region devGrid4_CustomCallback
        protected void devGrid4_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 마스터 항목 기준 조회
            Dictionary<string, object> paramDic;
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameters); // 웹에서 전달된 파라미터
            }
            else
            {
                paramDic = new Dictionary<string, object>();
            }
            QWK13B_LST(paramDic);
        }
        #endregion

        #region devGrid4_CustomColumnDisplayText
        protected void devGrid4_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            DevExpressLib.GetBoolString(new string[] { "F_USESTATUS" }, "사용", "중단", e);
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
            // 유형별 콜백 처리 후, 결과값(처리결과, 메시지, 데이터 등)을 result변수(Dictionary)에 담아 json으로 읽을 수 있도록 serialize하여 리턴한다.
            string sReqID = String.Empty;
            bool bExecute = false;
            string errMsg = String.Empty;   // 오류 메시지
            string pkey = String.Empty;
            Dictionary<string, object> oDataDic = null;
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> paramDic = jss.Deserialize<Dictionary<string, object>>(e.Parameter);
            bool ISOK = false;  // 처리 결과
            string msg = string.Empty;  // 처리 메시지
            Dictionary<string, string> PKEY = new Dictionary<string, string>(); // 저장 후 키값
            Dictionary<string, object> result = new Dictionary<string, object>();   // 콜백 리턴 값
            Dictionary<string, object> PAGEDATA = new Dictionary<string, object>();

            switch (paramDic["ACTION"].ToString())
            {
                case "NEW":
                case "EDIT":
                    if (!QWK13M_INS_UPD(paramDic, out errMsg, out pkey))
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
                        PKEY["F_QWK13MID"] = pkey;
                        PKEY["F_REPORTTP"] = paramDic["F_REPORTTP"].ToString();
                        PKEY["F_REVNO"] = paramDic["F_REVNO"].ToString();
                        result["PKEY"] = PKEY;
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET":
                    ds = QWK13M_GET(paramDic, out errMsg);
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
                case "CHKEXISTS":
                    bool bExists = QWK13M_CHK_EXISTS(paramDic, out errMsg);
                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else if (bExists)
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "이미 등록된 양식이 있습니다. 등록한 양식을 수정 또는 개정하세요";
                    }
                    else
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "";
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "CFM":
                    if (!QWK13M_CFM(paramDic, out errMsg))
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
                        PKEY["F_QWK13MID"] = paramDic["F_QWK13MID"].ToString();
                        PKEY["F_REPORTTP"] = paramDic["F_REPORTTP"].ToString();
                        PKEY["F_REVNO"] = paramDic["F_REVNO"].ToString();
                        result["PKEY"] = PKEY;
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "REV":
                    if (!QWK13M_REV(paramDic, out errMsg, out pkey))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "개정되었습니다.";
                        PKEY["F_QWK13MID"] = pkey;
                        PKEY["F_REPORTTP"] = paramDic["F_REPORTTP"].ToString();
                        PKEY["F_REVNO"] = paramDic["F_REVNO"].ToString();
                        result["PKEY"] = PKEY;
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "NEW_A":
                case "EDIT_A":
                    if (!QWK13A_INS_UPD(paramDic, out errMsg))
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
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET_A":
                    ds = QWK13A_GET(paramDic, out errMsg);
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
                case "SORT_A":
                    if (!QWK13A_SRT(paramDic, out errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "변경되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;

                case "NEW_B":
                case "EDIT_B":
                    if (!QWK13B_INS_UPD(paramDic, out errMsg))
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
                    }
                    e.Result = jss.Serialize(result);
                    break;
                case "GET_B":
                    ds = QWK13B_GET(paramDic, out errMsg);
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
                case "SORT_B":
                    if (!QWK13B_SRT(paramDic, out errMsg))
                    {
                        ISOK = false;
                        result["ISOK"] = ISOK;
                        result["MSG"] = errMsg;
                    }
                    else
                    {
                        ISOK = true;
                        result["ISOK"] = ISOK;
                        result["MSG"] = "변경되었습니다.";
                    }
                    e.Result = jss.Serialize(result);
                    break;
            }
        }
        #endregion

        #endregion
    }
}