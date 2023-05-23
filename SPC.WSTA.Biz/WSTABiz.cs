using System;
using System.Collections.Generic;
using System.Data;

using SPC.WSTA.Dac;

namespace SPC.WSTA.Biz
{
    public class WSTABiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 품목별 Worst분석

        #region 품목별Worst분석
        public DataSet QWK03A_WSTA0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 PIE CHART
        public DataSet QWK03A_WSTA0101_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0101_CHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0101_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0101_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세카운트
        public DataSet QWK03A_WSTA0101_DATA_CNT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0101_DATA_CNT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 항목별 Worst분석

        #region 항목별Worst분석
        public DataSet QWK03A_WSTA0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0102_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst Chart
        public DataSet QWK03A_WSTA0102_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0102_CHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0102_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0102_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세카운트
        public DataSet QWK03A_WSTA0102_DATA_CNT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0102_DATA_CNT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정능력 Worst분석

        #region 공정능력Worst분석
        public DataSet QWK03A_WSTA0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0103_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0103_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0103_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력Worst분석 DATA 상세카운트
        public DataSet QWK03A_WSTA0103_DATA_CNT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0103_DATA_CNT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 주별분석
        public DataSet QWK03A_WSTA0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0104_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 월별분석
        public DataSet QWK03A_WSTA0105_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0105_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 월별분석_2
        public DataSet QWK03A_WSTA0107_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0107_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 주별분석_2
        public DataSet QWK03A_WSTA0108_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0108_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 관리한계이력그래프
        public DataSet WSTA0109_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.WSTA0109_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인별 Worst분석

        #region 불량원인별 Worst분석
        public DataSet QWK03A_WSTA0106_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0106_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인별 Worst Chart
        public DataSet QWK03A_WSTA0106_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0106_CHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion


        #region 불량원인별 Worst분석  PIE CHART
        public DataSet QWK03A_WSTA0106_PIECHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WSTADac dac = new WSTADac())
            {
                ds = dac.QWK03A_WSTA0106_PIECHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
