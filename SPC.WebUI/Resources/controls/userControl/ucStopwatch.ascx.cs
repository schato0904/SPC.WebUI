using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPC.WebUI.Resources.controls.userControl
{
    public partial class ucStopwatch : System.Web.UI.UserControl
    {
        #region 사용자 정의 속성, 프로퍼티

        #region 변수선언
        #endregion

        #region 프로퍼티
        private string _ClientInstanceName = "ddlDIFF";
        public string ClientInstanceName
        {
            get { return this._ClientInstanceName; }
            set { this._ClientInstanceName = value; }
        }

        private string _CallbackEvent = "NullCallback";
        public string CallbackEvent
        {
            get { return this._CallbackEvent; }
            set { this._CallbackEvent = value; }
        }

        private string _ItemList = "";
        public string ItemList
        {
            get { return this._ItemList; }
            set { this._ItemList = value; }
        }

        private string _ShowRemaintime = "false";
        public string ShowRemaintime
        {
            get { return this._ShowRemaintime; }
            set { this._ShowRemaintime = value; }
        }
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

            if (!IsPostBack)
                ddlDIFF.DataBind();
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

        #endregion

        #region 사용자이벤트

        protected void ddlDIFF_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.ClientInstanceName = String.Format("ddl{0}", ClientInstanceName);
            comboBox.ClientSideEvents.SelectedIndexChanged = String.Format("{0}SelectedIndexChanged", ClientInstanceName);
        }

        #endregion

        protected void ddlDIFF_DataBinding(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxComboBox comboBox = sender as DevExpress.Web.ASPxComboBox;
            comboBox.Items.Clear();

            string ItemText = String.Empty;
            Int32 ItemValue = 0;
            string[] Item = new string[2];

            foreach (string itemList in ItemList.Split(';'))
            {
                Item = itemList.Split('|');
                switch (Item[0])
                {
                    case "M":
                        ItemText = String.Format("{0}분", Item[1]);
                        ItemValue = Convert.ToInt32(Item[1]) * 60 * 1000;
                        break;
                    case "H":
                        ItemText = String.Format("{0}시간", Item[1]);
                        ItemValue = Convert.ToInt32(Item[1]) * 60 * 60 * 1000;
                        break;
                    case "D":
                        ItemText = String.Format("{0}일", Item[1]);
                        ItemValue = Convert.ToInt32(Item[1]) * 60 * 60 * 24 * 1000;
                        break;
                }
                comboBox.Items.Add(ItemText, ItemValue);
            }

            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }
    }
}