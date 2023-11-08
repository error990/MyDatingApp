using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// We dont need this anymore because of deriving from BaseApiController (take a look inside this class)
// [ApiController]
// [Route("api/[controller]")] // api/users
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    //[AllowAnonymous] // Overrides [Authorize] on Controller level!
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        IEnumerable<MemberDto> usersToReturn = await _userRepository.GetMembersAsync();
        return Ok(usersToReturn);
    }

    [HttpGet("{username}")] // api/users/mclean
    public async Task<ActionResult<MemberDto?>> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

    // [HttpGet("{id}")] // api/users/3
    // public async Task<ActionResult<AppUser?>> GetUserById(int id)
    // {
    //     return await _userRepository.GetUserByIdAsync(id);
    // }
}