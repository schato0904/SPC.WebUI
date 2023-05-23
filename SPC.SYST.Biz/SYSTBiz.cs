using System;
using System.Collections.Generic;
using System.Data;
using SPC.SYST.Dac;

namespace SPC.SYST.Biz
{
    public class SYSTBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 업체별 공통코드 관리

        #region 업체별 공통코드 목록
        /// <summary>
        /// 기능명 : SYCOD01_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYCOD01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.SYCOD01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 업체별 공통코드 목록
        /// <summary>
        /// 기능명 : SYCOD01_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYCOD01_LST_FOR_ITEM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.SYCOD01_LST_FOR_ITEM(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 업체별 공통코드 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 업체별 공통코드, 수정, 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYCOD01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SYSTDac dac = new SYSTDac())
            {
                bExecute = dac.PROC_SYCOD01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 업체별 메뉴권한 관리

        #region 업체별 메뉴권한 목록
        /// <summary>
        /// 기능명 : SYPGM02_AUTHORITY_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYPGM02_AUTHORITY_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.SYPGM02_AUTHORITY_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 업체별 메뉴권한 저장,수정
        /// <summary>
        /// 기능명 : PROC_SYAUT01_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYAUT01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SYSTDac dac = new SYSTDac())
            {
                bExecute = dac.PROC_SYAUT01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 업체별 사용자 관리

        #region 업체별 사용자 목록
        /// <summary>
        /// 기능명 : SYPGM02_AUTHORITY_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYUSR01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.SYUSR01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자 저장,수정
        /// <summary>
        /// 기능명 : PROC_SYAUT01_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYUSR01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SYSTDac dac = new SYSTDac())
            {
                bExecute = dac.PROC_SYUSR01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 마스터 업체 별 협력업체 관리

        #region 마스터 업체별 협력업체 목록
        /// <summary>
        /// 기능명 : QCD00_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-15
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet QCD00_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.QCD00_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SMS사용자관리

        #region 사용자목록 조회
        public DataSet SYUSR01_QCD013_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.SYUSR01_QCD013_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회
        public DataSet QCD013_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.QCD013_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회(전체 버튼 클릭시)
        public DataSet QCD013_LST_EDIT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.QCD013_LST_EDIT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 정보 저장, 수정, 삭제
        public bool PROC_QCD013_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SYSTDac dac = new SYSTDac())
            {
                bExecute = dac.PROC_QCD013_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 모니터링 공지사항

        #region 조회
        public DataSet QCDMONITORINGNOTICE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SYSTDac dac = new SYSTDac())
            {
                ds = dac.QCDMONITORINGNOTICE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        public bool PROC_QCDMONITORINGNOTICE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SYSTDac dac = new SYSTDac())
            {
                bExecute = dac.PROC_QCDMONITORINGNOTICE_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion
    }
}
