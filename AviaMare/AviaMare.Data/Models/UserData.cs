using Enums.Users;
using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Models
{
    public class UserData : BaseModel, IUserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public virtual TicketData? Ticket { get; set; }

        public Language Language { get; set; }

        public virtual List<TicketData> TicketsWhichUsersLike { get; set; }
        public virtual List<ChatMessageData> ChatMessages { get; set; } = new();
    }
}
