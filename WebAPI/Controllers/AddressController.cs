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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AddressDTO> CreateAddress([FromBody] AddressDTO? addressDto) {
        if (addressDto is null) {
            return BadRequest(addressDto);
        }

        // SS: when creating an address object, the id should be 0
        if (addressDto.Id > 0) {
            return StatusCode(StatusCodes.Status500InternalServerError, addressDto);
        }

        var address = _mapper.Map<Address>(addressDto);
        _context.Addresses.Add(address);
        _context.SaveChanges();

        // SS: note that the address object is updated with the database's primary key,
        // NOT the dto object!
        return Ok(address);
    }
}