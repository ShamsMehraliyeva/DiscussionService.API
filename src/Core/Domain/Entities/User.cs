namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public User(int id):base(id)
        {
        }
    }
}
