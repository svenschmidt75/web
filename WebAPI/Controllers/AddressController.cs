using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers;

[Route("api/Address")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly TeacherDbContext _context;

    public AddressController(TeacherDbContext context) {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<AddressDTO> GetAddresses() {
        return _context.Addresses.ToList();
    }

}