using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AviaMare.Models;
using AviaMare.Models.Home;
using AviaMare.Services;
using AviaMare.Data.Interface.Repositories;
using AviaMare.Data.Repositories;
using AviaMare.Data;
using Microsoft.AspNetCore.Hosting;
using AviaMare.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AviaMare.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;
        private ITicketRepositoryReal _ticketRepository;
        private IUserRepositryReal _userRepositryReal;
        private WebDbContext _webDbContext;
        public HomeController(ITicketRepositoryReal ticketRepository,
            WebDbContext webDbContext,
            IUserRepositryReal userRepositryReal,
            AuthService authService)
        {
            _ticketRepository = ticketRepository;
            _webDbContext = webDbContext;
            _userRepositryReal = userRepositryReal;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            if (!_ticketRepository.Any())
            {
                GenerateDefaultTicket();
            }

            var ticketsFromDb = _ticketRepository.GetAll();
            var ticketsViewModels = ticketsFromDb
                .Select(dbTicket =>
                    new TicketViewModel
                    {
                        Id = dbTicket.Id,
                        Destination = dbTicket.Destination,
                        Departure = dbTicket.Departure,
                        IdPlane = dbTicket.IdPlane,
                        Time = dbTicket.Time,
                        Cost = dbTicket.Cost,
                        TakeOffTime = dbTicket.TakeOffTime,
                        LandingTime = dbTicket.LandingTime,
                    }
                )
                .ToList();  

            return View(ticketsViewModels);
        }

        private void GenerateDefaultTicket()
        {
            for (int i = 0; i < 2; i++)
            {
                var dataModel = new TicketData
                {
                    Destination = "1",
                    Departure = "2",
                    IdPlane = 3,
                    Time = 4,
                    Cost = 5,
                    TakeOffTime = DateTime.Now,
                    LandingTime = DateTime.Now,
                };

                _ticketRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult CreateTicket()
        {
            var viewModel = new TicketCreationViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateTicket(TicketCreationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var currentUserId = _authService.GetUserId();

            var dataTicket = new TicketData
            {
                Destination = viewModel.Destination,
                Departure = viewModel.Departure,
                IdPlane = viewModel.IdPlane,
                Time = viewModel.Time,
                Cost = viewModel.Cost,
                TakeOffTime = viewModel.TakeOffTime,
                LandingTime = viewModel.LandingTime
            };

            _ticketRepository.Create(dataTicket);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            _ticketRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
