using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class CliqResultResponse
    {
        public List<responce> responce { get; set; }
    }
    public class responce
    {
        public string Code { get; set; }
        public string msg { get; set; }
        public string BookingID { get; set; }
        public string CliqAttributeID { get; set; }
        public string Result { get; set; }
    }
}
