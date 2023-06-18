using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FinalProject.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IWalletRepository Walletrepository { get; set; }

        public HomeController(ILogger<HomeController> logger, IWalletRepository walletRepository)
        {
            _logger = logger;
            Walletrepository = walletRepository;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = await Walletrepository.GetBalanceAsync(userId);
            

            return View(balance);

        }


        [HttpPost]
        public async Task <IActionResult>RecordTransactionHistory( decimal amount,string transactionType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await Walletrepository.RecordTransaction(userId, amount, transactionType);

            return RedirectToAction("TransactionHistory");

        }


        [HttpGet]
        public async Task<IActionResult> TransactionHistory(DateTime? fromDate, DateTime? toDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (fromDate == null || toDate == null)
            {
               
                fromDate = new DateTime(1753, 1, 1);
                toDate = new DateTime(9999, 12, 31);
            }

            var transactionHistory = await Walletrepository.GetTransactionHistory(userId, fromDate.Value, toDate.Value);

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View(transactionHistory);
        }


        [HttpGet]
        
        public async Task<JsonResult> MyWalletAsync(string userId)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
           

            
            //var balance = await Walletrepository.GetBalanceAsync(userId);

            
            return Json(userId);
        }


        [HttpPost]
        public async Task<IActionResult> Deposit(decimal amount)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            await Walletrepository.AddMoneyToWallet(userId, amount);
            var transactionType = "Deposit";

            await Walletrepository.RecordTransaction(userId, amount, transactionType);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Withdraw(decimal amount)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transactionType = "Withdraw";

            await Walletrepository.RecordTransaction(userId, amount, transactionType);
            await Walletrepository.WithdrawMoneyFromWallet(userId, amount);


            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}