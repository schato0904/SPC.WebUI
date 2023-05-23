using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.FDCK.Dac
{
    /// <summary>
    /// 설비일상점검
    /// </summary>
    public class FDCKDac : IDisposable
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

        
        #region FDCK0101 - 설비등록
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH01_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH01_INS(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH01_INS", out resultMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH01_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH01_UPD", out resultMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH01_DEL(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH01_DEL", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region FDCKUSERPOP - 사용자 선택 트리 팝업
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-07
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public DataSet SYUSR01_LST1(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_SYUSR01_LST1", out errMsg);
            }

            return ds;
        }
        #endregion

        #region FDCK0102 - 설비점검항목관리

        public DataSet QCD_MACH10_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH10_LST", out errMsg);
            }

            return ds;
        }
        
        #endregion

        #region FDCK0103 - 점검타입관리

        public DataSet QCD_MACH03_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH03_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0104 - 설비별타입

        public DataSet QCD_MACH04_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH04_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0105 - 설비별 점검항목 기준관리

        //설비코드 팝업
        public DataSet GetMACH11_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH11_LST", out errMsg);
            }

            return ds;
        }

        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH11_INS(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH11_INS", out resultMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH11_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH11_UPD", out resultMsg);
            }

            return bExecute;
        }

        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : JNLEE
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH11_DEL(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QCD_MACH11_DEL", out resultMsg);
            }

            return bExecute;
        }

        #endregion

        #region FDCK0106 - 점검이력관리

        public DataSet QCD_MACH13_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QCD_MACH13_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0107 - 설비점검 관리자 확인

        //관리자 불러오기
        public DataSet QWK_MACH10_GD1_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QWK_MACH10_LST", out errMsg);
            }

            return ds;
        }


        public bool QWK_MACH10_INS(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_FD_QWK_MACH10_INS", out resultMsg);
            }

            return bExecute;
        }
        

        //일별시트 불러오기
        public DataSet QWK_MACH10_GD2_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QWK_MACH10_1_LST", out errMsg);
            }

            return ds;
        }

        //분기별시트 불러오기
        public DataSet QWK_MACH10_GD3_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QWK_MACH10_2_LST", out errMsg);
            }

            return ds;
        }
        //문제점 불러오기
        public DataSet QWK_MACH10_GD4_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QWK_MACH10_3_LST", out errMsg);
            }

            return ds;
        }

        #endregion

        #region FDCK0110 - 전체 설비점검 현황
        //점검현황 조회
        public DataSet QWK_MACH10_2_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_FD_QWK_MACH10_2_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region FDCK_BATCH공통

        public bool PROC_QCD_MACH_BATCH(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }

        #endregion

        #region 설비일상점검(신규)

        #region 공통

        #region MultiExecute
        /// <summary>
        /// PROC_QCD_MACH_MULTI
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool PROC_QCD_MACH_MULTI(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region MultiExecute
        /// <summary>
        /// PROC_QCD_MACH_MULTI
        /// </summary>
        /// <param name="oSps">string[]</param>
        /// <param name="oParams">object[]</param>
        /// <param name="resultMsg">out string</param>
        /// <returns></returns>
        public bool PROC_QCD_MACH_MULTI(string[] oSps, object[] oParams, out List<object> oOutParamList, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
                oOutParamList = spcDB.oOutParamList;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH21_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH21_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_INS(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH21_INS", out resultMsg);
                oOutParams = spcDB.oOutParamDic;
            }

            return bExecute;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH21_UPD", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH21_DEL(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH21_DEL", out resultMsg);
                oOutParams = spcDB.oOutParamDic;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비담당자관리

        #region 사용자조회
        /// <summary>
        /// 기능명 : 사용자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH_USR_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH_USR_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 담당자조회
        /// <summary>
        /// 기능명 : 담당자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH22_LST(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                result = spcDB.ExecuteScaler("USP_QCD_MACH22_LST").ToString();
            }

            return result;
        }
        #endregion

        #region 확인자조회
        /// <summary>
        /// 기능명 : 확인자조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH23_LST(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                result = spcDB.ExecuteScaler("USP_QCD_MACH23_LST").ToString();
            }

            return result;
        }
        #endregion

        #endregion

        #region 공장레이아웃관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH28_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH28_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH28_INS_UPD(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH28_INS_UPD", out resultMsg);
                oOutParams = spcDB.oOutParamDic;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검기준관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH26_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(뷰)
        /// <summary>
        /// 기능명 : 조회(뷰)
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH26_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_INS(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH26_INS", out resultMsg);
                oOutParams = spcDB.oOutParamDic;
            }

            return bExecute;
        }
        #endregion

        #region 수정모드(삭제, 리비전, 일반수정)
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public string QCD_MACH26_CHK(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                result = spcDB.ExecuteScaler("USP_QCD_MACH26_CHK").ToString();
            }

            return result;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH26_UPD", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 리비전
        /// <summary>
        /// 기능명 : 리비전
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_REV(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH26_REV", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 삭제
        /// <summary>
        /// 기능명 : 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QCD_MACH26_DEL(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QCD_MACH26_DEL", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검기준조회

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH26_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비점검기준이력조회

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST3(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH26_LST3", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 점검등록

        #region 조회(사용자 별 대상설비)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH21_LST_BY_USR(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH21_LST_BY_USR", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(점검대상)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH23_MACH26_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH23_MACH26_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(트렌드)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_CHART(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH23_CHART", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 점검항목별트렌드조회

        #region 조회(점검항목)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QCD_MACH26_LST4(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QCD_MACH26_LST4", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 설비이상조치관리

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH24_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 수정
        /// <summary>
        /// 기능명 : 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QWK_MACH24_UPD(Dictionary<string, string> oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK_MACH24_UPD", out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 설비점검시트조회

        #region 관리자여부체크
        /// <summary>
        /// 기능명 : 관리자 유무체크
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-06
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public string QCD_MACH23_CHK(Dictionary<string, string> oParams)
        {
            string result = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                result = spcDB.ExecuteScaler("USP_QCD_MACH23_CHK").ToString();
            }

            return result;
        }
        #endregion

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH23_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_MONITOR_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH23_MONITOR_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(청명)
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH23_LST_CM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH23_LST_CM", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비이상조치조회
        /// <summary>
        /// 기능명 : 설비이상조치조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH24_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 설비이상조치조회(청명)
        /// <summary>
        /// 기능명 : 설비이상조치조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH24_LST2_CM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH24_LST2_CM", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 일별체크정보조회
        /// <summary>
        /// 기능명 : 일별체크정보조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH22_CFM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH22_CFM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택일체크정보조회
        /// <summary>
        /// 기능명 : 선택일체크정보조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-09-04
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">object[]</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public DataSet QWK_MACH22_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK_MACH22_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택일 관리자확인
        /// <summary>
        /// 기능명 : 선택일 관리자확인
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-02-02
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool QWK_MACH22_INS_UPD(Dictionary<string, string> oParams, out Dictionary<string, object> oOutParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK_MACH22_INS_UPD", out resultMsg);
                oOutParams = spcDB.oOutParamDic;
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion
    }
}
