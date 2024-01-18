#if NET5_0_OR_GREATER
using Microsoft.Data.SqlClient;
#else
using System.Data.SqlClient;
#endif
using System.Data.Common;
using System;
using System.Data;
using System.Collections;
#if NET45_OR_GREATER || NET5_0_OR_GREATER
using System.Threading.Tasks;
#endif

namespace Utility.DatabaseInterface
{
    /// <summary>
    /// DB_Interface class simplifies the process of pulling data from Microsoft SQL Server.
    /// </summary>
    public class DB_Interface
    {
        /// <summary>
        /// Class initialization setting the ConnectionString to string.empty instead of null.
        /// </summary>
        public DB_Interface() : base()
        {
            ConnectionString = string.Empty;
        }

        /// <summary>
        /// Initialization with optional connectionString parameter.
        /// </summary>
        /// <param name="connectionString">The connection string to your database.</param>
        public DB_Interface(string connectionString) : base()
        {
            ConnectionString = connectionString;
        }
        #region "Properties"
        /// <summary>
        ///     ''' This should be the connection string from your web.config file for connecting to the Database
        ///     ''' </summary>
        ///     ''' <value></value>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string ConnectionString { get; set; }

        private Exception ConnectionStringErrorMessage
        {
            get
            {
                return new Exception("Can not connect to Database because the ConnectionString property has not been properly set");
            }
        }
        #endregion

        #region "SQL Statements"
        /// <summary>
        ///     ''' Runs a SQL statement and returns Nothing upon successful completion
        ///     ''' </summary>
        ///     ''' <param name="strSQL">The SQL statement to be run</param>
        ///     ''' <param name="Timeout">How long short the SQL statement is allowed to run before it times out</param>
        ///     ''' <returns>This functions will only return Nothing unless it returns an error</returns>
        public string RunSQL(string strSQL, Int32 Timeout = 30)
        {
            string _returnVal = string.Empty;

            if (!(string.IsNullOrEmpty(strSQL)))
            {
                int intRowCount;
                try
                {
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        using (SqlCommand cmdUpdate = new SqlCommand
                        {
                            CommandTimeout = Timeout,
                            Connection = con,
                            CommandType = CommandType.Text,
                            CommandText = strSQL
                        })
                        {
                            cmdUpdate.Connection.Open();
                            intRowCount = cmdUpdate.ExecuteNonQuery();
                            cmdUpdate.Connection.Close();
                        }
                    }
                    else
                        throw (ConnectionStringErrorMessage);
                }
                catch
                {
                    throw;
                }
            }

            return _returnVal;
        }
#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Runs a SQL statement and returns Nothing upon successful completion
        ///     ''' </summary>
        ///     ''' <param name="strSQL">The SQL statement to be run</param>
        ///     ''' <param name="Timeout">How long short the SQL statement is allowed to run before it times out</param>
        ///     ''' <returns>This functions will only return Nothing unless it returns an error</returns>
        public async Task<string> RunSQLAsync(string strSQL, Int32 Timeout = 30)
        {
            string _returnVal = string.Empty;

            if (!(string.IsNullOrEmpty(strSQL)))
            {
                int intRowCount;
                try
                {
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        using (SqlCommand cmdUpdate = new SqlCommand
                        {
                            CommandTimeout = Timeout,
                            Connection = con,
                            CommandType = CommandType.Text,
                            CommandText = strSQL
                        })
                        {
                            await cmdUpdate.Connection.OpenAsync();
                            intRowCount = await cmdUpdate.ExecuteNonQueryAsync();
                            cmdUpdate.Connection.Close();
                        }
                    }
                    else
                        throw (ConnectionStringErrorMessage);
                }
                catch
                {
                    throw;
                }
            }

            return _returnVal;
        }
