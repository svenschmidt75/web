using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly TeacherDbContext _context;

    public AddressController(TeacherDbContext context) {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Address> GetAddresses() {
        return _context.Addresses.ToList();
    }

}