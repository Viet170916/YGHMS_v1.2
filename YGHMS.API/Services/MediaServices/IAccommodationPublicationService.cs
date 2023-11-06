using AutoMapper;
using YGHMS.API.DTO.Common;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.MediaServices;

public interface IAccommodationPublicationService
{
    public ResponseDTO<IEnumerable<int>> AddListMediaOfAccommodation(
      int accommodationID,
      IEnumerable<int> listDetailMediaID
    );
}

public class AccommodationMediaService : IAccommodationPublicationService
{
    private readonly RentalManagementContext _context;
    private readonly IMapper _mapper;

    public AccommodationMediaService(RentalManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ResponseDTO<IEnumerable<int>> AddListMediaOfAccommodation(
      int accommodationID,
      IEnumerable<int> listDetailMediaID
    )
    {
        if (accommodationID <= 0 || listDetailMediaID == null)
            return new ResponseDTO<IEnumerable<int>>() { Code = (int)RESPONSE_CODE.BadRequest, Data = new List<int>(), };
        var newListAccommodationMedia = listDetailMediaID.Select(detailMediaID =>
        {
            var accomMedia = new AccommodationPublication() { AccommodationId = accommodationID, MediaId = detailMediaID, };
            _context.AccommodationPublications.Add(accomMedia);
            return accomMedia;
        });
        _context.SaveChanges();
        return new ResponseDTO<IEnumerable<int>>()
        {
            Code = (int)RESPONSE_CODE.Created,
            Data = newListAccommodationMedia.Select(accomMedia => accomMedia.Id),
        };
    }
}