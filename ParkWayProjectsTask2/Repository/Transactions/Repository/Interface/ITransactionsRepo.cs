using ParkWayProjectsTask2.Models;
using ParkWayProjectsTask2.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkWayProjectsTask2.Repository.Transactions.Repository.Interface
{
    public interface ITransactionsRepo
    {
        Task<TransactionSurchargeResponseModel> calculateTransactionSurchargeAsync(long amount);
    }
}
