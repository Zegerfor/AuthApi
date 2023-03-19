using ContentCreators.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationApi.Entities;

public class User : IdentityUser
{
    public virtual ICollection<Article> Article { get; set; }

    public User()
    {
        Article = new List<Article>();
    }
}