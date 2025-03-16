using AviaMare.Data.Interface.Repositories;
using AviaMare.Data.Models;
using AviaMare.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AviaMare.Models.Home;
using AviaMare.Services;

namespace AviaMare.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiTicketsController : ControllerBase
    {
        private ITicketRepositoryReal _ticketRepository;
        private AuthService _authService;

        public ApiTicketsController(ITicketRepositoryReal ticketRepository, AuthService authService)
        {
            _ticketRepository = ticketRepository;
            _authService = authService;
        }

        public bool Remove(int id)
        {
            _ticketRepository.Delete(id);
            return true;
        }

        public List<TicketViewModel> SearchTicket(string departure, string destination, DateTime takeOffTime, decimal cost, string sortOrder)
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

            return viewModels;
        }


    }
}
