using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YGHMS.API.Common;
using YGHMS.API.DTO.AccommodationDTOs.ApartmentDTOs;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.ApartmentServices;

public interface IApartmentService
{
  public IList<ApartmentDetailResponse> GetApartmentDetailResponses(string user, int postId);
  public int AddApartment(ApartmentRequest apartment, int userId);
  public ApartmentRequest UpdateApartment(ApartmentRequest apartment, int userId);
  public ResponseDTO<IList<int>> AddListOfApartment(CreateNewApartmentDTO[] listOfNewApartmentDTO);

  public ApartmentDetailResponse? GetApartmentShortenResponsesForReservation(
    int userId,
    int reservationId,
    int status
  );

  public IList<ApartmentRequest>? GetDraftApartments(int postId, int userHeaderUserId);
}

public class ApartmentService : IApartmentService
{
  private readonly RentalManagementContext _context;
  private readonly IMapper _mapper;

  public ApartmentService(RentalManagementContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public IList<ApartmentDetailResponse> GetApartmentDetailResponses(string user, int postId)
  {
    return _context.Apartments
                   .Where(ap => !ap.IsDeleted)
                   .Where(ap => ap.Accommodation.Owner.UserName == user)
                   .Where(ap => ap.AccommodationId == postId)
                   .SelectApartmentDetailResponses()
                   .ToList();
  }

  public ApartmentDetailResponse? GetApartmentShortenResponsesForReservation(
    int userId,
    int reservationId,
    int status
  )
  {
    return _context.Apartments
                   .Where(ap => ap.Reservations
                                  .FirstOrDefault(re => re.UserId == userId &&
                                                        re.Id == reservationId &&
                                                        re.Status == status &&
                                                        !re.IsDeleted) != null)
                   .SelectApartmentDetailResponses_Shorten()
                   .FirstOrDefault();
  }

  public int AddApartment(ApartmentRequest apartment, int userId)
  {
    if (apartment.AccommodationId == null) throw new(Constants.ErrorMessages.POST_NOT_FOUND);
    if (apartment.ApartmentPublications?.Count < 1) throw new(Constants.ErrorMessages.LACK_OF_IMAGES);
    if (apartment.ApartmentBedTypes?.Count < 1) throw new(Constants.ErrorMessages.NO_ROOM);
    var newApartment = _mapper.Map<Apartment>(apartment);
    newApartment.IsDeleted = false;
    newApartment.MaxOccupant = 0;
    newApartment.CreateAt = DateTime.Now;
    newApartment.ModifyAt = DateTime.Now;
    newApartment.OwnerId = userId;
    var newApartmentEntity = _context.Apartments.Add(newApartment);
    _context.SaveChanges();
    return newApartmentEntity.Entity.Id;
  }

  public ApartmentRequest UpdateApartment(ApartmentRequest apartment, int userId)
  {
    if (apartment.Id == null) throw new(Constants.ErrorMessages.NotFound);
    if (apartment.AccommodationId == null) throw new(Constants.ErrorMessages.POST_NOT_FOUND);
    var existingApartment = _context.Apartments
                                    .Include(a => a.ApartmentPublications)
                                    .Include(a => a.ApartmentsAmenities)
                                    .Include(a => a.ApartmentBedTypes)
                                    .FirstOrDefault(ap => !ap.IsDeleted &&
                                                          ap.AccommodationId == apartment.AccommodationId &&
                                                          ap.OwnerId == userId &&
                                                          ap.Id == apartment.Id);
    var updatedInstance = _mapper.Map<Apartment>(apartment);
    if (existingApartment == null) throw new(Constants.ErrorMessages.NotFound);
    existingApartment.Description = apartment.Description;
    if (updatedInstance.ApartmentPublications.Count > 0)
    {
      foreach (var apartmentApartmentPublication in updatedInstance.ApartmentPublications)
      {
        existingApartment.ApartmentPublications.Add(apartmentApartmentPublication);
      }
    }

    var updatedApartmentBedType = updatedInstance.ApartmentBedTypes;
    if (updatedApartmentBedType.Any())
    {
      var existItem = existingApartment.ApartmentBedTypes
                                       .Where(a => !updatedApartmentBedType
                                                    .Select(u => u.Type)
                                                    .Contains(a.Type));
      var existItemDeleted = existingApartment.ApartmentBedTypes
                                              .Where(a => updatedApartmentBedType
                                                          .Select(u => u.Type)
                                                          .Contains(a.Type));
      if (existItem.Any())
      {
        foreach (var room in existItem) { room.IsDelete = 1; }
      }

      if (existItemDeleted.Any())
      {
        foreach (var room in existItemDeleted) {
          room.IsDelete = 0; 
          
        }
      }

      foreach (var updatedInstanceApartmentBedType in updatedApartmentBedType)
      {
        if (existingApartment.ApartmentBedTypes.Select(bt => bt.Type).Contains(updatedInstanceApartmentBedType.Type))
        {
          var roomType =
            existingApartment.ApartmentBedTypes
                             .FirstOrDefault(ap => ap.Type == updatedInstanceApartmentBedType.Type);
          roomType!.Type = updatedInstanceApartmentBedType.Type;
          roomType.Price = updatedInstanceApartmentBedType.Price;
          roomType.Quantity = updatedInstanceApartmentBedType.Quantity;
        }

        if (!existingApartment.ApartmentBedTypes.Select(bt => bt.Type).Contains(updatedInstanceApartmentBedType.Type))
        {
          existingApartment.ApartmentBedTypes.Add(updatedInstanceApartmentBedType);
        }
      }
    }

    var updatedApartmentAmenities = updatedInstance.ApartmentsAmenities;
    if (updatedApartmentAmenities.Any())
    {
      var existItem = existingApartment.ApartmentsAmenities
                                       .Where(a => !updatedApartmentAmenities
                                                    .Select(u => u.AmenityId)
                                                    .Contains(a.AmenityId));
      var existItemDeleted = existingApartment.ApartmentsAmenities
                                              .Where(a => updatedApartmentAmenities
                                                          .Select(u => u.AmenityId)
                                                          .Contains(a.AmenityId));
      if (existItem!.Any())
      {
        foreach (var apartmentsAmenity in existItem) { apartmentsAmenity.IsDeleted = true; }
      }

      if (existItemDeleted!.Any())
      {
        foreach (var apartmentsAmenity in existItemDeleted) { apartmentsAmenity.IsDeleted = false; }
      }

      foreach (var apartmentsAmenity in updatedApartmentAmenities)
      {
        if (!existingApartment.ApartmentsAmenities.Select(bt => bt.AmenityId).Contains(apartmentsAmenity.AmenityId))
        {
          existingApartment.ApartmentsAmenities.Add(apartmentsAmenity);
        }
      }
    }

    existingApartment.Price = apartment.Price;
    existingApartment.Area = apartment.Area;
    existingApartment.ModifyAt = DateTime.Now;
    existingApartment.Name = apartment.Name!;
    _context.SaveChanges();
    return _mapper.Map<ApartmentRequest>(existingApartment);
  }

  public ResponseDTO<IList<int>> AddListOfApartment(CreateNewApartmentDTO[] listOfNewApartmentDTO)
  {
    if (listOfNewApartmentDTO == null || listOfNewApartmentDTO.Length == 0)
      return new ResponseDTO<IList<int>>() { Code = (int)RESPONSE_CODE.BadRequest, Data = new List<int>() };
    var listOfNewApartment =
      listOfNewApartmentDTO.Select(a => _mapper.Map<CreateNewApartmentDTO, Apartment>(a)).ToList();
    _context.Apartments.AddRange(listOfNewApartment);
    _context.SaveChanges();
    return new ResponseDTO<IList<int>>()
    {
      Code = (int)RESPONSE_CODE.Created, Data = listOfNewApartment.Select(a => a.Id).ToList()
    };
  }

  public IList<ApartmentRequest>? GetDraftApartments(int postId, int userHeaderUserId)
  {
    var apartments = _context
                     .Apartments
                     .Include(ap=>ap.Accommodation)
                     .Where(ap =>
                               (ap.OwnerId == userHeaderUserId || ap.Accommodation.OwnerId == userHeaderUserId));
    if (!apartments.Any()) throw new(Constants.ErrorMessages.NoRight);
    var a = apartments.ToList();

    apartments = apartments
      .Where(ap =>
        ap.AccommodationId == postId &&
        !ap.IsDeleted &&
        (ap.Accommodation.Status == (int)PostStatus.DRAFT ||
         ap.Accommodation.Status != (int)PostStatus.DELETED));
    if (!apartments.Any()) throw new(Constants.ErrorMessages.NotFound);
    return apartments.Select(apartment => new ApartmentRequest
                     {
                       Id = apartment.Id,
                       AccommodationId = apartment.AccommodationId,
                       Area = apartment.Area,
                       Name = apartment.Name,
                       Description = apartment.Description,
                       ApartmentsAmenities =
                         apartment.ApartmentsAmenities
                                  .Where(a => !a.IsDeleted)
                                  .Select(apa =>
                                    new ApartmentsAmenityRequest { AmenityId = apa.AmenityId, Id = apa.Id })
                                  .ToList(),
                       Images = apartment.ApartmentPublications
                                         .Where(a => !a.IsDeleted)
                                         .Select(img =>
                                           new Image { Url = img.Media!.Url, Description = img.Media.Description, })
                                         .ToList(),
                       ApartmentBedTypes = apartment.ApartmentBedTypes
                                                    .Where(a => a.IsDelete == 0)
                                                    .Select(room => new ApartmentBedTypeRequest
                                                    {
                                                      Id = room.Id,
                                                      ApartmentId = room.ApartmentId,
                                                      Price = room.Price,
                                                      Quantity = room.Quantity,
                                                      Type = room.Type,
                                                    })
                                                    .ToList(),
                     })
                     .ToList();
  }
}