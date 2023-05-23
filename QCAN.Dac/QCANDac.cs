using System;
using System.Collections.Generic;
using System.Data;
using CTF.Web.Framework.Helper;

namespace QCAN.Dac
{
    public class QCANDac : IDisposable
    {
        private DBHelper spcDB;

        #region Dispose
        public void Dispose()
        {
            if (spcDB != null)
            {
                spcDB.Dispose();
                spcDB = null;
            }
        }
        #endregion
    }
}
