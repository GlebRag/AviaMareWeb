using AviaMare.Data.Models;
using AviaMare.Data;
using Enums.Users;
using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Interface.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Data.Repositories
{
    public interface ITicketRepositoryReal : ITicketRepository<TicketData>
    {

    }

    public class TicketRepository : BaseRepository<TicketData>, ITicketRepositoryReal
    {
        public TicketRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        //TODO: FindByDestination, FindByDeparture, FindByDate, FindByMaxCost, FindByMinCost, FindByTime, BuyTicket
    }
}
