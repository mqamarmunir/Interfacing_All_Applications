using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Data;
using System.Configuration;
namespace BusinessLayer
{
    public class clsBLMain
    {

        #region "Class Variables"
        static clsoperation objTrans = new clsoperation();
        static clsdbhims objdbhims = new clsdbhims();
        static QueryBuilder objQB = new QueryBuilder();
        private const string Default = "~!@";
        private const string TableName = "mi_tresult";

        private string StrErrorMessage = "";
        private string StrFilename = Default;
        private string StrSenton = Default;
        private string StrSentto = Default;
        private string StrStatus = Default;
        private string Strmaxid = Default;
        private string StrFailreason = Default;
        private string _ResultID = Default;

        public string ResultID
        {
            get { return _ResultID; }
            set { _ResultID = value; }
        }


        #endregion

        #region "Properties"
        public string FailureReason
        {
            get { return StrFailreason; }
            set { StrFailreason = value; }
        }
        public string Error
        {
            get { return StrErrorMessage; }
            set { StrErrorMessage = value; }
        }
        public string Filename
        {
            get { return StrFilename; }
            set { StrFilename = value; }
        }
        public string Senton
        {
            get { return StrSenton; }
            set { StrSenton = value; }
        }
        public string Sentto
        {
            get { return StrSentto; }
            set { StrSentto = value; }
        }
        public string status
        {
            get { return StrStatus; }
            set { StrStatus = value; }
        }


        public string MAxID
        {
            get { return Strmaxid; }

            set { Strmaxid = value; }
        }
        //public string Password
        //{
        //    get { return StrPasword; }
        //    set { StrPasword = value; }
        //}
        //public string RSerialNO
        //{
        //    get { return Str_RSerialNo; }
        //    set { Str_RSerialNo = value; }
        //}
        //public string MSerialNO
        //{
        //    get { return Str_MSerialNo; }
        //    set { Str_MSerialNo = value; }
        //}

        #endregion


        #region "Methods"



        public DataView GetAll(int flag)
        {
            switch (flag)
            {
                case 1:
                    objdbhims.Query = "select * from mi_tpreferencesetting where Status='Y'";
                    break;

                case 2:
                    objdbhims.Query = "select * from mi_tresult where ResultID > '" + Strmaxid + "'";
                    //objdbhims.Query = objdbhims.Query + " Where Upper(t.GroupName) = '" + this._GroupName.ToUpper() + "'";
                    break;

                case 3:
                    objdbhims.Query = "select max(ResultID) AS maxid from mi_tresult where ResultID>'" + Strmaxid + "'";
                    break;

                case 4:
                    objdbhims.Query = "select max(maxresultid) as maxresultid from mi_taudit  ";
                    break;
                case 5:
                    objdbhims.Query = @"Select '' as Machine_TestID,'' as Lims_test_name
                                        Union
                                        SELECT Machine_TestID,Lims_test_name FROM mi_ttests m where m.Active='Y'";
                    break;
                case 6:
                    objdbhims.Query = "SELECT Attributeid,LimsAttributeName FROM mi_ttestattribute m where m.Active='Y' and Machine_testid=" + this._TestID;
                    break;
                case 7:
                    objdbhims.Query = @"SELECT m.resultid,m.BookingID,(Select c.test_id from cliqmachinemappings c where c.machineattributecode=m.attributeid and c.Active=1 limit 1) as cliqtestid,(Select c.CliqAttributeID from cliqmachinemappings c where c.machineattributecode=m.attributeid and c.active=1 limit 1) as CliqAttributeid,m.result,m.clientid,m.machineName MachineID,m.attributeid machineAttributeCode
                                        FROM mi_tresult m where m.Status='N' order by resultid desc limit 20";
//                    objdbhims.Query = @"SELECT m.resultid,m.BookingID,c.Test_ID cliqtestid,c.CliqAttributeID,m.result,m.clientid,m.machineName MachineID,m.attributeid MachineAttributeCode
//                                        FROM mi_tresult m inner join cliqmachinemappings c on c.machineattributecode=m.attributeid
//                                        Where m.Status='N'
//                                        limit 20";
                    break;
            }

            return objTrans.DataTrigger_Get_All(objdbhims);
        }
        public bool Update()
        {

            clsoperation objTrans = new clsoperation();
            QueryBuilder objQB = new QueryBuilder();

            objTrans.Start_Transaction();
            objdbhims.Query = objQB.QBUpdate(MakeArray(), TableName);
            this.StrErrorMessage = objTrans.DataTrigger_Update(objdbhims);
            objTrans.End_Transaction();

            if (this.StrErrorMessage.Equals("True"))
            {
                this.StrErrorMessage = objTrans.OperationError;
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool Insert()
        {

            try
            {
                //clsoperation objTrans = new clsoperation();
                // QueryBuilder objQB = new QueryBuilder();
                objTrans.Start_Transaction();

                // objdbhims.Query = objQB.QBGetMax("GroupId", TableName);
                // this._GroupId = objTrans.DataTrigger_Get_Max(objdbhims);

                //if (!this._GroupId.Equals("True"))
                //{
                objdbhims.Query = objQB.QBInsert(MakeArray(), TableName);
                this.StrErrorMessage = objTrans.DataTrigger_Insert(objdbhims);

                objTrans.End_Transaction();

                if (this.StrErrorMessage.Equals("True"))
                {
                    this.StrErrorMessage = objTrans.OperationError;
                    return false;
                }


                return true;

                // }

            }
            catch (Exception e)
            {
                this.StrErrorMessage = e.Message;
                return false;
            }
        }

        public bool Deleteolddata()
        {
            clsoperation objTrans = new clsoperation();
            QueryBuilder objQB = new QueryBuilder();

            objTrans.Start_Transaction();
            objdbhims.Query = "Delete from mi_tresult where enteredon<date_sub(Now(), Interval 4 day)";// Will delete 4 days old data
            this.StrErrorMessage = objTrans.DataTrigger_Delete(objdbhims);
            if (StrErrorMessage.Equals("True"))
            {
                objTrans.End_Transaction();
                this.StrErrorMessage = objTrans.OperationError;
                return false;
            }
            objTrans.End_Transaction();
            return true;
        }


        private string[,] MakeArray()
        {
            string[,] strArr = new string[4, 3];

            if (!this._ResultID.Equals(Default))
            {
                strArr[0, 0] = "ResultID";
                strArr[0, 1] = this._ResultID;
                strArr[0, 2] = "int";
            }
            if (!this.StrSenton.Equals(Default))
            {
                strArr[1, 0] = "Senton";
                strArr[1, 1] = this.StrSenton;
                strArr[1, 2] = "datetime";
            }
            if (!this.StrSentto.Equals(Default))
            {
                strArr[2, 0] = "Sentto";
                strArr[2, 1] = this.StrSentto;
                strArr[2, 2] = "string";
            }
            if (!this.StrStatus.Equals(Default))
            {
                strArr[3, 0] = "Status";
                strArr[3, 1] = this.StrStatus;
                strArr[3, 2] = "string";
            }
            


            return strArr;
        }


        public string _TestID { get; set; }
    }
        #endregion
}
