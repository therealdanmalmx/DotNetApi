using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dataContextDapper;
    public UserController(IConfiguration configuration)
    {
        _dataContextDapper = new DataContextDapper(configuration);
    }

    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
            [Active] FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dataContextDapper.LoadData<User>(sql);

        return users;

    }

    [HttpGet("GetSingleUser/{userId}")]
    // public IEnumerable<User> GetUsers()
    public User GetSingleUser(string userId)
    {
              string sql = @"
            SELECT [UserId],
                    [FirstName],
                    [LastName],
                    [Email],
                    [Gender],
                    [Active]
                FROM TutorialAppSchema.Users
                    WHERE UserId = " + userId.ToString();

        User user = _dataContextDapper.LoadDataSingle<User>(sql);

        return user;

    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {

        string sql = @"
        INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES (" +
            "'" + user.FirstName +
            "', '" + user.LastName +
            "', '" + user.Email +
            "', '" + user.Gender +
            "', '" + user.Active +
        "')";

        Console.WriteLine(sql);

            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to add user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName +
                "', [LastName] = '" + user.LastName +
                "', [Email] = '" + user.Email +
                "', [Gender] = '" + user.Gender +
                "', [Active] = '" + user.Active +
            "' WHERE UserId = " + user.UserId;

            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to update user");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to delete user");

    }

}
