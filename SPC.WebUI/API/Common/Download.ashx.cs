using System;
using System.Collections.Generic;
using System.Web;

using System.IO;
using System.Data;
using ICSharpCode.SharpZipLib.Zip;
using CTF.Web.Framework.Helper;
using SPC.Common.Biz;

namespace SPC.WebUI.API.Common
{
    /// <summary>
    /// Download의 요약 설명입니다.
    /// </summary>
    public class Download : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string attfileno = context.Request.Params["attfileno"];
            string attfileseq = context.Request.Params["attfileseq"];
            string data_gbn = context.Request.Params["data_gbn"];
            string compcd = context.Request.Params["compcd"];
            string errMsg = String.Empty;

            try
            {
                if (!String.IsNullOrEmpty(attfileseq))
                {
                    OneDownload(attfileno, attfileseq, data_gbn, compcd);
                }
                else
                {
                    MuntiZipDownload(attfileno, data_gbn, compcd);
                }
            }
            catch (Exception e)
            {
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();

                LogHelper.LogWrite(e);
                response.Write(String.Format("<script type=\"text/javascript\">alert('{0}'); window.open('', '_self', ''); window.close();</script>", e.Message));
                
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        void OneDownload(string attfileno, string attfileseq, string data_gbn, string COMPCD)
        {
            DataSet ds = null;

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Add("ATTFILENO", attfileno);
                oParamDic.Add("ATTFILESEQ", attfileseq);
                oParamDic.Add("DATA_GBN", data_gbn);
                ds = biz.GetATTFILE_LST(oParamDic, out errMsg);
            }

            if ((ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
            {
                DataRow dtRow = ds.Tables[0].Rows[0];

                string fullPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), COMPCD, data_gbn);
                string downfile = String.Format("{0}{1}", fullPath, dtRow["DATA_NAME"]);

                if (!File.Exists(downfile))
                {
                    throw new Exception("서버에서 파일이 삭제되었거나 찾을 수 없습니다");
                }
                else
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = CommonHelper.GetMimeType((string)dtRow["DATA_EXTN"]);
                    response.AddHeader("Content-Disposition", String.Format("attachment; filename={0};", dtRow["DATA_ORIGIN_NAME"]));
                    response.TransmitFile(String.Format("{0}{1}", fullPath, dtRow["DATA_NAME"]));
                    response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //response.End();
                }
            }
        }

        void MuntiZipDownload(string attfileno, string data_gbn, string COMPCD)
        {
            DataSet ds = null;

            string errMsg = String.Empty;

            using (CommonBiz biz = new CommonBiz())
            {
                Dictionary<string, string> oParamDic = new Dictionary<string, string>();
                oParamDic.Add("ATTFILENO", attfileno);
                oParamDic.Add("DATA_GBN", data_gbn);
                ds = biz.GetATTFILE_LST(oParamDic, out errMsg);
            }

            if ((ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
            {
                string fullPath = String.Format(CommonHelper.GetAppSectionsString("defaultCompSubPath"), COMPCD, data_gbn);
                
                // 파일존재유무확인
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    string downfile = String.Format("{0}{1}", fullPath, dtRow["DATA_NAME"]);

                    if (!File.Exists(downfile))
                    {
                        throw new Exception("서버에서 파일이 삭제되었거나 찾을 수 없습니다");
                    }
                }
                
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.zip", attfileno));
                response.ContentType = "application/zip";

                using (var zipStream = new ZipOutputStream(response.OutputStream))
                {
                    foreach (DataRow dtRow in ds.Tables[0].Rows)
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(String.Format("{0}{1}", fullPath, dtRow["DATA_NAME"]));

                        var fileEntry = new ZipEntry(Path.GetFileName(String.Format("{0}{1}", fullPath, dtRow["DATA_NAME"])))
                        {
                            Size = fileBytes.Length
                        };

                        zipStream.PutNextEntry(fileEntry);
                        zipStream.Write(fileBytes, 0, fileBytes.Length);
                    }

                    zipStream.Flush();
                    zipStream.Close();
                }
            }
        }
    }
}