using Enums.Users;

namespace AviaMare.Data.Interface.Models
{
    public interface IUserData : IBaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public Role Role { get; set; }
    }
}
