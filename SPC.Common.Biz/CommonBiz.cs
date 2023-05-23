using System;
using System.Collections.Generic;
using System.Data;

using SPC.Common.Dac;

namespace SPC.Common.Biz
{
    public class CommonBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 조회 프로시저
        /// <summary>
        /// 기능명 : 조회 프로시저
        /// 작성자 : KIM S
        /// 작성일 : 2014-10-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetDatSet(Dictionary<string, string> oParams, out string errMsg, string procNM)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetDatSet(oParams, out errMsg, procNM);
            }

            return ds;
        }

        public Int32 GetDatSet_Count(Dictionary<string, string> oParams, string procNM)
        {
            Int32 resultCnt = -1;

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.GetDatSet_Count(oParams, procNM);
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
            DataSet ds = null;

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetLoginPageInfo(out errMsg);
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
            DataSet ds = null;

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.SYUSRLOG_LMENU_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_SYUSRLOG(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                resultCode = dac.COMM0201_CHANGE_PASSWORD(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.SYUSRLOGINLOG_INS(oParams, out errMsg);
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
            DataSet ds = null;

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.SYUSRLOGINLOG_GET(oParams, out errMsg);
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
            DataSet ds = null;

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetCommonCodeList(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                bExists = dac.PROC_QCM10_EXT_COMMCD(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExists = dac.PROC_SYUSR01_DUPLICATE(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_QCM10_INS(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_QCM10_UPD(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_QCM10_DEL(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCM01_LST2(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCM01_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCM02_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD72_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모델코드
        /// <summary>
        /// 기능명 : 모델코드
        /// 작성자 : hasra
        /// 작성일 : 2015-01-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD17_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD17_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD00_BAN_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD73_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD74_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목목록(성신)
        public DataSet GetQCD01_LST_SUNGSHIN(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD01_LST_SUNGSHIN(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목목록(보그워너)
        public DataSet GetQCD01_LST_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD01_LST_BORGWARNER(oParams, out errMsg);
            }

            return ds;
        }
        public DataSet QCM99_LST_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCM99_LST_BORGWARNER(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.INSPITEM_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.INSPDETAIL_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQC30_LST(oParams, out errMsg);
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
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public Int32 GetATTFILE_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.GetATTFILE_CNT(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetATTFILE_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_ATTFILE_INS(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.PROC_ATTFILE_DEL(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 파일삭제시 부모테이블 UPDATE
        /// <summary>
        /// 기능명 : 파일삭제시 부모테이블 UPDATE
        /// 작성자 : KIMS
        /// 작성일 : 2015-01-16
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public bool ATTFILE_ATTFILENO_UPD(object[] oParams)
        {
            bool bExecute = false;

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.ATTFILE_ATTFILENO_UPD(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD01_POPUP_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD01_WERD_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD74_WERD_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 검사항목검색 정보

        #region 검사항목검색목록
        /// <summary>
        /// 기능명 : 검사항목검색목록
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD33_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목검색목록(보그워너)
        /// <summary>
        /// 기능명 : 검사항목검색목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-03
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD33_POPUP_BORGWARNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD33_POPUP_BORGWARNER_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기종정보 목록

        #region 기종정보 목록조회
        /// <summary>
        /// 기능명 : 기종정보 목록조회
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD17_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 기종정보 목록조회(성신)
        public DataSet QCD17_LST_SUNGSHIN(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD17_LST_SUNGSHIN(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 기종정보 모델명
        /// <summary>
        /// 기능명 : 기종정보 모델명
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD17_MODELNM_GET(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.MENUL_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.MENUM_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.QWK03A_POPUP_CNT(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_POPUP_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.QWK08A_POPUP_CNT(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK08A_POPUP_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0101_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0101_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별Worst분석 DATA 상세카운트
        public Int32 QWK03A_WSTA0101_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.QWK03A_WSTA0101_DATA_CNT(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0102_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0102_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0102_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별Worst분석 DATA 상세카운트
        public Int32 QWK03A_WSTA0102_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.QWK03A_WSTA0102_DATA_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 공정능력 Worst분석

        #region 공정능력Worst분석
        public DataSet QWK03A_WSTA0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0103_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력Worst분석 DATA 상세
        public DataSet QWK03A_WSTA0103_DATA_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QWK03A_WSTA0103_DATA_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력Worst분석 DATA 상세카운트
        public Int32 QWK03A_WSTA0103_DATA_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (CommonDac dac = new CommonDac())
            {
                resultCnt = dac.QWK03A_WSTA0103_DATA_CNT(oParams);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD107_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 주차별 시작일, 종료일
        public DataSet GetQCD107_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQCD107_GET(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQWKWRONGREPORT_COMM0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 트리정보

        #region 트리정보 목록조회
        /// <summary>
        /// 기능명 : 트리정보 목록조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet TREE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.TREE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 트리정보 목록조회(DevExpress 용)
        /// <summary>
        /// 기능명 : 트리정보 목록조회(DevExpress 용)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-27
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet DEVTREE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.DEVTREE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 생산도번관리 목록(코티드)

        #region 생산도번관리 목록조회
        /// <summary>
        /// 기능명 : 생산도번관리 목록조회
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD17A_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비일상정검

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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetMACH14_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        } 

        #endregion

        #region 설비검사항목검색목록
        /// <summary>
        /// 기능명 : 설비검사항목검색목록
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD_MACH10_POPUP_LST(oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.WinAPInTx(sSp, oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.WinAPITxSingle(sSp, oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                bExecute = dac.WinAPITxMultiple(oSps, oParams, out errMsg);
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

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.QCD40_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 대륙
        #region 구분목록
        public DataSet GetQWK110_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.GetQWK110_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #region 업체목록
        public DataSet COMPANY_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (CommonDac dac = new CommonDac())
            {
                ds = dac.COMPANY_DACO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

    }
}
