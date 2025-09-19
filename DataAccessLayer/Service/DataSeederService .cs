using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Microsoft.AspNetCore.Identity;
using Repositey;

namespace Application.Service
{
    public class DataSeederService : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeederService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                   
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(admin, "Admin123@"); 
            }
        }
    }
}
