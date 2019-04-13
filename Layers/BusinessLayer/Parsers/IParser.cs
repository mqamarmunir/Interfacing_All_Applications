using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public interface IParser
    {
        void Parse(string data, mi_tinstruments machineSettings);
    }
}
