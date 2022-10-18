using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        private DateTime? LastLogin { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}