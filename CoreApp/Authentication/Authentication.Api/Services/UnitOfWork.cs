using Authentication.Context;
using Authentication.Repository;
using Context.Common;

namespace Authentication.Api.Services
{
    public class UnitOfWork : ContextAware<AuthenticationContext>, IUnitOfWork, IDisposable
    {
        private readonly AuthenticationContext? _context;
        public IPartnerRepository Partners { get; private set; }
        public IOrganisationRepository Organisations { get; private set; }
        public IUserRepository Users { get; private set; }
        public IAccountRepository Accounts { get; private set; }
        public IProfileRepository Profiles { get; private set; }

        public UnitOfWork(AuthenticationContext context, IContextFactory<AuthenticationContext>? contextFactory = null) : base(null)
        {
            _context = context;
            Partners = new PartnerRepository(context);
            Organisations = new OrganisationRepository(context);
            Users = new UserRepository(context);
            Accounts = new AccountRepository(context);
            Profiles = new ProfileRepository(context);
        }

        /// <summary>
        /// The unit of work for unit testing
        /// </summary>
        /// <param name="contextFactory">Creational factory to create a context</param>
        public UnitOfWork(IContextFactory<AuthenticationContext> contextFactory) : base(contextFactory)
        {
            var context = _context = CreateContext();
            Partners = new PartnerRepository(context);
            Organisations = new OrganisationRepository(context);
            Users = new UserRepository(context);
            Accounts = new AccountRepository(context);
            Profiles = new ProfileRepository(context);
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
