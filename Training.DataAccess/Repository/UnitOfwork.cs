using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;

namespace Training.DataAccess.Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; private set; }

        public UnitOfwork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            FoodType = new FoodTypeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
