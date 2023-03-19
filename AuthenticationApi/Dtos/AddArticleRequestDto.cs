using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AuthenticationApi.Dtos;

public class AddArticleRequestDto
{
    [NotNull]
    [MaxLength(Consts.TitleMaxLength, ErrorMessage = Consts.TitleLengthValidator)]
    public string Title { get; set; }

    [NotNull]
    [MaxLength(Consts.TextMaxLength, ErrorMessage = Consts.TextLengthValidator)]
    public string Text { get; set; }
}
