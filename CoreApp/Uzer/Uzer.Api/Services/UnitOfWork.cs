using Uzer.Context;
using Uzer.Repository;

namespace Uzer.Api.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext _context;
        public UnitOfWork(UserContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public async Task<int> DeadlineAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
