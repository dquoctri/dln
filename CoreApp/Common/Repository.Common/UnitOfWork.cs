using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
  
        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentException($"{typeof(DbContext)} must not be null!");
        }

        public int Deadline()
        {
            //TODO:: CreateDate and UpdateDate behavior 
            return _context.SaveChanges();
        }

        public async Task<int> DeadlineAsync()
        {
            //TODO:: CreateDate and UpdateDate behavior 
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
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
