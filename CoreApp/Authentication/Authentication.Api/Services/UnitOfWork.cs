using Authentication.Context;
using Authentication.Repository;
using Context.Common;

namespace Authentication.Api.Services
{
    public class UnitOfWork : ContextAware<AuthenticationContext>, IUnitOfWork, IDisposable
    {
        private readonly AuthenticationContext? _context;

        public IAccountRepository Accounts { get; private set; }

        public UnitOfWork(AuthenticationContext context, ContextFactory<AuthenticationContext>? contextFactory = null) : base(contextFactory)
        {
            if (contextFactory != null)
            {
                context = CreateContext() ?? context;
            }
            _context = context;
            Accounts = new AccountRepository(context);
        }

        /// <summary>
        /// The unit of work for unit testing
        /// </summary>
        /// <param name="contextFactory">Creational factory to create a context</param>
        public UnitOfWork(ContextFactory<AuthenticationContext> contextFactory) : base(contextFactory)
        {
            var context = CreateContext();
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
            Accounts = new AccountRepository(context);
        }

        public async Task<int> DeadlineAsync()
        {
            if (_context == null) return -1;
            return await _context.SaveChangesAsync();
        }

        public int Deadline()
        {
            if (_context == null) return -1;
            return _context.SaveChanges();
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
