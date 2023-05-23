using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.TOTH.Dac
{
    public class TOTHDac : IDisposable
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

        #region 치형분석 > 치형이력조회

        #region 마스터 목록 조회
        /// <summary>
        /// 기능명 : TOTH0101_1
        /// 작성자 : JNLEE
        /// 작성일 : 2015-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet TOTH0101_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TOTH0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 디테일 목록 조회
        /// <summary>
        /// 기능명 : TOTH0101_2
        /// 작성자 : JNLEE
        /// 작성일 : 2015-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet TOTH0101_2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TOTH0101_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion
        
        #region 치형분석 > 치형로그조회
        #region 치형로그조회
        /// <summary>
        /// 기능명 : TOTH0102_1
        /// 작성자 : JNLEE
        /// 작성일 : 2015-02-03
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet TOTH0102_1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_TOTH0102_LST", out errMsg);
            }

            return ds;
        }
        #endregion
        #endregion

        #region 샘플
        //#region SPC분석 > 품질종합현황

        //#region 검사규격을 구한다
        ///// <summary>
        ///// 기능명 : ANLS0101_1
        ///// 작성자 : RYU WON KYU
        ///// 작성일 : 2015-01-06
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0101_1(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0101_1", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사측정자료를 구한다
        ///// <summary>
        ///// 기능명 : ANLS0101_2
        ///// 작성자 : RYU WON KYU
        ///// 작성일 : 2015-01-07
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0101_2(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0101_2", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사히스토그램을 구한다
        ///// <summary>
        ///// 기능명 : ANLS0101_3
        ///// 작성자 : RYU WON KYU
        ///// 작성일 : 2015-01-07
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0101_3(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0101_3", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사분석자료를 구한다
        ///// <summary>
        ///// 기능명 : ANLS0101_4
        ///// 작성자 : RYU WON KYU
        ///// 작성일 : 2015-01-06
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0101_4(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0101_4", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > T/O결과등록

        //#region T/0결과등록 조회
        //public DataSet QWK105_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_QWK105_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region T/0결과등록 정보 입력
        //public bool PROC_QWK105_INS(Dictionary<string, string> oParams)
        //{
        //    bool bExecute = false;

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        bExecute = spcDB.ExecuteNonQuery("USP_QWK105_INS");
        //    }

        //    return bExecute;
        //}
        //#endregion

        //#region T/0결과등록 정보 수정
        //public bool PROC_QWK105_UPD(Dictionary<string, string> oParams)
        //{
        //    bool bExecute = false;

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        bExecute = spcDB.ExecuteNonQuery("USP_QWK105_UPD");
        //    }

        //    return bExecute;
        //}
        //#endregion

        //#region T/0결과등록 정보 삭제
        //public bool PROC_QWK105_DEL(Dictionary<string, string> oParams)
        //{
        //    bool bExecute = false;

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        bExecute = spcDB.ExecuteNonQuery("USP_QWK105_DEL");
        //    }

        //    return bExecute;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > T/O비교분석

        //#region T/O Data
        //public DataSet ANLS0302_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_QWK03A_ANLS0302_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 품질Data 조회
        //public DataSet ANLS0302_1_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_QWK03A_ANLS0302_1_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > 항목별 비교분석

        //#region 분석결과
        //public DataSet ANLS0207_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0207", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > XBar-R추이도

        //#region 검사규격을 구한다
        ///// <summary>
        ///// 기능명 : ANLS0103_1
        ///// 작성자 : KIM S
        ///// 작성일 : 2015-01-06
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0103_1(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0103_1", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사측정자료를 구한다
        ///// <summary>
        ///// 기능명 : ANLS0103_2
        ///// 작성자 : KIM S
        ///// 작성일 : 2015-01-07
        ///// 수정일 : 
        ///// 설  명 :
        ///// </summary>
        ///// <param name="oParams">Dictionary</param>
        ///// <param name="errMsg">out string</param>
        ///// <returns>DataSet</returns>
        //public DataSet ANLS0103_2(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0103_2", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > 공정능력평가

        //#region CHART DATA 조회
        //public DataSet ANLS0201_CHART(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0201_CHART_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사분석자료 조회
        //public DataSet ANLS0201_ANAL(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0201_ANAL_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 공정능력평가
        //public DataSet QCD14_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_QCD14_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > 공정능력추이도

        //#region 검사항목리스트조회
        //public DataSet ANLS0202_WORK_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0202_WORK_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 검사분석자료 조회
        //public DataSet ANLS0202_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ALNS0202_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > 종합산포도

        //#region 종합산포도
        //public DataSet ANLS0401_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0401_CHART_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion
        //#endregion

        //#region SPC분석 > 비교차이분석

        //#region 측정데이타목록, 종합데이타
        //public DataSet ANLS0204_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0204_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 비교차이분석 막대그래프
        //public DataSet ANLS0204_CHART_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0204_CHART_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion

        //#region SPC분석 > 4M변경분석

        //#region 4M변경분석 그리드,파이차트 조회
        //public DataSet ANLS0206_LST(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0206_LST", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#region 4M변경분석 차트조회
        //public DataSet ANLS0206_CHART(Dictionary<string, string> oParams, out string errMsg)
        //{
        //    DataSet ds = new DataSet();

        //    using (spcDB = new DBHelper())
        //    {
        //        spcDB.AddParameter(oParams);
        //        ds = spcDB.GetDataSet("USP_ANLS0206_CHART", out errMsg);
        //    }

        //    return ds;
        //}
        //#endregion

        //#endregion 
        #endregion
    }
}
