using AviaMare.Data.Repositories;

namespace AviaMare.Services
{
    public class UserService
    {
        private AuthService _authService;
        private IUserRepositryReal _userRepositryReal;

        public const string DEFAULT_AVATAR = "/images/AnimeGirl/avatar-default.webp";

        public UserService(AuthService authService, IUserRepositryReal userRepositryReal)
        {
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
    }
}
