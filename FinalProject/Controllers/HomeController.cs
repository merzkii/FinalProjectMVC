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
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = await Walletrepository.GetBalanceAsync(user);

            return View(balance);
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