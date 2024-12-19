using AviaMare.Data.Models;
using AviaMare.Data;
using Enums.Users;
using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Interface.Repositories;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using AviaMare.Data.Interface.Models;
using Microsoft.IdentityModel.Tokens;

namespace AviaMare.Data.Repositories
{
    public interface ITicketRepositoryReal : ITicketRepository<TicketData>
    {
        void BuyTicket(int ticketId, int userId);
        void Create(TicketData dataTicket);
        IEnumerable<TicketData> GetTicket(int userId);
        bool IsThisUserBoughtThisTicket(int ticketId, int userId);
    }

    public class TicketRepository : BaseRepository<TicketData>, ITicketRepositoryReal
    {
        public TicketRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void BuyTicket(int ticketId, int userId)
        {
            var user = _webDbContext.Users.First(x => x.Id == userId);
            var ticket = _dbSet.First(x => x.Id == ticketId);
            ticket.Buyers.Add(user);
            ticket.Count--;
            _webDbContext.SaveChanges();
        }

        public void Create(TicketData dataTicket)
        {
            Add(dataTicket);
        }

        public IEnumerable<TicketData> GetTicket(int userId)
        {
            var result = _webDbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Ticket);

            
            return result.ToList();

        }

        public bool IsThisUserBoughtThisTicket(int ticketId, int userId)
        {
            var user = _webDbContext.Users.First(x => x.Id == userId);
            var ticket = _dbSet.First(x => x.Id == ticketId);
            if(user.Ticket == ticket)
            {
                return true;
            }
            return false;

        }

        //TODO: FindByDestination, FindByDeparture, FindByDate, FindByMaxCost, FindByMinCost, FindByTime
    }
}
