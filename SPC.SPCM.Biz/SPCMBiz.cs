using System;
using System.Collections.Generic;
using System.Data;

using SPC.SPCM.Dac;

namespace SPC.SPCM.Biz
{
    public class SPCMBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 기본정보

        #region 창고조회
        public DataSet SPB01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 거래처조회
        public DataSet SPB02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB02_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 출고유형조회
        public DataSet SPB03_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB03_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 입고유형조회
        public DataSet SPB06_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB06_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 입출고유형조회
        public DataSet SPB06A_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB06A_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 예비품목분류조회
        public DataSet SPB04_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB04_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 예비품목조회
        public DataSet SPB05_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPB05_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region MAX입고번호조회
        public DataSet MAXIPNO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.MAXIPNO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region MAX출고번호조회
        public DataSet MAXOPNO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.MAXOPNO_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장, 수정, 삭제(OUT)
        public bool PROC_SPB01_BATCH(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SPCMDac dac = new SPCMDac())
            {
                bExecute = dac.PROC_SPB01_BATCH(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 저장, 수정, 삭제
        public bool PROC_SPB01_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SPCMDac dac = new SPCMDac())
            {
                bExecute = dac.PROC_SPB01_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 배치 업데이트
        public bool PROC_BATCH_UPDATE(string[] oSps, object[] oParams, out List<object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SPCMDac dac = new SPCMDac())
            {
                bExecute = dac.PROC_BATCH_UPDATE(oSps, oParams, out oOutParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 재고관리

        #region 기초재고 조회

        public DataSet SPCM0201_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0201_LST(oParams, out errMsg);
            }

            return ds;
        }
        // 기초 디테일조회
        public DataSet SPCM0201D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0201D_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입고마스터 조회

        public DataSet SPCM0202_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0202_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입고디테일 조회

        public DataSet SPCM0202D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0202D_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 출고마스터 조회

        public DataSet SPCM0203_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0203_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 출고디테일 조회

        public DataSet SPCM0203D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0203D_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 실사디테일 조회

        public DataSet SPCM0205D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0205D_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 입출고이력 조회

        public DataSet SPCM0206_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0206_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 월수불현황 조회

        public DataSet SPCM0301_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0301_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 일수불현황 조회

        public DataSet SPCM0304D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0304D_LST(oParams, out errMsg);
            }

            return ds;
        }

        #endregion

        #region 현재고현황 조회

        public DataSet SPCM0302D_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.SPCM0302D_LST(oParams, out errMsg);
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

            using (SPCMDac dac = new SPCMDac())
            {
                ds = dac.DEVTREE_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

    }
}
