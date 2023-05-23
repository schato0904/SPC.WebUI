using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// LogHelper의 요약 설명입니다.
    /// </summary>
    public class LogHelper
    {
        #region Log File Directory
        public static string logMapPath = String.Format(CommonHelper.GetAppSectionsString("subPath"), CommonHelper.GetAppSectionsString("defaultFileLog"));
        #endregion

        #region Log Writer For Application
        public static void LogWrite(string strMessage)
        {
            string logTodayMapPath = String.Format("{0}{1}.txt", logMapPath, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            HttpContext current = HttpContext.Current;

            sb.AppendLine("================================================================================");
            sb.AppendLine(" Error Generation Time : ");
            sb.AppendLine(errTime);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage       : ");
            sb.AppendLine(strMessage);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public static void LogWrite(Exception ex)
        {
            string logTodayMapPath = String.Format("{0}{1}.txt", logMapPath, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            sb.AppendLine("================================================================================");
            sb.AppendLine(" Error Generation Time : ");
            sb.AppendLine(errTime);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" Error Type : ");
            sb.AppendLine(ex.GetType().ToString());
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" Error StackTrace : ");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage : ");
            sb.AppendLine(ex.Message);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public static void LogWrite(Exception ex, string preface)
        {
            string logTodayMapPath = String.Format(@"{0}{1}_{2}.txt", logMapPath, preface, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            sb.AppendLine("================================================================================");
            sb.AppendLine(" Error Generation Time : ");
            sb.AppendLine(errTime);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" Error Type : ");
            sb.AppendLine(ex.GetType().ToString());
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" Error StackTrace : ");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage : ");
            sb.AppendLine(ex.Message);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region Log Writer For Web
        public static void LogWriteWeb(string strMessage)
        {
            string logTodayMapPath = String.Format(@"{0}{1}.txt", logMapPath, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            HttpContext current = HttpContext.Current;

            sb.AppendLine("================================================================================");
            sb.AppendLine(errTime);
            sb.AppendLine(String.Format(" RawUrl             : {0}", HttpContext.Current.Request.RawUrl));
            sb.AppendLine(String.Format(" User               : {0}", HttpContext.Current.User.Identity.Name));
            sb.AppendLine(String.Format(" IP                 : {0}", CommonHelper.GetIP4Address()));
            sb.AppendLine(String.Format(" UserAgent          : {0}", HttpContext.Current.Request.UserAgent));
            sb.AppendLine(String.Format(" ServerVariables    : {0}", HttpContext.Current.Request.ServerVariables));
            sb.AppendLine(String.Format(" Form               : {0}", HttpContext.Current.Request.Form));
            sb.AppendLine(String.Format(" Cookies            : {0}", HttpContext.Current.Request.Cookies));
            sb.AppendLine(String.Format(" QueryString        : {0}", HttpContext.Current.Request.QueryString));
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage       : ");
            sb.AppendLine(strMessage);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public static void LogWriteWeb(Exception ex)
        {
            string logTodayMapPath = String.Format(@"{0}{1}.txt", logMapPath, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            sb.AppendLine("================================================================================");
            sb.AppendLine(errTime);
            sb.AppendLine(String.Format(" RawUrl             : {0}", HttpContext.Current.Request.RawUrl));
            sb.AppendLine(String.Format(" User               : {0}", HttpContext.Current.User.Identity.Name));
            sb.AppendLine(String.Format(" IP                 : {0}", CommonHelper.GetIP4Address()));
            sb.AppendLine(String.Format(" UserAgent          : {0}", HttpContext.Current.Request.UserAgent));
            sb.AppendLine(String.Format(" ServerVariables    : {0}", HttpContext.Current.Request.ServerVariables));
            sb.AppendLine(String.Format(" Form               : {0}", HttpContext.Current.Request.Form));
            sb.AppendLine(String.Format(" Cookies            : {0}", HttpContext.Current.Request.Cookies));
            sb.AppendLine(String.Format(" QueryString        : {0}", HttpContext.Current.Request.QueryString));
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" nStackTrace       : ");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage       : ");
            sb.AppendLine(ex.Message);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public static void LogWriteWeb(Exception ex, string preface)
        {
            string logTodayMapPath = String.Format(@"{0}{1}_{2}.txt", logMapPath, preface, DateTime.Today.ToString("yyyyMMdd"));

            StringBuilder sb = new StringBuilder(2048);
            string errTime = String.Format("[{0:yyyyMMddHHssmm}]", DateTime.Now);

            sb.AppendLine("================================================================================");
            sb.AppendLine(errTime);
            sb.AppendLine(String.Format(" RawUrl             : {0}", HttpContext.Current.Request.RawUrl));
            sb.AppendLine(String.Format(" User               : {0}", HttpContext.Current.User.Identity.Name));
            sb.AppendLine(String.Format(" IP                 : {0}", CommonHelper.GetIP4Address()));
            sb.AppendLine(String.Format(" UserAgent          : {0}", HttpContext.Current.Request.UserAgent));
            sb.AppendLine(String.Format(" ServerVariables    : {0}", HttpContext.Current.Request.ServerVariables));
            sb.AppendLine(String.Format(" Form               : {0}", HttpContext.Current.Request.Form));
            sb.AppendLine(String.Format(" Cookies            : {0}", HttpContext.Current.Request.Cookies));
            sb.AppendLine(String.Format(" QueryString        : {0}", HttpContext.Current.Request.QueryString));
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" nStackTrace       : ");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(" ErrorMessage       : ");
            sb.AppendLine(ex.Message);
            sb.AppendLine("================================================================================");

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logMapPath);
                if (!di.Exists) di.Create();

                System.IO.FileStream logFile = new System.IO.FileStream(logTodayMapPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}