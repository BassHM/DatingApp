using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Entities;

namespace Api.Controllers;

[Route("api/[controller]")] // https://localhost:5001/api/members
[ApiController]
public class MembersController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<AppUser>> GetMembers()
    {
        var members = context.Users.ToList();
        return members;
    }

    [HttpGet("{id}")] // https://localhost:5001/api/members/bob-id
    public ActionResult<AppUser> GetMember(string id)
    {
        var member = context.Users.Find(id);
        if (member == null) return NotFound();
        return member;
    }
}