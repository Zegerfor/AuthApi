using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Dtos;

public class RegisterRequest
{
    [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
    public string? UserName { get; set; }
    [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
    public string? Email { get; set; }
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordRegisterValidationError)]
    public string? Password { get; set; }
}