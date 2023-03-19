using AuthenticationApi.Db;
using AuthenticationApi.Dtos;
using AuthenticationApi.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Linq;
using ContentCreators.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthenticationApi.Services;

public class PostingService : IPostingService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public PostingService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _contextAccessor = httpContextAccessor;
    }
    public async Task<Result<string>> AddArticle(AddArticleRequestDto request)
    {
        var user = _contextAccessor.HttpContext.User;
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        _context.Article?.Add(new Article
        {
            Title = request.Title,
            Text = request.Text,
            Created_at = DateTime.Now,
            UserId = userId
        });
        _context.SaveChanges();
        return Result.Ok();
    }

    public async Task<Result<string>> DeleteArticle(int id)
    {
        Article article = _context.Article?
            .Where(o => o.Id == id)
            .FirstOrDefault();
        if (article == null)
        {
            return Result.Fail($"Статья с \"Id: {id}\" не найдена");
        }
        _context.Article?.Remove(article);
        _context.SaveChanges();
        return Result.Ok($"Статья с \"Id: {id}\" успешно удалена");
    }

    public async Task<Result<string>> FindArticleByText(string text)
    {
        var articlesList = _context.Article?
            .Where(o => o.Text == text)
            .ToList();
        if (articlesList.IsNullOrEmpty())
        {
            return Result.Fail($"Совпадений не найдено");
        }
        string json = JsonSerializer.Serialize(articlesList);
        return Result.Ok(json);
    }

    public async Task<Result<string>> FindArticleByTitle(string title)
    {
        var articlesList = _context.Article?
            .Where(o => o.Title == title)
            .ToList();
        if (articlesList.IsNullOrEmpty())
        {
            return Result.Fail($"Совпадений не найдено");
        }
        string json = JsonSerializer.Serialize(articlesList);
        return Result.Ok(json);
    }

    public async Task<Result<string>> GetAllPosts()
    {
        var articlesList = _context.Article;
        if (articlesList.IsNullOrEmpty())
        {
            return Result.Fail($"Совпадений не найдено");
        }
        string json = JsonSerializer.Serialize(articlesList);
        return Result.Ok(json);
    }
}
