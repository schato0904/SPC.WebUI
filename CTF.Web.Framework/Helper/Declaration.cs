using System;

namespace CTF.Web.Framework.Helper
{
    #region ����ü����

    #region �����������ñ���ü
    /// <summary>
    /// �����������ñ���ü 
    /// </summary>
    public struct SYSFileInfos
    {
        /// <summary>��������</summary>
        public bool IsSuccess;
        /// <summary>���ϸ�</summary>
        public string Name;
        /// <summary>����ũ��B</summary>
        public double LengthByte;
        /// <summary>����ũ��MB</summary>
        public double LengthMByte;
        /// <summary>����ð�</summary>
        public string TimeCreation;
        /// <summary>�������׼����ð�</summary>
        public string TimeLastAccess;
        /// <summary>�����������ð�</summary>
        public string TimeLastWrite;
        /// <summary>���翩��</summary>
        public bool IsExits;
        /// <summary>Ȯ����</summary>
        public string Extension;
        /// <summary>�б����뿩��</summary>
        public bool IsReadOnly;
        /// <summary>���丮���</summary>
        public string PathDirectory;
        /// <summary>���丮�������</summary>
        public string PathDirectoryBefore;
        /// <summary>���ϰ��</summary>
        public string PathFile;
        /// <summary>����Ȯ�������ܰ��</summary>
        public string PathFileNoExtension;
    }
    #endregion

    #region ���Ͼ��ε��ȯ���ñ���ü
    /// <summary>
    /// ���Ͼ��ε��ȯ���ñ���ü 
    /// </summary>
    public class FileUpLoadRtn
    {
        /// <summary>
        /// ���Ͽ�������
        /// </summary>
        public FileReturn RtnKind;
        /// <summary>
        /// ���ϰ�
        /// </summary>
        public string RtnData;
    }
    #endregion

    #endregion

    #region ����������

    #region �ý��۰���

    #region Ű�������Ű - KeyCTRL
    /// <summary>
    /// Ű�������Ű(CTRL)
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
    /// True/False,Yes/No,���/�̻��
    ///</summary>
    public enum Selection
    {
        ///<summary>True/Yes/���/����</summary>
        Yes = 1,
        ///<summary>False/No/�̻��/������</summary>
        No = 2
    }
    #endregion

    #region ��������/�������� - SortingKind
    /// <summary>
    /// ��������/��������         
    ///</summary>
    public enum SortingKind
    {
        ///<summary>��������</summary>
        Ascending = 1,
        ///<summary>��������</summary>
        Descending = 2
    }
    #endregion

    #region ������±��� - DatePrint
    /// <summary>
    /// ������±���
    /// </summary>
    public enum DatePrint
    {
        ///<summary>��þ���</summary>
        None = 1,
        ///<summary>�������</summary>
        Dash = 2,
        ///<summary>��þ���+���Ϲ�ȯ</summary>
        NoneWeek = 3,
        ///<summary>�������+���Ϲ�ȯ</summary>
        DashWeek = 4
    }
    #endregion

    #region �ݿø�/�ø�/�������� - NumRounding
    /// <summary>
    /// �ݿø�/�ø�/��������
    /// </summary>
    public enum NumRounding
    {
        ///<summary>5�̻�ݿø�</summary>
        Half5 = 1,
        ///<summary>5�ʰ��ݿø�</summary>
        Half51 = 2,
        ///<summary>�ø�</summary>
        Up = 3,
        ///<summary>����</summary>
        Down = 4
    }
    #endregion

    #region Ư������ȭ - SpecialCharConverting
    /// <summary>
    /// Ư������ȭ
    /// </summary>
    public enum SpecialCharConverting
    {
        ///<summary>Ư������</summary>
        ToSpecial = 1,
        ///<summary>�Ϲݹ���</summary>
        ToNormal = 2
    }
    #endregion

