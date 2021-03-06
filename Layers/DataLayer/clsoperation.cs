using System;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using BusinessEntities;
using System.Collections.Generic;

namespace DataLayer
{
    /// <summary>
    /// Summary description for clsoperation.
    /// </summary>
    public class clsoperation : IDisposable
    {

        private string strmsg = "False";
        protected MySqlCommand ObjCmd;
        private string StrOperationError;
        protected MySqlConnection Conn;
        protected MySqlTransaction DbTrans;
        protected clsdbconnection Objconn = new clsdbconnection();

        public clsoperation()
        {

        }

        #region "Properties"

        public string OperationError
        {
            get { return StrOperationError; }
            set { StrOperationError = value; }
        }

        public string StrMsg
        {
            set { strmsg = value; }
        }

        public MySqlConnection GetConnection
        {
            get { return Conn; }
        }

        public MySqlTransaction DBTransaction
        {
            get { return DbTrans; }
        }

        #endregion

        #region "Enumeration"

        public enum Get_PKey : byte
        {
            Yes = 1,
            No = 2
        }

        #endregion

        #region "Error_Transction"

        public void Start_Transaction()
        {
            // Get Database connection
            Conn = Objconn.Odbc_SQL_Connection;
            // Declare Transaction Variable 
            DbTrans = Conn.BeginTransaction();  //New a transaction		
        }

        public void End_Transaction()
        {
            if (strmsg == "True")
            {
                // Rollback transaction
                DbTrans.Rollback();
            }
            else
            {
                // commit transaction
                DbTrans.Commit();
            }

            this.Conn.Close();
            this.Conn.Dispose();
            this.Conn = null;
        }

        #endregion

        #region DataAccess


        public string DataTrigger_Delete(Iinterface Entity)
        {
            try
            {
                // Get Command object with Stored procedure
                ObjCmd = Entity.Delete();
                // to Command object for a pending local transaction
                ObjCmd.Connection = Conn;
                ObjCmd.Transaction = DbTrans;



                ObjCmd.ExecuteNonQuery();

                strmsg = "False";
            }
            catch (Exception e)
            {
                OperationError = (e.Message) + "Sorry, Record can not be deleted.";
                strmsg = "True";
                return strmsg;
            }

            return strmsg;
        }
        public string DataTrigger_Insert(Iinterface Entity)
        {
            try
            {
                // Get Command object with Stored procedure
                ObjCmd = Entity.Insert();
                // to Command object for a pending local transaction
                ObjCmd.Connection = DbTrans.Connection;
                ObjCmd.Transaction = DbTrans;

                ObjCmd.ExecuteNonQuery();

                strmsg = "False";
            }
            catch (Exception e)
            {
                OperationError = (e.Message) + "Sorry, Record can not be inserted.";
                strmsg = "True";
                return strmsg;
            }

            return strmsg;
        }

        /*public string DataTrigger_Delete(Iinterface Entity)
        {
            // Get Command object with Stored procedure
            ObjCmd = Entity.Delete();
            // to Command object for a pending local transaction
            ObjCmd.Transaction = DbTrans;
			
            try
            {
                ObjCmd.Connection = Conn;				
                ObjCmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                OperationError = (e.Message) + "Sorry, Record can not be deleted.";
                strmsg="True";
            }

            return strmsg ;
        }*/
        public string DataTrigger_Update(Iinterface Entity)
        {
            // Get Command object with Stored procedure
            ObjCmd = Entity.Update();
            // to Command object for a pending local transaction
            ObjCmd.Connection = DbTrans.Connection;
            ObjCmd.Transaction = DbTrans;

            try
            {

                ObjCmd.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                // Rollback transaction
                //DbTrans.Rollback();
                OperationError = (e.Message) + "Sorry, Record can not be updated.";
                strmsg = "True";
            }

            return strmsg;
        }

