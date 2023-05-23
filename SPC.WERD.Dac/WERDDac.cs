using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;


namespace SPC.WERD.Dac
{
    /// <summary>
    /// 기능명 : UserDac
    /// 작성자 : RYU WON KYU
    /// 작성일 : 2014-09-27
    /// 수정일 : 
    /// 설  명 : 사용자 Database 처리용 함수 모음
    /// </summary>
    public class WERDDac : IDisposable
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

        #region CRUD
        #region CRUD
        public bool PROC_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #region 공정불량 > 기본정보관리

        #region 공정별부적합원인 리스트조회
        public DataSet QCD7401_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD7401_LST", out errMsg);
            }

            return ds;
        }


        //////////////////////////////////////////////////////////////////////
        public bool QCD7401_LST(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }


        public bool PROC_BATCH(string oSp, object[] oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery3(oSp, oParams);
            }

            return bExecute;
        }


        //////////////////////////////////////////////////////////////////////// 추가






        /// <summary>
        /// 
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        #endregion

        #region 공정별손실금액 조회
        public DataSet QCD7402_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD7402_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별손실금액 조회
        public DataSet QCD7401_WERD0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD7401_WERD0103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별손실금액 조회
        public DataSet QCD7402_MERGE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD7402_MERGE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정불량등록 리스트
        public DataSet QWK110_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0103_QWK110_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질목표등록
        public DataSet WERD0107_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0107_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처 조회(대륙)
        public DataSet WERD0108_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0108_DACO_LST", out errMsg);
            }
            return ds;
        }
        #endregion

        #region 거래처 품번 조회(대륙)

        #region 품번 조회
        public DataSet WERD0109_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0109_DACO_LST", out errMsg);
            }
            return ds;
        }
        #endregion

        #region 팝업 조회
        public DataSet WERD0109_DACO_POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0109_DACO_POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팝업 거래처 조회
        public DataSet WERD0109_DACO_POP_COMP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0109_DACO_POP_COMP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 팝업 저장, 수정, 삭제
        public bool PROC_WERD0109_DACO_POP_BATCH(string[] oSps, object[] oParams, out string resultMsg)
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

        #endregion

        #region 공정불량 > 일보관리
        #region 공정검사일보등록
        public DataSet WERD0201_QWK110_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0201_QWK110_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정검사일보상세조회
        public DataSet WERD0201_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0201_QWK110_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정검사일보조회
        public DataSet WERD0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0202_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정검사일보조회 전체 갯수
        public Int32 WERD0202_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0202_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 품목별공정검사현황
        public DataSet WERD0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0203_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별공정검사현황 전체 갯수
        public Int32 WERD0203_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0203_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 모델별공정검사현황
        public DataSet WERD0204_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0204_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모델별공정검사현황 전체 갯수
        public Int32 WERD0204_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0204_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 공정별공정검사현황
        public DataSet WERD0205_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0205_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별공정검사현황 전체 갯수
        public Int32 WERD0205_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0205_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 작업자별공정검사현황
        public DataSet WERD0206_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0206_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업자별공정검사현황 전체 갯수
        public Int32 WERD0206_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0206_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 공정별부적합현황
        public DataSet WERD0207_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0207_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별부적합현황 전체 갯수
        public Int32 WERD0207_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0207_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 불량유형조회
        public DataSet NGCAUSE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_NGCAUSE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별불량유형조회
        public DataSet NGTYPE_WORKCD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_NGTYPE_WORKCD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 불량원인조회
        public DataSet NGTYPE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_NGTYPE_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 공정불량 > 집계분석
        #region 공정별 부적합 집계현황
        public DataSet WERD0301_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0301_WORK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별 부적합 집계현황 전체 갯수
        public Int32 WERD0301_WORK_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0301_WORK_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 공정별 부적합 집계현황(부적합수량집계)
        public DataSet WERD0301_NG_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0301_NG_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별 부적합 집계현황(부적합수량집계)
        public Int32 WERD0301_NG_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0301_NG_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 품목별부적합집계현황
        public DataSet WERD0302_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0302_WORK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별부적합집계현황 전체 갯수
        public Int32 WERD0302_WORK_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0302_WORK_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 품목별부적합집계현황(부적합수량집계)
        public DataSet WERD0302_NG_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0302_NG_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별부적합집계현황(부적합수량집계)
        public Int32 WERD0302_NG_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0302_NG_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 모델부적합집계현황
        public DataSet WERD0303_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0303_WORK_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모델부적합집계현황 전체 갯수
        public Int32 WERD0303_WORK_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0303_WORK_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 모델부적합집계현황(부적합수량집계)
        public DataSet WERD0303_NG_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0303_NG_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모델부적합집계현황(부적합수량집계)
        public Int32 WERD0303_NG_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0303_NG_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 작업자별집계현황
        public DataSet WERD0308_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0308_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업자별집계현황 전체 갯수
        public Int32 WERD0308_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD0308_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 년품목별부적합집계
        public DataSet WERD0309_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0309_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 년모델/품목별부적합집계
        public DataSet WERD0310_MODEL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0310_MODEL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 년모델/품목별부적합집계
        public DataSet WERD0310_ITEM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0310_ITEM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 년공정/품목별부적합집계
        public DataSet WERD0311_MODEL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0311_MODEL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 년공정/품목별부적합집계
        public DataSet WERD0311_ITEM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0311_ITEM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 년부적합유형/원인집계
        public DataSet WERD0312_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD0312_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 공정불량 > 그래프분석

        #region 일부적합추이도
        public DataSet WERD4001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4001_LST", out errMsg);
            }

            return ds;
        }
        public DataSet WERD4001_TYPE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4001_TYPE_LST", out errMsg);
            }

            return ds;
        }
        public DataSet WERD4001_LST_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4001_LST_1", out errMsg);
            }

            return ds;
        }
        public DataSet WERD4001_LST_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4001_LST_2", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 월공정별품질추이도
        public DataSet WERD4002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4002_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 부적합유형/원인파레트도
        public DataSet WERD4003_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4003_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정별부적합현황
        public DataSet WERD4004_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4004_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 부적합개선추이도
        public DataSet WERD4005_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD4005_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 유형/원인 리스트
        public DataSet WERD_SYCOD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_SYCOD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정불량(대륙)

        #region Data 조회
        public DataSet WERD_DACO1001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO1001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 집계 조회
        public DataSet WERD_DACO1002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO1002_LST", out errMsg);
            }

            return ds;
        }

        public DataSet WERD_DACO1002POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO1002POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 WERD_DACO1001_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD_DACO1001_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 WERD_DACO1002_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD_DACO1002_CNT"));
            }

            return resultCnt;
        }
        #endregion

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

        #region 품목별Worst분석 PIE CHART
        public DataSet QWK03A_WSTA0101_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_WSTA0101_PIECHART_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별WORST(대륙)

        #region MasterTable
        public DataSet WERD_DACO2001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO2001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region DetailTable
        public DataSet WERD_DACO2001_PIECHART_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO2001_PIECHART_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 업체별WORST

        #region MasterTable
        public DataSet WERD_DACO2002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO2002_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region DetailTable
        public DataSet WERD_DACO2002_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO2002_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

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

        #region 기간별 조회
        public DataSet WERD_DACO3001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO3001_LST", out errMsg);
            }

            return ds;
        }
        public Int32 WERD_DACO3001_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_WERD_DACO3001_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 협력사 집계
        public DataSet WERD_DACO4001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO4001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 연별 불량집계
        public DataSet WERD_DACO5001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO5001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 협력사별 불량집계
        public DataSet WERD_DACO6001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO6001_LST", out errMsg);
            }

            return ds;
        }

        public DataSet WERD_DACO6001_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD_DACO6001_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 결재권한관리
        public DataSet WERD5001_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD5001_DACO_LST", out errMsg);
            }
            return ds;
        }

        public bool WERD5001_DACO_PROC(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 체크시트 결재
        public DataSet WERD5002_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD5002_DACO_LST", out errMsg);
            }
            return ds;
        }

        public DataSet WERD5002_DACO_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_WERD5002_DACO_DETAIL_LST", out errMsg);
            }
            return ds;
        }

        public bool WERD5002_DACO_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_WERD5002_DACO_UPD");
            }

            return bExecute;
        }
        #endregion

        #endregion
    }
}
