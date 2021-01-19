using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using nexxe.inventory.optimization.DataAccess.Interface;
using nexxe.inventory.optimization.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.Service.Implementation
{
    public class SafetyStockService : ISafetyStockService
    {
        ISafetyStockRepo _safetyStockRepo;
        public SafetyStockService(ISafetyStockRepo safetyStockRepo)
        {
            _safetyStockRepo = safetyStockRepo;
        }
        public async Task<ResponseDTO<List<SafetyStockValue>>> GetSafetyStock(SafetyStockViewModel safetyStockModel)
        {
            try
            {
                validateInputModel(safetyStockModel);
                var stockList = await _safetyStockRepo.GetDataToCalaculateSafeStock(safetyStockModel);
                CalculateSafetyStock(stockList, safetyStockModel);
                RemoveElementsFromListGreaterThenEndDate(stockList, safetyStockModel);
                return new ResponseDTO<List<SafetyStockValue>>()
                {
                    IsSuccess = true,
                    ReturnValue = stockList,
                    Exception = null,
                    ErrorCode = null,
                    CorrelationId = null,
                    ErrorMessage = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<SafetyStockValue>>()
                {
                    IsSuccess = false,
                    ReturnValue = null,
                    Exception = ex,
                    ErrorCode = null,
                    CorrelationId = null,
                    ErrorMessage = ex.Message,
                    Errors = null
                };
            }

        }

        private void validateInputModel(SafetyStockViewModel safetyStockModel)
        {
            var errors = new List<string>();
            if (safetyStockModel.EndDate < safetyStockModel.StartDate)
            {
                errors.Add("Start date should be greater then end date");
            }
            if (safetyStockModel.ItemId <= 0 || safetyStockModel.SupplierId <= 0 || safetyStockModel.LocationId <= 0)
            {
                errors.Add("ItemId or SupplierId or LocationId cannot be zero or less then zero");
            }
            if (errors.Count > 0)
            {
                throw new ApplicationException(string.Join(", ", errors));
            }
        }

        private void RemoveElementsFromListGreaterThenEndDate(List<SafetyStockValue> stockList, SafetyStockViewModel safetyStockModel)
        {
            stockList.RemoveAll(s => s.DemandDate > safetyStockModel.EndDate);
        }

        private void CalculateSafetyStock(List<SafetyStockValue> stockList, SafetyStockViewModel safetyStockModel)
        {
            for (int i = 0; i < stockList.Count; i++)
            {
                if (stockList[i].DemandDate <= safetyStockModel.EndDate)
                {
                    var demandList = new List<int>();
                    int systemLeadTime = stockList[i].SystemLeadTime;
                    for (int j = 1; j <= systemLeadTime; j++)
                    {
                        var elem = stockList.ElementAtOrDefault(i + j);
                        if (elem != null)
                        {
                            demandList.Add(elem.Demand);
                        }
                        else
                        {
                            stockList[i].SafeStock = null;
                            break;
                        }
                    }
                    stockList[i].SafeStock = 1.88 * Math.Sqrt(systemLeadTime) * CalculateStandardDeviation(demandList);
                }
            }
        }
        private double CalculateStandardDeviation(List<int> values)
        {
            double standardDeviation = 0;

            if (values.Count > 0)
            {
                // Compute the average.     
                double avg = values.Average();
                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                // Put it all together.      
                standardDeviation = Math.Sqrt((sum) / (values.Count - 1));
            }
            return standardDeviation;
        }
    }
}
