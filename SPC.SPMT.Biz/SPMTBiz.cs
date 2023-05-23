using System;
using System.Collections.Generic;
using System.Data;

using SPC.SPMT.Dac;

namespace SPC.SPMT.Biz
{
    public class SPMTBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 출하검사 데이타 관리

        #region 출하검사 측정 데이타목록
        /// <summary>
        /// 기능명 : QWK03_SPMT0101_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-10-15
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <returns>DataSet</returns>
        public DataSet QWK03_SPMT0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPMTDac dac = new SPMTDac())
            {
                ds = dac.QWK03_SPMT0101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 출하검사용 마스터키 구하기
        /// <summary>
        /// 기능명 : SHP01_MASTER_GET
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>string</returns>
        public string SHP01_MASTER_GET(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (SPMTDac dac = new SPMTDac())
            {
                result = dac.SHP01_MASTER_GET(oParams);
            }

            return result;
        }
        #endregion

        #region 저장된 마스터 정보 목록
        /// <summary>
        /// 기능명 : SHP01_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet SHP01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPMTDac dac = new SPMTDac())
            {
                ds = dac.SHP01_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장된 마스터 정보 구하기
        /// <summary>
        /// 기능명 : SHP01_GET
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet SHP01_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPMTDac dac = new SPMTDac())
            {
                ds = dac.SHP01_GET(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장된 마스터 삭제처리(Flag)
        /// <summary>
        /// 기능명 : SHP01_DEL
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>DataSet</returns>
        public bool SHP01_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (SPMTDac dac = new SPMTDac())
            {
                bExecute = dac.SHP01_DEL(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 출하검사용 Data 저장
        /// <summary>
        /// 기능명 : PROC_SHIPMENT_BATCH
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-11-24
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_SHIPMENT_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (SPMTDac dac = new SPMTDac())
            {
                bExecute = dac.PROC_SHIPMENT_BATCH(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 저장된 납품 정보 목록
        /// <summary>
        /// 기능명 : SHP02_LST
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2014-12-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet SHP02_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (SPMTDac dac = new SPMTDac())
            {
                ds = dac.SHP02_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion
    }
}
