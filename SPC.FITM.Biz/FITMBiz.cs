using System;
using System.Collections.Generic;
using System.Data;
using SPC.FITM.Dac;

namespace SPC.FITM.Biz
{
    public class FITMBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
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

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QCD40_TBL_LST(oParams, out errMsg);
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

            using (FITMDac dac = new FITMDac())
            {
                bExecute = dac.QCD40_TBL_PROC(oSps, oParams, out resultMsg);
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

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.FIRSTITEM01_RESERVE_LST(oParams, out errMsg);
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

            using (FITMDac dac = new FITMDac())
            {
                bExecute = dac.FIRSTITEM01_RESERVE_PROC(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 초중종모니터링

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

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.FIRSTITEM01_MORNITORING_LST(oParams, out errMsg);
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

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK03_TIME_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data조회 > Data조회

        #region Data 조회
        // 협력사
        public DataSet QWK04A_FITM0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0202_LST(oParams, out errMsg);
            }

            return ds;
        }
        // 오토, 네오오토
        public DataSet QWK04A_FITM0202_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0202_LST_MST(oParams, out errMsg);
            }

            return ds;
        }

        // 호세코
        public DataSet QWK04A_FITM0202_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0202_FOSECO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        // 협력사
        public Int32 QWK04A_FITM0202_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0202_CNT(oParams);
            }

            return resultCnt;
        }
        // 오토, 네오오토
        public Int32 QWK04A_FITM0202_CNT_MST(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0202_CNT_MST(oParams);
            }

            return resultCnt;
        }

        // 호세코
        public Int32 QWK04A_FITM0202_FOSECO_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0202_FOSECO_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data조회 > Data수정

        #region 초품사유콤보
        public DataSet FOURCD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.FOURCD_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data 조회
        public DataSet QWK04A_FITM0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0203_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK04A_FITM0203_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0203_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region Data 조회 호세코
        public DataSet QWK04A_FITM0203_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0203_FOSECO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수 호세코
        public Int32 QWK04A_FITM0203_FOSECO_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0203_FOSECO_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region Data수정
        public bool QWK04A_FITM0203_UPD(Dictionary<string, string> oParams)
        {
            bool resultCnt = false;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0203_UPD(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region data수정
        /// <summary>
        /// 기능명 : PROC_SYAUT01_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool QWK04A_FITM0203_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FITMDac dac = new FITMDac())
            {
                bExecute = dac.QWK04A_FITM0203_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region Data삭제
        public bool QWK04A_FITM0203_DEL(Dictionary<string, string> oParams)
        {
            bool resultCnt = false;

            using (FITMDac dac = new FITMDac())
            {
                resultCnt = dac.QWK04A_FITM0203_DEL(oParams);
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

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.QWK04A_FITM0204_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region Data조회 > D초중종 Check Sheet

        #region Data 조회
        public DataSet FITM0205_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.FITM0205_LST(oParams, out errMsg);
            }

            return ds;
        }        
        #endregion

        #region Data REPORT DATA 조회
        public DataSet FITM0205_REPORT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FITMDac dac = new FITMDac())
            {
                ds = dac.FITM0205_REPORT_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
