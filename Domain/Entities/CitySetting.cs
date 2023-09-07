using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CitySetting : BaseEntity
    {
        public string Name { get; set; }

        public int MaximunFeePerDay { get; set; }

        /// <summary>
        /// indecate mineuts for caculte fee for a car from last calculation
        /// </summary>
        public int MinimumTimeCalcuteFee { get; set; }

        public List<int> FreeMonthList { get; set; }
    }
}
