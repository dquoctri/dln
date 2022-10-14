namespace Authentication.Entity
{
    public class Account
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        private DateTime? LastLogin { get; set; }

    }
}