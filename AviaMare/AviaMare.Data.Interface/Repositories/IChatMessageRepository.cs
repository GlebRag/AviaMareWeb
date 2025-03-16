using AviaMare.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMare.Data.Interface.Repositories
{
    public interface IChatMessageRepository<T> : IBaseRepository<T>
        where T : IChatMessageData
    {
    }
}
