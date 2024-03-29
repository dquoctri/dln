﻿using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class PartnerRepository : CrudRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Partner? GetByName(string name)
        {
            return _context.Set<Partner>().Where(x => x.Name == name).FirstOrDefault();
        }

        public bool IsExistedName(string name)
        {
            return _context.Set<Partner>().Any(x => x.Name.Equals(name));
        }
    }
}
