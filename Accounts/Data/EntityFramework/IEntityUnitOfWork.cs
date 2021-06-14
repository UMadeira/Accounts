using Accounts.Patterns.Repository;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Data.EntityFramework
{
    interface IEntityUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; }
    }
}
