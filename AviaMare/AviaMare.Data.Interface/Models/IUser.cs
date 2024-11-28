using Enums.Users;

namespace AviaMare.Data.Interface.Models
{
    public interface IUser : IBaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
