using System;
using System.Collections.Generic;
using System.Data;

using SPC.LTRK.Dac;

namespace SPC.LTRK.Biz
{
    public class LTRKBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 기본정보

        #region 거래처관리

        #region 조회
        public DataSet QCM99_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QCM99_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        public bool PROC_QCM99_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.PROC_QCM99_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion

        #region 작업지시관리

        #region 작업지시그룹등록

        #region 엑셀다운로드 > 공정목록(설비등록된 공정만)
        public DataSet QCD74_LTRK0201_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QCD74_LTRK0201_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회
        public DataSet QPM21_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM21_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        public bool QPM21_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM21_INS(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 작업지시그룹상태변경
        public bool QPM21_STATUS_CHG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM21_STATUS_CHG(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 작업지시등록(현황)

        #region 조회(일자별)
        public DataSet QPM22_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM22_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(기간별)
        public DataSet QPM22_LST_BY_BETWEENDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM22_LST_BY_BETWEENDATE(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        public bool PROC_QPM22_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.PROC_QPM22_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 상태변경
        public bool QPM22_STATUS_CHG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM22_STATUS_CHG(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 전체상태변경
        public bool QPM22_STATUS_CHG_ALL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM22_STATUS_CHG_ALL(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 전체마감
        public bool QPM22_CLOSE_ALL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM22_CLOSE_ALL(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        public bool QPM22_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.QPM22_DEL(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 작업지시현황 > 투입현황
        public DataSet QPM23_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM23_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업지시현황 > 생산실적
        public DataSet QWK110_LST_FOR_JAKJI(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QWK110_LST_FOR_JAKJI(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Lot Tracking

        #region 조회
        public DataSet LTRK0204_MASTER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.LTRK0204_MASTER(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #endregion

        #region 자재 및 완제품 수불관리

        #region 기간별 입고현황

        #region 조회
        public DataSet QPM11_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM11_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기간별 출고현황

        #region 조회
        public DataSet QPM12_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM12_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 현재고현황

        #region 조회
        public DataSet QPM13_LST_FOR_DATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM13_LST_FOR_DATE(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 수불현황

        #region 조회
        public DataSet QPM13_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM13_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        public bool PROC_QPM13_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.PROC_QPM13_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion

        #region 마감관리

        #region 월마감관리

        #region 조회
        public DataSet QPM01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마감 및 취소
        public bool PROC_CLOSE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (LTRKDac dac = new LTRKDac())
            {
                bExecute = dac.PROC_CLOSE_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마감이력조회
        public DataSet QPM02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM02_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 해당월 마감여부 체크
        public DataSet QPM01_CLOSE_CHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (LTRKDac dac = new LTRKDac())
            {
                ds = dac.QPM01_CLOSE_CHK(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #endregion
    }
}
