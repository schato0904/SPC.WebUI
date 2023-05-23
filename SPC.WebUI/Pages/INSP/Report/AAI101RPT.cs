using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SPC.WebUI.Common;
using CTF.Web.Framework.Helper;

namespace SPC.WebUI.Pages.INSP.Report
{
    public partial class AAI101RPT : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수

        DataTable dtQWK13M = new DataTable();
        DataTable dtQWK13D = new DataTable();
        DataTable dtQWK13IM = new DataTable();
        DataTable dtQWK13ID = new DataTable();
        DataTable dtQWK13IS = new DataTable();
        string sJUDGETP = String.Empty;

        #endregion

        #region 프로퍼티
        CommonCode CachecommonCode
        {
            get
            {
                CommonCode commonCode = new CommonCode();

                if (!CacheHelper.Exists("SPCCommonCode"))
                {
                    CreateCommonCodeCache(commonCode);
                }
                else
                {
                    CacheHelper.Get("SPCCommonCode", out commonCode);

                    if (null == commonCode)
                    {
                        CreateCommonCodeCache(commonCode);
                    }
                }

                return commonCode;
            }
        }
        #endregion

        #region 생성자
        public AAI101RPT(
            DataTable _dtQWK13M,
            DataTable _dtQWK13D,
            DataTable _dtQWK13IM,
            DataTable _dtQWK13ID,
            DataTable _dtQWK13IS)
        {
            this.dtQWK13M = _dtQWK13M;
            this.dtQWK13D = _dtQWK13D;
            this.dtQWK13IM = _dtQWK13IM;
            this.dtQWK13ID = _dtQWK13ID;
            this.dtQWK13IS = _dtQWK13IS;

            InitializeComponent();

            // 헤더 정보 입력
            SetHeader();

            // 컨텐츠 정보입력
            SetContent();
        }
        #endregion

        #region 사용자정의 함수

