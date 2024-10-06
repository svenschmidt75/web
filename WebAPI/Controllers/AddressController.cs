using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Repository;

namespace WebAPI.Controllers;

[Route("api/Address")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly ITeacherRepository _repository;
    private readonly IMapper _mapper;

    public AddressController(ITeacherRepository repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddresses() {
        var addresses = await _repository.GetAddresses();
        return Ok(_mapper.Map<IEnumerable<AddressDTO>>(addresses));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AddressDTO>> CreateAddress([FromBody] AddressDTO? addressDto) {
        if (addressDto is null) {
            return BadRequest(addressDto);
        }

        // SS: when creating an address object, the id should be 0
        if (addressDto.Id > 0) {
            return StatusCode(StatusCodes.Status500InternalServerError, addressDto);
        }

        var address = _mapper.Map<Address>(addressDto);
        await _repository.AddAddress(address);
        _repository.SaveChanges();

        // SS: note that the address object is updated with the database's primary key,
        // NOT the dto object!
        return Ok(address);
    }
}