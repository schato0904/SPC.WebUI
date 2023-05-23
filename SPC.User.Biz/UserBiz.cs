using System;
using System.Collections.Generic;
using System.Data;
using SPC.User.Dac;

namespace SPC.User.Biz
{
    /// <summary>
    /// 기능명 : UserBiz
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-09-27
    /// 수정일 : 
    /// 설  명 : 사용자 처리용 함수 모음
    /// </summary>
    public class UserBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
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

            using (UserDac dac = new UserDac())
            {
                ds = dac.ProcLogin(oParams, out errMsg);
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

            using (UserDac dac = new UserDac())
            {
                ds = dac.ProcByPassLogin(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자접속통계

        #region 사용자접속통계(MASTER)
        /// <summary>
        /// 기능명 : SYUSR01_MASTER_LST
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

            using (UserDac dac = new UserDac())
            {
                ds = dac.SYUSRLOG_MASTER_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자접속통계(DETAIL)
        /// <summary>
        /// 기능명 : SYUSR01_MASTER_LST
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

            using (UserDac dac = new UserDac())
            {
                ds = dac.SYUSRLOG_DETAIL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
