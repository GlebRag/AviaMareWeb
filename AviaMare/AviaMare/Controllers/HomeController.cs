using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AviaMare.Models;
using AviaMare.Models.Home;
using AviaMare.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
