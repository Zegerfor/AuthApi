using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticationApi.Db;
using Microsoft.AspNetCore.Authorization;
using AuthenticationApi.Dtos;
using Azure.Core;
using AuthenticationApi.Services;
using AuthenticationApi.Extensions;
using ContentCreators.Entities;

namespace AuthenticationApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPostingService _postingService;

    public ArticlesController(AppDbContext context, IPostingService postingService)
    {
        _context = context;
        _postingService = postingService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> GetArticles()
    {
        var result = await _postingService.GetAllPosts();
            
        return GetResult(result);
    }

    [HttpGet("FindByTitle")]
    public async Task<IActionResult> GetArticleByTitle(string title)
    {
        var result = await _postingService.FindArticleByTitle(title);

        return GetResult(result);
    }

    [HttpGet("FindByText")]
    public async Task<IActionResult> GetArticleByText(string text)
    {
        var result = await _postingService.FindArticleByText(text);

        return GetResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> PostArticle([FromBody] AddArticleRequestDto request)
    {
        var result = await _postingService.AddArticle(request);

        return GetResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var result = await _postingService.DeleteArticle(id);

        return GetResult(result);
    }

    private IActionResult GetResult(FluentResults.Result<string> result)
    {
        var ResultDto = result.ToResultDto();

        if (!ResultDto.IsSuccess)
        {
            return BadRequest(ResultDto);
        }

        return Ok(ResultDto);
    }
}

