using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.Common.Dac
{
    /// <summary>
    /// 기능명 : CommonDacNTx
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-09-03
    /// 수정일 : 
    /// 설  명 : 공용 Database 처리용 함수 모음
    /// </summary>
    public class CommonDac : IDisposable
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

        #region 조회 프로시저
        /// <summary>
        /// 기능명 : 조회 프로시저
        /// 작성자 : KIM S
        /// 작성일 : 2014-11-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetDatSet(Dictionary<string, string> oParams, out string errMsg, string procNM)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet(procNM, out errMsg);
            }

            return ds;
        }

        public Int32 GetDatSet_Count(Dictionary<string, string> oParams, string procNM)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler(procNM));
            }

            return resultCnt;
        }
        #endregion

        #region 로그인페이지 정보를 구한다
        /// <summary>
        /// 기능명 : 로그인페이지 정보를 구한다
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-09-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetLoginPageInfo(out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                ds = spcDB.GetDataSet("USP_SYLOGIN_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자접속로그

        #region 사용자접속정보(대메뉴목록)
        /// <summary>
        /// 기능명 : 사용자접속정보(대메뉴목록)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet SYUSRLOG_LMENU_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSRLOG_LMENU_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region 사용자접속정보저장
        /// <summary>
        /// 기능명 : 사용자접속정보저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_SYUSRLOG(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_SYUSRLOG_INS_UPD");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 비밀번호변경
        /// <summary>
        /// 기능명 : 비밀번호변경
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>string</returns>
        public string COMM0201_CHANGE_PASSWORD(Dictionary<string, string> oParams)
        {
            string resultCode = String.Empty;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                resultCode = spcDB.ExecuteScaler("USP_COMM0201_CHANGE_PASSWORD").ToString();
            }

            return resultCode;
        }
        #endregion

        #region 사용자로그인 로그기록
        /// <summary>
        /// 기능명 : 사용자로그인 로그기록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2018-04-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool SYUSRLOGINLOG_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_SYUSRLOGINLOG_INS", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 사용자최종접속정보
        /// <summary>
        /// 기능명 : 사용자최종접속정보
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2018-04-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet SYUSRLOGINLOG_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSRLOGINLOG_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공통코드

        #region 공통코드목록
        /// <summary>
        /// 기능명 : 공통코드목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetCommonCodeList(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_COMMONCODE", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 코드중복검사
        /// <summary>
        /// 기능명 : 코드중복검사
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-14
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QCM10_EXT_COMMCD(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_QCM10_EXT_COMMCD");
            }

            return bExists;
        }
        #endregion

        #region 유저아이디 중복검사
        /// <summary>
        /// 기능명 : 유저아이디 중복검사
        /// 작성자 : 박병수
        /// 작성일 : 2015-04-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_SYUSR01_DUPLICATE(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_SYUSR01_DUPLICATE");
            }

            return bExists;
        }
        #endregion


        #region 공통코드저장
        /// <summary>
        /// 기능명 : 공통코드저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QCM10_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCM10_INS");
            }

            return bExecute;
        }
        #endregion

        #region 공통코드수정
        /// <summary>
        /// 기능명 : 공통코드수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QCM10_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCM10_UPD");
            }

            return bExecute;
        }
        #endregion

        #region 공통코드삭제
        /// <summary>
        /// 기능명 : 공통코드삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-08
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QCM10_DEL(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_QCM10_DEL", oParams);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 업체별, 공장별 정보

        #region 업체목록
        /// <summary>
        /// 기능명 : 업체목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCM01_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCM01_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 업체별 협력업체목록
        /// <summary>
        /// 기능명 : 마스터 업체별 협력업체목록
        /// 작성자 : KIM S
        /// 작성일 : 2014-12-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCM01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCM01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공장목록
        /// <summary>
        /// 기능명 : 공장목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCM02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCM02_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반목록
        /// <summary>
        /// 기능명 : 반목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD72_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD72_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반별 납품업체 목록
        /// <summary>
        /// 기능명 : 반별 납품업체 목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD00_BAN_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD00_BAN_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인 목록
        /// <summary>
        /// 기능명 : 라인 목록
        /// 작성자 : KIM S
        /// 작성일 : 2014-11-14
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD73_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD73_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정목록
        /// <summary>
        /// 기능명 : 공정목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD74_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD74_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목목록

        #region 품목목록
        /// <summary>
        /// 기능명 : 품목목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목목록(성신)
        public DataSet GetQCD01_LST_SUNGSHIN(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_LST_SUNGSHIN", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목목록(보그워너)
        public DataSet GetQCD01_LST_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_LST_BORGWARNER", out errMsg);
            }

            return ds;
        }
        public DataSet QCM99_LST_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCM99_LST_BORGWARNER", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 검사항목목록
        /// <summary>
        /// 기능명 : 검사항목목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-29
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet INSPITEM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INSPITEM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목목록(그룹)
        /// <summary>
        /// 기능명 : 검사항목목록(그룹)
        /// 작성자 : KIM
        /// 작성일 : 2020-09-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet INSPDETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INSPDETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 계측기 정보

        #region 계측기목록
        /// <summary>
        /// 기능명 : 계측기목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQC30_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD30_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 파일 정보

        #region 파일갯수
        /// <summary>
        /// 기능명 : 파일갯수
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>Int32</returns>
        public Int32 GetATTFILE_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_ATTFILE_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 파일목록
        /// <summary>
        /// 기능명 : 파일목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-30
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetATTFILE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_ATTFILE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 파일등록
        /// <summary>
        /// 기능명 : 파일등록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-30
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public bool PROC_ATTFILE_INS(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_ATTFILE_INS", oParams);
            }

            return bExecute;
        }
        #endregion

        #region 파일삭제
        /// <summary>
        /// 기능명 : 파일삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-30
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public bool PROC_ATTFILE_DEL(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_ATTFILE_DEL", oParams);
            }

            return bExecute;
        }
        #endregion

        #region 파일삭제시 부모테이블 UPDATE
        /// <summary>
        /// 기능명 : 파일삭제시 부모테이블 UPDATE
        /// 작성자 : KIM S
        /// 작성일 : 2015-01-16
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public bool ATTFILE_ATTFILENO_UPD(object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery3("USP_ATTFILE_ATTFILENO_UPD", oParams);
            }

            return bExecute;
        }
        #endregion


        #endregion

        #region 품목검색 정보

        #region 품목검색목록
        /// <summary>
        /// 기능명 : 품목검색목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-03
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD01_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정검사 품목검색목록
        /// <summary>
        /// 기능명 : 공정검사품목검색목록
        /// 작성자 : KIM S
        /// 작성일 : 2018-01-29
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD01_WERD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_WERD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정검사공정검색목록
        /// <summary>
        /// 기능명 : 공정검사공정검색목록
        /// 작성자 : KIM S
        /// 작성일 : 2018-01-29
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD74_WERD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD74_WERD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 검사항목검색 정보

        #region 검사항목검색목록
        /// <summary>
        /// 기능명 : 검사항목목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-03
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD33_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD33_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목검색목록(보그워너)
        /// <summary>
        /// 기능명 : 검사항목목록
        /// 작성자 : KIM S
        /// 작성일 : 
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD33_POPUP_BORGWARNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD33_POPUP_BORGWARNER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 모델정보

        #region 모델정보 목록조회
        /// <summary>
        /// 기능명 : 모델정보 목록 조회
        /// 작성자 : KIM S
        /// 작성일 : 2014-11-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD17_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD17_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region 모델정보 목록조회(성신)
        public DataSet QCD17_LST_SUNGSHIN(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD17_LST_SUNGSHIN", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모델정보 모델명
        /// <summary>
        /// 기능명 : 모델정보 모델명
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD17_MODELNM_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD17_MODELNM_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 메뉴정보

        #region 대메뉴목록
        /// <summary>
        /// 기능명 : 대메뉴목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-25
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>DataSet</returns>
        public DataSet MENUL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MENUL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 중메뉴목록
        /// <summary>
        /// 기능명 : 중메뉴목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-25
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>DataSet</returns>
        public DataSet MENUM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MENUM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 측정정보

        #region 측정갯수(팝업용)
        /// <summary>
        /// 기능명 : 측정갯수(팝업용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public Int32 QWK03A_POPUP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03_POPUP_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 측정목록(팝업용)
        /// <summary>
        /// 기능명 : 측정목록(팝업용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK03A_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 전수측정갯수(팝업용)
        /// <summary>
        /// 기능명 : 전수측정갯수(팝업용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public Int32 QWK08A_POPUP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08_POPUP_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 전수측정목록(팝업용)
        /// <summary>
        /// 기능명 : 전수측정목록(팝업용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-09-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK08A_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 품목별 Worst분석

        #region 품목별Worst분석
        public DataSet QWK03A_WSTA0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0101_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0101_DATA_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세 카운트
        public Int32 QWK03A_WSTA0101_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_WSTA0101_DATA_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 항목별 Worst분석

        #region 항목별Worst분석
        public DataSet QWK03A_WSTA0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0102_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0102_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0102_DATA_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세 카운트
        public Int32 QWK03A_WSTA0102_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_WSTA0102_DATA_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 검사항목 Worst분석

        #region 검사항목Worst분석
        public DataSet QWK03A_WSTA0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0103_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0103_DATA_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목Worst분석 DATA 상세 카운트
        public Int32 QWK03A_WSTA0103_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_WSTA0103_DATA_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 주차정보
        #region 주차리스트
        /// <summary>
        /// 기능명 : 주차리스트
        /// 작성자 : hasra
        /// 작성일 : 2015-01-15
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD107_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD107_WEEK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 주차별 시작일, 종료일
        public DataSet GetQCD107_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD107_GET", out errMsg);
            }

            return ds;
        }
        #endregion 
        #endregion

        #region 공지사항(공정이상통보 데이터)
        /// <summary>
        /// 기능명 : 공지사항(공정이상통보 데이터)
        /// 작성자 : 이주남
        /// 작성일 : 2015-01-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQWKWRONGREPORT_COMM0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWKWRONGREPORT_COMM0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 트리정보

        #region 트리정보 목록조회
        /// <summary>
        /// 기능명 : 트리정보 목록 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet TREE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TREE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 트리정보 목록조회(DevExpress 용)
        /// <summary>
        /// 기능명 : 트리정보 목록 조회(DevExpress 용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet DEVTREE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_DEVTREE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 생산도번관리(코티드)

        #region 생산도번관리 목록조회
        /// <summary>
        /// 기능명 : 생산도번관리 목록 조회
        /// 작성자 : KIM S
        /// 작성일 : 2014-11-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD17A_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD17A_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비정보
		  
        #region 설비코드팝업
        /// <summary>
        /// 기능명 : 설비코드팝업
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2017-02-16
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetMACH14_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH04_POP_LST", out errMsg);
            }

            return ds;
        } 

        #endregion

        #region 설비검사항목검색목록
        /// <summary>
        /// 기능명 : 설비검사항목목록
        /// 작성자 : LEE HWAN UI
        /// 작성일 : 2017-02-16
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD_MACH10_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH10_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region WinAPI

        #region nTx
        /// <summary>
        /// 기능명 : 웹서비스 통신용 모듈(nTx)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2018-04-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="sSp">string</param>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet WinAPInTx(string sSp, Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                spcDB.bUsedTrans = false;
                ds = spcDB.GetDataSet(sSp, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Tx(Single)
        /// <summary>
        /// 기능명 : 웹 서비스 통신용 모듈(Tx-Single)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2018-04-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="sSp">string</param>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool WinAPITxSingle(string sSp, Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery(sSp, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region Tx(Multiple)
        /// <summary>
        /// 기능명 : 웹 서비스 통신용 모듈(Tx-Multiple)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2018-04-05
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="sSp">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool WinAPITxMultiple(string[] oSps, object[] oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 4M
        /// <summary>
        /// 기능명 : 4M
        /// 작성자 : KIM S
        /// 작성일 : 2019-04-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet QCD40_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD40_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 대륙 
        #region 구분목록
        public DataSet GetQWK110_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_GETGUBUN_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 업체목록
        public DataSet COMPANY_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_COMPANY_DACO_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion
    }
}
