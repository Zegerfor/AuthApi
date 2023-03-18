using AuthenticationApi.Db;
using AuthenticationApi.Dtos;
using AuthenticationApi.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Linq;
using ContentCreators.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApi.Services;

public class PostingService : IPostingService
{
    private readonly AppDbContext _context;

    public PostingService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<string>> AddArticle(AddArticleRequestDto request)
    {
        _context.Article?.Add(new Article
        {
            Title = request.Title,
            Text = request.Text,
            Created_at = DateTime.Now
        });
        //int count = _context.Article.Local.Count;
        _context.SaveChanges();
        return Result.Ok();
    }

    //public Task<Result<string>> DeletePost(LoginRequest request)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Result<string>> DeleteArticle(int id)
    {
        Article article = _context.Article?
            .Where(o => o.Id == id)
            .FirstOrDefault();
        if (article == null)
        {
            return Result.Fail($"Статья с \"Id: {id}\" отсутствует");
        }
        _context.Article?.Remove(article);
        _context.SaveChanges();
        return Result.Ok();
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
