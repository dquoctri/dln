using Context.Common;
using Microsoft.EntityFrameworkCore;
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

        public UnitOfWork(UserContext context, ContextFactory<UserContext>? contextFactory = null) : base(contextFactory)
        {
            if (contextFactory != null)
            {
                context = CreateContext() ?? context;
            }
            _context = context;
            Users = new UserRepository(context);
            Partners = new PartnerRepository(context);
            Organisations = new OrganisationRepository(context);
        }

        /// <summary>
        /// The unit of work for unit testing
        /// </summary>
        /// <param name="contextFactory">Creational factory to create a context</param>
        public UnitOfWork(ContextFactory<UserContext> contextFactory) : base(contextFactory)
        {
            var context = CreateContext();
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
            Users = new UserRepository(context);
            Partners = new PartnerRepository(context);
            Organisations = new OrganisationRepository(context);
        }

        public async Task<int> DeadlineAsync()
        {
            if (_context == null) return -1;
            if (_context is IDeadline deadline)
            {
                return await deadline.DeadlineAsync();
            }
            throw new DbUpdateException("The context is read-only");
        }

        public int Deadline()
        {
            if (_context == null) return -1;
            if (_context is IDeadline deadline)
            {
                return deadline.Deadline();
            }
            throw new DbUpdateException("The context is read-only");
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
