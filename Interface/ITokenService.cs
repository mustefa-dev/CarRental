using CarRental.DATA.DTOs.User;
using CarRental.Entities;

namespace e_parliament.Interface;

public interface ITokenService
{
    string CreateToken(AppUser user, Role role);
}