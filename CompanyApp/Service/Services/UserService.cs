using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository(); 
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var existingUser = await _repository.GetUserByEmailAsync(email);
            return existingUser != null;
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _repository.GetUserByEmailAsync(email); 

            if (user == null)
            {
                throw new ArgumentException("Email or Password is incorrect."); 
            }

            if (user.Password != password)
            {
                throw new ArgumentException("Email or Password is incorrect.");
            }

            return true;
        }
        public async Task RegisterAsync(User user, string confirmPassword)
        {
            if (!user.Email.Contains("@"))
            {
                throw new ArgumentException("Email must contain '@'.");
            }

            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new ArgumentException("Full Name cannot be empty.");
            }
            if (!Regex.IsMatch(user.FullName, @"^[a-zA-Z\s]+$"))
            {
                throw new ArgumentException("Full Name can only contain letters and spaces. Special characters are not allowed.");
            }
            if (user.Password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }
            if (user.Password != confirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }
            var existingUser = await _repository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("A user with this email already exists.");
            }
            await _repository.CreateAsync(user);
        }

    }
}
