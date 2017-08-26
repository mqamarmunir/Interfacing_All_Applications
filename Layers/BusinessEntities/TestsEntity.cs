using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TestsEntity
    {
        public long Machine_testid { get; set; }
        public string Lims_testid { get; set; }
        public string Lims_test_name { get; set; }
        public string Machine_Test_name { get; set; }
        public string LOINC_code { get; set; }
        public long Instrumentid { get; set; }
        public long EnteredBy { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string ClientID { get; set; }
        public string Active { get; set; }
        public string DeptID { get; set; }
        public string MachineTestCode { get; set; }
    }
}
