using AutoMapper;
using WebAPI.Model;

namespace WebAPI;

public class MappingConfig : Profile {
    public MappingConfig() {
        CreateMap<Address, AddressDTO>().ReverseMap();
    }
}