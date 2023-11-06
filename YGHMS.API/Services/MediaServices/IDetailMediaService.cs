using AutoMapper;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.PublicationDTOs;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.MediaServices;

public interface IDetailMediaService
{
    public ResponseDTO<IEnumerable<int>> AddListOfDetailMedium(
      List<PublicationDTO> detailMediaDTOs
    );
}

public class DetailMediaService : IDetailMediaService
{
    private readonly RentalManagementContext _context;
    private readonly IMapper _mapper;

    public DetailMediaService(RentalManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ResponseDTO<IEnumerable<int>> AddListOfDetailMedium(List<PublicationDTO> detailMediaDTOs)
    {
        if (detailMediaDTOs == null)
            return new ResponseDTO<IEnumerable<int>>()
            {
                Code = (int)RESPONSE_CODE.BadRequest,
                Data = new List<int>(), //empty list
            };
        var newListDetailMedia = detailMediaDTOs.Select(dto =>
        {
            var detailMedia = _mapper.Map<PublicationDTO, Publication>(dto);
            
            return detailMedia;
        });
        _context.Publications.AddRange(newListDetailMedia);
        _context.SaveChanges();
        return new ResponseDTO<IEnumerable<int>>()
        {
            Code = (int)RESPONSE_CODE.Created,
            Data = newListDetailMedia.Select(detailMedia => detailMedia.Id),
        };
    }
}