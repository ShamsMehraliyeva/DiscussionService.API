namespace Domain.Models.Auth;

public class AccessTokenModel
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
