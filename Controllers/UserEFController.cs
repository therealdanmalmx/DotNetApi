using AutoMapper;
using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _dataContextEF;
    IUserRepository _userRepository;

    IMapper _mapper;

    public UserEFController(IConfiguration configuration, IUserRepository userRepository)
    {
        _dataContextEF = new DataContextEF(configuration);
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserJobInfo, UserJobInfo>();
            cfg.CreateMap<UserSalary, UserSalary>();
        }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _dataContextEF.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

            if (user != null) {
                return user;
            }
        throw new Exception("Failed to get user");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb  = _mapper.Map<User>(user);

        _userRepository.AddEntity<User>(userDb);
        if (_userRepository.SaveChanges()) {

                return Ok();
        }

        throw new Exception("Failed to add user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _dataContextEF.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();

            if (userDb != null) {

                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender = user.Gender;

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
        throw new Exception("Failed to get user");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _dataContextEF.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

            if (userDb != null) {

                _userRepository.RemoveEntity<User>(userDb);

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
        throw new Exception("Failed to delete user");

    }

    // USER JOB INFO

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {

        IEnumerable<UserJobInfo> userJobInfo = _dataContextEF.UserJobInfo.ToList<UserJobInfo>();
        return userJobInfo;

    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetSingleUserJobInfo(int userId)
    {
        return _dataContextEF.UserJobInfo
            .Where(u => u.UserId == userId)
            .ToList();
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEF(UserJobInfo userJobInfo)
    {

        _userRepository.AddEntity<UserJobInfo>(userJobInfo);
        if (_userRepository.SaveChanges()) {

                return Ok();
        }

        throw new Exception("ADDING USR JOB FAILED");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobDb = _dataContextEF.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId)
            .FirstOrDefault();

            if (userJobDb != null) {

                _mapper.Map(userJobInfo, userJobDb);

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
        throw new Exception("Failed to update user salary");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfoDb = _dataContextEF.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault();

            if (userJobInfoDb != null) {

                _userRepository.RemoveEntity<UserJobInfo>(userJobInfoDb);

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
            throw new Exception("DELETING JOB INFO FAILED RO SAVE");

        throw new Exception("FAILED TO FIND JOB INFO TO DELETE");
    }
    // USER SALARY

    [HttpGet("GetUsersSalary/{userId}")]
    public IEnumerable<UserSalary> GetUsersSalary(int userId)
    {
        return _dataContextEF.UserSalary
            .Where(u => u.UserId == userId)
            .ToList();
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalaryInfo)
    {

        _userRepository.AddEntity<UserSalary>(userSalaryInfo);
        if (_userRepository.SaveChanges()) {

            return Ok();
        }

        throw new Exception("ADDING USER SALARY FAILED");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalaryInfo)
    {
        UserSalary? userSalaryDb = _dataContextEF.UserSalary
            .Where(u => u.UserId == userSalaryInfo.UserId)
            .FirstOrDefault();

            if (userSalaryDb != null) {

                _mapper.Map(userSalaryInfo, userSalaryDb);

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
        throw new Exception("Failed to update user salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userSalaryDb = _dataContextEF.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault();

            if (userSalaryDb != null) {

                _userRepository.RemoveEntity<UserSalary>(userSalaryDb);

                if (_userRepository.SaveChanges()) {
                    return Ok();
                }
            }
            throw new Exception("DELETING JOB INFO FAILED RO SAVE");

        throw new Exception("FAILED TO FIND JOB INFO TO DELETE");
    }
}
