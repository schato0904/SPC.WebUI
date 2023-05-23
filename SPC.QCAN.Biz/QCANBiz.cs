using System;
using System.Collections.Generic;
using System.Data;

using SPC.QCAN.Dac;

namespace SPC.QCAN.Biz
{
    public class QCANBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 품질집계 > 품목별공정능력집계

        #region 품목별공정능력집계
        public DataSet QWK03A_QCAN0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK03A_QCAN0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별공정능력집계 전체 갯수
        public Int32 QWK03A_QCAN0101_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (QCANDac dac = new QCANDac())
            {
                resultCnt = dac.QWK03A_QCAN0101_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 품질집계 > 공정능력그룹별집계

        #region 공정능력그룹별집계
        public DataSet QWK03A_QCAN0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK03A_QCAN0102_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력그룹별집계 전체 갯수
        public Int32 QWK03A_QCAN0102_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (QCANDac dac = new QCANDac())
            {
                resultCnt = dac.QWK03A_QCAN0102_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 공정능력월별집계
        public DataSet QWK03A_QCAN0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK03A_QCAN0103_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력월별집계 날짜
        public DataSet QWK03A_QCAN0103_MONTH_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK03A_QCAN0103_MONTH_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력 월별집계 전체 갯수
        public Int32 QWK03A_QCAN0103_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (QCANDac dac = new QCANDac())
            {
                resultCnt = dac.QWK03A_QCAN0103_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 집계 > 항목별품질현황조회
        #region 항목별품질현황
        public DataSet QWK04A_QCAN0106_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK04A_QCAN0106_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 집계 > 공정그룹별집계분석

        #region 공정리스트
        public DataSet QCD74_QCAN0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QCD74_QCAN0104_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력조회
        public DataSet QWK04A_QCAN0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (QCANDac dac = new QCANDac())
            {
                ds = dac.QWK04A_QCAN0104_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
        
    }
}
