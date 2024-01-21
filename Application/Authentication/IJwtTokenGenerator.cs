using Domain.Users;

namespace Application.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}