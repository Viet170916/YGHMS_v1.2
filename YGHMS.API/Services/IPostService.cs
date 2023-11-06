using AutoMapper;
using YGHMS.API.Common;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Enums;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Services;

public interface IPostService
{
  public IList<PostDisplayAsListDto> GetPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId);
  public IList<PostDisplayAsOptionList> GetPersonalPostDisplayAsOptionList(int userId, int? page);
  public IList<PostDisplayAsListDto> GetPostDisplayAsHighlightList(int page, int? userId);
  public IList<PostDisplayAsListDto> GetFilteredPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId);
  public IList<PostDisplayAsListDto> GetPersonalPostDisplayAsListStatus(int userId, int status, int page);
  public DetailPostResponse GetPostResponses(string user, int postId);
  public DateTime GetPostExpirationDate(int userId, int postId);
  public void ExtendPostExpirationDate(int userId, int postId, DateTime toDate);
}

public class PostService : IPostService
{
  private readonly RentalManagementContext _context;
  public readonly IMapper _mapper;

  public PostService(RentalManagementContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public IList<PostDisplayAsListDto> GetPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId)
  {
    var posts = _context.Accommodations
                        .Where(acom => !acom.IsDeleted);
    if (userId != null)
      posts = posts
        .Where(acom => acom.OwnerId == userId);
    return posts
           .Paging(page, 24)
           .SelectPostDisplayAsListQueryable(userId)
           .ToList();
  }

  public IList<PostDisplayAsOptionList> GetPersonalPostDisplayAsOptionList(int userId, int? page)
  {
    return _context.Accommodations
                   .Draft()
                   .Where(post => !post.IsDeleted && post.OwnerId == userId)
                   .Select(post => new
                   {
                     Post = post,
                     Title = post.Title,
                     PendingReservationCount = post.Apartments
                                                   .Sum(apartment => apartment.Reservations
                                                                              .Count(reservation =>
                                                                                reservation.Status ==
                                                                                (int)ReservationStatus.PENDING)),
                     ApartmentCount = post.Apartments.Count(),
                     ThumbnailUrl = Uri.BuildUrlWithHost(post.AccommodationPublications
                                                             .FirstOrDefault()!
                                                             .Media!.Url),
                   })
                   .OrderByDescending(x => x.PendingReservationCount)
                   .Select(x => new PostDisplayAsOptionList
                   {
                     Id = x.Post.Id,
                     Title = x.Title,
                     NumberOfOrder = x.PendingReservationCount,
                     NumberOfPost = x.ApartmentCount,
                     ThumbnailUrl = x.ThumbnailUrl,
                   })
                   .ToList();
  }

  public IList<PostDisplayAsListDto> GetFilteredPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId)
  {
    var activePost = _context.Accommodations.IsNotDeletedPost();
    if (filter?.Price != null) activePost = activePost.IsPriceFilterNotNull(filter.Price);
    if (filter?.AvailableDate != null) activePost = activePost.IsDateRangeNotNull(filter.AvailableDate);
    if (filter?.Amenities != null) activePost = activePost.IsAmenitiesNotNull(filter.Amenities);
    if (filter?.Location != null)
    {
      // activePost = activePost.Where(post =>
      //   _mapper.Map<AddressDto>(post.Address)
      //          .DisplayName
      //          .Contains(filter.Location.DisplayName));
      var location = GetLocation.FetchDataAsync(filter.Location.Latitude, filter.Location.Longitude);
      var addressName = location.Result.display_name;
      activePost = activePost.Where(post =>
        addressName.Contains(post!.Address!.Commune!) ||
        addressName.Contains(post!.Address!.District!) ||
        addressName.Contains(post!.Address!.City!));
    }

    if (filter?.NumberOfBed != null)
      activePost = activePost
        .Where(post => post.Apartments
                           .Any(a => a.TypeOfBed == filter.NumberOfBed));
    return activePost.SelectPostDisplayAsListQueryable(null).ToList();
  }

  public IList<PostDisplayAsListDto> GetPersonalPostDisplayAsListStatus(int userId, int status, int page)
  {
    var post = _context.Accommodations.Private(userId);
    switch (status)
    {
      case (int)PostStatus.DELETED:
        post = post.Deleted();
        break;
      case (int)PostStatus.EXPIRED:
        post = post.Expired();
        break;
      case (int)PostStatus.WAITING:
        post = post.AreWaiting();
        break;
      case (int)PostStatus.ACTIVATED:
        post = post.StillActivated();
        break;
      case (int)PostStatus.APPROVED:
        post = post.Approved();
        break;
      case (int)PostStatus.REJECTED:
        post = post.Rejected();
        break;
      case (int)PostStatus.DRAFT:
        post = post.Draft();
        break;
    }

    return post.OrderByDescending(post => post.CreateAt)
               .Paging(page, 20)
               .SelectPostDisplayAsListQueryable(userId)
               .ToList();
  }

  public IList<PostDisplayAsListDto> GetPostDisplayAsHighlightList(int page, int? userId)
  {
    throw new NotImplementedException();
  }

  public DetailPostResponse GetPostResponses(string user, int postId)
  {
    var result = _context.Accommodations
                         // .StillActivated()
                         // .Where()
                         .SelectDetailPostResponse()
                         .FirstOrDefault(post => post.Id == postId && post.Owner.UserName == user);
    ;
    return result;
  }

  public DateTime GetPostExpirationDate(int userId, int postId)
  {
    var result = _context.Accommodations.FirstOrDefault(p => p.OwnerId == userId && p.Id == postId);
    if (result is null) throw new(ResponseMessage.NOT_FOUND);
    return (DateTime)result.Expiration!;
  }

  public void ExtendPostExpirationDate(int userId, int postId, DateTime toDate)
  {
    var post = _context.Accommodations.FirstOrDefault(p => p.Id == postId && p.OwnerId == userId);
    if (post is null)
      throw new(ResponseMessage.NOT_FOUND);
    else
      if (post.Expiration >= toDate)
      {
        throw new(ResponseMessage.NEW_EXPIRATION_DATE_MUST_BE_AFTER_OLD_EXPIRATION_DATE);
      }
      else
        if (toDate <= DateTime.Now)
          throw new(ResponseMessage.NEW_EXPIRATION_DATE_MUST_BE_AFTER_TODAY_DATE);
        else
        {
          post.Expiration = toDate;
          _context.SaveChanges();
        }
  }
}