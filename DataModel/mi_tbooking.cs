//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class mi_tbooking
    {
        public long BookingID { get; set; }
        public string LabID { get; set; }
        public Nullable<long> PatientID { get; set; }
        public string Patient_name { get; set; }
        public string Test_Code { get; set; }
        public string Machine_TestID { get; set; }
        public string Test_Name { get; set; }
        public long InstrumentID { get; set; }
        public Nullable<System.DateTime> Machine_Time { get; set; }
        public string SeqID { get; set; }
        public string BatchNo { get; set; }
        public string Sample_Type { get; set; }
        public long EnteredBy { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string ClientID { get; set; }
        public string Active { get; set; }
        public Nullable<System.DateTime> SendOn { get; set; }
        public Nullable<System.DateTime> ReceivedOn { get; set; }
    }
}
