﻿using System;
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
        public string BookingID { get; set; }

        public string Result { get; set; }
        public string ClientID { get; set; }
        public string CliqMachineID { get; set; }
        public string MachineAttributeCode { get; set; }
    }
}
