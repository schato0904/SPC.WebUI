using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace SPC.BORD.Dac
{
    public class BORDDac : IDisposable
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

        #region 게시판

        #region 게시판 등록
        public bool BOARD_INS(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 게시판 조회
        public DataSet BOARD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 총갯수 조회
        public Int32 BOARD_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_BOARD_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 게시판 조회(협력사)
        public DataSet BOARD_VENDOR_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_VENDOR_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 총갯수 조회(협력사)
        public Int32 BOARD_VENDOR_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                resultCnt = Convert.ToInt32(spcDB.ExecuteScaler("USP_BOARD_VENDOR_CNT"));
            }

            return resultCnt;
        }
        #endregion

        #region 답글 등록
        public bool BOARD_REPLY_INS(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                bExecute = spcDB.MultiExecuteNonQuery2(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion



        #region 게시판 상세보기
        public DataSet BOARD_DETAIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_DETAIL", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기 체크박스
        public DataSet BOARD_DETAIL_CHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_DETAIL_CHK", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 댓글등록
        public DataSet BOARD_COMMENT_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_COMMENT_INS", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 댓글입력
        public DataSet BOARD_COMMENT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_COMMENT_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기 수정
        public DataSet BOARD_DETAIL_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_DETAIL_UPD", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기 삭제
        public DataSet BOARD_DETAIL_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_DETAIL_DEL", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 답글달기
        public DataSet BOARD_REPLY(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_REPLY", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 답글화면 상세보기
        public DataSet BOARD_REPLY_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_REPLY_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 답글삭제
        public DataSet BOARD_COMMENTCOUNT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_COMMENT_COUNT", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 메인페이지 공지사항리스트
        public DataSet BOARD_MAIN_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                ds = spcDB.GetDataSet("USP_BOARD_MAIN_LST", out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 내용 삭제 권한검사
        /// <summary>
        /// 기능명 : 게시판 내용 삭제 권한검사
        /// 작성자 : 박병수
        /// 작성일 : 2015-05-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_BOARD_DUPLICATE(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_BOARD_DELCHK");
            }

            return bExists;
        }
        #endregion


        #region 게시판 답글내용 삭제 권한검사
        /// <summary>
        /// 기능명 : 게시판 내용 삭제 권한검사
        /// 작성자 : 박병수
        /// 작성일 : 2015-05-18
        /// 수정일 : 
        /// 설  명 :
        /// </summary>
        /// <param name="oParams">Dictionary</param>
        /// <returns>bool</returns>
        public bool PROC_BOARD_REPLY_DUPLICATE(Dictionary<string, string> oParams)
        {
            bool bExists = false;

            using (spcDB = new DBHelper("CTFUSER"))
            {
                spcDB.AddParameter(oParams);
                bExists = (bool)spcDB.ExecuteScaler("USP_BOARD_REPLY_DELCHK");
            }

            return bExists;
        }
        #endregion
        
        #endregion
    }
}
