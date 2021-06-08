namespace Accounts.Data
{
    interface IUser : IItem
    {
        string Password { get; set; }
        string Username { get; set; }
    }
}