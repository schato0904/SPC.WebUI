using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.SPCM.Dac
{
    public class SPCMDac : IDisposable
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

        #region 기본정보

        #region 창고조회
        public DataSet SPB01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처조회
        public DataSet SPB02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB02_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 출고유형조회
        public DataSet SPB03_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB03_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 입고유형조회
        public DataSet SPB06_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB06_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 입출고유형조회
        public DataSet SPB06A_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB06A_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 예비품목분류조회
        public DataSet SPB04_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB04_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 예비품목조회
        public DataSet SPB05_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPB05_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region MAX입고번호조회
        public DataSet MAXIPNO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM01_MAXIPNO", out errMsg);
            }

            return ds;
        }
        #endregion

        #region MAX출고번호조회
        public DataSet MAXOPNO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM03_MAXOPNO", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정, 삭제 (out)
        public bool PROC_SPB01_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParams = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion

        #region 저장, 수정, 삭제
        public bool PROC_SPB01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

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

        #region 재고관리

        #region 기초재고조회

        public DataSet SPCM0201_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM01_LST", out errMsg);
            }

            return ds;
        }

        // 기초재고 디테일 조회
        public DataSet SPCM0201D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM08D_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입고마스터조회

        public DataSet SPCM0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM01D_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입고디테일조회

        public DataSet SPCM0202D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM01_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 출고마스터조회

        public DataSet SPCM0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM04_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 출고디테일조회

        public DataSet SPCM0203D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM03_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 실사디테일조회

        public DataSet SPCM0205D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM05D_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입출고이력조회

        public DataSet SPCM0206_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM06_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 월수불현황

        public DataSet SPCM0301_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM07_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 일수불현황

        public DataSet SPCM0304D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM07D_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region 현재고현황조회

        public DataSet SPCM0302D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SPM05N_LST", out errMsg);
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
                ds = spcDB.GetDataSet("USP_SPCM_DEVTREE_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
