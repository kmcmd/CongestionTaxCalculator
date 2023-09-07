using Application;
using Application.Constants;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.Services
{
    public class CongestionTaxCalculatorServiceTest : BaseTest
    {
        private ICongestionTaxCalculatorService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new CongestionTaxCalculatorService();
        }

        [Test]
        public void GetTaxOneDay_With_Empty_Array_Dates_Returns_Zero()
        {
            var vehicle = _fixture.Create<Vehicle>();
            var dates = new DateTime[] { };
            var hourFeeList = _fixture.Create<List<HourFeeModel>>();

            var result = _service
                .SetHourFee(hourFeeList)
                .GetTaxOneDay(vehicle, dates);

            result.Should().Be(0);
        }

        [Test]
        public void GetTaxOneDay_With_Two_Date_Less_MaximumDayAmount_Calculte_One()
        {
            Vehicle vehicle = GetVehicle(VehicleTypeEnum.PersonalCar);
            var extepedAmout = 5;
            HourFeeModel hourFee1 = GetOneHourFees(9, 11, extepedAmout);
            var hourFeeList = new List<HourFeeModel> { hourFee1 };

            var dates = new DateTime[2] {
                new DateTime(2000, 1, 20, 9, 0, 0),
                new DateTime(2000, 1, 20, 9, 25, 0)
            };

            var result = _service
                .SetHourFee(hourFeeList)
                .SetMaximumDayAmount(50)
                .GetTaxOneDay(vehicle, dates);

            result.Should().Be(extepedAmout);
        }

        [Test]
        public void GetTaxOneDay_Just_Calculte_Up_To_Maximun()
        {
            Vehicle vehicle = GetVehicle(VehicleTypeEnum.PersonalCar);
            HourFeeModel hourFee1 = GetOneHourFees(9, 11, 40);
            HourFeeModel hourFee2 = GetOneHourFees(12, 15, 30);
            var hourFeeList = new List<HourFeeModel> { hourFee1, hourFee2 };
            var maxAmount = 60;

            var dates = new DateTime[2] {
                new DateTime(2000, 1, 20, 9, 0, 0),
                new DateTime(2000, 1, 20, 14, 55, 0)
            };

            _service.SetMaximumDayAmount(maxAmount);
            var result = _service.SetHourFee(hourFeeList)
                .GetTaxOneDay(vehicle, dates);

            result.Should().Be(maxAmount);
        }

        [Test]
        public void GetTaxOneDay_Check_Correct_Fee_When_Start_HourFee_Greater_Than_Start_Hour()
        {
            var excepedAmoutnt = 40;
            Vehicle vehicle = GetVehicle(VehicleTypeEnum.PersonalCar);
            HourFeeModel hourFee1 = GetOneHourFees(18, 5, excepedAmoutnt);
            var hourFeeList = new List<HourFeeModel> { hourFee1 };
            var dates = new DateTime[] {
                new DateTime(2000, 1, 20, 20, 0, 0)
            };

            var result = _service.SetHourFee(hourFeeList)
                .GetTaxOneDay(vehicle, dates);

            result.Should().Be(excepedAmoutnt);
        }

        private HourFeeModel GetOneHourFees(int fromHour, int toHour, int amout)
        {
            return _fixture.Build<HourFeeModel>()
               .With(c => c.Start, new TimeSpan(fromHour, 0, 0))
               .With(c => c.End, new TimeSpan(toHour, 0, 0))
               .With(c => c.FeeAmount, amout)
               .Create();
        }

        private Vehicle GetVehicle(VehicleTypeEnum vehicle)
        {
            return _fixture.Build<Vehicle>()
                .With(c => c.VehicleType, vehicle)
                .Create();
        }

        [Test]
        public void GetTax_With_Invalid_Argument_Faild()
        {
            var vehicle = _fixture.Create<Vehicle>();

            Action action = ()=> _service.GetTax(vehicle, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void GetTaxOneDay_With_Invalid_Argument_Faild()
        {
            var vehicle = _fixture.Create<Vehicle>();
            var dates = _fixture.Create<DateTime[]>();

            Action action = ()=> _service.GetTaxOneDay(null, dates);
            action.Should().Throw<ArgumentNullException>();
            //
            action = () => _service.GetTaxOneDay(vehicle, null);
            action.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void GetTaxOneDay_When_HourFee_Is_null_Faild()
        {
            var vehicle = _fixture.Create<Vehicle>();
            var dates = _fixture.Create<DateTime[]>();

            Action action = ()=> _service.GetTaxOneDay(vehicle, dates);
            action.Should().Throw<NullReferenceException>()
                .WithMessage(ConstantMessages.NullHourFeeListMessage);
        }
    }
}
