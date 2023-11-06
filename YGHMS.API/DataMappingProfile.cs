using AutoMapper;
using YGHMS.API.DTO.AccommodationDTOs;
using YGHMS.API.DTO.AccommodationDTOs.ApartmentDTOs;
using YGHMS.API.DTO.PublicationDTOs;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.ReservationDTOs;
using YGHMS.API.DTO.ReviewDTOs;
using YGHMS.API.Infra.Models;

namespace YGHMS.API;

public class DataMappingProfile : Profile
{
	public DataMappingProfile()
	{
		CreateMap<AccommodationPublication, AccommodationPublicationRequest>().ReverseMap();
		CreateMap<Accommodation, AccommodationRequest>().ReverseMap();
		CreateMap<Apartment, ApartmentRequest>().ReverseMap();
		CreateMap<ApartmentBedType, ApartmentBedTypeRequest>().ReverseMap();
		CreateMap<ApartmentsAmenity, ApartmentsAmenityRequest>().ReverseMap();
		CreateMap<ApartmentPublicationRequest, ApartmentPublication>().ReverseMap();
		CreateMap<Address, AddressDto>().ReverseMap();
		CreateMap<Accommodation, AccommodationResponse>().ReverseMap();
		CreateMap<Accommodation, CreateNewAccommodationDTO>().ReverseMap();
		CreateMap<Apartment, CreateNewApartmentDTO>().ReverseMap();
		CreateMap<Publication, PublicationDTO>().ReverseMap();
		CreateMap<ReviewResponseDTO, Review>().ReverseMap();
		CreateMap<ReviewRequestDTO, Review>().ReverseMap();
	}
}