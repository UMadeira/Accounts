namespace Accounts.Data.Classes
{
    internal class Item : IItem
    {
        public int    Id        { get; set; } = 0;
        public bool   Zombie    { get; set; } = false;
        public byte[] TimeStamp { get; set; }

        public IItem Entity => this;
    }
}
