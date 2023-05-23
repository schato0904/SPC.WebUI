using System;
using System.Collections.Generic;
using System.Data;

using SPC.ADTR.Dac;

namespace SPC.ADTR.Biz
{
    public class ADTRBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 라인모니터링 조회

        #region 라인모니터링 조회
        public DataSet QWK04A_ADTR0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0102_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수
        public Int32 QWK04A_ADTR0102_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0102_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 라인모니터링 조회(오토)
        public DataSet QWK04A_ADTR0102_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0102_LST_MST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수(오토)
        public Int32 QWK04A_ADTR0102_CNT_MST(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0102_CNT_MST(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 라인모니터링 > 불량항목 팝업

        #region 라인모니터링 > 불량항목 팝업 조회
        public DataSet QWK04A_ADTR0102_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0102_POPUP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정능력 모니터링 조회

        #region 공정능력 모니터링 조회
        // 협력사
        public DataSet QWK04A_ADTR0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0103_LST(oParams, out errMsg);
            }

            return ds;
        }
        // 마스터
        public DataSet QWK04A_ADTR0103_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0103_LST_MST(oParams, out errMsg);
            }

            return ds;
        }
        // 천일엔지니어링
        public DataSet QWK04A_ADTR0103_LST_CHUNIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0103_LST_CHUNIL(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 일일공정점검

        #region 일일공정점검 조회
        public DataSet QWK04A_ADTR0103_NSK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0103_NSK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 일일공정점검 저장
        public DataSet QWK04A_ADTR0103_NSK_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0103_NSK_INS(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 실시간 측정 모니터링(FND)
        /// <summary>
        /// 실시간 측정 모니터링(FND)
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MEASURE_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.MEASURE_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 월별모니터링 조회
        public DataSet ADTR0106_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0106_DACO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 > Data조회

        #region Data 조회
        // Data조회(천일)
        public DataSet QWK04A_ADTR0402_LST_CHUNIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0402_LST_CHUNIL(oParams, out errMsg);
            }

            return ds;
        }
        // 협력사
        public DataSet QWK04A_ADTR0402_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0402_LST(oParams, out errMsg);
            }

            return ds;
        }
        // 오토, 네오오토
        public DataSet QWK04A_ADTR0402_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0402_LST_MST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        // Data조회(천일)
        public Int32 QWK04A_ADTR0402_CNT_CHUNIL(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0402_CNT_CHUNIL(oParams);
            }

            return resultCnt;
        }
        // 협력사
        public Int32 QWK04A_ADTR0402_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0402_CNT(oParams);
            }

            return resultCnt;
        }
        // 오토, 네오오토
        public Int32 QWK04A_ADTR0402_CNT_MST(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0402_CNT_MST(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data조회 > Data수정

        #region Data 조회
        public DataSet QWK04A_ADTR0401_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0401_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK04A_ADTR0401_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0401_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region Data수정
        public bool QWK04A_ADTR0401_UPD(Dictionary<string, string> oParams)
        {
            bool resultCnt = false;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0401_UPD(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region Data삭제
        public bool QWK04A_ADTR0401_DEL(Dictionary<string, string> oParams)
        {
            bool resultCnt = false;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0401_DEL(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data조회 > Data수정이력조회

        #region Data 조회
        public DataSet QWK04A_ADTR0403_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0403_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data수정이력조회 전체 갯수
        public Int32 QWK04A_ADTR0403_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0403_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data조회 > Lot 수정

        #region LOT 조회
        public DataSet QWK03A_ADTR0404_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK03A_ADTR0404_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region LOT 전체 갯수
        public Int32 QWK03A_ADTR0404_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK03A_ADTR0404_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region LOT 상세 조회
        public DataSet QWK03A_ADTR0404_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK03A_ADTR0404_DETAIL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region LOT 수정
        public bool QWK04A_ADTR0404_UPD(Dictionary<string, string> oParams)
        {
            bool resultCnt = false;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0404_UPD(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region LOT 배치
        public bool PROC_ADTR0404_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (ADTRDac dac = new ADTRDac())
            {
                bExecute = dac.PROC_ADTR0404_BATCH(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion
        #endregion

        #region 검사그룹별 Data조회
        public DataSet ADTR0405_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0405_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region 실시간 라인 모니터링 조회

        #region 실시간 라인 모니터링 조회
        public DataSet QCDPCNM_ADTR0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QCDPCNM_ADTR0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 실시간 라인 모니터링 조회
        public DataSet ADTR0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 실시간 라인 모니터링 조회(공정)
        public DataSet ADTR0101_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0101_WORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 실시간 라인 모니터링 조회(공정그룹)
        public DataSet QCDPCWGROUP_ADTR0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QCDPCWGROUP_ADTR0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 수분측정 Data 조회
        // 협력사
        public DataSet ADTR0402_PHE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0402_PHE_LST(oParams, out errMsg);
            }

            return ds;
        }

        public DataSet QWK04A_ADTR0402_PHE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0402_PHE_LST(oParams, out errMsg);
            }

            return ds;
        }

        public Int32 QWK04A_ADTR0402_PHE_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0402_PHE_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 실시간 라인 모니터링 팝업

        #region 실시간 라인 모니터링 팝업
        public DataSet QCDPCNM_ADTR0101_POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QCDPCNM_ADTR0101_POP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 실시간 라인 모니터링 팝업(공정)
        public DataSet QCDPCNM_ADTR0101_WORK_POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QCDPCNM_ADTR0101_WORK_POP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정 그룹 반 라인
        public DataSet GETWGROUPBANLINE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.GETWGROUPBANLINE(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 실시간 라인 모니터링 팝업(공정그룹)
        public DataSet QCDPCWGROUP_ADTR0101_POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QCDPCWGROUP_ADTR0101_POP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정이상조회

        #region 공정이상조회
        public DataSet QWKWRONGREPORTGUN_ADTR0201_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWKWRONGREPORTGUN_ADTR0201_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정이상조회 전체 갯수
        public Int32 QWKWRONGREPORTGUN_ADTR0201_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKWRONGREPORTGUN_ADTR0201_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 공정이상 저장
        public bool QWKWRONGREPORTGUN_ADTR0201_UPD(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool resultCnt = false;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKWRONGREPORTGUN_ADTR0201_UPD(oSps, oParams, out resultMsg);
            }

            return resultCnt;
        }
        #endregion

        #region 공정이상조회 성신
        public DataSet QWKWRONGREPORTGUN_ADTR0201_SUNGSHIN_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWKWRONGREPORTGUN_ADTR0201_SUNGSHIN_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 이상통보 조회

        #region 이상통보 조회
        public DataSet QWKWRONGREPORT_ADTR0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWKWRONGREPORT_ADTR0202_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 이상통보 조회 전체 갯수
        public Int32 QWKWRONGREPORT_ADTR0202_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKWRONGREPORT_ADTR0202_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 이상통보 저장
        public bool QWKWRONGREPORT_ADTR0202_UPD(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool resultCnt = false;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKWRONGREPORT_ADTR0202_UPD(oSps, oParams, out resultMsg);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 공정이상조치보고서

        #region 공정이상조치보고서 조회
        public DataSet QWK03A_ADTR0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK03A_ADTR0203_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정이상조치보고서 조회 전체 갯수
        public Int32 QWK03A_ADTR0203_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK03A_ADTR0203_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 공정이상조치보고서 상세 조회
        public DataSet QWK03A_ADTR0203_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK03A_ADTR0203_DETAIL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정이상미조치현황

        #region 공정이상미조치현황 목록
        /// <summary>
        /// 기능명 : ADTR0204_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ADTR0204_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0204_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상세내역 최대 시료수
        /// <summary>
        /// 기능명 : ADTR0204_SIRYO_MAX
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>Int32</returns>
        public Int32 ADTR0204_SIRYO_MAX(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0204_SIRYO_MAX(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 상세내역 전체갯수
        /// <summary>
        /// 기능명 : ADTR0204_DETAIL_CNT
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>Int32</returns>
        public Int32 ADTR0204_DETAIL_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0204_DETAIL_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #region 상세내역 목록
        /// <summary>
        /// 기능명 : ADTR0204_DETAIL_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-17
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet ADTR0204_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0204_DETAIL_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 인수인계 조회

        #region 인수인계 조회
        public DataSet QWKTAKEOVER_ADTR0301_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWKTAKEOVER_ADTR0301_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 인수인계 조회 전체 갯수
        public Int32 QWKTAKEOVER_ADTR0301_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKTAKEOVER_ADTR0301_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 무작업 조회

        #region 무작업 조회
        public DataSet QWKNOWORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWKNOWORK_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 무작업 조회 전체 갯수
        public Int32 QWKNOWORK_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWKNOWORK_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 자주검사 횟수조회

        #region 자주검사 횟수조회
        public DataSet ADTR0501_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0501_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 자주검사조회 전체 갯수
        public Int32 ADTR0501_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0501_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 자주검사 횟수조회 FND용

        #region 자주검사 횟수조회 FND용
        public DataSet ADTR0501_LST_FND(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0501_LST_FND(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 자주검사조회 전체 갯수FND용
        public Int32 ADTR0501_CNT_FND(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0501_CNT_FND(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 자주검사 횟수조회 품목별

        #region 자주검사 횟수조회품목별
        public DataSet ADTR0502_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0502_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 자주검사조회 전체 갯수 품목별
        public Int32 ADTR0502_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0502_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region HIPIS연계 > HIPIS연계DATA조회

        #region Data 조회
        // 협력사
        public DataSet QWK04A_ADTR0601_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.QWK04A_ADTR0601_LST(oParams, out errMsg);
            }

            return ds;
        }
        
        #endregion

        #region Data조회 전체 갯수
        // 협력사
        public Int32 QWK04A_ADTR0601_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.QWK04A_ADTR0601_CNT(oParams);
            }

            return resultCnt;
        }        
        #endregion

        #endregion

        #region 가공불량집계

        #region 가공불량집계 조회
        public DataSet ADTR0205_QWK03E_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0205_QWK03E_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #endregion

        #region 가공불량Data조회 > Data조회

        #region 가공불량Data 조회
        // 협력사
        public DataSet ADTR0701_QWK03E_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0701_QWK03E_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 가공불량Data조회 전체 갯수
        // 협력사
        public Int32 ADTR0701_QWK03E_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (ADTRDac dac = new ADTRDac())
            {
                resultCnt = dac.ADTR0701_QWK03E_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 가공불량 기간별총수량 조회

        #region 가공불량 기간별총수량 조회
        // 협력사
        public DataSet ADTR0702_QWK03E_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0702_QWK03E_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        
        #endregion

        #region 가공불량 항목별 조회

        #region 가공불량 항목별 조회
        // 협력사
        public DataSet ADTR0703_QWK03E_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0703_QWK03E_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 가공불량 Worst5 조회

        #region 가공불량 Worst5 조회
        public DataSet ADTR0704_QWK03E_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (ADTRDac dac = new ADTRDac())
            {
                ds = dac.ADTR0704_QWK03E_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
