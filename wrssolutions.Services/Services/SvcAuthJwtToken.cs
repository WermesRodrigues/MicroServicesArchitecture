using wrssolutions.Configs;
using wrssolutions.Domain.Entities.Auth;
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using wrssolutions.DTO.Dto;
using wrssolutions.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace wrssolutions.Services.Services
{
    public class SvcAuthJwtToken : ISvcAuthJwtToken
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISvcPerson _svcPerson;
        private readonly ISvcLoggerMongo _svcLoggerMongo;

        public SvcAuthJwtToken(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ISvcPerson svcPerson,
                ISvcLoggerMongo svcLoggerMongo)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _svcPerson = svcPerson;
            _svcLoggerMongo = svcLoggerMongo;
        }

        public async Task<string> Login(dtoLoginInput model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberLogin, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return JwtToken(model.Email);
                }

                return "";
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return "";
            }
        }

        public async Task<string> Register(dtoRegisterInput model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    IdentityResult result;

                    user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        IsAdmin = false
                    };

                    result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        _svcPerson.Insert(new Domain.Entities.Person.Person()
                        {
                            Email = model.Email,
                            Fname = model.Fname,
                            Lname = model.Lname,
                            CompanyID = model.CompanyID,
                        });

                        return JwtToken(model.Email);
                    }
                    else
                    {
                        string errorLog = string.Empty;

                        if (result.Errors != null && result.Errors.Count() > 0)
                        {
                            foreach (string error in result.Errors.Select(s => s.Description))
                            {
                                errorLog += error + " ";
                            }
                        }

                        return errorLog;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return "";
            }
        }

        public string JwtToken(string _user)
        {
            try
            {
                var user = _userManager.FindByNameAsync(_user).GetAwaiter().GetResult();

                if (user == null)
                    return "";

                var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();
                var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));

                if (!string.IsNullOrEmpty(user.Email))
                    claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim("role", userRole));
                }

                var identityClaims = new ClaimsIdentity();
                identityClaims.AddClaims(claims);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.JwtSecret));
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = Settings.JwtEmissor,
                    Audience = Settings.JwtValidate,
                    //Expires = DateTime.UtcNow.AddHours(Configs.JwtExpiration),
                    Subject = identityClaims,
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                });

                var encodedToken = tokenHandler.WriteToken(token);

                return encodedToken;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                throw new Exception(this.GetType().Name + "|" + ex?.Message.ToString());
            }
        }
    }
}
