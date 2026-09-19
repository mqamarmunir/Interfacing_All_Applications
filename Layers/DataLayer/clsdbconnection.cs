using System;
using System.Configuration;
using MySqlConnector;

namespace DataLayer
{
    /// <summary>
    /// Summary description for clsdbconnection.
    /// </summary>
    public class clsdbconnection
    {
        public clsdbconnection()
        {
        }

        MySqlConnection Conn;
        private bool disposed = false;

        #region Enumerations

        public enum ConnectionType
        {
            MySQL = 1,
            SQLSERVER = 2
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Get SQL Server Connection
        /// </summary>
        public MySqlConnection Odbc_SQL_Connection
        {
            get { return Dbconnection(clsdbconnection.ConnectionType.MySQL); }
        }

        #endregion

        #region "Connection String"

        private MySqlConnection Dbconnection(ConnectionType ConnectToDB)
        {
            string StrConnection = null;
            AppSettingsReader con = new AppSettingsReader();
            StrConnection = "User Id=" + con.GetValue("susername", "".GetType()).ToString() + "; PWD =" + con.GetValue("spassword", "".GetType()).ToString() + "; Server=" + con.GetValue("sserver", "".GetType()).ToString() + ";Port=3306;Database=" + con.GetValue("sdb", "".GetType()).ToString() + ";respect binary flags = false";

            try
            {
                Conn = new MySqlConnection(StrConnection);
                Conn.Open();
                return Conn;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return null;
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                GC.Collect();
            }
            disposed = true;
        }
    }
}