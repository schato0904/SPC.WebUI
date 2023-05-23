using System;
using System.Collections.Generic;
using System.Data;
using SPC.FDCK.Dac;

namespace SPC.FDCK.Biz
{
    /// <summary>
    /// 설비일상점검
    /// </summary>
    public class FDCKBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region FDCK0101 - 설비등록

        #region Data 조회
        public DataSet QCD_MACH01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        #region Data 등록
        public bool QCD_MACH01_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH01_INS(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion
        #region Data 수정
        public bool QCD_MACH01_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH01_UPD(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion
        #region Data 삭제
        public bool QCD_MACH01_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH01_DEL(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion
        #endregion

        #region FDCKUSERPOP - 사용자 선택 트리 팝업
        public DataSet SYUSR01_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.SYUSR01_LST1(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region FDCK0102 - 설비점검항목관리

        public DataSet QCD_MACH10_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH10_LST(oParams, out errMsg);
            }

            return ds;
        }
        
        #endregion

        #region FDCK0103 - 점검타입관리

        public DataSet QCD_MACH03_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH03_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0104 - 설비별타입

        public DataSet QCD_MACH04_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH04_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0105 - 설비별 점검항목 기준관리

        public DataSet GetMACH11_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.GetMACH11_LST(oParams, out errMsg);
            }

            return ds;
        }

        #region Data 등록
        public bool QCD_MACH11_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH11_INS(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion
        #region Data 수정
        public bool QCD_MACH11_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH11_UPD(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion
        #region Data 삭제
        public bool QCD_MACH11_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QCD_MACH11_DEL(oParams, out errMsg);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region FDCK0106 - 점검항목이력관리

        public DataSet QCD_MACH13_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH13_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0107 - 설비점검 관리자 확인

        //관리자 불러오기
        public DataSet QWK_MACH10_GD1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH10_GD1_LST(oParams, out errMsg);
            }

            return ds;
        }
        //저장
        public bool QWK_MACH10_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            errMsg = string.Empty;
            bool resultCnt = false;

            using (FDCKDac dac = new FDCKDac())
            {
                resultCnt = dac.QWK_MACH10_INS(oParams, out errMsg);
            }

            return resultCnt;
        }

        //일별시트 불러오기
        public DataSet QWK_MACH10_GD2_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH10_GD2_LST(oParams, out errMsg);
            }

            return ds;
        }

        //분기별시트 불러오기
        public DataSet QWK_MACH10_GD3_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH10_GD3_LST(oParams, out errMsg);
            }

            return ds;
        }

        //문제점 불러오기
        public DataSet QWK_MACH10_GD4_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH10_GD4_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion
        
        #region FDCK0110 - 전체 설비점검 현황
        //점검현황 조회
        public DataSet QWK_MACH10_2_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH10_2_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region FDCK_BATCH공통

        public bool PROC_QCD_MACH_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.PROC_QCD_MACH_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }

        #endregion

        #region 설비일상점검(신규)

        #region 공통

        #region MultiExecute
        /// <summary>
        /// PROC_QCD_MACH_MULTI
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool PROC_QCD_MACH_MULTI(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.PROC_QCD_MACH_MULTI(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region MultiExecute
        /// <summary>
        /// PROC_QCD_MACH_MULTI
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool PROC_QCD_MACH_MULTI(string[] oSps, object[] oParams, out List<object> oOutParamList, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.PROC_QCD_MACH_MULTI(oSps, oParams, out oOutParamList, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH21_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH21_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_INS(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH21_INS(oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH21_UPD(oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_DEL(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH21_DEL(oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비담당자관리

        #region 사용자조회
        /// <summary>
        /// 기능명 : 사용자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH_USR_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH_USR_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 담당자조회
        /// <summary>
        /// 기능명 : 담당자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH22_LST(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (FDCKDac dac = new FDCKDac())
            {
                result = dac.QCD_MACH22_LST(oParams);
            }

            return result;
        }
        #endregion

        #region 확인자조회
        /// <summary>
        /// 기능명 : 확인자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH23_LST(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (FDCKDac dac = new FDCKDac())
            {
                result = dac.QCD_MACH23_LST(oParams);
            }

            return result;
        }
        #endregion

        #endregion

        #region 공장레이아웃관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH28_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH28_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH28_INS_UPD(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH28_INS_UPD(oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검기준관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH26_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(뷰)
        /// <summary>
        /// 기능명 : 조회(뷰)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH26_GET(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_INS(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH26_INS(oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 수정모드(삭제, 리비전, 일반수정)
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public string QCD_MACH26_CHK(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (FDCKDac dac = new FDCKDac())
            {
                result = dac.QCD_MACH26_CHK(oParams);
            }

            return result;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH26_UPD(oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 리비전
        /// <summary>
        /// 기능명 : 리비전
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_REV(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH26_REV(oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_DEL(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QCD_MACH26_DEL(oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검기준조회

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH26_LST2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비점검기준이력조회

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH26_LST3(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 점검등록

        #region 조회(사용자 별 대상설비)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH21_LST_BY_USR(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH21_LST_BY_USR(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(점검대상)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH23_MACH26_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH23_MACH26_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(트렌드)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH23_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 점검항목별트렌드조회

        #region 조회(점검항목)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QCD_MACH26_LST4(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비이상조치관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH24_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QWK_MACH24_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QWK_MACH24_UPD(oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검시트조회

        #region 관리자여부체크
        /// <summary>
        /// 기능명 : 관리자여부체크
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH23_CHK(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (FDCKDac dac = new FDCKDac())
            {
                result = dac.QCD_MACH23_CHK(oParams);
            }

            return result;
        }
        #endregion

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH23_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_MONITOR_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH23_MONITOR_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(청명)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_LST_CM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH23_LST_CM(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비이상조치조회
        /// <summary>
        /// 기능명 : 설비이상조치조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH24_LST2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비이상조치조회(청명)
        /// <summary>
        /// 기능명 : 설비이상조치조회(청명)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST2_CM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH24_LST2_CM(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 일별체크정보조회
        /// <summary>
        /// 기능명 : 일별체크정보조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH22_CFM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH22_CFM_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택일체크정보조회
        /// <summary>
        /// 기능명 : 선택일체크정보조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH22_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (FDCKDac dac = new FDCKDac())
            {
                ds = dac.QWK_MACH22_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택일 관리자확인
        /// <summary>
        /// 기능명 : 선택일 관리자확인
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QWK_MACH22_INS_UPD(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (FDCKDac dac = new FDCKDac())
            {
                bExecute = dac.QWK_MACH22_INS_UPD(oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion
    }
}
