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
            try
            {
                return View();
            }
            catch
            {
                return View("Error1");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTransactionSurchargeFee(long amount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Error1");
                }

                var result = await _transactionsRepo.calculateTransactionSurchargeAsync(amount);

                ViewData["Message"] = result.Message;
                ViewData["Amount"] = result.Amount;
                ViewData["TransferAmount"] = result.TransferAmount;
                ViewData["Charge"] = result.Charge;
                ViewData["DebitAmount"] = result.DebitedAmount;

                return View();

            }
            catch
            {
                return View("Error1");
            }
        }
    }
}
