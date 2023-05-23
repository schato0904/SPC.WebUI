using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.WKSD.Dac
{
    public class WKSDDac : IDisposable
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

        #region 작업표준서 > 작업표준서 조회

        #region 작업표준서 정보조회
        public DataSet DWK01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_DWK01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 정보 입력
        public bool PROC_DWK01_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_DWK01_INS");
            }

            return bExecute;
        }
        #endregion

        #region 작업표준서 정보 수정
        public bool PROC_DWK01_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_DWK01_UPD");
            }

            return bExecute;
        }
        #endregion

        #region 작업표준서 정보 삭제
        public bool PROC_DWK01_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_DWK01_DEL");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 작업표준서 > 작업표준서 개정이력 조회

        #region 작업표준서개정이력 조회
        public DataSet DWK01_HIST_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_DWK01_HIST_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 개정이력 전체 갯수
        public Int32 DWK01_HIST_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_DWK01_HIST_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 작업표준서 > 작업표준서 미등록 현황 조회

        #region 작업표준서 미등록 현황 조회
        public DataSet DWK01_UNREGISTERED_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_DWK01_UNREGISTERED_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 미등록 현황 전체 갯수
        public Int32 DWK01_UNREGISTERED_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_DWK01_UNREGISTERED_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion
    }
}
