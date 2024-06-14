using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Claims;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;
using Talabat.Service;
using Talabat.Core;
using Address = Talabat.Core.Entities.Identity.Address;
using Talabat.Apis.Extensions;
using System.Runtime.Serialization;

namespace Talabat.Apis.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthService authService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }


        #region Login [HttpPost]

        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser_Dto>> Login(Login_Dto loginDto)
        {
            // if model state is not valid , he don't go inside End point .
            // Factory . 

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            // this make check at passwrod , dont't make check 
            // if user is false , [This Account will be block Or not ]
            var CheckUserPassword = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (CheckUserPassword.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            var token = await _authService.CreateTokenAsync(user);

            var AppUserThatLoggin = new ApplicationUser_Dto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,

                Token = token
            };

            return Ok(AppUserThatLoggin);
        }

        #endregion


        #region Register [HttpPost]

        [HttpPost("Register")] // Post : base/api/account/register 
        public async Task<ActionResult<ApplicationUser_Dto>> Register(Register_Dto register_Dto)
        {
            if (CheckEmailExist(register_Dto.Email).Result.Value == true)
            {
                var errorResponse = new ApiValidationErrorResponse()
                {
                    Errors = new string[] {$"{register_Dto.Email} already exist , try to use another ."}
                };

                return BadRequest(errorResponse);
            }

            var user = new ApplicationUser()
            {
                DisplayName = register_Dto.DisplayName,
                Email = register_Dto.Email, // E.Khalifa.Dev@Gmail.com
                UserName = register_Dto.Email.Split("@")[0],// E.Khalifa.Dev  
                PhoneNumber = register_Dto.PhoneNumber
            };

            var resultOfCreating = await _userManager.CreateAsync(user, register_Dto.Password);

            if (resultOfCreating.Succeeded is false)
                return BadRequest(new ApiResponse(400));

            var token = await _authService.CreateTokenAsync(user);

            var appUserToReturn = new ApplicationUser_Dto()
            {
                DisplayName = user.DisplayName,

                Email = user.Email,

                Token = token
            };

            return Ok(appUserToReturn);

        }

        #endregion


        #region Get Current User [HttpGet]

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser_Dto>> GetCurrentUser()
        {
            var appUser = await GetTheCurrentAuthUser();

            var userToReturn = _mapper.Map<ApplicationUser, ApplicationUser_Dto>(appUser);

            userToReturn.Token = await _authService.CreateTokenAsync(appUser);

            return Ok(userToReturn);
        }

        #endregion


        #region Get User Address [HttpGet]

        [HttpGet("Address")] // Get : Baserul/api/account/address . 
        [Authorize]
        public async Task<ActionResult<Address_Dto>> GetAddressForUser()
        {
            var appUser = await GetTheCurrentAuthUser();

            var addressToReturn = _mapper.Map<Address, Address_Dto>(appUser.Address);
            // here i must make include fo
            return Ok(addressToReturn);
        }

        #endregion


        #region Update Address [HttpPut]

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<Address_Dto>> UpdateUserAddress(Address_Dto addressDto)
        {
            var address = _mapper.Map<Address_Dto, Address>(addressDto);

            var appUser = await GetTheCurrentAuthUser();

            address.Id = appUser.Address.Id;

            appUser.Address = address;

            var result = await _userManager.UpdateAsync(appUser);

            return (result.Succeeded) ? Ok(addressDto) : BadRequest(new ApiResponse(400));
        }

        #endregion


        #region Make Check At Email , If Exist => return True . 

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var Email = await _userManager.FindByEmailAsync(email);

            return (Email is not null) ? true : false;
        }

        #endregion

        private async Task<ApplicationUser> GetTheCurrentAuthUser()
        {
            var appUser = await _userManager.FindUserIncludingAddressAsync(User);

            return appUser;
        }
        // Address in navigation property , by default it will not Loaded .  

    }
}
