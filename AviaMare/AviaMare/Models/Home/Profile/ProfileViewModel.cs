namespace AviaMare.Models.Home.Profile
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        //public string AvatarUrl { get; set; }
        public List<TicketShortInfoViewModel> Tickets { get; set; }
    }
}
