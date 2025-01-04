using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Data;
using Training.Models;
using Training.Utility;

namespace Training.DataAccess.DbInit
{
    public class DbInit : IDbInit
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInit(AppDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }
        public void Init()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            if (!_roleManager.RoleExistsAsync(SD.KitchenRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.KitchenRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ManagerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.FrontDeskRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CutromerRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    EmailConfirmed = true,
                    FisrtName = "Sebi",
                    LastName = "Admin"
                }, "Admin123!").GetAwaiter().GetResult();

                AppUser user = _db.AppUsers.FirstOrDefault(u => u.Email == "admin@domain.com");
                _userManager.AddToRoleAsync(user, SD.ManagerRole).GetAwaiter().GetResult();
            }
            return;
        }

    }
}
