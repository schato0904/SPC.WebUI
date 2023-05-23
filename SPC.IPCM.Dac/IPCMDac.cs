using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.IPCM.Dac
{
    public class IPCMDac : IDisposable
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

        #region 개선대책 > 문제이상

        #region 품질이상제기 조회
        public DataSet QWK101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0101_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상대책 팝업 조회
        public DataSet QWK101POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0102POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 팝업 조회
        public DataSet QWK103POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0103POP_LST", out errMsg);
            }

            return ds;
        }
        #endregion


        #region 품질이상확인 품질이상제기 팝업 조회
        public DataSet QWK101POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("QWK101POP", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 품질이상대책 팝업 조회
        public DataSet QWK103POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("QWK103POP", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상대책 조회
        public DataSet QWK103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0102_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 조회
        public DataSet QWK104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0103_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상제기 정보 입력
        public bool PROC_QWK101_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0101_ins");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상제기 정보 수정
        public bool PROC_QWK101_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0101_upd");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 정보 입력
        public bool PROC_QWK103_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0102POP_INS");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 정보 입력
        public bool PROC_QWK104_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0103POP_INS");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 정보 수정
        public bool PROC_QWK103_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0102POP_UPD");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 정보 수정
        public bool PROC_QWK104_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0103POP_UPD");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상제기 삭제
        public bool PROC_QWK101_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0101_del");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 삭제
        public bool PROC_QWK103_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0102_DEL");
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 삭제
        public bool PROC_QWK104_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_IPCM0103_DEL");
            }

            return bExecute;
        }
        #endregion

        #region 프로그램중복검사
        /// <summary>
        /// 기능명 : 프로그램중복검사
        /// 작성자 : 박병수
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_QCM103_EXT_COMMCD(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_IPCM_PUBLICNO");
            }

            return bExists;
        }
        #endregion

        #region 프로그램중복검사
        /// <summary>
        /// 기능명 : 프로그램중복검사
        /// 작성자 : 박병수
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <returns>bool</returns>
        public bool PROC_QWK104_EXT_COMMCD(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_IPCM_PUBLICNO2");
            }

            return bExists;
        }
        #endregion

        #endregion

        #region 개선대책 진행현황
        public DataSet IPCM0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_IPCM0104_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 개선요청 > 품질이상제기

        #region 이상제기목록
        /// <summary>
        /// 기능명 : 이상제기목록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-09
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK100_NOPAGING_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK100_NOPAGING_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 이상제기정보
        /// <summary>
        /// 기능명 : 이상제기정보
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns>DataSet</returns>
        public DataSet QWK100_GET(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_QWK100_GET", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상제기 등록
        /// <summary>
        /// 기능명 : 품질이상제기 등록
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QWK100_STEP1_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK100_STEP1_INS", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상제기 수정
        /// <summary>
        /// 기능명 : 품질이상제기 수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QWK100_STEP1_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK100_STEP1_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상제기 삭제
        /// <summary>
        /// 기능명 : 품질이상제기 삭제
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-10
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QWK100_STEP1_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK100_STEP1_DEL", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 등록/수정
        /// <summary>
        /// 기능명 : 품질이상대책 등록/수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QWK100_STEP2_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK100_STEP2_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 완료 등록/수정
        /// <summary>
        /// 기능명 : 품질이상대책 완료 등록/수정
        /// 작성자 : RYU WON KYU
        /// 작성일 : 2015-03-11
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <param name="errMsg">out string</param>
        /// <returns></returns>
        public bool QWK100_STEP3_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper())
            {
                spcDB.AddParameter(oParams);
                bExecute = spcDB.ExecuteNonQuery("USP_QWK100_STEP3_UPD", out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion
    }
}
