using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.FITM.Dac
{
    public class FITMDac : IDisposable
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

        #region 초품변경사유

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QCD40_TBL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD40_TBL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 저장, 수정, 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool QCD40_TBL_PROC(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 초중종예약관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet FIRSTITEM01_RESERVE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FIRSTITEM01_RESERVE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        /// <summary>
        /// 기능명 : 저장, 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool FIRSTITEM01_RESERVE_PROC(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 초중종 모니터링

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet FIRSTITEM01_MORNITORING_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FIRSTITEM01_MORNITORING_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(시간대별 상세조회)
        /// <summary>
        /// 기능명 : 조회(시간대별 상세조회)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK03_TIME_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03_TIME_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data 조회

        #region Data 조회
        // 협력사
        public DataSet QWK04A_FITM0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_FITM0202_LST", out errMsg);
            }

            return ds;
        }
        // 오토, 네오오토
        public DataSet QWK04A_FITM0202_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_FITM0202_LST_MST", out errMsg);
            }

            return ds;
        }

        // 호세코
        public DataSet QWK04A_FITM0202_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_FITM0202_FOSECO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        // 협력사
        public Int32 QWK04A_FITM0202_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK04A_FITM0202_CNT"));
            }

            return resultCnt;
        }
        // 오토, 네오오토
        public Int32 QWK04A_FITM0202_CNT_MST(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK04A_FITM0202_CNT_MST"));
            }

            return resultCnt;
        }


        // 호세코
        public Int32 QWK04A_FITM0202_FOSECO_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK04A_FITM0202_FOSECO_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data 조회 > Data수정

        #region 초품사유
        public DataSet FOURCD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FOURCD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data 조회
        public DataSet QWK04A_FITM0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_FITM0203_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK04A_FITM0203_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK04A_FITM0203_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region Data수정
        public bool QWK04A_FITM0203_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FITM0203_UPD");
            }

            return bExecute;
        }
        #endregion

        #region Data수정
        /// <summary>
        /// 기능명 : PROC_SYUSR01_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool QWK04A_FITM0203_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region Data삭제
        public bool QWK04A_FITM0203_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FITM0203_DEL");
            }

            return bExecute;
        }
        #endregion


        #region Data 조회 호세코
        public DataSet QWK04A_FITM0203_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK04A_FITM0203_FOSECO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수 호세코
        public Int32 QWK04A_FITM0203_FOSECO_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK04A_FITM0203_FOSECO_CNT"));
            }

            return resultCnt;
        }
        #endregion
        #endregion

        #region Data조회 > 검사항목별Data조회
        #region Data 조회
        public DataSet QWK04A_FITM0204_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FITM0204_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region Data관리 > 초중종 Check Sheet

        #region Data관리 > 초중종 Check Sheet
        public DataSet FITM0205_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FITM0205_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data관리 > 초중종 Check Sheet REPORT DATA조회
        public DataSet FITM0205_REPORT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FITM0205_REPORT_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
