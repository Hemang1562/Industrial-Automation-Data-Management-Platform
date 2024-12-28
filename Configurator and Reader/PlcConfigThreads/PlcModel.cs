using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcConfigThreads
{
    [Serializable]
    public class PlcModel
    {
        public int id { get; set; }
        public string plc_Name { get; set; }
        public string plc_Type { get; set; }
        public string plc_IP { get; set; }

        public int plc_port { get; set; }
        public int plc_slaveAdress { get; set; }
        public int plc_startAdress { get; set; }
        public int noOfPoints { get; set; }

        public List<string> IntervalCheckedItems { get; set; } = new List<string>();
        public List<string> LiveDataList { get; set; } = new List<string>();
        public List<string> ThresHoldCheckedItems { get; set; } = new List<string>();
        public List<string> OnOffBit { get; set; } = new List<string>();
        public List<string> ValueChange { get; set; } = new List<string>();
    }
}
