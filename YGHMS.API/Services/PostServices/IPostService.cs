using AutoMapper;
using YGHMS.API.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Services.PostServices;

public interface IPostService
{
  public IList<PostDisplayAsListDto> GetPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId);
  public IList<PostDisplayAsOptionList> GetPersonalPostDisplayAsOptionList(int userId, int? page);
  public IList<PostDisplayAsListDto> GetPostDisplayAsHighlightList(int page, int? userId);
  public PostUpdatePage IsPostExist(int id);
  public IList<PostDisplayAsListDto> GetFilteredPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId);
  public IList<PostDisplayAsListDto> GetPersonalPostDisplayAsListStatus(int userId, int status, int page);
  public DetailPostResponse GetPostResponses(string user, int postId);
  public AccommodationRequest? PostUpdateRequest(int userId, int postId);
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

  public PostUpdatePage IsPostExist(int id)
  {
    var post = _context.Accommodations
                       .Where(post => post.Id == id)
                       .Select(acc => new
                       {
                         AccommodationPublicationsCount = acc.AccommodationPublications.Count,
                         EstateTypesId = acc.EstateTypesId,
                         Latitude = acc.Latitude,
                         Longitude = acc.Longitude,
                         AddressId = acc.Address,
                         Title = acc.Title,
                         Description = acc.Description,
                         Policies = acc.Policies,
                         ApartmentsCount = acc.Apartments.Count,
                         Expiration = acc.Expiration,
                       })
                       .FirstOrDefault();
    if (post!.EstateTypesId is null) { return PostUpdatePage.ESTATE; }

    if ((post.Latitude is null || post.Longitude is null) && post.AddressId is null) { return PostUpdatePage.LOCATION; }

    if (post.AccommodationPublicationsCount < 1) { return PostUpdatePage.POST_IMAGE; }

    if (post.Title is null || post.Description is null) { return PostUpdatePage.TITLE_AND_DESC; }

    if (post.Policies is null) { return PostUpdatePage.POLICIES; }

    if (post.ApartmentsCount < 1) { return PostUpdatePage.APARTMENTS; }

    if (post!.Expiration <= DateTime.Today || post.Expiration == null) { return PostUpdatePage.EXPIRATION; }

    if (post.Expiration is not null) { return PostUpdatePage.EXPIRATION; }

    throw new(Constants.ErrorMessages.NotFound);
  }

  public AccommodationRequest? PostUpdateRequest(int userId, int postId)
  {
    return (_context.Accommodations
                    // .Include(accommodation => accommodation.AccommodationPublications)
                    .Where(ac => userId == ac.OwnerId && postId == ac.Id)
                    .Select(acc => new AccommodationRequest
                    {
                      Id = acc.Id,
                      Latitude = acc.Latitude,
                      Longitude = acc.Longitude,
                      Title = acc.Title,
                      Description = acc.Description,
                      Expiration = acc.Expiration,
                      Policies = acc.Policies,
                      Quality = acc.Quality,
                      EstateTypesId = acc.EstateTypesId,
                      AccommodationPublications = acc.AccommodationPublications.Select(accP =>
                                                       new AccommodationPublicationRequest
                                                       {
                                                         Id = accP.Id,
                                                         AccommodationId = accP.AccommodationId,
                                                         MediaId = accP.MediaId,
                                                         Url = Uri.BuildUrlWithHost(accP.Media!.Url),
                                                       })
                                                     .ToList(),
                    })
                    .FirstOrDefault());
  }

  public IList<PostDisplayAsOptionList> GetPersonalPostDisplayAsOptionList(int userId, int? page)
  {
    return _context.Accommodations
                   .StillActivated()
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
                     ApartmentCount = post.Apartments.Count,
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
                   .ToList() ;
  }

  public IList<PostDisplayAsListDto> GetPersonalPostDisplayAsListStatus(int userId, int status, int page)
  {
    var post = _context.Accommodations.Private(userId);
    switch ((PostStatus)status)
    {
      case PostStatus.DELETED:
        post = post.Deleted();
        break;
      case PostStatus.EXPIRED:
        post = post.Expired();
        break;
      case PostStatus.WAITING:
        post = post.AreWaiting();
        break;
      case PostStatus.ACTIVATED:
        post = post.StillActivated();
        break;
      case PostStatus.APPROVED:
        post = post.Approved();
        break;
      case PostStatus.REJECTED:
        post = post.Rejected();
        break;
      case PostStatus.DRAFT:
        post = post.Draft();
        break;
    }

    return post.OrderByDescending(post => post.CreateAt)
               .Paging(page, 20)
               .SelectPostDisplayAsListQueryable(userId)
               .ToList();
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

  public IList<PostDisplayAsListDto> GetFilteredPostDisplayAsList(int page, HomeFilterResponse? filter, int? userId)
  {
    var activePost = _context.Accommodations.IsNotDeletedPost();
    if (filter?.Price != null) activePost = activePost.IsPriceFilterNotNull(filter.Price);
    if (filter?.AvailableDate != null) activePost = activePost.IsDateRangeNotNull(filter.AvailableDate);
    if (filter?.Amenities != null) activePost = activePost.IsAmenitiesNotNull(filter.Amenities);
    if (filter?.Location != null)
    {
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
    return activePost
           .StillActivated()
           .SelectPostDisplayAsListQueryable(userId)
           .ToList();
  }

  public IList<PostDisplayAsListDto> GetPostDisplayAsHighlightList(int page, int? userId)
  {
    throw new NotImplementedException();
  }

  public DetailPostResponse GetPostResponses(string userName, int postId)
  {
    var result = _context.Accommodations
                         .StillActivated()
                         .SelectDetailPostResponse()
                         .FirstOrDefault(post => post.Id == postId && post.Owner!.UserName == userName);
    return result!;
  }
  
  }