using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMare.Data.Interface.Models
{
    public interface IChatMessageData : IBaseModel
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; }
    }
}
