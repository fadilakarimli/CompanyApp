using Service.Services;
using System.Text.RegularExpressions;
using Domain.Entities;


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
        public async Task Logout()
        {
            Console.WriteLine("You have successfully logged out.");
        }
        public async Task Register()
        {
            string fullName;
        FullName:
            Console.Write("Enter Full Name for Registration: ");
            fullName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                Console.WriteLine("Full Name is required. Please enter again.");
                goto FullName;
            }

            if (!Regex.IsMatch(fullName, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine("Full Name format is wrong. Only letters and spaces are allowed. Please enter again.");
                goto FullName;
            }

            string email;
        Email:
            Console.Write("Enter Email for Registration: ");
            email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email is required. Please enter again.");
                goto Email;
            }

            if (!email.Contains("@"))
            {
                Console.WriteLine("Email must contain '@'. Please enter again.");
                goto Email;
            }

            if (string.IsNullOrWhiteSpace(email) || email == "@" || !email.Contains("@"))
            {
                Console.WriteLine("Email format is incorrect. Please enter a valid email.");
                goto Email;
            }

            bool emailExists = await _userService.CheckEmailExistsAsync(email);
            if (emailExists)
            {
                Console.WriteLine("This email is already registered. Please enter a different email.");
                goto Email;
            }

            string password;
        Password:
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password is required. Please enter again.");
                goto Password;
            }
            if (password.Length < 6)
            {
                Console.WriteLine("Password must be at least 6 characters long. Please enter again.");
                goto Password;
            }
            string confirmPassword;
        ConfirmPassword:
            Console.Write("Confirm Password: ");
            confirmPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                Console.WriteLine("Confirm Password is required. Please enter again.");
                goto ConfirmPassword;
            }
            if (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match. Please enter again.");
                goto ConfirmPassword;
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
                goto FullName;
            }
        }

    }
}

