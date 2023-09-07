using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HourFee : BaseEntity
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public int FeeAmount { get; set; }
    }
}
