using System;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// ImpersonateHelper의 요약 설명입니다.
    /// </summary>
    public class ImpersonateHelper : IDisposable
    {
        // Intended for users who will be interactively using the computer.
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        // Use the standard logon provider for the system.
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        // Private members:
        // Holds windows impersonation context for this instance of the object.  This 
        // is also held here so we can undo impersonation by using this handle.
        private System.Security.Principal.WindowsImpersonationContext m_ImpersonationContext;

        // Attempts to log a user on to the local computer. The local computer is the 
        // computer from which LogonUser was called. You cannot use LogonUser to log on 
        // to a remote computer.
        // Additional documentation here:
        // http://msdn.microsoft.com/en-us/library/aa378184(VS.85).aspx
        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        // Creates a new access token that duplicates one already in existence.
        // Additional documentation here:
        // http://msdn.microsoft.com/en-us/library/aa446616(VS.85).aspx
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        // Terminates the impersonation of a client application.
        // Additional documentation here:
        // http://msdn.microsoft.com/en-us/library/aa379317(VS.85).aspx
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        // Closes an open object handle.
        // Additional documentation here:
        // http://msdn.microsoft.com/en-us/library/ms724211(VS.85).aspx
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        // Impersonates the user specified by sUserName, sDomain, and sPassword.
        public bool ImpersonateUser(String sUserName, String sDomain, String sPassword)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            // Remove the current impersonation by calling RevertToSelf()
            if (RevertToSelf())
            {
                // If user successfully logs in, set up their impersonation.
                if (LogonUserA(
                    sUserName,
                    sDomain,
                    sPassword,
                    LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT,
                    ref token) != 0)
                {
                    // Make a copy of the token for the windows identity private member.
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        // set the private member for the current impersonation context.
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        m_ImpersonationContext = tempWindowsIdentity.Impersonate();
                        if (m_ImpersonationContext != null)
                        {
                            // close handles to the tokens we just created.
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }

            if (token != IntPtr.Zero)
            {
                // close handle if we created it.
                CloseHandle(token);
            }

            if (tokenDuplicate != IntPtr.Zero)
            {
                // close handle if we created it.
                CloseHandle(tokenDuplicate);
            }

            // impersonation or login must have failed so return false.
            return false;
        }

        // Remove tthe current impersonation to revert the context back to
        // it's default impersonation login.
        public void UndoImpersonation()
        {
            m_ImpersonationContext.Undo();
        }

        public void Dispose()
        {
            if (m_ImpersonationContext != null)
            {
                m_ImpersonationContext.Dispose();
                m_ImpersonationContext = null;
            }
        }

        // 사용방법
        //// Create new instance of our new ImpersonateManager wrapper class.
        //ImpersonateManager impersonate = new ImpersonateManager();
        //if (impersonate.ImpersonateUser("MyUserName", "MyWindowsDomain", "MyPassword"))
        //{
        //    // do some more privileged operation here...
 
        //    // remove impersonation now
        //    impersonate.UndoImpersonation();
        //}
        //else
        //{
        //    // Failed to impersonate, most likely because the login information
        //    // provided was incorrect or unable to authenticate.
        //}
    }
}