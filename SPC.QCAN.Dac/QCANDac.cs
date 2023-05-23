using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.QCAN.Dac
{
    public class QCANDac : IDisposable
    {
        private DBHelper spcDB;

        #region Dispose
        public void Dispose()
        {
            if (spcDB != null)
            {
                spcDB.Dispose();
                spcDB = null;
            }
        }
        #endregion

        #region Data 조회

        #region Data 조회
        public DataSet QWK03A_QCAN0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_QCAN0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK03A_QCAN0101_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_QCAN0101_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 공정능력그룹별집계

        #region 공정능력그룹별집계
        public DataSet QWK03A_QCAN0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_QCAN0102_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력그룹별집계 전체 갯수
        public Int32 QWK03A_QCAN0102_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_QCAN0102_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 공정능력월별집계

        #region 공정능력월별집계
        public DataSet QWK03A_QCAN0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_QCAN0103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력월별집계 날짜
        public DataSet QWK03A_QCAN0103_MONTH_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_QCAN0103_MONTH_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력월별집계 전체 갯수
        public Int32 QWK03A_QCAN0103_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_QCAN0103_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 항목별품질현황조회
        #region Data 조회
        public DataSet QWK04A_QCAN0106_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_QCAN0106_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 집계 > 공정그룹별집계분석

        #region 공정리스트조회
        public DataSet QCD74_QCAN0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD74_QCAN0104_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력조회
        public DataSet QWK04A_QCAN0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_QCAN0104_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
        
        
    }
}
