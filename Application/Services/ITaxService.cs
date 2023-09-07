using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITaxService
    {
        Task<int> GetCongestionTaxAsync(int cityId, int vehicleId, DateTime[] dates);
    }
}
