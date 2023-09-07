using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public VehicleTypeEnum VehicleType { get; set; }
    }
}
