using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AviaMare.Models;
using AviaMare.Models.Home;
using AviaMare.Services;

namespace AviaMare.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;

        public HomeController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var userName = _authService.GetName();

            viewModel.UserName = userName;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
