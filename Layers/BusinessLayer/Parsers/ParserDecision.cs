using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public static class ParserDecision
    {
        public static void Parsethisandinsert(string data, mi_tinstruments machineSettings)
        {
            IParser parser;
            switch (machineSettings.ParsingAlgorithm)
            {
                case 1:
                    parser = new ASTM();
                    parser.Parse(data, machineSettings);
                    break;//case 1 ends here
                case 2://AU480 Beckman
                    parser = new AU480();
                    parser.Parse(data, machineSettings);
                    break;
                case 3:
                    parser = new BeckManLH750();
                    parser.Parse(data, machineSettings);
                    break;
                case 4:
                    parser = new ASTM_AbbottArchitect();
                    parser.Parse(data, machineSettings);
                    break;
                case 5:
                    parser = new ASTM_Cobas6000();
                    parser.Parse(data, machineSettings);
                    break;
                case 6:
                    parser = new SysmexKX21();
                    parser.Parse(data, machineSettings);
                    break;
            }
        }
    }
}
