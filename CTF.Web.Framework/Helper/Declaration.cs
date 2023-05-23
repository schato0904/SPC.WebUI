using System;

namespace CTF.Web.Framework.Helper
{
    #region 구조체집합

    #region 파일정보관련구조체
    /// <summary>
    /// 파일정보관련구조체 
    /// </summary>
    public struct SYSFileInfos
    {
        /// <summary>성공여부</summary>
        public bool IsSuccess;
        /// <summary>파일명</summary>
        public string Name;
        /// <summary>파일크기B</summary>
        public double LengthByte;
        /// <summary>파일크기MB</summary>
        public double LengthMByte;
        /// <summary>만든시간</summary>
        public string TimeCreation;
        /// <summary>마지막액세스시간</summary>
        public string TimeLastAccess;
        /// <summary>마지막수정시간</summary>
        public string TimeLastWrite;
        /// <summary>존재여부</summary>
        public bool IsExits;
        /// <summary>확장자</summary>
        public string Extension;
        /// <summary>읽기전용여부</summary>
        public bool IsReadOnly;
        /// <summary>디렉토리경로</summary>
        public string PathDirectory;
        /// <summary>디렉토리직전경로</summary>
        public string PathDirectoryBefore;
        /// <summary>파일경로</summary>
        public string PathFile;
        /// <summary>파일확장자제외경로</summary>
        public string PathFileNoExtension;
    }
    #endregion

    #region 파일업로드반환관련구조체
    /// <summary>
    /// 파일업로드반환관련구조체 
    /// </summary>
    public class FileUpLoadRtn
    {
        /// <summary>
        /// 파일에러정보
        /// </summary>
        public FileReturn RtnKind;
        /// <summary>
        /// 리턴값
        /// </summary>
        public string RtnData;
    }
    #endregion

    #endregion

    #region 열거형집합

    #region 시스템관련

    #region 키보드단축키 - KeyCTRL
    /// <summary>
    /// 키보드단축키(CTRL)
    /// </summary>
    public enum KeyCTRL
    {
        /// <summary>
        /// CTRL+A
        /// </summary>
        CTRL_A = 1,
        /// <summary>
        /// CTRL+B
        /// </summary>
        CTRL_B = 2,
        /// <summary>
        /// CTRL+C
        /// </summary>
        CTRL_C = 3,
        /// <summary>
        /// CTRL+D
        /// </summary>
        CTRL_D = 4,
        /// <summary>
        /// CTRL+E
        /// </summary>
        CTRL_E = 5,
        /// <summary>
        /// CTRL+F
        /// </summary>
        CTRL_F = 6,
        /// <summary>
        /// CTRL+G
        /// </summary>
        CTRL_G = 7,
        /// <summary>
        /// CTRL+H
        /// </summary>
        CTRL_H = 8,
        /// <summary>
        /// CTRL+I
        /// </summary>
        CTRL_I = 9,
        /// <summary>
        /// CTRL+K
        /// </summary>
        CTRL_K = 11,
        /// <summary>
        /// CTRL+L
        /// </summary>
        CTRL_L = 12,
        /// <summary>
        /// CTRL+M
        /// </summary>
        CTRL_M = 13,
        /// <summary>
        /// CTRL+O
        /// </summary>
        CTRL_O = 15,
        /// <summary>
        /// CTRL+P
        /// </summary>
        CTRL_P = 16,
        /// <summary>
        /// CTRL+Q
        /// </summary>
        CTRL_Q = 17,
        /// <summary>
        /// CTRL+R
        /// </summary>
        CTRL_R = 18,
        /// <summary>
        /// CTRL+S
        /// </summary>
        CTRL_S = 19,
        /// <summary>
        /// CTRL+T
        /// </summary>
        CTRL_T = 20,
        /// <summary>
        /// CTRL+U
        /// </summary>
        CTRL_U = 21,
        /// <summary>
        /// CTRL+V
        /// </summary>
        CTRL_V = 22,
        /// <summary>
        /// CTRL+W
        /// </summary>
        CTRL_W = 23,
        /// <summary>
        /// CTRL+X
        /// </summary>
        CTRL_X = 24,
        /// <summary>
        /// CTRL+Y
        /// </summary>
        CTRL_Y = 25,
        /// <summary>
        /// CTRL+Z
        /// </summary>
        CTRL_Z = 26
    }
    #endregion

    #region True/False - Selection
    /// <summary>
    /// True/False,Yes/No,사용/미사용
    ///</summary>
    public enum Selection
    {
        ///<summary>True/Yes/사용/적용</summary>
        Yes = 1,
        ///<summary>False/No/미사용/미적용</summary>
        No = 2
    }
    #endregion

    #region 오름차순/내림차순 - SortingKind
    /// <summary>
    /// 오름차순/내림차순         
    ///</summary>
    public enum SortingKind
    {
        ///<summary>오름차순</summary>
        Ascending = 1,
        ///<summary>내림차순</summary>
        Descending = 2
    }
    #endregion

