using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface ICongestionTaxCalculatorService
    {
        int GetTax(Vehicle vehicle, DateTime[] dates);
        int GetTaxOneDay(Vehicle vehicle, DateTime[] dates);
        ICongestionTaxCalculatorService SetHourFee(List<HourFeeModel> hourFeeList);
        ICongestionTaxCalculatorService SetMinimumTimeCalcuteFee(int amount);
        ICongestionTaxCalculatorService SetMaximumDayAmount(int amount);
        ICongestionTaxCalculatorService SetFreeMonthes(List<int> freeMonthes);
        ICongestionTaxCalculatorService SetHoliDays(DateTime[] dates);
        ICongestionTaxCalculatorService GetFreeVehicles(VehicleTypeEnum[] vehicles);
    }
}
