using AuthenticationApi.Dtos;
using FluentResults;

namespace AuthenticationApi.Services;

public interface IPostingService
{
    Task<Result<string>> AddArticle(AddArticleRequestDto request);
    Task<Result<string>> DeleteArticle(int id);
    Task<Result<string>> FindArticleByTitle(string title);
    Task<Result<string>> FindArticleByText(string text);
    Task<Result<string>> GetAllPosts();
}

