using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.Data.Context
{
    public static class DBContextExtensions
    {
        public static void ConvertSQLLiteUnSupportedTypes(this ModelBuilder modelBuilder, DbContext context)
        {
            if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset and decimals via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                //https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/

                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(TimeSpan)
                            || property.PropertyType == typeof(TimeSpan?))
                        {
                            modelBuilder
                                .Entity(entityType.Name)
                                .Property(property.Name)
                                .HasConversion(new TimeSpanToTicksConverter());
                        }
                    }
                }
            }
        }

    }
}
