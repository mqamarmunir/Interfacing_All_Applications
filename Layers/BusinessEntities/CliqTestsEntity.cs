using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class CliqTestsEntity
    {
        public int test_id { get; set; }
        public string test_name { get; set; }
        public int department_id { get; set; }
        public List<CliqAttributeEntity> attrubute_info { get; set; }
    }
}
