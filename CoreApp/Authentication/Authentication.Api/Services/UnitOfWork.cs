﻿using Microsoft.EntityFrameworkCore;

namespace Authentication.Api.Services
{
    public class UnitOfWork<T> : IUnitOfWork, IDisposable where T : DbContext, new()
    {
        private readonly DbContext _context;
  
        public UnitOfWork(T context)
        {
            _context = context;
        }

        public int Deadline()
        {
            return _context.SaveChanges();
        }

        public async Task<int> DeadlineAsync()
        {
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
