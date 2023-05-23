using System;

namespace SPC.WebUI.Common
{
    #region Login Page Info
    /// <summary>
    /// 로그인 페이지 정보 모델
    /// </summary>
    public class ctfLoginInfo
    {
        public string F_LOGINPGMID { get; set; }        // 로그인페이지 폴더명
        public string F_COMPCD { get; set; }            // 업체코드
        public string F_COMPNMKR { get; set; }          // 업체명(한글)
        public string F_COMPNMUS { get; set; }          // 업체명(영문)
        public string F_COMPNMCN { get; set; }          // 업체명(중문)
        public string F_COMPNM { get; set; }            // 업체명(현재언어)
        public string F_COMPCOPY { get; set; }          // 카피라이트
        public bool F_USEMULTILANG { get; set; }        // 다국어사용여부(true : 사용, false : 미사용)
        public bool F_USELANGKR { get; set; }           // 다국어(한글)사용여부(true : 사용, false : 미사용)
        public bool F_USELANGUS { get; set; }           // 다국어(영문)사용여부(true : 사용, false : 미사용)
        public bool F_USELANGCN { get; set; }           // 다국어(중문)사용여부(true : 사용, false : 미사용)
        public bool F_USEBOARD { get; set; }            // 게시판사용여부(true : 사용, false : 미사용)
        public bool F_ENCRTPTPW { get; set; }           // 비밀번호암호화사용여부(true : 사용, false : 미사용)
        public string F_CSRCOMPCD { get; set; }         // CSR용 업체코드
    }
    #endregion
}