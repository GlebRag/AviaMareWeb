using AviaMare.Data.Models;
using AviaMare.Data;
using Enums.Users;
using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Interface.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Data.Repositories
{
    public interface IPlaneRepositoryReal : IPlaneRepository<PlaneData>
    {

    }

    public class PlaneRepository : BaseRepository<PlaneData>, IPlaneRepositoryReal
    {
        public PlaneRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }
    }
}
