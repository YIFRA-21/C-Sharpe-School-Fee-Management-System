namespace SchoolFeeManagemetSystem.API.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SchoolFeeManagemetSystem.API.Interface;
    using ScoolFeeManagementSystem.Data.Context;
    using ScoolFeeManagementSystem.Data.Entities;
    using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDBContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO> CreateAsync(CreateUserDTO dto)
        {
            // Check if username exists
            if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                FullName = dto.FullName,
                Role = dto.Role,
                IsActive = true
            };

            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (user == null)
                throw new Exception("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid username or password");

            if (!user.IsActive)
                throw new Exception("User is inactive");

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await _context.Users
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Username = x.Username,
                    FullName = x.FullName,
                    Role = x.Role
                }).ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
