using AutoMapper;
using CarRental.DATA.DTOs.User;
using CarRental.Entities;
using CarRental.Repository;
using e_parliament.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services{
    public interface IUserService{
        Task<(UserDto? user, string? error)> Login(LoginForm loginForm);
        Task<(AppUser? user, string? error)> DeleteUser(Guid id);
        Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm);
        Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id);

        Task<(UserDto? user, string? error)> GetUserById(Guid id);


        Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter);
        
        Task<(UserDto? user, string? error)> GetMyProfile(Guid id);

    }

    public class UserService : IUserService{
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper, ITokenService tokenService) {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<(UserDto? user, string? error)> Login(LoginForm loginForm) {
            var user = await _repositoryWrapper.User.Get(u => u.Email == loginForm.Email, i => i.Include(x => x.Role));
            if (user == null) return (null, "User not found");
            if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.Password)) return (null, "Wrong password");
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user, user.Role);
            return (userDto, null);
        }

        public async Task<(AppUser? user, string? error)> DeleteUser(Guid id) {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            await _repositoryWrapper.User.SoftDelete(id);
            return (user, null);
        }

        public async Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm) {
            var role = await _repositoryWrapper.Role.Get(r => r.Id == registerForm.Role);
            if (role == null) return (null, "Role not found");
            var user = await _repositoryWrapper.User.Get(u => u.Email == registerForm.Email);
            if (user != null) return (null, "User already exists");
            var newUser = new AppUser
            {
                Email = registerForm.Email,
                FullName = registerForm.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
                AddressId =     registerForm.AddressId, 
                Document =registerForm.Document
            };
                
            // set role 
            newUser.RoleId = role.Id;

            await _repositoryWrapper.User.CreateUser(newUser);
            newUser.Role = role;
            var userDto = _mapper.Map<UserDto>(newUser);
            userDto.Token = _tokenService.CreateToken(newUser, role);
            return (userDto, null);
        }

        public async Task<(AppUser? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid id) {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            if ( updateUserForm.Password != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserForm.Password);
            }
           
            user = _mapper.Map(updateUserForm, user);
            await _repositoryWrapper.User.UpdateUser(user);
            return (user, null);
        }

        public async Task<(UserDto? user, string? error)> GetUserById(Guid id) {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null) return (null, "User not found");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }

        public async Task<(List<UserDto>? user, int? totalCount, string? error)> GetAll(UserFilter filter)
        {
            var (users , totalCount) = await _repositoryWrapper.User.GetAll<UserDto>(
                x => (
                    (filter.FullName == null || x.FullName.Contains(filter.FullName)) &&
                    (filter.Email == null || x.Email.Contains(filter.Email)) &&
                    (filter.RoleId == null || x.RoleId.Equals(filter.RoleId)) &&
                    (filter.IsActive == null || x.IsActive.Equals(filter.IsActive))
                    ) ,
                filter.PageNumber , filter.PageSize);
            return (users , totalCount , null);
        }
        
        public async Task<(UserDto? user, string? error)> GetMyProfile(Guid id) {
            var (users, _) = await _repositoryWrapper.User.GetAll<UserDto>(u => u.Id == id);
            if (users == null || !users.Any()) return (null, "User not found");
            var userDto = _mapper.Map<UserDto>(users.First());
            return (userDto, null);
        }
    }
}