        public DataView DataTrigger_Get_All(Iinterface Entity)
        {
            try
            {
                using (Conn = Objconn.Odbc_SQL_Connection)
                {
                    ObjCmd = Entity.Get_All();
                    ObjCmd.Connection = Conn;

                    using (MySqlDataAdapter da = new MySqlDataAdapter(ObjCmd))
                    {
                        DataSet DS = new DataSet();
                        da.Fill(DS);
                        ObjCmd.Connection.Close();
                        ObjCmd.Connection.Dispose();
                        ObjCmd.Connection = null;

                        DataView DV = new DataView(DS.Tables[0]);

                        DS.Dispose();
                        DS = null;              //null
                                                // da = null;              //null
                        Objconn.Dispose();      //null
                                                //null
                        ObjCmd = null;          //null
                        return DV;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<T> DataTrigger_Get_All<T>(Iinterface Entity) where T : class
        {
            try
            {
                using (Conn = Objconn.Odbc_SQL_Connection)
                {
                    ObjCmd = Entity.Get_All();
                    ObjCmd.Connection = Conn;
                    var list = SqlMapper.Query<T>(Conn, ObjCmd.CommandText);

                    ObjCmd.Connection.Close();
                    ObjCmd.Connection.Dispose();
                    ObjCmd.Connection = null;


                    Objconn.Dispose();      //null
                    Conn.Close();           //null
                    Conn.Dispose();         //null
                    Conn = null;            //null
                    ObjCmd = null;          //null

                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //		public DataView Transaction_Get_All(Iinterface Entity)
        //		{
        //			ObjCmd = Entity.Get_All();
        //			ObjCmd.Transaction = DbTrans;
        //			ObjCmd.Connection = Conn;
        //				
        //			// Dataadapter
        //			OdbcDataAdapter da = new OdbcDataAdapter(ObjCmd);
        //			// fill dataset
        //			DataSet DS = new DataSet();
        //			da.Fill(DS);
        //
        //			// Create the DataView Object
        //			DataView DV = new DataView(DS.Tables[0]);
        //			
        //			return DV;
        //		}

        public DataView DataTrigger_Get_Single(Iinterface Entity)
        {
            // Get Database connection
            Conn = Objconn.Odbc_SQL_Connection;
            ObjCmd = Entity.Get_Single();
            ObjCmd.Connection = Conn;
            // Dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(ObjCmd);
            // fill dataset
            DataSet DS = new DataSet();
            da.Fill(DS);
            ObjCmd.Connection.Close();
            ObjCmd.Connection.Dispose();
            ObjCmd.Connection = null;
            // Create the DataView Object
            DataView DV = new DataView(DS.Tables[0]);
            return DV;
        }

        public DataView Transaction_Get_Single(Iinterface Entity)
        {
            ObjCmd = Entity.Get_Single();
            //ObjCmd.Connection = Conn;
            ObjCmd.Connection = DbTrans.Connection;
            ObjCmd.Transaction = DbTrans;
            // Dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(ObjCmd);
            // fill dataset
            DataSet DS = new DataSet();
            da.Fill(DS);

            // Create the DataView Object
            DataView DV = new DataView(DS.Tables[0]);

            return DV;
        }

        public string DataTrigger_Get_Max(Iinterface Entity)
        {
            try
            {
                // Get Database connection
                ObjCmd = Entity.Get_Max();
                ObjCmd.Connection = DbTrans.Connection;
                ObjCmd.Transaction = DbTrans;


                // Dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(ObjCmd);
                // fill dataset
                DataSet DS = new DataSet();
                da.Fill(DS);
                //ObjCmd.Connection.Close();

                return DS.Tables[0].Rows[0]["MaxID"].ToString();
            }
            catch (Exception e)
            {
                OperationError = (e.Message) + "Sorry, Maximum Primary Key can not be extracted.";
                strmsg = "True";
            }

            return strmsg;
        }

        #endregion

        public void Dispose()
        {
            Objconn.Dispose();
        }
    }
}