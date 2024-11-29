using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Interface.Repositories
{
    public interface ITicketRepository<T> : IBaseRepository<T>
        where T : ITicket
    {
    }
}
