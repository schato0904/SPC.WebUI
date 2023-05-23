using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.User.Dac
{
    /// <summary>
    /// 기능명 : UserDac
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-09-27
    /// 수정일 : 
    /// 설  명 : 사용자 Database 처리용 함수 모음
    /// </summary>
    public class UserDac : IDisposable
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

        #region 로그인
        /// <summary>
        /// 기능명 : ProcLogin
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ProcLogin(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_LOGIN", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 로그인(ByPass)
        /// <summary>
        /// 기능명 : ProcByPassLogin
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ProcByPassLogin(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BYPASS_LOGIN", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자접속통계

        #region 사용자접속통계(MASTER)
        /// <summary>
        /// 기능명 : SYUSRLOG_MASTER_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet SYUSRLOG_MASTER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSRLOG_MASTER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자접속통계(DETAIL)
        /// <summary>
        /// 기능명 : SYUSRLOG_DETAIL_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet SYUSRLOG_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSRLOG_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
