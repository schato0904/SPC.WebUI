using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTF.Web.Framework.Helper;
using System.Data;
using SPC.Common.Biz;
using SPC.WebUI.Common;

namespace SPC.WebUI.Pages.COMM
{
    public partial class COMM0102 : WebUIBasePage
    {

        #region 변수, 기본

        private DBHelper spcDB = new DBHelper("CUST");
        DataSet ds = null;
        DataSet ds1 = null;
        string[] procResult = { "2", "Unknown Error" };
        Int32 totalCnt = 0;
        static int CurrPage = 0;
        protected string oSetParam = String.Empty;
        public string dt = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Init(object sender, EventArgs e)
        {

            GetManage_LST();
            GetRequest();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Web_Init();
            GETUSER();
            //GETCUST();

            devGrid1.JSProperties["cpResultCode"] = "";
            devGrid1.JSProperties["cpResultMsg"] = "";
            devGrid1.JSProperties["cpResult"] = "";

            devGrid2.JSProperties["cpResultCode"] = "";
            devGrid2.JSProperties["cpResultMsg"] = "";
            devGrid2.JSProperties["cpResult"] = "";

            devCallback.JSProperties["cpResultCode"] = "";
            devCallback.JSProperties["cpResultMsg"] = "";
            devCallback.JSProperties["cpResult"] = "";

            devCallback.JSProperties["cpParam"] = "";
        }

        void GetRequest()
        {

        }

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

        void SetDefaultButton()
        { }

        void SetDefaultObject()
        {

            //this.tdResult08.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }

        void SetClientScripts()
        { }

        void SetDefaultValue()
        {
        }

        #endregion

        #region init

        protected void tdResult01_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupCompSearch()");
        }

        protected void tdResult12_Init(object sender, EventArgs e)
        {
            (sender as DevExpress.Web.ASPxTextBox).Attributes.Add("ondblclick", "fn_OnPopupCompSearch2()");
        }
        #endregion

        #region Table

        void TDUPDATE()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();
            oParamDic.Add("F_COMPNM", tdResult01.Text);
            oParamDic.Add("F_INSTALLUSER", tdResult06.Text);
            oParamDic.Add("F_MANAGEUSER", tdResult07.Text);
            oParamDic.Add("F_COUNDT", tdResult08.Text);
            oParamDic.Add("F_REMARK", tdResult09.Text);
            oParamDic.Add("F_REASON", tdResult10.Text);
            oParamDic.Add("F_COUNREMARK", tdResult11.Text);
            oParamDic.Add("F_CUST01ID", tdHide01.Text);
            oParamDic.Add("F_USEGUBUN", rdoGbn.Value.ToString());
            ds = SETCUST_LST(oParamDic, out errMsg);

        }

        void Delete()
        {
            string errMsg = String.Empty;

            oParamDic.Clear();
            
            oParamDic.Add("F_CUST01ID", tdHide01.Text);

            ds = CUST_DEL(oParamDic, out errMsg);
        }
        protected void devCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string strParam = e.Parameter.ToString();

            if (strParam == "SAVE")
                TDUPDATE();
            else if (strParam == "DELETE")
                Delete();

