using AutoMapper;
using YGHMS.API.DTO.Common;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.MediaServices
{
    public interface IApartmentPublicationService
    {
        public ResponseDTO<IEnumerable<int>> AddListMediaOfApartment(int apartmentID, IEnumerable<int> listDetailMediaID);
    }

    public class ApartmentMediaService : IApartmentPublicationService
    {
        private readonly RentalManagementContext _context;
        private readonly IMapper _mapper;

        public ApartmentMediaService(RentalManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponseDTO<IEnumerable<int>> AddListMediaOfApartment(int apartmentID, IEnumerable<int> listDetailMediaID)
        {
            if (apartmentID <= 0 || listDetailMediaID == null)
            {
                return new ResponseDTO<IEnumerable<int>>() { Code = (int)RESPONSE_CODE.BadRequest, Data = new List<int>(), };
            }
            var newListApartmentMedia = listDetailMediaID.Select(detailMediaID =>
            {
                var apartmentMedia = new ApartmentPublication() { ApartmentId = apartmentID, MediaId = detailMediaID, };
                _context.ApartmentPublications.Add(apartmentMedia);
                return apartmentMedia;
            });
            _context.SaveChanges();
            return new ResponseDTO<IEnumerable<int>>()
            {
                Code = (int)RESPONSE_CODE.Created,
                Data = newListApartmentMedia.Select(aparmentMedia => aparmentMedia.Id),
            };
        }
    }
}
