using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Models
{
    public class HeatmapVisitDataModel
    {
        public int Position { get; set; }

        public int Hour  { get; set; }

        public int HeatValue { get; set; }
    }
}