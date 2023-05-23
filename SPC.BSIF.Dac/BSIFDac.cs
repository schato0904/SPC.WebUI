using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.BSIF.Dac
{
    /// <summary>ㅣ
    /// 기능명 : BSIFDac
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-10-15ㅣ
    /// 수정일 : 
    /// 설  명 : 기본정보 Database 처리용 함수 모음
    /// </summary>
    public class BSIFDac : IDisposable
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

        #region 기본정보 > 검사기준관리

        #region 검사기준 전체 갯수
        /// <summary>
        /// 기능명 : GetQCD34_CNT
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>Int32</returns>
        public Int32 GetQCD34_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QCD34_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 검사기준목록
        /// <summary>
        /// 기능명 : GetQCD34_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        public DataSet GetQCD34_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_LST2", out errMsg);
            }

            return ds;
        }
        #region 검사기준다중복사 목록

        #endregion

        #region 검사기준 전체 갯수(Batch 용)
        /// <summary>
        /// 기능명 : GetQCD34_CNT
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>Int32</returns>
        public Int32 GetQCD34_BATCH_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QCD34_BATCH_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 검사기준목록(Batch 용)
        /// <summary>
        /// 기능명 : GetQCD34_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_BATCH_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_BATCH_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준목록(팝업용)
        /// <summary>
        /// 기능명 : GetQCD34_POPUP_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-23
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준등록
        public bool QCD34_PROC_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD34_INS");
            }

            return bExecute;
        }
        #endregion

        #region 검사기준수정
        public bool QCD34_PROC_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD34_UPD");
            }

            return bExecute;
        }
        #endregion

        #region 검사기준삭제
        public bool QCD34_PROC_DEL(string[] oQuerys, object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oQuerys, oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준복사(Exists Check)
        public bool QCD34_PROC_EXISTS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteScaler("USP_QCD34_EXISTS").ToString().Equals("1");
            }

            return bExecute;
        }
        #endregion

        #region 검사기준복사
        public bool QCD34_PROC_COPY(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD34_COPY");
            }

            return bExecute;
        }
        #endregion

        #region 검사기준다중복사
        public bool QCD34_PROC_COPY2(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD34_COPY2");
            }

            return bExecute;
        }
        #endregion

        #region 검사기준 저장, 수정, 삭제
        /// <summary>
        /// 기능명 : PROC_QCD34_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-06-19
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_QCD34_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준목록(NEW)
        /// <summary>
        /// 기능명 : GetQCD34_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_NEW_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_NEW_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준목록팝업(NEW)
        /// <summary>
        /// 기능명 : GetQCD34_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_POPNEW_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34_POPNEW_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 동일항목검사기준수정
        /// <summary>
        /// 기능명 : BSIF0303_1COPY_LST
        /// 작성자 : KIM S
        /// 작성일 : 2017-07-31
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet BSIF0303_1COPY_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0303_1COPY_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준수정이력팝업
        /// <summary>
        /// 기능명 : QCD34A_POP_LST
        /// 작성자 : KIM S
        /// 작성일 : 2019-10-30
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet QCD34A_POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34A_POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 기본정보 > 반정보관리

        #region 반정보조회
        public DataSet QCD72_LST(Dictionary<string, string> oParams, out string errMsg)
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

        #region 반정보 저장, 수정, 삭제
        public bool PROC_QCD72_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 라인정보관리

        #region 라인정보조회
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

        #region 라인정보 저장, 수정, 삭제
        public bool PROC_QCD73_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 공정코드 관리

        #region 공정정보조회
        public DataSet QCD74_LST(Dictionary<string, string> oParams, out string errMsg)
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

        #region 공정정보 저장, 수정, 삭제
        public bool PROC_QCD74_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 공정별 설비관리

        #region 공정정보조회
        public DataSet QCD74_MACH_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD74_MACH_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정정보 저장, 수정, 삭제
        public bool PROC_QCD74_MACH_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 모델정보 관리

        #region 모델정보 저장, 수정, 삭제
        public bool PROC_QCD17_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg, out outMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 품목정보 관리

        #region 품목정보 저장, 수정, 삭제
        public bool PROC_QCD01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품목정보 저장, 수정, 삭제
        public bool PROC_QCD01_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg, out outMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 품목정보관리 > 품목별 라인등록

        #region 품목별 라인 조회
        public DataSet QCD011_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD011_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별 라인 저장, 수정, 삭제
        public bool PROC_QCD011_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 품목정보관리 > 품목별 도면 및 사진등록

        #region 도면 및 사진 목록
        public DataSet QCD014_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD014_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 도면 등록 및 수정
        public bool QCD014_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD014_INS_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 도면 삭제
        public bool QCD014_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD014_DEL", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 검사항목 관리

        #region 검사항목 조회
        public DataSet QCD33_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD33_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목 저장, 수정, 삭제
        public bool PROC_QCD33_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg, out outMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 설비관리

        #region 조회
        public DataSet QCD75_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD75_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        public bool PROC_QCD75_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 검사기준관리
        public DataSet QCD74_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD74_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 기본정보 > 항목별공정이상 설정

        #region 항목별공정이상 설정 조회
        public DataSet QCD34B_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34B_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별공정이상 저장, 수정, 삭제
        public bool PROC_QCD34B_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 반별PC정보 관리

        #region 반별PC정보 조회
        public DataSet QCDPCNM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDPCNM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반별PC정보 조회(공정코드별)
        public DataSet QCDPCNM_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDPCNM_WORK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반별PC정보 조회(공정그룹별)
        public DataSet QCDPCNM_WGROUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDPCNM_WGROUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반별PC정보 저장, 수정, 삭제
        public bool PROC_QCDPCNM_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 반별PC정보 저장, 수정, 삭제(공정코드별)
        public bool PROC_QCDPCNM_WORK_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 반별PC정보 저장, 수정, 삭제(공정그룹별)
        public bool PROC_QCDPCNM_WGROUP_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 모니터링공정관리 콤보
        public DataSet MORNITORING_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MORNITORING_WORK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 반별PC정보 관리 > PC별 라인 등록

        #region PC별 라인정보 조회
        public DataSet QCDPCBANLINE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDPCBANLINE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC별 라인 저장, 수정, 삭제
        public bool PROC_QCDPCBANLINE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 공정그룹별 공정정보 조회
        public DataSet QCDPCWGROUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDPCWGROUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정그룹별 공정정보 저장, 수정, 삭제
        public bool PROC_QCDPCWGROUP_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 사용자별 라인등록

        #region 사용자목록 조회
        public DataSet SYUSR01_QCD012_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSR01_QCD012_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회
        public DataSet QCD012_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD012_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회(버튼 클릭시)
        public DataSet QCD012_LST_EDIT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD012_LST_EDIT", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 저장, 수정, 삭제
        public bool PROC_QCD012_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 경광등 알람시간 관리

        #region 알람시간 조회
        public DataSet QCDALRAMTIME_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDALRAMTIME_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 알람시간 저장, 수정, 삭제
        public bool PROC_QCDALRAMTIME_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 경광등 사용설정

        #region 경광등사용설정 조회
        public DataSet QCDALRAMUSE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDALRAMUSE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 경광등 사용 저장, 수정, 삭제
        public bool PROC_QCDALRAMUSE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 부적합유형 정보관리

        #region 부적합유형 조회
        public DataSet QCD103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 부적합유형 저장, 수정, 삭제
        public bool PROC_QCD103_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 부적합유형 정보관리(대륙QR코드)

        #region 부적합유형 조회
        public DataSet BSIF0401_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0401_DACO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 불량원인코드 정보관리

        #region 불량원인코드 조회
        public DataSet QCD41_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD41_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인코드 저장, 수정, 삭제
        public bool PROC_QCD41_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 생산무사유 정보관리

        #region 생산무사유 조회
        public DataSet QCD101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 생산무사유 저장, 수정, 삭제
        public bool PROC_QCD101_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 공정이상기본설정 정보관리

        #region 공정이상기본설정 조회
        public DataSet QCDSHEWHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDSHEWHART_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정이상기본설정 저장
        public bool PROC_QCDSHEWHART_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 검사기준수정이력조회

        #region 검사기준수정이력조회
        public DataSet QCD34A_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD34A_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준수정이력조회 전체 갯수
        public Int32 QCD34A_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QCD34A_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 기본정보 > 관리한계 이력조회

        #region 관리한계 이력 목록
        /// <summary>
        /// 기능명 : QCD35_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QCD35_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD35_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 관리한계 이력 등록
        /// <summary>
        /// 기능명 : QCD35_INS
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QCD35_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD35_INS", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 숙련도관리

        #region 숙련도정보조회
        public DataSet QCDLEVEL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCDLEVEL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 숙련도입력
        /// <summary>
        /// 기능명 : 품질이상제기 등록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QCDLEVEL_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCDLEVEL_INS", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도수정
        /// <summary>
        /// 기능명 : 품질이상제기 등록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QCDLEVEL_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCDLEVEL_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 저장, 수정, 삭제
        public bool PROC_QCDLEVEL_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 이미지 수정
        public bool QCDLEVEL_IMAGE_UPD(string[] oSps, object[] oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery4(oSps, oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 삭제
        public bool QCDLEVEL_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCDLEVEL_DEL");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 업무지시관리

        #region 채번
        /// <summary>
        /// WORK01_LST
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public string WORK01_IDX(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                result = spcDB.ExecuteScaler("USP_WORK01_IDX").ToString();
            }

            return result;
        }
        #endregion

        #region 목록
        /// <summary>
        /// WORK01_LST
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet WORK01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WORK01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작지목록
        /// <summary>
        /// WORK01_LST
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MPPLN03V_SPC_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MPPLN03V_SPC_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 가공불량항목관리

        #region 가공불량항목 조회
        public DataSet QCE33_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCE33_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 가공불량항목 조회
        public DataSet QCE33_1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCE33_1_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 가공불량항목 저장, 수정
        public bool QCE33_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 라인별가공불량항목관리

        #region 라인별가공불량항목 조회
        public DataSet QCE34_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCE34_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인별가공불량항목 저장, 수정
        public bool QCE34_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 생산도번관리 관리(코티드)

        #region 생산도번관리 저장, 수정, 삭제
        public bool PROC_QCD17A_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 기본정보 > 3차원 매핑

        #region 매핑 리스트 조회
        public DataSet BSIF0306_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0306_LST", out errMsg);
            }

            return ds;
        }      
        #endregion

        #endregion

        #region 기본정보 > 모니터별 항목설정

        #region LOCATION 항목 조회
        public DataSet BSIF0505_BORGWARNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0505_BORGWARNER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목 콤보박스 조회
        public DataSet BSIF0505_BORGWARNER_MEAINSP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0505_BORGWARNER_MEAINSP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목 저장
        public bool BSIF0505_BORGWARNER_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_BSIF0505_BORGWARNER_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 시스템설정 > 검사기준삭제
        #region 측정Data 전체 갯수
        /// <summary>
        /// 기능명 : BSIF0303_DEL_LST
        /// 작성자 : KIM S
        /// 작성일 : 2017-09-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">oParams</param>
        /// <returns>Int32</returns>
        public Int32 BSIF0303_DEL_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_BSIF0303_DEL_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 측정Data LIST
        /// <summary>
        /// 기능명 : BSIF0303_DEL_LST
        /// 작성자 : KIM S
        /// 작성일 : 2017-09-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet BSIF0303_DEL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0303_DEL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region ANDON

        #region ANDON 사용자목록 조회
        public DataSet SYUSR01_ANDON_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSR01_ANDON_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region ANDON 상세 조회
        public DataSet SYUSR01_ANDON_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSR01_ANDON_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region ANDON 발생현황조회
        public DataSet BSIF0504_ANDON_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0504_ANDON_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 거래처관리

        #region 거래처관리 거래처 조회
        public DataSet PARTNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_PARTNER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목조회
        public DataSet QCD01_PARTNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_PARTNER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목조회
        public DataSet QCD01_PARTNER_MERGE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD01_PARTNER_MERGE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목조회
        public DataSet BSIF0903_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BSIF0903_FOSECO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
        #region 배치 업데이트
        #region 배치 업데이트
        public bool PROC_BATCH_UPDATE(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion
        #endregion
    }
}
