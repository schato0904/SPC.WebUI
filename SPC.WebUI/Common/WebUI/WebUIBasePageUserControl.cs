using System;
using System.Web.UI;

namespace SPC.WebUI.Common
{
	public class WebUIBasePageUserControl : System.Web.UI.UserControl
	{
        #region Cast WebUIBasePage
        public new WebUIBasePage Page
        {
            get { return (WebUIBasePage)base.Page; }
        }
        #endregion

        #region Common Method : 재정의 하여 사용
        public virtual string GetValue()
        {
            return string.Empty;
        }
        #endregion

        #region Find UserControl Control Value

        #region 업체
        public string GetCompCD()
        {
            string resultCode = String.Empty;

            if (Page.gsVENDOR)
                resultCode = Page.gsCOMPCD;
            else
            {
                UserControl _UserControl = Parent.FindControl("ucComp") as UserControl;

                if (_UserControl != null)
                {
                    DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMPCD") as DevExpress.Web.ASPxTextBox;
                    resultCode = _hidTextBox.Text;
                }
            }

            return resultCode;
        }
        #endregion

        #region 공장
        public string GetFactCD()
        {
            string resultCode = String.Empty;

            if (Page.gsVENDOR)
                resultCode = Page.gsFACTCD;
            else
            {
                UserControl _UserControl = Parent.FindControl("ucFact") as UserControl;

                if (_UserControl != null)
                {
                    DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidFACTCD") as DevExpress.Web.ASPxTextBox;
                    resultCode = _hidTextBox.Text;
                }
            }

            return resultCode;
        }
        #endregion

        #region 반
        public string GetBanCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucBan") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidBANCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 라인
        public string GetLineCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucLine") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidLINECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 품목
        public string GetItemCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucItem") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidITEMCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정
        public string GetWorkCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucWork") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공정팝업
        public string GetWorkPOPCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucWorkPOP") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidWORKPOPCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 검사항목
        public string GetInspItemCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucInspectionItem") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidINSPITEMCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 검사유형
        public string GetInspectionCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucInspection") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidINSPECTION") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region From일자
        public string GetFromDt()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucDate") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCFROMDT") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region To일자
        public string GetToDt()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucDate") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidUCTODT") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 공통코드
        public string GetCommonCode()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucCommonCode") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidCOMMONCODECD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 4M
        public string Get4MCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucFourM") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hid4MCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #region 설비
        public string GetEquipCD()
        {
            string resultCode = String.Empty;

            UserControl _UserControl = Parent.FindControl("ucEquip") as UserControl;

            if (_UserControl != null)
            {
                DevExpress.Web.ASPxTextBox _hidTextBox = _UserControl.FindControl("hidEQUIPCD") as DevExpress.Web.ASPxTextBox;
                resultCode = _hidTextBox.Text;
            }

            return resultCode;
        }
        #endregion

        #endregion
	}
}