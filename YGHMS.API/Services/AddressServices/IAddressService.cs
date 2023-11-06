using AutoMapper;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.Infra;

namespace YGHMS.API.Services.AddressServices;

public interface IAddressService
{
  public ResponseDTO<List<AddressDto>> GetFirstFiveAddressesBySearching(string q);
  public AddressDto? GetExactLocation(string q);
}

public class AddressService : IAddressService
{
  private readonly RentalManagementContext _context;
  private readonly IMapper _mapper;

  public AddressService(RentalManagementContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public ResponseDTO<List<AddressDto>> GetFirstFiveAddressesBySearching(string q)
  {
    q = q.Trim();
    var cityOnly =
      _context.Addresses.Where(address => (address.City ?? "").Contains(q))
              .Select(address => new
              {
                address.City,
                index = (address.City ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 ?
                  (address.City ?? "").Length :
                  (address.City ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase),
              })
              .Distinct()
              .OrderBy(value => value.index)
              .Take(6)
              .Select(city => new AddressDto() { City = city.City, })
              .AsEnumerable();
    var districts =
      _context.Addresses.Where(address => (address.District ?? string.Empty).Contains(q)
                                          || (address.District + ", " + address.City).Contains(q)
                                          || (address.City + ", " + address.District).Contains(q))
              .Select(address => new
              {
                address.City,
                address.District,
                index =
                  (address.District + ", " + address.City).IndexOf(q,
                    StringComparison.OrdinalIgnoreCase) < 0 ?
                    (address.City + ", " + address.District).Length :
                    ((address.District ?? "") + ", " + (address.City ?? "")).IndexOf(q,
                      StringComparison.OrdinalIgnoreCase),
              })
              .Distinct()
              .OrderBy(value => value.index)
              .Take(6)
              .Select(city => new AddressDto() { City = city.City, District = city.District, })
              .AsEnumerable();
    var commune =
      _context.Addresses.Where(address => (address.Commune ?? "").Contains(q)
                                          || (address.City + ", " + address.District + ", " +
                                              address.Commune).Contains(q)
                                          || (address.Commune + ", " + address.District + ", " +
                                              address.City).Contains(q))
              .Select(address => new
              {
                address.City,
                address.District,
                address.Commune,
                address.Id,
                index = ((address.Commune ?? "") + ", " + (address.District ?? "") + ", " +
                         (address.City ?? ""))
                  .IndexOf(q, StringComparison.OrdinalIgnoreCase) < 0 ?
                    ((address.City ?? "") + ", " + (address.District ?? "") + ", " +
                     (address.Commune ?? "")).Length :
                    ((address.Commune ?? "") + ", " + (address.District ?? "") + ", " +
                     (address.City ?? ""))
                    .IndexOf(q, StringComparison.OrdinalIgnoreCase),
              })
              .Distinct()
              .OrderBy(value => value.index)
              .Select(address =>
                new AddressDto()
                {
                  City = address.City, District = address.District, Commune = address.Commune, Id = address.Id,
                })
              .Take(6)
              .AsEnumerable();
    var fullAttributesList = cityOnly.Union(districts)
                                     .Union(commune)
                                     .Select(address => new
                                     {
                                       address.City,
                                       address.District,
                                       address.Commune,
                                       Index = ((address.City ?? "") + (address.District ?? "") +
                                                (address.Commune ?? "")).IndexOf(q,
                                         StringComparison.OrdinalIgnoreCase) < 0 ?
                                         ((address.City ?? "") +
                                          (address.District ?? "") +
                                          (address.Commune ?? "")).Length :
                                         (address.Commune ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase),
                                     })
                                     .OrderBy(address => address.Index)
                                     .Select(value => new AddressDto()
                                     {
                                       City = value.City, District = value.District, Commune = value.Commune,
                                     })
                                     .Take(6)
                                     .ToList();
    return new ResponseDTO<List<AddressDto>>() { Code = (int)RESPONSE_CODE.OK, Data = fullAttributesList, };
  }

  public AddressDto? GetExactLocation(string q)
  {
    return  _context.Addresses
                                    .Select(address => new AddressDto()
                                    {
                                      Id = address.Id,
                                      Commune = address.Commune,
                                      City = address.City,
                                      District = address.District,
                                      Detail = address.Detail,
                                    })
                                    .OrderBy(ad => ad.Commune!.IndexOf(q, StringComparison.OrdinalIgnoreCase))
                                    .FirstOrDefault(ad => ad.Commune != null && ad.Commune.IndexOf(q, StringComparison.OrdinalIgnoreCase) != -1);

  }
}