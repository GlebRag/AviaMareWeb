using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Interface.Repositories
{
    public interface IPlaneRepository<T> : IBaseRepository<T>
        where T : IPlaneData
    {
    }
}
