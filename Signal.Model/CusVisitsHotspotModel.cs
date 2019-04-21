using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal.Model
{
    public class CusVisitsHotspotModel
    {
        public CusVisitsHotspotModel() {
            visitHotspotLists = new List<VisitHotspotList>();
        }
        public string type { get; set; }
        public List<VisitHotspotList> visitHotspotLists { get; set; }
    }
    public class VisitHotspotList {

        public string xAxis { get; set; }

        public string yAxis { get; set; }

        public string Names { get; set; }

        public string[] Macs { get; set; }
    }
}
