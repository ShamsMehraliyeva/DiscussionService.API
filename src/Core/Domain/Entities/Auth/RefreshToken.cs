namespace Domain.Entities.Auth;

public class RefreshToken:BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime CreateDate { get; set; }


    public virtual User User { get; set; }

    public RefreshToken()
    {
    }

    public RefreshToken(int id, string token, DateTime expireDate, DateTime createDate)
    {
        Id = id;
        Token = token;
        ExpireDate = expireDate;
        CreateDate = createDate;
    }
}
