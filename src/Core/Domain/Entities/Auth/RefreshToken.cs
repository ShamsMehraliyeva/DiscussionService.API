namespace Domain.Entities.Auth;

public class RefreshToken:Entity
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string CreatedByIp { get; set; }


    public virtual User User { get; set; }

    public RefreshToken()
    {
    }

    public RefreshToken(int id, string token, DateTime expires, DateTime created, string createdByIp)
    {
        Id = id;
        Token = token;
        Expires = expires;
        Created = created;
        CreatedByIp = createdByIp;
    }
}
