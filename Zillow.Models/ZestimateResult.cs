using System;
using System.Xml.Serialization;

namespace Zillow.Models
{
    public class ZestimateResult
    {
        public int Low { get; set; }
        public int High { get; set; }

        public int RentLow { get; set; }
        public int RentHigh { get; set; }
    }
}
