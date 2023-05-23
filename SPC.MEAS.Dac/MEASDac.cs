using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.MEAS.Dac
{
    /// <summary>
    /// 설비일상점검
    /// </summary>
    public class MEASDac : IDisposable
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

        #region UPDATE, INSERT, DELETE
        /// <summary>
        /// 기능명 : UPDATE, INSERT, DELETE
        /// 작성자 : KIM S
        /// 작성일 : 2015-06-18
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

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            resultMsg = spcDB.command.Parameters["@PKEY"].Value.ToString();

            return bExecute;
        }
        #endregion

        #region UPDATE, INSERT, DELETE NO PKEY
        /// <summary>
        /// 기능명 : UPDATE, INSERT, DELETE
        /// 작성자 : KIM S
        /// 작성일 : 2015-06-18
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

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_TEAM_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_TERM_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_BAN_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_PROC_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : MEAS1001_LST
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2015-06-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : MEAS1001_MS01D1_LST
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2015-06-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet MEAS1001_MS01D1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_MS01D1_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : MEAS1001_INF
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1001_INF", out errMsg);
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
        /// <returns>DataSet</returns>
        public bool MEAS1001_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                //if (!oParams.ContainsKey("F_MS01MID")) oParams.Add("F_MS01MID", "OUTPUT");
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS1001_INS", out errMsg);
                //pkey = bExecute ? (spcDB.command.Parameters["@F_MS01MID"].Value ?? "").ToString() : "";
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
        /// <returns>DataSet</returns>
        public bool MEAS1001_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS1001_UPD", out errMsg);
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
        /// <returns>DataSet</returns>
        public bool MEAS1001_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS1001_DEL", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS1002_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MEAS1002_CNT"));
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2001_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MEAS2001_CNT"));
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2002_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MEAS2002_CNT"));
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2003_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MEAS2003_CNT"));
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2004_MST_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2004_DTL_LST", out errMsg);
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
        /// <returns>DataSet</returns>
        public bool MEAS2004_INS(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                if (!oParams.ContainsKey("F_REQNO")) oParams.Add("F_REQNO", "OUTPUT");

                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS2004_INS", out errMsg);
                pkey = bExecute ? (spcDB.command.Parameters["@F_REQNO"].Value ?? "").ToString() : "";
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
        /// <returns>DataSet</returns>
        public bool MEAS2004_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS2004_UPD", out errMsg);
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
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public bool MEAS2004_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS2004_DEL", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2005_MST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS2005_DTL", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 검교정접수 저장
        /// 작성자 : KIM MIN SAM
        /// 작성일 : 2015-08-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public bool MEAS2005_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS2005_INS", out errMsg);
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
        /// <returns>DataSet</returns>
        public bool MEAS2005_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_MEAS2005_DEL", out errMsg);
            }

            return bExecute;
        }

        #endregion

        #endregion

        #region 계측기 > 검교정실적관리

        #region 계측기 > 검교정실적관리 > 의뢰번호 팝업

        /// <summary>
        /// 검교정접수 목록
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MEAS4001_POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS4001_POPUP_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 검교정접수 목록 개수
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public Int32 MEAS4001_POP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MEAS4001_POPUP_CNT"));
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS4001_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 검교정접수 저장, 수정, 삭제
        /// </summary>
        /// <param name="oSps"></param>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool MEAS4001_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }

        #endregion

        #region 계측기 > 검교정실적관리 > 검교정현황
        /// <summary>
        /// 검교정현황
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet MS01D5_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D5_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 업체별 공통코드 목록
        /// <summary>
        /// 기능명 : SYCOD01_LST
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2015-06-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYCOD01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYCOD01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 정기검사 저장,수정
        /// <summary>
        /// 기능명 : USP_MEAS1001_BATCH
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2015-06-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool USP_MEAS1001_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 계측기 > 기본정보관리

        #region 생산팀 코드관리 조회
        /// <summary>
        /// 기능명 : 프로그램조회
        /// 작성자 : 박병수
        /// 작성일 : 2016-6-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9004_LST", out errMsg);
            }

            return ds;
        }
        #endregion



        #region 프로그램 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : 박병수
        /// 작성일 : 2016-6-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion


        #region 프로그램 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : 박병수
        /// 작성일 : 2016-6-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9004_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9004_FACTCHK", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팀구분 콤보박스 목록조회
        /// <summary>
        /// 기능명 : 팀구분 콤보박스 목록조회
        /// 작성자 : 박병수
        /// 작성일 : 2016-6-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9005_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9005_FACTCHK", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반코드관리 조회
        /// <summary>
        /// 기능명 : 프로그램조회
        /// 작성자 : 박병수
        /// 작성일 : 2016-6-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MEAS9005_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9005_LST", out errMsg);
            }

            return ds;
        }
        #endregion


        #region 콤보박스용조회
        public DataSet MEAS9005_FACTCHK_COMBO(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9005_FACTCHK_COMBO", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 콤보박스용조회
        public DataSet MEAS9006_BANCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9006_BANCHK", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팀구분 콤보박스 목록조회
        public DataSet MEAS9006_FACTCHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9006_TEAMCHK", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정코드관리 조회

        public DataSet MEAS9006_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9006_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 계측기협력사 조회

        public DataSet MEAS9007_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MEAS9007_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 이력관리

        #region 계측기 이력등록
        #region 계측기 이력등록 그리드
        /// <summary>
        /// 기능명 : 계측기 이력등록 그리드
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet MS01D4_GRID_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D4_GRID_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D4_MEAS3002_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D4_MEAS3003_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01M_MEAS3004_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D4_MEAS3004_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01M_MEAS3005_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D1_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D3_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01D4_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01M_TBL_POPUP_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CM06M_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반정보 목록
        /// <summary>
        /// 기능명 : 반정보 목록
        /// 작성자 : KIMS
        /// 작성일 : 2016-08-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public DataSet CM07M_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CM07M_POPUP_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CM08M_POPUP_LST", out errMsg);
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

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MS01M_LST_CURR", out errMsg);
            }

            return ds;
        }
        #endregion
    }
}
