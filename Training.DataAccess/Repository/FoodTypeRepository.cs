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
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly AppDbContext _db;

        public FoodTypeRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }
     
        public void Update(FoodType foodType)
        {
            var objFromDb = _db.FoodTypes.FirstOrDefault(u=>u.Id == foodType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = foodType.Name;
 
            }
        }
    }
}
