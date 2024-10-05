using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers;

[Route("api/Address")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly TeacherDbContext _context;
    private readonly IMapper _mapper;

    public AddressController(TeacherDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<AddressDTO>> GetAddresses() {
        var addresses = _context.Addresses.ToList();
        return Ok(_mapper.Map<IEnumerable<AddressDTO>>(addresses));
    }

}