using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace SPC.WebUI.API.Common.Android
{
    
    public class Mimgupload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // 업로드할 파일의 정의 'uploadedfile'은 안드로이드 소스에서 지정한 이름
            HttpPostedFile fp = context.Request.Files["uploadedfile"];
            // 저장할 폴더의 절대경로
            string targetDirectory = @"D:\UploadedImages";

            string targetChk = targetDirectory;


            
            //전송받은 파일 이름으로 저장
            string[] strFilename = fp.FileName.Split('/');

            string strname = strFilename[strFilename.Length - 1];
            string[] strUserdic = strname.Split('_');
            string[] strUsername = strUserdic[1].Split('.');


            DirectoryInfo di = new DirectoryInfo(targetChk + "\\" + strUsername[0]);
            


            if (di.Exists == false)
            {
                di.Create();
            }

            targetDirectory = targetDirectory + "\\" + strUsername[0] + "\\" + strFilename[strFilename.Length - 1];
            try
            {
                // File을 서버에 지정한 이름으로 저장함
                fp.SaveAs(targetDirectory);
                
            }
            catch (Exception e)
            {
                // 에러 발생 처리
                context.Response.Write(e.Message);
                return;
            }

            // 업로드 성공
            context.Response.Write("Uploaded =" + targetDirectory);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}