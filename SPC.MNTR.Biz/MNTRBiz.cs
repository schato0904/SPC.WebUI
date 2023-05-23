using System;
using System.Collections.Generic;
using System.Data;

using SPC.MNTR.Dac;

namespace SPC.MNTR.Biz
{
    public class MNTRBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 모니터링 > 생산량조회
        /// <summary>
        /// 기능명 : MONITORING_PRODUCTION_VOLUMN
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-12
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_PRODUCTION_VOLUMN(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_PRODUCTION_VOLUMN(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 공정능력
        /// <summary>
        /// 기능명 : MONITORING_PROCESS_CAPABILITY
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-12
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_PROCESS_CAPABILITY(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_PROCESS_CAPABILITY(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 반별 생산수
        /// <summary>
        /// 기능명 : MONITORING_PROCESS_ANALISYS
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-12
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_PROCESS_ANALISYS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_PROCESS_ANALISYS(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 반별 생산수 : 업체 품목, 공정별 WORST3
        /// <summary>
        /// 기능명 : MONITORING_PROCESS_ANALISYS_WORST3
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-01-12
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_PROCESS_ANALISYS_WORST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_PROCESS_ANALISYS_WORST3(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 협력사 별 Worst

        #region 업체별 Worst
        /// <summary>
        /// 기능명 : MONITORING_MNTR0904_COMP
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0904_COMP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0904_COMP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품목별 Worst
        /// <summary>
        /// 기능명 : MONITORING_MNTR0904_ITEM
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0904_ITEM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0904_ITEM(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 항목별 Worst
        /// <summary>
        /// 기능명 : MONITORING_MNTR0904_INSP
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0904_INSP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0904_INSP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 모니터링 > 협력사 전수설비 라인 현황판
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MNTR0906_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MNTR0906_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion
        
        #region 모니터링 > 공정능력(협력사별, 전체)
        /// <summary>
        /// 기능명 : 공정능력(협력사별, 전체)
        /// 작성자 : JNLEE
        /// 작성일 : 2015-02-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MNTR0903_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MNTR0903_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 모니터링 > 협력사 SPC 현황

        #region 협력사 SPC 현황
        /// <summary>
        /// 기능명 : MONITORING_MNTR0901
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-02-13
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0901(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0901(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 모니터링 > 협력바 주간 공정품질 추이도

        #region 공정불량현황

        #region 추이 Worst5(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_1_5WORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_1_5WORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_1_5WORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 추이 전 협력사(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_1_TWORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_1_TWORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_1_TWORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 공정능력미달

        #region 추이 Worst5(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_2_5WORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_2_5WORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_2_5WORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 추이 전 협력사(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_2_TWORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_2_TWORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_2_TWORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 데이터수신현황

        #region 추이 Worst5(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_3_5WORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_3_5WORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_3_5WORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 추이 전 협력사(For Chart)
        /// <summary>
        /// 기능명 : MONITORING_MNTR0907_3_TWORST_CHART
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-04-01
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet MONITORING_MNTR0907_3_TWORST_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (MNTRDac dac = new MNTRDac())
            {
                ds = dac.MONITORING_MNTR0907_3_TWORST_CHART(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #endregion
    }
}
