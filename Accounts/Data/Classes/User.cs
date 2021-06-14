namespace Accounts.Data.Classes
{
    internal class User : Item, IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
