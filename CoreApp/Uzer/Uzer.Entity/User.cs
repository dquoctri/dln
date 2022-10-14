using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uzer.Entity
{
    public class User
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        private bool EmailVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        private Organisation Organisation { get; set; }
    }
}
