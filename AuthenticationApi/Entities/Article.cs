using AuthenticationApi.Entities;

namespace ContentCreators.Entities
{
    public class Article
    {        
        public int Id { get; set; }
       
        public string Title { get; set; }
        
        public string Text { get; set; }

        public DateTime Created_at { get; set; }

        public virtual User ContentCreator { get; set; }
    }
}
