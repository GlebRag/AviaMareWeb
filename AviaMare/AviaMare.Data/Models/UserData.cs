using Enums.Users;
using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Models
{
    public class UserData : BaseModel, IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public virtual TicketData? Ticket { get; set; }
    }
}
