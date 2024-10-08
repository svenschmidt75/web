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
        if (!ModelState.IsValid) {
            // SS: return ModelState so the validation message is shown
            return BadRequest(ModelState);
        }

        if (addressDto is null) {
            return BadRequest(addressDto);
        }

        // SS: when creating an address object, the id should be 0
        if (addressDto.Id > 0) {
            return StatusCode(StatusCodes.Status500InternalServerError, addressDto);
        }

        var address = _mapper.Map<Address>(addressDto);
        await _repository.AddAddress(address);
        await _repository.SaveChanges();

        // SS: note that the address object is updated with the database's primary key,
        // NOT the dto object!
        return Ok(address);
    }

    [HttpGet("{id}", Name = "GetAddressById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AddressDTO?>> GetAddress(int id) {
        var address = await _repository.GetAddress(address => address.Id == id);
        if (address == null) {
            return NotFound();
        }

        var addressDto = _mapper.Map<AddressDTO>(address);
        return Ok(addressDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDTO? addressDto) {
        if (!ModelState.IsValid) {
            // SS: return ModelState so the validation message is shown
            return BadRequest(ModelState);
        }

        if (addressDto == null || id != addressDto.Id) {
            return BadRequest(addressDto);
        }

        var address = await _repository.GetAddress(address => address.Id == id);
        if (address == null) {
            return NotFound();
        }

        // SS: update entire Address object
        _mapper.Map(addressDto, address);
        await _repository.SaveChanges();

        return Ok(addressDto);
    }

}