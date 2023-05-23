using System;
using System.Collections.Generic;
using System.Data;

using SPC.WKSD.Dac;
namespace SPC.WKSD.Biz
{
    public class WKSDBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 작업표준서 > 작업표준서 등록

        #region 작업표준서 정보 조회
        public DataSet DWK01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WKSDDac dac = new WKSDDac())
            {
                ds = dac.DWK01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 정보 입력
        public bool PROC_DWK01_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (WKSDDac dac = new WKSDDac())
            {
                bExecute = dac.PROC_DWK01_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 작업표준서 정보 수정
        public bool PROC_DWK01_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (WKSDDac dac = new WKSDDac())
            {
                bExecute = dac.PROC_DWK01_UPD(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 작업표준서 정보 삭제
        public bool PROC_DWK01_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (WKSDDac dac = new WKSDDac())
            {
                bExecute = dac.PROC_DWK01_DEL(oParams);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 작업표준서 > 작업표준서 개정이력 조회

        #region 작업표준서개정이력 조회
        public DataSet DWK01_HIST_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WKSDDac dac = new WKSDDac())
            {
                ds = dac.DWK01_HIST_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 개정이력 전체 갯수
        public Int32 DWK01_HIST_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (WKSDDac dac = new WKSDDac())
            {
                resultCnt = dac.DWK01_HIST_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

        #region 작업표준서 > 작업표준서 미등록현황 조회

        #region 작업표준서 미등록 현황 조회
        public DataSet DWK01_UNREGISTERED_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (WKSDDac dac = new WKSDDac())
            {
                ds = dac.DWK01_UNREGISTERED_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 작업표준서 미등록현황 전체 갯수
        public Int32 DWK01_UNREGISTERED_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (WKSDDac dac = new WKSDDac())
            {
                resultCnt = dac.DWK01_UNREGISTERED_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion

        #endregion

    }
}
