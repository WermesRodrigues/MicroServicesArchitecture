using wrssolutions.DTO.Dto;
using wrssolutions.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace wrssolutions.API.Controllers.Auth.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly ISvcAuthJwtToken authJwtToken;
        public AuthController(
                ISvcAuthJwtToken _authJwtToken)
        {
            authJwtToken = _authJwtToken;
        }

        //
        // POST: /Auth/Login
        [AllowAnonymous]
        [HttpPost, Route("Login")]
        [SwaggerOperation(Summary = "Auth/Login")]
        public async Task<IActionResult> Login(dtoLoginInput model)
        {
            string userToken = await authJwtToken.Login(model);

            if (!string.IsNullOrEmpty(userToken))
            {
                return Ok(userToken);
            }

            return BadRequest();
        }


        // POST: /Auth/Register
        [AllowAnonymous]
        [HttpPost, Route("Register")]
        [SwaggerOperation(Summary = "Auth/Register")]
        public async Task<IActionResult> Register(dtoRegisterInput model)
        {
            string userToken = await authJwtToken.Register(model);

            if (!string.IsNullOrEmpty(userToken))
            {
                return Ok(userToken);
            }

            return BadRequest(userToken);
        }
    }
}
