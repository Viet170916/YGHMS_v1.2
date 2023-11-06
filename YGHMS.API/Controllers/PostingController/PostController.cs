using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.Services.PostServices;

namespace YGHMS.API.Controllers.PostingController;

[Route("api/post")]
[ApiController]
public class PostController : BaseApiController
{
  private readonly ILogger<PostController> _logger;
  private readonly IPostService _postService;

    public PostController(
      ILogger<PostController> logger,
      IPostService landlordReservation
    )
    {
        _logger = logger;
        _postService = landlordReservation;
    }

  [HttpPost("id-is-exists")] public IActionResult IsPostExist(int postId)
  {
    try
    {
      return Ok(new
      {
        IsVerified = true,
        Page = _postService.IsPostExist(postId),
        Data = _postService.PostUpdateRequest(UserHeader.UserId, postId),
      });
    }
    catch (Exception e)
    {
      Console.Write(e);
      return NotFound();
    }
  }

  [HttpPost("public")] public IActionResult GetPublicPostListDetail(PostPublicFilter request)
  {
    try
    {
      IList<PostDisplayAsListDto>
        result = _postService.GetFilteredPostDisplayAsList(request.Page, request.Filter, UserHeader is not null ? UserHeader.UserId: null);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new { error = "error", }); }
  }

    [HttpPost("personal")]
    public IActionResult GetPrivatePostListDetail(int page)
    {
        try
        {
            IList<PostDisplayAsListDto> result = _postService.GetPostDisplayAsList(page, null, UserHeader.UserId);
            return Ok(result);
        }
        catch (Exception e) { return BadRequest(new { error = "error", }); }
    }

    [HttpGet("option")]
    public IActionResult GetPrivatePostListDetailAsOptionList()
    {
        try
        {
            IList<PostDisplayAsOptionList> result = _postService.GetPersonalPostDisplayAsOptionList(UserHeader.UserId, null);
            return Ok(result);
        }
        catch (Exception e) { return BadRequest(new { error = "error", }); }
    }

    [HttpGet("yours/{status:int}")]
    public IActionResult GetPrivatePostListDetailWithStatus(int status, int page = 1)
    {
        try
        {
            IList<PostDisplayAsListDto> result = _postService
              .GetPersonalPostDisplayAsListStatus(UserHeader.UserId, status, page);
            return Ok(result);
        }
        catch (Exception e) { return BadRequest(new { error = "error", }); }
    }

  [HttpGet("{user}")] public IActionResult GetDetailPost(string user, int postId)
  {
    try
    {
      var result = _postService
        .GetPostResponses(user, postId);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new { error = "error", }); }
  }
}