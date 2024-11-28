namespace AviaMare.Data.Interface.Repositories
{
    public interface IBaseRepository<T> : IBaseQueryRepository<T>, IBaseCommandRepository<T>
    {

    }

    public interface IBaseQueryRepository<T>
    {
        IEnumerable<T> GetAll();

        T? Get(int id);

        bool Any();
    }

    public interface IBaseCommandRepository<T>
    {
        void Add(T data);

        void Delete(T data);

        void Delete(int id);
    }
}
