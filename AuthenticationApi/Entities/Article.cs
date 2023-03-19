using AuthenticationApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ContentCreators.Entities
{
    public class Article
    {        
        public int Id { get; set; }

        [NotNull]
        [StringLength(255)]
        public string Title { get; set; }

        [NotNull]
        [StringLength(500)]
        public string Text { get; set; }

        public DateTime Created_at { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
