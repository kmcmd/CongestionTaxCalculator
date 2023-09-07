using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TaxService : ITaxService
    {
        private readonly ICongestionTaxCalculatorService _calculatorService;
        private readonly IGenericRepository<Vehicle> _vehicleRepository;
        private readonly IGenericRepository<HourFee> _hourFeeRepository;
        private readonly IGenericRepository<CitySetting> _citySettingRepository;

        public TaxService(
            ICongestionTaxCalculatorService congestionTaxCalculatorService,
            IGenericRepository<Vehicle> vehicleRepository,
            IGenericRepository<HourFee> hourFeeRepository,
            IGenericRepository<CitySetting> citySettingRepository
            )
        {
            _calculatorService = congestionTaxCalculatorService;
            _vehicleRepository = vehicleRepository;
            _hourFeeRepository = hourFeeRepository;
            _citySettingRepository = citySettingRepository;
        }

        public async Task<int> GetCongestionTaxAsync(int cityId, int vehicleId, DateTime[] dates)
        {
            var city = await _citySettingRepository.GetByIdAsync(cityId);
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            var hourFeeList = (await _hourFeeRepository.GetAsync())
                .Select(c => new HourFeeModel {Start = c.Start, End = c.End, FeeAmount = c.FeeAmount })
                .ToList();

            var datelist = dates.Where(c => c.Year == 2013).ToArray();
            
            return _calculatorService
                .SetMaximumDayAmount(city.MaximunFeePerDay)
                .SetMinimumTimeCalcuteFee(city.MinimumTimeCalcuteFee)
                .SetFreeMonthes(city.FreeMonthList)
                .SetHourFee(hourFeeList)
                .GetTax(vehicle, datelist);
        }
    }
}
