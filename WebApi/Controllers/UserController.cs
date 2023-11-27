using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController {
    private readonly DatabaseContext _databaseContext;

    public UserController(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    public async Task<List<User>> GetUsers() {
        User user = new User {
            Username = new Random().Next().ToString(),
            FirstName = "test",
            LastName = "test"
        };
        await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();
        return await _databaseContext.Users.ToListAsync();
    }
}