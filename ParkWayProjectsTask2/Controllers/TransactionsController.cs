using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkWayProjectsTask2.Repository.Configuration.Repository.Interface;
using ParkWayProjectsTask2.Repository.Transactions.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWayProjectsTask2.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepo _transactionsRepo;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionsRepo transactionsRepo, ILogger<TransactionsController> logger)
        {
            _transactionsRepo = transactionsRepo;
            _logger = logger;
        }

        // GET: TransactionsController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CalculateTransactionSurchargeFee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTransactionSurchargeFee(long amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _transactionsRepo.calculateTransactionSurchargeAsync(amount);

            ViewData["Message"] = result.Message;
            ViewData["Amount"] = result.Amount;
            ViewData["TransferAmount"] = result.TransferAmount;
            ViewData["Charge"] = result.Charge;
            ViewData["DebitAmount"] = result.DebitedAmount;

            //logs the information of the transaction
            _logger.LogInformation(string.Format("Message: {0} Amount: {1} Transfer Amount: {2} Charge: {3} DebitedAmoumt: {4}", result.Message, result.Amount, result.TransferAmount, result.Charge, result.DebitedAmount));

            return View();
        }
    }
}
