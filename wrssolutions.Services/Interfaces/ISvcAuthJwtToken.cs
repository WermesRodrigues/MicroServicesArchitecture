using wrssolutions.DTO.Dto;

namespace wrssolutions.Services.Interfaces
{
    public interface ISvcAuthJwtToken
    {
        Task<string> Login(dtoLoginInput model);
        Task<string> Register(dtoRegisterInput model);
    }
}
