namespace Passwordless.SampleApi.DTOs;

public class RegisterUserRequest
{
    public Guid UserId { get; init; } = Guid.Empty;
    public string UserName { get; init; }
    public IEnumerable<string> Aliases { get; init; } = new List<string>();

}