namespace Enums.Users
{
    [Flags]
    public enum Role
    {
        User = 1,                // x 0000 0001
        Admin = 2,               // x 0000 0010
        AviaMareModerator = 4, // x 0000 0100
                                // x 0000 1000 умножаем число на 2, т.е след роль будет числом 8
        
    }
}
