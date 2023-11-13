using RubiconmpSolution.Core.Entities.Abstract;

namespace RubiconmpSolution.Core.Entities.DTO.Auth;

public class RegisterDto : IDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}