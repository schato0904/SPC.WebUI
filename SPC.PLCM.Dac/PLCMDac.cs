using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.PLCM.Dac
{
    public class PLCMDac : IDisposable
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

        #region 설비정보 리스트
        public DataSet PLC01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비정보-> 마스터 리스트
        public DataSet PLC02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC02_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 리스트 (ID GROUP)
        public DataSet PLC02_LST_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC02_LST_2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비정보-> 검사항목 리스트
        public DataSet PLC03_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC03_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비별 레시피 리스트
        public DataSet PLC04_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC04_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비별 레시피 리스트 - 검사기준
        public DataSet PLC07_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC07_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비별 검사항목 마스터 리스트
        public DataSet PLC05_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC05_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비정보-> STEP 리스트
        public DataSet PLC06_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLC06_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region PLCM1001 : 설비정보관리

        public DataSet USP_PLCM1001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_PLCM1001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1001_LST2", out errMsg);
            }

            return ds;
        }

        public bool USP_PLCM1001_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_PLCM1001_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_PLCM1001_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_PLCM1001_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_PLCM1001_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_PLCM1001_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region PLCM1002 > 설비조건항목 관리

        #region 검사항목 조회
        public DataSet PHIL33_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PHIL33_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM1003 > 설비조건 기준 관리

        #region 검사항목 조회
        public DataSet USP_PLCM1003_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1003_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준복사(Exists Check)
        public bool PHIL34_PROC_EXISTS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteScaler("USP_PHIL34_EXISTS").ToString().Equals("1");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region PLCM1004 > 설비조건 기준이력관리

        #region 검사기준이력 조회
        public DataSet USP_PLCM1004_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1004_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM1005 > 설비STEP 관리

        #region 설비STEP 조회
        public DataSet USP_PLCM1005_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1005_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비STEP별 검사항목 조회
        public DataSet USP_PLCM1005POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM1005POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM2001 : 설비Data조회

        public DataSet USP_PLCM2001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2001_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region PLCM2002 : 설비별작업조회

        #region 조회
        public DataSet USP_PLCM2002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2002_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM2003 : 설비별 항목별 조건그래프 조회

        public DataSet USP_PLCM2003_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2003_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region PLCM2004 : 검사항목별 조건그래프

        public DataSet USP_PLCM2004_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2004_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region PLCM2005 : XRF 조회

        public DataSet USP_PLCM2005_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2005_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region PLCM2006 : PR코팅 조회

        #region 우측 데이터 조회
        public DataSet USP_PLCM2006_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2006_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM2007 : 액분석 조회

        #region 우측 데이터 조회
        public DataSet USP_PLCM2007_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2007_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM2008 : 설비별 생산이력
        
        #region 조회
        public DataSet PLCM2008_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM2008_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM3001 : 일별작업현황

        #region 조회
        public DataSet USP_PLCM3001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM3002 : 월별작업현황

        #region 조회
        public DataSet USP_PLCM3002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3002_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팝업 조회
        public DataSet USP_PLCM3002POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3002POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팝업 설비명 조회
        public DataSet USP_PLCM3002POP_MACHNM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3002POP_MACHNM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM3003 : 설비별 월 작업현황

        #region 조회
        public DataSet USP_PLCM3003_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3003_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region PLCM3004 : 마스터별 작업현황

        #region 컬럼 조회
        public DataSet PLCM3004_COL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3004_COL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터ID 조회
        public DataSet PLCM3004_MASTERID_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3004_MASTERID_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회
        public DataSet USP_PLCM3004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PLCM3004_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 배치 업데이트
        #region 배치 업데이트
        public bool PROC_BATCH_UPDATE(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion
        #endregion
    }
}
