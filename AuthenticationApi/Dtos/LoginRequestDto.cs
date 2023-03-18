using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Dtos;

public class LoginRequest
{
    [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
    public string? Username { get; set; }
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordLoginValidationError)]
    public string? Password { get; set; }
}