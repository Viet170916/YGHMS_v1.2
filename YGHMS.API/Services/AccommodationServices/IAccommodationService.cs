using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YGHMS.API.Common;
using YGHMS.API.DTO.AccommodationDTOs;
using YGHMS.API.DTO.Common;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.AccommodationServices;

public interface IAccommodationService
{
  public int? AddAccommodation(int ownerId);
  public PostUpdatePage? UpdateDraftAccommodation(int userId, Accommodation newAccommodation, int page);
}

public class AccommodationService : IAccommodationService
{
  private readonly RentalManagementContext _context;
  private readonly IMapper _mapper;

  public AccommodationService(RentalManagementContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public int? AddAccommodation(int ownerId)
  {
    var newAccommodation = new Accommodation()
    {
      OwnerId = ownerId,
      CreateAt = DateTime.Now,
      ModifyAt = DateTime.Now,
      IsDeleted = false,
      Status = (int)PostStatus.DRAFT,
    };
    _context.Accommodations.Add(newAccommodation);
    _context.SaveChanges();
    return newAccommodation.Id!;
  }

  public ResponseDTO<CreateNewAccommodationDTO> getAccommodationByID(int id)
  {
    var accom = _context.Accommodations.FirstOrDefault(x => x.Id == id);
    if (accom == null)
    {
      return new ResponseDTO<CreateNewAccommodationDTO>() { Code = (int)RESPONSE_CODE.NotFound, Data = null };
    }
    else
    {
      return new ResponseDTO<CreateNewAccommodationDTO>()
      {
        Code = (int)RESPONSE_CODE.OK, Data = _mapper.Map<Accommodation, CreateNewAccommodationDTO>(accom)
      };
    }
  }

  public ResponseDTO<int> InitializeNewAccommodation()
  {
    var initAccommodation = new Accommodation();
    initAccommodation.OwnerId = 1;
    _context.Accommodations.Add(initAccommodation);
    _context.SaveChanges();
    return new ResponseDTO<int>() { Code = (int)RESPONSE_CODE.Created, Data = initAccommodation.Id, };
  }

  public PostUpdatePage? UpdateDraftAccommodation(int userId, Accommodation newAccommodation, int page)
  {
    var accommodationUpdated = _context.Accommodations
                                       .Include(ac => ac.Apartments)
                                       // .Draft()
                                       .Private(userId)
                                       .FirstOrDefault(acco => acco.Id == newAccommodation.Id);
    if (accommodationUpdated is null) throw new(Constants.ErrorMessages.NotFound);
    switch ((PostUpdatePage)page)
    {
      case PostUpdatePage.ESTATE:
        accommodationUpdated!.EstateTypesId = newAccommodation.EstateTypesId;
        break;
      case PostUpdatePage.POST_IMAGE:
        if (_context.Publications
                    .Any(pub1 =>
                      pub1.AccommodationPublications.Count >= 1 &&
                      newAccommodation
                        .AccommodationPublications
                        .Select(ac => ac.MediaId)
                        .Contains(pub1.Id) &&
                      pub1.AccommodationPublications
                          .FirstOrDefault(acp => acp.Accommodation.OwnerId == userId) == null))
        {
          throw new(Constants.ErrorMessages.FORBIDDAN);
        }

        // if (newAccommodation.AccommodationPublications.Count < 5) { throw new(Constants.ErrorMessages.UNDER_5_IMAGES); }
        accommodationUpdated!.AccommodationPublications = newAccommodation.AccommodationPublications;
        break;
      case PostUpdatePage.LOCATION:
        if (newAccommodation.Latitude is null && newAccommodation.Longitude is null && newAccommodation.Address is null)
          throw new(Constants.ErrorMessages.BadParam);
        if (newAccommodation.Latitude is not null && newAccommodation.Longitude is not null)
        {
          accommodationUpdated!.Latitude = newAccommodation.Latitude;
          accommodationUpdated!.Longitude = newAccommodation.Longitude;
        }

        if (newAccommodation.Address is not null) { accommodationUpdated!.Address = newAccommodation.Address; }

        break;
      case PostUpdatePage.POLICIES:
        accommodationUpdated!.Policies = newAccommodation.Policies;
        break;
      case PostUpdatePage.EXPIRATION:
        if (newAccommodation.Expiration <= DateTime.Today) throw new(Constants.ErrorMessages.INVALID_EXPIRATION_SETED);
        accommodationUpdated!.Expiration = newAccommodation.Expiration;
        accommodationUpdated.Status = (int)PostStatus.ACTIVATED;
        break;
      case PostUpdatePage.TITLE_AND_DESC:
        if (string.IsNullOrWhiteSpace(newAccommodation.Title) ||
            string.IsNullOrWhiteSpace(newAccommodation.Description))
          throw new(Constants.ErrorMessages.BadParam);
        accommodationUpdated!.Title = newAccommodation.Title;
        accommodationUpdated!.Description = newAccommodation.Description;
        break;
      case PostUpdatePage.APARTMENTS:
        if (!accommodationUpdated!.Apartments.Any()) throw new(Constants.ErrorMessages.NO_APARTMENT);
        break;
      default: throw new(Constants.ErrorMessages.UpdateFail);
    }

    _context.SaveChanges();
    return (PostUpdatePage)(page + 1);
  }
}