using System;
using System.Collections.Generic;
using System.Data;
using SPC.MEAS.Dac;

namespace SPC.MEAS.Biz
{
    /// <summary>
    /// 설비일상점검
    /// </summary>
    public class MEASBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region UPDATE INSERT DELETE
        /// <summary>
        /// 기능명 : UPDATE INSERT DELETE
        /// 작성자 : KIM S
        /// 작성일 : 2015-05-14
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// /// <param name="oParams">object[]</param>
        /// /// <param name="resultMsg">out string[]</param>
        /// <returns>bool</returns>
        public bool PROC_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.PROC_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region UPDATE INSERT DELETE NO PKEY
        /// <summary>
        /// 기능명 : UPDATE INSERT DELETE
        /// 작성자 : KIM S
        /// 작성일 : 2015-05-14
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// /// <param name="oParams">object[]</param>
        /// /// <param name="resultMsg">out string[]</param>
        /// <returns>bool</returns>
        public bool PROC_BATCH_NO(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.PROC_BATCH_NO(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 업체별 공통코드 목록
        /// <summary>
        /// 기능명 : SYCOD01_LST
        /// 작성자 : LEE HWANUI
        /// 작성일 : 2015-06-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYCOD01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.SYCOD01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 계측기 > 계측기관리 > 계측기정보관리

        /// <summary>
        /// 기능명 : 생산팀 정보 목록
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_TEAM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_TEAM_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 교정주기List
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_TERM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_TERM_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 반 정보 목록
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_BAN_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_BAN_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 공정 정보 목록
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_PROC_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_PROC_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : MEAS1001_LST
        /// 작성자 : LEE HWANUI
        /// 작성일 : 2015-06-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : MEAS1001_MS01D1_LST
        /// 작성자 : LEE HWANUI
        /// 작성일 : 2015-06-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_MS01D1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_MS01D1_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 계측기 정보관리 상세정보
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_INF(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1001_INF(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 계측기 정보관리 저장
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS1001_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS1001_INS(oParams, out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 계측기 정보관리 수정
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS1001_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS1001_UPD(oParams, out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 계측기 정보관리 삭제
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-07-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS1001_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS1001_DEL(oParams, out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region 계측기 > 계측기관리 > 계측기보유현황

        /// <summary>
        /// 계측기보유현황 목록 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS1002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS1002_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 계측기보유현황 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS1002_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (MEASDac dac = new MEASDac())
            {
                resultCnt = dac.MEAS1002_CNT(oParams);
            }

            return resultCnt;
        }

        #endregion

        #region 계측기 > 검교정계획관리

        #region 계측기 > 검교정계획관리 > 년검교정계획

        /// <summary>
        /// 년검교정계획 목록 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2001_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 년검교정계획 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS2001_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (MEASDac dac = new MEASDac())
            {
                resultCnt = dac.MEAS2001_CNT(oParams);
            }

            return resultCnt;
        }

        #endregion

        #region 계측기 > 검교정계획관리 > 월검교정계획

        /// <summary>
        /// 월검교정계획 목록 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2002_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 월검교정계획 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS2002_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (MEASDac dac = new MEASDac())
            {
                resultCnt = dac.MEAS2002_CNT(oParams);
            }

            return resultCnt;
        }

        #endregion

        #region 계측기 > 검교정계획관리 > 팀별검교정계획

        /// <summary>
        /// 팀별검교정계획 목록 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2003_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2003_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 팀별검교정계획 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS2003_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (MEASDac dac = new MEASDac())
            {
                resultCnt = dac.MEAS2003_CNT(oParams);
            }

            return resultCnt;
        }

        #endregion

        #region 계측기 > 검교정계획관리 > 검교정신청

        /// <summary>
        /// 검교정신청 의뢰 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2004_MST_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2004_MST_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 검교정신청 의뢰 상세 조회
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2004_DTL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2004_DTL_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 검교정신청 저장
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS2004_INS(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS2004_INS(oParams, out errMsg, out pkey);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 검교정신청 수정
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS2004_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS2004_UPD(oParams, out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 검교정신청 삭제
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool MEAS2004_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS2004_DEL(oParams, out errMsg);
            }

            return bExecute;
        }

        #endregion

        #region 계측기 > 검교정계획관리 > 검교정접수

        /// <summary>
        /// 검교정접수 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2005_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2005_MST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 검교정접수 상세 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS2005_DTL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS2005_DTL(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 검교정접수 저장
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS2005_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS2005_INS(oParams, out errMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 검교정접수 삭제
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>bool</returns>
        public bool MEAS2005_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS2005_DEL(oParams, out errMsg);
            }

            return bExecute;
        }

        #endregion

        #endregion

        #region 계측기 > 검교정실적관리

        #region 계측기 > 검교정실적관리 > 의뢰번호

        /// <summary>
        /// 검교정접수 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS4001_POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS4001_POP_LST(oParams, out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 계측기보유현황 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS4001_POP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (MEASDac dac = new MEASDac())
            {
                resultCnt = dac.MEAS4001_POP_CNT(oParams);
            }

            return resultCnt;
        }

        #endregion

        #region 계측기 > 검교정실적관리 > 검교정실적등록

        /// <summary>
        /// 검교정접수 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS4001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS4001_LST(oParams, out errMsg);
            }

