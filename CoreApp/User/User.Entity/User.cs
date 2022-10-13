using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string? EmailAddress { get; set; }
        private bool EmailVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        private DateTime? LastLogin { get; set; }
        private Organisation Organisation { get; set; }
    }
}