    #region 일자출력기준 - DatePrint
    /// <summary>
    /// 일자출력기준
    /// </summary>
    public enum DatePrint
    {
        ///<summary>대시없이</summary>
        None = 1,
        ///<summary>대시포함</summary>
        Dash = 2,
        ///<summary>대시없이+요일반환</summary>
        NoneWeek = 3,
        ///<summary>대시포함+요일반환</summary>
        DashWeek = 4
    }
    #endregion

    #region 반올림/올림/버림종류 - NumRounding
    /// <summary>
    /// 반올림/올림/버림종류
    /// </summary>
    public enum NumRounding
    {
        ///<summary>5이상반올림</summary>
        Half5 = 1,
        ///<summary>5초과반올림</summary>
        Half51 = 2,
        ///<summary>올림</summary>
        Up = 3,
        ///<summary>버림</summary>
        Down = 4
    }
    #endregion

    #region 특수문자화 - SpecialCharConverting
    /// <summary>
    /// 특수문자화
    /// </summary>
    public enum SpecialCharConverting
    {
        ///<summary>특수문자</summary>
        ToSpecial = 1,
        ///<summary>일반문자</summary>
        ToNormal = 2
    }
    #endregion

    #region 파일반환값 - FileReturn
    /// <summary>
    /// 파일반환값
    /// </summary>
    public enum FileReturn
    {
        /// <summary>
        /// 실패
        /// </summary>
        Fail = 0,
        /// <summary>
        /// 성공
        /// </summary>
        Success = 1,
        /// <summary>
        /// 파일길이에러
        /// </summary>
        OverLength = 2,
        /// <summary>
        /// 에러
        /// </summary>
        Error = 3
    }
    #endregion

    #region data gbn 구분 상수
    public enum DataGBN
    {
        /// <summary>
        /// 도면 전치수검사 파일 Upload
        /// </summary>
        qimu020m = 0
    }
    #endregion

    #endregion

    #region 그리드관련
    public enum GrdColumnType
    {
        /// <summary>
        /// BinaryImage 컬럼
        /// </summary>
        BinaryImageColumn,
        /// <summary>
        /// ButtonEdit 컬럼
        /// </summary>
        ButtonEditColumn,
        /// <summary>
        /// CheckBox 컬럼
        /// </summary>
        CheckColumn,
        /// <summary>
        /// ColorEdit 컬럼
        /// </summary>
        ColorEditColumn,
        /// <summary>
        /// ComboBox 컬럼
        /// </summary>
        ComboBoxColumn,
        /// <summary>
        /// Date 컬럼
        /// </summary>
        DateColumn,
        /// <summary>
        /// DropDownEdit 컬럼
        /// </summary>
        DropDownEditColumn,
        /// <summary>
        /// HyperLink 컬럼
        /// </summary>
        HyperLinkColumn,
        /// <summary>
        /// Image 컬럼
        /// </summary>
        ImageColumn,
        /// <summary>
        /// Memo 컬럼
        /// </summary>
        MemoColumn,
        /// <summary>
        /// ProgressBar 컬럼
        /// </summary>
        ProgressBarColumn,
        /// <summary>
        /// SpinEdit 컬럼
        /// </summary>
        SpinEditColumn,
        /// <summary>
        /// TextBox 컬럼
        /// </summary>
        TextColumn,
        /// <summary>
        /// TimeEdit 컬럼
        /// </summary>
        TimeEditColumn,
        /// <summary>
        /// TokenBox 컬럼
        /// </summary>
        TokenBoxColumn,
        /// <summary>
        /// Command 컬럼
        /// </summary>
        CommandColumn
    }

    #region 그리드 편집모드
    public enum GrdEditMode
    {
        /// <summary>
        /// 해당 컬럼의 편집 방식을 지정하지 않습니다.
        /// </summary>
        None,
        /// <summary>
        /// 해당 컬럼의 모든 셀은 편집이 가능합니다.
        /// </summary>
        EditableAll,        
        /// <summary>
        /// 해당 컬럼의 모든 셀은 읽기전용 입니다. 셀의 내용을 복사할 수 있는 상태가 됩니다.
        /// </summary>
        ReadOnlyAll,
        /// <summary>
        /// 해당 컬럼의 모든 셀은 비활성화 됩니다. 셀의 내용을 복사할 수 없는 상태가 됩니다.
        /// </summary>
        Disabled
    }
    #endregion

    #endregion

    #endregion

    #region 문자형 집합

    #region 언어 관련
    public class LangClsCode
    {
        /// <summary>
        /// 한국
        /// </summary>
        public readonly static string ko_KR = "ko-KR";

        /// <summary>
        /// 미국
        /// </summary>
        public readonly static string en_US = "en-US";

        /// <summary>
        /// 일본
        /// </summary>
        public readonly static string ja_JP = "ja-JP";

        /// <summary>
        /// 중국
        /// </summary>
        public readonly static string zh_CN = "zh-CN";
    }
    #endregion

    #region 메시지 모듈 관련
    public class MessageModule
    {
        /// <summary>
        /// 공통
        /// </summary>
        public readonly static string Common = "Common";

        /// <summary>
        /// 시스템관리
        /// </summary>
        public readonly static string StmMgt = "StmMgt";
    }
    #endregion

    #endregion
}