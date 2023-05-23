using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SPC.WebUI.Common.Library
{    
    /// <summary>
    /// 컨트롤 확장메소드
    /// </summary>
    public static class ControlExtension
    {
        static ControlExtension()
        {
            ImageTypes = new Dictionary<string, string>();
            ImageTypes.Add("FFD8","jpg");
            ImageTypes.Add("424D","bmp");
            ImageTypes.Add("474946","gif");
            ImageTypes.Add("89504E470D0A1A0A","png");
        }
        #region 확장메소드

        #region Dictionary
        /// <summary>
        /// GetValue
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            if (source != null && source.ContainsKey(key))
                return source[key];

            return default(TValue);
        }
        /// <summary>
        /// GetString
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, string nullResult = "")
        {
            if (source != null && source.ContainsKey(key) && source[key] != null)
                return source[key].ToString();

            return nullResult;
        }
        #endregion

        #region String
        /// <summary>
        /// 문자열이 다음 중 하나와 일치하는 지 확인
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="strs">비교 대상 문자열 목록</param>
        /// <returns></returns>
        public static bool In(this string str, params string[] strs)
        {
            return strs.Any(x => x == str);
        }
        #endregion

        #endregion

        /// <summary>
        ///     <para> Registers a hexadecimal value used for a given image type </para>
        ///     <param name="imageType"> The type of image, example: "png" </param>
        ///     <param name="uniqueHeaderAsHex"> The type of image, example: "89504E470D0A1A0A" </param>
        /// </summary>
        public static void RegisterImageHeaderSignature(string imageType, string uniqueHeaderAsHex)
        {
            Regex validator = new Regex(@"^[A-F0-9]+$", RegexOptions.CultureInvariant);

            uniqueHeaderAsHex = uniqueHeaderAsHex.Replace(" ", "");

            if (string.IsNullOrWhiteSpace(imageType))         throw new ArgumentNullException("imageType");
            if (string.IsNullOrWhiteSpace(uniqueHeaderAsHex)) throw new ArgumentNullException("uniqueHeaderAsHex");
            if (uniqueHeaderAsHex.Length % 2 != 0)            throw new ArgumentException    ("Hexadecimal value is invalid");
            if (!validator.IsMatch(uniqueHeaderAsHex))        throw new ArgumentException    ("Hexadecimal value is invalid");

            ImageTypes.Add(uniqueHeaderAsHex, imageType);
        }

        private static Dictionary<string, string> ImageTypes;

        public static bool IsImage(this Stream stream)
        {
            string imageType;
            return stream.IsImage(out imageType);
        }

        public static bool IsImage(this Stream stream, out string imageType)
        {
            stream.Seek(0, SeekOrigin.Begin);
            StringBuilder builder = new StringBuilder();
            int largestByteHeader = ImageTypes.Max(img => img.Value.Length);

            for (int i = 0; i < largestByteHeader; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                builder.Append(bit);

                string builtHex = builder.ToString();
                bool isImage = ImageTypes.Keys.Any(img => img == builtHex);
                if (isImage)
                {
                    imageType = ImageTypes[builder.ToString()];
                    return true;
                }
            }
            imageType = null;
            return false;
        }
    }
}
