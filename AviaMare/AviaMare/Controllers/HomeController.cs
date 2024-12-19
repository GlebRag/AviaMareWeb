using Microsoft.AspNetCore.Mvc;
using AviaMare.Models.Home;
using AviaMare.Services;
using AviaMare.Data.Repositories;
using AviaMare.Data;
using AviaMare.Data.Models;
using AviaMare.Controllers.AuthAttributes;
using AviaMare.Models.Home.Profile;
using AviaMare.Data.Interface.Models;

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
                        Count = dbTicket.Count
                    }
                )
                .ToList();  

            return View(ticketsViewModels);
        }

        public IActionResult SearchTicket(string departure, string destination, DateTime takeOffTime, decimal cost, string sortOrder)
        {

            var viewModels = _ticketRepository
                .SearchTicket(departure, destination, takeOffTime, cost, sortOrder)
                .Select(dbTicket => new TicketViewModel
                {
                    Id = dbTicket.Id,
                    Destination = dbTicket.Destination,
                    Departure = dbTicket.Departure,
                    IdPlane = dbTicket.IdPlane,
                    Time = dbTicket.Time,
                    Cost = dbTicket.Cost,
                    TakeOffTime = dbTicket.TakeOffTime,
                    LandingTime = dbTicket.LandingTime,
                    Count = dbTicket?.Count ?? 0
                })
                .ToList();

            return View("Index", viewModels);
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
                    Count = 0
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
                LandingTime = viewModel.LandingTime,
                Count = viewModel.Count
            };

            _ticketRepository.Create(dataTicket);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult LinkUserAndTicket(int ticketId, int userId)
        {
            _ticketRepository.BuyTicket(ticketId, userId);
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

        public IActionResult Profile()
        {
            if (!_authService.IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth" );
            }

            var viewModel = new ProfileViewModel();

            viewModel.UserName = _authService.GetName()!;

            var userId = _authService.GetUserId()!.Value;

            //viewModel.AvatarUrl = _userRepositryReal.GetAvatarUrl(userId);

            var ticketForUser = _ticketRepository.GetTicket(userId);
            if (ticketForUser.All(ticket => ticket == null))
            {
                return View(viewModel);
            }
            viewModel.Tickets = ticketForUser
                .Select(x => new TicketShortInfoViewModel
                {
                    Destination = x.Destination,
                    Departure = x.Departure,
                    IdPlane = x.IdPlane,
                    Time = x.Time,
                    TakeOffTime = x.TakeOffTime,
                    LandingTime = x.LandingTime
                })
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            _ticketRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
