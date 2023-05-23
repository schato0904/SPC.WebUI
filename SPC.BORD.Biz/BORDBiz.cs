using System;
using System.Collections.Generic;
using System.Data;

using SPC.BORD.Dac;

namespace SPC.BORD.Biz
{
    public class BORDBiz : IDisposable
    {
        #region Dispose
        public void Dispose()
        {
        }
        #endregion


        #region 게시판

        #region 게시판 등록
        public bool BOARD_INS(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BORDDac dac = new BORDDac())
            {
                bExecute = dac.BOARD_INS(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion

        #region 게시판 조회
        public DataSet BOARD_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 전체 갯수
        public Int32 BOARD_CNT(Dictionary<string, string> oParams)
        {
            Int32 resultCnt = -1;

            using (BORDDac dac = new BORDDac())
            {
                resultCnt = dac.BOARD_CNT(oParams);
            }

            return resultCnt;
        }
        #endregion


        #region 답글 등록
        public bool BOARD_REPLY_INS(string[] oSps, object[] oParams, out string resultMsg)
        {
            bool bExecute = false;

            using (BORDDac dac = new BORDDac())
            {
                bExecute = dac.BOARD_REPLY_INS(oSps, oParams, out resultMsg);
            }

            return bExecute;
        }
        #endregion


        #region 게시판 상세보기
        public DataSet BOARD_DETAIL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_DETAIL(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기_체크박스
        public DataSet BOARD_DETAIL_CHK(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_DETAIL_CHK(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 댓글등록
        public DataSet BOARD_COMMENT_INS(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_COMMENT_INS(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 댓글조회
        public DataSet BOARD_COMMENT_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_COMMENT_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기 수정
        public DataSet BOARD_DETAIL_UPD(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_DETAIL_UPD(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 상세보기 삭제
        public DataSet BOARD_DETAIL_DEL(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_DETAIL_DEL(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 게시판 답글생성
        public DataSet BOARD_REPLY(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_REPLY(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 답글화면 상세보기
        public DataSet BOARD_REPLY_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_REPLY_LST(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 댓글카운트구하기
        public DataSet BOARD_COMMENTCOUNT(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_COMMENTCOUNT(oParams, out errMsg);
            }

            return ds;
        }
        #endregion

        #region 메인페이지 공지사항리스트
        public DataSet BOARD_MAIN_LST(Dictionary<string, string> oParams, out string errMsg)
        {
            DataSet ds = new DataSet();

            using (BORDDac dac = new BORDDac())
            {
                ds = dac.BOARD_MAIN_LST(oParams, out errMsg);
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

            using (BORDDac dac = new BORDDac())
            {
                bExists = dac.PROC_BOARD_DUPLICATE(oParams);
            }

            return bExists;
        }
        #endregion


        #region 게시판 답글 내용 삭제 권한검사
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

            using (BORDDac dac = new BORDDac())
            {
                bExists = dac.PROC_BOARD_REPLY_DUPLICATE(oParams);
            }

            return bExists;
        }
        #endregion

        
        #endregion
    }
}
