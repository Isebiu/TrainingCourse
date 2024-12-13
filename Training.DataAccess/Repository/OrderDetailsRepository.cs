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
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly AppDbContext _db;

        public OrderDetailsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            //var objFromDb  = _db.OrderDetails.FirstOrDefault(x => x.Id == orderDetails.Id);
            //objFromDb.Name = orderDetails.Name;
            //objFromDb.Count = orderDetails.Count;
            _db.OrderDetails.Update(orderDetails);
        }
    }
}