            devCallback.JSProperties["cpParam"] = strParam;
        }

        #endregion

        #region devGrid1

        protected void devGrid1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            GETUSER();
        }

        void GETUSER()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();
            oParamDic.Add("F_CSCD", hidUSER.Text);
            ds = GETUSER_LST(oParamDic, out errMsg);


            devGrid1.DataSource = ds;



            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid1.JSProperties["cpResultCode"] = "0";
                devGrid1.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid1.DataBind();
            }
        }

        #endregion

        #region devGrid2

        protected void devGrid2_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            GETCUST();
        }

        void GETCUST()
        {
            string errMsg = String.Empty;


            oParamDic.Clear();
            oParamDic.Add("F_COMPNM", tdResult12.Text);
            oParamDic.Add("F_TODT", GetToDt());
            oParamDic.Add("F_FROMDT", GetFromDt());
            ds = GETCUST_LST(oParamDic, out errMsg);


            devGrid2.DataSource = ds;

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                devGrid2.JSProperties["cpResultCode"] = "0";
                devGrid2.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                devGrid2.DataBind();
            }
        }

        #endregion

        #region combobox

        protected void ddlUSER_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            GetManage_LST();
        }

        void GetManage_LST()
        {
            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                oParamDic.Clear();

                ds = GetManage_LST(oParamDic, out errMsg);
            }

            ddlUSER.DataSource = ds;
            ddlUSER.DataBind();
        }

        #endregion

        #region batch

        protected void devGrid1_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            int idx = 0;
            int count = e.InsertValues.Count + e.UpdateValues.Count + e.DeleteValues.Count;
            string[] oSPs = new string[count];
            object[] oParameters = new object[count];
            bool bExecute = false;
            string resultMsg = String.Empty;


            if (e.InsertValues.Count > 0)
            {
                foreach (var Value in e.InsertValues)
                {
                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_CSCD", hidUSER.Text);
                    //oParamDic.Add("F_CSCD", (Value.NewValues["F_CSCD"] ?? "").ToString());
                    oParamDic.Add("F_USER", (Value.NewValues["F_USER"] ?? "").ToString());
                    oParamDic.Add("F_PHONE", (Value.NewValues["F_PHONE"] ?? "").ToString());
                    oParamDic.Add("F_EMAIL", (Value.NewValues["F_EMAIL"] ?? "").ToString());
                    oParamDic.Add("F_REMARK", (Value.NewValues["F_REMARK"] ?? "").ToString());

                    oSPs[idx] = "USP_CUST02_INS";
                    oParameters[idx] = (object)oParamDic;


                    idx++;

                }
            }

            if (e.DeleteValues.Count > 0)
            {
                foreach (var Value in e.DeleteValues)
                {

                    oParamDic = new Dictionary<string, string>();
                    oParamDic.Add("F_CSCD", hidUSER.Text);
                    oParamDic.Add("F_USER", (Value.Values["F_USER"] ?? "").ToString());
                    oParamDic.Add("F_PHONE", (Value.Values["F_PHONE"] ?? "").ToString());
                    oParamDic.Add("F_EMAIL", (Value.Values["F_EMAIL"] ?? "").ToString());
                    oParamDic.Add("F_REMARK", (Value.Values["F_REMARK"] ?? "").ToString());

                    oSPs[idx] = "USP_CUST02_DEL";
                    oParameters[idx] = (object)oParamDic;

                    //GetManage_LST();
                    idx++;
                }
            }


            bExecute = PROC_CUST02_BATCH(oSPs, oParameters, out oOutParamList, out resultMsg);

            if (!bExecute)
            {
                procResult = new string[] { "0", String.Format("저장 중 서버에서 장애가 발생하였습니다.\r계속해서 장애가 발행하는 경우 관리자에게 문의 바랍니다.\r\n에러내용 : {0}", resultMsg) };
            }
            else
            {
                procResult = new string[] { "1", "저장이 완료되었습니다." };


                GETUSER();
            }



            devGrid1.JSProperties["cpResultCode"] = procResult[0];
            devGrid1.JSProperties["cpResultMsg"] = procResult[1];
            //devGrid1.JSProperties["cpResult"] = "";
            //devGrid1.JSProperties["cpResultreInert"] = reInsert;
            //devGrid1.JSProperties["cpResultcount"] = erroridx;


            e.Handled = true;


        }
        #endregion

        #region dbhelper

        #region setcust

        public DataSet SETCUST_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = SETCUST_LST2(oParams, out errMsg);


            return ds;
        }

        public DataSet SETCUST_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SETCUST_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CUST DEL

        public DataSet CUST_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CUST_DEL", out errMsg);
            }

            return ds;
        }

        #endregion

        #region getuser

        public DataSet GETUSER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GETUSER_LST2(oParams, out errMsg);


            return ds;
        }
        public DataSet GETUSER_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MANAGEUSER_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region getcust

        public DataSet GETCUST_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GETCUST_LST2(oParams, out errMsg);


            return ds;
        }
        public DataSet GETCUST_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MANAGECUST_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region getmanage

        public DataSet GetManage_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();


            ds = GetManage_LST2(oParams, out errMsg);


            return ds;
        }
        public DataSet GetManage_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CUST"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_GETMANAGE_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region proc_cust02

        public bool PROC_CUST02_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;


            bExecute = PROC_CUST022_BATCH(oSps, oParams, out oOutParams, out resultMsg);


            return bExecute;
        }
        public bool PROC_CUST022_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CUST"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }

        #endregion

        #endregion








    }
}