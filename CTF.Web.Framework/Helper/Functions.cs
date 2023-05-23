using System;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// 기능명 : Functions
    /// 작성자 : SSH
    /// 작성일 : 2014-03-15
    /// 수정일 :
    /// 설  명 :
    /// </summary>
    public class Functions
    {
        #region 문자열 구분자
        /// <summary>
        /// 열의 행 구분자
        /// </summary>
        private const string _Split = "√";
        /// <summary>
        /// 열 구분자
        /// </summary>
        private const string _Delimeter = "•";
        #endregion 

        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>        
        public Functions() { }
        #endregion

        #region Function - 암호화관련함수
        public sealed class Encrypts
        {
            #region 암호화
            public string HashPasswordToString(string encValue, string encType)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(encValue, String.IsNullOrEmpty(encType) ? "SHA1" : encType);
            }
            #endregion

            #region Base64

            #region Encode
            public string EncodeTo64(string toEncode)
            {
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
                return returnValue;
            }
            #endregion

            #region Decode
            public string DecodeFrom64(string encodedData)
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
                string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
                return returnValue;
            }
            #endregion

            #endregion

            #region GetUniqueID
            public string GetUniqueKey()
            {
                const int maxSize = 40;
                char[] chars = new char[62];
                string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                chars = a.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                System.Security.Cryptography.RNGCryptoServiceProvider crypto = new System.Security.Cryptography.RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = maxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length - 1)]); }
                return result.ToString();
            }

            #endregion

            #region RSA
            public int BitStrength = 1024;
            public string publicKeyXML = "<RSAKeyValue><Modulus>meiyXW6EP7XjdSWTZIqQjfMQ8RS8VjtT0mOgzFkC57goeDJPtZiWZpBNnPUQ3OXvfOgkmuG5ANIdKPFA7quo2ZAD6WUgCfl51Om2YphbSyWr9+gYmGY9D+dDUw8gV+dJp15z5ztc50WcCn1rUV9uG/6yEv5aiWFCc1pf+fKDbME=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            public string privateKeyXML = "<RSAKeyValue><Modulus>meiyXW6EP7XjdSWTZIqQjfMQ8RS8VjtT0mOgzFkC57goeDJPtZiWZpBNnPUQ3OXvfOgkmuG5ANIdKPFA7quo2ZAD6WUgCfl51Om2YphbSyWr9+gYmGY9D+dDUw8gV+dJp15z5ztc50WcCn1rUV9uG/6yEv5aiWFCc1pf+fKDbME=</Modulus><Exponent>AQAB</Exponent><P>yu/DgO1KWXzVvMLrbQW+2G28oI/yeNUD6ZyjdrOrHNZvzyKM52F26K8xyjLU8jMcPV7RggmYz6SPRYld82pCAw==</P><Q>wicbWmUZBKESt6V6/f5PEQoMkdsxPjQ32DoMM8D58zHwo2SCWlVgvlt+g6YhGOwJD4wAks99zmdcoMdyHJWc6w==</Q><DP>xzVXl97XZkLp2XMKAaprLi+Cw6aqYbzRK2is+d/i79r8RLvoz1VYkY8w9Ai0CtDrPr8uMFVVYTdrwNnYlRLQmw==</DP><DQ>uW8kfufER1mYSPKyT9kOp1WTv9M8aw7wr4JxmRSdJhvym/wpTCHzbpxwb0jCn80AsmqjOZUXsYWjQKR9ZrO21Q==</DQ><InverseQ>oeIgrD3/ABnFLrbegDzosgHV1cG0TnUA6WRnUfBpTfm9Nz9KbznxIB4kQ409DvzZK9kGlgrV3fgKlQDKg2WxuQ==</InverseQ><D>j6MOy4m33Om4hPzziNKUxBWDyyrJ9kRaHzChwfSUdIN3KW3y3Ayy2Ld157UC7tWc41qXxwBAM1bkpfA55ETKSAa7CKnBButZJ932URJTp6+HF8ywPATC/wuKTxdF8ptXrYPDP3EbHNqk1qEtGxUcuLkDtz31dJBhVJrpB/MBSZ0=</D></RSAKeyValue>";

            public string RSAEncryptString(string inputString)
            {
                // TODO: Add Proper Exception Handlers
                RSACryptoServiceProvider rsaCryptoServiceProvider =
                                              new RSACryptoServiceProvider(BitStrength);
                rsaCryptoServiceProvider.FromXmlString(publicKeyXML);
                int keySize = BitStrength / 8;
                byte[] bytes = Encoding.UTF32.GetBytes(inputString);
                // The hash function in use by the .NET RSACryptoServiceProvider here 
                // is SHA1
                // int maxLength = ( keySize ) - 2 - 
                //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
                int maxLength = keySize - 42;
                int dataLength = bytes.Length;
                int iterations = dataLength / maxLength;
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i <= iterations; i++)
                {
                    byte[] tempBytes = new byte[
                            (dataLength - maxLength * i > maxLength) ? maxLength :
                                                          dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                      tempBytes.Length);
                    byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                              true);
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes. It does this after encryption and before 
                    // decryption. If you do not require compatibility with Microsoft 
                    // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                    // next line and the corresponding one in the DecryptString function.
                    Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only 
                    // ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
                return stringBuilder.ToString();
            }

            public string RSADecryptString(string inputString)
            {
                // TODO: Add Proper Exception Handlers
                RSACryptoServiceProvider rsaCryptoServiceProvider
                                         = new RSACryptoServiceProvider(BitStrength);
                rsaCryptoServiceProvider.FromXmlString(privateKeyXML);
                int base64BlockSize = ((BitStrength / 8) % 3 != 0) ?
                  (((BitStrength / 8) / 3) * 4) + 4 : ((BitStrength / 8) / 3) * 4;
                int iterations = inputString.Length / base64BlockSize;
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < iterations; i++)
                {
                    byte[] encryptedBytes = Convert.FromBase64String(
                         inputString.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic 
                    // API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the 
                    // EncryptString function.
                    Array.Reverse(encryptedBytes);
                    arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                        encryptedBytes, true));
                }
                return Encoding.UTF32.GetString(arrayList.ToArray(
                                          Type.GetType("System.Byte")) as byte[]);
            }
            #endregion
        }
        #endregion

        #region Function - 문자관련함수
        /// <summary>
        /// 문자관련함수
        /// </summary>
        public sealed class Strings
        {
            #region 널값처리 - Nz
            /// <summary>
            /// 널값처리
            /// </summary>
            /// <param name="pValue">처리할값</param>
            /// <returns>string</returns>
            public string Nz(string pValue)
            {
                if (pValue == null) return "";
                else return pValue;
            }
            /// <summary>
            /// 널값처리
            /// </summary>
            /// <param name="pValue">처리할값</param>
            /// <param name="pNumber">숫자문자체크</param>
            /// <returns>string</returns>
            public string Nz(string pValue, bool pNumber)
            {
                if (pValue == null || pValue == "")
                {
                    if (pNumber) return "0";
                    else return "";
                }
                else return pValue;
            }
            #endregion

            #region 특수문자처리 - ReplaceChar
            /// <summary>
            /// 특수문자처리
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pMode">특수문자로변환/일반문자로변환</param>
            /// <returns></returns>			
            public string ReplaceChar(string pValue, SpecialCharConverting pMode)
            {
                if (pMode == SpecialCharConverting.ToSpecial)
                {
                    pValue = pValue.Replace("^", "＾");
                    pValue = pValue.Replace("<", "＜");
                    pValue = pValue.Replace(">", "＞");
                    pValue = pValue.Replace("&", "＆");
                    pValue = pValue.Replace("'", "＇");
                    pValue = pValue.Replace("\"", "＂");
                    pValue = pValue.Replace("|", "｜");
                    pValue = pValue.Replace("@", "＠");
                    pValue = pValue.Replace("$", "＄");
                    pValue = pValue.Replace("%", "％");
                    pValue = pValue.Replace("*", "＊");
                    pValue = pValue.Replace("#", "＃");
                    pValue = pValue.Replace("+", "＋");
                    pValue = pValue.Replace(";", "；");
                    pValue = pValue.Replace("_", "＿");
                    pValue = pValue.Replace("/", "／");
                }
                else
                {
                    pValue = pValue.Replace("＾", "^");
                    pValue = pValue.Replace("＜", "<");
                    pValue = pValue.Replace("＞", ">");
                    pValue = pValue.Replace("＆", "&");
                    pValue = pValue.Replace("＇", "'");
                    pValue = pValue.Replace("＂", "\"");
                    pValue = pValue.Replace("｜", "|");
                    pValue = pValue.Replace("＠", "@");
                    pValue = pValue.Replace("＄", "$");
                    pValue = pValue.Replace("％", "%");
                    pValue = pValue.Replace("＊", "*");
                    pValue = pValue.Replace("＃", "#");
                    pValue = pValue.Replace("＋", "+");
                    pValue = pValue.Replace("；", ";");
                    pValue = pValue.Replace("＿", "_");
                    pValue = pValue.Replace("／", "/");
                    pValue = pValue.Replace("@@", "#");
                }
                return pValue;
            }
            #endregion

            #region 문자열바이트길이 - ByteLength
            /// <summary>
            /// 문자열바이트길이
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>길이</returns>
            public int ByteLength(string pValue)
            {
                byte[] arrayByte = System.Text.Encoding.Default.GetBytes(pValue);
                return arrayByte.Length;
            }
            #endregion

            #region 문자와바이트배열생성 - ByteAry
            /// <summary>
            /// 문자와바이트배열생성
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="cc"></param>
            /// <param name="ii"></param>
            private void ByteAry(string pValue, out char[] cc, out int[] ii)
            {
                cc = pValue.ToCharArray();
                int[] iii = new int[cc.Length];
                ii = iii; //문자열바이트수체크를 위한 변수 선언
                for (int i = 1; i <= cc.Length; i++) { ii[i - 1] = ByteCharLength(cc[i - 1]); } //배열당바이트수체크
            }
            #endregion

            #region 문자바이트반환 - ByteCharLength
            /// <summary>
            /// 문자바이트반환
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>길이</returns>
            private int ByteCharLength(char pValue)
            {
                return pValue > byte.MaxValue ? 2 : 1;
            }
            #endregion

            #region 바이트단위Left - ByteLeft
            /// <summary>
            /// 바이트단위Left
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">길이</param>
            /// <returns>문자열</returns>
            public string ByteLeft(string pValue, int pLen)
            {
                char[] cc;
                int[] ii;
                long count = 0;
                StringBuilder sb = new StringBuilder();

                ByteAry(pValue, out cc, out ii); //문자 바이트와 바이트수 체킹

                for (int i = 1; i <= pLen; i++)
                {
                    if (i > cc.Length) break; //에러통과용
                    count += ii[i - 1];
                    if (count > pLen) break;
                    else sb.Append(cc[i - 1].ToString()); //문자열조합
                }
                return sb.ToString();
            }
            #endregion

            #region 바이트단위Right - ByteRight
            /// <summary>
            /// 바이트단위Right
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">길이</param>
            /// <returns>문자열</returns>
            public string ByteRight(string pValue, int pLen)
            {
                char[] cc;
                int[] ii;
                long count = 0;
                string sb = "";

                ByteAry(pValue, out cc, out ii); //문자 바이트와 바이트수 체킹

                for (int i = cc.Length; i >= cc.Length - pLen; i--)
                {
                    if (i == 0) break; //에러통과용
                    count += ii[i - 1];
                    if (count > pLen) break;
                    else sb = cc[i - 1].ToString() + sb; //문자열조합
                }
                return sb.ToString();
            }
            #endregion

            #region 바이트단위Substring - ByteSubsting
            /// <summary>
            /// 바이트단위Substring
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">길이</param>
            /// <returns>문자엵</returns>
            public string ByteSubsting(string pValue, int pLen)
            {
                return FnByteSubsting(pValue, 1, pLen);
            }
            /// <summary>
            /// 바이트단위Substring
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pStart">시작</param>
            /// <param name="pLen">길이</param>
            /// <returns>문자열</returns>
            public string FnByteSubsting(string pValue, int pStart, int pLen)
            {
                char[] cc;
                int[] ii;
                long count = 0;
                StringBuilder sb = new StringBuilder();

                ByteAry(pValue, out cc, out ii); //문자 바이트와 바이트수 체킹

                for (int i = pStart; i <= pLen; i++)
                {
                    if (i > cc.Length) break; //에러통과용
                    count += ii[i - 1];
                    if (count > pLen) break;
                    else sb.Append(cc[i - 1].ToString()); //문자열조합
                }
                return sb.ToString();
            }
            #endregion

            #region 바이트단위문자열분할 - ByteSplit
            /// <summary>
            /// 바이트단위문자열분할
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pEnd">자를숫자</param>
            /// <returns></returns>
            public string ByteSplit(string pValue, int pEnd)
            {
                return ByteSplit(pValue, 0, pEnd);
            }
            /// <summary>
            /// 바이트단위문자열분할
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pStart">시작숫자</param>
            /// <param name="pEnd">자를숫자</param>
            /// <returns>문자열</returns>
            public string ByteSplit(string pValue, int pStart, int pEnd)
            {
                char[] cc;
                int[] ii;
                long count = 0;
                StringBuilder sb = new StringBuilder();

                ByteAry(pValue, out cc, out ii); //문자 바이트와 바이트수 체킹

                for (int i = 1; i <= cc.Length; i++)
                {
                    count += ii[i - 1];
                    if (count > pEnd - pStart) { count = 0; sb.Append("\r\n"); i--; } //엔터처리
                    else { sb.Append(cc[i - 1].ToString()); } //문자열조합

                }
                return sb.ToString();
            }
            #endregion

            #region 텍스트정형화 - ByteBlock
            /// <summary>
            /// 텍스트정형화
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">문자열길이</param>
            /// <returns>문자열</returns>
            public string ByteBlock(string pValue, int pLen)
            {
                return ByteBlock(pValue, pLen, false, false);
            }
            /// <summary>
            /// 텍스트정형화
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">문자열길이</param>
            /// <param name="pMoney">금액여부</param>
            /// <param name="pRight">오른쪽정렬</param>
            /// <returns>문자열</returns>
            public string ByteBlock(string pValue, int pLen, bool pMoney, bool pRight)
            {
                string[] pvalue = pValue.Replace("\n", "").Split(Convert.ToChar("\r"));
                return ByteBlock(pvalue, pLen, pMoney, pRight);
            }
            /// <summary>
            /// 텍스트정형화
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">문자열길이</param>
            /// <returns>문자열</returns>
            public string ByteBlock(string[] pValue, int pLen)
            {
                return ByteBlock(pValue, pLen, false, false);
            }
            /// <summary>
            /// 텍스트정형화
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pLen">문자열길이</param>
            /// <param name="pMoney">금액여부</param>
            /// <param name="pRight">오른쪽정렬</param>
            /// <returns>문자열</returns>
            public string ByteBlock(string[] pValue, int pLen, bool pMoney, bool pRight)
            {
                char[] cc;
                int[] ii;
                int count = 0;
                StringBuilder sb = new StringBuilder(); //루프용
                StringBuilder Sb = new StringBuilder(); //최종반환용

                for (int h = 1; h <= pValue.Length; h++)
                {
                    if (pMoney)
                    {   //금액형식으로 변경
                        try { if (decimal.Parse(pValue[h - 1]) > 0) pValue[h - 1] = Format_Money(pValue[h - 1]); }
                        catch { }
                    }

                    ByteAry(pValue[h - 1], out cc, out ii); //문자 바이트와 바이트수 체킹
                    count = 0;

                    for (int i = 1; i <= cc.Length; i++)
                    {
                        count += ii[i - 1];
                        if (count > pLen) { count -= ii[i - 1]; break; }
                        sb.Append(cc[i - 1].ToString()); //문자열조합
                    }
                    if (!pRight) { Sb.Append(sb.ToString() + Space(pLen - count) + "★\r\n"); sb.Remove(0, sb.Length); }
                    else { Sb.Append(Space(pLen - count) + sb.ToString() + "★\r\n"); sb.Remove(0, sb.Length); }
                }
                return Sb.ToString();
            }
            #endregion

            #region 스페이스문자생성 - Space
            /// <summary>
            /// 스페이스문자생성
            /// </summary>
            /// <param name="pLength">스페이스길이</param>
            /// <returns>문자열</returns>
                        
            public string Space(int pLength)
            {
                if (pLength > 0) return string.Format("{0," + pLength + "}", "");
                else return "";
            }
            #endregion

            #region 포맷형식날짜 - Format_Date
            /// <summary>
            /// 포맷형식날짜
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>문자열</returns>
            public string Format_Date(string pValue)
            {
                try
                {
                    return string.Format("{0}-{1}-{2}", pValue.Substring(0, 4), pValue.Substring(4, 2), pValue.Substring(6, 2));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion

            #region 포맷형식원화 - Format_Money
            /// <summary>
            /// 문자를원화형식으로변경
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>문자열</returns>
            public string Format_Money(string pValue)
            {
                try
                {
                    bool bb = pValue.IndexOf(".")>0;
                    decimal dd = decimal.Parse(pValue);
                    string ss = dd.ToString("N");
                    if (!bb) ss = ss.Substring(0, ss.IndexOf("."));
                    return ss;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion

            #region 특정문자의갯수 - StringCount
            /// <summary>
            /// 특정문자의갯수
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pFindString">찾을문자열</param>
            /// <returns>갯수</returns>
            public int StringCount(string pValue, string pFindString)
            {
                char[] cc = pValue.ToCharArray();
                int cnt = 0;

                for (int i = 0; i < cc.Length; i++)
                {
                    if (cc[i].ToString() == pFindString) cnt++;
                }
                return cnt;
            }
            #endregion

            #region 인덱스값선택하기 - Choose
            /// <summary>
            /// 인덱스값선택하기
            /// </summary>
            /// <param name="pIndex">인덱스</param>
            /// <param name="pValue">배열</param>
            /// <returns>문자열</returns>             
            public string Choose(int pIndex, string[] pValue)
            {
                return pValue[pIndex].ToString();
            }
            #endregion

            #region 금액한글표기 - MoneyToKerean
            /// <summary>
            /// 금액한글표기
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>문자열</returns>            
            public string MoneyToKerean(string pValue)
            {
                return MoneyToKerean(decimal.Parse(pValue));
            }
            /// <summary>
            /// 금액한글표기
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>문자열</returns>
            public string MoneyToKerean(decimal pValue)
            {
                char[] pvalue = pValue.ToString().Replace(",","").ToCharArray();
                string[] numb = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" }; //숫자배열
                string[] unit = new string[] { "", "십", "백", "천" }; //금액배열
                string[] sect = new string[] { "", "만", "억", "조" }; //단위배열
                int j = pvalue.Length - 1;
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < pvalue.Length; i++)
                {
                    sb.Append(numb[int.Parse(pvalue[i].ToString())]); //숫자대입
                    if (j % 4 != 0 && pvalue[i].ToString() != "0") sb.Append(unit[j % 4]); //금액대입
                    if (j % 4 == 0) sb.Append(sect[j / 4]); j--; //단위대입
                }
                return sb.ToString() + "원정";
            }
            #endregion

            #region 1차원배열정렬 - SortAry1St
            /// <summary>
            /// 차원배열정렬
            /// </summary>
            /// <param name="pAry">배열</param>
            /// <param name="AryQur">정렬순서</param>
            /// <returns>배열</returns>            
            public string[] SortAry1St(string[] pAry, SortingKind AryQur)
            {
                Array pary = new string[pAry.Length]; pAry.CopyTo(pary, 0);
                Array.Sort(pary);
                int i = AryQur == SortingKind.Ascending ? 0 : pary.Length - 1;
                foreach (string aa in pary) 
                { 
                    pAry[i] = aa.ToString();
                    if (AryQur == SortingKind.Ascending) i++;
                    if (AryQur == SortingKind.Descending) i--;
                }
                return pAry;
            }
            #endregion
        }
        #endregion

        #region Function - 숫자관련함수
        /// <summary>
        /// 숫자관련함수
        /// </summary>
        public class Numbers
        {
            #region 올림.반올림.버림 - Rounding
            /// <summary>
            /// 올림.반올림.버림            
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pPoint">소수점자리</param>
            /// <param name="pKind">반올림상수</param>
            /// <returns>결과값</returns>                       
            public double Rounding(double pValue, int pPoint, NumRounding pKind)
            {
                string pvalue = pValue.ToString();
                return Rounding(pvalue, pPoint, pKind);
            }
            /// <summary>
            /// 올림.반올림.버림
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pPoint">소수점자리</param>
            /// <param name="pKind">반올림상수</param>
            /// <returns>결과값</returns>
            public double Rounding(string pValue, string pPoint, NumRounding pKind)
            {
                int ppoint = int.Parse(pPoint);
                return Rounding(pValue, ppoint, pKind);
            }
            /// <summary>
            /// 올림.반올림.버림
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pPoint">소수점자리</param>
            /// <param name="pKind">반올림상수</param>
            /// <returns>결과값</returns>
            public double Rounding(string pValue, int pPoint, NumRounding pKind)
            {
                try
                {
                    if (pPoint == 0) pPoint = 1; //0.1은 소수점 첫째자리기준
                    if (pValue.IndexOf(".") == -1) pValue += ".0"; //소수점연산화
                    int nBasis = pPoint > 0 ? pValue.IndexOf(".") - pPoint : pValue.IndexOf(".") - pPoint; //기준포인터 Basis 지정
                    char[] nValAry = new char[pValue.Length]; //문자배열선언
                    pValue.CopyTo(0, nValAry, 0, pValue.Length); //문자배열에값할당
                    StringBuilder sb = new StringBuilder();
                    bool nEndChk = false; //올림을 한번 했을경우 더이상 같은 로직을 타지 못하도록
                    //int nTotal = 0; //51반올림시 후방합계를 구하기 위한 변수

                    for (int i = 0; i < nValAry.Length; i++)
                    {
                        if (nValAry[i].ToString() == ".") sb.Append(nValAry[i].ToString()); //소수점문자일경우
                        else if (!nEndChk && (i == nBasis || i + 1 == nValAry.Length))
                        {
                            sb.Append(Rounding_Sub(nValAry, i, pKind)); 
                            nEndChk = true;                            
                        }
                        else if (i <= nBasis) sb.Append(nValAry[i].ToString()); //해당숫자대입
                        else sb.Append("0"); //기준포인터를벗어나는배열에0할당
                    }
                    return double.Parse(sb.ToString());
                }
                catch (Exception ex)
                {
                    string Ex = ex.Message; //에러공통모듈작성후재코딩요망
                    return 0;
                }
            }
            /// <summary>
            /// 올림.반올림.버림
            /// </summary>
            /// <param name="pCAry">값</param>
            /// <param name="pPoint">소수점자리</param>
            /// <param name="pKind">반올림상수</param>
            /// <returns>결과값</returns>
            private string Rounding_Sub(char[] pCAry, int pPoint, NumRounding pKind)
            {
                int nTotal = 0;

                if (pCAry.Length - 1 <= pPoint) nTotal = 0;
                else
                {
                    int nPoint = pCAry[pPoint + 1].ToString() == "." ? pPoint + 2 : pPoint + 1;

                    switch (pKind)
                    {
                        case NumRounding.Half5:     //반올림5
                            nTotal = int.Parse(pCAry[nPoint].ToString()) >= 5 ? 1 : 0;
                            break;
                        case NumRounding.Half51:    //반올림51
                            //다음자리이후부터마지막자리까지합을구한다.
                            if (int.Parse(pCAry[nPoint].ToString()) >= 5)
                            {
                                for (int j = nPoint + 1; j < pCAry.Length; j++) { nTotal += int.Parse(pCAry[j].ToString()); }
                            }
                            nTotal = nTotal > 0 ? 1 : 0;
                            break;
                        case NumRounding.Up:        //마지막자리올림
                            nTotal = int.Parse(pCAry[nPoint].ToString()) > 0 ? 1 : 0; ;
                            break;
                        case NumRounding.Down:       //버림
                            nTotal = 0;
                            break;
                    }
                }
                if (nTotal == 0) nTotal = int.Parse(pCAry[pPoint].ToString());
                else nTotal = int.Parse(pCAry[pPoint].ToString()) + 1;
                return nTotal.ToString();
            }
            #endregion            

            #region 퍼센트구하기 - Percent
            /// <summary>
            /// 퍼센트구하기           
            /// </summary>
            /// <param name="pValue1">값</param>
            /// <param name="pValue2">퍼센트</param>
            /// <returns>결과값</returns>            
            public double Percent(double pValue1, double pValue2)
            {
                if (pValue2 == 0 || pValue1 == 0) return 100;
                else return (pValue2 / pValue1) * 100;
            }
            #endregion            

            #region 퍼센트값구하기 - PercentValue
            /// <summary>
            /// 퍼센트값구하기
            /// </summary>
            /// <param name="pValue">값</param>
            /// <param name="pPercent">퍼센트</param>
            /// <returns>decimal</returns>
            public decimal PercentValue(decimal pValue, decimal pPercent)
            {
                return pValue * pPercent;
            }
            #endregion

            #region 논리연산Not - Not
            /// <summary>
            /// 논리연산Not
            /// </summary>
            /// <param name="cValue">값</param>
            /// <returns>int</returns>
            public int Not(int cValue)
            {
                return (cValue + 1) % 2;
            }
            #endregion
        }
        #endregion

        #region Function - 날짜관련함수
        /// <summary>
        /// 날짜관련함수
        /// </summary>
        public class Dates
        {
            #region 당일날짜가져오기 - Now
            /// <summary>
            /// 당일날짜가져오기            
            /// </summary>
            /// <returns>결과값</returns>
            public string Now()
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
            /// <summary>
            /// 당일날짜가져오기
            /// </summary>
            /// <param name="pDatePrint"></param>
            /// <returns></returns>
            public string Now(DatePrint pDatePrint)
            {
                string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                
                switch (pDatePrint)
                {
                    case DatePrint.None:
                    case DatePrint.NoneWeek:
                        NowDate = DateTime.Now.ToString("yyyyMMdd");
                        break;
                    case DatePrint.Dash:
                    case DatePrint.DashWeek:
                        NowDate = DateTime.Now.ToString("yyyy-MM-dd");                            
                        break;                                                
                }
                return NowDate;
            }

            /// <summary>
            /// 당일날짜가져오기
            /// </summary>
            /// <param name="pTime">시간포함여부</param>
            /// <returns>결과값</returns>
            public string Now(bool pTime)
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            #endregion

            #region 날짜형식변경 함수 - NoneToDash
            /// <summary>
            /// 날짜(YYYYMMDD)를 날짜형식(YYYY-MM-DD)로 바꿔준다
            /// </summary>
            /// <param name="pValue">변경할값</param>
            /// <returns>결과값</returns>
            public string NoneToDash(string pValue)
            {
                if (pValue == "") return "";
                else              return string.Format("{0:####-##-##}", int.Parse(pValue));
            }
            #endregion

            #region 날짜형식변경 함수 - DashToNone
            /// <summary>
            /// .기능명 : 날짜(YYYY-MM-DD)를 날짜형식(YYYYMMDD)로 바꿔준다
            /// .작성자 : Leira
            /// .작성일 : 2008.06.18
            /// .수정일 :             
            /// .설  명 : 
            /// </summary>
            /// <param name="pValue">변경할값</param>
            /// <returns></returns>
            public string DashToNone(string pValue)
            {
                return pValue.Replace("-", "");
            }
            #endregion

            #region 시작종료일자반환 - MonthReturn
            /// <summary>            
            /// 시작종료일자반환
            /// </summary>
            /// <param name="pSDate">시작일</param>
            /// <param name="pEDate">종료일</param>
            public void MonthReturn(HtmlInputText pSDate, HtmlInputText pEDate)
            {
                DateTime nsdt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));
                DateTime nedt = DateTime.Now.AddMonths(1); DateTime.Parse(nedt.ToString("yyyy-MM-01")); nedt.AddDays(-1);
                pSDate.Value = nsdt.ToString();
                pEDate.Value = nedt.ToString();
            }
            #endregion

            #region 날짜를 오늘날짜와 비교- DateDiff
            /// <summary>
            /// 스트링으로 날짜형식비교 
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <returns>비교차</returns>
            public int DateDiff(string pDate)
            {
                string nDate = System.DateTime.Today.ToString("yyyy-MM-dd");

                int sValue = 0;

                try
                {
                    DateTime dsDate = DateTime.Parse(pDate);
                    DateTime dnDate = DateTime.Parse(nDate);

                    sValue = DateTime.Compare(dsDate, dnDate);
                }
                catch
                {
                    sValue = 1;
                }

                return sValue;
            }
            #endregion

            #region 두 날짜를 비교- DateDiff
            /// <summary>
            /// 스트링으로 날짜형식비교 
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <returns>비교차</returns>
            public int DateDiff(string pDate1, string pDate2)
            {
                int sValue = 0;

                try
                {
                    DateTime dsDate = DateTime.Parse(pDate1);
                    DateTime dnDate = DateTime.Parse(pDate2);

                    sValue = DateTime.Compare(dsDate, dnDate);
                }
                catch
                {
                    sValue = 1;
                }

                return sValue;
            }
            #endregion

            #region 다음달구하기 - MonthNext
            /// <summary>
            /// 다음달구하기
            /// </summary>
            /// <param name="pValue">날짜</param>
            /// <returns>날짜</returns>
            public DateTime MonthNext(DateTime pValue)
            {
                pValue.AddMonths(1);
                return DateTime.Parse(pValue.ToString("yyyy-MM-01"));
            }
            #endregion

            #region 콤보박스에 년도를 채워주는 함수 - DateYearListFill
            /// <summary>
            /// 콤보박스등에 년도를 채워주는 함수
            /// </summary>
            /// <param name="dropDownList">dropDownList 컨트롤</param>
            /// <param name="sYyDiff">시작년도차</param>
            /// <param name="eYyDiff">끝년도차</param>
            /// <param name="orderby">오름차순/내림차순</param>
            /// <param name="spaceYn">공백필드여부</param>
            /// <param name="deftValueYn">기본값여부</param>
            public void DateYearListFill(DropDownList pObj, int sYyDiff, int eYyDiff, SortingKind orderby, Selection spaceYn, Selection deftValueYn)
            {
                pObj.Items.Clear();
                if (spaceYn == Selection.Yes) pObj.Items.Add(new ListItem("", ""));

                if (orderby == SortingKind.Ascending)
                {
                    for (int iCnt = int.Parse(DateTime.Now.ToString("yyyy")) - sYyDiff; iCnt <= int.Parse(DateTime.Now.ToString("yyyy")) + eYyDiff; iCnt++)
                    {
                        pObj.Items.Add(new ListItem(iCnt.ToString() + "년", iCnt.ToString()));
                    }
                }
                else
                {
                    for (int iCnt = int.Parse(DateTime.Now.ToString("yyyy")) + eYyDiff; iCnt >= int.Parse(DateTime.Now.ToString("yyyy")) - sYyDiff; iCnt--)
                    {
                        pObj.Items.Add(new ListItem(iCnt.ToString() + "년", iCnt.ToString()));
                    }
                }

                if (deftValueYn == Selection.Yes) pObj.SelectedValue = DateTime.Now.ToString("yyyy");
            }
            #endregion

            #region 콤보박스에 월을 채워주는 함수- DateMonthFill
            /// <summary>
            /// 콤보박스에 월을 채워주는 함수            
            /// </summary>
            /// <param name="dropDownList">dropDownList 컨트롤</param>
            /// <param name="orderby">오름차순/내림차순</param>
            /// <param name="spaceYn">공백필드여부</param>
            /// <param name="deftValueYn">기본값여부</param>
            public void DateMonthFill(DropDownList dropDownList,SortingKind orderby, Selection spaceYn, Selection deftValueYn)
            {
                dropDownList.Items.Clear();
                if (spaceYn == Selection.Yes) dropDownList.Items.Add(new ListItem("", ""));

                if (orderby == SortingKind.Ascending)
                {
                    for (int iCnt = 1; iCnt <= 12; iCnt++)
                    {
                        dropDownList.Items.Add(new ListItem(iCnt.ToString() + "월", iCnt.ToString("00")));
                    }
                }
                else
                {
                    for (int iCnt = 12; iCnt >= 1; iCnt--)
                    {
                        dropDownList.Items.Add(new ListItem(iCnt.ToString() + "월", iCnt.ToString("00")));
                    }
                }

                if (deftValueYn == Selection.Yes) dropDownList.SelectedValue = DateTime.Now.ToString("MM");
            }
            #endregion

            #region 날짜범위리스트 - RangeToDrop
            /// <summary>
            /// 날짜범위리스트            
            /// </summary>
            /// <param name="pObj">DropDownList 컨트롤</param>
            /// <param name="pindex">기본값</param>
            public void RangeToDrop(System.Web.UI.WebControls.DropDownList pObj, int pindex)
            {
                pObj.Attributes.Add("OnChange", "javascript:PF_DateRange()");
                //pObj.Items.Add(new ListItem("--선택--", "^"));
                pObj.Items.Add(new ListItem("당일", Range("1")));
                pObj.Items.Add(new ListItem("당월", Range("NM")));
                pObj.Items.Add(new ListItem("당년", Range("NY")));
                pObj.Items.Add(new ListItem("최근 1주일", Range("7")));
                pObj.Items.Add(new ListItem("최근 10일", Range("10")));
                pObj.Items.Add(new ListItem("최근 1개월", Range(DateTime.Now.AddMonths(-1).Subtract(DateTime.Now).ToString())));
                pObj.Items.Add(new ListItem("최근 6개월", Range(DateTime.Now.AddMonths(-6).Subtract(DateTime.Now).ToString())));
                pObj.Items.Add(new ListItem("최근 1년", Range(DateTime.Now.AddYears(-1).Subtract(DateTime.Now).ToString())));
                pObj.SelectedIndex = pindex;
            }
            /// <summary>
            /// 날짜범위리스트
            /// </summary>
            /// <param name="pObj">DropDownList 컨트롤</param>
            /// <param name="pindex">기본값</param>
            /// <param name="sDate">시작일</param>
            /// <param name="eDate">종료일</param>
            public void RangeToDrop(System.Web.UI.WebControls.DropDownList pObj, int pindex,
                System.Web.UI.HtmlControls.HtmlInputText sDate, System.Web.UI.HtmlControls.HtmlInputText eDate)
            {
                RangeToDrop(pObj, pindex);
                string[] adate = pObj.SelectedValue.Split('^');
                sDate.Value = adate[0];
                eDate.Value = adate[1];
            }
            /// <summary>
            /// 날짜범위리스트
            /// </summary>
            /// <param name="pObj">DropDownList 컨트롤</param>
            /// <param name="pindex">기본값</param>
            /// <param name="sDate">시작일</param>
            /// <param name="eDate">종료일</param>
            /// <param name="pEmpty">공백여부</param>
            public void RangeToDrop(System.Web.UI.WebControls.DropDownList pObj, int pindex,
                System.Web.UI.HtmlControls.HtmlInputText sDate, System.Web.UI.HtmlControls.HtmlInputText eDate, bool pEmpty)
            {
                RangeToDrop(pObj, pindex);
                pObj.Items.Add(new ListItem("기간없음", "^"));

                if (pEmpty)
                {
                    sDate.Value = "";
                    eDate.Value = "";
                    pObj.SelectedIndex = 8;
                }
                else
                {
                    string[] adate = pObj.SelectedValue.Split('^');
                    sDate.Value = adate[0];
                    eDate.Value = adate[1];
                }
            }
            #endregion

            #region 날짜범위 - DateRange
            /// <summary>
            /// 날짜범위            
            /// </summary>
            /// <param name="pValue">값</param>
            /// <returns>결과값</returns>
            private string Range(string pValue)
            {
                string rtnData = "";
                switch (pValue)
                {
                    case "NM": //당월
                        string mm = DateTime.Now.Month.ToString();
                        if (mm.Length == 1) mm = "0" + mm;
                        rtnData += DateTime.Now.Year.ToString() + mm + "01^";
                        DateTime nedt = DateTime.Now.AddMonths(1); nedt = DateTime.Parse(nedt.Year + "-" + nedt.Month + "-01"); nedt = nedt.AddDays(-1);
                        rtnData += nedt.ToString("yyyyMMdd");
                        break;
                    case "NY": //당년
                        rtnData += DateTime.Now.Year.ToString() + "0101" + "^";
                        rtnData += DateTime.Now.Year.ToString() + "1231";
                        break;
                    default:   //기타
                        if (pValue.IndexOf(".") > 0) pValue = pValue.Substring(1, pValue.IndexOf(".") - 1);
                        rtnData += DateTime.Now.AddDays(1-int.Parse(pValue)).ToString("yyyyMMdd") + "^";
                        rtnData += DateTime.Now.ToString("yyyyMMdd");
                        break;
                }
                return rtnData.Replace("-","");
            }
            #endregion

            #region 한글요일 - WeekenKoreanName
            /// <summary>
            /// 한글요일            
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <returns>요일</returns>
            public string WeekenKoreanName(DateTime pDate)
            {
                string strDay = pDate.DayOfWeek.ToString();

                switch (strDay)
                {
                    case "Sunday": return "일요일";
                    case "Monday": return "월요일";
                    case "Tuesday": return "화요일";
                    case "Wednesday": return "수요일";
                    case "Thursday": return "목요일";
                    case "Friday": return "금요일";
                    case "Saturday": return "토요일";
                    default: return "none";
                }
            }
            #endregion

            #region 숫자요일 - WeekenNumber
            /// <summary>
            /// .기능명 : 숫자요일
            /// .작성자 : Leira
            /// .작성일 : 2008.11.27
            /// .수정일 : 
            /// .변  수 : pDate(날짜)
            /// .설  명 : 받은 데이트값을 숫자로 변경(일요일1~7)
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <returns>요일번호</returns>
            public Int16 WeekenNumber(DateTime pDate)
            {
                string strDay = pDate.DayOfWeek.ToString();

                switch (strDay)
                {
                    case "Sunday": return 1;
                    case "Monday": return 2;
                    case "Tuesday": return 3;
                    case "Wednesday": return 4;
                    case "Thursday": return 5;
                    case "Friday": return 6;
                    case "Saturday": return 7;
                    default: return 0;
                }
            }
            #endregion

            #region 한주일자배열반환 - WeekenArray
            /// <summary>
            /// 한주일자배열반환            
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <param name="DatePrint">일자출력기준</param>
            /// <returns>배열</returns>
            public string[] WeekenArray(string pDate, DatePrint DatePrint)
            {
                string[] arys = new string[7];
                DateTime dts = DateTime.Parse(pDate);
                int w = WeekenNumber(dts);
                dts = dts.AddDays(-(w - 1));

                for (int i = 0; i < 7; i++)
                {
                    switch (DatePrint)
                    {
                        case DatePrint.None:
                            arys[i] = dts.AddDays(i).ToString("yyyyMMdd");
                            break;
                        case DatePrint.Dash:
                            arys[i] = dts.AddDays(i).ToString("yyyy-MM-dd");
                            break;
                        case DatePrint.NoneWeek:
                            arys[i] = dts.AddDays(i).ToString("yyyyMMdd") + " " + WeekenKoreanName(dts.AddDays(i));
                            break;
                        case DatePrint.DashWeek:
                            arys[i] = dts.AddDays(i).ToString("yyyy-MM-dd") + " " + WeekenKoreanName(dts.AddDays(i));
                            break;
                        default:
                            arys[i] = "";
                            break;
                    }
                }
                return arys;
            }
            #endregion

            #region 한달일자배열반환 - MonthArray
            /// <summary>
            /// 한달일자배열반환
            /// </summary>
            /// <param name="pDate">날짜</param>
            /// <param name="DatePrint">일자출력기준</param>
            /// <returns></returns>
            public string[] MonthArray(string pDate, DatePrint DatePrint)
            {
                DateTime dts = DateTime.Parse(pDate);
                dts = DateTime.Parse(dts.ToString("yyyy-MM-01"));
                DateTime dte = DateTime.Parse(MonthLastDay(dts.ToString("yyyy-MM-dd")));

                string[] arys = new string[dte.Day - 1];

                for (int i = 0; i < arys.Length; i++)
                {
                    switch (DatePrint)
                    {
                        case DatePrint.None:
                            arys[i] = dts.AddDays(i).ToString("yyyyMMdd");
                            break;
                        case DatePrint.Dash:
                            arys[i] = dts.AddDays(i).ToString("yyyy-MM-dd");
                            break;
                        case DatePrint.NoneWeek:
                            arys[i] = dts.AddDays(i).ToString("yyyyMMdd") + " " + WeekenKoreanName(dts.AddDays(i));
                            break;
                        case DatePrint.DashWeek:
                            arys[i] = dts.AddDays(i).ToString("yyyy-MM-dd") + " " + WeekenKoreanName(dts.AddDays(i));
                            break;
                        default:
                            arys[i] = "";
                            break;
                    }
                }
                return arys;
            }
            #endregion

            #region 해당월마지막일자구하기 - MonthLastDay
            /// <summary>
            /// DD월마지막일자
            /// 해당월 마지막일자 구하기
            /// </summary>
            /// <param name="NowDate">날짜</param>
            /// <returns>값</returns>
            public string MonthLastDay(string NowDate)
            {
                DateTime nowDate = DateTime.Parse(NowDate);
                nowDate.AddMonths(1);
                nowDate = DateTime.Parse(nowDate.ToString("yyyy-MM-01"));
                return nowDate.AddDays(-1).ToString();
            }
            #endregion

            #region 전월마지막일자구하기 - MonthLastDayPrev
            /// <summary>
            /// DD직전월
            /// 전달 구하기
            /// </summary>
            /// <param name="NowDate">날짜</param>
            /// <returns>값</returns>
            public string MonthLastDayPrev(string NowDate)
            {
                DateTime nowDate = DateTime.Parse(NowDate);
                nowDate = DateTime.Parse(nowDate.ToString("yyyy-MM-01"));
                return nowDate.AddDays(-1).ToString();
            }
            #endregion

            #region 해당주가 년중 몇번째 주인가 - WeekOfYear
            // 주어진 날짜가 1년 중 몇 번째 주(week)인가를 반환한다.
            // CultureInfo.CurrentCulture를 사용하여 달력 규칙을 정한다.
            public int GetWeekOfYear(DateTime targetDate)
            {
                return GetWeekOfYear(targetDate, null);
            }

            // 주어진 날짜가 1년 중 몇 번째 주(week)인가를 반환한다.
            // 달력 규칙은 매개변수로 주어진 CultureInfo를 사용한다.
            public int GetWeekOfYear(DateTime targetDate, System.Globalization.CultureInfo culture)
            {
                if (culture == null)
                {
                    culture = System.Globalization.CultureInfo.CurrentCulture;
                }
                System.Globalization.CalendarWeekRule weekRule = culture.DateTimeFormat.CalendarWeekRule;
                DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
                return culture.Calendar.GetWeekOfYear(targetDate, weekRule, firstDayOfWeek);
            }
            #endregion

            #region 해당주가 월중 몇번째 주인가 - WeekOfMonth
            public int GetWeekOfMonth(DateTime targetDate)
            {
                return GetWeekOfMonth(targetDate, null);
            }

            public int GetWeekOfMonth(DateTime targetDate, System.Globalization.CultureInfo culture)
            {
                if (culture == null)
                {
                    culture = System.Globalization.CultureInfo.CurrentCulture;
                }
                DateTime beginningOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);

                while (targetDate.Date.AddDays(1).DayOfWeek != culture.DateTimeFormat.FirstDayOfWeek)
                    targetDate = targetDate.AddDays(1);

                return (int)Math.Truncate((double)targetDate.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
            }
            #endregion

            #region 해당주차 시작일
            public DateTime GetFirstDayOfWeek(int year, int weekNumber)
            {
                return GetFirstDayOfWeek(year, weekNumber, null);
            }

            public DateTime GetFirstDayOfWeek(int year, int weekNumber, System.Globalization.CultureInfo culture)
            {
                if (culture == null)
                {
                    culture = System.Globalization.CultureInfo.CurrentCulture;
                }
                System.Globalization.Calendar calendar = culture.Calendar;
                DateTime firstOfYear = new DateTime(year, 1, 1, calendar);
                DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber - 1);
                DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

                while (targetDay.DayOfWeek != firstDayOfWeek)
                {
                    targetDay = targetDay.AddDays(-1);
                }

                return targetDay;
            }
            #endregion
        }
        #endregion

        #region Function - 시스템관련함수
        /// <summary>
        /// 시스템관련함수
        /// </summary>
        public class Systems
        {
            
        }
        #endregion

        #region Function - 컨트롤관련함수
        /// <summary>
        /// 컨트롤관련함수
        /// </summary>
        public class Ctrols
        {
        }
        #endregion

        #region Function - Page관련함수
        /// <summary>
        /// Page관련함수
        /// </summary>
        public class Pages
        {
           
        }
        #endregion

        #region Function - 엑셀관련함수
        /// <summary>
        /// 엑셀관련함수
        /// </summary>
        public class Excels
        {
            #region 엑셀파일가져오기 - RetrieveExcelFile
            /// <summary>
            /// 엑셀파일가져오기
            /// </summary>
            /// <param name="pExcelFileName">파일경로명(서버내의 로컬경로)</param>
            /// <param name="pSheetNames"> 읽을 Sheet명에 대한 배열</param>
            /// <returns>DataSet</returns>
            public DataSet RetrieveExcelFile(string pExcelFileName, string[] pSheetNames)
            {   
                DataSet ds = new DataSet();

                OleDbConnection conn = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=Excel 8.0;", pExcelFileName));
                conn.Open();

                for (int i = 0; i < pSheetNames.Length; i++)
                {
                    OleDbDataAdapter DBAdapter = new OleDbDataAdapter(String.Format("Select * from [{0}$]", pSheetNames[i]), conn);                    

                    DBAdapter.Fill(ds, pSheetNames[i]);
                }
                conn.Close();

                return ds;
            }
            /// <summary>
            /// 엑셀파일가져오기
            /// </summary>
            /// <param name="pExcelFileName">파일경로명(서버내의 로컬경로)</param>
            /// <returns>DataSet</returns>
            public DataSet RetrieveExcelFile(string pExcelFileName)
            {                
                DataSet ds = new DataSet();

                OleDbConnection conn = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=Excel 8.0;", pExcelFileName));
                conn.Open();

                //Sheet명 구하기
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                string sheetname = schemaTable.Rows[0]["Table_Name"].ToString();
                string excQuery = String.Format(@"Select * from [{0}]", sheetname);
                
                OleDbDataAdapter DBAdapter = new OleDbDataAdapter(excQuery, conn);
                DBAdapter.Fill(ds, sheetname);
                
                conn.Close();

                return ds;
            }
            /// <summary>
            /// 엑셀파일가져오기(Excel 2007)
            /// </summary>
            /// <param name="pExcelFileName">파일경로명(서버내의 로컬경로)</param>
            /// <returns></returns>
            public DataSet RetrieveExcelFileUpVersion(string pExcelFileName)
            {
                DataSet ds = new DataSet();

                OleDbConnection conn = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=Excel 8.0;HDR=Yes;IMEX=1", pExcelFileName));
                conn.Open();

                //Sheet명 구하기
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                string sheetname = schemaTable.Rows[0]["Table_Name"].ToString();
                string excQuery = String.Format(@"Select * from [{0}]", sheetname);

                OleDbDataAdapter DBAdapter = new OleDbDataAdapter(excQuery, conn);
                DBAdapter.Fill(ds, sheetname);

                conn.Close();

                return ds;
            }
            #endregion
        }
        #endregion

        #region Function - Global Resource 관련함수
        public class GlobalResource
        {
            string m_sResourceFileID = "gResource";

            public GlobalResource() {}
            public GlobalResource(string resourceID) { m_sResourceFileID = resourceID; }

            public string GetResource(string key)
            {
                return (String)HttpContext.GetGlobalResourceObject(m_sResourceFileID, key);
            }
        }
        #endregion
    }

    public static class StaticFunctions
    {
        #region Function - Global Resource 관련함수
        public static class staticGlobalResource
        {
            public static string m_sResourceFileID = "gResource";

            public static string GetResource(string key)
            {
                return (String)HttpContext.GetGlobalResourceObject(m_sResourceFileID, key);
            }

            public static string GetResource(string key, string digit, int len)
            {
                string msg = (String)HttpContext.GetGlobalResourceObject(m_sResourceFileID, key);

                return String.Format("{0}{1}{2}",
                    msg.Substring(0, len),
                    digit,
                    msg.Substring(len - 1));
            }
        }
        #endregion

        #region Function - Data 관련함수
        public static class staticData
        {
            public static DataTable GetGroupedBy(DataTable dt, string columnNamesInDt, string groupByColumnNames, string typeOfCalculation)
            {
                //Return its own if the column names are empty
                if (columnNamesInDt == string.Empty || groupByColumnNames == string.Empty)
                {
                    return dt;
                }

                //Once the columns are added find the distinct rows and group it bu the numbet
                DataTable _dt = dt.DefaultView.ToTable(true, groupByColumnNames.Split(','));

                //The column names in data table
                string[] _columnNamesInDt = columnNamesInDt.Split(',');

                for (int i = 0; i < _columnNamesInDt.Length; i++)
                {
                    if (!groupByColumnNames.Contains(_columnNamesInDt[i]))
                    {
                        _dt.Columns.Add(_columnNamesInDt[i]);
                    }
                }

                //Gets the collection and send it back
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    for (int j = 0; j < _columnNamesInDt.Length; j++)
                    {
                        if (!groupByColumnNames.Contains(_columnNamesInDt[j]))
                        {
                            _dt.Rows[i][j] = dt.Compute(String.Format("{0}({1})", typeOfCalculation, _columnNamesInDt[j]), String.Format("{0} = '{1}'", groupByColumnNames.Split(',')[0], _dt.Rows[i][groupByColumnNames.Split(',')[0]]));
                        }
                    }
                }

                return _dt;
            }
        }
        #endregion
    }
}
