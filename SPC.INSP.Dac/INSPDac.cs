using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.INSP.Dac
{
    public class INSPDac : IDisposable
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

        #region 검사성적서생성

        #region 리포트종류목록
        /// <summary>
        /// 기능명 : 리포트종류목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet CUSTOMER_REPORT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_CUSTOMER_REPORT_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 생성 측정데이터 조회
        /// <summary>
        /// 기능명 : 검사성적서 생성 측정데이터 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK03A_INSP0101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 생성 측정데이터 조회(호세코)
        /// <summary>
        /// 기능명 : 검사성적서 생성 측정데이터 조회(호세코)
        /// 작성자 : KIMS
        /// 작성일 : 2019-06-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK03A_INSP0101_FOSECO_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0101_FOSECO_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 출력
        /// <summary>
        /// 기능명 : 검사성적서 출력
        /// 작성자 : KIMS
        /// 작성일 : 2019-06-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK03A_INSP0101_FOSECO_REPORT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0101_FOSECO_REPORT_LST", out errMsg);
            }

            return ds;
        }

        public DataSet QWK03A_INSP0101_FOSECO_REPORT_LST2(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0101_FOSECO_REPORT_LST2", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 헤더 조회
        /// <summary>
        /// 기능명 : 검사성적서 헤더 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet INS01_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INS01_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 Data 저장
        /// <summary>
        /// 기능명 : 검사성적서 Data 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_INSPECTION_BATCH(string[] oSps, object[] oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 검사성적서조회

        #region 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-20
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet INS01_INSP0201_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INS01_INSP0201_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 출력용 전체 데이터 조회
        /// <summary>
        /// 기능명 : 검사성적서 출력용 전체 데이터 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2017-03-22
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet INS_ALL_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INS_ALL_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 천일엔지니어링 검사성적서

        #region 검사성적서 출력용 전체 데이터 조회(천일엔지니어링)
        public DataSet INS_ALL_GET_CHUNIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INS_ALL_GET_CHUNIL", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 조회(천일엔지니어링)
        public DataSet INSP0201_LST_CHUNIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INSP0201_LST_CHUNIL", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 삭제(천일엔지니어링)
        public bool INSP0201_DEL_CHUNIL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_INSP0201_DEL_CHUNIL", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 검사성적서 생성 측정데이터 조회(천일엔지니어링)
        /// <summary>
        /// 기능명 : 검사성적서 생성 측정데이터 조회
        /// 작성자 : LKJ
        /// 작성일 : 2020-02-21
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet INSP0101_CHUNIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_INSP0101_CHUNIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #endregion

        #region 폴리텍 검사성적서

        #region 검사성적서 양식관리

        #region 마스터 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13M_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13M_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 개정이력 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13M_REV_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13M_REV_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 정보
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13M_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13M_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 정보 등록여부확인
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13M_CHK_EXISTS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13M_CHK_EXISTS", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13M_INS_UPD(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13M_INS_UPD", out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                {
                    pkey = spcDB.command.Parameters["@PKEY"].Value.ToString();
                }
            }

            return bExecute;
        }
        #endregion

        #region 마스터 확정
        /// <summary>
        /// 기능명 : 확정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13M_CFM(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13M_CFM", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 마스터 개정
        /// <summary>
        /// 기능명 : 개정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13M_REV(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13M_REV", out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                {
                    pkey = spcDB.command.Parameters["@PKEY"].Value.ToString();
                }
            }

            return bExecute;
        }
        #endregion

        #region 검사항목분류 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13A_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13A_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목분류 선택
        /// <summary>
        /// 기능명 : 선택
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13A_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13A_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목분류 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13A_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13A_INS_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 검사항목분류 출력순서변경
        /// <summary>
        /// 기능명 : 출력순서변경
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13A_SRT(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13A_SRT", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 검사항목분류 검사기준 조회
        /// <summary>
        /// 기능명 : 조회
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13B_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13B_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목분류 검사기준 선택
        /// <summary>
        /// 기능명 : 선택
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK13B_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13B_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사항목분류 검사기준 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13B_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13B_INS_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 검사항목분류 검사기준 출력순서변경
        /// <summary>
        /// 기능명 : 출력순서변경
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13B_SRT(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13B_SRT", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 검사성적서 생성

        #region 갯수
        public Int32 QWK03A_INSP0302_MASTER_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_QWK03A_INSP0302_MASTER_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 목록
        public DataSet QWK03A_INSP0302_MASTER_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302_MASTER_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상세목록
        public DataSet QWK03A_INSP0302_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품명,공정명(팝업)
        public DataSet QWK03A_INSP0302POP_GET_NM(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302POP_GET_NM", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상세목록(팝업)
        public DataSet QWK03A_INSP0302POP_DETAIL_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302POP_DETAIL_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 선택한 중간검사(EF) 샘플검사목록
        public DataSet QWK03A_INSP0302_SUB_GET_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302_SUB_GET_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 샘플검사(중간검사-EF)목록(팝업)
        public DataSet QWK03A_INSP0302_SUB_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK03A_INSP0302_SUB_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 검사성적서 마스터 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13IM_INS_UPD(Dictionary<string, string> oParams, out string errMsg, out string pkey)
        {
            bool bExecute = false;
            pkey = String.Empty;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13IM_INS_UPD", out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                {
                    pkey = spcDB.command.Parameters["@PKEY"].Value.ToString();
                }
            }

            return bExecute;
        }
        #endregion

        #region 검사성적서 측정정보 저장
        /// <summary>
        /// 기능명 : 저장
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13ID_INS_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13ID_INS_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #region 검사성적서

        #region 마스터 목록
        public DataSet QWK13IM_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13IM_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 정보(팝업)
        public DataSet QWK13IM_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13IM_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 상세목록(팝업)
        public DataSet QWK13ID_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13ID_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 중간검사(EF) 샘플검사목록(팝업)
        public DataSet QWK13IS_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13IS_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 인쇄용 정보
        public DataSet QWK13_REPORT_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK13_REPORT_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 마스터 확정
        /// <summary>
        /// 기능명 : 확정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2019-02-28
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary<string, string></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool QWK13IM_CFM(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK13IM_CFM", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion

        #endregion

        #region 배치
        /// <summary>
        /// PROC_BATCH
        /// </summary>
        /// <param name="oSps">List<string></param>
        /// <param name="oParams">List<object></param>
        /// <param name="errMsg">out string</param>
        /// <returns>bool</returns>
        public bool PROC_BATCH(List<string> oSps, List<object> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps.ToArray(), oParams.ToArray(), out errMsg);
            }

            return bExecute;
        }
        #endregion
    }
}
