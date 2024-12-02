using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.DataAccess.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly AppDbContext _db;

        public MenuItemRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }
     
        public void Update(MenuItem menuItem)
        {
            var objFromDb = _db.MenuItems.FirstOrDefault(u=>u.Id == menuItem.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = menuItem.Name;
                objFromDb.Description = menuItem.Description;
                objFromDb.Price = menuItem.Price;
                objFromDb.CategoryId = menuItem.CategoryId;
                objFromDb.FoodTypeId= menuItem.FoodTypeId;
                if (menuItem.Image != null)
                {
                    objFromDb.Image = menuItem.Image;
                }
            }
        }
    }
}