    #region ���Ϲ�ȯ�� - FileReturn
    /// <summary>
    /// ���Ϲ�ȯ��
    /// </summary>
    public enum FileReturn
    {
        /// <summary>
        /// ����
        /// </summary>
        Fail = 0,
        /// <summary>
        /// ����
        /// </summary>
        Success = 1,
        /// <summary>
        /// ���ϱ��̿���
        /// </summary>
        OverLength = 2,
        /// <summary>
        /// ����
        /// </summary>
        Error = 3
    }
    #endregion

    #region data gbn ���� ���
    public enum DataGBN
    {
        /// <summary>
        /// ���� ��ġ���˻� ���� Upload
        /// </summary>
        qimu020m = 0
    }
    #endregion

    #endregion

    #region �׸������
    public enum GrdColumnType
    {
        /// <summary>
        /// BinaryImage �÷�
        /// </summary>
        BinaryImageColumn,
        /// <summary>
        /// ButtonEdit �÷�
        /// </summary>
        ButtonEditColumn,
        /// <summary>
        /// CheckBox �÷�
        /// </summary>
        CheckColumn,
        /// <summary>
        /// ColorEdit �÷�
        /// </summary>
        ColorEditColumn,
        /// <summary>
        /// ComboBox �÷�
        /// </summary>
        ComboBoxColumn,
        /// <summary>
        /// Date �÷�
        /// </summary>
        DateColumn,
        /// <summary>
        /// DropDownEdit �÷�
        /// </summary>
        DropDownEditColumn,
        /// <summary>
        /// HyperLink �÷�
        /// </summary>
        HyperLinkColumn,
        /// <summary>
        /// Image �÷�
        /// </summary>
        ImageColumn,
        /// <summary>
        /// Memo �÷�
        /// </summary>
        MemoColumn,
        /// <summary>
        /// ProgressBar �÷�
        /// </summary>
        ProgressBarColumn,
        /// <summary>
        /// SpinEdit �÷�
        /// </summary>
        SpinEditColumn,
        /// <summary>
        /// TextBox �÷�
        /// </summary>
        TextColumn,
        /// <summary>
        /// TimeEdit �÷�
        /// </summary>
        TimeEditColumn,
        /// <summary>
        /// TokenBox �÷�
        /// </summary>
        TokenBoxColumn,
        /// <summary>
        /// Command �÷�
        /// </summary>
        CommandColumn
    }

    #region �׸��� �������
    public enum GrdEditMode
    {
        /// <summary>
        /// �ش� �÷��� ���� ����� �������� �ʽ��ϴ�.
        /// </summary>
        None,
        /// <summary>
        /// �ش� �÷��� ��� ���� ������ �����մϴ�.
        /// </summary>
        EditableAll,        
        /// <summary>
        /// �ش� �÷��� ��� ���� �б����� �Դϴ�. ���� ������ ������ �� �ִ� ���°� �˴ϴ�.
        /// </summary>
        ReadOnlyAll,
        /// <summary>
        /// �ش� �÷��� ��� ���� ��Ȱ��ȭ �˴ϴ�. ���� ������ ������ �� ���� ���°� �˴ϴ�.
        /// </summary>
        Disabled
    }
    #endregion

    #endregion

    #endregion

    #region ������ ����

    #region ��� ����
    public class LangClsCode
    {
        /// <summary>
        /// �ѱ�
        /// </summary>
        public readonly static string ko_KR = "ko-KR";

        /// <summary>
        /// �̱�
        /// </summary>
        public readonly static string en_US = "en-US";

        /// <summary>
        /// �Ϻ�
        /// </summary>
        public readonly static string ja_JP = "ja-JP";

        /// <summary>
        /// �߱�
        /// </summary>
        public readonly static string zh_CN = "zh-CN";
    }
    #endregion

    #region �޽��� ��� ����
    public class MessageModule
    {
        /// <summary>
        /// ����
        /// </summary>
        public readonly static string Common = "Common";

        /// <summary>
        /// �ý��۰���
        /// </summary>
        public readonly static string StmMgt = "StmMgt";
    }
    #endregion

    #endregion
}