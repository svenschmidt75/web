using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly TeacherDbContext _context;

    public AddressController(TeacherDbContext context) {
        _context = context;
    }
    
}