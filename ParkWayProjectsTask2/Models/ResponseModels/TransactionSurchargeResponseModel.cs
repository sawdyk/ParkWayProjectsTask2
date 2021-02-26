using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWayProjectsTask2.Models.ResponseModels
{
    public class TransactionSurchargeResponseModel
    {
        public System.Net.HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public long Amount { get; set; }
        public long TransferAmount { get; set; }
        public long Charge { get; set; }
        public long DebitedAmount { get; set; }
    }
}
