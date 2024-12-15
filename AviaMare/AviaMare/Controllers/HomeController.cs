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

        //[HttpGet]
        //public IActionResult CreateTicket()
        //{
        //    var viewModel = new TicketCreationViewModel();

        //    viewModel.FilmDirectors = _filmDirectorRepository
        //        .GetAll()
        //        .Select(x => new SelectListItem(x.LastName, x.Id.ToString())) // .Select(x => new SelectListItem(x.Name + " " + x.LastName, x.Id.ToString()))
        //        .ToList();

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public IActionResult CreateTicket(TicketCreationViewModel viewModel)
        //{
        //    if (_.HasSimilarUrl(viewModel.Url))
        //    {
        //        ModelState.AddModelError(
        //            nameof(MovieCreationViewModel.Url),
        //            "Такой url уже есть");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        viewModel.FilmDirectors = _filmDirectorRepository
        //            .GetAll()
        //            .Select(x => new SelectListItem(x.LastName, x.Id.ToString())) // .Select(x => new SelectListItem(x.Name + " " + x.LastName, x.Id.ToString()))
        //            .ToList();

        //        return View(viewModel);
        //    }

        //    var currentUserId = _authService.GetUserId();

        //    var dataMovie = new MovieData
        //    {
        //        Name = viewModel.Name,
        //        ImageSrc = viewModel.Url,
        //        //Tags = viewModel.Tags,
        //    };
        //    //_moviePosterRepository.Add(dataMovie);

        //    _moviePosterRepository.Create(dataMovie, currentUserId!.Value, viewModel.FilmDirectorId);

        //    return RedirectToAction("AllPosters");
        //}

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
