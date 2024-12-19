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
using Microsoft.Data.SqlClient;
using System.Text;

namespace AviaMare.Data.Repositories
{
    public interface ITicketRepositoryReal : ITicketRepository<TicketData>
    {
        void BuyTicket(int ticketId, int userId);
        void Create(TicketData dataTicket);
        IEnumerable<TicketData> GetTicket(int userId);
        bool IsThisUserBoughtThisTicket(int ticketId, int userId);
        IEnumerable<TicketShortInfo> SearchTicket(string departure, string destination, DateTime? takeOffTime, decimal? cost, string sortOrder);
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

        public IEnumerable<TicketShortInfo> SearchTicket(string departure, string destination, DateTime? takeOffTime, decimal? cost, string sortOrder)
        {
            var parameters = new List<SqlParameter>();
            var sql = new StringBuilder("SELECT * FROM dbo.Tickets WHERE 1=1"); //Это условие всгеда истинно

            if (departure != "null")
            {
                sql.Append(" AND Departure = @Departure");
                parameters.Add(new SqlParameter("@Departure", departure));
            }

            if (destination != "null")
            {
                sql.Append(" AND Destination = @Destination");
                parameters.Add(new SqlParameter("@Destination", destination));
            }

            // Условия для TakeOffTime
            if (takeOffTime.HasValue)
            {
                if (takeOffTime.Value == DateTime.MinValue) // Если дата равна 01.01.0001
                {
                    // От сегодняшней даты и после (использовать потом)
                    //sql.Append(" AND TakeOffTime >= @CurrentDate");
                    //parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now.Date));

                    //От сегодняшней даты и до(временно)
                    sql.Append(" AND TakeOffTime <= @CurrentDate");
                    parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now.Date));
                }
                else
                {
                    sql.Append(" AND CAST(TakeOffTime AS DATE) = @TakeOffTime");
                    parameters.Add(new SqlParameter("@TakeOffTime", takeOffTime.Value.Date));
                }
            }

            if (cost.HasValue)
            {
                sql.Append(" AND Cost <= @Cost");
                parameters.Add(new SqlParameter("@Cost", cost.Value));
                if (sortOrder == "asc")
                {
                    sql.Append(" ORDER BY Cost ASC");
                }
                if(sortOrder == "desc")
                {
                    sql.Append(" ORDER BY Cost DESC");
                }
            }

            var result = _webDbContext
                .Database
                .SqlQueryRaw<TicketShortInfo>(sql.ToString(), parameters.ToArray())
                .ToList();

            return result;
        }

        //TODO: FindByDestination, FindByDeparture, FindByDate, FindByMaxCost, FindByMinCost, FindByTime
    }
}
