using Microsoft.AspNetCore.Mvc.Rendering;

namespace AviaMare.Models.Home
{
    public class IndexViewModel
    {
        public string UserName { get; set; }

        public List<TicketViewModel> Tickets { get; set; }
    }
}
