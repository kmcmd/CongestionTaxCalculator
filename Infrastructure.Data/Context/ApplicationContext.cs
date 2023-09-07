using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.Configurationes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddDbSets(modelBuilder);
            AddEntityConfigurations(modelBuilder);
            modelBuilder.ConvertSQLLiteUnSupportedTypes(this);
        }

        private static void AddEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new HourFeeConfig());
            modelBuilder.ApplyConfiguration(new CitySettingConfig());
        }

        private void AddDbSets(ModelBuilder modelBuilder)
        {
            #region Seed Vehicle

            modelBuilder
                .Entity<Vehicle>()
                .HasData(
                    new Vehicle { Id = 1, VehicleType = VehicleTypeEnum.Buss },
                    new Vehicle { Id = 2, VehicleType = VehicleTypeEnum.Diplomat },
                    new Vehicle { Id = 3, VehicleType = VehicleTypeEnum.Emergency },
                    new Vehicle { Id = 4, VehicleType = VehicleTypeEnum.Foreign },
                    new Vehicle { Id = 5, VehicleType = VehicleTypeEnum.Military },
                    new Vehicle { Id = 6, VehicleType = VehicleTypeEnum.Motorcycles },
                    new Vehicle { Id = 7, VehicleType = VehicleTypeEnum.Tractor },
                    new Vehicle { Id = 8, VehicleType = VehicleTypeEnum.PersonalCar });
            #endregion

            #region Seed HourFee
            modelBuilder
                .Entity<HourFee>()
                .HasData(
                    new HourFee {
                        Id = 1,
                        Start = new TimeSpan(6, 0, 0),
                        End = new TimeSpan(6, 29, 0),
                        FeeAmount = 8
                    },                
                    new HourFee { 
                        Id = 2,
                        Start = new TimeSpan(6, 30, 0),
                        End = new TimeSpan(6, 59, 0),
                        FeeAmount = 13
                    },                
                    new HourFee { 
                        Id = 3,
                        Start = new TimeSpan(7, 0, 0),
                        End = new TimeSpan(7, 59, 0),
                        FeeAmount = 18
                    },                
                    new HourFee { 
                        Id = 4,
                        Start = new TimeSpan(8, 0, 0),
                        End = new TimeSpan(8, 29, 0),
                        FeeAmount = 13
                    },                
                    new HourFee { 
                        Id = 5,
                        Start = new TimeSpan(8, 30, 0),
                        End = new TimeSpan(14, 59, 0),
                        FeeAmount = 8
                    },                
                    new HourFee { 
                        Id = 6,
                        Start = new TimeSpan(15, 0, 0),
                        End = new TimeSpan(15, 29, 0),
                        FeeAmount = 13
                    },                
                    new HourFee { 
                        Id = 7,
                        Start = new TimeSpan(15, 30, 0),
                        End = new TimeSpan(16, 59, 0),
                        FeeAmount = 18
                    },                
                    new HourFee { 
                        Id = 8,
                        Start = new TimeSpan(17, 0, 0),
                        End = new TimeSpan(17, 59, 0),
                        FeeAmount = 13
                    },                
                    new HourFee { 
                        Id = 9,
                        Start = new TimeSpan(18, 0, 0),
                        End = new TimeSpan(18, 29, 0),
                        FeeAmount = 8
                    },                
                    new HourFee { 
                        Id = 10,
                        Start = new TimeSpan(18, 30, 0),
                        End = new TimeSpan(05, 59, 0),
                        FeeAmount = 0
                    });
            #endregion

            #region Seed Vehicle

            modelBuilder
                .Entity<CitySetting>()
                .HasData(
                    new CitySetting { Id = 1, Name = "Gothenburg", 
                        MaximunFeePerDay= 60, 
                        MinimumTimeCalcuteFee = 60,
                        FreeMonthList = new List<int> { 7 }
                    },
                    new CitySetting { Id = 2, Name = "London",
                        MaximunFeePerDay= 80,
                        MinimumTimeCalcuteFee = 30,
                    }
                );
            #endregion
        }
    }
}
