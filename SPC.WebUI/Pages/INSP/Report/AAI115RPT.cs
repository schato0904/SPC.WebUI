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
    public partial class AAI115RPT : DevExpress.XtraReports.UI.XtraReport
    {
        #region 변수

        DataTable dtQWK13M = new DataTable();
        DataTable dtQWK13D = new DataTable();
        DataTable dtQWK13IM = new DataTable();
        DataTable dtQWK13ID = new DataTable();
        DataTable dtQWK13IS = new DataTable();
        string sJUDGETP = String.Empty;
        string sDRAWIMG = String.Empty;

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
        public AAI115RPT(
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
            DevExpressLib.SetText(xrTableCell8, drQWK13M["F_TYPENM"].ToString().Replace("(품명)", String.Format("({0})", drQWK13IM["F_INITEMTITLE"])));
            // 작성
            DevExpressLib.SetText(xrTableCell25, drQWK13IM["F_WRITER"].ToString());
            // 검토
            DevExpressLib.SetText(xrTableCell26, drQWK13IM["F_REVIEWER"].ToString());
            // 승인
            DevExpressLib.SetText(xrTableCell27, drQWK13IM["F_APPROVER"].ToString());
            // 기준서NO
            DevExpressLib.SetText(xrTableCell21, drQWK13IM["F_INBASENO"].ToString());
            // 로트번호
            DevExpressLib.SetText(xrTableCell33, drQWK13IM["F_LOTNO"].ToString());
            // 제정일자
            DevExpressLib.SetText(xrTableCell45, drQWK13IM["F_ESTDATE"].ToString());
            // 입고일자
            DevExpressLib.SetText(xrTableCell23, drQWK13IM["F_INDATE"].ToString());
            // 열선외경
            DevExpressLib.SetText(xrTableCell35, String.Format("{0}{1}", drQWK13IM["F_THERMICOUT"], drQWK13IM["F_THERMICUNIT"]));
            // 피복외경
            DevExpressLib.SetText(xrTableCell47, String.Format("{0}{1}", drQWK13IM["F_COVEROUT"], drQWK13IM["F_COVERUNIT"]));
            // 제품명
            DevExpressLib.SetText(xrTableCell7, drQWK13IM["F_INITEMNM"].ToString());
            // 형식
            DevExpressLib.SetText(xrTableCell54, drQWK13IM["F_INTYPE"].ToString());
            // 도번
            DevExpressLib.SetText(xrTableCell56, drQWK13IM["F_DRAWNO"].ToString());
            // 코팅원재료
            DevExpressLib.SetText(xrTableCell6, drQWK13IM["F_COATING"].ToString());
            // 피복원재료
            DevExpressLib.SetText(xrTableCell58, drQWK13IM["F_COVERING"].ToString());
            // 수량
            DevExpressLib.SetText(xrTableCell53, drQWK13IM["F_THERMICCNT"].ToString());
            // 특이사항
            DevExpressLib.SetText(xrTableCell64, drQWK13M["F_REMARK"].ToString());
            // 종합판정
            DevExpressLib.SetText(xrTableCell65, drQWK13IM["F_JUDGE"].ToString().Equals("1") ? "합격" : "불합격");
            // 문서번호
            DevExpressLib.SetText(xrTableCell9, drQWK13M["F_DOCNUM"].ToString());

            // 판정구분
            sJUDGETP = drQWK13M["F_JUDGETP"].ToString();

            // 도면
            sDRAWIMG = drQWK13IM["F_DRAWIMG"].ToString();
        }
        #endregion

        #region 컨텐츠 정보입력
        void SetContent()
        {
            // 개정정보
            int nRevNo = 0;
            foreach (DataRow dr in this.dtQWK13D.Select("F_ASSORTMENT='AAI203'", "F_GROUPCNT DESC"))
            {
                nRevNo++;

                XRTableCell tblCell1 = (XRTableCell)this.xrTable1.FindControl(String.Format("xrTableCell10{0}", nRevNo), true);
                DevExpressLib.SetText(tblCell1, dr["F_GROUPCNT"].ToString());
                XRTableCell tblCell2 = (XRTableCell)this.xrTable1.FindControl(String.Format("xrTableCell11{0}", nRevNo), true);
                DevExpressLib.SetText(tblCell2, dr["F_INSPNM"].ToString());
                XRTableCell tblCell3 = (XRTableCell)this.xrTable1.FindControl(String.Format("xrTableCell12{0}", nRevNo), true);
                DevExpressLib.SetText(tblCell3, dr["F_STANDARD"].ToString());

                if (nRevNo >= 3)
                    break;
            }

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

            int i = 0, j = 0, k = 0, x = 0, nSiryo = 0;
            string curDisplayNo = String.Empty, preDisplayNo = String.Empty, sMeainspcd = String.Empty, sMax = String.Empty, sMin = String.Empty, sUnit = String.Empty, sStandard = String.Empty;
            DevExpress.XtraPrinting.BorderSide borderSide = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);

            ///*
            //AAI201	일반용
            //AAI202	치수용
            //AAI203	개정정보용
            //AAI204	외관용
            //AAI299	공정관리용
            // */

            foreach (DataRow dr in this.dtQWK13D.Select("F_ASSORTMENT<>'AAI203'", "F_DIVISIONIDX ASC"))
            {
                nSiryo = Convert.ToInt32(dr["F_GROUPCNT"]);
                sMeainspcd = dr["F_TERM"].ToString();

                if (dr["F_ASSORTMENT"].ToString().Equals("AAI201"))    // 일반인 경우
                {
                    xrRow = new XRTableRow();
                    xrRow.HeightF = nSiryo > 2 ? 20F : 40F;

                    // 검사항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 93F;
                    xrCell.Text = dr["F_DIVISIONNM"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = nSiryo;
                    xrRow.Cells.Add(xrCell);

                    // 검사방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 117F;
                    xrCell.Text = dr["F_METHOD"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = nSiryo;
                    xrRow.Cells.Add(xrCell);

                    // 단위체판정기준
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 649F;
                    xrCell.Text = dr["F_STANDARD"].ToString();
                    xrCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3);
                    xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = nSiryo;
                    xrRow.Cells.Add(xrCell);

                    // 측정기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 101F;
                    xrCell.Text = dr["F_EQUIPMENT"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = nSiryo;
                    xrRow.Cells.Add(xrCell);

                    j = 0;
                    foreach (DataRow drSub in this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", sMeainspcd)))
                    {
                        if (j.Equals(0))
                        {
                            // 시료
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 39F;
                            xrCell.Text = String.Format("n{0}", j + 1);
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 70F;
                            xrCell.Text = drSub["F_MEASURE"].ToString();
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            if (i.Equals(0))
                                xrRow.Borders = borderSide;

                            xrTable.Rows.Add(xrRow);
                        }
                        else
                        {
                            xrRow = new XRTableRow();
                            xrRow.HeightF = nSiryo > 2 ? 20F : 40F;

                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 93F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 검사방법
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 117F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 단위체판정기준
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 649F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정기
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 101F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 시료
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 39F;
                            xrCell.Text = String.Format("n{0}", j + 1);
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 70F;
                            xrCell.Text = drSub["F_MEASURE"].ToString();
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            xrTable.Rows.Add(xrRow);
                        }

                        j++;

                        if (j >= nSiryo)
                            break;
                    }

                    for (k = j; k < nSiryo; k++)
                    {
                        if (k.Equals(0))
                        {
                            // 시료
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 39F;
                            xrCell.Text = String.Format("n{0}", k + 1);
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 70F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            if (i.Equals(0))
                                xrRow.Borders = borderSide;

                            xrTable.Rows.Add(xrRow);
                        }
                        else
                        {
                            xrRow = new XRTableRow();
                            xrRow.HeightF = nSiryo > 2 ? 20F : 40F;

                            // 검사항목
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 93F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 검사방법
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 117F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 단위체판정기준
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 649F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정기
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 101F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 시료
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 39F;
                            xrCell.Text = String.Format("n{0}", k + 1);
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            // 측정값
                            xrCell = new XRTableCell();
                            xrCell.WidthF = 70F;
                            xrCell.Text = String.Empty;
                            xrCell.CanGrow = false;
                            xrRow.Cells.Add(xrCell);

                            xrTable.Rows.Add(xrRow);
                        }
                    }
                }
                else if (dr["F_ASSORTMENT"].ToString().Equals("AAI202"))    // 치수인 경우
                {
                    System.Collections.Generic.List<string> oMeasure = new System.Collections.Generic.List<string>();
                    foreach (DataRow drSub in this.dtQWK13ID.Select(String.Format("F_MEAINSPCD='{0}'", sMeainspcd)))
                    {
                        oMeasure.Add(drSub["F_MEASURE"].ToString());
                    }

                    // 첫줄 ----------------------------------------------
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 20F;

                    // 검사항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 93F;
                    xrCell.Text = dr["F_DIVISIONNM"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 4;
                    xrRow.Cells.Add(xrCell);

                    // 검사방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 117F;
                    xrCell.Text = dr["F_METHOD"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 4;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "황동1종";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.35";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.45";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.60";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.70";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.90";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ1.10";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ1.50";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 73F;
                    xrCell.Text = "Φ1.75";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 101F;
                    xrCell.Text = dr["F_EQUIPMENT"].ToString();
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 4;
                    xrRow.Cells.Add(xrCell);

                    // 시료
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 39F;
                    xrCell.Text = "n1";
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrRow.Cells.Add(xrCell);

                    // 측정값
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = oMeasure.Count > 0 ? oMeasure[0] : "";
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);

                    // 둘째줄 ----------------------------------------------
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 40F;

                    // 검사항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 93F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 검사방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 117F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "저항값";
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.6468 ±{0}0.0194", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.4070 ±{0}0.0188", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.2270 ±{0}0.0185", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.1635 ±{0}0.0125", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.0977 ±{0}0.0100", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.0662 ±{0}0.0058", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.0360 ±{0}0.0042", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 73F;
                    xrCell.Text = String.Format("0.0262 ±{0}0.0037", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 101F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 시료
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 39F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정값
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);

                    // 세째줄 ----------------------------------------------
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 20F;

                    // 검사항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 93F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 검사방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 117F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "황동2종";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.40";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.60";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "니켈선";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.40";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "Φ0.50";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 73F;
                    xrCell.Text = "구리(Φ0.40)";
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 101F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 시료
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 39F;
                    xrCell.Text = "n2";
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrRow.Cells.Add(xrCell);

                    // 측정값
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = oMeasure.Count > 1 ? oMeasure[1] : "";
                    xrCell.CanGrow = false;
                    xrCell.RowSpan = 2;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);

                    // 네째줄 ----------------------------------------------
                    xrRow = new XRTableRow();
                    xrRow.HeightF = 40F;

                    // 검사항목
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 93F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 검사방법
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 117F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "저항값";
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.5350 ±{0}0.0190", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.2388 ±{0}0.0185", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = "허용공차";
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.716 ±{0}0.050", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Format("0.458 ±{0}0.032", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Empty;
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 72F;
                    xrCell.Text = String.Empty;
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 항목설명
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 73F;
                    xrCell.Text = String.Format("0.1372 ±{0}0.0069", Environment.NewLine);
                    xrCell.Multiline = true;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정기
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 101F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 시료
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 39F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    // 측정값
                    xrCell = new XRTableCell();
                    xrCell.WidthF = 70F;
                    xrCell.Text = String.Empty;
                    xrCell.CanGrow = false;
                    xrRow.Cells.Add(xrCell);

                    xrTable.Rows.Add(xrRow);
                }

                i++;
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

        #region xrPictureBox1_BeforePrint
        /// <summary>
        /// xrPictureBox1_BeforePrint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">System.Drawing.Printing.PrintEventArgs</param>
        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!String.IsNullOrEmpty(sDRAWIMG))
            {
                XRPictureBox pictureBox = sender as XRPictureBox;
                //pictureBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
                pictureBox.ImageUrl = sDRAWIMG;
            }
        }
        #endregion

        #endregion
    }
}
