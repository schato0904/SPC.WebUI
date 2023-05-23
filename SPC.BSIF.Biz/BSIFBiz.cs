using System;
using System.Collections.Generic;
using System.Data;

using SPC.BSIF.Dac;

namespace SPC.BSIF.Biz
{
    public class BSIFBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
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

            using (BSIFDac dac = new BSIFDac())
            {
                resultCnt = dac.GetQCD34_CNT(oParams);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준다중복사 목록
        public DataSet GetQCD34_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_LST2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준 전체 갯수(Batch 용)
        /// <summary>
        /// 기능명 : GetQCD34_BATCH_CNT
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

            using (BSIFDac dac = new BSIFDac())
            {
                resultCnt = dac.GetQCD34_BATCH_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 검사기준목록(Batch 용)
        /// <summary>
        /// 기능명 : GetQCD34_BATCH_LST
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_BATCH_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준목록(팝업용)
        /// <summary>
        /// 기능명 : GetQCD34_POPUP_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet GetQCD34_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준등록
        public bool QCD34_PROC_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준수정
        public bool QCD34_PROC_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_UPD(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준삭제
        public bool QCD34_PROC_DEL(string[] oQuerys, object[] oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_DEL(oQuerys, oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준복사(Exists Check)
        public bool QCD34_PROC_EXISTS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_EXISTS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준복사
        public bool QCD34_PROC_COPY(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_COPY(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준다중복사
        public bool QCD34_PROC_COPY2(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD34_PROC_COPY2(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 검사기준 저장, 수정, 삭제
        public bool PROC_QCD34_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD34_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_NEW_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.GetQCD34_POPNEW_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0303_1COPY_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD34A_POP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 반정보관리

        #region 반정보 조회
        public DataSet QCD72_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD72_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반정보 저장, 수정, 삭제
        public bool PROC_QCD72_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD72_BATCH(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 라인정보관리

        #region 라인정보 조회
        public DataSet QCD73_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD73_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 반정보 저장, 수정, 삭제
        public bool PROC_QCD73_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD73_BATCH(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 공정정보관리

        #region 공정정보 조회
        public DataSet QCD74_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD74_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정정보 저장, 수정, 삭제
        public bool PROC_QCD74_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD74_BATCH(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 공정별 설비관리

        #region 공정정보 조회
        public DataSet QCD74_MACH_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD74_MACH_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정정보 저장, 수정, 삭제
        public bool PROC_QCD74_MACH_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD74_MACH_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 모델정보관리

        #region 모델정보 저장, 수정, 삭제
        public bool PROC_QCD17_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD17_BATCH(oSps, oParams, out resultMsg, out outMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 품목정보관리

        #region 품목정보 저장, 수정, 삭제
        public bool PROC_QCD01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품목정보 저장, 수정, 삭제
        public bool PROC_QCD01_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD01_BATCH(oSps, oParams, out resultMsg, out outMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD011_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별 라인 저장, 수정, 삭제
        public bool PROC_QCD011_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD011_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD014_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 도면 등록 및 수정
        public bool QCD014_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD014_INS_UPD(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 도면 삭제
        public bool QCD014_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCD014_DEL(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 검사항목관리

        #region 검사항목 조회
        public DataSet QCD33_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD33_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목 저장, 수정, 삭제
        public bool PROC_QCD33_BATCH(string[] oSps, object[] oParams, out string resultMsg, out string[] outMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD33_BATCH(oSps, oParams, out resultMsg, out outMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD75_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정
        public bool PROC_QCD75_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD75_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 검사기준관리
        public DataSet QCD74_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD74_LST2(oParams, out errMsg);
            }

            return ds;

        }
        #endregion

        #region 기본정보 > 항목별공정이상설정 관리

        #region 항목별공정이상설정 조회
        public DataSet QCD34B_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD34B_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별공정이상설정 저장, 수정, 삭제
        public bool PROC_QCD34B_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD34B_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 반별 PC정보 관리

        #region PC정보 조회
        public DataSet QCDPCNM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDPCNM_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC정보 저장, 수정, 삭제
        public bool PROC_QCDPCNM_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDPCNM_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region PC정보 조회(공정코드별)
        public DataSet QCDPCNM_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDPCNM_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC정보 저장, 수정, 삭제(공정코드별)
        public bool PROC_QCDPCNM_WORK_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDPCNM_WORK_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 모니터링공정관리 콤보
        public DataSet MORNITORING_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.MORNITORING_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC정보 조회(공정그룹별)
        public DataSet QCDPCNM_WGROUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDPCNM_WGROUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC정보 저장, 수정, 삭제(공정그룹별)
        public bool PROC_QCDPCNM_WGROUP_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDPCNM_WGROUP_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 반별 PC정보 관리 > PC별 라인 정보 등록

        #region PC별 라인 정보 조회
        public DataSet QCDPCBANLINE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDPCBANLINE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC별 라인 정보 저장, 수정, 삭제
        public bool PROC_QCDPCBANLINE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDPCBANLINE_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region PC별 라인 정보 조회
        public DataSet QCDPCWGROUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDPCWGROUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region PC별 라인 정보 저장, 수정, 삭제
        public bool PROC_QCDPCWGROUP_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDPCWGROUP_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.SYUSR01_QCD012_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회
        public DataSet QCD012_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD012_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 조회(전체 버튼 클릭시)
        public DataSet QCD012_LST_EDIT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD012_LST_EDIT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 사용자별 라인 정보 저장, 수정, 삭제
        public bool PROC_QCD012_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD012_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDALRAMTIME_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 알람시간 저장, 수정, 삭제
        public bool PROC_QCDALRAMTIME_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDALRAMTIME_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDALRAMUSE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 경광등 사용설정 저장, 수정, 삭제
        public bool PROC_QCDALRAMUSE_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDALRAMUSE_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD103_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 부적합유형 저장, 수정, 삭제
        public bool PROC_QCD103_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD103_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0401_DACO_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD41_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인코드 저장, 수정, 삭제
        public bool PROC_QCD41_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD41_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 생산무사유 저장, 수정, 삭제
        public bool PROC_QCD101_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD101_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 공정이상기본설정

        #region 공정이상기본설정 조회
        public DataSet QCDSHEWHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDSHEWHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정이상기본설정 저장
        public bool PROC_QCDSHEWHART_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDSHEWHART_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD34A_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사기준수정이력조회 전체 갯수
        public Int32 QCD34A_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (BSIFDac dac = new BSIFDac())
            {
                resultCnt = dac.QCD34A_CNT(oParams);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD35_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD35_INS(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 기본정보 > 숙련도관리

        #region 숙련도 조회
        public DataSet QCDLEVEL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCDLEVEL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 숙련도입력
        /// <summary>
        /// 기능명 : 숙련도입력
        /// 작성자 : 
        /// 작성일 : 
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QCDLEVEL_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCDLEVEL_INS(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도수정
        /// <summary>
        /// 기능명 : 숙련도입력
        /// 작성자 : 
        /// 작성일 : 
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QCDLEVEL_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCDLEVEL_UPD(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 삭제
        public bool QCDLEVEL_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCDLEVEL_DEL(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 저장, 수정, 삭제
        public bool PROC_QCDLEVEL_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCDLEVEL_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 숙련도 이미지 수정
        public bool QCDLEVEL_IMAGE_UPD(string[] oSps, object[] oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCDLEVEL_IMAGE_UPD(oSps, oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                result = dac.WORK01_IDX(oParams);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.WORK01_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.MPPLN03V_SPC_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCE33_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 가공불량항목 조회
        public DataSet QCE33_1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCE33_1_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 가공불량항목 저장, 수정
        public bool QCE33_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCE33_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCE34_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인별가공불량항목 저장, 수정
        public bool QCE34_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.QCE34_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 기본정보 > 생산도번관리(코티드)

        #region 생산도번관리 저장, 수정, 삭제
        public bool PROC_QCD17A_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_QCD17A_BATCH(oSps, oParams, out resultMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0306_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0505_BORGWARNER_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목 콤보박스 조회
        public DataSet BSIF0505_BORGWARNER_MEAINSP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0505_BORGWARNER_MEAINSP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목 저장
        public bool BSIF0505_BORGWARNER_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.BSIF0505_BORGWARNER_UPD(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                resultCnt = dac.BSIF0303_DEL_CNT(oParams);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0303_DEL_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.SYUSR01_ANDON_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region ANDON 상세 조회
        public DataSet SYUSR01_ANDON_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.SYUSR01_ANDON_DETAIL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region ANDON 발생현황조회
        public DataSet BSIF0504_ANDON_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0504_ANDON_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 거래처관리

        #region 거래처 조회
        public DataSet PARTNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.PARTNER_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목 조회
        public DataSet QCD01_PARTNER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD01_PARTNER_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목 조회
        public DataSet QCD01_PARTNER_MERGE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.QCD01_PARTNER_MERGE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처별 품목 조회
        public DataSet BSIF0903_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BSIFDac dac = new BSIFDac())
            {
                ds = dac.BSIF0903_FOSECO_LST(oParams, out errMsg);
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

            using (BSIFDac dac = new BSIFDac())
            {
                bExecute = dac.PROC_BATCH_UPDATE(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion
        #endregion
    }
}
