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
    public partial class AAI113RPT : DevExpress.XtraReports.UI.XtraReport
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
        public AAI113RPT(
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
            DevExpressLib.SetText(xrTableCell1, String.Format("{0}{1}({2} - {3})", drQWK13M["F_TYPENM"], Environment.NewLine, drQWK13M["F_ITEMNM"], drQWK13IM["F_INMATERIAL"]));
            // 작성
            DevExpressLib.SetText(xrTableCell6, drQWK13IM["F_WRITER"].ToString());
            // 검토
            DevExpressLib.SetText(xrTableCell7, drQWK13IM["F_REVIEWER"].ToString());
            // 승인
            DevExpressLib.SetText(xrTableCell8, drQWK13IM["F_APPROVER"].ToString());
            // 문서번호
            DevExpressLib.SetText(xrTableCell9, drQWK13M["F_DOCNUM"].ToString());
            // 원료명
            DevExpressLib.SetText(xrTableCell13, drQWK13IM["F_MATERIAL"].ToString());
            // 로트번호
            DevExpressLib.SetText(xrTableCell15, drQWK13IM["F_LOTNO"].ToString());
            // 입고일자
            DevExpressLib.SetText(xrTableCell17, drQWK13IM["F_INDATE"].ToString());
            // 검사일자
            DevExpressLib.SetText(xrTableCell19, drQWK13IM["F_WORKDATE"].ToString());
            // 검사원
            DevExpressLib.SetText(xrTableCell21, drQWK13IM["F_WORKMAN"].ToString());
            // 입고수량
            DevExpressLib.SetText(xrTableCell23, String.Format("{0:#,###} {1}", !String.IsNullOrEmpty(drQWK13IM["F_INCNT"].ToString()) ? Convert.ToInt32(drQWK13IM["F_INCNT"]) : 0, drQWK13IM["F_INUNIT"]));
            // 특기사항
            DevExpressLib.SetText(xrTableCell24, drQWK13M["F_REMARK"].ToString());
            // 종합판정
            DevExpressLib.SetText(xrTableCell26, drQWK13IM["F_JUDGE"].ToString().Equals("1") ? "합격" : "불합격");
            
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
            int i = 0, j = 0, k = 0, x = 0, nSiryo = 0, nDivisionCnt = 0, nInspCnt = 0, curIdx = 0, preIdx = 0;
            string curDisplayNo = String.Empty, preDisplayNo = String.Empty, sMax = String.Empty, sMin = String.Empty, sUnit = String.Empty, sStandard = String.Empty;
            DevExpress.XtraPrinting.BorderSide borderSide = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);

            ///*
            //AAI201	일반용
            //AAI202	치수용
            //AAI203	개정정보용
            //AAI204	외관용
            //AAI299	공정관리용
            // */

            foreach (DataRow dr in this.dtQWK13D.Rows)
            {
                isTransParent = dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? false : Convert.ToBoolean(dr["F_TRANSPARENT"]);
                isExcept = dr["F_ASSORTMENT"].ToString().Equals("AAI202") ? false : Convert.ToBoolean(dr["F_ISEXCEPT"]);
                nDivisionCnt = Convert.ToInt32(this.dtQWK13D.Select(String.Format("F_DIVISIONIDX={0}", dr["F_DIVISIONIDX"])).Length);
                curIdx = Convert.ToInt32(dr["F_DIVISIONIDX"]);

                if (!dr["F_ASSORTMENT"].ToString().Equals("AAI202"))    // 치수가 아닌경우
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 35F;

                    // 순번
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
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
                            if (nDivisionCnt > 1)
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
                        xrCell.WidthF = 90F;
                        xrCell.Text = dr["F_INSPNM"].ToString();
                        xrCell.Multiline = true;
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 130F;
                        xrCell.Text = dr["F_DIVISIONNM"].ToString();
                        xrCell.Multiline = true;
                        xrRow.Cells.Add(xrCell);
                    }

                    // 단위
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 50F;
                    if (!isTransParent)
                    {
                        xrCell.Text = dr["F_GROUPCNT"].ToString();
                    }
                    else
                    {
                        xrCell.Text = dr["F_METHOD"].ToString();
                    }
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 시험방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 120F;
                    xrCell.Text = dr["F_EQUIPMENT"].ToString();
                    xrCell.Multiline = true;
                    xrRow.Cells.Add(xrCell);

                    // 품질기준
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 227F;
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                    xrCell.Text = dr["F_STANDARD"].ToString();
                    xrCell.Multiline = true;
                    xrRow.Cells.Add(xrCell);

                    // 검사결과
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 55F;
                    xrCell.Text = dr["F_ASSORTMENT"].ToString().Equals("AAI204") ? String.Format("이상{0}없음", Environment.NewLine) : dr["F_TERM"].ToString();
                    xrCell.Multiline = true;
                    xrRow.Cells.Add(xrCell);

                    // 검사결과
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 55F;
                    xrCell.Text = dr["F_ASSORTMENT"].ToString().Equals("AAI204") ? String.Format("이상{0}없음", Environment.NewLine) : dr["F_TERM"].ToString();
                    xrCell.Multiline = true;
                    xrRow.Cells.Add(xrCell);

                    // 판정
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 50F;
                    xrCell.Text = GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    if (i.Equals(0))
                        xrRow.Borders = borderSide;

                    xrTable.Rows.Add(xrRow);

                    preIdx = curIdx;

                    i++;
                }
                else // 치수인 경우
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 35F;

                    if (x.Equals(0))
                    {
                        // 순번
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = (i + 1).ToString();
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = nDivisionCnt;
                        xrRow.Cells.Add(xrCell);

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 130F;
                        xrCell.Text = dr["F_DIVISIONNM"].ToString();
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = nDivisionCnt;
                        xrRow.Cells.Add(xrCell);

                        // 단위
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 50F;
                        xrCell.Text = dr["F_METHOD"].ToString();
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = nDivisionCnt;
                        xrRow.Cells.Add(xrCell);

                        // 시험방법
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 120F;
                        xrCell.Text = dr["F_EQUIPMENT"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = nDivisionCnt;
                        xrRow.Cells.Add(xrCell);

                        i++;
                    }
                    else
                    {
                        // 순번
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 130F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 단위
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 50F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 시험방법
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 120F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    if (Convert.ToInt32(dr["F_GROUPCNT"]) > 1)
                    {
                        nInspCnt = Convert.ToInt32(dr["F_GROUPCNT"]);
                        nSiryo = nInspCnt;
                    }

                    if (nSiryo.Equals(nInspCnt))
                    {
                        // 품질기준
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = dr["F_INSPNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.RowSpan = nSiryo;
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        // 품질기준
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    // 품질기준
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 40F;
                    xrCell.Text = dr["F_STANDARD"].ToString();
                    xrCell.Multiline = true;
                    xrRow.Cells.Add(xrCell);

                    j = 0;
                    bool bJudge = true;

                    foreach (DataRow drSub in this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", dr["F_TERM"])))
                    {
                        if (bJudge)
                            bJudge = !drSub["F_NGOKCHK"].ToString().Equals("1");

                        sStandard = drSub["F_STANDARD"].ToString();
                        sMax = drSub["F_MAX"].ToString();
                        sMin = drSub["F_MIN"].ToString();

                        if (!String.IsNullOrEmpty(sMax) && !String.IsNullOrEmpty(sMin))
                        {
                            if (sMax == sMin)
                                sStandard = String.Format("{0}±{1}{2}",sStandard, sMin, sMax);
                            else
                                sStandard = String.Format("{0}+{2}-{1}", sStandard, sMin, sMax);
                        }
                        else if (!String.IsNullOrEmpty(sMax))
                            sStandard = String.Format("{0}+{1}",sStandard, sMax);
                        else if (!String.IsNullOrEmpty(sMin))
                            sStandard = String.Format("{0}-{1}", sStandard, sMin);

                        if (j.Equals(0))
                        {
                            // 품질기준
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 127F;
                            xrCell.Text = sStandard;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        // 측정값
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 55F;
                        xrCell.Text = drSub["F_MEASURE"].ToString();
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        j++;
                    }

                    for (k = j; k < 2; k++)
                    {
                        // 측정값
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 55F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    // 판정
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 50F;
                    xrCell.Text = !bJudge ? GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[1] : GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);

                    nSiryo--;
                    x++;
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
