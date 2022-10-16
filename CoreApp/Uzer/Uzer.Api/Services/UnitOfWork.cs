using Context.Common;
using Uzer.Context;
using Uzer.Repository;

namespace Uzer.Api.Services
{
    public class UnitOfWork : ContextAware<UserContext>, IUnitOfWork, IDisposable
    {
        private readonly UserContext? _context;
        public UnitOfWork(UserContext userContext) : base(null)
        {
            _context = userContext;
            Users = new UserRepository(_context);
        }

        public UnitOfWork(UserContext userContext, ContextFactory<UserContext>? contextFactory = null) : base(contextFactory)
        {
            _context = userContext ?? Context();
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public async Task<int> DeadlineAsync()
        {
            if (_context == null) return -1;
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
