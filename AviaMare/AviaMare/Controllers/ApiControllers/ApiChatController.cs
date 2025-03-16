using AviaMare.Data.Interface.Repositories;
using AviaMare.Data.Models;
using AviaMare.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AviaMare.Models.Home;
using AviaMare.Services;

namespace AviaMare.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiChatController : ControllerBase
    {
        private IChatMessageRepositoryReal _chatMessageRepository;

        public ApiChatController(IChatMessageRepositoryReal chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public List<string> GetLastMessages()
        {
            return _chatMessageRepository.GetLastMessages();
        }
    }
}
