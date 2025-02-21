using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string email, string password);
        Task RegisterAsync(User user , string confirmPassword);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
