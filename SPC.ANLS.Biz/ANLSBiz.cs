using System;
using System.Collections.Generic;
using System.Data;

using SPC.ANLS.Dac;

namespace SPC.ANLS.Biz
{
    public class ANLSBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region SPC분석 > 품질종합현황

        #region 검사규격을 구한다
        /// <summary>
        /// 기능명 : ANLS0101_1
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_1(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사측정자료를 구한다
        /// <summary>
        /// 기능명 : ANLS0101_2
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사히스토그램을 구한다
        /// <summary>
        /// 기능명 : ANLS0101_3
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_3(oParams, out errMsg);
            }

            return ds;
        }

        public DataSet ANLS0101_3_New(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_3_New(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사분석자료를 구한다
        /// <summary>
        /// 기능명 : ANLS0101_4
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_4(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별 XBar_R관리도
        /// <summary>
        /// 기능명 : USP_ANLS0102_WORK_1
        /// 작성자 : KIM
        /// 작성일 : 2020-09-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0102_WORK_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0102_WORK_1(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별 XBar-R관리도 검사측정자료를 구한다
        /// <summary>
        /// 기능명 : USP_ANLS0101_WORK_2
        /// 작성자 : KIM
        /// 작성일 : 2020-09-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_WORK_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_WORK_2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별 XBar_R관리도 공정능력
        /// <summary>
        /// 기능명 : USP_ANLS0102_WORK_1
        /// 작성자 : KIM
        /// 작성일 : 2020-09-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0101_WORK_4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_WORK_4(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > T/O결과등록

        #region T/O결과등록 정보 조회
        public DataSet QWK105_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.QWK105_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region T/O결과등록 정보 입력
        public bool PROC_QWK105_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (ANLSDac dac = new ANLSDac())
            {
                bExecute = dac.PROC_QWK105_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region T/O결과등록 정보 수정
        public bool PROC_QWK105_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (ANLSDac dac = new ANLSDac())
            {
                bExecute = dac.PROC_QWK105_UPD(oParams);
            }

            return bExecute;
        }
        #endregion

        #region T/O결과등록 정보 삭제
        public bool PROC_QWK105_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (ANLSDac dac = new ANLSDac())
            {
                bExecute = dac.PROC_QWK105_DEL(oParams);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region SPC분석 > T/O비교분석

        #region T/O Data
        public DataSet ANLS0302_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0302_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질Data
        public DataSet ANLS0302_1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0302_1_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
        
        #region SPC분석 > 항목별비교분석

        #region 분석결과 DATA
        public DataSet ANLS0207_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0207_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
        
        #region SPC분석 > XBar-R추이도

        #region 검사규격을 구한다
        /// <summary>
        /// 기능명 : ANLS0103_1
        /// 작성자 : KIM S
        /// 작성일 : 2015-01-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0103_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0103_1(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사측정자료를 구한다
        /// <summary>
        /// 기능명 : ANLS0103_2
        /// 작성자 : KIM S
        /// 작성일 : 2015-01-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ANLS0103_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0103_2(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > P관리도

        #region 조회
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet ANLS0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0104_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > 공정능력평가

        #region CHART DATA 조회
        public DataSet ANLS0201_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0201_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사분석자료 조회
        public DataSet ANLS0201_ANAL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0201_ANAL(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력평가
        public DataSet QCD14_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.QCD14_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion        

        #endregion

        #region SPC분석 > 공정능력추이도

        #region 검사항목리스트
        public DataSet ANLS0202_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0202_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사분석자료 조회
        public DataSet ANLS0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0202_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > 종합산포도

        #region 종합산포도
        public DataSet ANLS0401_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0401_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > 비교차이분석

        #region 측정데이타목록, 종합데이타
        public DataSet ANLS0204_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0204_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 비교차이분석 막대그래프
        public DataSet ANLS0204_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0204_CHART_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region SPC분석 > 4M변경분석

        #region 4M변경분석 그리드, 파이차트 조회
        public DataSet ANLS0206_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0206_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 4M변경분석 차트조회
        public DataSet ANLS0206_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0206_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 품질종합현황 검사항목별
        #region 검사항목리스트
        public DataSet ANLS0101_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목 수량
        public Int32 ANLS0101_WORK_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ANLSDac dac = new ANLSDac())
            {
                resultCnt = dac.ANLS0101_WORK_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion
        #endregion

        #region SPC분석 > 항목별XBar-R관리도
        public DataSet ANLS0101_1_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_1_BORGWARNER(oParams, out errMsg);
            }

            return ds;
        }

        public DataSet ANLS0101_2_BORGWARNER(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ANLSDac dac = new ANLSDac())
            {
                ds = dac.ANLS0101_2_BORGWARNER(oParams, out errMsg);
            }

            return ds;
        }

        #endregion
    }
}
