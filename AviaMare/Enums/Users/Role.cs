namespace Enums.Users
{
    [Flags]
    public enum Role
    {
        User = 1,           // x 0000 0001
        Admin = 2,          // x 0000 0010
        AnimeModerator = 4, // x 0000 0100
        AnimeAuthor = 8,    // x 0000 1000
        SurveysCreatorOrEditor = 16
    }
}
