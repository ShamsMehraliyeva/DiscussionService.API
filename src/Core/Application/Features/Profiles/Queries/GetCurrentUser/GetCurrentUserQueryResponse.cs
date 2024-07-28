namespace Application.Features.Profiles.Queries.GetCurrentUser;

public class GetCurrentUserQueryResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}