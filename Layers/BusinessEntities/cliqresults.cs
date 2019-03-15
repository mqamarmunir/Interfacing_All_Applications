using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class cliqresults
    {
        public int ResultID { get; set; }
        public string BookingID { get; set; }
        public string CliqTestID { get; set; }
        public string CliqAttributeID { get; set; }
        public string Result { get; set; }
        public string ClientID { get; set; }
        public string MachineID { get; set; }
        public string MachineAttributeCode { get; set; }
    }
    public class cliqresultsNew
    {
        public int ResultID { get; set; }
        public long BookingID { get; set; }

        public string Result { get; set; }
        public int ClientID { get; set; }
        public int CliqMachineID { get; set; }
        public string MachineAttributeCode { get; set; }
    }
    public class cliqResultsBookingwise
    {
        public int branch_id { get; set; }
        public long order_no { get; set; }
        
        public List<cliqResultBookingwiseDetail> data { get; set; }

    }
    public class cliqResultBookingwiseDetail
    {
        public string attribute_id { get; set; }
        public string attribute_result { get; set; }
        public int machine_id { get; set; }
    }
}
