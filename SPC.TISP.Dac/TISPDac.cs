using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;


namespace SPC.TISP.Dac
{
    public class TISPDac : IDisposable
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

        #region 모니터링 > 설비별공지사항

        #region 설비별공지사항조회
        public DataSet TISP0001_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0001_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비별공지사항 저장
        public bool TISP0001_UPD(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 설비로그조회
        public DataSet TISP0002_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0002_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 모니터링 > 라인모니터링 조회

        #region 라인모니터링 조회
        public DataSet QWK09A_TISP0103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수
        public Int32 QWK09A_TISP0103_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0103_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 라인모니터링 조회
        public DataSet QWK09A_TISP0103_LST_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0103_LST_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수
        public Int32 QWK09A_TISP0103_CNT_NSUMDATE(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0103_CNT_NSUMDATE"));
            }

            return resultCnt;
        }
        #endregion

        #region 라인모니터링 조회(오토)
        public DataSet QWK09A_TISP0103_LST_MST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0103_LST_MST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수(오토)
        public Int32 QWK09A_TISP0103_CNT_MST(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0103_CNT_MST"));
            }

            return resultCnt;
        }
        #endregion

        #region 라인모니터링 조회(오토)
        public DataSet QWK09A_TISP0103_LST_MST_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0103_LST_MST_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 전체 갯수(오토)
        public Int32 QWK09A_TISP0103_CNT_MST_NSUMDATE(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0103_CNT_MST_NSUMDATE"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 모니터링 > 라인모니터링 > 불량항목팝업

        #region 라인모니터링 > 불량항목 팝업 조회
        public DataSet QWK09A_TISP0103_POPUP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0103_POPUP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 라인모니터링 > 불량항목 팝업 전체 갯수
        public Int32 QWK09A_TISP0103_POPUP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0103_POPUP_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion        

        #region 모니터링 > 설비모니터링 조회

        #region 설비모니터링 조회
        public DataSet QWK08A_TISP0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_LST", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0101_LST_FOR_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_LST_FOR_NSUMDATE", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0101_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_DACO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비모니터링 부적합 팝업
        public Int32 QWK08A_TISP0101_POP_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08A_TISP0101_POP_CNT"));
            }

            return resultCnt;
        }
        public DataSet QWK08A_TISP0101_POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비모니터링 CHART조회
        public DataSet QWK08A_TISP0101_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_CHART", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0101_CHART_FOR_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0101_CHART_FOR_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data조회 > 설비별불량집계 조회

        #region 설비별불량집계 조회
        public DataSet QWK08A_TISP0301_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0301_LST", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0301_LST_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0301_LST_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비별불량집계 상세 조회
        public Int32 QWK08A_TISP0301_DETAIL_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08A_TISP0301_DETAIL_CNT"));
            }

            return resultCnt;
        }
        public DataSet QWK08A_TISP0301_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0301_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data조회 > 공정이상조회

        #region 공정이상조회
        public DataSet MACH_WRONGGUN_TISP0302_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_MACH_WRONGGUN_TISP0302_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정이상조회 전체 갯수
        public Int32 MACH_WRONGGUN_TISP0302_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_MACH_WRONGGUN_TISP0302_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 공정이상 저장
        public bool MACH_WRONGGUN_TISP0302_UPD(string[] oSps, object[] oParams, out string resultMsg)
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

        #region Data조회 > Data 조회

        #region Data 조회
        public DataSet QWK08A_TISP0303_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0303_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK08A_TISP0303_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08A_TISP0303_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data 조회 > Data수정

        #region Data 조회
        public DataSet QWK08A_TISP0304_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK09A_TISP0304_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK08A_TISP0304_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK09A_TISP0304_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region Data수정
        public bool QWK08A_TISP0304_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                spcDB.bUsedTrans = false;
                bExecute = spcDB.ExecuteNonQuery("USP_TISP0304_UPD");
            }

            return bExecute;
        }
        #endregion

        #region Data삭제
        public bool QWK08A_TISP0304_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_TISP0304_DEL");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region Data조회 > Data 수정이력조회

        #region Data 조회
        public DataSet QWK10A_TISP0305_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK10A_TISP0305_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data조회 전체 갯수
        public Int32 QWK10A_TISP0305_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK10A_TISP0305_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data조회 > Data삭제 조회

        #region Data삭제 조회
        public DataSet QWK08A_TISP0501_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0501_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data삭제 조회
        public Int32 QWK08A_TISP0501_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08A_TISP0501_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region Data삭제 조회
        public DataSet QWK08A_TISP0501_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0501_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data삭제
        public bool QWK08A_TISP0501_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                spcDB.bUsedTrans = false;
                bExecute = spcDB.ExecuteNonQuery("USP_TISP0501_DEL");
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region Data조회 > Data삭제이력조회

        #region Data삭제이력조회
        public DataSet QWK08A_TISP0502_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0502_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region Data삭제이력조회
        public DataSet QWK08A_TISP0502_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0502_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data조회 > DATA조회(QR)

        #region 조회
        public DataSet TISP0306_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0306_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회CNT
        public Int32 TISP0306_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_TISP0306_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 수율분석 > 주별분석
        #region 주별분석
        public DataSet QWK03A_TISP0401_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_TISP0401_LST", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0401_LST_FOR_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0401_LST_FOR_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 수율분석 > 월별분석
        #region 월별분석
        public DataSet QWK03A_TISP0402_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_TISP0402_LST", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0402_LST_FOR_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0402_LST_FOR_NSUMDATE", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion        

        #region Data분석 > 공정능력모니터링 조회

        #region 공정능력모니터링 조회
        public DataSet QWK08A_TISP0102_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0102_LST", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0102_LST_FOR_NSUMDATE(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0102_LST_FOR_NSUMDATE", out errMsg);
            }

            return ds;
        }
        public DataSet QWK08A_TISP0102_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0102_DACO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data분석 > X-Rs관리도
        #region X-Rs관리도
        public DataSet TISP0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0104_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        
        #endregion

        #region Data분석 > X-Rs관리도(대륙)
        public DataSet TISP0104_DACO_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0104_DACO_WORK_LST", out errMsg);
            }

            return ds;
        }

        public DataSet TISP0104_DACO_INSP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0104_DACO_INSP_LST", out errMsg);
            }

            return ds;
        }

        public DataSet TISP0104_DACO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0104_DACO_LST", out errMsg);
            }

            return ds;
        }

        public DataSet TISP0104_DACO_PIECE_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0104_DACO_PIECE_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region Data분석 > 공정능력평가
        #region 공정능력평가 분석
        public DataSet TISP0105_ANAL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_ANAL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 공정능력평가 차트
        public DataSet TISP0105_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_CHART_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region Data분석 > 공정능력평가(대륙)
        public DataSet TISP0105_ANAL_DACO(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_ANAL_LST_DACO", out errMsg);
            }

            return ds;
        }

        public DataSet TISP0105_CHART_DACO(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_CHART_LST_DACO", out errMsg);
            }

            return ds;
        }

        public DataSet TISP01POP_4_DACO(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_4_DACO", out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region 품질종합현황 팝업

        #region 검사규격을 구한다
        public DataSet TISP01POP_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP01POP_1", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사측정자료를 구한다
        public DataSet TISP01POP_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP01POP_2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사히스토그램을 구한다
        public DataSet TISP01POP_3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP01POP_3", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사분석자료를 구한다
        public DataSet TISP01POP_4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TISP0105_4", out errMsg);
            }

            return ds;
        }
        #endregion

        #region X차트를 구한다
        public DataSet QWK08A_TISP01POP_NEW_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP01POP_NEW_CHART", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data분석 > 품목별 WORST
        #region 품목별 WORST
        public DataSet TISP0107_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0107_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data분석 > 항목별 WORST
        #region 항목별 WORST
        public DataSet TISP0108_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0108_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data분석 > 기종별 WORST
        #region 기종별 WORST
        public DataSet TISP0109_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0109_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region Data분석 > 일자별전수현황

        #region 일자별전수현황 조회
        public DataSet QWK08A_TISP0106_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0106_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 일자별전수현황 전체 갯수
        public Int32 QWK08A_TISP0106_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK08A_TISP0106_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region Data분석 > P관리도

        #region P관리도
        public DataSet QWK08A_TISP0106_LST_FOR_NSUMHOUR(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK08A_TISP0106_LST_FOR_NSUMHOUR", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
