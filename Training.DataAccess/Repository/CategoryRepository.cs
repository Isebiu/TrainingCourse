using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(u=>u.Id ==category.Id); //ef va inmagazina si urmari acel obj din db si se va schimba (ca si cum ar fi referinta)
            objFromDb.Name = category.Name; //Update the name property
            objFromDb.Order = category.Order;//update the Order property
        }
    }
}
