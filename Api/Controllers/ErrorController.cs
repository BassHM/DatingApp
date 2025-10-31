using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ErrorController : BaseApiController
{

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest() // 400
    {
        return BadRequest("Bad request");
    }

    [HttpGet("auth")]
    public IActionResult GetAuth() // 401
    {
        return Unauthorized();
    }

    [HttpGet("not-found")]
    public IActionResult GetNotFound() // 404
    {
        return NotFound();
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError() // 500
    {
        throw new Exception("Server error");
    }
}