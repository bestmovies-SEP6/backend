using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase {

    [Authorize]
    [HttpGet("authorized")]
    public ActionResult GetAuthorized() {
        return Ok("Authorized");
    }

    //Example for conditional return type
    [HttpGet, Route("try")]
    public ActionResult Try2() {
        if (User.Identity!.IsAuthenticated) {
            return Ok($"Authorized  try {User.Identity.Name}");;
        }
        else {
            return Ok("Not Authorized");
        }
    }
    
}