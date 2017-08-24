using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CliqMachineEntity
    {
        public long id { get; set; }
        public long BranchID { get; set; }
        public long Test_ID { get; set; }
        public string TestName { get; set; }
        public long CliqAttributeID { get; set; }
        public string AttributeName { get; set; }
        public string MachineAttributeCode { get; set; }
        public bool Active { get; set; }
        public long MachineID { get; set; }
    }
}
