﻿namespace Domain.Entities.Auth
{
    public class User:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }
    }
}