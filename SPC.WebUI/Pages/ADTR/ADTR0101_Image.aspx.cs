using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SPC.WebUI.Common;
using SPC.Common.Biz;
using SPC.ADTR.Biz;
using SPC.BSIF.Biz;
using System.Text;
using System.Xml;
using System.Web.Script.Services;
using CTF.Web.Framework.Helper;


namespace SPC.WebUI.Pages.ADTR
{
    public partial class ADTR0101_Image : WebUIBasePage
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        DataSet ds = null;
        string[] procResult = { "2", "Unknown Error" };
        protected string ImageURL = String.Empty;
        protected string iconURL = String.Empty;
        protected string[] arrBullet = { "blue.gif", "gray.gif", "green.gif", "red.gif" };
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

            //ImagURL = string.Format("../../Resources/controls/login/{0}/Image", gsLOGINPGMID);
            ImageURL = Page.ResolveUrl(String.Format("~/API/Common/Gathering/Download.ashx?code={0}&name={1}&gbn={2}", gsCOMPCD, "monitoring.png", "monitoring"));
            iconURL = Page.ResolveUrl("~/Resources/icons/");

            if (CommonHelper.GetAppSectionsString("BulletSize").Equals("14"))
                arrBullet = new string[] { "14x14_blue.png", "14x14_gray.png", "14x14_green.png", "14x14_red.png" };

            if (String.IsNullOrEmpty(hidGridAction.Text) || hidGridAction.Text.Equals("false"))
            {
                // PC정보를 구한다
                QCDPCNM_ADTR0101_LST();
            }
            
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
                //QCDPCNM_ADTR0101_LST();
                // 페이지 초기화
                Web_Init();

                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "";
                pnlSearch.JSProperties["cpResultMsg"] = "";
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

        #region PC정보를 구한다
        void QCDPCNM_ADTR0101_LST()
        {
            string errMsg = String.Empty;

            using (ADTRBiz biz = new ADTRBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                oParamDic.Add("F_FROMDT", GetFromDt());
                oParamDic.Add("F_TODT", GetToDt());


                ds = biz.QCDPCNM_ADTR0101_LST(oParamDic, out errMsg);
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                // Grid Callback Init
                pnlSearch.JSProperties["cpResultCode"] = "0";
                pnlSearch.JSProperties["cpResultMsg"] = errMsg;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }


                pnlSearch.JSProperties["cpTable"] = serializer.Serialize(rows);
            }
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region devGrid CustomCallback
        /// <summary>
        /// devGrid_CustomCallback
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ASPxGridViewCustomCallbackEventArgs</param>
        protected void pnlSearch_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            QCDPCNM_ADTR0101_LST();
        }
        #endregion

        #endregion
    }
}