using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// UploadHelper의 요약 설명입니다.
    /// </summary>
    public class UploadHelper
    {
        private string _baseDirectory;
        private int _maxUploadSize;
        private string _saveFileName = String.Empty;
        private bool _overWrite = false;

        public UploadHelper() { }

        public UploadHelper(string baseDir)
        {
            this._baseDirectory = baseDir;
        }

        #region UploadFile
        public string UploadFile(HttpPostedFile file)
        {
            string dir = GetDirectory();
            string filePath = Path.Combine(
                dir,
                String.Format("{0}{1}", !_saveFileName.Equals("") ? _saveFileName : Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName)));
            // 업로드 가능여부체크
            if (!bDoUploadFile(Path.GetExtension(file.FileName)))
            {
                throw new Exception("can't upload file extension");
            }
            // 제한용량 체크
            if (this.MaxUploadSize > 0)
            {
                if (file.ContentLength / 1024 > this.MaxUploadSize)
                    throw new Exception(string.Format("exceed allow size(max:{0}KB)", this.MaxUploadSize));
            }
            if (!_overWrite)
            {
                int seq = 0;
                // 중복체크
                while (File.Exists(filePath))
                {
                    filePath = Path.Combine(
                        dir,
                        string.Format("{0}_({1}){2}", !_saveFileName.Equals("") ? _saveFileName : Path.GetFileNameWithoutExtension(file.FileName), ++seq, Path.GetExtension(file.FileName))
                        );
                }
            }

            // 프로세스에 파일이 Lock되는 현상을 막기위해 파일스트림을 이용한다
            SaveFile(file, filePath);

            return GetSubDirectory() + Path.GetFileName(filePath);
        }

        public string UploadFileWithOutSub(HttpPostedFile file)
        {
            string dir = GetDirectoryWithOutSub();
            string filePath = Path.Combine(
                dir,
                String.Format("{0}{1}", !_saveFileName.Equals("") ? _saveFileName : Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName)));

            // 업로드 가능여부체크
            if (!bDoUploadFile(Path.GetExtension(file.FileName)))
            {
                throw new Exception("can't upload file extension");
            }

            // 제한용량 체크
            if (this.MaxUploadSize > 0)
            {
                if (file.ContentLength / 1024 > this.MaxUploadSize)
                    throw new Exception(string.Format("exceed allow size(max:{0}KB)", this.MaxUploadSize));
            }
            if (!_overWrite)
            {
                int seq = 0;
                // 중복체크
                while (File.Exists(filePath))
                {
                    filePath = Path.Combine(
                        dir,
                        string.Format("{0}_({1}){2}", !_saveFileName.Equals("") ? _saveFileName : Path.GetFileNameWithoutExtension(file.FileName), ++seq, Path.GetExtension(file.FileName))
                        );
                }
            }
            
            // 프로세스에 파일이 Lock되는 현상을 막기위해 파일스트림을 이용한다
            SaveFile(file, filePath);
            
            return Path.GetFileName(filePath);
        }
        #endregion

        #region 파일업로드 시 파일 Lock되는 현상을 막기위해 FileStream을 이용해 저장한다
        void SaveFile(HttpPostedFile file, string filePath)
        {
            // 프로세스에 파일이 Lock되는 현상을 막기위해 파일스트림을 이용한다
            int nFileLen = file.ContentLength;
            byte[] bFileData = new byte[nFileLen];

            file.InputStream.Read(bFileData, 0, file.ContentLength);

            // Create a file
            FileStream newFile = new FileStream(filePath, FileMode.Create);

            // Write data to the file
            BinaryWriter binWriter = new BinaryWriter(newFile);
            binWriter.Write(bFileData);

            // Close file
            binWriter.Close();

            // Dispose
            newFile.Dispose();
        }
        #endregion

        #region MakeThumbNail
        public string MakeThumbNail(string originFilePath, int width, int height)
        {
            try
            {
                using (Image photoImg = Image.FromFile(originFilePath))
                {
                    // height가 0일경우 width의 비례대로 설정
                    if (height == 0)
                    {
                        height = Convert.ToInt32(photoImg.Height / (photoImg.Width / Convert.ToSingle(width)));
                    }
                    using (Image thumbPhoto = photoImg.GetThumbnailImage(width, height, null, new System.IntPtr()))
                    {
                        int seq = 0;
                        string dir = GetDirectory();
                        string filePath = Path.Combine(dir, string.Format(
                            "{0}_thumb{1}",
                            Path.GetFileNameWithoutExtension(originFilePath),
                            Path.GetExtension(originFilePath)
                        ));
                        // 중복체크
                        while (File.Exists(filePath))
                        {
                            filePath = Path.Combine(
                                dir,
                                string.Format("{0}_thumb({1}){2}", Path.GetFileNameWithoutExtension(originFilePath), ++seq, Path.GetExtension(originFilePath))
                                );
                        }
                        thumbPhoto.Save(filePath, ImageFormat.Png);
                        return GetSubDirectory() + Path.GetFileName(filePath);
                    }
                }
            }
            catch (Exception exp)
            {

                LogHelper.LogWrite(exp);
                return "";
            }
        }
        #endregion

        #region MakeHQThumbImage
        public string MakeHQThumbImage(string originFilePath, int width, int height, string type)
        {
            try
            {
                Image photoImg = Image.FromFile(originFilePath);

                int srcWidth = photoImg.Width;
                int srcHeight = photoImg.Height;
                int thumbWidth = width;
                int thumbHeight = height;
                Decimal sizeRatio;

                // width가 0일경우 height의 비례대로 설정
                if (width == 0)
                {
                    sizeRatio = ((Decimal)srcWidth / srcHeight);
                    thumbWidth = Decimal.ToInt32(sizeRatio * thumbHeight);
                }

                // height가 0일경우 width의 비례대로 설정
                if (height == 0)
                {
                    sizeRatio = ((Decimal)srcHeight / srcWidth);
                    thumbHeight = Decimal.ToInt32(sizeRatio * thumbWidth);
                }

                Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);

                Graphics gr = Graphics.FromImage(bmp);
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.High;
                Rectangle rectDestination = new Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(photoImg, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

                string dir = GetDirectoryWithOutSub();
                string filePath = Path.Combine(dir, string.Format(
                    "{0}{2}{1}",
                    Path.GetFileNameWithoutExtension(originFilePath),
                    Path.GetExtension(originFilePath),
                    type.Equals("") ? "" : String.Format("_{0}", type)
                ));

                if (!_overWrite)
                {
                    int seq = 0;

                    // 중복체크
                    while (File.Exists(filePath))
                    {
                        filePath = Path.Combine(
                            dir,
                            string.Format("{0}{3}({1}){2}", Path.GetFileNameWithoutExtension(originFilePath), ++seq, Path.GetExtension(originFilePath), type.Equals("") ? "" : String.Format("_{0}", type))
                            );
                    }
                }
                bmp.Save(filePath);
                bmp.Dispose();
                photoImg.Dispose();

                return Path.GetFileName(filePath);
            }
            catch (Exception exp)
            {
                LogHelper.LogWrite(exp);
                return "";
            }
        }
        #endregion

        #region GetSubDirectory
        private string GetSubDirectory()
        {
            return string.Format("{0}/{1}/{2}/",
                DateTime.Now.Year.ToString(),
                DateTime.Today.Month.ToString().PadLeft(2, '0'),
                DateTime.Today.Day.ToString().PadLeft(2, '0')
                );
        }
        #endregion

        #region GetDirectory
        private string GetDirectory()
        {
            string directory = this._baseDirectory;
            // Directory 생성
            directory += GetSubDirectory();
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            return directory;
        }

        private string GetDirectoryWithOutSub()
        {
            string directory = this._baseDirectory;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            return directory;
        }
        #endregion

        #region UploadHelper Property
        public string BaseDirectory
        {
            get { return _baseDirectory; }
            set { this._baseDirectory = value; }
        }

        public int MaxUploadSize
        {
            get { return _maxUploadSize; }
            set { this._maxUploadSize = value; }
        }

        public string saveFileName
        {
            get { return _saveFileName; }
            set { this._saveFileName = value; }
        }

        public bool overWrite
        {
            get { return _overWrite; }
            set { this._overWrite = value; }
        }
        #endregion

        #region 선택된 파일을 실제로 삭제한다.
        public bool Delete(string virtual_path)
        {
            bool bDelete = false;

            try
            {
                FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(virtual_path));
                fi.Delete();

                bDelete = true;
            }
            catch (Exception e)
            {
                LogHelper.LogWrite(e);
            }

            return bDelete;
        }
        #endregion

        #region 해당 파일의 업로드를 금지한다
        bool bDoUploadFile(string ext)
        {
            string[] pArgs1 = { ".asp", ".aspx", ".exe", ".com", ".bat", ".js", ".vbs" };

            for (int i = 0; i < pArgs1.Length; i++)
            {
                if (ext == pArgs1[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 이미지 여부를 판단한다.
        public bool bCheckImageFile(string ext)
        {
            ext = ext.ToLower();
            string[] pArgs1 = { ".bmp", ".jpg", ".gif", ".png" };

            for (int i = 0; i < pArgs1.Length; i++)
            {
                if (ext == pArgs1[i])
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 파일크기를 구한다
        public string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }
        #endregion

        #region 이미지 선택영역 Crop
        public string Crop(string originFilePath, int width, int height, int x, int y, string type)
        {
            try
            {
                Image photoImg = Image.FromFile(originFilePath);

                int thumbWidth = width;
                int thumbHeight = height;

                Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);

                Graphics gr = Graphics.FromImage(bmp);
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.High;
                Rectangle rectDestination = new Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(photoImg, rectDestination, x, y, thumbWidth, thumbHeight, GraphicsUnit.Pixel);

                string dir = GetDirectoryWithOutSub();
                string filePath = Path.Combine(dir, string.Format(
                    "{0}{2}{1}",
                    Path.GetFileNameWithoutExtension(originFilePath),
                    Path.GetExtension(originFilePath),
                    type.Equals("") ? "" : String.Format("_{0}", type)
                ));

                if (!_overWrite)
                {
                    int seq = 0;

                    // 중복체크
                    while (File.Exists(filePath))
                    {
                        filePath = Path.Combine(
                            dir,
                            string.Format("{0}{3}({1}){2}", Path.GetFileNameWithoutExtension(originFilePath), ++seq, Path.GetExtension(originFilePath), type.Equals("") ? "" : String.Format("_{0}", type))
                            );
                    }
                }
                bmp.Save(filePath);
                bmp.Dispose();
                photoImg.Dispose();

                return Path.GetFileName(filePath);
            }
            catch (Exception exp)
            {
                LogHelper.LogWrite(exp);
                return "";
            }
        }
        #endregion
    }
}