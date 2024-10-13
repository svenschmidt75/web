using AutoMapper;
using Web.Base.Model;

namespace WebAPI;

public class MappingConfig : Profile {
    public MappingConfig() {
        CreateMap<Address, AddressDTO>().ReverseMap();
    }
}