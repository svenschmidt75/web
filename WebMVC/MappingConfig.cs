using AutoMapper;
using Web.Base.Model;

namespace WebMVC;

public class MappingConfig : Profile {
    public MappingConfig() {
        CreateMap<Address, AddressDTO>().ReverseMap();
    }
}