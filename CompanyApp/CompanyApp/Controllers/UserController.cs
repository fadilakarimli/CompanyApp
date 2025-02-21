using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Repository.Repositories.Interfaces;
using Repository.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompanyApp.Controllers
{
    public class UserController
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        public async Task<string> Login(string email, string password)
        {
            try
            {
                bool isLoggedIn = await _userService.LoginAsync(email, password);
                if (isLoggedIn)
                {
                    return "Login successful!";
                }
                else
                {
                    return "Login failed. Check email or password.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task Register()
        {
            string fullName;
            while (true)
            {
                Console.Write("Enter Full Name for Registration: ");
                fullName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    Console.WriteLine("Full Name is required. Please enter again.");
                    continue;
                }
                if (fullName.Any(char.IsDigit))
                {
                    Console.WriteLine("Full Name cannot contain numbers. Please enter again.");
                    continue;
                }
                break;
            }

            string email;
            while (true)
            {
                Console.Write("Enter Email for Registration: ");
                email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email is required. Please enter again.");
                    continue;
                }
                if (!email.Contains("@"))
                {
                    Console.WriteLine("Email must contain '@'. Please enter again.");
                    continue;
                }

                bool emailExists = await _userService.CheckEmailExistsAsync(email);
                if (emailExists)
                {
                    Console.WriteLine("This email is already registered. Please enter a different email.");
                    continue;
                }

                break;
            }

            string password;
            while (true)
            {
                Console.Write("Enter Password: ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password is required. Please enter again.");
                    continue;
                }
                break;
            }

            string confirmPassword;
            while (true)
            {
                Console.Write("Confirm Password: ");
                confirmPassword = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(confirmPassword))
                {
                    Console.WriteLine("Confirm Password is required. Please enter again.");
                    continue;
                }
                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match. Please enter again.");
                    continue;
                }
                break;
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password
            };

            try
            {
                await _userService.RegisterAsync(user, confirmPassword);
                Console.WriteLine("Registration successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Register();
            }
        }





    }
}

