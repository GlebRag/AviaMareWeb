using AviaMare.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMare.Data.Models
{
    public class ChatMessageData : BaseModel, IChatMessageData
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; }
        public virtual UserData? User { get; set; }
    }
}
