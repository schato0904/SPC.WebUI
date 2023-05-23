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
    public partial class AAI123RPT : DevExpress.XtraReports.UI.XtraReport
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
        public AAI123RPT(
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
            DevExpressLib.SetText(xrTableCell1, drQWK13M["F_TYPENM"].ToString());
            // 사출기
            DevExpressLib.SetText(xrTableCell8, drQWK13IM["F_INJMACHINE"].ToString());
            // 제품명
            DevExpressLib.SetText(xrTableCell12, drQWK13IM["F_INITEMNM"].ToString());
            // 형식
            DevExpressLib.SetText(xrTableCell13, drQWK13IM["F_INTYPE"].ToString());
            // 생산일자
            DevExpressLib.SetText(xrTableCell14, String.Format("■생산일자:{0} ({1})", Convert.ToDateTime(drQWK13IM["F_MAKEDATE"]).ToString("yyyy년 MM월 dd일"), drQWK13IM["F_DAYNIGHT"].ToString().Equals("D") ? "주간" : "야간"));
            // 검사방식
            DevExpressLib.SetText(xrTableCell29, drQWK13M["F_CONDITION"].ToString());
            // 제품로트번호
            DevExpressLib.SetText(xrTableCell17, drQWK13IM["F_LOTNO"].ToString());
            // 원재료로트번호
            DevExpressLib.SetText(xrTableCell21, drQWK13IM["F_RAWLOTNO"].ToString());
            // 전열선로트번호
            DevExpressLib.SetText(xrTableCell25, drQWK13IM["F_WIRELOTNO"].ToString());
            // 종합판정
            DevExpressLib.SetText(xrTableCell22, drQWK13IM["F_JUDGE"].ToString().Equals("1") ? "합격" : "불합격");
            // 문서번호
            DevExpressLib.SetText(xrTableCell9, drQWK13M["F_DOCNUM"].ToString());

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

            DataRow drStandard;
            bool isTransParent = false;
            int i = 0, j = 0, k = 0, x = 0, nSiryo = 0;
            string curDisplayNo = String.Empty, preDisplayNo = String.Empty, sMax = String.Empty, sMin = String.Empty, sUnit = String.Empty, sStandard = String.Empty;
            DevExpress.XtraPrinting.BorderSide borderSide = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);

            nSiryo = 0;

            foreach(DataRow dr in this.dtQWK13D.Select("F_ASSORTMENT='AAI202'"))
            {
                nSiryo = dr["F_GROUPCNT"].ToString().Split(',').Length;
            }

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

                if (dr["F_ASSORTMENT"].ToString().Equals("AAI201") || dr["F_ASSORTMENT"].ToString().Equals("AAI204"))    // 치수가 아닌 경우
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 100F;

                    if (x.Equals(0))
                    {
                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = String.Format("중{0}간{0}검{0}사", Environment.NewLine);
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = 3 + (nSiryo.Equals(0) ? 1 : nSiryo);
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
                    xrCell.WidthF = 60F;
                    xrCell.Text = dr["F_DIVISIONNM"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    if (dr["F_ASSORTMENT"].ToString().Equals("AAI204") || isTransParent)
                    {
                        // 검사기준 및 검사방법
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 300F;
                        xrCell.Text = dr["F_STANDARD"].ToString();
                        xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                        xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }
                    else
                    {
                        // 서브 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = dr["F_INSPNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        sStandard = String.Empty;
                        if (this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", dr["F_GROUPCNT"])).Length > 0)
                        {
                            drStandard = this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", dr["F_GROUPCNT"]))[0];

                            sStandard = drStandard["F_STANDARD"].ToString();
                            sMax = drStandard["F_MAX"].ToString();
                            sMin = drStandard["F_MIN"].ToString();

                            if (!String.IsNullOrEmpty(sMax) && !String.IsNullOrEmpty(sMin))
                            {
                                if (sMax == sMin)
                                    sStandard = String.Format("{0}±{1}", sStandard, sMax);
                                else
                                    sStandard = String.Format("{0}{3}+{2}-{1}", sStandard, sMin, sMax, Environment.NewLine);
                            }
                            else if (!String.IsNullOrEmpty(sMax))
                                sStandard = String.Format("{0}+{1}", sStandard, sMax);
                            else if (!String.IsNullOrEmpty(sMin))
                                sStandard = String.Format("{0}-{1}", sStandard, sMin);
                        }

                        // 검사기준 및 검사방법
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 240F;
                        xrCell.Text = sStandard;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    j = 0;
                    foreach(DataRow drSub in this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", dr["F_GROUPCNT"])))
                    {   
                        // 측정값
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 54F;
                        xrCell.Text = drSub["F_MEASURE"].ToString();
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                        j++;
                    }

                    for (k = j; k < 5; k++)
                    {
                        // 측정값
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 54F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);
                    }

                    // 판정
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 57F;
                    xrCell.Text = GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    if (i.Equals(0))
                        xrRow.Borders = borderSide;

                    xrTable.Rows.Add(xrRow);

                    x++;
                }
                else if (dr["F_ASSORTMENT"].ToString().Equals("AAI202"))    // 치수인 경우
                {
                    bool bJudge = true;

                    if (nSiryo.Equals(0))
                    {
                        xrRow = new XRTableRow();
                        xrRow.HeightF = 100F;

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 60F;
                        xrCell.Text = dr["F_DIVISIONNM"].ToString();
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        // 검사기준 및 검사방법
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 300F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        for (k = 1; k < 5; k++)
                        {
                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 54F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);
                        }

                        // 판정
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 57F;
                        xrCell.Text = String.Empty;
                        xrCell.CanGrow = false;
                        xrRow.Cells.Add(xrCell);

                        if (i.Equals(0))
                            xrRow.Borders = borderSide;

                        xrTable.Rows.Add(xrRow);
                    }
                    else
                    {
                        foreach (string F_MEAINSPCD in dr["F_GROUPCNT"].ToString().Split(','))
                        {
                            xrRow = new XRTableRow();
                            if (nSiryo.Equals(0))
                                xrRow.HeightF = 100F;
                            else
                                xrRow.HeightF = (float)(100F / nSiryo);

                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 40F;
                            xrCell.Text = String.Empty;
                            xrCell.Multiline = true;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 60F;
                            xrCell.Text = dr["F_DIVISIONNM"].ToString();
                            xrCell.Multiline = true;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            sStandard = String.Empty;
                            if (this.dtQWK13IS.Select(String.Format("F_MEAINSPCD='{0}'", F_MEAINSPCD)).Length > 0)
                            {
                                drStandard = this.dtQWK13IS.Select(String.Format("F_MEAINSPCD='{0}'", F_MEAINSPCD))[0];

                                sStandard = drStandard["F_STANDARD"].ToString();
                                sMax = drStandard["F_MAX"].ToString();
                                sMin = drStandard["F_MIN"].ToString();

                                if (!String.IsNullOrEmpty(sMax) && !String.IsNullOrEmpty(sMin))
                                {
                                    if (sMax == sMin)
                                        sStandard = String.Format("{0}±{1}", sStandard, sMax);
                                    else
                                        sStandard = String.Format("{0}{3}+{2}-{1}", sStandard, sMin, sMax, Environment.NewLine);
                                }
                                else if (!String.IsNullOrEmpty(sMax))
                                    sStandard = String.Format("{0}+{1}", sStandard, sMax);
                                else if (!String.IsNullOrEmpty(sMin))
                                    sStandard = String.Format("{0}-{1}", sStandard, sMin);
                            }

                            // 검사기준 및 검사방법
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 300F;
                            xrCell.Text = sStandard;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            j = 0;
                            bJudge = true;
                            foreach (DataRow drSub in this.dtQWK13IS.Select(String.Format("F_MEAINSPCD='{0}'", F_MEAINSPCD)))
                            {
                                if (bJudge)
                                    bJudge = !drSub["F_NGOKCHK"].ToString().Equals("1");

                                // 측정값
                                xrCell = new XRTableCell();
                                xrCell.WidthF = 54F;
                                xrCell.Text = drSub["F_MEASURE"].ToString();
                                xrCell.CanGrow = false;
                                xrRow.Cells.Add(xrCell);
                                j++;
                            }

                            for (k = j; k < 5; k++)
                            {
                                // 측정값
                                xrCell = new XRTableCell();
                                xrCell.WidthF = 54F;
                                xrCell.Text = String.Empty;
                                xrCell.CanGrow = false;
                                xrRow.Cells.Add(xrCell);
                            }

                            // 판정
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 57F;
                            xrCell.Text = !bJudge ? GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[1] : GetCommonCodeDic(sJUDGETP).COMMNMKR.Split('/')[0];
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            if (i.Equals(0))
                                xrRow.Borders = borderSide;

                            xrTable.Rows.Add(xrRow);
                        }
                    }
                }

                i++;
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.Detail1.Controls.Add(xrTable);

            // 공정관리
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

            // 라인
            xrRow = new XRTableRow();
            xrRow.HeightF = 3F;

            xrCell = new XRTableCell();
            xrCell.WidthF = 727F;
            xrCell.Text = String.Empty;
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            xrRow.Borders = borderSide;

            xrTable.Rows.Add(xrRow);

            // 타이틀
            xrRow = new XRTableRow();
            xrRow.HeightF = 25F;

            // 검사구분
            xrCell = new XRTableCell();
            xrCell.WidthF = 40F;
            xrCell.Text = String.Empty;
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            // 공정명
            xrCell = new XRTableCell();
            xrCell.WidthF = 70F;
            xrCell.Text = "공정명";
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            // 관리항목
            xrCell = new XRTableCell();
            xrCell.WidthF = 80F;
            xrCell.Text = "관리항목";
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            // 관리기준
            xrCell = new XRTableCell();
            xrCell.WidthF = 210F;
            xrCell.Text = "관리기준";
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            // 관리주기
            xrCell = new XRTableCell();
            xrCell.WidthF = 108F;
            xrCell.Text = "관리주기";
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            // 관리기록
            xrCell = new XRTableCell();
            xrCell.WidthF = 219F;
            xrCell.Text = "관리기록";
            xrCell.CanGrow = false;
            xrRow.Cells.Add(xrCell);

            xrTable.Rows.Add(xrRow);

            j = 0;
            nSiryo = this.dtQWK13D.Select("F_ASSORTMENT='AAI299'").Length;

            if (nSiryo.Equals(0))
            {
                xrRow = new XRTableRow();
                xrRow.HeightF = 150F;

                // 검사구분
                xrCell = new XRTableCell();
                xrCell.WidthF = 40F;
                xrCell.Text = String.Format("공{0}정{0}관{0}리", Environment.NewLine);
                xrCell.Multiline = true;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                // 공정명
                xrCell = new XRTableCell();
                xrCell.WidthF = 70F;
                xrCell.Text = String.Empty;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                // 관리항목
                xrCell = new XRTableCell();
                xrCell.WidthF = 80F;
                xrCell.Text = String.Empty;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                // 관리기준
                xrCell = new XRTableCell();
                xrCell.WidthF = 210F;
                xrCell.Text = String.Empty;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                // 관리주기
                xrCell = new XRTableCell();
                xrCell.WidthF = 108F;
                xrCell.Text = String.Empty;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                // 관리기록
                xrCell = new XRTableCell();
                xrCell.WidthF = 219F;
                xrCell.Text = String.Empty;
                xrCell.CanGrow = false;
                xrRow.Cells.Add(xrCell);

                xrTable.Rows.Add(xrRow);
            }
            else
            {
                foreach (DataRow dr in this.dtQWK13D.Select("F_ASSORTMENT='AAI299'", "F_DIVISIONIDX ASC"))
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 50F;
                    
                    if (j.Equals(0))
                    {
                        // 검사항목
                        xrCell = new XRTableCell();
                        xrCell.WidthF = 40F;
                        xrCell.Text = String.Format("공{0}정{0}관{0}리", Environment.NewLine);
                        xrCell.Multiline = true;
                        xrCell.CanGrow = false;
                        xrCell.RowSpan = nSiryo;
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

                    // 공정명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = dr["F_DIVISIONNM"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 관리항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 80F;
                    xrCell.Text = dr["F_METHOD"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 관리기준
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 210F;
                    xrCell.Text = dr["F_STANDARD"].ToString();
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 관리주기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 108F;
                    xrCell.Text = dr["F_EQUIPMENT"].ToString();
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 관리기록
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 219F;
                    xrCell.Text = dr["F_TERM"].ToString();
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);

                    j++;
                }
            }

            xrTable.AdjustSize();
            xrTable.EndInit();

            this.GroupFooter1.Controls.Add(xrTable);
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
