using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        //private readonly string _token;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IToken _tokenProvider;
        public AccountController(IToken tokenProvider, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tokenProvider = tokenProvider;
            _signInManager = signInManager;
            _userManager = userManager;           
        }

      /*  protected IActionResult HandleResponse<T>(Response<T> response)
        {
            //string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            //string output = JsonConvert.SerializeObject(response);
            return StatusCode(response.Code, response);
        }
        */
        
       /* [HttpGet]
        public IActionResult GetAccount()
        {
            return Ok(_tokenProvider.CreateToken("ASD"));
        } */

       [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized("Invalid email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return Ok(CreateUserObject(user));
            }

            return Unauthorized("Invalid password");
        } 

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Invalid Email");
            }

            var user = new AppUser
            {
                UserName = registerDto.FName + "_" + registerDto.LName,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Pasword);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(CreateUserObject(user));
        }

        private UserDTO CreateUserObject(AppUser user) => new UserDTO { UserId = user.Id , Token = _tokenProvider.CreateToken(user) };
    }
}