            return ds;
        }

        // <summary>
        /// 검교정접수 저장, 수정, 삭제
        /// </summary>
        /// <param name="oSps"></param>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool MEAS4001_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.MEAS4001_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }

        #endregion

        #region 계측기 > 검교정실적관리 > 검교정현황
        /// <summary>
        /// 검교정접수 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MS01D5_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D5_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 정기검사 저장,수정
        /// <summary>
        /// 기능명 : USP_MEAS1001_BATCH
        /// 작성자 : LEE HWANUI
        /// 작성일 : 2015-06-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool USP_MEAS1001_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.USP_MEAS1001_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 계측기 - 기본정보관리

        #region 생산팀코드관리 조회
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : 박병수
        /// 작성일 : 2016-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9004_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 프로그램 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : 박병수
        /// 작성일 : 2016-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (MEASDac dac = new MEASDac())
            {
                bExecute = dac.PROC_SYPGM01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 공장구분 콤보박스 목록조회
        /// <summary>
        /// 기능명 : 공장구분 콤보박스 목록조회
        /// 작성자 : 박병수
        /// 작성일 : 2016-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9004_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9004_FACTCHK(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팀구분 콤보박스 목록조회
        /// <summary>
        /// 기능명 : 팀구분 콤보박스 목록조회
        /// 작성자 : 박병수
        /// 작성일 : 2016-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9005_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9005_FACTCHK(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반코드관리 조회
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : 박병수
        /// 작성일 : 2016-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9005_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9005_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion


        #region 팀코드관리 조회(콤보박스용)
        public DataSet MEAS9006_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9006_FACTCHK(oParams, out errMsg);
            }

            return ds;
        }
        #endregion


        #region 공정코드관리 조회
        public DataSet MEAS9006_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9006_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 계측기 협력사조회
        public DataSet MEAS9007_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MEAS9007_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 이력관리

        #region 계측기이력등록
        #region 계측기이력등록그리드
        /// <summary>
        /// 기능명 : 계측기이력등록그리드
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_GRID_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D4_GRID_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 계측기미반납현황
        #region 계측기미반납현황
        /// <summary>
        /// 기능명 : 계측기미반납현황
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_MEAS3002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D4_MEAS3002_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 개인별사용이력현황
        #region 개인별사용이력현황
        /// <summary>
        /// 기능명 : 개인별사용이력현황
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_MEAS3003_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D4_MEAS3003_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 계측기별사용이력현황

        #region 계측기별사용이력현황 계측기목록
        /// <summary>
        /// 기능명 : 계측기별사용이력현황 계측기목록
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01M_MEAS3004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01M_MEAS3004_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 계측기별사용이력현황 계측기이력
        /// <summary>
        /// 기능명 : 계측기별사용이력현황 계측기이력
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_MEAS3004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D4_MEAS3004_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 계측기이력카드

        #region 계측기이력카드 계측기 리스트
        /// <summary>
        /// 기능명 : 계측기이력카드 계측기 리스트
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01M_MEAS3005_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01M_MEAS3005_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 부속품 리스트
        /// <summary>
        /// 기능명 : 부속품 리스트
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D1_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검교정정보
        /// <summary>
        /// 기능명 : 검교정정보
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D3_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D3_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 계측기이력
        /// <summary>
        /// 기능명 : 계측기이력
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01D4_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 계측기 관리번호 팝업
        /// <summary>
        /// 기능명 : 계측기 관리번호 팝업
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01M_TBL_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01M_TBL_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 생산팀 목록
        /// <summary>
        /// 기능명 : 생산팀 목록
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet CM06M_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.CM06M_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반 목록
        /// <summary>
        /// 기능명 : 반 목록
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet CM07M_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.CM07M_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정 목록
        /// <summary>
        /// 기능명 : 공정 목록
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet CM08M_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.CM08M_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 메인화면 알림
        /// <summary>
        /// 메인화면 알림
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MS01M_LST_CURR(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MEASDac dac = new MEASDac())
            {
                ds = dac.MS01M_LST_CURR(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
    }
}