        #region 전역캐쉬 가져오기
        CommonCode.CodeDic GetCommonCodeDic(string code)
        {
            try
            {
                CommonCode.CodeDic codeDic = CachecommonCode[code.Substring(0, 2)];

                for (int i = 2; i < code.Length; i += 2)
                {
                    codeDic = codeDic[code.Substring(0, i + 2)];

                    // 코드가 존재하지 않을경우, 공백 반환
                    if (codeDic == null) return null;
                }

                return codeDic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 헤더 정보 입력
        void SetHeader()
        {
            DataRow drQWK13M = this.dtQWK13M.Rows[0];
            DataRow drQWK13IM = this.dtQWK13IM.Rows[0];

            // 성적서 메인 타이틀
            DevExpressLib.SetText(xrTableCell4, drQWK13M["F_TYPENM"].ToString());
            // 성적서 품명
            DevExpressLib.SetText(xrTableCell10, String.Format("품명 : {0} ({1})", drQWK13IM["F_INITEMNM"], drQWK13IM["F_KSINFO"]));
            // 재료, SDR, 형태
            DevExpressLib.SetText(xrTableCell16, String.Format("(재료 : {0}, SDR : {1}, 형태 : {2})", drQWK13M["F_MATERIAL"], drQWK13M["F_SDR"], drQWK13M["F_SHAPE"]));
            // 종류 및 호칭
            DevExpressLib.SetText(xrTableCell23, drQWK13IM["F_TITLE"].ToString());
            // 로트번호
            DevExpressLib.SetText(xrTableCell25, drQWK13IM["F_LOTNO"].ToString());
            // 로트크기
            DevExpressLib.SetText(xrTableCell27, String.Format("{0:#,###} {1}", !String.IsNullOrEmpty(drQWK13IM["F_LOTCNT"].ToString()) ? Convert.ToInt32(drQWK13IM["F_LOTCNT"]) : 0, drQWK13IM["F_UNIT"]));
            // 검사일자
            DevExpressLib.SetText(xrTableCell29, drQWK13IM["F_WORKDATE"].ToString());
            // 검사방식 및 조건
            DevExpressLib.SetText(xrTableCell31, drQWK13M["F_CONDITION"].ToString());
            // 검사원
            DevExpressLib.SetText(xrTableCell33, drQWK13IM["F_WORKMAN"].ToString());
            // 문서번호
            DevExpressLib.SetText(xrTableCell1, drQWK13M["F_DOCNUM"].ToString());
            // 특기사항
            DevExpressLib.SetText(xrTableCell9, drQWK13M["F_REMARK"].ToString());
            // 종합판정
            DevExpressLib.SetText(xrTableCell41, drQWK13IM["F_JUDGE"].ToString().Equals("1") ? "합격" : "불합격");
            // 작성
            DevExpressLib.SetText(xrTableCell42, drQWK13IM["F_WRITER"].ToString());
            // 검토
            DevExpressLib.SetText(xrTableCell43, drQWK13IM["F_REVIEWER"].ToString());
            // 승인
            DevExpressLib.SetText(xrTableCell44, drQWK13IM["F_APPROVER"].ToString());
            // 판정구분
            sJUDGETP = drQWK13M["F_JUDGETP"].ToString();
        }
        #endregion

        #region 컨텐츠 정보입력
        void SetContent()
        {
            XRTable xrTable = null;
            XRTableRow xrRow = null;
            XRTableCell xrCell = null;

            xrTable = new XRTable()
            {
                Font = new System.Drawing.Font("맑은 고딕", 8F, FontStyle.Regular),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1F,
                BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanGrow = false
            };
            xrTable.BeginInit();

            bool isTransParent = false, isExcept = false;
            int i = 0, j = 0, k = 0, nSiryo = 0, nDivisionCnt = 0, nInspCnt = 0, curIdx = 0, preIdx = 0;
            string curDisplayNo = String.Empty, preDisplayNo = String.Empty, sMax = String.Empty, sMin = String.Empty, sUnit = String.Empty, sStandard = String.Empty;
            DevExpress.XtraPrinting.BorderSide borderSide = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);

            foreach (DataRow dr in this.dtQWK13ID.Rows)
            {
                nSiryo = Convert.ToInt32(dr["F_INSPCNT"]);
                break;
            }

            /*
            AAI201	일반용
            AAI202	치수용
            AAI203	개정정보용
            AAI204	외관용
            AAI299	공정관리용
             */

            foreach (DataRow dr in this.dtQWK13D.Rows)
            {
                isTransParent = dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? false : Convert.ToBoolean(dr["F_TRANSPARENT"]);
                isExcept = dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? false : Convert.ToBoolean(dr["F_ISEXCEPT"]);
                nDivisionCnt = !dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? Convert.ToInt32(this.dtQWK13D.Select(String.Format("F_DIVISIONIDX={0}", dr["F_DIVISIONIDX"])).Length) : nSiryo;
                nInspCnt = !dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? Convert.ToInt32(dr["F_GROUPCNT"]) : 1;
                curIdx = Convert.ToInt32(dr["F_DIVISIONIDX"]);

                if (!dr["F_ASSORTMENT"].ToString().Equals("AAI202"))    // 치수가 아닌경우
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 35F;

                    // 순번
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 30F;
                    xrCell.Text = (i + 1).ToString();
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    if (!isTransParent)
                    {
                        if (!curIdx.Equals(preIdx))
                        {
                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 40F;
                            xrCell.Text = dr["F_DIVISIONNM"].ToString();
                            xrCell.Multiline = true;
                            xrCell.CanGrow = false;
                            if (nDivisionCnt > 0)
                                xrCell.RowSpan = nDivisionCnt;
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 40F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 110F;
                        xrCell.Text = dr["F_INSPNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        if (nInspCnt > 1)
                            xrCell.RowSpan = nInspCnt;
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 150F;
                        xrCell.Text = dr["F_DIVISIONNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    if (dr["F_ASSORTMENT"].ToString().Equals("AAI204"))
                    {
                        // 판정기준
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 187F;
                        xrCell.Text = dr["F_STANDARD"].ToString();
                        xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                        xrCell.Multiline = true;
                        xrRow.Cells.Add(xrCell);

                        for (j = 0; j < 5; j++)
                        {
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = "이상없음";
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }
                    else if (dr["F_ASSORTMENT"].ToString().Equals("AAI202"))
                    {
                        // 판정기준
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 187F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        for (j = 0; j < 5; j++)
                        {
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = "이상없음";
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        // 판정기준
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 367F;
                        xrCell.Text = dr["F_STANDARD"].ToString();
                        xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0);
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        if (!isExcept)
                        {
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 120F;
                            xrCell.Text = dr["F_TERM"].ToString();
                            xrCell.Multiline = true;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 180F;
                            xrCell.Text = "해당사항없음";
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }
                    }

                    if (i.Equals(0))
                        xrRow.Borders = borderSide;

                    xrTable.Rows.Add(xrRow);

                    preIdx = curIdx;

                    i++;
                }
                else // 치수인 경우
                {
                    bool bJudge = true;

                    if (this.dtQWK13ID.Rows.Count > 0)
                    {
                        xrRow = new XRTableRow();
                        xrRow.HeightF = 35F;

                        // 순번
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 30F;
                        xrCell.Text = (i + 1).ToString();
                        if (nDivisionCnt > 1)
                            xrCell.RowSpan = nDivisionCnt;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = dr["F_DIVISIONNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        if (nDivisionCnt > 1)
                            xrCell.RowSpan = nDivisionCnt;
                        xrRow.Cells.Add(xrCell);

                        i++;
                    }

                    foreach (DataRow drSub in this.dtQWK13ID.Rows)
                    {
                        curDisplayNo = drSub["F_DISPLAYNO"].ToString();
                        if (bJudge)
                            bJudge = !drSub["F_NGOKCHK"].ToString().Equals("1");

                        if (!curDisplayNo.Equals(preDisplayNo))
                        {
                            if (!String.IsNullOrEmpty(preDisplayNo))
                            {
                                for (k = j; k < 5; k++)
                                {
                                    // 측정값
                                    xrCell = new XRTableCell();
                                    xrCell.WidthF = 60F;
                                    xrCell.Text = String.Empty;
                                    xrCell.CanGrow = false;
                                    xrRow.Cells.Add(xrCell);
                                }

                                xrCell = new XRTableCell();
                                xrCell.WidthF = 60F;
                                xrCell.Text = !bJudge ? GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[1] : GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                                xrCell.CanGrow = false;
                                xrRow.Cells.Add(xrCell);

                                xrTable.Rows.Add(xrRow);

                                xrRow = new XRTableRow();
                                xrRow.HeightF = 35F;

                                // 순번
                                xrCell = new XRTableCell();
                                xrCell.WidthF = 30F;
                                xrCell.Text = String.Empty;
                                xrCell.CanGrow = false;
                                xrRow.Cells.Add(xrCell);

                                // 검사항목
                                xrCell = new XRTableCell();
                                xrCell.WidthF = 40F;
                                xrCell.Text = String.Empty;
                                xrCell.CanGrow = false;
                                xrRow.Cells.Add(xrCell);
                            }

                            j = 0;

                            sUnit = !String.IsNullOrEmpty(drSub["F_UNIT"].ToString()) ? GetCommonCodeDic(drSub["F_UNIT"].ToString()).COMMNMKR : "";
                            sMax = drSub["F_MAX"].ToString();
                            sMin = drSub["F_MIN"].ToString();

                            if (!String.IsNullOrEmpty(sMax) && !String.IsNullOrEmpty(sMin))
                                sStandard = String.Format("{0}{2} ~ {1}{2}", sMin, sMax, sUnit);
                            else if (!String.IsNullOrEmpty(sMax))
                                sStandard = String.Format("≤ {0}{1}", sMax, sUnit);
                            else if (!String.IsNullOrEmpty(sMin))
                                sStandard = String.Format("≥ {0}{1}", sMin, sUnit);

                            
                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 110F;
                            xrCell.Text = drSub["F_INSPDETAIL"].ToString();
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 판정기준
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 187F;
                            xrCell.Text = sStandard;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = drSub["F_MEASURE"].ToString();
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }
                        else
                        {
                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = drSub["F_MEASURE"].ToString();
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        preDisplayNo = curDisplayNo;
                        j++;
                    }

                    for (k = j; k < 5; k++)
                    {
                        // 측정값
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    xrCell = new XRTableCell();
                    xrCell.WidthF = 60F;
                    xrCell.Text = !bJudge ? GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[1] : GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);
                }
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);
        }
        #endregion

        #endregion

        #region 사용자이벤트

        #region CreateCommonCodeCache
        /// <summary>
        /// CreateCommonCodeCache
        /// </summary>
        /// <param name="commonCode">CommonCode</param>
        void CreateCommonCodeCache(CommonCode commonCode)
        {
            commonCode.LoadCommonCode();
            CacheHelper.Add(commonCode, "SPCCommonCode");
        }
        #endregion

        #endregion
    }
}
