using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;

namespace MovieRental.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();
        }
    }
}
