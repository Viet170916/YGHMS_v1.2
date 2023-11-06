using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.TypeModels;

namespace YGHMS.API.DTO.ResponseModels.PostDTOs;

public class PostDisplayAsListDto
{
  public string? Image { get; set; }
  public string? Title { get; set; }
  public PostLocation? Location { get; set; }
  public PriceRangeResponse? Price { get; set; }
  public int Id { get; set; }
  public decimal? Quality { get; set; }
  public string? UserName { get; set; }
  public ReviewInPostList? Reviews { get; set; }
  public FollowerInPostList? Follower { get; set; }
  public bool IsFollowed { get; set; }
}

public class ReviewInPostList
{
  public decimal? Rate { get; set; }
  public int? Count { get; set; }
  public IList<Taken>? FiveTaken { get; set; }
}

public class Taken
{
  public string? Avatar { get; set; }
  public string? Name { get; set; }
}

public class FollowerInPostList
{
  public int? Count { get; set; }
  public IList<Taken>? FiveTaken { get; set; }
}