using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.Data;
using SPC.WebUI.Common;
using SPC.FDCK.Biz;
using DevExpress.Web.ASPxTreeList;

namespace SPC.WebUI.Pages.FDCK.Popup
{
    public partial class FDCKUSERPOP : WebUIBasePage
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
            if ( !IsCallback )
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
                //devGrid.JSProperties["cpResultCode"] = "";
                //devGrid.JSProperties["cpResultMsg"] = "";
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
            // 콜백함수명 처리
            hidCallbackFn.Text = Request.QueryString.Get("CALLBACKFN") ?? "";
            // 파라미터로 넘겨받은 부서, 사용자에 기본 체크 설정
            var keyfields = Request.QueryString.Get("KEYFIELDS") ?? "";
            if (!string.IsNullOrWhiteSpace(keyfields))
            {
                List<string> teamList = keyfields.Split('|')[0].Split(';').ToList<string>();
                List<string> userList = keyfields.Split('|')[1].Split(';').ToList<string>();
                if (teamList.Count > 0 || userList.Count > 0)
                {
                    TreeListNodeIterator iterator = devTree.CreateNodeIterator();
                    TreeListNode node;
                    while (true)
                    {
                        node = iterator.GetNext();
                        if (node == null) break;

                        if (((DataRowView)node.DataItem)["F_TYPE"].ToString() == "T")
                        {
                            if (teamList.Contains(((DataRowView)node.DataItem)["F_CODE"].ToString()))
                            {
                                node.Selected = true;
                            }
                        }
                        else
                        {
                            if (userList.Contains(((DataRowView)node.DataItem)["F_CODE"].ToString()))
                            {
                                node.Selected = true;
                            }
                        }
                    }
                }
            }
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
        { }
        #endregion

        #endregion

        #region 사용자 정의 함수

        #region 검사항목목록
        void USER_LST(string keyParams = "")
        {
            string errMsg = String.Empty;

            using (FDCKBiz biz = new FDCKBiz())
            {
                oParamDic.Clear();
                oParamDic.Add("F_COMPCD", gsCOMPCD);
                oParamDic.Add("F_FACTCD", gsFACTCD);
                ds = biz.SYUSR01_LST1(oParamDic, out errMsg);
            }

            if (ds.Tables.Count > 0)
            {                
                devTree.DataSource = ds.Tables[0];
                devTree.DataBind();
            }
        }
        #endregion

        #region 트리 설정
        void devTree_Settings()
        {
            //TreeListNodeIterator iterator = devTree.CreateNodeIterator();
            //TreeListNode node;
            //while (true)
            //{
            //    node = iterator.GetNext();
            //    if (node == null) break;

            //    // 부서는 선택 불가
            //    if (((DataRowView)node.DataItem)["F_TYPE"].ToString() == "T")
            //    {
            //        node.AllowSelect = false;
            //    }
            //    else
            //    {
            //        node.AllowSelect = true;
            //    }
            //}
            TreeListNodeIterator iterator = devTree.CreateNodeIterator();
            TreeListNode node = iterator.GetNext();
            while (node != null)
            {
                node.AllowSelect = !node.HasChildren;
                node = iterator.GetNext();                
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
        protected void devGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // 검사항목목록
            //INSPITEM_LST(e.Parameters);
        }
        #endregion

        #region devTree_Init
        protected void devTree_Init(object sender, EventArgs e)
        {
            // 목록 조회
            USER_LST();
            //devTree_Settings();
        } 
        #endregion

        #region devPopCallback_Callback
        protected void devPopCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string resultTeam = string.Empty;
            string resultUser = string.Empty;
            string resultNm = string.Empty;

            TreeListNodeIterator iterator = devTree.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
                if (node.Selected)
                {
                    resultNm += ((DataRowView)node.DataItem)["F_CODENM"].ToString() + ";";
                    // 사용자와 부서를 구분하여 파라미터 전달
                    if (((DataRowView)node.DataItem)["F_TYPE"].ToString() == "T")
                    {
                        resultTeam += ((DataRowView)node.DataItem)["F_CODE"].ToString() + ";";
                    }
                    else
                    {
                        resultUser += ((DataRowView)node.DataItem)["F_CODE"].ToString() + ";";
                    }
                    
                }
            }

            resultTeam = resultTeam.TrimEnd(';');
            resultUser = resultUser.TrimEnd(';');
            resultNm = resultNm.TrimEnd(';');

            if (!string.IsNullOrWhiteSpace(resultNm))
            {
                e.Result = string.Format("{0}|{1}|{2}", resultNm, resultTeam, resultUser);
            }
            else
            {
                e.Result = "";
            }
        }
        #endregion

        #region devTree_DataBound
        protected void devTree_DataBound(object sender, EventArgs e)
        {
            this.devTree_Settings();
        } 
        #endregion

        #endregion
    }
}