#endif


        /// <summary>
        ///     ''' Runs a SQL statement and returns Nothing upon successful completion
        ///     ''' </summary>
        ///     ''' <param name="strSQL">The SQL statement to be run</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the SQL.</param>
        ///     ''' <param name="Timeout">How long short the SQL statement is allowed to run before it times out</param>
        ///     ''' <returns>This functions will only return Nothing unless it returns an error</returns>
        public string RunSQL(string strSQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            string _returnVal = null;

            if (!(string.IsNullOrEmpty(strSQL)))
            {
                int intRowCount;

                try
                {
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        using (SqlCommand cmdUpdate = new SqlCommand
                        {
                            CommandTimeout = Timeout,
                            Connection = con,
                            CommandType = CommandType.Text,
                            CommandText = strSQL
                        })
                        {
                            // Iterate through the ParameterList, if it exists.
                            // Add the parameters to the sqlCommand.
                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqlParameter in ParameterList)
                                    cmdUpdate.Parameters.Add(_sqlParameter);
                            }
                            cmdUpdate.Connection.Open();
                            intRowCount = cmdUpdate.ExecuteNonQuery();
                            cmdUpdate.Connection.Close();
                        }
                    }
                    else
                        throw (ConnectionStringErrorMessage);
                }
                catch
                {
                    throw;
                }
            }

            return _returnVal;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Runs a SQL statement and returns Nothing upon successful completion
        ///     ''' </summary>
        ///     ''' <param name="strSQL">The SQL statement to be run</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the SQL.</param>
        ///     ''' <param name="Timeout">How long short the SQL statement is allowed to run before it times out</param>
        ///     ''' <returns>This functions will only return Nothing unless it returns an error</returns>
        public async Task<string> RunSQLAsync(string strSQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            string _returnVal = null;

            if (!(string.IsNullOrEmpty(strSQL)))
            {
                int intRowCount;

                try
                {
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        using (SqlCommand cmdUpdate = new SqlCommand
                        {
                            CommandTimeout = Timeout,
                            Connection = con,
                            CommandType = CommandType.Text,
                            CommandText = strSQL
                        })
                        {
                            // Iterate through the ParameterList, if it exists.
                            // Add the parameters to the sqlCommand.
                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqlParameter in ParameterList)
                                    cmdUpdate.Parameters.Add(_sqlParameter);
                            }
                            await cmdUpdate.Connection.OpenAsync();
                            intRowCount = await cmdUpdate.ExecuteNonQueryAsync();
                            cmdUpdate.Connection.Close();
                        }
                    }
                    else
                        throw (ConnectionStringErrorMessage);
                }
                catch
                {
                    throw;
                }
            }

            return _returnVal;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a datatable
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataTable containing the data returned by the StoredProcedure</returns>
        public DataTable GetDataTableFromReader(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to fill a datatable
            DataTable _dt = new DataTable();

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conectionn = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        Connection = conectionn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = StoredProcedureName
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                command.Parameters.Add(_sqlParameter);
                        }

                        // Use a datareader to fill the datatable.
                        command.Connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                _dt.Load(reader);
                            }
                            reader.Close();
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return _dt;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a datatable
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataTable containing the data returned by the StoredProcedure</returns>
        public async Task<DataTable> GetDataTableFromReaderAsync(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to fill a datatable
            DataTable _dt = new DataTable();

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = StoredProcedureName
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                command.Parameters.Add(_sqlParameter);
                        }

                        // Use a datareader to fill the datatable.
                        await command.Connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                _dt.Load(reader);
                            }
                            reader.Close();
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return _dt;
        }
