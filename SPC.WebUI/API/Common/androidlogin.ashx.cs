using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using CTF.Web.Framework.Helper;
using System.Threading;
using SPC.User.Biz;
using System.Globalization;
using System.Web.Security;
using SPC.Common.Biz;
using SPC.WebUI.Common;
using SPC.PFRC.Biz;


namespace SPC.WebUI.API.Common
{
    
    public class androidlogin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // 업로드할 파일의 정의 'uploadedfile'은 안드로이드 소스에서 지정한 이름
            HttpPostedFile fp = context.Request.Files["uploadedfile"];
            // 저장할 폴더의 절대경로
            string targetDirectory = @"D:\UploadedImages"; //context.Request.PhysicalApplicationPath + "UploadedImages"; 가상경로를 인식하지 못하여 직접 경로 설정

            // 테스트를 위해 파일이름은 전송받은 파일 이름으로 저장
            string[] strFilename = fp.FileName.Split('/');
            //targetDirectory = targetDirectory + "\\" + strFilename[strFilename.Length - 1];
            try
            {
                // File을 서버에 지정한 이름으로 저장함
                //fp.SaveAs(targetDirectory);

                context.Response.Write("로그인 요청 성공");


            }
            catch (Exception e)
            {
                // 에러 발생 처리
                context.Response.Write(e.Message);
                return;
            }

            // 업로드 성공
            //context.Response.Write("Uploaded =" + targetDirectory);
            context.Response.Write("testis");

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