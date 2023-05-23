using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.CATM.Dac
{
    public class CATMDac : IDisposable
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

        #region CATM1001 : 공통코드관리
        public DataSet USP_CATM1001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1001_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1001_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1001_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1001_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1001_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1001_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1001_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1001_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1001_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1101 : 설비정보관리
        public DataSet USP_CATM1101_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1101_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1101_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1101_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1101_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1101_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1101_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1101_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1101_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1101_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1101_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1101_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1102 : 설비수리이력관리
        public DataSet USP_CATM1102_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1102_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1102_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1102_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1102_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1102_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1102_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1102_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1102_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1102_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1102_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1102_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1201 : 작업지시서등록
        public DataSet USP_CATM1201_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST4", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1201_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = string.Empty;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM1201_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_UPD1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1201_UPD2(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_UPD2", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1201 : 작업지시서등록(삼성정공)
        public DataSet USP_CATM1201_LST1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST1_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST2_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST2_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST3_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST3_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1201_LST4_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1201_LST4_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1201_INS1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = string.Empty;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_INS1_SAMSUNG", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM1201_UPD1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_UPD1_SAMSUNG", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1201_UPD2_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1201_UPD2_SAMSUNG", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1203 : 작업지시서관리
        public DataSet USP_CATM1203_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1203_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1203_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1203_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1203_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1203_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1203_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1203_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1211 : 작업지시서등록(NEW)
        public DataSet USP_CATM1211_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1211_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1211_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1211_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_LST4", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1211_LST5(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_LST5", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1211_2_RPT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1211_2_RPT", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1211_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = string.Empty;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1211_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM1211_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1211_UPD1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1211_UPD2(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1211_UPD2", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1211_COMPLETE(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1211_COMPLETE", out errMsg);
            }

            return bExecute;
        }

        #endregion
              
        #region CATM1202 : 작업지시서조회
        public DataSet USP_CATM1202_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1202_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM1202 : 작업지시서조회(삼성정공)
        public DataSet USP_CATM1202_LST1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1202_LST1_SAMSUNG", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM1301 : 생산실적등록
        public DataSet USP_CATM1301_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1301_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1301_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1301_LST2", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 작업지시에 생산수량 업데이트
        /// 트랜잭션에 포함 필요
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool USP_CATM1301_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1301_INS1", out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 불량유형별 수량 저장
        /// 트랜잭션에 포함 필요
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool USP_CATM1301_INS2(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1301_INS2", out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 유실유형별 시간 저장
        /// 트랜잭션에 포함 필요
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool USP_CATM1301_INS3(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1301_INS3", out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 불량수량 집계하여 작업지시에 업데이트
        /// 트랜잭션에 포함 필요
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool USP_CATM1301_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1301_UPD1", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region CATM1302 : 생산실적조회

        public DataSet USP_CATM1302_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1302_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1302_RPT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1302_RPT", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1302_POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1302_POP", out errMsg);
            }

            return ds;
        }
        #endregion        

        #region CATM1303 : 유실내역조회
        public DataSet USP_CATM1303_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1303_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM1304 : 용해로투입이력관리
        public DataSet USP_CATM1304_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1304_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1304_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1304_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1304_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1304_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM1304_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1304_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM1304_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1304_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM1304_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM1304_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM1305 : 작업일보조회
        public DataSet USP_CATM1305_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1305_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1305_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1305_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1305_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1305_LST3", out errMsg);
            }

            return ds;
        }
        #endregion

        #region CATM1401 : 일별생산현황
        public DataSet USP_CATM1401_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1401_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM1402 : 일생산현황
        public DataSet USP_CATM1402_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1402_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1402_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1402_LST3", out errMsg);
            }

            return ds;
        }
        #endregion

        #region CATM1403 : 월생산현황
        public DataSet USP_CATM1403_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1403_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM1403_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM1403_LST3", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM2001 : 사입처등록
        public DataSet USP_CATM2001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2001_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2001_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2001_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2001_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2001_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2001_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2001_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2001_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2001_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2002 : 원재료품목등록
        public DataSet USP_CATM2002_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2002_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2002_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2002_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2002_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2002_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2002_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2002_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2002_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2002_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2002_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2002_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2101 : 입고등록
        public DataSet USP_CATM2101_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2101_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2101_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2101_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2101_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2101_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2101_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2101_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2101_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2101_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2101_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2101_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2111 : 입고등록(NEW)
        public DataSet USP_CATM2111_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2111_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2111_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2111_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2111_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2111_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2111_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2111_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2111_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2111_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2111_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2111_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2121 : 입고등록(NEW NEW)
        public DataSet USP_CATM2121_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2121_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2121_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2121_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2121_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2121_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2121_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2121_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2121_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2121_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2121_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2121_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2102 : 출고등록
        public DataSet USP_CATM2102_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2102_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2102_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2102_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2102_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2102_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2102_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2102_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2102_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2102_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2102_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2102_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2112 : 출고등록(NEW)
        public DataSet USP_CATM2112_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2112_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2112_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2112_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2112_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2112_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2112_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2112_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2112_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2112_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2112_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2112_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2122 : 원재료(중자)출고등록(NEW NEW)
        public DataSet USP_CATM2122_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2122_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2122_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2122_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2122_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2122_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2122_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2122_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2122_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2122_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2122_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2122_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2123 : 원재료(원재료)출고등록(NEW NEW)
        public DataSet USP_CATM2123_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2123_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2123_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2123_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2123_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2123_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM2123_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2123_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM2123_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2123_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2123_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2123_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2103 : 실사조정
        public DataSet USP_CATM2103_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2103_LST1", out errMsg);
            }

            return ds;
        }

        // 트랜잭션 포함
        public bool USP_CATM2103_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2103_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2103_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2103_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM2103_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM2103_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM2104 : 입출고 이력조회
        public DataSet USP_CATM2104_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2104_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM2201 : 일별재고현황
        public DataSet USP_CATM2201_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2201_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM2202 : 일수불현황
        public DataSet USP_CATM2202_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2202_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2202_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2202_LST3", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM2203 : 월수불현황
        public DataSet USP_CATM2203_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2203_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM2203_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM2203_LST3", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM3001 : 출하처등록
        public DataSet USP_CATM3001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3001_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3001_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3001_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM3001_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3001_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3001_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3001_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3001_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3001_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM3101 : 출하지시서등록
        public DataSet USP_CATM3101_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3101_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3101_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3101_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3101_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3101_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3101_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3101_LST4", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM3101_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3101_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM3101_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3101_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3101_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3101_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM3111 : 출하지시서등록(NEW)
        public DataSet USP_CATM3111_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3111_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3111_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3111_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3111_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3111_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3111_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3111_LST4", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3111_LST5(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3111_LST5", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM3111_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3111_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM3111_INS2(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3111_INS2", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3111_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3111_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3111_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper(false))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3111_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM3121 : 출하지시서등록(NEW NEW)
        public DataSet USP_CATM3121_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3121_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3121_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3121_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3121_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3121_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3121_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3121_LST4", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM3121_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            pkey = string.Empty;
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3121_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM3121_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3121_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM3121_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3121_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM3131 : 현재고 현황
        public DataSet USP_CATM3131_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3131_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #region CATM3301 : 반품관리

        public DataSet USP_CATM3301_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3301_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3301_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_LST3", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3301_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_LST4", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3301_COMBO(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_COMBO", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3301_CUST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3301_CUST", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM3301_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM3301_INS_UPD", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM3102 : 출하이력조회
        public DataSet USP_CATM3102_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3102_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM3201 : 업체별 출하현황
        public DataSet USP_CATM3201_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3201_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM3202 : 제품별 출하현황
        public DataSet USP_CATM3202_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3202_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM3203 : 일수불현황
        public DataSet USP_CATM3203_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3203_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3203_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3203_LST3", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM3204 : 월수불현황
        public DataSet USP_CATM3204_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3204_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM3204_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM3204_LST3", out errMsg);
            }

            return ds;
        }

        #endregion


        #region CATM4001 : 금형등록
        public DataSet USP_CATM4001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4001_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM4001_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4001_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4001_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM4001 : 금형등록(삼성정공)
        public DataSet USP_CATM4001_LST1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST1_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4001_LST2_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST2_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4001_LST3_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4001_LST3_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM4001_INS1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_INS1_SAMSUNG", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4001_DEL1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_DEL1_SAMSUNG", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4001_UPD1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4001_UPD1_SAMSUNG", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM4002 : 금형수리유형관리
        public DataSet USP_CATM4002_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4002_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4002_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4002_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4002_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4002_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM4002_INS1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4002_INS1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4002_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4002_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4002_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4002_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM4101 : 금형조회

        //조회
        public DataSet USP_CATM4101_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4101_LST1", out errMsg);
            }

            return ds;
        }

        //수정
        public bool USP_CATM4101_UPD1(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                spcDB.bUsedTrans = false;
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4101_UPD");
            }

            return bExecute;
        }

        public bool PROC_CATM0401_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
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

        #region CATM4102 : 금형수리이력관리
        public DataSet USP_CATM4102_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4102_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4102_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4102_LST2", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM4102_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4102_LST3", out errMsg);
            }

            return ds;
        }

        public bool USP_CATM4102_INS1(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = string.Empty;

            using (spcDB = new DBHelper())
            {
                oParams["PKEY"] = "OUTPUT";
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4102_INS1", out errMsg);
                if (bExecute && spcDB.oOutParamDic.ContainsKey("PKEY"))
                {
                    pkey = (spcDB.oOutParamDic["PKEY"] ?? "").ToString();
                }
            }

            return bExecute;
        }

        public bool USP_CATM4102_DEL1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4102_DEL1", out errMsg);
            }

            return bExecute;
        }

        public bool USP_CATM4102_UPD1(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_CATM4102_UPD1", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region CATM4103 : 금형수리이력조회
        public DataSet USP_CATM4103_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4103_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM4104 : 금형사용현황
        public DataSet USP_CATM4104_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM4104_LST1", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM5001 : 용해로모니터링
        public DataSet USP_CATM5001_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5001_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM5001_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5001_LST3", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM5001 : 용해로모니터링(삼성정공)
        public DataSet USP_CATM5001_LST1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5001_LST1_SAMSUNG", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM5001_LST3_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5001_LST3_SAMSUNG", out errMsg);
            }

            return ds;
        }

        #endregion

        #region CATM5002 : 주조기모니터링
        public DataSet USP_CATM5002_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5002_LST1", out errMsg);
            }

            return ds;
        }

        public DataSet USP_CATM5002_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5002_LST3", out errMsg);
            }

            return ds;
        }
        #endregion

        #region CATM5002 : 주조기모니터링(삼성정공)
        public DataSet USP_CATM5002_LST1_SAMSUNG(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CATM5002_LST1_SAMSUNG", out errMsg);
            }

            return ds;
        }
      
        #endregion
    }
}
