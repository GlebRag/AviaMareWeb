using AviaMare.Data;
using AviaMare.Data.Models;
using AviaMare.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AviaMare.Services
{
    public class UserService
    {
        private AuthService _authService;
        private IUserRepositryReal _userRepositryReal;
        private WebDbContext _webDbContext;
        public const string DEFAULT_AVATAR = "/images/AnimeGirl/avatar-default.webp";

        public UserService(AuthService authService, WebDbContext webDbContext, IUserRepositryReal userRepositryReal)
        {
            _webDbContext = webDbContext;
            _authService = authService;
            _userRepositryReal = userRepositryReal;
        }

        //public string GetAvatar()
        //{
        //    var userId = _authService.GetUserId();
        //    if (userId is null)
        //    {
        //        return DEFAULT_AVATAR;
        //    }

        //    var user = _userRepositryReal.Get(userId.Value);
        //    return user.AvatarUrl;
        //}

        public bool IsThisUserBoughtThisTicket(int ticketId, int userId)
        {
            var user = _webDbContext.Users.First(x => x.Id == userId);
            var ticket = _webDbContext.Tickets.First(x => x.Id == ticketId);
            if (user.Ticket == ticket)
            {
                return true;
            }
            return false;

        }
    }
}
