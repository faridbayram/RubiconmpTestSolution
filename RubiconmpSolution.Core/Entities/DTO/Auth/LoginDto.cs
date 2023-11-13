using RubiconmpSolution.Core.Entities.Abstract;

namespace RubiconmpSolution.Core.Entities.DTO.Auth;

public class LoginDto : IDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}