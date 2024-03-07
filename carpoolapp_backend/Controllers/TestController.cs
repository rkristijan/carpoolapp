using carpoolapp_backend.Data;
using carpoolapp_backend.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace carpoolapp_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller {
    private readonly DataContext _context;
    
    public TestController(DataContext context) {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<TestEnt>> GetTest() {
        var testresponse = _context.TestEnts.ToList();

        return testresponse;
    }
}