#endif
        /// <summary>
        ///     ''' Executes the provided SQL and returns a datatable
        ///     ''' </summary>
        ///     ''' <param name="SQL">SQL text to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataTable containing the data returned by the SQL</returns>
        public DataTable GetDataTableFromReaderUsingSQL(string SQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to fill a datatable
            DataTable _dt = new DataTable();

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conectionn = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        Connection = conectionn,
                        CommandType = CommandType.Text,
                        CommandText = SQL
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                command.Parameters.Add(_sqlParameter);
                        }

                        // Use a datareader to fill the datatable.
                        command.Connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                _dt.Load(reader);
                            }
                            reader.Close();
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return _dt;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided SQL and returns a datatable
        ///     ''' </summary>
        ///     ''' <param name="SQL">SQL text to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataTable containing the data returned by the SQL</returns>
        public async Task<DataTable> GetDataTableFromReaderUsingSQLAsync(string SQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to fill a datatable
            DataTable _dt = new DataTable();

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandText = SQL
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                command.Parameters.Add(_sqlParameter);
                        }

                        // Use a datareader to fill the datatable.
                        await command.Connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                _dt.Load(reader);
                            }
                            reader.Close();
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return _dt;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns either Success or Error
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ConvertEmptyStringToNull">Flag for handling empty strings</param>
        ///     ''' <returns>A String containing either Success or Error</returns>
        public string RunStoredProcedure(string StoredProcedureName, ArrayList ParameterList = default, bool ConvertEmptyStringToNull = false, Int32 Timeout = 30)
        {
            // runs a stored procedure that doesn't return any values
            string _returnVal = "Error";

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                if (ConvertEmptyStringToNull)
                                {
                                    if (_sqlParameter.Value.ToString() == "")
                                        _sqlParameter.Value = DBNull.Value;
                                }
                                command.Parameters.Add(_sqlParameter);
                            }
                        }

                        command.Connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            _returnVal = "Success";
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return _returnVal;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns either Success or Error
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ConvertEmptyStringToNull">Flag for handling empty strings</param>
        ///     ''' <returns>A String containing either Success or Error</returns>
        public async Task<string> RunStoredProcedureAsync(string StoredProcedureName, ArrayList ParameterList = default, bool ConvertEmptyStringToNull = false, Int32 Timeout = 30)
        {
            // runs a stored procedure that doesn't return any values
            string _returnVal = "Error";

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                if (ConvertEmptyStringToNull)
                                {
                                    if (_sqlParameter.Value.ToString() == "")
                                        _sqlParameter.Value = DBNull.Value;
                                }
                                command.Parameters.Add(_sqlParameter);
                            }
                        }

                        await command.Connection.OpenAsync();
                        if (await command.ExecuteNonQueryAsync() > 0)
                        {
                            _returnVal = "Success";
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return _returnVal;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns the DbDataRecord
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DbDataRecord containing the data selected by the StoredProcedure</returns>
        public DbDataRecord GetRowFromDataReader(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            DbDataRecord dataRecord = null;
            // use a datareader to obtain a row from a stored procedure
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                command.Parameters.Add(_sqlParameter);
                            }
                        }

                        command.Connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                foreach (DbDataRecord record in reader)
                                {
                                    return record;
                                }
                            }
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return dataRecord;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns the DbDataRecord
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DbDataRecord containing the data selected by the StoredProcedure</returns>
        public async Task<DbDataRecord> GetRowFromDataReaderAsync(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            DbDataRecord dataRecord = null;
            // use a datareader to obtain a row from a stored procedure
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                command.Parameters.Add(_sqlParameter);
                            }
                        }
                        await command.Connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                foreach (DbDataRecord record in reader)
                                {
                                    return record;
                                }
                            }
                        }
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return dataRecord;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ConvertEmptyStringToNull">Flag for handling empty strings</param>
        ///     ''' <returns>An Object containing the data selected by the StoredProcedure</returns>
        public object GetValueFromSP(string StoredProcedureName, ArrayList ParameterList = default, bool ConvertEmptyStringToNull = false, Int32 Timeout = 30)
        {
            // returns an object returned by a stored procedure
            object Result = null;

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                if (ConvertEmptyStringToNull)
                                {
                                    if (_sqlParameter.Value.ToString() == "")
                                        _sqlParameter.Value = DBNull.Value;
                                }
                                command.Parameters.Add(_sqlParameter);
                            }
                        }

                        command.Connection.Open();
                        Result = command.ExecuteScalar();
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return Result;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ConvertEmptyStringToNull">Flag for handling empty strings</param>
        ///     ''' <returns>An Object containing the data selected by the StoredProcedure</returns>
        public async Task<object> GetValueFromSPAsync(string StoredProcedureName, ArrayList ParameterList = default, bool ConvertEmptyStringToNull = false, Int32 Timeout = 30)
        {
            // returns an object returned by a stored procedure
            object Result = null;

            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandText = StoredProcedureName
                    })
                    {

                        // Iterate through the ParameterList, if it exists.
                        // Add the parameters to the sqlCommand.
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                            {
                                if (ConvertEmptyStringToNull)
                                {
                                    if (_sqlParameter.Value.ToString() == "")
                                        _sqlParameter.Value = DBNull.Value;
                                }
                                command.Parameters.Add(_sqlParameter);
                            }
                        }

                        await command.Connection.OpenAsync();
                        Result = await command.ExecuteScalarAsync();
                        command.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }

            return Result;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided sql statement and returns the value specified in the sql statement.
        ///     ''' </summary>
        ///     ''' <param name="SQL">SQL Statement to execute (should produce only a single value).</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the SQL statement.</param>
        ///     ''' <returns>A DataTable containing the Pivoted Data</returns>
        public string GetValueFromSQL(string SQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            string Result = null;
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.Text,
                        Connection = conn,
                        CommandText = SQL
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                cmd.Parameters.Add(_sqlParameter);
                        }
                        cmd.Connection.Open();
                        Result = cmd.ExecuteScalar().ToString();
                        cmd.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return Result;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided sql statement and returns the value specified in the sql statement.
        ///     ''' </summary>
        ///     ''' <param name="SQL">SQL Statement to execute (should produce only a single value).</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the SQL statement.</param>
        ///     ''' <returns>A DataTable containing the Pivoted Data</returns>
        public async Task<string> GetValueFromSQLAsync(string SQL, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            string Result = null;
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand
                    {
                        CommandTimeout = Timeout,
                        CommandType = CommandType.Text,
                        Connection = conn,
                        CommandText = SQL
                    })
                    {
                        if (!(ParameterList == null))
                        {
                            foreach (SqlParameter _sqlParameter in ParameterList)
                                cmd.Parameters.Add(_sqlParameter);
                        }
                        await cmd.Connection.OpenAsync();
                        Result = (await cmd.ExecuteScalarAsync()).ToString();
                        cmd.Connection.Close();
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return Result;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a DataSet
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataSet containing the data selected by the StoredProcedure</returns>
        public DataSet GetDataSet(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            DataSet _ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = Timeout;
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqlParameter in ParameterList)
                                    cmd.Parameters.Add(_sqlParameter);
                            }
                            cmd.Connection.Open();
                            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                            {

                                sqlDataAdapter.Fill(_ds, "Table1");
                            }

                            transaction.Commit();
                            cmd.Connection.Close();
                        }
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return _ds;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a DataSet
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>A DataSet containing the data selected by the StoredProcedure</returns>
        public async Task<DataSet> GetDataSetAsync(string StoredProcedureName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            DataSet _ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = Timeout;
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqlParameter in ParameterList)
                                    cmd.Parameters.Add(_sqlParameter);
                            }
                            await cmd.Connection.OpenAsync();
                            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                            {

                                sqlDataAdapter.Fill(_ds, "Table1");
                            }

                            transaction.Commit();
                            cmd.Connection.Close();
                        }
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch
            {
                throw;
            }
            return _ds;
        }
