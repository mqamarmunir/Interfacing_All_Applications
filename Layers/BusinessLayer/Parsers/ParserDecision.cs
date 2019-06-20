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
                    break;
                case 2:
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
                case 7:
                    parser = new DIRUIH500();
                    parser.Parse(data, machineSettings);
                    break;
                case 8:
                    parser = new ASTM_CellDyn();
                    parser.Parse(data, machineSettings);
                    break;
                case 9:
                    parser = new ASTM_AbbottAlinity();
                    parser.Parse(data, machineSettings);
                    break;
              
            }
        }
    }
}
