using AviaMare.Data.Repositories;
using AviaMare.Services;
using Microsoft.AspNetCore.SignalR;

namespace AviaMare.Hubs
{
    public interface IChatHub
    {
        Task NewMessageAdded(string message);
    }
    public class ChatHub : Hub<IChatHub>
    {
        private AuthService _authService;
        private IChatMessageRepositoryReal _chatMessageRepositoryReal;

        public ChatHub(AuthService authService, IChatMessageRepositoryReal chatMessageRepositoryReal)
        {
            _authService = authService;
            _chatMessageRepositoryReal = chatMessageRepositoryReal;
        }

        public void addNewMessage(string message)
        {
            var userName = _authService.GetName();
            var newMessage = $"{userName}: {message}";

            SendMessage(newMessage);
        }

        private void SendMessage(string message)
        {
            var userId = _authService.GetUserId();
            _chatMessageRepositoryReal.AddMessage(userId, message);
            Clients.All.NewMessageAdded(message).Wait();
        }
    }
}
