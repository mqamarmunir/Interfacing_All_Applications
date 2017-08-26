using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TestAttributeEntity
    {
        public long AttributeID { get; set; }
        public long Machine_testid { get; set; }
        public string LIMSAttributeID { get; set; }
        public string LIMSAttributeName { get; set; }
        public string MachineAttributeName { get; set; }
        public long EnteredBy { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string ClientId { get; set; }
        public string Active { get; set; }
        public string MachineAttributeCode { get; set; }
    }
}
