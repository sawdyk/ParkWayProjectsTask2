using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkWayProjectsTask2.Helpers.Utilities;
using ParkWayProjectsTask2.Models;
using ParkWayProjectsTask2.Models.ResponseModels;
using ParkWayProjectsTask2.Repository.Transactions.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ParkWayProjectsTask2.Repository.Transactions.Repository
{
    public class TransactionsRepo : ITransactionsRepo
    {
        private readonly ILogger<TransactionsRepo> _logger;

        public TransactionsRepo(ILogger<TransactionsRepo> logger)
        {
            _logger = logger;
        }
        public async Task<TransactionSurchargeResponseModel> calculateTransactionSurchargeAsync(long amount)
        {
            try
            {
                //check if the json configuration file exists
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fees.config.json")))
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fees.config.json");

                    long transferAmount = 0;
                    long debitAmount = 0;
                    long charge = 0;

                    //check if the amount is greater than 0
                    if (amount > 0)
                    {
                        using (StreamReader r = new StreamReader(path))
                        {
                            string json = r.ReadToEnd();
                            ChargesModel data = JsonConvert.DeserializeObject<ChargesModel>(json);

                            long countFees = data.fees.Count;

                            for (int i = 0; i < countFees; i++)
                            {
                                if (amount >= data.fees[i].minAmount && amount <= data.fees[i].maxAmount)
                                {
                                    charge = data.fees[i].feeAmount;
                                    transferAmount = amount - charge;
                                    debitAmount = transferAmount + charge;

                                    return new TransactionSurchargeResponseModel
                                    {
                                        Code = System.Net.HttpStatusCode.OK,
                                        Message = "Transaction Surcharge Fee Calculation was Successful",
                                        Amount = amount,
                                        TransferAmount = transferAmount,
                                        Charge = charge,
                                        DebitedAmount = debitAmount
                                    };

                                }
                            }
                        }
                    }

                    return new TransactionSurchargeResponseModel
                    {
                        Code = System.Net.HttpStatusCode.BadRequest,
                        Message = "Amount to be Transferred must be greater than 0"
                    };
                }
              
                return new TransactionSurchargeResponseModel 
                { 
                    Code = System.Net.HttpStatusCode.BadRequest, 
                    Message = "Configuration File Doesn't Exists, Please kindly upload the json Configuration file!" 
                };

            }
            catch (Exception exMessage)
            {
                //Logs Information
                var logInfo = new Logger(_logger);
                logInfo.logException(exMessage);
                return new TransactionSurchargeResponseModel { Code = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occured" };
            }
        }
    }
}
