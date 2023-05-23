using System;
using System.Collections.Generic;
using System.Data;

using SPC.IPCM.Dac;

namespace SPC.IPCM.Biz
{
    public class IPCMBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion

        #region 개선요청 > 문제이상제기

        #region 문제이상제기
        public DataSet QWK101_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK101_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상대책 팝업 조회
        public DataSet QWK101POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK101POP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 팝업 조회
        public DataSet QWK103POP_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK103POP_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 품질이상제기 팝업 조회
        public DataSet QWK101POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK101POP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인 품질이상대책 팝업 조회
        public DataSet QWK103POP(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK103POP(oParams, out errMsg);
            }

            return ds;
        }
        #endregion


        #region 품질이상대책  조회
        public DataSet QWK103_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK103_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 품질이상확인  조회
        public DataSet QWK104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK104_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion


        #region 품질이상제기 입력
        public bool PROC_QWK101_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK101_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상제기 수정
        public bool PROC_QWK101_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK101_UPD(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 입력
        public bool PROC_QWK103_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK103_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 입력
        public bool PROC_QWK104_INS(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK104_INS(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상대책 수정
        public bool PROC_QWK103_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK103_UPD(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 수정
        public bool PROC_QWK104_UPD(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK104_UPD(oParams);
            }

            return bExecute;
        }
        #endregion


        #region 품질이상제기 삭제
        public bool PROC_QWK101_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK101_DEL(oParams);
            }

            return bExecute;
        }
        #endregion


        #region 품질이상대책 삭제
        public bool PROC_QWK103_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK103_DEL(oParams);
            }

            return bExecute;
        }
        #endregion

        #region 품질이상확인 삭제
        public bool PROC_QWK104_DEL(Dictionary<string, string> oParams)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.PROC_QWK104_DEL(oParams);
            }

            return bExecute;
        }
        #endregion


        #region 코드중복검사
        /// <summary>
        /// 기능명 : 코드중복검사
        /// 작성자 : 박병수
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QCM103_EXT_COMMCD(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExists = dac.PROC_QCM103_EXT_COMMCD(oParams);
            }

            return bExists;
        }
        #endregion


        #region 코드중복검사
        /// <summary>
        /// 기능명 : 코드중복검사
        /// 작성자 : 박병수
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_QWK104_EXT_COMMCD(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExists = dac.PROC_QWK104_EXT_COMMCD(oParams);
            }

            return bExists;
        }
        #endregion
        

        #endregion

        #region 개선대책 진행현황
        public DataSet IPCM0104_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.IPCM0104_LST(oParams, out errMsg);
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
            DataSet ds = null;

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK100_NOPAGING_LST(oParams, out errMsg);
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
            DataSet ds = null;

            using (IPCMDac dac = new IPCMDac())
            {
                ds = dac.QWK100_GET(oParams, out errMsg);
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

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.QWK100_STEP1_INS(oParams, out errMsg);
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

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.QWK100_STEP1_UPD(oParams, out errMsg);
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

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.QWK100_STEP1_DEL(oParams, out errMsg);
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
        /// <returns>bool</returns>
        public bool QWK100_STEP2_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.QWK100_STEP2_UPD(oParams, out errMsg);
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
        /// <returns>bool</returns>
        public bool QWK100_STEP3_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            bool bExecute = false;

            using (IPCMDac dac = new IPCMDac())
            {
                bExecute = dac.QWK100_STEP3_UPD(oParams, out errMsg);
            }

            return bExecute;
        }
        #endregion

        #endregion
    }
}
