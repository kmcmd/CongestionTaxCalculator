using Application.Constants;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
    {
        private List<HourFeeModel> _hourFeeList;
        private int maximumFee = 60;
        private int minimumTimeCalcuteFeeMineutes = 60;
        private DateTime[] _holiDays;
        private VehicleTypeEnum[] _freeVehicles;
        private List<int> _freeMonthList = new List<int>();

        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            if (dates == null)
                throw new ArgumentNullException(nameof(dates));

            int totalFee = 0;
            var dayGroups = dates.GroupBy(c => new { c.Year, c.DayOfYear }).ToList();

            foreach (var item in dayGroups)
            {
                totalFee += GetTaxOneDay(vehicle, item.ToArray());
            }

            return totalFee;
        }

        /**
        * Calculate the total toll fee for one day
        *
        * @param vehicle - the vehicle
        * @param dates   - date and time of all passes on one day
        * @return - the total congestion tax for that day
        */
        public int GetTaxOneDay(Vehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            if (dates == null)
                throw new ArgumentNullException(nameof(dates));
            if(_hourFeeList == null)
                throw new NullReferenceException(ConstantMessages.NullHourFeeListMessage);

            if (dates.Length == 0)
                return 0;
            var dateList = dates.OrderBy(c => c).ToList();
            int totalFee = 0;
            DateTime lastFeeTime = default;

            foreach (DateTime date in dateList)
            {
                if ((date - lastFeeTime).TotalMinutes < minimumTimeCalcuteFeeMineutes)
                    continue;

                var fee = GetTollFee(date, vehicle);
                if (fee == 0)
                    continue;
                totalFee += fee;
                lastFeeTime = date;

                if (totalFee >= maximumFee)
                    return maximumFee;
            }

            return totalFee;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            var freeVehicles = GetFreeVehicles();

            if (freeVehicles.Any(c=> c == vehicle.VehicleType))
                return true;

            return false;
        }

        private int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            var time = date.TimeOfDay;
            var hourFee = _hourFeeList.FirstOrDefault(c =>
                (c.Start < c.End && c.Start <= time && c.End >= time) ||
                (c.Start > c.End && c.Start <= time || c.End >= time));

            return hourFee?.FeeAmount ?? 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;
            var nextDay = date.AddDays(1);

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;
            //check for free monthes
            if (GetFreeMonthes().Contains(month))
                return true;
            var freeDays = GetHoliDays();
            //check is holiday
            if (freeDays.Any(c => c.Month == month && c.Day == day))
                return true;
            //check is a day before holiday
            if (freeDays.Any(c => c.Month == nextDay.Month && c.Day == nextDay.Day))
                return true;

            return false;
        }

        private DateTime[] GetHoliDays()
        {
            if (_holiDays == null)
            {
                _holiDays =  new DateTime[]
                {
                    new DateTime(1, 1, 1),
                    new DateTime(1, 3, 28),
                    new DateTime(1, 3, 29),
                    new DateTime(1, 4, 1),
                    new DateTime(1, 4, 30),
                    new DateTime(1, 5, 1),
                    new DateTime(1, 5, 8),
                    new DateTime(1, 5, 9),
                    new DateTime(1, 6, 5),
                    new DateTime(1, 6, 6),
                    new DateTime(1, 6, 21),
                    new DateTime(1, 11, 1),
                    new DateTime(1, 12, 24),
                    new DateTime(1, 12, 25),
                    new DateTime(1, 12, 26),
                    new DateTime(1, 12, 31),
                };
            }

            return _holiDays;
        }
        public ICongestionTaxCalculatorService SetHoliDays(DateTime[] dates)
        {
            _holiDays = dates;
            return this;
        }

        public ICongestionTaxCalculatorService SetHourFee(List<HourFeeModel> hourFeeList)
        {
            _hourFeeList = hourFeeList;
            return this;
        }
        public ICongestionTaxCalculatorService GetFreeVehicles(VehicleTypeEnum[] vehicles)
        {
            _freeVehicles = vehicles;
            return this;
        }
        public ICongestionTaxCalculatorService SetFreeMonthes(List<int> freeMonthes)
        {
            _freeMonthList = freeMonthes;
            return this;
        }
        public ICongestionTaxCalculatorService SetMaximumDayAmount(int amount)
        {
            maximumFee = amount;
            return this;
        }

        public ICongestionTaxCalculatorService SetMinimumTimeCalcuteFee(int amount)
        {
            minimumTimeCalcuteFeeMineutes = amount;
            return this;
        }

        public List<int> GetFreeMonthes()
        {
            if (_freeMonthList == null)
                _freeMonthList = new List<int>();
            return _freeMonthList;
        }
        private VehicleTypeEnum[] GetFreeVehicles()
        {
            if (_freeVehicles == null)
            {
                _freeVehicles = new VehicleTypeEnum[]
                {
                    VehicleTypeEnum.Motorcycles,
                    VehicleTypeEnum.Tractor,
                    VehicleTypeEnum.Emergency,
                    VehicleTypeEnum.Diplomat,
                    VehicleTypeEnum.Foreign,
                    VehicleTypeEnum.Military
                };
            }

            return _freeVehicles;
        }
    }
}