#endif


        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a String Array
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnIndex">The index of the column from the results for which you would like to return</param>
        ///     ''' <returns>A String Array containing the data selected by the StoredProcedure based on the column index provided</returns>
        public string[] GetArrayFromDataReader(string StoredProcedureName, ArrayList ParameterList = default, short ColumnIndex = 0, Int32 Timeout = 30)
        {
            string[] returnValue = new string[0];
            int cnt = 0;

            try
            {
                DataTable dt = GetDataTableFromReader(StoredProcedureName, ParameterList, Timeout);

                Array.Resize<string>(ref returnValue, dt.Rows.Count);

                foreach (DataRow row in dt.Rows)
                {
                    returnValue[cnt] = row[ColumnIndex].ToString();
                }
            }
            catch
            {
                throw;
            }

            return returnValue;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns a String Array
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnIndex">The index of the column from the results for which you would like to return</param>
        ///     ''' <returns>A String Array containing the data selected by the StoredProcedure based on the column index provided</returns>
        public async Task<string[]> GetArrayFromDataReaderAsync(string StoredProcedureName, ArrayList ParameterList = default, short ColumnIndex = 0, Int32 Timeout = 30)
        {
            string[] returnValue = new string[0];
            int cnt = 0;

            try
            {
                DataTable dt = await GetDataTableFromReaderAsync(StoredProcedureName, ParameterList, Timeout);

                Array.Resize<string>(ref returnValue, dt.Rows.Count);

                foreach (DataRow row in dt.Rows)
                {
                    returnValue[cnt] = row[ColumnIndex].ToString();
                }
            }
            catch
            {
                throw;
            }

            return returnValue;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnName">The name of the column that you want to return</param>
        ///     ''' <returns>An Object containing the column of data selected by the StoredProcedure</returns>
        public string[] GetArrayFromDataReader(string StoredProcedureName, ArrayList ParameterList, string ColumnName, Int32 Timeout = 30)
        {
            string[] returnValue = new string[0];
            int cnt = 0;

            try
            {
                DataTable dt = GetDataTableFromReader(StoredProcedureName, ParameterList, Timeout);

                Array.Resize<string>(ref returnValue, dt.Rows.Count);

                foreach (DataRow row in dt.Rows)
                {
                    returnValue[cnt] = row[ColumnName].ToString();
                }
            }
            catch
            {
                throw;
            }

            return returnValue;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnName">The name of the column that you want to return</param>
        ///     ''' <returns>An Object containing the column of data selected by the StoredProcedure</returns>
        public async Task<string[]> GetArrayFromDataReaderAsync(string StoredProcedureName, ArrayList ParameterList, string ColumnName, Int32 Timeout = 30)
        {
            string[] returnValue = new string[0];
            int cnt = 0;

            try
            {
                DataTable dt = await GetDataTableFromReaderAsync(StoredProcedureName, ParameterList, Timeout);

                Array.Resize<string>(ref returnValue, dt.Rows.Count);

                foreach (DataRow row in dt.Rows)
                {
                    returnValue[cnt] = row[ColumnName].ToString();
                }
            }
            catch
            {
                throw;
            }

            return returnValue;
        }
#endif
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an ArrayList
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnIndex">The index of the column from the results for which you would like to return</param>
        ///     ''' <returns>A String Array containing the data selected by the StoredProcedure based on the column index provided</returns>
        public ArrayList GetArrayListFromDataReader(string StoredProcedureName, ArrayList ParameterList = default, short ColumnIndex = 0, Int32 Timeout = 30)
        {
            DataTable _t = default;
            ArrayList _s = new ArrayList();

            try
            {
                _t = GetDataTableFromReader(StoredProcedureName, ParameterList, Timeout);

                foreach (DataRow _dr in _t.Rows)
                    _s.Add(_dr[ColumnIndex].ToString());
            }
            catch
            {
                throw;
            }

            return _s;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an ArrayList
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnIndex">The index of the column from the results for which you would like to return</param>
        ///     ''' <returns>A String Array containing the data selected by the StoredProcedure based on the column index provided</returns>
        public async Task<ArrayList> GetArrayListFromDataReaderAsync(string StoredProcedureName, ArrayList ParameterList = default, short ColumnIndex = 0, Int32 Timeout = 30)
        {
            DataTable _t = default;
            ArrayList _s = new ArrayList();

            try
            {
                _t = await GetDataTableFromReaderAsync(StoredProcedureName, ParameterList, Timeout);

                foreach (DataRow _dr in _t.Rows)
                    _s.Add(_dr[ColumnIndex].ToString());
            }
            catch
            {
                throw;
            }

            return _s;
        }
#endif

        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an ArrayList
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnName">The name of the column that you want to return</param>
        ///     ''' <returns>An Object containing the column of data selected by the StoredProcedure</returns>
        public ArrayList GetArrayListFromDataReader(string StoredProcedureName, ArrayList ParameterList, string ColumnName, Int32 Timeout = 30)
        {
            DataTable _t = default;
            ArrayList _s = new ArrayList();

            try
            {
                _t = GetDataTableFromReader(StoredProcedureName, ParameterList, Timeout);

                foreach (DataRow _dr in _t.Rows)
                {
                    if (!(string.IsNullOrEmpty(ColumnName)))
                    {
                        if (!string.IsNullOrEmpty(ColumnName))
                            _s.Add(_dr[ColumnName].ToString());
                        else
                            _s.Add(_dr[0].ToString());
                    }
                    else
                        _s.Add(_dr[0].ToString());
                }
            }
            catch
            {
                throw;
            }

            return _s;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an ArrayList
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ColumnName">The name of the column that you want to return</param>
        ///     ''' <returns>An Object containing the column of data selected by the StoredProcedure</returns>
        public async Task<ArrayList> GetArrayListFromDataReaderAsync(string StoredProcedureName, ArrayList ParameterList, string ColumnName, Int32 Timeout = 30)
        {
            DataTable _t = default;
            ArrayList _s = new ArrayList();

            try
            {
                _t = await GetDataTableFromReaderAsync(StoredProcedureName, ParameterList, Timeout);

                foreach (DataRow _dr in _t.Rows)
                {
                    if (!(string.IsNullOrEmpty(ColumnName)))
                    {
                        if (!string.IsNullOrEmpty(ColumnName))
                            _s.Add(_dr[ColumnName].ToString());
                        else
                            _s.Add(_dr[0].ToString());
                    }
                    else
                        _s.Add(_dr[0].ToString());
                }
            }
            catch
            {
                throw;
            }

            return _s;
        }
#endif
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object with the output parameters rules in it
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="OutputParameterName">OutputParameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>An Object containing the OutputParameters and their results from the supplied StoredProcedure</returns>
        public object GetOutputParameterValueFromSP(string StoredProcedureName, string OutputParameterName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to obtain an array of the values from the 1st column of a stored procedure

            object _result = null;
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(StoredProcedureName, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = Timeout;
                            // Using transaction As SqlTransaction = conn.BeginTransaction()

                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqLParameter in ParameterList)
                                    cmd.Parameters.Add(_sqLParameter);
                            }

                            cmd.ExecuteNonQuery();

                            _result = cmd.Parameters[OutputParameterName].Value;
                        }
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
            return _result;
        }

#if NET45_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        ///     ''' Executes the provided StoredProcedure and returns an Object with the output parameters rules in it
        ///     ''' </summary>
        ///     ''' <param name="StoredProcedureName">Name of the StoredProcedure to execute</param>
        ///     ''' <param name="OutputParameterName">OutputParameters that are needed by the StoredProcedure.</param>
        ///     ''' <param name="ParameterList">Parameters that are needed by the StoredProcedure.</param>
        ///     ''' <returns>An Object containing the OutputParameters and their results from the supplied StoredProcedure</returns>
        public async Task<object> GetOutputParameterValueFromSPAsync(string StoredProcedureName, string OutputParameterName, ArrayList ParameterList = default, Int32 Timeout = 30)
        {
            // use a datareader to obtain an array of the values from the 1st column of a stored procedure

            object _result = null;
            try
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        await conn.OpenAsync();
                        using (SqlCommand cmd = new SqlCommand(StoredProcedureName, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = Timeout;
                            // Using transaction As SqlTransaction = conn.BeginTransaction()

                            if (!(ParameterList == null))
                            {
                                foreach (SqlParameter _sqLParameter in ParameterList)
                                    cmd.Parameters.Add(_sqLParameter);
                            }

                            await cmd.ExecuteNonQueryAsync();

                            _result = cmd.Parameters[OutputParameterName].Value;
                        }
                    }
                }
                else
                    throw (ConnectionStringErrorMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
            return _result;
        }
#endif
        #endregion

        #region "Parameter Items"
        /// <summary>

        ///     ''' Removes a Parameter from the supplied ParameterList by Reference
        ///     ''' </summary>
        ///     ''' <param name="SqlCommandToClear">Parameter name to be removed</param>
        ///     ''' <param name="ParameterList">ParameterList with the Parameter to be removed</param>
        public void ClearParameterList(ref SqlCommand SqlCommandToClear, ref ArrayList ParameterList)
        {
            if (!(ParameterList == null))
            {
                foreach (SqlParameter _sqlParameter in ParameterList)
                    SqlCommandToClear.Parameters.RemoveAt(_sqlParameter.ParameterName);
            }
        }
        /// <summary>
        ///     ''' Removes all Parameters from the supplied ParameterList by Reference
        ///     ''' </summary>
        ///     ''' <param name="ParameterList">ParameterList with the Parameter to be removed to be cleared</param>
        public void ClearParameterList(ref ArrayList ParameterList)
        {
            if (!(ParameterList == null))
                ParameterList.Clear();
        }
        /// <summary>
        ///     ''' Adds a Parameter to the supplied ParameterList by Reference
        ///     ''' </summary>
        ///     ''' <param name="_ParameterName">Name of the Parameter</param>
        ///     ''' <param name="_ParameterValue">Value for the Parameter</param>
        ///     ''' <param name="ParameterList">ParameterList with the Parameter to be added</param>
        ///     ''' <remarks></remarks>
        public void AddToParameterList(string _ParameterName, object _ParameterValue, ref ArrayList ParameterList)
        {
            if (!(ParameterList == null))
                ParameterList.Add(new SqlParameter(_ParameterName, _ParameterValue));
        }
        #endregion
    }
}
