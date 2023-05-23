using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.PFRC.Dac
{
    /// <summary>
    /// 기능명 : PFRCDac
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-10-15
    /// 수정일 : 
    /// 설  명 : 환경설정 Database 처리용 함수 모음
    /// </summary>
    public class PFRCDac : IDisposable
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

        #region 프로그램관리

        #region 프로그램목록
        /// <summary>
        /// 기능명 : SYPGM01_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-15
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYPGM01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYPGM01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 프로그램 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-16
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 프로그램중복검사
        /// <summary>
        /// 기능명 : 프로그램중복검사
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-14
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_EXT_PGMID(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_SYPGM01_EXT_PGMID");
            }

            return bExists;
        }
        #endregion

        #region 프로그램저장
        /// <summary>
        /// 기능명 : 프로그램저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_INS(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_SYPGM01_INS", oParams);
            }

            return bExecute;
        }
        #endregion

        #region 프로그램수정
        /// <summary>
        /// 기능명 : 프로그램수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_UPD(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_SYPGM01_UPD", oParams);
            }

            return bExecute;
        }
        #endregion

        #region 프로그램삭제
        /// <summary>
        /// 기능명 : 프로그램삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM01_DEL(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_SYPGM01_DEL", oParams);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 메뉴관리

        #region 메뉴목록
        /// <summary>
        /// 기능명 : SYPGM02_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet SYPGM02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYPGM02_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 메뉴 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : 프로그램저장, 수정, 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SYPGM02_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 측정프로그램 인증관리

        #region 조회
        /// <summary>
        /// QCDACTIVATE_LST
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QCDACTIVATE_LST(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                ds = spcDB.GetDataSet("USP_QCDACTIVATE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상태변경
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oParams"></param>
        /// <returns></returns>
        public bool QCDACTIVATE_RES(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCDACTIVATE_RES", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 모니터링관리

        #region 조회
        /// <summary>
        /// 기능명 : QCDMONITORING_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2020-07-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet QCDMONITORING_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDMONITORING_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        /// <summary>
        /// 기능명 : 저장, 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2020-07-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_QCDMONITORING_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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
    }
}
