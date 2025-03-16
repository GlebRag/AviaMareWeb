using AviaMare.Data.Models;
using AviaMare.Data;
using Enums.Users;
using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Interface.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Data.Repositories
{
    public interface IChatMessageRepositoryReal : IChatMessageRepository<ChatMessageData>
    {
        void AddMessage(int? userId, string message);
        List<string> GetLastMessages();
    }

    public class ChatMessageRepository : BaseRepository<ChatMessageData>, IChatMessageRepositoryReal
    {
        public const int COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM = 3;
        public ChatMessageRepository(WebDbContext context) : base(context)
        {
        }

        public void AddMessage(int? userId, string message)
        {
            var isMessageDuplicate = _dbSet
                    .OrderByDescending(x => x.CreationTime)
                    .Take(COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM)
                    .Any(x => x.Message == message);

            if (isMessageDuplicate)
            {
                //TODO Your message block - it is a spam
                return;
            }

            var messageData = new ChatMessageData
            {
                CreationTime = DateTime.Now,
                Message = message,
                User = !userId.HasValue ? null : _webDbContext.Users.First(x => x.Id == userId)
            };

            base.Add(messageData);
        }

        public List<string> GetLastMessages()
        {
            var count = 20;
            return _dbSet
                    .OrderByDescending(x => x.CreationTime)
                    .Take(count)
                    .OrderBy(x => x.CreationTime)
                    .Select(x => x.Message)
                    .ToList();
        }
    }
}
