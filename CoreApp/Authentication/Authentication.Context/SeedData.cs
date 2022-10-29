using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Context
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AuthenticationContext(
                serviceProvider.GetRequiredService<DbContextOptions<AuthenticationContext>>()))
            {
                context.Database.Migrate();
                InitPartners(context);
                InitOrganizers(context);
                InitAccounts(context);
                context.SaveChanges();
            }
        }

        public static void InitPartners(AuthenticationContext context)
        {
            // Look for any partners.
            if (context.Partners == null || context.Partners.Any())
            {
                return;
            }
            context.Partners.AddRange(
                new Partner
                {
                    Name = "System",
                    Description = "The system partner"
                }
            );
        }

        public static void InitOrganizers(AuthenticationContext context)
        {
            // Look for any partners.
            if (context.Partners == null || context.Organizers == null || context.Organizers.Any())
            {
                return;
            }
            var partner = context.Partners.Where(p => "System".Equals(p.Name)).FirstOrDefault();
            // No partner found
            if (partner == null) {
                return;
            };
            context.Organizers.AddRange(
                new Organizer
                {
                    Name = "System",
                    Description = "The system partner",
                    Partner = partner,
                }
            );
        }

        public static void InitAccounts(AuthenticationContext context)
        {
            // Look for any accounts.
            if (context.Organizers == null || context.Accounts == null || context.Accounts.Any())
            {
                return;   // DB has been seeded
            }
            var organizer = context.Organizers.Where(p => "System".Equals(p.Name)).FirstOrDefault();
            // No organizer found
            if (organizer == null)
            {
                return;
            };
            context.Accounts.AddRange(
                new Account
                {
                    Username = "admin",
                    PasswordHash = "123547",
                    Salt = "abc",
                    CreatedDate = DateTime.UtcNow,
                    Organizer = organizer,
                },
                new Account
                {
                    Username = "user",
                    PasswordHash = "123547",
                    Salt = "abc",
                    CreatedDate = DateTime.UtcNow,
                    Organizer = organizer,
                }
            );
        }
    }
}
