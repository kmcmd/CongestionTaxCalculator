using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class HourFeeModel
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public int FeeAmount { get; set; }
    }
}
