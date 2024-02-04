using ByteInsights.Data;
using ByteInsights.Enums;
using ByteInsights.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace ByteInsights.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext   _dbContext;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, 
                           RoleManager<IdentityRole> roleManager, 
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // Task: Create the DB from the Migration
            await _dbContext.Database.MigrateAsync();


            // Task 1: Seed a few roles into the System

            await SeedRolesAsync();


            // Task 2: Seed a few users into the System
            await SeedUsersAsync();
        }

        

        private  async Task SeedRolesAsync()
        {
            // if there are roles in the System, do nothing
            if(_dbContext.Roles.Any())
            {
                return;
            } 

            // otherwise we want to create a few Roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                // We need to use RoleManager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));

            }
        }


        private async Task SeedUsersAsync()
        {
            // if there are User in the System, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Step 1: Creates a  new instance of  BlogUser

            var adminUser = new BlogUser()
            {
                Email = "dutta_joy@hotmail.com",
                UserName = "dutta_joy@hotmail.com",
                FirstName = "Joy",
                LastName = "Dutta", 
                PhoneNumber = "+447828101808",
                EmailConfirmed = true,

            };

            // Step 2: Use the UserManager to create a new user that is defined by the adminUser variable
            await _userManager.CreateAsync(adminUser, "Abc&123!");


            // Step 3: Add this new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());


            //Step 1 repeat: Creates a  new instance of  BlogUser to be used as moderator

            var modUser = new BlogUser()
            {
                Email = "developmentjai@gmail.com",
                UserName = "developmentjai@gmail.com",
                FirstName = "Joy",
                LastName = "Dutta",
                PhoneNumber = "+447828101808",
                EmailConfirmed = true,

            };

            // Step 2 repeat: Use the UserManager to create a new user that is defined by the modUser variable
            await _userManager.CreateAsync(modUser, "Abc&123!");


            // Step 3 repeat: Add this new user to the Moderator role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());


        }




    }
}
