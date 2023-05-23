using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace CTF.Web.Framework.Helper
{
    /// <summary>
    /// dbHelper is a helper class that takes the common data classes and allows you
    /// to specify the provider to use, execute commands, add parameters, and return datasets.
    /// See examples for usage.
    /// </summary>
    public class DBHelper : IDisposable
    {
        #region private members
        private string _connectionstring = "";
        private DbConnection _connection;
        private DbCommand _command;
        private DbCommand _commandArithAbort;
        private DbProviderFactory _factory = null;
        private bool bRollback = false;
        #endregion

        #region constructor
        public DBHelper()
        {
            CreateDBObjects("SPC");
        }

        public DBHelper(string stConnectionName)
        {
            CreateDBObjects(stConnectionName);
        }
        #endregion

        #region properties

        /// <summary>
        /// Gets or Sets the connection string for the database
        /// </summary>
        public string connectionstring
        {
            get
            {
                return _connectionstring;
            }
            set
            {
                if (value != "")
                {
                    _connectionstring = value;
                }
            }
        }

        /// <summary>
        /// Gets the connection object for the database
        /// </summary>
        public DbConnection connection
        {
            get
            {
                return _connection;
            }
        }

        /// <summary>
        /// Gets the command object for the database
        /// </summary>
        public DbCommand command
        {
            get
            {
                return _command;
            }
        }

        /// <summary>
        /// Gets the command object for the database
        /// </summary>
        public DbCommand commandArithAbort
        {
            get
            {
                return _commandArithAbort;
            }
        }

        /// <summary>
        /// Gets then connection state for database
        /// </summary>
        public ConnectionState connectionstate
        {
            get
            {
                return connection.State;
            }
        }

        #endregion

        # region methods

        #region Set Connection
        /// <summary>
        /// Determines the correct provider to use and sets up the connection and command
        /// objects for use in other methods
        /// </summary>
        /// <param name="stConnectionName">The connection name</param>
        public void CreateDBObjects(string stConnectionName)
        {
            if (stConnectionName.Equals("ORA"))
                _factory = OracleClientFactory.Instance;
            else
                _factory = SqlClientFactory.Instance;

            string connectString = CommonHelper.GetConnectionString(stConnectionName);

            _connection = _factory.CreateConnection();
            _command = _factory.CreateCommand();
            _commandArithAbort = _factory.CreateCommand();

            _connection.ConnectionString = connectString;
            _command.Connection = connection;
            _commandArithAbort.Connection = connection;

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        #endregion

        #region parameters

        public void AddParameter(Dictionary<string, string> oDic)
        {
            if (oDic != null)
            {
                foreach (KeyValuePair<string, string> pair in oDic)
                {
                    DbParameter oParameter = _factory.CreateParameter();
                    oParameter.ParameterName = String.Format("@{0}", pair.Key);
                    oParameter.Value = pair.Value;
                    command.Parameters.Add(oParameter);
                }
            }
        }

        /// <summary>
        /// Creates a parameter and adds it to the command object
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The paremeter value</param>
        /// <returns></returns>
        public int AddParameter(string name, object value)
        {
            DbParameter parm = _factory.CreateParameter();
            parm.ParameterName = name;
            parm.Value = value;
            return command.Parameters.Add(parm);
        }

        /// <summary>
        /// Creates a parameter and adds it to the command object
        /// </summary>
        /// <param name="parameter">A parameter object</param>
        /// <returns></returns>
        public int AddParameter(DbParameter parameter)
        {
            return command.Parameters.Add(parameter);
        }

        #endregion

        #region transactions

        /// <summary>
        /// Starts a transaction for the command object
        /// </summary>
        private void BeginTransaction()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            command.Transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Commits a transaction for the command object
        /// </summary>
        private void CommitTransaction()
        {
            if (!bRollback)
                command.Transaction.Commit();
        }

        /// <summary>
        /// Rolls back the transaction for the command object
        /// </summary>
        private void RollbackTransaction()
        {
            command.Transaction.Rollback();
        }

        #endregion

        #region execute database functions

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <returns>A Boolean value</returns>
        public bool ExecuteNonQuery(string query)
        {
            bool bExecute = false;

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            int i = -1;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                i = command.ExecuteNonQuery();

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }


        public bool ExecuteNonQuery(string query, out string errMsg)
        {
            bool bExecute = false;
            errMsg = "";

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            int i = -1;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                i = command.ExecuteNonQuery();

                bExecute = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A Boolean value</returns>
        public bool ExecuteNonQuery(string query, CommandType commandtype)
        {
            bool bExecute = false;

            command.CommandText = query;
            command.CommandType = commandtype;
            int i = -1;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                i = command.ExecuteNonQuery();

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery1(string[] query)
        {
            int i = -1;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < query.Length; idx++)
                {
                    command.CommandText = query[idx];
                    command.CommandType = CommandType.StoredProcedure;
                    i = command.ExecuteNonQuery();
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery1(string[] query, object[] commandtype, ref string ErrMsg)
        {
            int i = -1;
            int iQueryCnt = query.Length;
            int iExecute = 0;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < iQueryCnt; idx++)
                {
                    if (!String.IsNullOrEmpty(query[idx]))
                    {
                        command.CommandText = query[idx];
                        command.CommandType = (CommandType)commandtype[idx];
                        i = command.ExecuteNonQuery();
                        iExecute += i;
                    }
                }

                if (iExecute != iQueryCnt)
                {
                    throw new Exception(String.Format("저장 또는 수정된 데이타 갯수가 맞지 않습니다(수행할갯수:{0}, 수행된갯수:{1})", iQueryCnt, iExecute));
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="param">The parameters, object array</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery2(string[] query, object[] param)
        {
            int i = -1;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < query.Length; idx++)
                {
                    if (!String.IsNullOrEmpty(query[idx]))
                    {
                        command.Parameters.Clear();
                        AddParameter((Dictionary<string, string>)param[idx]);
                        command.CommandText = query[idx];
                        command.CommandType = CommandType.StoredProcedure;
                        i = command.ExecuteNonQuery();
                    }
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="param">The parameters, object array</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery2(string[] query, object[] param, out string resultMsg)
        {
            int i = -1;
            resultMsg = "";
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < query.Length; idx++)
                {
                    if (!String.IsNullOrEmpty(query[idx]))
                    {
                        command.Parameters.Clear();
                        AddParameter((Dictionary<string, string>)param[idx]);
                        command.CommandText = query[idx];
                        command.CommandType = CommandType.StoredProcedure;
                        i = command.ExecuteNonQuery();
                    }
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
                resultMsg = ex.Message;
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="param">The parameters, object array</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery2(string[] query, object[] param, object[] commandtype)
        {
            int i = -1;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < query.Length; idx++)
                {
                    if (!String.IsNullOrEmpty(query[idx]))
                    {
                        command.Parameters.Clear();
                        AddParameter((Dictionary<string, string>)param[idx]);
                        command.CommandText = query[idx];
                        command.CommandType = (CommandType)commandtype[idx];
                        i = command.ExecuteNonQuery();
                    }
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="param">The parameters, object array</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery3(string query, object[] param)
        {
            int i = -1;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < param.Length; idx++)
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        command.Parameters.Clear();
                        AddParameter((Dictionary<string, string>)param[idx]);
                        command.CommandText = query;
                        command.CommandType = CommandType.StoredProcedure;
                        i = command.ExecuteNonQuery();
                    }
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="param">The parameters, object array</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A Boolean value</returns>
        public bool MultiExecuteNonQuery3(string query, object[] param, object[] commandtype)
        {
            int i = -1;
            bool bExecute = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                BeginTransaction();

                for (int idx = 0; idx < param.Length; idx++)
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        command.Parameters.Clear();
                        AddParameter((Dictionary<string, string>)param[idx]);
                        command.CommandText = query;
                        command.CommandType = (CommandType)commandtype[idx];
                        i = command.ExecuteNonQuery();
                    }
                }

                bExecute = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                bRollback = true;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                CommitTransaction();
                Dispose();
            }

            return bExecute;
        }

        /// <summary>
        /// Executes a statement that returns a single value. 
        /// If this method is called on a query that returns multiple rows and columns, only the first column of the first row is returned.
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <returns>An object that holds the return value(s) from the query</returns>
        public object ExecuteScaler(string query)
        {
            object obj = null;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                obj = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }

                Dispose();
            }

            return obj;
        }

        /// <summary>
        /// Executes a statement that returns a single value. 
        /// If this method is called on a query that returns multiple rows and columns, only the first column of the first row is returned.
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>An object that holds the return value(s) from the query</returns>
        public object ExecuteScaler(string query, CommandType commandtype)
        {
            object obj = null;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.CommandText = query;
                command.CommandType = commandtype;
                obj = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }

                Dispose();
            }

            return obj;
        }

        /// <summary>
        /// Executes a SQL statement that returns a result set.
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <returns>A datareader object</returns>
        public DbDataReader ExecuteReader(string query, out string errMsg)
        {
            errMsg = String.Empty;

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            DbDataReader reader = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                if (connectionstate == System.Data.ConnectionState.Open)
                {
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }
            }

            return reader;
        }

        /// <summary>
        /// Executes a SQL statement that returns a result set.
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A datareader object</returns>
        public DbDataReader ExecuteReader(string query, CommandType commandtype, out string errMsg)
        {
            errMsg = String.Empty;

            command.CommandText = query;
            command.CommandType = commandtype;
            DbDataReader reader = null;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                if (connectionstate == System.Data.ConnectionState.Open)
                {
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }
            }

            return reader;
        }

        /// <summary>
        /// Generates a dataset
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <returns>A dataset containing data from the database</returns>
        public DataSet GetDataSet(string query, out string errMsg)
        {
            ArithAbort();
            
            errMsg = String.Empty;

            DbDataAdapter adapter = _factory.CreateDataAdapter();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }

                Dispose();
            }
            return ds;
        }

        /// <summary>
        /// Generates a dataset
        /// </summary>
        /// <param name="query">The query, either SQL or Procedures</param>
        /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
        /// <returns>A dataset containing data from the database</returns>
        public DataSet GetDataSet(string query, CommandType commandtype, out string errMsg)
        {
            ArithAbort();

            errMsg = String.Empty;

            DbDataAdapter adapter = _factory.CreateDataAdapter();
            command.CommandText = query;
            command.CommandType = commandtype;
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.LogWrite(ex);
            }
            finally
            {
                command.Parameters.Clear();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    command.Dispose();
                }

                Dispose();
            }
            return ds;
        }

        #endregion

        #region ArithAbort
        void ArithAbort()
        {
            commandArithAbort.CommandText = "SET ARITHABORT ON";
            commandArithAbort.CommandType = CommandType.Text;
            commandArithAbort.ExecuteNonQuery();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        #endregion

        #endregion
    }
}