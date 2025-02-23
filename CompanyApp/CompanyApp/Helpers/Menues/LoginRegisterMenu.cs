using Domain.Entities;
using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CompanyApp.Helpers.Menues
{
    public class LoginRegisterMenu
    {
        private readonly IUserService _userService;
        private readonly MainMenu _mainMenu;

        public LoginRegisterMenu()
        {
            _userService = new UserService();
            _mainMenu = new MainMenu();
        }

        public async Task Start()
        {
            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("Please enter your choice: ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 1 || choice == 2)
                    {
                        if (choice == 1)
                        {
                            string email;
                            string password;

                            while (true)
                            {
                                Console.WriteLine("Enter Email: ");
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
                                break;
                            }

                            while (true)
                            {
                                Console.WriteLine("Enter Password: ");
                                password = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(password))
                                {
                                    Console.WriteLine("Password is required. Please enter again.");
                                    continue;
                                }
                                break;
                            }

                            try
                            {
                                var user = await _userService.LoginAsync(email, password);
                                Console.WriteLine("Login Successful!");

                                await _mainMenu.Start();
                                break;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else if (choice == 2)
                        {
                            string fullName;
                        FullNameInput:
                            Console.WriteLine("Enter Full Name for Registration: ");
                            fullName = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(fullName))
                            {
                                Console.WriteLine("Full Name is required. Please enter again.");
                                goto FullNameInput;
                            }

                            if (Regex.IsMatch(fullName, @"[!@#$%^&*(),.?""{}|<>]"))
                            {
                                Console.WriteLine("Full Name cannot contain special characters. Please enter again.");
                                goto FullNameInput;
                            }

                            if (fullName.Any(char.IsDigit))
                            {
                                Console.WriteLine("Full Name cannot contain numbers. Please enter again.");
                                goto FullNameInput;
                            }

                            string email;
                        EmailInput:
                            Console.WriteLine("Enter Email for Registration: ");
                            email = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine("Email is required. Please enter again.");
                                goto EmailInput;
                            }
                            if (!email.Contains("@"))
                            {
                                Console.WriteLine("Email must contain '@'. Please enter again.");
                                goto EmailInput;
                            }
                            if (string.IsNullOrWhiteSpace(email) || email == "@" || !email.Contains("@"))
                            {
                                Console.WriteLine("Email format is incorrect. Please enter correct format for email.");
                                goto EmailInput;
                            }

                            string password;
                        PasswordInput:
                            Console.WriteLine("Enter Password: ");
                            password = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(password))
                            {
                                Console.WriteLine("Password is required. Please enter again.");
                                goto PasswordInput;
                            }

                            if (password.Length < 6)
                            {
                                Console.WriteLine("Password must be at least 6 characters. Please enter again.");
                                goto PasswordInput;
                            }

                            string confirmPassword;
                        ConfirmPasswordInput:
                            Console.WriteLine("Confirm Password: ");
                            confirmPassword = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(confirmPassword))
                            {
                                Console.WriteLine("Confirm Password is required. Please enter again.");
                                goto ConfirmPasswordInput;
                            }
                            if (password != confirmPassword)
                            {
                                Console.WriteLine("Passwords do not match. Please enter again.");
                                goto ConfirmPasswordInput;
                            }

                            try
                            {
                                await _userService.RegisterAsync(new User { Email = email, FullName = fullName, Password = password }, confirmPassword);
                                Console.WriteLine("Registration successful!");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto FullNameInput;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid choice (1 or 2).");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid number (1 or 2).");
                }
            }
        }


    }
}
