using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Interface.Repositories
{
    public interface IUserRepositry<T> : IBaseRepository<T>
        where T : IUser
    {
    }
}
