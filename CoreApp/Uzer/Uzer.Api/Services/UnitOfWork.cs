using Context.Common;
using Uzer.Context;
using Uzer.Repository;

namespace Uzer.Api.Services
{
    public class UnitOfWork : ContextAware<UserContext>, IUnitOfWork, IDisposable
    {
        private readonly UserContext? _context;

        public IUserRepository Users { get; private set; }
        public IOrganisationRepository Organisations { get; private set; }
        public IPartnerRepository Partners { get; private set; }

        public UnitOfWork(UserContext userContext) : base(null)
        {
            _context = userContext;
            Partners = new PartnerRepository(userContext);
            Organisations = new OrganisationRepository(userContext);
            Users = new UserRepository(userContext);
        }

        public UnitOfWork(UserContext userContext, ContextFactory<UserContext>? contextFactory = null) : base(contextFactory)
        {
            var context = userContext ?? Context();
            _context = context;
            Users = new UserRepository(context);
            Partners = new PartnerRepository(context);
            Organisations = new OrganisationRepository(context);
        }

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
